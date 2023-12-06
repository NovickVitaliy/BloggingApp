using AutoMapper;
using BloggingApp.Web.Models.DTOs;
using BloggingApp.Web.Models.Identity;
using BloggingApp.Web.Models.Main;

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
    }
}