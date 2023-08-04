using HubPoint.Services.Common.Abstractions.Commands;
using MediatR.Pipeline;

namespace HubPoint.Services.Security.Api.Infrastructure;

public class CommitProcessor<TRequest, TResponse> : IRequestPostProcessor<TRequest, TResponse>
    where TRequest : notnull
{
    private readonly SecurityDbContext _context;

    public CommitProcessor(SecurityDbContext context)
    {
        _context = context ?? throw new ArgumentNullException(nameof(context));
    }

    public async Task Process(TRequest request, TResponse response, CancellationToken cancellationToken)
    {
        if (request is not ICommand && request is not ICommand<TResponse>)
            return;
        
        await _context.SaveChangesAsync(cancellationToken);
    }
}