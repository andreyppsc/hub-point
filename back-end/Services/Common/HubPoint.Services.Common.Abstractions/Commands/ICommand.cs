using MediatR;

namespace HubPoint.Services.Common.Abstractions.Commands;

public interface ICommand : IRequest { }

public interface ICommand<out TResponse> : IRequest<TResponse> { }