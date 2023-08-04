using HubPoint.Services.Common.Abstractions.Domain;
using Microsoft.EntityFrameworkCore;

namespace HubPoint.Services.Common.Infrastructure.Persistence;

public abstract class DbContextBase : DbContext
{
    private readonly IMediator _mediator;

    protected DbContextBase(DbContextOptions options, IMediator mediator) : base(options)
    {
        _mediator = mediator;
    }

    public override async Task<int> SaveChangesAsync(CancellationToken cancellationToken = new CancellationToken())
    {
        var entries = ChangeTracker
            .Entries<EntityBase>()
            .Where(x => x.Entity.DomainEvents?.Count > 0);

        foreach (var entry in entries)
        {
            foreach (var domainEvent in entry.Entity.DomainEvents!)
            {
                await _mediator.Publish(domainEvent, cancellationToken);
            }
            
            entry.Entity.ClearDomainEvents();;
        }
        
        return await base.SaveChangesAsync(cancellationToken);
    }
}