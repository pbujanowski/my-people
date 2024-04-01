using MyPeople.Common.Models.Dtos;
using MyPeople.Services.Images.Application.Tests.Fixtures;
using MyPeople.Services.Images.Domain.Entities;

namespace MyPeople.Services.Images.Application.Tests.Mappings;

public class MappingProfileTests : IClassFixture<MappingProfileFixture>
{
    private readonly MappingProfileFixture _fixture;

    public MappingProfileTests(MappingProfileFixture fixture)
    {
        _fixture = fixture;
    }

    [Fact]
    public void ShouldHaveValidConfiguration()
    {
        _fixture.ConfigurationProvider.AssertConfigurationIsValid();

        Assert.True(true);
    }
    
    [Theory]
    [InlineData(typeof(Image), typeof(ImageDto))]
    [InlineData(typeof(ImageDto), typeof(Image))]
    [InlineData(typeof(CreateImageDto), typeof(Image))]
    [InlineData(typeof(DeleteImageDto), typeof(Image))]
    public void ShouldMapObjectsProperly(Type sourceType, Type destinationType)
    {
        var sourceObject = Activator.CreateInstance(sourceType);
        var destinationObject = _fixture.Mapper.Map(sourceObject, sourceType, destinationType);
        
        Assert.Equal(destinationType, destinationObject?.GetType());
    }
}