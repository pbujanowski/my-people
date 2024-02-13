using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.Authorization;
using MyPeople.Common.Abstractions.Services;
using MyPeople.Common.Models.Dtos;

namespace MyPeople.Client.Web.Components.Post;

public partial class PostList
{
    [Inject] public IPostService PostService { get; set; } = default!;

    [Inject] public AuthenticationStateProvider AuthenticationStateProvider { get; set; } = default!;

    [Parameter] [EditorRequired] public IEnumerable<PostDto> Posts { get; set; } = default!;

    public Guid CurrentUserId { get; set; }

    protected override async Task OnInitializedAsync()
    {
        CurrentUserId = await GetCurrentUserIdAsync();
        await base.OnInitializedAsync();
    }

    private async Task<Guid> GetCurrentUserIdAsync()
    {
        var authenticationState = await AuthenticationStateProvider.GetAuthenticationStateAsync();
        return Guid.Parse(
            authenticationState.User.FindFirst("sub")?.Value ?? Guid.Empty.ToString()
        );
    }

    private async Task GetAllPostsAsync()
    {
        Posts = await PostService.GetAllPostsAsync();
    }

    private async Task EditPostAsync(PostDto post)
    {
        var updatePostDto = new UpdatePostDto
        {
            Id = post.Id,
            CreatedAt = post.CreatedAt,
            UpdatedAt = post.UpdatedAt,
            UserId = post.UserId,
            UserDisplayName = post.UserDisplayName,
            Content = post.Content
        };

        await PostService.UpdatePostAsync(updatePostDto);
        await GetAllPostsAsync();
    }

    private async Task RemovePostAsync(PostDto post)
    {
        var deletePostDto = new DeletePostDto { Id = post.Id, UserId = post.UserId };

        await PostService.DeletePostAsync(deletePostDto);
        await GetAllPostsAsync();
    }
}