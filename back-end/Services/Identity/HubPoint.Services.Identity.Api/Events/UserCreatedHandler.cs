using EasyNetQ.AutoSubscribe;
using HubPoint.Services.Security.Events;

namespace HubPoint.Services.Identity.Api.Events;

public class UserCreatedHandler : IConsumeAsync<UserCreated>
{
    public async Task ConsumeAsync(UserCreated message, CancellationToken cancellationToken = new CancellationToken())
    {
        Console.WriteLine(message.UserId);
    }
}