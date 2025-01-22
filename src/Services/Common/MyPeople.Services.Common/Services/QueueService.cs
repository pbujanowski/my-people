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
    public async Task<SQSResponseDto> QueueObjectsAsync(SQSRequestDto sqsRequestDto)
    {
        var response = new SQSResponseDto();

        try
        {
            logger.LogInformation("Objects queuing started.");

            if (awsConfiguration.SQS is null)
            {
                throw new ConfigurationException("AWS:SQS");
            }

            var sendMessageBatchRequest = new SendMessageBatchRequest
            {
                Entries =
                [
                    .. sqsRequestDto.Messages.Select(message => new SendMessageBatchRequestEntry()
                    {
                        MessageBody = message,
                    }),
                ],
                QueueUrl = sqsRequestDto.QueueUrl,
            };

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

            await client.SendMessageBatchAsync(sendMessageBatchRequest);

            response.StatusCode = 201;
            response.Message = "Objects have been queued successfully.";
        }
        catch (AmazonSQSException amazonSQSException)
        {
            logger.LogError(amazonSQSException, "Objects queuing failed.");
            response.StatusCode = (int)amazonSQSException.StatusCode;
            response.Message = amazonSQSException.Message;
        }
        catch (Exception exception)
        {
            logger.LogError(exception, "Objects queuing failed.");
            response.StatusCode = 500;
            response.Message = exception.Message;
        }

        return response;
    }
}
