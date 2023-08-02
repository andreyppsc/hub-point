using HubPoint.Services.Common.Abstractions.Queries;
using HubPoint.Services.Security.Api.Domain;
using HubPoint.Services.Security.Api.Infrastructure;
using Microsoft.EntityFrameworkCore;

namespace HubPoint.Services.Security.Api.Application.Queries;

public class GetAllUserQuery : IQuery<List<User>> { }

public class GetAllUserQueryHandler : IQueryHandler<GetAllUserQuery, List<User>>
{
    private readonly AppDbContext _context;

    public GetAllUserQueryHandler(AppDbContext context)
    {
        _context = context;
    }

    public async Task<List<User>> Handle(GetAllUserQuery request, CancellationToken cancellationToken)
    {
        return await _context.Users.ToListAsync(cancellationToken: cancellationToken);
    }
}