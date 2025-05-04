using FlujoApp.Api.Core.Interfaces;
using FlujoApp.Api.Core.Services;
using FlujoApp.Api.Infraestructure;
using FlujoApp.Api.Infraestructure.Repositories;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using FlujoApp.Api.Core.Services.Ejecutores;



var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddScoped<IFlujoService, FlujoService>();
builder.Services.AddScoped<IFlujoRepository, FlujoRepository>();

//builder.Services.AddScoped<RegistroUsuarioExecutor>();
//builder.Services.AddScoped<EnviarCorreoExecutor>();
builder.Services.AddScoped<IPasoExecutor, RegistroUsuarioExecutor>();
builder.Services.AddScoped<IPasoExecutor, EnviarCorreoExecutor>();
builder.Services.AddScoped<IPasoExecutorFactory, PasoExecutorFactory>();
//builder.Services.AddScoped<IFlujoService, FlujoService>();
//builder.Services.AddScoped<IFlujoRepository, FlujoRepository>();


//builder.Services.AddScoped<IPasoExecutor, RegistroUsuarioExecutor>();
//builder.Services.AddScoped<IPasoExecutor, EnviarCorreoExecutor>();

// Fábrica de ejecutores (nuevo interface)
//builder.Services.AddScoped<IPasoExecutorFactory, PasoExecutorFactory>();

builder.Services.AddDbContext<ApplicationDbContext>(options =>
    options.UseNpgsql(builder.Configuration.GetConnectionString("PostgresConnection")));




builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var app = builder.Build();

// Configure the HTTP request pipeline.


using (var scope = app.Services.CreateScope())
{
    var context = scope.ServiceProvider.GetRequiredService<ApplicationDbContext>();
    await context.Database.MigrateAsync(); // <-- Aquí
}

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
