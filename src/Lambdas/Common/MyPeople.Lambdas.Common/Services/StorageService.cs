using Amazon.Runtime;
using Amazon.S3;
using Amazon.S3.Transfer;
using MyPeople.Common.Configuration.Configurations;
using MyPeople.Lambdas.Common.Dtos;

namespace MyPeople.Lambdas.Common.Services;

public class StorageService(AwsConfiguration awsConfiguration) : IStorageService
{
    public async Task<S3ResponseDto> UploadFileAsync(S3ObjectDto s3ObjectDto)
    {
        var response = new S3ResponseDto();
        try
        {
            var uploadRequest = new TransferUtilityUploadRequest
            {
                Key = s3ObjectDto.Name,
                InputStream = s3ObjectDto.InputStream,
                BucketName = s3ObjectDto.BucketName,
            };

            var config = new AmazonS3Config { AuthenticationRegion = awsConfiguration.Region };

            var credentials = new BasicAWSCredentials(
                awsConfiguration.AccessKeyId,
                awsConfiguration.SecretAccessKey
            );

            using var client = new AmazonS3Client(credentials, config);

            var transferUtility = new TransferUtility(client);

            await transferUtility.UploadAsync(uploadRequest);

            response.StatusCode = 201;
            response.Message = $"{s3ObjectDto.Name} has been uploaded successfully.";
        }
        catch (AmazonS3Exception amazonS3Exception)
        {
            response.StatusCode = (int)amazonS3Exception.StatusCode;
            response.Message = amazonS3Exception.Message;
        }
        catch (Exception exception)
        {
            response.StatusCode = 500;
            response.Message = exception.Message;
        }

        return response;
    }
}
