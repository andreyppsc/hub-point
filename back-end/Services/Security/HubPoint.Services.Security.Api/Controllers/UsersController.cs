namespace HubPoint.Services.Security.Api.Controllers;

[Authorize]
[ApiController]
[Route("users")]
public class UsersController : ControllerBase
{
    [HttpGet]
    public async Task<IActionResult> GetAll(CancellationToken ct = default)
    {
        var usernames = new List<string>
        {
            "john.doe",
            "mary.smith",
            "james.wilson",
            "sarah.jones",
            "michael.brown",
            "linda.davis",
            "robert.miller",
            "emily.walker",
            "david.thompson",
            "olivia.harris"
        };
        
        return Ok(usernames);
    }
}