using EasyNetQ;
using HubPoint.Services.Common.Abstractions.Events;

// ReSharper disable once CheckNamespace
namespace HubPoint.Services.IntegrationEvents;

[Queue("user-created", ExchangeName = "user-created")]
public class UserCreated : IDomainEvent, IIntegrationEvent
{
    public Guid UserId { get; set; }
}