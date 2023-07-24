using HubPoint.Services.Common.Contracts.Events;
using MassTransit;

namespace HubPoint.Services.Security.Api.Domain.Events;

public class UserCreated
{
    public Guid UserId { get; set; }
}

public class UserCreatedHandler : IConsumer<UserCreated>
{
    private readonly ILogger<UserCreatedHandler> _logger;
    private readonly IBus _bus;

    public UserCreatedHandler(ILogger<UserCreatedHandler> logger, IBus bus)
    {
        _logger = logger;
        _bus = bus;
    }

    public async Task Consume(ConsumeContext<UserCreated> context)
    {
        _logger.LogInformation("handling domain event with id {UserId}", context.Message.UserId);
        //await context.Publish(new UserCreatedIntegrationEvent { UserId = context.Message.UserId });
        await _bus.Publish(new UserCreatedIntegrationEvent { UserId = context.Message.UserId });
    }
}