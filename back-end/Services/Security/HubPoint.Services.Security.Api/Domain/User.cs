using HubPoint.Services.Common.Abstractions.Domain;
using HubPoint.Services.Security.Api.Domain.Events;

namespace HubPoint.Services.Security.Api.Domain;

public class User : EntityBase
{
    public Guid UserId { get; private set; }
    public string UserName { get; private set; }
    public string HashedPassword { get; private set; }
    
#pragma warning disable CS8618
    private User() { }
#pragma warning restore CS8618

    public User(string userName, string hashedPassword)
    {
        UserId = Guid.NewGuid();
        UserName = userName;
        HashedPassword = hashedPassword;
        
        AddDomainEvent(new UserCreated { UserId = UserId });
    }
}