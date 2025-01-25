using Amazon.CloudWatchLogs;
using Microsoft.Extensions.Configuration;
using MyPeople.Common.Configuration.Configurations;
using Serilog;
using Serilog.Formatting.Json;
using Serilog.Sinks.AwsCloudWatch;

namespace MyPeople.Common.Logging;

internal static class LoggingInitializer
{
    public static void Initialize(IConfiguration configuration)
    {
        var awsConfiguration = configuration.GetSection("AWS").Get<AwsConfiguration>();

        var loggerConfiguration = new LoggerConfiguration();

        if (awsConfiguration?.CloudWatch is not null)
        {
            var client = new AmazonCloudWatchLogsClient(
                awsAccessKeyId: awsConfiguration.AccessKeyId,
                awsSecretAccessKey: awsConfiguration.SecretAccessKey,
                clientConfig: new()
                {
                    ServiceURL = awsConfiguration.CloudWatch.ServiceUrl,
                    AuthenticationRegion = awsConfiguration.Region,
                }
            );
            var options = new CloudWatchSinkOptions
            {
                LogGroupName = awsConfiguration.CloudWatch.LogGroupName,
                CreateLogGroup = true,
                RetryAttempts = 3,
                TextFormatter = new JsonFormatter(),
            };
            loggerConfiguration.WriteTo.AmazonCloudWatch(options, client);
        }

        loggerConfiguration.WriteTo.Console();

        Log.Logger = loggerConfiguration.CreateLogger();
    }
}
