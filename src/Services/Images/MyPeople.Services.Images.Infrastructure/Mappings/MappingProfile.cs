using AutoMapper;
using MyPeople.Services.Images.Application.Dtos;
using MyPeople.Services.Images.Domain.Entities;

namespace MyPeople.Services.Images.Infrastructure.Mappings;

public class MappingProfile : Profile
{
    public MappingProfile()
    {
        CreateMap<Image, ImageDto>();
        CreateMap<ImageDto, Image>();
    }
}
