using System.Reflection;
using EasyNetQ.AutoSubscribe;
using EasyNetQ.DI;
using HubPoint.Services.Common.Infrastructure.Jwt;
using HubPoint.Services.Common.Infrastructure.Outbox;

namespace HubPoint.Services.Common.Infrastructure;

public static class Extensions
{
    public static void AddInfrastructure(this IServiceCollection services, Action<ServiceConfiguration> configuration)
    {
        var assembly = Assembly.GetCallingAssembly();
        
        var serviceConfiguration = new ServiceConfiguration();
        configuration.Invoke(serviceConfiguration);

        if (serviceConfiguration.UseInMemoryOutbox)
        {
            services.AddScoped<IOutbox, InMemoryOutbox>();
        }
        
        services.AddMediatR(c =>
        {
            c.NotificationPublisherType = typeof(IntegrationEventsPublisher);

            foreach (var openBehavior in serviceConfiguration.MediatorOpenBehaviors)
            {
                c.AddOpenBehavior(openBehavior);
            }
            
            foreach (var preProcessor in serviceConfiguration.MediatorPreProcessors)
            {
                c.AddOpenRequestPreProcessor(preProcessor);
            }
            
            foreach (var postProcessor in serviceConfiguration.MediatorPostProcessors)
            {
                c.AddOpenRequestPostProcessor(postProcessor);
            }
            
            c.AddOpenRequestPostProcessor(typeof(OutboxProcessor<,>));

            c.RegisterServicesFromAssembly(assembly);
        });

        var eventHandlerTypes = assembly.GetTypes()
            .Where(x => x.GetInterfaces().Any(i => i.IsGenericType && i.GetGenericTypeDefinition() == typeof(IIntegrationEventHandler<>)));
        
        foreach (var handlerType in eventHandlerTypes)
        {
            services.AddTransient(handlerType);
        }

        services.RegisterEasyNetQ(serviceConfiguration.RabbitMqConnectionString, s =>
        {
            s.EnableSystemTextJson();
        });

        services.AddJwtAuthentication();
    }
}