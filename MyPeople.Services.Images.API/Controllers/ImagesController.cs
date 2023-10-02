using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using MyPeople.Services.Images.Application.Dtos;
using MyPeople.Services.Images.Application.Services;

namespace MyPeople.Services.Images.API.Controllers;

[ApiController]
[Route("api/[controller]")]
public class ImagesController : ControllerBase
{
    private readonly IImageService _imageService;
    private readonly ILogger<ImagesController> _logger;

    public ImagesController(IImageService imageService, ILogger<ImagesController> logger)
    {
        _imageService = imageService;
        _logger = logger;
    }

    [Authorize]
    [HttpGet("{id}")]
    public async Task<IActionResult> GetImageById(Guid id)
    {
        try
        {
            var image = await _imageService.GetImageByIdAsync(id);
            if (image is null)
            {
                return NotFound();
            }

            return Ok(image);
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Error during getting image by ID. Image ID: '{ImageId}'.", id);
            return StatusCode(500);
        }
    }

    [Authorize]
    [HttpPost]
    public async Task<IActionResult> CreateImage(ImageDto imageDto)
    {
        try
        {
            var image = await _imageService.CreateImageAsync(imageDto);
            return Ok(image);
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Error during creating image. Image: '{Image}'.", imageDto);
            return StatusCode(500);
        }
    }
}
