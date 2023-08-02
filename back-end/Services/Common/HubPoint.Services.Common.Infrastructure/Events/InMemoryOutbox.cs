using EasyNetQ;
using HubPoint.Services.Common.Abstractions.Events;
using Microsoft.Extensions.Logging;

namespace HubPoint.Services.Common.Infrastructure.Events;

public class InMemoryOutbox : IOutbox
{
    private readonly ILogger<InMemoryOutbox> _logger;
    private readonly List<IIntegrationEvent> _events = new();

    public InMemoryOutbox(ILogger<InMemoryOutbox> logger)
    {
        _logger = logger;
    }

    public void Add<TIntegrationEvent>(TIntegrationEvent integrationEvent) where TIntegrationEvent : IIntegrationEvent
    {
        _events.Add(integrationEvent);
    }

    public async Task Process(CancellationToken cancellationToken = default)
    {
        var bus = RabbitHutch.CreateBus("host=localhost;virtualHost=hub-point;username=hub-point;password=hub-point", s => s.EnableSystemTextJson());
        
        foreach (var @event in _events)
        {
            await bus.PubSub.PublishAsync(@event, @event.GetType(), cancellationToken: cancellationToken).ConfigureAwait(false);
        }
        
        bus.Dispose();
    }
}