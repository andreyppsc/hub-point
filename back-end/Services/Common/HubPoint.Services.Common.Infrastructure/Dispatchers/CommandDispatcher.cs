using HubPoint.Services.Common.Abstractions.Commands;
using MassTransit.Mediator;

namespace HubPoint.Services.Common.Infrastructure.Dispatchers;

public class CommandDispatcher : ICommandDispatcher
{
    private readonly IMediator _mediator;

    public CommandDispatcher(IMediator mediator)
    {
        _mediator = mediator;
    }

    public async Task Send(ICommand command, CancellationToken cancellationToken = default)
    {
        await _mediator.Send(command, cancellationToken);
    }

    public async Task<TResponse> Send<TResponse>(ICommand<TResponse> command, CancellationToken cancellationToken = default) where TResponse : class
    {
        var client = _mediator.CreateRequestClient<ICommand<TResponse>>();
        var response = await client.GetResponse<TResponse>(command, cancellationToken);
        return response.Message;
    }
}