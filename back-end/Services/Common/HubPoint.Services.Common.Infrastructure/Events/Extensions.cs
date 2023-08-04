using System.Reflection;
using EasyNetQ.AutoSubscribe;
using EasyNetQ.DI;

namespace HubPoint.Services.Common.Infrastructure.Events;

public static class Extensions
{
    public static IApplicationBuilder UseIntegrationEvents(this IApplicationBuilder app, string subscriptionPrefix)
    {
        var assembly = Assembly.GetCallingAssembly();
        
        var eventHandlerTypes = assembly.GetTypes()
            .Where(x => x.GetInterfaces().Any(i => i.IsGenericType && i.GetGenericTypeDefinition() == typeof(IIntegrationEventHandler<>)));
        
        var bus = app.ApplicationServices.GetRequiredService<IBus>();
        var subscriber = new AutoSubscriber(bus, subscriptionPrefix)
        {
            AutoSubscriberMessageDispatcher = new DefaultAutoSubscriberMessageDispatcher(app.ApplicationServices.GetService<IServiceResolver>())
        };
        
        subscriber.SubscribeAsync(eventHandlerTypes.ToArray());
        
        return app;
    }
}