﻿using HubPoint.Services.Common.Abstractions.Events;
using MediatR.Pipeline;

namespace HubPoint.Services.Common.Infrastructure.Events;

public class OutboxProcessor<TRequest, TResponse> : IRequestPostProcessor<TRequest, TResponse>
    where TRequest : notnull
{
    private readonly IOutbox _outbox;

    public OutboxProcessor(IOutbox outbox)
    {
        _outbox = outbox;
    }

    public async Task Process(TRequest request, TResponse response, CancellationToken cancellationToken)
    {
        await _outbox.Process(cancellationToken);
    }
}