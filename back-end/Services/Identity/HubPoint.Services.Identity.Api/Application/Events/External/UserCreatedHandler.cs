using EasyNetQ.AutoSubscribe;
using HubPoint.Services.Common.Abstractions.Events;
using HubPoint.Services.Identity.Api.Domain.Integration;
using HubPoint.Services.Identity.Api.Infrastructure.Persistence;
using HubPoint.Services.Security.Events;

namespace HubPoint.Services.Identity.Api.Application.Events.External;

public class UserCreatedHandler : IIntegrationEventHandler<UserCreated>
{
    private readonly ILogger<UserCreatedHandler> _logger;
    private readonly IdentityDbContext _context;

    public UserCreatedHandler(ILogger<UserCreatedHandler> logger, IdentityDbContext context)
    {
        _logger = logger;
        _context = context;
    }

    public async Task ConsumeAsync(UserCreated message, CancellationToken cancellationToken = default)
    {
        _context.Users.Add(new User { UserId = message.UserId, UserName = message.UserName });
        await _context.SaveChangesAsync(cancellationToken);
        _logger.LogInformation("test");
    }
}