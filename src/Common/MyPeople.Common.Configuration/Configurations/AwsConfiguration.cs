namespace MyPeople.Common.Configuration.Configurations;

public class AwsConfiguration
{
    public string? AccessKeyId { get; set; }

    public string? SecretAccessKey { get; set; }

    public string? Region { get; set; }

    public AwsCloudWatchConfiguration? CloudWatch { get; set; }
}
