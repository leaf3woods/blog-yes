using Autofac;
using Autofac.Extensions.DependencyInjection;
using BlogYes.Infrastructure.DbContexts;
using Microsoft.EntityFrameworkCore;
using Npgsql;
using System.Reflection;

var builder = WebApplication.CreateBuilder(args);

// Change container to autofac
builder.Host.UseServiceProviderFactory(new AutofacServiceProviderFactory());
builder.Host.ConfigureContainer<ContainerBuilder>(config =>
    config.RegisterAssemblyModules(Assembly.GetExecutingAssembly()));

// Add services to the container.
builder.Services.AddControllers();

// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

// Add dbContext pool
builder.Services.AddPooledDbContextFactory<PgDbContext>(options =>
    new NpgsqlDataSourceBuilder(builder.Configuration.GetConnectionString("pgsql")).Build());

// Add mapper profiles
builder.Services.AddAutoMapper(config => config.AddMaps(Assembly.GetExecutingAssembly()));

// Add mediatR
builder.Services.AddMediatR(config =>
    config.RegisterServicesFromAssemblies(Assembly.GetExecutingAssembly()));

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();