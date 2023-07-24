namespace HubPoint.Services.Common.Abstractions.Commands;

public interface ICommandDispatcher
{
    public Task Send(ICommand command, CancellationToken cancellationToken = default);

    public Task<TResponse> Send<TResponse>(ICommand<TResponse> command, CancellationToken cancellationToken = default)
        where TResponse : class;
}