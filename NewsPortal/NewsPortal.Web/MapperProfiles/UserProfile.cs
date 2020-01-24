using AutoMapper;
using NewsPortal.Logic.Model;
using OAuth2.Models;
using System;

namespace NewsPortal.Web.Profiles
{
    public class UserProfile : Profile
    {
        public UserProfile()
        {
            CreateMap<UserInfo, ApplicationUser>()
                .ForMember(b => b.Id, opt => opt.Ignore())
                .ForMember(b => b.GoogleId, opt => opt.MapFrom(b => b.Id))
                .ForMember(b => b.UserName, opt => opt.MapFrom(b => b.Email.Split('@', StringSplitOptions.None)[0]))
                .ForMember(b => b.Email, opt => opt.MapFrom(b => b.Email))
                .ForMember(b => b.FirstName, opt => opt.MapFrom(b => b.FirstName))
                .ForMember(b => b.LastName, opt => opt.MapFrom(b => b.LastName))
                .ReverseMap();
        }
    }
}
