using HubPoint.Services.Common.Abstractions.Events;

namespace HubPoint.Services.Security.Events;

public class UserCreated : IDomainEvent, IIntegrationEvent
{
    public Guid UserId { get; set; }
}