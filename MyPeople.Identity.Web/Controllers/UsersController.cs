using Microsoft.AspNetCore.Mvc;
using MyPeople.Identity.Application.Services;

namespace MyPeople.Identity.Web.Controllers;

[ApiController]
[Route("[controller]")]
public class UsersController : ControllerBase
{
    private readonly IApplicationUserService _applicationUserService;
    private readonly ILogger<UsersController> _logger;

    public UsersController(IApplicationUserService applicationUserService, ILogger<UsersController> logger)
    {
        _applicationUserService = applicationUserService;
        _logger = logger;
    }

    [HttpGet("{userId}")]
    public async Task<IActionResult> GetUserById(Guid userId)
    {
        try
        {
            var user = await _applicationUserService.GetUserByIdAsync(userId);
            if (user is null)
            {
                return NotFound();
            }
            return Ok(user);
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, ex.Message);
            return StatusCode(500);
        }
    }
}
