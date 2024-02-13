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
        CreateMap<CreateImageDto, Image>();
        CreateMap<DeleteImageDto, Image>();
    }
}