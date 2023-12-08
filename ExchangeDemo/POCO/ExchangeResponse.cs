
using AutoMapper.Configuration.Annotations;

namespace ExchangeDemo.POCO
{
    public class Exchange
    {
        [SourceMember("disclaimer")]
        public string Disclaimer { get; set; }

        [SourceMember("license")]
        public string License { get; set; }

        [SourceMember("timestamp")]
        public DateTime TimeStamp { get; set; }

        [SourceMember("base")]
        public string Base { get; set; }

        [SourceMember("rates")]
        public IEnumerable<Rate> Rates { get; set; }

    }

    public class ExchangeResponse
    {
        public string disclaimer { get; set; }

        public string license { get; set; }

        public DateTime timestamp { get; set; }

        public List<Rate> rates { get; set; }
    }
}
