namespace HubPoint.Services.Common.Contracts.Requests;

public record GetUser
{
    public string UserName { get; init; } = default!;
}

public record GetUserResult
{
    public Guid UserId { get; init; }
    public string UserName { get; init; } = default!;
}