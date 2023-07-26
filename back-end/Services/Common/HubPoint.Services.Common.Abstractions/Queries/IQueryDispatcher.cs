namespace HubPoint.Services.Common.Abstractions.Queries;

public interface IQueryDispatcher
{
    public Task<TResponse> Send<TResponse>(IQuery<TResponse> query, CancellationToken cancellationToken = default);
}