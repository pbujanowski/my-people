using AutoMapper;
using MyPeople.Services.Posts.Application.Mappings;

namespace MyPeople.Services.Posts.Application.Tests.Fixtures;

public class MappingProfileFixture
{
    public IConfigurationProvider ConfigurationProvider { get; }

    public IMapper Mapper { get; }

    public MappingProfileFixture()
    {
        ConfigurationProvider = new MapperConfiguration(config =>
            config.AddProfile(typeof(MappingProfile))
        );

        Mapper = ConfigurationProvider.CreateMapper();
    }
}
