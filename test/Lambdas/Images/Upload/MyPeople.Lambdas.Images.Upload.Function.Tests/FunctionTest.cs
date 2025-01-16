using System.Text;
using System.Text.Json;
using Amazon.Lambda.SQSEvents;
using Amazon.Lambda.TestUtilities;
using FluentAssertions;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using MyPeople.Common.Models.Dtos;
using MyPeople.Lambdas.Common.Dtos;
using MyPeople.Lambdas.Common.Services;
using MyPeople.Lambdas.Images.Upload.Application.Dtos;
using MyPeople.Lambdas.Images.Upload.Infrastructure.Configurations;
using MyPeople.Lambdas.Images.Upload.Infrastructure.Services;
using NSubstitute;
using Xunit;

namespace MyPeople.Lambdas.Images.Upload.Function.Tests;

public class FunctionTest
{
    [Fact]
    public async Task TestFunction()
    {
        var imageMock = new ImageDto
        {
            Name = "test-image",
            Content =
                "/9j/4AAQSkZJRgABAQEASABIAAD/2wBDAP////////////////////////////"
                + "//////////////////////////////////////////////////////////wg"
                + "ALCAABAAEBAREA/8QAFBABAAAAAAAAAAAAAAAAAAAAAP/aAAgBAQABPxA=",
            ContentType = "image/jpeg",
        };

        var configuration = Substitute.For<IConfiguration>();
        var imagesAwsConfiguration = new ImagesAwsConfiguration
        {
            AccessKeyId = "test",
            SecretAccessKey = "test",
            ImageStorage = new() { BucketName = "test-bucket" },
        };
        var storageService = Substitute.For<IStorageService>();

        var imageStorageServiceLogger = Substitute.For<ILogger<ImageStorageService>>();
        var imageStorageService = new ImageStorageService(
            imageStorageServiceLogger,
            storageService,
            imagesAwsConfiguration
        );

        using var imageStream = new MemoryStream(Encoding.UTF8.GetBytes(imageMock.Content));

        var s3Object = new S3ObjectDto
        {
            Name = imageMock.Name,
            BucketName = imagesAwsConfiguration.ImageStorage.BucketName,
            InputStream = imageStream,
        };

        var s3Response = new S3ResponseDto
        {
            StatusCode = 201,
            Message = $"{s3Object.Name} has been uploaded successfully.",
        };

        var imageUploadResponse = new ImageUploadResponseDto
        {
            StatusCode = s3Response.StatusCode,
            Message = s3Response.Message,
        };

        storageService.UploadFileAsync(s3Object).ReturnsForAnyArgs(Task.FromResult(s3Response));

        var function = new Function(
            configuration,
            imagesAwsConfiguration,
            storageService,
            imageStorageService
        );

        var context = new TestLambdaContext();

        var functionRequest = new FunctionRequestDto { Image = imageMock };
        var functionRequestJson = JsonSerializer.Serialize(functionRequest);

        var sqsEvent = new SQSEvent() { Records = [new() { Body = functionRequestJson }] };

        var functionResponse = await function.FunctionHandler(sqsEvent, context);

        functionResponse.StatusCode.Should().Be(201);
        functionResponse.Message.Should().Be($"{imageMock.Name} has been uploaded successfully.");
    }
}
