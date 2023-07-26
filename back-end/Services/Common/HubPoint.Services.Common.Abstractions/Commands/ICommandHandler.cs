namespace HubPoint.Services.Common.Abstractions.Commands;

public interface ICommandHandler<in TCommand> where TCommand : ICommand { }

public interface ICommandHandler<in TCommand, out TResponse>
    where TCommand : ICommand<TResponse>
    where TResponse : class
{
    
}