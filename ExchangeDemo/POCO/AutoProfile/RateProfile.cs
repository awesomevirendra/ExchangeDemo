using AutoMapper;
using ExchangeDemo.Models;

namespace ExchangeDemo.POCO.AutoProfile
{
    public class RateProfile : Profile
    {
        public RateProfile()
        {
            CreateMap<Rate, RateEntity>();
            CreateMap<RateEntity, Rate>();
        }
    }
}
