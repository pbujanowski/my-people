using System.Text;
using System.Text.Json;
using Amazon.Lambda.TestUtilities;
using FluentAssertions;
using Microsoft.Extensions.Configuration;
using MyPeople.Common.Configuration.Configurations;
using MyPeople.Common.Models.Dtos;
using MyPeople.Lambdas.Common.Dtos;
using MyPeople.Lambdas.Common.Services;
using MyPeople.Lambdas.Images.Application.Dtos;
using MyPeople.Lambdas.Images.Application.Services;
using NSubstitute;
using Xunit;

namespace MyPeople.Lambdas.Images.Function.Tests;

public class FunctionTest
{
    [Fact]
    public async Task TestFunction()
    {
        var imageMock = new ImageDto { Name = "test-image", Content = string.Empty };
        var imageJson = JsonSerializer.Serialize(imageMock);

        var configuration = Substitute.For<IConfiguration>();
        var awsConfiguration = Substitute.For<AwsConfiguration>();
        var storageService = Substitute.For<IStorageService>();

        using var imageStream = new MemoryStream(Encoding.UTF8.GetBytes(imageMock.Content));

        var s3Object = new S3ObjectDto
        {
            Name = imageMock.Name,
            BucketName = "test-bucket",
            InputStream = imageStream,
        };

        var s3Response = new S3ResponseDto
        {
            StatusCode = 200,
            Message = $"{s3Object.Name} has been uploaded successfully.",
        };

        var imageUploadResponse = new ImageUploadResponseDto
        {
            StatusCode = s3Response.StatusCode,
            Message = s3Response.Message,
        };

        storageService.UploadFileAsync(s3Object).ReturnsForAnyArgs(Task.FromResult(s3Response));

        var function = new Function(configuration, awsConfiguration, storageService);

        var context = new TestLambdaContext();

        var functionResponse = await function.FunctionHandler(imageJson, context);

        functionResponse.StatusCode.Should().Be(200);
        functionResponse.Message.Should().Be($"{imageMock.Name} has been uploaded successfully.");
    }
}
