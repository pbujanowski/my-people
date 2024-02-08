using AutoMapper;
using MyPeople.Common.Models.Dtos;
using MyPeople.Services.Posts.Application.Dtos;
using MyPeople.Services.Posts.Domain.Entities;

namespace MyPeople.Services.Posts.Application.Mappings;

public class MappingProfile : Profile
{
    public MappingProfile()
    {
        CreateMap<Post, PostDto>()
            .ForMember(
                e => e.ImagesIds,
                options =>
                {
                    options.PreCondition(x => x.ImagesIds is not null);
                    options.MapFrom(x => x.ImagesIds!.Select(e => e.ImageId));
                }
            );
        CreateMap<PostDto, Post>();
        CreateMap<CreatePostDto, Post>();
        CreateMap<DeletePostDto, Post>();
        CreateMap<UpdatePostDto, Post>();

        CreateMap<PostImage, PostImageDto>();
        CreateMap<PostImageDto, PostImage>();
        CreateMap<CreatePostImageDto, PostImage>();
    }
}
