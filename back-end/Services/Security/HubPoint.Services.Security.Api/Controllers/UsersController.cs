using HubPoint.Services.Common.Abstractions.Commands;
using HubPoint.Services.Security.Api.Application;
using MassTransit;
using MassTransit.Mediator;

namespace HubPoint.Services.Security.Api.Controllers;

[ApiController]
[Route("users")]
public class UsersController : ControllerBase
{
    private readonly IBus _commandDispatcher;

    public UsersController(IBus commandDispatcher)
    {
        _commandDispatcher = commandDispatcher;
    }

    [HttpPost]
    public async Task<IActionResult> Create(CancellationToken cancellationToken = default)
    {
        //var user = await _commandDispatcher.Send(new CreateUserCommand(), cancellationToken);

        var client = _commandDispatcher.CreateRequestClient<CreateUserCommand>();
        var response = await client.GetResponse<UserDto>(new CreateUserCommand(), cancellationToken);

        return Ok(response.Message.UserId);
    }
}