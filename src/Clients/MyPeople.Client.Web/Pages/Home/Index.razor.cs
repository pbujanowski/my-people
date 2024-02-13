using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.Authorization;
using MyPeople.Common.Abstractions.Services;
using MyPeople.Common.Models.Dtos;

namespace MyPeople.Client.Web.Pages.Home;

public partial class Index
{
    [Inject] public IPostService PostService { get; set; } = default!;

    [Inject] public AuthenticationStateProvider AuthenticationStateProvider { get; set; } = default!;

    public IEnumerable<PostDto> Posts { get; set; } = [];

    public CreatePostDto NewPost { get; set; } = new();

    protected override async Task OnInitializedAsync()
    {
        await base.OnInitializedAsync();

        await GetPostsAsync();

        NewPost = await CreateNewPostAsync();
    }

    private async Task<CreatePostDto> CreateNewPostAsync()
    {
        var state = await AuthenticationStateProvider.GetAuthenticationStateAsync();
        var userId = state?.User?.FindFirst("sub")?.Value;
        return new CreatePostDto { UserId = userId is null ? null : new Guid(userId) };
    }

    private async Task GetPostsAsync()
    {
        Posts = await PostService.GetAllPostsAsync();
    }

    private async Task CreatePostAsync()
    {
        try
        {
            await PostService.CreatePostAsync(NewPost);
            await GetPostsAsync();
        }
        finally
        {
            NewPost = await CreateNewPostAsync();
        }
    }
}