using MassTransit.Mediator;

namespace HubPoint.Services.Common.Abstractions.Commands;

public interface ICommand
{
    
}

public interface ICommand<out TResponse> : Request<TResponse> where TResponse : class
{
    
}