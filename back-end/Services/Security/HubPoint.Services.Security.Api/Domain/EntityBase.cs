using HubPoint.Services.Common.Contracts.Events;

namespace HubPoint.Services.Security.Api.Domain;

public abstract class EntityBase
{
    private List<object>? _domainEvents;
    public IReadOnlyCollection<object>? DomainEvents => _domainEvents?.AsReadOnly();

    public void AddDomainEvent(object @event)
    {
        _domainEvents ??= new List<object>();
        _domainEvents.Add(@event);
    }

    public void ClearDomainEvents()
    {
        _domainEvents?.Clear();;
    }
}