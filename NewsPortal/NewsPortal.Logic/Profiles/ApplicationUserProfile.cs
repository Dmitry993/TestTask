using AutoMapper;
using NewsPortal.Data.Model;
using NewsPortal.Logic.Model;
using System;
using System.Collections.Generic;
using System.Text;

namespace NewsPortal.Logic.Profiles
{
    public class ApplicationUserProfile: Profile
    {
        public ApplicationUserProfile()
        {
            CreateMap<ApplicationUser, ApplicationUserDb>()
                .ForMember(b => b.Id, opt => opt.MapFrom(b => b.Id))
                .ForMember(b => b.UserName, opt => opt.MapFrom(b => b.UserName))
                .ForMember(b => b.FirstName, opt => opt.MapFrom(b => b.FirstName))
                .ForMember(b => b.LastName, opt => opt.MapFrom(b => b.LastName))
                .ForMember(b => b.Email, opt => opt.MapFrom(b => b.Email))
                .ForMember(b => b.Password, opt => opt.MapFrom(b => b.Password))
                .ReverseMap();
        }
    }
}
