using ExchangeDemo.Models;

namespace ExchangeDemo.Services.Interface
{
    public interface ISave
    {
        public int Save(DateTime timeStamp, IEnumerable<POCO.Rate> rate);
        public POCO.Exchange GetRates(int timestampID);
        public POCO.Exchange GetLatestRates();
    }
}
