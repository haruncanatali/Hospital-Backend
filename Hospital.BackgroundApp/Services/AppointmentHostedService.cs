using System.Text;
using System.Text.Json;
using Hospital.Application.Common.Models;
using Hospital.Application.Common.Models.Queue;
using Microsoft.Extensions.Options;
using RabbitMQ.Client;
using RabbitMQ.Client.Events;

namespace Hospital.BackgroundApp.Services;

public class AppointmentConsumer
{
    private readonly RabbitMQSetting _rabbitMqSetting;
    private readonly IServiceScopeFactory _scopeFactory;
    private IConnection _connection;
    private readonly JsonSerializerOptions options = new() { PropertyNamingPolicy = JsonNamingPolicy.CamelCase };
    
    public AppointmentConsumer(IOptions<RabbitMQSetting> rabbitMqSettings, IServiceScopeFactory scopeFactory)
    {
        _rabbitMqSetting = rabbitMqSettings.Value;
        _scopeFactory = scopeFactory;

        var factory = new ConnectionFactory
        {
            Uri = new Uri(_rabbitMqSetting.CloudUri)
        };

        _connection = factory.CreateConnection();
    }
    
    public Task ExecuteAsync()
    {
        var channel = _connection.CreateModel();
        channel.BasicQos(0, 1, false);

        var appointmentRequestConsumer = new EventingBasicConsumer(channel);
        AppointmentRequestConsumer(appointmentRequestConsumer, channel);
        
        return Task.CompletedTask;
    }
    
    private void AppointmentRequestConsumer(EventingBasicConsumer heatConsumer, IModel channel)
    {
        heatConsumer.Received += async (ch, ea) =>
        {
            using var scope = _scopeFactory.CreateScope();
            IAppointmentDeclarationService declarationService = scope.ServiceProvider.GetService<IAppointmentDeclarationService>()!;
            var content = Encoding.UTF8.GetString(ea.Body.ToArray());
            AddAppointmentRequestModel model =
                JsonSerializer.Deserialize<AddAppointmentRequestModel>(content, options)!;
            await declarationService!.AddAppointment(model!);
            channel.BasicAck(ea.DeliveryTag, false);
        };

        channel.BasicConsume(queue: _rabbitMqSetting.RoutingKey, false, heatConsumer);
    }
}

public class AppointmentHostedService : BackgroundService, IDisposable
{
    private readonly IServiceScopeFactory _scopeFactory;

    public AppointmentHostedService(IServiceScopeFactory scopeFactory)
    {
        _scopeFactory = scopeFactory;
    }

    protected override async Task ExecuteAsync(CancellationToken cancellationToken)
    {
        cancellationToken.ThrowIfCancellationRequested();

        using var scope = _scopeFactory.CreateScope();
        AppointmentConsumer service = scope.ServiceProvider.GetService<AppointmentConsumer>()!;

        await service.ExecuteAsync();
    }
}