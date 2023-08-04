using EasyNetQ.AutoSubscribe;

namespace HubPoint.Services.Common.Abstractions.Events;

public interface IIntegrationEventHandler<in TEvent> : IConsumeAsync<TEvent>
    where TEvent : class, IIntegrationEvent { }