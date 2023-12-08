using AutoMapper;
using ExchangeDemo.Models;
using ExchangeDemo.POCO;
using ExchangeDemo.POCO.Factory;
using ExchangeDemo.Services.Interface;
using ExchangeDemo.Util;
using Microsoft.EntityFrameworkCore;
using System.Text.Json;

namespace ExchangeDemo.Services
{
    public class ExchangeService : IExchangeService
    {
        readonly IMapper _mapper;
        readonly ISave? _save;
        readonly SaveFactory _saveFactory;
        readonly IConfiguration _configuration;
        readonly IRateService _rateService;
        public ExchangeService(IMapper mapper, SaveFactory saveFactory, IConfiguration configuration, IRateService rateService)
        {
            _mapper = mapper;
            _saveFactory = saveFactory;
            _save = _saveFactory.GetSaveService();
            _configuration = configuration;
            _rateService = rateService;
        }

        public async Task<ConvertResponse> Convert(string from, string to)
        {
            var rates = await Latest();

            var fromRate = rates.Rates.First(x => x.Name == from);
            var toRate = rates.Rates.First(x => x.Name == to);

            var valueRate = fromRate.ActualRate / toRate.ActualRate;

            return new ConvertResponse
            {
                From = fromRate,
                To = toRate,
                ToValue = valueRate
            };
        }

        async public Task<Exchange?> Latest()
        {
            var latest = _save?.GetLatestRates();

            if (latest == null || latest.TimeStamp.AddHours(5.50) < DateTime.Now.AddHours(-1))
            {
               
                var res = await _rateService.Get();

                var option = new JsonSerializerOptions();
                option.Converters.Add(new CustomJsonConvert());
                option.Converters.Add(new CustomTimestampConvert());

                var x = System.Text.Json.JsonSerializer.Deserialize<POCO.ExchangeResponse>(res, option);

                _save?.Save(x.timestamp, x.rates);

                return _mapper.Map<Exchange>(x);
            }
            else
            {
                return _save?.GetLatestRates();
            }
        }

        public void Save(Exchange exchange)
        {
            _save?.Save(exchange.TimeStamp, exchange.Rates);
        }
    }
}
