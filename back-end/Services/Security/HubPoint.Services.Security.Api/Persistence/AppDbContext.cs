using HubPoint.Services.Security.Api.Domain;
using HubPoint.Services.Security.Api.Domain.Events;
using MassTransit;
using Microsoft.EntityFrameworkCore;

namespace HubPoint.Services.Security.Api.Persistence;

internal class AppDbContext : DbContext
{
    private readonly IPublishEndpoint _publishEndpoint;
    private readonly IBus _bus;

    public DbSet<User> Users { get; set; } = default!;
    
    public AppDbContext(DbContextOptions<AppDbContext> options, IPublishEndpoint publishEndpoint, IBus bus) : base(options)
    {
        _publishEndpoint = publishEndpoint;
        _bus = bus;
    }

    /*public override async Task<int> SaveChangesAsync(CancellationToken cancellationToken = default)
    {
        var entries = ChangeTracker
            .Entries<EntityBase>()
            .Where(x => x.Entity.DomainEvents?.Count > 0);

        foreach (var entry in entries)
        {
            foreach (var @event in entry.Entity.DomainEvents!)
            {
                await _publishEndpoint.Publish(@event, cancellationToken);
                //await _bus.Publish(@event, cancellationToken);
            }
            
            entry.Entity.ClearDomainEvents();;
        }
        
        // await Task.Delay(10000, cancellationToken);

        return await base.SaveChangesAsync(cancellationToken);
    }*/
}