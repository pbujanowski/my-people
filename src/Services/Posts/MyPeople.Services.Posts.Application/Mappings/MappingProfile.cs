using AutoMapper;
using MyPeople.Common.Models.Dtos;
using MyPeople.Services.Posts.Domain.Entities;

namespace MyPeople.Services.Posts.Application.Mappings;

public class MappingProfile : Profile
{
    public MappingProfile()
    {
        CreateMap<Post, PostDto>()
            .ForMember(destination => destination.UserDisplayName, opt => opt.Ignore())
            .ForMember(destination => destination.Images, opt => opt.Ignore());

        CreateMap<PostDto, Post>()
            .ForMember(destination => destination.Images, opt => opt.Ignore());

        CreateMap<CreatePostDto, Post>()
            .ForMember(destination => destination.Id, opt => opt.Ignore())
            .ForMember(destination => destination.CreatedAt, opt => opt.Ignore())
            .ForMember(destination => destination.UpdatedAt, opt => opt.Ignore())
            .ForMember(destination => destination.Images, opt => opt.Ignore());

        CreateMap<DeletePostDto, Post>()
            .ForMember(destination => destination.Content, opt => opt.Ignore())
            .ForMember(destination => destination.Images, opt => opt.Ignore())
            .ForMember(destination => destination.CreatedAt, opt => opt.Ignore())
            .ForMember(destination => destination.UpdatedAt, opt => opt.Ignore());

        CreateMap<UpdatePostDto, Post>()
            .ForMember(destination => destination.Images, opt => opt.Ignore());
    }
}
