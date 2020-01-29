using AutoMapper;
using NewsPortal.Data.Models;
using NewsPortal.Logic.Model;

namespace NewsPortal.Logic.MapperProfiles
{
    public class UserPostProfile : Profile
    {
        public UserPostProfile()
        {
            CreateMap<Post, UserPost>().ReverseMap();
        }
    }
}
