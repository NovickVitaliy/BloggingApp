using AutoMapper;
using BloggingApp.Web.Configurations.AutoMapperConverters;
using BloggingApp.Web.Models.DTOs;
using BloggingApp.Web.Models.Main;
using BloggingApp.Web.Models.Main.Blogs;
using BloggingAppV2.Models.Main.Identity;

namespace BloggingApp.Web.Configurations;

public class AutoMapperProfile : Profile
{
    public AutoMapperProfile()
    {
        CreateMap<User, UserDto>();
        
        CreateMap<Photo, PhotoDto>();
        
        CreateMap<EditProfileDto, User>().ReverseMap()
            .ForMember(one => one.CountryName, 
                opt => opt.MapFrom(u => u.Country.Name));
        
        CreateMap<Country, CountryDto>();

        CreateMap<CreatePostRequest, Post>()
            .ForMember(post => post.Tags,
                opt => opt.ConvertUsing(new TagRequestToTagConverter()));

        CreateMap<Post, PostResponse>()
            .ForMember(response => response.Tags,
                opt => opt.ConvertUsing(new TagsToTagsResponseConverter()));

        CreateMap<Post, EditPostRequest>()
            .ForMember(request => request.Tags,
                opt => opt.ConvertUsing(new TagToTagRequestConverter()));

        CreateMap<EditPostRequest, Post>()
            .ForMember(post => post.Tags,
                opt => opt.ConvertUsing(new TagRequestToTagConverter()));
    }
}