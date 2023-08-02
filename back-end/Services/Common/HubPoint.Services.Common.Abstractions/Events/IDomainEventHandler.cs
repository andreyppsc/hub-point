using MediatR;

namespace HubPoint.Services.Common.Abstractions.Events;

public interface IDomainEventHandler<in TEvent> : INotificationHandler<TEvent>
    where TEvent : INotification { }