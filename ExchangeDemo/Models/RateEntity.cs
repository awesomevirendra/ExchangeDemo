namespace ExchangeDemo.Models
{
    public class RateEntity
    {
        public int ID { get; set; }
        public string? Name { get; set; }
        public decimal ActualRate { get; set; }
        public int TimeStampID { get; set; }
    }
}
