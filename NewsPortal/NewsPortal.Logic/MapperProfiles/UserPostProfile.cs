using AutoMapper;

namespace NewsPortal.Logic.MapperProfiles
{
    public class UserPostProfile : Profile
    {
        public UserPostProfile()
        {
            CreateMap<Data.Models.Post, Logic.Models.Post>().ReverseMap();
        }
    }
}
