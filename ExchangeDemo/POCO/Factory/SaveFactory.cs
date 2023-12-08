using ExchangeDemo.Services;
using ExchangeDemo.Services.Interface;

namespace ExchangeDemo.POCO.Factory
{
    public class SaveFactory
    {
        private readonly IServiceProvider _serviceProvider;
        public SaveFactory(IServiceProvider serviceProvider)
        {
            _serviceProvider = serviceProvider;
        }

        public ISave? GetSaveService(int mode = 1)
        {
            if (mode == 1)
                return _serviceProvider?.GetService<InMemorySaveService>();
            else
                throw new Exception("Implement other Save stratagy");

        }
    }
}
