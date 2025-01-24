using System.Text.Json;
using Amazon.Lambda.SQSEvents;
using Amazon.Lambda.TestUtilities;
using FluentAssertions;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using MyPeople.Common.Models.Dtos;
using MyPeople.Lambdas.Common.Services;
using MyPeople.Lambdas.Images.Upload.Application.Dtos;
using MyPeople.Lambdas.Images.Upload.Application.Services;
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
        var imagesMock = new List<ImageDto>
        {
            new()
            {
                Id = Guid.Parse("11ac50d8-cc93-4d78-acf2-339d3736abc6"),
                Name = "test-image-1",
                Content =
                    "/9j/4AAQSkZJRgABAQEASABIAAD/2wBDAP////////////////////////////"
                    + "//////////////////////////////////////////////////////////wg"
                    + "ALCAABAAEBAREA/8QAFBABAAAAAAAAAAAAAAAAAAAAAP/aAAgBAQABPxA=",
                ContentType = "image/jpeg",
                CreatedAt = DateTime.Parse("2025-01-24"),
            },
            new()
            {
                Id = Guid.Parse("e72cea3c-f3c4-43b5-b49e-132fd75d4cbc"),
                Name = "test-image-2",
                Content =
                    "/9j/4AAQSkZJRgABAQEASABIAAD/2wBDAP////////////////////////////"
                    + "//////////////////////////////////////////////////////////wg"
                    + "ALCAABAAEBAREA/8QAFBABAAAAAAAAAAAAAAAAAAAAAP/aAAgBAQABPxA=",
                ContentType = "image/jpeg",
                CreatedAt = DateTime.Parse("2025-01-24"),
            },
        };

        var imagesIds = imagesMock.Select(image => (Guid)image.Id!);

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

        var imageFetchService = Substitute.For<IImageFetchService>();
        imageFetchService
            .FetchImagesByIds(Arg.Any<IEnumerable<Guid>>())
            .ReturnsForAnyArgs(imagesMock);

        var function = new Function(
            configuration,
            imagesAwsConfiguration,
            storageService,
            imageStorageService,
            imageFetchService
        );

        var context = new TestLambdaContext();

        var functionRequest = new FunctionRequestDto { ImagesIds = imagesIds };
        var functionRequestJson = JsonSerializer.Serialize(functionRequest);

        var sqsEvent = new SQSEvent() { Records = [new() { Body = functionRequestJson }] };

        var functionResponse = await function.FunctionHandler(sqsEvent, context);

        functionResponse.StatusCode.Should().BeNull();
        functionResponse.Message.Should().BeNull();
    }
}
