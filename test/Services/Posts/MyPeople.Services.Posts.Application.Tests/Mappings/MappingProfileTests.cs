using AutoMapper;
using FluentAssertions;
using MyPeople.Common.Models.Dtos;
using MyPeople.Services.Posts.Application.Tests.Fixtures;
using MyPeople.Services.Posts.Domain.Entities;

namespace MyPeople.Services.Posts.Application.Tests.Mappings;

public class MappingProfileTests(MappingProfileFixture fixture)
    : IClassFixture<MappingProfileFixture>
{
    private readonly MappingProfileFixture _fixture = fixture;

    [Fact]
    public void ShouldHaveValidConfiguration()
    {
        var action = () => _fixture.ConfigurationProvider.AssertConfigurationIsValid();

        action.Should().NotThrow<AutoMapperConfigurationException>();
    }

    [Theory]
    [InlineData(typeof(Post), typeof(PostDto))]
    [InlineData(typeof(PostDto), typeof(Post))]
    [InlineData(typeof(CreatePostDto), typeof(Post))]
    [InlineData(typeof(DeletePostDto), typeof(Post))]
    [InlineData(typeof(UpdatePostDto), typeof(Post))]
    public void ShouldMapObjectsProperly(Type sourceType, Type destinationType)
    {
        var sourceObject = Activator.CreateInstance(sourceType);
        var destinationObject = _fixture.Mapper.Map(sourceObject, sourceType, destinationType);

        destinationType.Should().Be(destinationObject?.GetType());
    }
}
