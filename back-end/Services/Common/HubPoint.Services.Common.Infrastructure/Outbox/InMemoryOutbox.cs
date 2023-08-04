namespace HubPoint.Services.Common.Infrastructure.Outbox;

internal class InMemoryOutbox : IOutbox
{
    private readonly IPubSub _bus;
    private readonly List<IIntegrationEvent> _events = new();

    public InMemoryOutbox(IPubSub bus)
    {
        _bus = bus;
    }

    public void Add<TIntegrationEvent>(TIntegrationEvent integrationEvent) where TIntegrationEvent : IIntegrationEvent
    {
        _events.Add(integrationEvent);
    }

    public async Task Process(CancellationToken cancellationToken = default)
    {
        foreach (var @event in _events)
        {
            await _bus.PublishAsync(@event, @event.GetType(), cancellationToken).ConfigureAwait(false);
        }
    }
}