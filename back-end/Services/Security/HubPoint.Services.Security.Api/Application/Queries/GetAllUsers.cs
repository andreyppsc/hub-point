using HubPoint.Services.Common.Abstractions.Queries;
using HubPoint.Services.Security.Api.Domain;
using HubPoint.Services.Security.Api.Infrastructure;
using Microsoft.EntityFrameworkCore;

namespace HubPoint.Services.Security.Api.Application.Queries;

public class GetAllUsers : IQuery<List<User>> { }

public class GetAllUsersHandler : IQueryHandler<GetAllUsers, List<User>>
{
    private readonly SecurityDbContext _context;

    public GetAllUsersHandler(SecurityDbContext context)
    {
        _context = context;
    }

    public async Task<List<User>> Handle(GetAllUsers request, CancellationToken cancellationToken)
    {
        return await _context.Users.ToListAsync(cancellationToken: cancellationToken);
    }
}