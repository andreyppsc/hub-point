using MassTransit.Mediator;

namespace HubPoint.Services.Common.Abstractions.Commands;

public abstract class CommandHandler<TCommand> : MediatorRequestHandler<TCommand>
    where TCommand : class, ICommand
{
    
}

public abstract class CommandHandler<TCommand, TResponse> : MediatorRequestHandler<TCommand, TResponse>
    where TCommand : class, ICommand<TResponse>
    where TResponse : class
{
    
}