using AutoMapper;
using MyPeople.Services.Posts.Application.Dtos;
using MyPeople.Services.Posts.Domain.Entities;

namespace MyPeople.Services.Posts.Infrastructure.Mappings;

public class MappingProfile : Profile
{
    public MappingProfile()
    {
        CreateMap<Post, PostDto>();
        CreateMap<PostDto, Post>();
    }
}
