using AutoMapper;

namespace NewsPortal.Logic.MapperProfiles
{
    public class UserCommentProfile  : Profile
    {
        public UserCommentProfile()
        {
            CreateMap<Data.Models.Comment, Logic.Models.Comment>().ReverseMap();
        }
    }
}
