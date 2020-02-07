using AutoMapper;
using NewsPortal.Data.Model;
using NewsPortal.Logic.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace NewsPortal.Logic.Profiles
{
    public class ApplicationUserProfile: Profile
    {
        public ApplicationUserProfile()
        {
            CreateMap<ApplicationUser, User>().ReverseMap();            
        }
    }
}
