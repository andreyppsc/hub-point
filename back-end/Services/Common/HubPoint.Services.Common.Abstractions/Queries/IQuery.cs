using MediatR;

namespace HubPoint.Services.Common.Abstractions.Queries;

public interface IQuery<out TResponse> : IRequest<TResponse> { }