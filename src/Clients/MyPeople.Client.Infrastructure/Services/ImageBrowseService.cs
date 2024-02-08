using Microsoft.Extensions.Configuration;

namespace MyPeople.Client.Infrastructure.Services;

public class ImageBrowseService(IConfiguration configuration)
{
    private readonly IConfiguration _configuration = configuration;

    public string GetImageUrlById(Guid imageId)
    {
        var gatewayWebUrl = _configuration.GetSection("Gateways:Web:Url").Get<string>();
        return $"{gatewayWebUrl}/images/browse/{imageId}";
    }
}
