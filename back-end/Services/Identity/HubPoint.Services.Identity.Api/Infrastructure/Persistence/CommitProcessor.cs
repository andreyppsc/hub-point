using HubPoint.Services.Common.Abstractions.Commands;
using MediatR.Pipeline;

namespace HubPoint.Services.Identity.Api.Infrastructure.Persistence;

public class CommitProcessor<TRequest, TResponse> : IRequestPostProcessor<TRequest, TResponse>
    where TRequest : notnull
{
    private readonly IdentityDbContext _context;

    public CommitProcessor(IdentityDbContext context)
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