using AutoMapper;
using MyPeople.Services.Images.Application.Mappings;

namespace MyPeople.Services.Images.Application.Tests.Fixtures;

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
