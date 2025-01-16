using Amazon.Runtime;
using Amazon.S3;
using Amazon.S3.Transfer;
using Microsoft.Extensions.Logging;
using MyPeople.Common.Configuration.Configurations;
using MyPeople.Common.Configuration.Exceptions;
using MyPeople.Lambdas.Common.Dtos;

namespace MyPeople.Lambdas.Common.Services;

public class StorageService(ILogger<StorageService> logger, AwsConfiguration awsConfiguration)
    : IStorageService
{
    public async Task<S3ResponseDto> UploadFileAsync(S3ObjectDto s3ObjectDto)
    {
        var response = new S3ResponseDto();

        try
        {
            logger.LogInformation("File uploading started.");

            var uploadRequest = new TransferUtilityUploadRequest
            {
                Key = s3ObjectDto.Name,
                InputStream = s3ObjectDto.InputStream,
                BucketName = s3ObjectDto.BucketName,
            };

            if (awsConfiguration.S3 is null)
            {
                throw new ConfigurationException("AWS:S3");
            }

            var config = new AmazonS3Config
            {
                ServiceURL = awsConfiguration.S3.ServiceUrl,
                AuthenticationRegion = awsConfiguration.Region,
                Timeout = TimeSpan.FromSeconds(awsConfiguration.S3.Timeout),
                ForcePathStyle = true,
            };

            var credentials = new BasicAWSCredentials(
                awsConfiguration.AccessKeyId,
                awsConfiguration.SecretAccessKey
            );

            using var client = new AmazonS3Client(credentials, config);

            var transferUtility = new TransferUtility(client);

            await transferUtility.UploadAsync(uploadRequest);

            logger.LogInformation("File uploading finished.");

            response.StatusCode = 201;
            response.Message = $"{s3ObjectDto.Name} has been uploaded successfully.";
        }
        catch (AmazonS3Exception amazonS3Exception)
        {
            logger.LogError(amazonS3Exception, "File uploading failed.");
            response.StatusCode = (int)amazonS3Exception.StatusCode;
            response.Message = amazonS3Exception.Message;
        }
        catch (Exception exception)
        {
            logger.LogError(exception, "File uploading failed.");
            response.StatusCode = 500;
            response.Message = exception.Message;
        }

        return response;
    }
}
