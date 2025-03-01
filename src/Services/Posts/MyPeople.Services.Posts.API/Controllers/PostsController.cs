using System.Security.Claims;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using MyPeople.Common.Abstractions.Services;
using MyPeople.Common.Models.Dtos;
using MyPeople.Services.Posts.Application.Services;

namespace MyPeople.Services.Posts.API.Controllers;

[ApiController]
[Route("[controller]")]
public class PostsController(
    IPostService postService,
    IUserService userService,
    ILogger<PostsController> logger
) : ControllerBase
{
    private readonly ILogger<PostsController> _logger = logger;
    private readonly IPostService _postService = postService;
    private readonly IUserService _userService = userService;

    [Authorize]
    [HttpGet]
    public async Task<IActionResult> GetPosts()
    {
        _logger.LogInformation("Getting posts invoked.");

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
            _logger.LogError(ex, "Error during getting posts.");
            return StatusCode(500);
        }
    }

    [Authorize]
    [HttpGet("{id}")]
    public async Task<IActionResult> GetPostById(Guid id)
    {
        _logger.LogInformation("Getting post by ID invoked. Post ID: {Id}", id);

        try
        {
            var post = await _postService.GetPostByIdAsync(id);
            if (post is null)
            {
                return NotFound();
            }

            post.UserDisplayName = await _userService.GetUserDisplayNameById(post.UserId);

            return Ok(post);
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Error during getting post by ID. Post ID: {Id}.", id);
            return StatusCode(500);
        }
    }

    [Authorize]
    [HttpPost]
    public async Task<IActionResult> CreatePost(CreatePostDto postDto)
    {
        _logger.LogInformation("Creating post invoked. Post: {@Post}.", postDto);

        try
        {
            var post = await _postService.CreatePostAsync(postDto);
            return Ok(post);
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Error during creating post. Post: {@Post}.", postDto);
            return StatusCode(500);
        }
    }

    [Authorize]
    [HttpPut("{id}")]
    public async Task<IActionResult> UpdatePost(Guid id, UpdatePostDto postDto)
    {
        _logger.LogInformation("Updating post invoked. Post ID: {Id}, Post: {@Post}.", id, postDto);

        try
        {
            if (id != postDto.Id)
            {
                return BadRequest("Sent post IDs in request are not the same.");
            }

            var found = await _postService.GetPostByIdAsync(id);
            if (found is null)
            {
                return NotFound($"Post with ID '{id}' not found.");
            }

            var userId = User.FindFirstValue("sub");

            if (found.UserId.ToString() != userId)
            {
                return Forbid();
            }

            var post = await _postService.UpdatePostAsync(postDto);
            return Ok(post);
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Error during updating post. Post: {@Post}.", postDto);
            return StatusCode(500);
        }
    }

    [Authorize]
    [HttpDelete("{id}")]
    public async Task<IActionResult> DeletePost(Guid id)
    {
        _logger.LogInformation("Deleting post invoked. Post ID: {Id}.", id);

        try
        {
            var found = await _postService.GetPostByIdAsync(id);
            if (found is null)
            {
                return NotFound($"Post with ID '{id}' not found.");
            }

            var userId = User.FindFirstValue("sub");

            if (found.UserId.ToString() != userId)
            {
                return Forbid();
            }

            var deletePostDto = new DeletePostDto { Id = found.Id, UserId = found.UserId };

            var post = await _postService.DeletePostAsync(deletePostDto);
            return Ok(post);
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Error during deleting post. Post ID: '{PostId}'.", id);
            return StatusCode(500);
        }
    }
}
