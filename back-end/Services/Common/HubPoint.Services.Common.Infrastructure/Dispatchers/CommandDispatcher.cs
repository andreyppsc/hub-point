using System.Text.Json;
using HubPoint.Services.Common.Abstractions.Commands;
using RabbitMQ.Client;

namespace HubPoint.Services.Common.Infrastructure.Dispatchers;

public class CommandDispatcher : ICommandDispatcher
{
    private readonly IConnection _connection;

    public CommandDispatcher(IConnection connection)
    {
        _connection = connection;
    }

    public Task Send(ICommand command, CancellationToken cancellationToken = default)
    {
        using var channel = _connection.CreateModel();
        channel.QueueDeclare("test", false, false, true, null);

        var message = JsonSerializer.Serialize(command, command.GetType(), new JsonSerializerOptions
        {
            PropertyNamingPolicy = JsonNamingPolicy.CamelCase
        });
        var body = Encoding.UTF8.GetBytes(message);
        
        channel.BasicPublish(string.Empty, "test", body: body);
        
        return Task.CompletedTask;
    }

    public Task<TResponse> Send<TResponse>(ICommand<TResponse> command, CancellationToken cancellationToken = default)
    {
        throw new NotImplementedException();
    }
}