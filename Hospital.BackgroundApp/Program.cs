using Hospital.Application;
using Hospital.Application.Common.Interfaces;
using Hospital.Application.Common.Managers;
using Hospital.BackgroundApp.Configs;
using Hospital.BackgroundApp.Consumers;
using Hospital.BackgroundApp.Extensions;
using Hospital.BackgroundApp.Services;
using Hospital.Persistence;
using Microsoft.AspNetCore.Diagnostics;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddPersistence(builder.Configuration);
builder.Services.AddApplication();
builder.Services.AddAuthenticationConfig(builder.Configuration);
builder.Services.AddSettingsConfig(builder.Configuration);
builder.Services.AddLogging();
builder.Services.AddHttpContextAccessor();
builder.Services.AddTransient<ICurrentUserService, CurrentUserService>();

builder.Services.AddRabbitMqQueue(builder.Configuration,builder.Configuration.GetValue<string>("RabbitMQSetting:CloudUri"),builder.Configuration.GetValue<string>("RabbitMQSetting:RoutingKey"));

builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Services.AddTransient<IAppointmentDeclarationService, AppointmentDeclarationService>();
builder.Services.AddTransient<AppointmentConsumer>();
builder.Services.AddHostedService<AppointmentHostedService>();

builder.Services.AddHttpContextAccessor();
builder.Services.AddHttpClient();

var app = builder.Build();
AppContext.SetSwitch("Npgsql.EnableLegacyTimestampBehavior", true);

app.UseHttpLogging();
app.UseErrorHandlingMiddleware();

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.Run();