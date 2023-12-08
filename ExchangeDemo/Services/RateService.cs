using ExchangeDemo.Services.Interface;

namespace ExchangeDemo.Services
{
    public class RateService : IRateService
    {
        IConfiguration _configuration;
        public RateService(IConfiguration configuration)
        {
            _configuration = configuration;
        }
        public async Task<string> Get()
        {
            using var client = new HttpClient();

            var response = await client.GetAsync($"https://openexchangerates.org/api/latest.json?app_id={_configuration["APP_ID"]}");
            var res = await response.Content.ReadAsStringAsync();
            return res;
        }
    }
}
