namespace HubPoint.Services.Identity.Api.Domain.Integration;

public class User
{
    public Guid UserId { get; set; }

    public string UserName { get; set; } = default!;
}