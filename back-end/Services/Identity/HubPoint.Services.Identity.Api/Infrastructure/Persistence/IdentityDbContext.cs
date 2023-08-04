using HubPoint.Services.Common.Infrastructure.Persistence;
using HubPoint.Services.Identity.Api.Domain.Integration;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace HubPoint.Services.Identity.Api.Infrastructure.Persistence;

public class IdentityDbContext : DbContextBase
{
    public DbSet<User> Users { get; set; } = default!;
    
    public IdentityDbContext(DbContextOptions options, IMediator mediator) : base(options, mediator)
    {
        
    }
}