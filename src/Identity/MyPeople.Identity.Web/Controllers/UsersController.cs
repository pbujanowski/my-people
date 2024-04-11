using Microsoft.AspNetCore.Mvc;
using MyPeople.Identity.Application.Services;

namespace MyPeople.Identity.Web.Controllers;

[ApiController]
[Route("[controller]")]
public class UsersController(
    IApplicationUserService applicationUserService,
    ILogger<UsersController> logger
) : ControllerBase
{
    private readonly IApplicationUserService _applicationUserService = applicationUserService;
    private readonly ILogger<UsersController> _logger = logger;

    [HttpGet("{userId}")]
    public async Task<IActionResult> GetUserById(Guid userId, [FromQuery] bool displayName = false)
    {
        try
        {
            var user = await _applicationUserService.GetUserByIdAsync(userId);
            if (user is null)
                return NotFound();
            return Ok(displayName ? user.Email : user);
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Error during getting user by id. User ID: '{UserId}'.", userId);
            return StatusCode(500);
        }
    }
}
