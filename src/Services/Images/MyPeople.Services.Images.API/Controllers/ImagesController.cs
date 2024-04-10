using MediatR;
using Microsoft.AspNetCore.Mvc;
using MyPeople.Common.Models.Dtos;
using MyPeople.Services.Images.Application.Commands.CreateImages;
using MyPeople.Services.Images.Application.Commands.DeleteImages;
using MyPeople.Services.Images.Application.Queries.GetImageById;
using MyPeople.Services.Images.Application.Queries.GetImagesByIds;

namespace MyPeople.Services.Images.API.Controllers;

[ApiController]
[Route("[controller]")]
public class ImagesController(ILogger<ImagesController> logger, IMediator mediator)
    : ControllerBase
{
    private readonly ILogger<ImagesController> _logger = logger;
    private readonly IMediator _mediator = mediator;

    // [Authorize]
    [HttpGet("{id:guid}")]
    public async Task<IActionResult> GetImageById(Guid id)
    {
        try
        {
            var result = await _mediator.Send(new GetImageByIdQuery(id));
            var image = result.Image;
            if (image is null)
                return NotFound();

            return Ok(image);
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Error during getting image by ID. Image ID: '{ImageId}'.", id);
            return StatusCode(500);
        }
    }

    // [Authorize]
    [HttpGet("browse/{id:guid}")]
    public async Task<IActionResult> BrowseImageById(Guid id)
    {
        try
        {
            var result = await _mediator.Send(new GetImageByIdQuery(id));
            var image = result.Image;
            if (image?.Content is null || image.ContentType is null)
                return NotFound();

            return File(Convert.FromBase64String(image.Content), image.ContentType);
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Error during browsing image by ID. Image ID: '{ImageId}.", id);
            return StatusCode(500);
        }
    }

    // [Authorize]
    [HttpPost("many")]
    public async Task<IActionResult> GetImagesByIds(IEnumerable<Guid> ids)
    {
        try
        {
            var result = await _mediator.Send(new GetImagesByIdsQuery(ids));
            var images = result.Images;
            if (images is null || !images.Any())
                return NotFound();

            return Ok(images);
        }
        catch (Exception ex)
        {
            _logger.LogError(
                ex,
                "Error during getting images by images IDs. Images IDs: '{ImagesIds}'.",
                ids
            );
            return StatusCode(500);
        }
    }

    // [Authorize]
    [HttpPost]
    public async Task<IActionResult> CreateImages(IEnumerable<CreateImageDto> imageDtos)
    {
        try
        {
            var result = await _mediator.Send(new CreateImagesCommand(imageDtos));
            return Ok(result.Images);
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
            var result = await _mediator.Send(new DeleteImagesCommand(imageDtos));
            return Ok(result.Images);
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Error during deleting images. Images: '{Images}'.", imageDtos);
            return StatusCode(500);
        }
    }
}