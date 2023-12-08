using AutoMapper;

namespace ExchangeDemo.POCO.AutoProfile
{
    public class ExchangeProfile : Profile
    {
        public ExchangeProfile()
        {
            CreateMap<ExchangeResponse, Exchange>()
                .ForMember(x => x.Rates, y => y.MapFrom(z => z.rates))
                .ForMember(x => x.License, y => y.MapFrom(z => z.license))
                .ForMember(x => x.Disclaimer, y => y.MapFrom(z => z.disclaimer))
                .ForMember(x => x.TimeStamp, y => y.MapFrom(z => z.timestamp));

            CreateMap<Exchange, ExchangeResponse>();
        }
    }
}
