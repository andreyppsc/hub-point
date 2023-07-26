using HubPoint.Services.Security.Api.Domain.Events;
using MassTransit;

namespace HubPoint.Services.Security.Api.Domain;

public class User : EntityBase
{
    public Guid UserId { get; private set; }

    public User()
    {
        UserId = NewId.NextGuid();

        AddDomainEvent(new UserCreated { UserId = UserId });
    }
}