using HubPoint.Services.Common.Abstractions.Events;

namespace HubPoint.Services.Security.Api.Domain.Events;

public class UserCreated : IDomainEvent, IIntegrationEvent
{
    public Guid UserId { get; set; }
}