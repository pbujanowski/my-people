using System.Text.Json.Serialization;

namespace MyPeople.Identity.Web.Models;

public class UserinfoModel
{
    [JsonPropertyName("sub")] public string? Subject { get; set; }

    [JsonPropertyName("email")] public string? Email { get; set; }

    [JsonPropertyName("name")] public string? Name { get; set; }

    [JsonPropertyName("role")] public IEnumerable<string>? Role { get; set; }
}