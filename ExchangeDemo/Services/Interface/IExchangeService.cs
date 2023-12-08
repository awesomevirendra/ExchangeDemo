
namespace ExchangeDemo.Services.Interface
{
    public interface IExchangeService
    {
        Task<POCO.Exchange?> Latest();
        void Save(POCO.Exchange exchange);
        Task<POCO.ConvertResponse> Convert(string from, string to);
    }
}
