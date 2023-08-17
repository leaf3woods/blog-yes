using Autofac;
using Autofac.Extensions.DependencyInjection;
using BlogYes.Infrastructure.DbContexts;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using Npgsql;
using System.Reflection;
using BlogYes.Application.Utilities;
using BlogYes.Core.Utilities;

var builder = WebApplication.CreateBuilder(args);
SettingUtil.Initialize(builder.Configuration);
EncryptUtil.Initialize(SettingUtil.Jwt.KeyFolder);
// Change container to autoFac
builder.Host.UseServiceProviderFactory(new AutofacServiceProviderFactory());
builder.Host.ConfigureContainer<ContainerBuilder>(config =>
    config.RegisterAssemblyModules(Assembly.GetExecutingAssembly(), Assembly.Load("BlogYes." + nameof(BlogYes.Application))));

// Add services to the container.
builder.Services.AddLogging();

builder.Services.AddRouting(options => options.LowercaseUrls = true);
builder.Services.AddControllers().AddJsonOptions(config =>
{
    config.JsonSerializerOptions.DefaultIgnoreCondition = System.Text.Json.Serialization.JsonIgnoreCondition.WhenWritingNull;
    config.JsonSerializerOptions.PropertyNameCaseInsensitive = false;
});

builder.Services.AddAuthentication(options =>
{
    options.DefaultScheme = JwtBearerDefaults.AuthenticationScheme;
    options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
}).AddJwtBearer(option =>
{
    option.TokenValidationParameters = new TokenValidationParameters
    {

        ValidateIssuerSigningKey = true,
        IssuerSigningKey = new ECDsaSecurityKey(EncryptUtil.ECDsa),    // Use ECDsa
        ValidAlgorithms = new[] { SecurityAlgorithms.EcdsaSha256 },
        ValidateIssuer = true,
        ValidIssuer = SettingUtil.Jwt.Issuer,
        ValidateAudience = true,
        ValidAudience = SettingUtil.Jwt.Audience,
        RequireExpirationTime = true,
        ValidateLifetime = true,
        ClockSkew = SettingUtil.Jwt.ExpireMin
    };
});

builder.Services.AddAuthorization(options =>
    options.AddPolicy("super", new Microsoft.AspNetCore.Authorization.AuthorizationPolicy())
);

// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

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

app.Run();