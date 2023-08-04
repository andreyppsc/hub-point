using HubPoint.Services.Common.Infrastructure.Persistence;
using HubPoint.Services.Security.Api.Domain;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace HubPoint.Services.Security.Api.Infrastructure;

public class SecurityDbContext : DbContextBase
{
    public DbSet<User> Users { get; set; } = default!;
    
    public SecurityDbContext(DbContextOptions<SecurityDbContext> options, IMediator mediator) : base(options, mediator)
    {

    }
}