using AutoMapper;

namespace NewsPortal.Logic.MapperProfiles
{
    public class RatingProfile : Profile
    {
        public RatingProfile()
        {
            CreateMap<Data.Models.Rating, Logic.Models.Rating>().ReverseMap();
        }
    }
}
