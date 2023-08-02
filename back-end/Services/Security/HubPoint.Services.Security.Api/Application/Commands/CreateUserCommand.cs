using HubPoint.Services.Common.Abstractions.Commands;
using HubPoint.Services.Security.Api.Domain;
using HubPoint.Services.Security.Api.Infrastructure;

namespace HubPoint.Services.Security.Api.Application.Commands;

public class CreateUserCommand : ICommand
{
    public string Name { get; set; } = default!;
    public string Password { get; set; } = default!;
}

public class CreateUserCommandHandler : ICommandHandler<CreateUserCommand>
{
    private readonly AppDbContext _context;

    public CreateUserCommandHandler(AppDbContext context)
    {
        _context = context;
    }

    public async Task Handle(CreateUserCommand request, CancellationToken cancellationToken)
    {
        var user = new User(request.Name, request.Password);
        _context.Users.Add(user);
        // return Unit.Value;
    }
}
