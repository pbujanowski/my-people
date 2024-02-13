using Microsoft.AspNetCore.Components;
using MudBlazor;
using MyPeople.Common.Models.Dtos;

namespace MyPeople.Client.Web.Components.Post;

public partial class PostItem
{
    private PostDto? _oldPost;

    [Inject] public IDialogService DialogService { get; set; } = default!;

    [Parameter] [EditorRequired] public PostDto Post { get; set; } = default!;

    [Parameter] [EditorRequired] public Guid CurrentUserId { get; set; }

    [Parameter] [EditorRequired] public EventCallback<PostDto> EditPost { get; set; }

    [Parameter] [EditorRequired] public EventCallback<PostDto> RemovePost { get; set; }

    public bool IsEditMode { get; set; }

    private bool CheckIfPostIsOwnedByCurrentUser()
    {
        return Post.UserId == CurrentUserId;
    }

    private void EnableEditMode()
    {
        _oldPost = new PostDto
        {
            Id = Post.Id,
            UserId = Post.UserId,
            CreatedAt = Post.CreatedAt,
            UpdatedAt = Post.UpdatedAt,
            UserDisplayName = Post.UserDisplayName,
            Content = Post.Content
        };
        IsEditMode = true;
    }

    private void DisableEditMode(bool restoreOldPost)
    {
        if (restoreOldPost)
            Post = new PostDto
            {
                Id = _oldPost?.Id,
                UserId = _oldPost?.UserId,
                CreatedAt = _oldPost?.CreatedAt,
                UpdatedAt = _oldPost?.UpdatedAt,
                UserDisplayName = _oldPost?.UserDisplayName,
                Content = _oldPost?.Content
            };
        IsEditMode = false;
    }

    private async Task OnEditPostClick()
    {
        await EditPost.InvokeAsync(Post);
        DisableEditMode(false);
    }

    private async Task OnRemovePostClick()
    {
        var result = await DialogService.ShowMessageBox(
            "Confirmation",
            "Do you really want to remove this post?",
            "Remove",
            "Cancel"
        );
        if (result == true)
            await RemovePost.InvokeAsync(Post);
    }
}