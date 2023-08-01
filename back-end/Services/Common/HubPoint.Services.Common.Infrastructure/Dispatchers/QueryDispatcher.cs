using HubPoint.Services.Common.Abstractions.Queries;

namespace HubPoint.Services.Common.Infrastructure.Dispatchers;

public sealed class QueryDispatcher : IQueryDispatcher
{
    public Task<TResponse> Send<TResponse>(IQuery<TResponse> query, CancellationToken cancellationToken = default)
    {
        throw new NotImplementedException();
    }
}