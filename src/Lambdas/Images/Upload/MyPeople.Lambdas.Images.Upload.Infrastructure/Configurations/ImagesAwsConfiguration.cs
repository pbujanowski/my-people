using MyPeople.Common.Configuration.Configurations;

namespace MyPeople.Lambdas.Images.Upload.Infrastructure.Configurations;

public class ImagesAwsConfiguration : AwsConfiguration
{
    public ImageStorageConfiguration? ImageStorage { get; set; }
}
