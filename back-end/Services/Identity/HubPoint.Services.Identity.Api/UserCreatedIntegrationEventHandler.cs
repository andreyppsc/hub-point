using HubPoint.Services.Common.Contracts.Events;

namespace HubPoint.Services.Identity.Api;

public class UserCreatedIntegrationEventHandler : IConsumer<UserCreatedIntegrationEvent>
{
    private readonly ILogger<UserCreatedIntegrationEventHandler> _logger;

    public UserCreatedIntegrationEventHandler(ILogger<UserCreatedIntegrationEventHandler> logger)
    {
        _logger = logger;
    }

    public async Task Consume(ConsumeContext<UserCreatedIntegrationEvent> context)
    {
        _logger.LogInformation("handling integration event - correlation id {CorrelationId}", context.CorrelationId);
    }
}