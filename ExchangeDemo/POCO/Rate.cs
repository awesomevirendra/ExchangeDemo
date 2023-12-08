using AutoMapper.Configuration.Annotations;

namespace ExchangeDemo.POCO
{
    public class Rate
    {
        [SourceMember("name")]
        public string? Name { get; set; }

        [SourceMember("rates")]
        public decimal ActualRate { get; set; }
    }
}
