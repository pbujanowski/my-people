using Microsoft.AspNetCore.Components;
using MyPeople.Common.Models.Dtos;

namespace MyPeople.Client.Web.Components.Post;

public partial class PostCreate
{
    [Parameter, EditorRequired]
    public CreatePostDto Post { get; set; } = default!;

    [Parameter, EditorRequired]
    public EventCallback OnPostCreate { get; set; }
}
