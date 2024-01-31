using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using RabbitMQ.Client;

namespace Hospital.Application.Common.Managers;

public static class RabbitMqManager
{
    public static IServiceCollection AddRabbitMqQueue(this IServiceCollection services, IConfiguration configuration,
        string cloudUri,string routingKey)
    {
        var factory = new ConnectionFactory();

        factory.Uri = new Uri(cloudUri);
        
        using (IConnection connection = factory.CreateConnection())
        using (IModel channel = connection.CreateModel())
        {
            channel.QueueDeclare(routingKey, durable: true, exclusive: false, autoDelete: false);
            channel.BasicQos(prefetchSize:0, prefetchCount:1, global:false);
        }

        return services;
    }
}