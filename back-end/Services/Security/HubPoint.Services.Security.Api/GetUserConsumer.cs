using HubPoint.Services.Common.Contracts.Requests;
using MassTransit;

namespace HubPoint.Services.Security.Api;

public class GetUserConsumer : IConsumer<GetUser>
{
    private readonly ILogger<GetUserConsumer> _logger;

    public GetUserConsumer(ILogger<GetUserConsumer> logger)
    {
        _logger = logger;
    }

    public async Task Consume(ConsumeContext<GetUser> context)
    {
        _logger.LogInformation("Received this message: {Message}", context.Message);
        
        var usernames = new List<string>
        {
            "john.doe",
            "mary.smith",
            "james.wilson",
            "sarah.jones",
            "michael.brown",
            "linda.davis",
            "robert.miller",
            "emily.walker",
            "david.thompson",
            "olivia.harris",
            "andrei.popescu"
        };

        if (usernames.Contains(context.Message.UserName))
        {
            await context.RespondAsync<GetUserResult>(new
            {
                UserId = Guid.NewGuid(),
                context.Message.UserName
            });
        }
        else
        {
            throw new ConsumerMessageException("User doesn't exist");
        }
    }
}