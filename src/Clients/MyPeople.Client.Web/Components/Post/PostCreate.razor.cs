using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.Forms;
using MyPeople.Common.Models.Dtos;

namespace MyPeople.Client.Web.Components.Post;

public partial class PostCreate
{
    [Parameter] [EditorRequired] public CreatePostDto Post { get; set; } = default!;

    [Parameter] [EditorRequired] public EventCallback OnPostCreate { get; set; }

    private async Task UploadImages(IReadOnlyList<IBrowserFile> browserFiles)
    {
        foreach (var browserFile in browserFiles)
        {
            using var streamContent = new StreamContent(browserFile.OpenReadStream(10240000));
            var imageContent = await streamContent.ReadAsByteArrayAsync();

            var createImageDto = new CreateImageDto
            {
                Name = browserFile.Name,
                Content = Convert.ToBase64String(imageContent),
                ContentType = browserFile.ContentType
            };

            Post.Images ??= [];

            Post.Images.Add(createImageDto);
        }
    }

    private static string GetImageSource(CreateImageDto image)
    {
        return $"data:{image.ContentType};base64,{image.Content}";
    }
}