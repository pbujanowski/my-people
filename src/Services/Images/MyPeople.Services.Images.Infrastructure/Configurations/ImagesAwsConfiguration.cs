using MyPeople.Common.Configuration.Configurations;

namespace MyPeople.Services.Images.Infrastructure.Configurations;

public class ImagesAwsConfiguration : AwsConfiguration
{
    public ImageQueueConfiguration? ImageQueue { get; set; }
}
