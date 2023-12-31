using Autofac;
using Autofac.Extensions.DependencyInjection;
using BlogYes.Application.Auth;
using BlogYes.Application.Utilities;
using BlogYes.Core;
using BlogYes.Core.Utilities;
using BlogYes.Domain.Utilities;
using BlogYes.Infrastructure.DbContexts;
using BlogYes.WebApi.Utilities;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Diagnostics;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using Microsoft.OpenApi.Models;
using Npgsql;
using Serilog;
using System.Net;
using System.Reflection;

var builder = WebApplication.CreateBuilder(args);

#region util Initialize

RequireScopeUtil.Initialize();
SettingUtil.Initialize(builder.Configuration);
CryptoUtil.Initialize(SettingUtil.Jwt.KeyFolder);

#endregion util Initialize

// Change container to autoFac
builder.Host.UseServiceProviderFactory(new AutofacServiceProviderFactory());
builder.Host.ConfigureContainer<ContainerBuilder>(config =>
    config.RegisterAssemblyModules(Assembly.GetExecutingAssembly(), Assembly.Load("BlogYes." + nameof(BlogYes.Application))));

// Add services to the container.
builder.Host.UseSerilog((context, logger) =>
{
    logger.ReadFrom.Configuration(context.Configuration);
    logger.Enrich.FromLogContext();
});

builder.Services.AddLogging();
builder.Services.AddRouting(options => options.LowercaseUrls = true);
builder.Services.AddControllers().AddJsonOptions(config =>
{
    config.JsonSerializerOptions.DefaultIgnoreCondition = Options.CustomJsonSerializerOptions.DefaultIgnoreCondition;
    config.JsonSerializerOptions.PropertyNameCaseInsensitive = Options.CustomJsonSerializerOptions.PropertyNameCaseInsensitive;
});
builder.Services.AddHttpContextAccessor();
builder.Services.AddAuthentication(options =>
{
    options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
    options.DefaultScheme = JwtBearerDefaults.AuthenticationScheme;
    options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
}).AddJwtBearer(option =>
{
    option.TokenValidationParameters = new TokenValidationParameters
    {
        ValidateIssuerSigningKey = true,
        IssuerSigningKey = new ECDsaSecurityKey(CryptoUtil.PublicECDsa),    // Use ECDsa
        ValidAlgorithms = new[] { SecurityAlgorithms.EcdsaSha256 },
        ValidateIssuer = true,
        ValidIssuer = SettingUtil.Jwt.Issuer,
        ValidateAudience = true,
        ValidAudience = SettingUtil.Jwt.Audience,
        RequireExpirationTime = true,
        ValidateLifetime = true
    };
});

builder.Services.AddLocalization();

builder.Services.AddAuthorization(options =>
    options.AddPolicyExt(RequireScopeUtil.Scopes)
);

// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen(option =>
{
    var xmlFilename = $"{Assembly.GetExecutingAssembly().GetName().Name}.xml";
    option.IncludeXmlComments(Path.Combine(AppContext.BaseDirectory, xmlFilename));
    option.SwaggerDoc("v1", new OpenApiInfo
    {
        Description = "apis for bog yes system",
        Title = "blog yes!",
        Contact = new OpenApiContact
        {
            Name = "yes",
            Email = "en-yes@outlook.com",
            Url = new Uri("https://github.com/leaf3woods")
        }
    });
    option.AddSecurityDefinition(JwtBearerDefaults.AuthenticationScheme, new OpenApiSecurityScheme
    {
        In = ParameterLocation.Header,
        Type = SecuritySchemeType.Http,
        Description = "",
        Name = "Authentication",
        Scheme = JwtBearerDefaults.AuthenticationScheme,
        
    });
    option.AddSecurityRequirement(new OpenApiSecurityRequirement
    {
        {
            new OpenApiSecurityScheme
            {
                In = ParameterLocation.Header,
                Type = SecuritySchemeType.Http,
                Scheme = JwtBearerDefaults.AuthenticationScheme,
                Reference = new OpenApiReference
                {
                    Type = ReferenceType.SecurityScheme,
                    Id = JwtBearerDefaults.AuthenticationScheme
                }
            },
            Array.Empty<string>()
        },
    });
});

// Add dbContext pool
builder.Services.AddPooledDbContextFactory<PgDbContext>(options => options.UseNpgsql(
    new NpgsqlDataSourceBuilder(builder.Configuration.GetConnectionString("Postgres")).Build()
    ).EnableDetailedErrors());

// Add mapper profiles
builder.Services.AddAutoMapper(config => config.AddMaps(Assembly.Load("BlogYes." + nameof(BlogYes.Application))));

// Add mediatR
builder.Services.AddMediatR(config =>
    config.RegisterServicesFromAssemblies(Assembly.Load("BlogYes." + nameof(BlogYes.Application))));

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseAuthentication();
app.UseAuthorization();

app.MapControllers();

app.Services.GetService<InitialDatabase>()?.Initialize();

app.UseExceptionHandler(builder =>
    builder.Run(async context =>
        await ExceptionLocalizerExtension.LocalizeException(context)));

app.Run();
