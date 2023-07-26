using HubPoint.Services.Security.Api.Domain;
using HubPoint.Services.Security.Api.Domain.Events;
using MassTransit;
using Microsoft.EntityFrameworkCore;

namespace HubPoint.Services.Security.Api.Persistence;

public class AppDbContext : DbContext
{
    private readonly IPublishEndpoint _publishEndpoint;
    private readonly ILogger<AppDbContext> _logger;

    public DbSet<User> Users { get; set; } = default!;
    
    public AppDbContext(DbContextOptions<AppDbContext> options, IPublishEndpoint publishEndpoint, ILogger<AppDbContext> logger) : base(options)
    {
        _publishEndpoint = publishEndpoint;
        _logger = logger;
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
                _logger.LogInformation("publishing event {Event}", domainEvent.GetType().Name);
                await _publishEndpoint.Publish(domainEvent, cancellationToken);
                //await _bus.Publish(@event, cancellationToken);
            }
            
            entry.Entity.ClearDomainEvents();;
        }
        
        // await Task.Delay(10000, cancellationToken);

        return await base.SaveChangesAsync(cancellationToken);
    }
}