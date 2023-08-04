namespace HubPoint.Services.Security.Events;

[Queue("sec-user-created", ExchangeName = "sec-user-created")]
public class UserCreated : IDomainEvent, IIntegrationEvent
{
    public Guid UserId { get; init; }

    public string UserName { get; init; } = default!;
}