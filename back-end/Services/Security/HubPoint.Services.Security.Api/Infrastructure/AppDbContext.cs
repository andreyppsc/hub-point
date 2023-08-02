using HubPoint.Services.Common.Abstractions.Domain;
using HubPoint.Services.Security.Api.Domain;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace HubPoint.Services.Security.Api.Infrastructure;

public class AppDbContext : DbContext
{
    private readonly IMediator _mediator;

    public DbSet<User> Users { get; set; } = default!;
    
    public AppDbContext(DbContextOptions<AppDbContext> options, IMediator mediator) : base(options)
    {
        _mediator = mediator;
    }

    public override async Task<int> SaveChangesAsync(CancellationToken cancellationToken = default)
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