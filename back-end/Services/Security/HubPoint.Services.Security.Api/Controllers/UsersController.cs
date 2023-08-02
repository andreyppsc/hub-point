using HubPoint.Services.Security.Api.Application.Commands;
using HubPoint.Services.Security.Api.Application.Queries;
using MediatR;

namespace HubPoint.Services.Security.Api.Controllers;

[ApiController]
[Route("users")]
public class UsersController : ControllerBase
{
    private readonly IMediator _mediator;

    public UsersController(IMediator mediator)
    {
        _mediator = mediator;
    }

    public async Task<IActionResult> GetAll(CancellationToken cancellationToken = default)
    {
        return Ok(await _mediator.Send(new GetAllUserQuery(), cancellationToken));
    }

    [HttpPost]
    public async Task<IActionResult> Create(CreateUserCommand command, CancellationToken cancellationToken = default)
    {
        await _mediator.Send(command, cancellationToken);
        return Ok();
    }
}