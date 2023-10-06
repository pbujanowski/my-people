using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.Authorization;
using MyPeople.Common.Abstractions.Services;
using MyPeople.Common.Models.Dtos;

namespace MyPeople.Client.Web.Components.Post;

public partial class PostList
{
    [Inject]
    public IPostService PostService { get; set; } = default!;

    [Inject]
    public AuthenticationStateProvider AuthenticationStateProvider { get; set; } = default!;

    [Parameter, EditorRequired]
    public IEnumerable<PostDto> Posts { get; set; } = default!;

    private Guid GetCurrentUserId() =>
        Guid.Parse(
            AuthenticationStateProvider
                .GetAuthenticationStateAsync()
                .Result.User.FindFirst("sub")
                ?.Value ?? Guid.Empty.ToString()
        );

    private async Task GetAllPostsAsync()
    {
        Posts = await PostService.GetAllPostsAsync();
    }
}
