using Amazon.Runtime;
using Amazon.SQS;
using Amazon.SQS.Model;
using Microsoft.Extensions.Logging;
using MyPeople.Common.Configuration.Configurations;
using MyPeople.Common.Configuration.Exceptions;
using MyPeople.Services.Common.Dtos;

namespace MyPeople.Services.Common.Services;

public class QueueService(ILogger<QueueService> logger, AwsConfiguration awsConfiguration)
    : IQueueService
{
    public async Task<SQSResponseDto> QueueObjectAsync(SQSRequestDto sqsRequestDto)
    {
        var response = new SQSResponseDto();

        try
        {
            logger.LogInformation("Object queuing started.");

            if (awsConfiguration.SQS is null)
            {
                throw new ConfigurationException("AWS:SQS");
            }

            var sendMessageRequest = new SendMessageRequest(
                sqsRequestDto.QueueUrl,
                sqsRequestDto.Message
            );

            logger.LogDebug("SendMessageRequest: {@Request}.", sendMessageRequest);

            var config = new AmazonSQSConfig
            {
                ServiceURL = awsConfiguration.SQS.ServiceUrl,
                AuthenticationRegion = awsConfiguration.Region,
                Timeout = TimeSpan.FromSeconds(awsConfiguration.SQS.Timeout),
            };

            var credentials = new BasicAWSCredentials(
                awsConfiguration.AccessKeyId,
                awsConfiguration.SecretAccessKey
            );

            using var client = new AmazonSQSClient(credentials, config);

            await client.SendMessageAsync(sendMessageRequest);

            response.StatusCode = 201;
            response.Message = "Object has been queued successfully.";
        }
        catch (AmazonSQSException amazonSQSException)
        {
            logger.LogError(amazonSQSException, "Object queuing failed.");
            response.StatusCode = (int)amazonSQSException.StatusCode;
            response.Message = amazonSQSException.Message;
        }
        catch (Exception exception)
        {
            logger.LogError(exception, "Object queuing failed.");
            response.StatusCode = 500;
            response.Message = exception.Message;
        }

        return response;
    }
}
