using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using MyPeople.Common.Abstractions.Services;
using MyPeople.Common.Models.Dtos;

namespace MyPeople.Services.Images.API.Controllers;

[ApiController]
[Route("[controller]")]
public class ImagesController(IImageService imageService, ILogger<ImagesController> logger)
    : ControllerBase
{
    private readonly IImageService _imageService = imageService;
    private readonly ILogger<ImagesController> _logger = logger;

    // [Authorize]
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

    // [Authorize]
    [HttpGet("browse/{id}")]
    public async Task<IActionResult> BrowseImageById(Guid id)
    {
        try
        {
            var image = await _imageService.GetImageByIdAsync(id);
            if (image is null || image.Content is null || image.ContentType is null)
            {
                return NotFound();
            }

            return File(Convert.FromBase64String(image.Content), image.ContentType);
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Error during browsing image by ID. Image ID: '{ImageId}.", id);
            return StatusCode(500);
        }
    }

    // [Authorize]
    [HttpPost]
    public async Task<IActionResult> CreateImages(IEnumerable<CreateImageDto> imageDtos)
    {
        try
        {
            var image = await _imageService.CreateImagesAsync(imageDtos);
            return Ok(image);
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Error during creating images. Images: '{Images}'.", imageDtos);
            return StatusCode(500);
        }
    }

    // [Authorize]
    [HttpPost("delete")]
    public async Task<IActionResult> DeleteImages(IEnumerable<DeleteImageDto> imageDtos)
    {
        try
        {
            var images = await _imageService.DeleteImagesAsync(imageDtos);
            return Ok(images);
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Error during deleting images. Images: '{Images}'.", imageDtos);
            return StatusCode(500);
        }
    }
}
