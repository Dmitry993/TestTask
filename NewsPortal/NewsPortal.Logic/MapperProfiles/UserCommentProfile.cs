using AutoMapper;
using System;
using System.Collections.Generic;
using System.Text;
using NewsPortal.Logic.Model;
using NewsPortal.Data.Models;

namespace NewsPortal.Logic.MapperProfiles
{
    public class UserCommentProfile  : Profile
    {
        public UserCommentProfile()
        {
            CreateMap<Comment, UserComment>().ReverseMap();
        }
    }
}
