using MassTransit.Mediator;

namespace HubPoint.Services.Common.Abstractions.Queries;

public abstract class QueryHandler<TQuery, TResponse> : MediatorRequestHandler<TQuery, TResponse>
    where TQuery : class, IQuery<TResponse>
    where TResponse : class
{
    
}