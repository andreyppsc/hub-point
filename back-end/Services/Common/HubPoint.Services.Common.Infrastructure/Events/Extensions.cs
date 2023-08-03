using EasyNetQ;
using EasyNetQ.AutoSubscribe;
using Microsoft.AspNetCore.Builder;

namespace HubPoint.Services.Common.Infrastructure.Events;

public static class Extensions
{
    public static IApplicationBuilder AddSubscribers(this IApplicationBuilder app, params Type[] types)
    {
        var bus = app.ApplicationServices.GetRequiredService<IBus>();
        var subscriber = new AutoSubscriber(bus, "test");
        subscriber.SubscribeAsync(types);

        return app;
    }
}