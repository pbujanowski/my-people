using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using MyPeople.Services.Posts.Application.Dtos;
using MyPeople.Services.Posts.Application.Services;

namespace MyPeople.Services.Posts.API.Controllers;

[ApiController]
[Route("[controller]")]
public class PostsController : ControllerBase
{
    private readonly IPostService _postService;
    private readonly IUserService _userService;
    private readonly ILogger<PostsController> _logger;

    public PostsController(IPostService postService,
        IUserService userService,
        ILogger<PostsController> logger)
    {
        _postService = postService;
        _userService = userService;
        _logger = logger;
    }

    [Authorize]
    [HttpGet]
    public async Task<IActionResult> GetPosts()
    {
        try
        {
            var posts = await _postService.GetAllPostsAsync();
            foreach (var post in posts)
            {
                post.UserDisplayName = await _userService.GetUserDisplayNameById(post.UserId);
            }
            return Ok(posts);
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, ex.Message);
            return StatusCode(500);
        }
    }

    [Authorize]
    [HttpPost]
    public async Task<IActionResult> CreatePost(PostDto postDto)
    {
        try
        {
            var post = await _postService.CreatePostAsync(postDto);
            return Ok(post);
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, ex.Message);
            return StatusCode(500);
        }
    }
}