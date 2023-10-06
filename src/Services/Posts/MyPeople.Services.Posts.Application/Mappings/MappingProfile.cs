using AutoMapper;
using MyPeople.Common.Models.Dtos;
using MyPeople.Services.Posts.Domain.Entities;

namespace MyPeople.Services.Posts.Application.Mappings;

public class MappingProfile : Profile
{
    public MappingProfile()
    {
        CreateMap<Post, PostDto>();
        CreateMap<PostDto, Post>();
        CreateMap<CreatePostDto, Post>();
        CreateMap<DeletePostDto, Post>();
        CreateMap<UpdatePostDto, Post>();
    }
}
