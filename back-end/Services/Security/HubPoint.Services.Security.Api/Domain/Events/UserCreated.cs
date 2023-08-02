using HubPoint.Services.Common.Abstractions.Events;

// ReSharper disable once CheckNamespace
namespace HubPoint.Services.IntegrationEvents;

public class UserCreated : IDomainEvent, IIntegrationEvent
{
    public Guid UserId { get; set; }
}