using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using ExchangeDemo.POCO;
using Newtonsoft.Json;
using System.Text.Json;
using ExchangeDemo.Util;
using ExchangeDemo.Services;
using ExchangeDemo.Services.Interface;

namespace ExchangeDemo.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ExchangeController : ControllerBase
    {
        private IExchangeService _exchangeService;
        public ExchangeController(IExchangeService exchangeService)
        {
            _exchangeService = exchangeService;
        }

        [HttpGet(Name = "LatestExchangeRates")]
        public async Task<POCO.Exchange> Latest()
        {
            return await _exchangeService.Latest();
        }

        [HttpGet]
        [Route("/api/convert")]
        public async Task<ConvertResponse> Convert(string from, string to)
        {
            return await _exchangeService.Convert(from, to);
        }
    }
}
