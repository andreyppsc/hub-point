using HubPoint.Services.Common.Abstractions.Events;

namespace HubPoint.Services.Common.Abstractions.Domain;

public abstract class EntityBase
{
    private List<IDomainEvent>? _domainEvents;

    public IReadOnlyCollection<IDomainEvent>? DomainEvents => _domainEvents?.AsReadOnly();

    protected void AddDomainEvent(IDomainEvent domainEvent)
    {
        _domainEvents ??= new List<IDomainEvent>();
        _domainEvents.Add(domainEvent);
    }

    protected void RemoveDomainEvent(IDomainEvent domainEvent)
    {
        _domainEvents?.Remove(domainEvent);
    }

    public void ClearDomainEvents()
    {
        _domainEvents?.Clear();
    }
}