using HubPoint.Services.Common.Abstractions.Events;
using MediatR;

namespace HubPoint.Services.Common.Infrastructure.Events;

public class IntegrationEventsPublisher : INotificationPublisher
{
    private readonly IOutbox _outbox;

    public IntegrationEventsPublisher(IOutbox outbox)
    {
        _outbox = outbox;
    }

    public async Task Publish(IEnumerable<NotificationHandlerExecutor> handlerExecutors, INotification notification, CancellationToken cancellationToken)
    {
        foreach (var handler in handlerExecutors)
        {
            if (notification is IIntegrationEvent integrationEvent)
            {
                _outbox.Add(integrationEvent);
            }
            
            await handler.HandlerCallback(notification, cancellationToken);
        }
    }
}