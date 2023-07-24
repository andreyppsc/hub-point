using HubPoint.Services.Common.Abstractions.Commands;
using HubPoint.Services.Security.Api.Domain.Events;
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

public class CreateUserCommand : ICommand<UserDto>
{
    
}

public class CreateUserCommandHandler : CommandHandler<CreateUserCommand, UserDto>
{
    private readonly ILogger<CreateUserCommandHandler> _logger;
    private readonly IPublishEndpoint _publishEndpoint;

    public CreateUserCommandHandler(ILogger<CreateUserCommandHandler> logger, IPublishEndpoint publishEndpoint)
    {
        _logger = logger;
        _publishEndpoint = publishEndpoint;
    }

    protected override async Task<UserDto> Handle(CreateUserCommand request, CancellationToken cancellationToken)
    {
        _logger.LogInformation("handling command");

        var userId = Guid.NewGuid();

        await _publishEndpoint.Publish<UserCreated>(new { UserId = userId, __CorrelationId = userId }, cancellationToken);
        
        return new UserDto(userId);
    }
}