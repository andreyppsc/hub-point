using MassTransit.Mediator;

namespace HubPoint.Services.Common.Abstractions.Queries;

public interface IQuery<out TResponse> : Request<TResponse> where TResponse : class
{
    
}