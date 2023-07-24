using HubPoint.Services.Common.Abstractions.Commands;
using HubPoint.Services.Security.Api.Application;
using MassTransit.Mediator;

namespace HubPoint.Services.Security.Api.Controllers;

[ApiController]
[Route("users")]
public class UsersController : ControllerBase
{
    private readonly ICommandDispatcher _commandDispatcher;

    public UsersController(ICommandDispatcher commandDispatcher)
    {
        _commandDispatcher = commandDispatcher;
    }

    [HttpPost]
    public async Task<IActionResult> Create(CancellationToken cancellationToken = default)
    {
        var user = await _commandDispatcher.Send(new CreateUserCommand(), cancellationToken);

        return Ok(user.UserId);
    }
}