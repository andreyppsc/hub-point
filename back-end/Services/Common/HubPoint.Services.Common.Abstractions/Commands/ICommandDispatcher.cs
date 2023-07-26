namespace HubPoint.Services.Common.Abstractions.Commands;

public interface ICommandDispatcher
{
    Task Send(ICommand command, CancellationToken cancellationToken = default);

    Task<TResponse> Send<TResponse>(ICommand<TResponse> command, CancellationToken cancellationToken = default) where TResponse : class;
}