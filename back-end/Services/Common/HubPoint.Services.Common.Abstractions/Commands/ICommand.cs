namespace HubPoint.Services.Common.Abstractions.Commands;

public interface ICommand { }

public interface ICommand<out TResponse> where TResponse : class { }