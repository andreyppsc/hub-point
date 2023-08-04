using HubPoint.Services.Common.Abstractions.Commands;
using HubPoint.Services.Security.Api.Domain;
using HubPoint.Services.Security.Api.Infrastructure;

namespace HubPoint.Services.Security.Api.Application.Commands;

public class CreateUser : ICommand
{
    public string Name { get; set; } = default!;
    public string Password { get; set; } = default!;
}

public class CreateUserHandler : ICommandHandler<CreateUser>
{
    private readonly SecurityDbContext _context;

    public CreateUserHandler(SecurityDbContext context)
    {
        _context = context;
    }

    public Task Handle(CreateUser request, CancellationToken cancellationToken)
    {
        var user = new User(request.Name, request.Password);
        _context.Users.Add(user);

        return Task.CompletedTask;
    }
}
