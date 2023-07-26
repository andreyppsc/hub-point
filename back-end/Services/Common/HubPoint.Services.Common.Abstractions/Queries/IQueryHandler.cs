namespace HubPoint.Services.Common.Abstractions.Queries;

public interface IQueryHandler<in TQuery, out TResponse> where TQuery : IQuery<TResponse> { }