using HubPoint.Services.Common.Abstractions.Commands;
using MassTransit;

namespace HubPoint.Services.Common.Infrastructure.Dispatchers;

public class CommandDispatcher : ICommandDispatcher
{
    private readonly IBus _bus;
    private readonly IServiceProvider _provider;

    public CommandDispatcher(IBus bus, IServiceProvider provider)
    {
        _bus = bus;
        _provider = provider;
    }

    public async Task Send(ICommand command, CancellationToken cancellationToken = default)
    {
        var endpoint  = await _bus.GetSendEndpoint(_bus.Address);
        await endpoint.Send(command, cancellationToken);
    }

    public async Task<TResponse> Send<TResponse>(ICommand<TResponse> command, CancellationToken cancellationToken = default) where TResponse : class
    {
        var clientFactory = _provider.GetRequiredService<IClientFactory>();
        var client = clientFactory.CreateRequestClient<ICommand<TResponse>>(_bus.Address);
        var response = await client.GetResponse<TResponse>(command, cancellationToken);
        return response.Message;
    }
}