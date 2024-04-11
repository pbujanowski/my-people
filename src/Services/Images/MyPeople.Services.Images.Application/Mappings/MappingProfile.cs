using AutoMapper;
using MyPeople.Common.Models.Dtos;
using MyPeople.Services.Images.Domain.Entities;

namespace MyPeople.Services.Images.Application.Mappings;

public class MappingProfile : Profile
{
    public MappingProfile()
    {
        CreateMap<Image, ImageDto>();
        CreateMap<ImageDto, Image>();
        CreateMap<CreateImageDto, Image>()
            .ForMember(destination => destination.Id, options => options.Ignore())
            .ForMember(destination => destination.CreatedAt, options => options.Ignore())
            .ForMember(destination => destination.UpdatedAt, options => options.Ignore());

        CreateMap<DeleteImageDto, Image>()
            .ForMember(destination => destination.Name, options => options.Ignore())
            .ForMember(destination => destination.ContentType, options => options.Ignore())
            .ForMember(destination => destination.Content, options => options.Ignore())
            .ForMember(destination => destination.CreatedAt, options => options.Ignore())
            .ForMember(destination => destination.UpdatedAt, options => options.Ignore());
    }
}
