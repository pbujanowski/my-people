using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using MyPeople.Services.Posts.Application.Dtos;

namespace MyPeople.Services.Posts.API.Controllers;

[ApiController]
[Route("[controller]")]
public class PostsController : ControllerBase
{
    [Authorize]
    [HttpGet]
    public async Task<IActionResult> GetPosts()
    {
        await Task.CompletedTask;
        return Ok(new List<PostDto>
        {
            new PostDto
            {
                Id = Guid.NewGuid(),
                Content = "Hello world no 1!"
            },
            new PostDto
            {
                Id = Guid.NewGuid(),
                Content = "Hello world no 2!"
            }
        });
    }
}