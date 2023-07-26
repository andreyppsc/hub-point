using HubPoint.Services.Common.Abstractions.Commands;
using HubPoint.Services.Security.Api.Domain;
using HubPoint.Services.Security.Api.Domain.Events;
using HubPoint.Services.Security.Api.Persistence;
using MassTransit;

namespace HubPoint.Services.Security.Api.Application;

public class UserDto
{
    public Guid UserId { get; set; }

    public UserDto()
    {
        
    }

    public UserDto(Guid userId)
    {
        UserId = userId;
    }
}

public class CreateUserCommand
{
    
}

public class CreateUserCommandHandler : IConsumer<CreateUserCommand>
{
    private readonly ILogger<CreateUserCommandHandler> _logger;
    private readonly AppDbContext _context;

    public CreateUserCommandHandler(ILogger<CreateUserCommandHandler> logger, AppDbContext context)
    {
        _logger = logger;
        _context = context;
    }

    public async Task Consume(ConsumeContext<CreateUserCommand> context)
    {
        _logger.LogInformation("handling command");

        var user = new User();

        _context.Users.Add(user);

        //await context.Publish<UserCreated>(new { user.UserId, __CorrelationId = user.UserId });

        _logger.LogInformation("wait 5 seconds then save to db");
        await Task.Delay(5000);

        await _context.SaveChangesAsync(context.CancellationToken);
        
        _logger.LogInformation("wait 5 seconds before replying");
        await Task.Delay(5000);

        await context.RespondAsync(new UserDto(user.UserId));
    }
}