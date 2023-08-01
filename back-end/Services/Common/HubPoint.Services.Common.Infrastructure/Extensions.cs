using RabbitMQ.Client;

namespace HubPoint.Services.Common.Infrastructure;

public static class Extensions
{
    public static void AddRabbitMq(this IServiceCollection services)
    {
        var factory = new ConnectionFactory
        {
            UserName = "hub-point",
            Password = "hub-point",
            VirtualHost = "hub-point",
            HostName = "172.29.14.167"
        };

        services.AddSingleton<IConnection>(_ => factory.CreateConnection());
    }
}