using System.Text;
using System.Text.Json;
using Hospital.Application.Common.Models;
using Microsoft.Extensions.Options;
using RabbitMQ.Client;

namespace Hospital.Application.Common.Helpers.Queue;

public class QueueHelper
{
    private readonly RabbitMQSetting _rabbitMqSettings;

    public QueueHelper(IOptions<RabbitMQSetting> rabbitMqSettings)
    {
        _rabbitMqSettings = rabbitMqSettings.Value;
    }

    public void Send(object model)
    {
        var factory = new ConnectionFactory();

        factory.Uri = new Uri(_rabbitMqSettings.CloudUri);
        
        using (IConnection connection = factory.CreateConnection())
        using (IModel channel = connection.CreateModel())
        {
            channel.QueueDeclare(_rabbitMqSettings.RoutingKey, durable: true, exclusive: false, autoDelete: false);
            
            var options = new JsonSerializerOptions
            {
                PropertyNamingPolicy = JsonNamingPolicy.CamelCase,
            };
            
            string message = JsonSerializer.Serialize(model, options);
            var body = Encoding.UTF8.GetBytes(message);

            channel.BasicPublish(exchange: String.Empty,
                routingKey: _rabbitMqSettings.RoutingKey,
                body: body);
        }
    }
}