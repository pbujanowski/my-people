using MyPeople.Services.Posts.Application.Services;

namespace MyPeople.Services.Posts.Infrastructure.Services;

public class UserService(HttpClient httpClient) : IUserService
{
    private readonly HttpClient _httpClient = httpClient;

    public async Task<string?> GetUserDisplayNameById(Guid? userId)
    {
        if (userId is null)
            return null;

        using var response = await _httpClient.GetAsync($"/users/{userId}?displayName=true");
        return response.IsSuccessStatusCode ? await response.Content.ReadAsStringAsync() : null;
    }
}
