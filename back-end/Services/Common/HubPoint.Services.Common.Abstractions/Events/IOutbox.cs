namespace HubPoint.Services.Common.Abstractions.Events;

public interface IOutbox
{
    void Add<TIntegrationEvent>(TIntegrationEvent integrationEvent) where TIntegrationEvent : IIntegrationEvent;
    Task Process(CancellationToken cancellationToken = default);
}