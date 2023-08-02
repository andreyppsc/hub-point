using HubPoint.Services.Common.Abstractions.Commands;
using MediatR.Pipeline;

namespace HubPoint.Services.Security.Api.Infrastructure;

public class CommitPostProcessor<TRequest, TResponse> : IRequestPostProcessor<TRequest, TResponse>
    where TRequest : notnull
{
    private readonly ILogger<CommitPostProcessor<TRequest, TResponse>> _logger;
    private readonly AppDbContext _context;

    public CommitPostProcessor(ILogger<CommitPostProcessor<TRequest, TResponse>> logger, AppDbContext context)
    {
        _logger = logger ?? throw new ArgumentNullException(nameof(logger));
        _context = context ?? throw new ArgumentNullException(nameof(context));
    }

    public async Task Process(TRequest request, TResponse response, CancellationToken cancellationToken)
    {
        if (request is not ICommand && request is not ICommand<TResponse>)
            return;

        _logger.LogInformation("Committing changes of command [{Name}]", typeof(TRequest).Name);
        await _context.SaveChangesAsync(cancellationToken);
    }
}