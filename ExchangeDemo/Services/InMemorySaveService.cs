using AutoMapper;
using ExchangeDemo.Models;
using ExchangeDemo.POCO;
using ExchangeDemo.Services.Interface;
using Microsoft.EntityFrameworkCore;

namespace ExchangeDemo.Services
{
    public class InMemorySaveService : ISave
    {
        IMapper _mapper;
        public InMemorySaveService(IMapper mapper)
        {
            _mapper = mapper;
        }

        public POCO.Exchange GetLatestRates()
        {
            var b = new DbContextOptionsBuilder<ExchangeContext>();

            b.UseInMemoryDatabase("exchangeDB");

            var option = b.Options;

            using(var context = new ExchangeContext(option))
            {
                var timestamp =  context.TimeStamps.OrderByDescending(x => x.ID).FirstOrDefault();
                if (timestamp != null)
                {
                    var rates = _mapper.Map<IList<POCO.Rate>>(context.Rates.Where(x => x.TimeStampID == timestamp.ID).ToList());
                    return new POCO.Exchange { Rates = rates, TimeStamp = timestamp.TimeStamp };
                }

                return null;
            }

        }

        public POCO.Exchange GetRates(int timestampID)
        {
            var b = new DbContextOptionsBuilder<ExchangeContext>();

            b.UseInMemoryDatabase("exchangeDB");

            var option = b.Options;

            using (var context = new ExchangeContext(option))
            {
                var timestamp = context.TimeStamps.First(x => x.ID == timestampID);
                var rates = _mapper.Map<IList<POCO.Rate>>(context.Rates.Where(x => x.TimeStampID == timestampID).ToList());
                return new POCO.Exchange { Rates = rates, TimeStamp = timestamp.TimeStamp };
            }
        }

        public int Save(DateTime timeStamp, IEnumerable<POCO.Rate> rate)
        {
            var b = new DbContextOptionsBuilder<ExchangeContext>();

            b.UseInMemoryDatabase("exchangeDB");

            var option = b.Options;

            using (var context = new ExchangeContext(option))
            {
                var timestampEntity = new TimestampEntity { TimeStamp = timeStamp };
                context.TimeStamps.Add(timestampEntity);
                if(context.SaveChanges() > 0)
                {
                    var rateEntity = _mapper.Map<IList<RateEntity>>(rate).Select(x =>{
                        x.TimeStampID = timestampEntity.ID;
                        return x;
                    });

                    context.Rates.AddRange(rateEntity);
                    return context.SaveChanges();
                }
                return 0;
            }
        }
    }
}
