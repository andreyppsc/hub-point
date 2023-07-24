using HubPoint.Services.Common.Abstractions.Queries;
using MassTransit.Mediator;

namespace HubPoint.Services.Common.Infrastructure.Dispatchers;

public sealed class QueryDispatcher : IQueryDispatcher
{
    private readonly IMediator _mediator;

    public QueryDispatcher(IMediator mediator)
    {
        _mediator = mediator;
    }

    public async Task<TResponse> Send<TResponse>(IQuery<TResponse> query, CancellationToken cancellationToken = default) where TResponse : class
    {
        var client = _mediator.CreateRequestClient<IQuery<TResponse>>();
        var response = await client.GetResponse<TResponse>(query, cancellationToken);
        return response.Message;
    }
}