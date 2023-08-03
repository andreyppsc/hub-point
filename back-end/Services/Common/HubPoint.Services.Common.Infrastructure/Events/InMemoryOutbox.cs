using EasyNetQ;
using HubPoint.Services.Common.Abstractions.Events;
using Microsoft.Extensions.Logging;

namespace HubPoint.Services.Common.Infrastructure.Events;

public class InMemoryOutbox : IOutbox
{
    private readonly ILogger<InMemoryOutbox> _logger;
    private readonly List<IIntegrationEvent> _events = new();
    private readonly IPubSub _bus;

    public InMemoryOutbox(ILogger<InMemoryOutbox> logger, IPubSub bus)
    {
        _logger = logger;
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
            await _bus.PublishAsync(@event, @event.GetType(), cancellationToken: cancellationToken).ConfigureAwait(false);
        }
    }
}