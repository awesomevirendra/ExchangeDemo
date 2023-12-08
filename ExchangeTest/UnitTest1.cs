using AutoMapper;
using ExchangeDemo.Controllers;
using ExchangeDemo.POCO.Factory;
using ExchangeDemo.Services;
using ExchangeDemo.Services.Interface;
using Microsoft.Extensions.Configuration;
using Moq;

namespace ExchangeTest
{
    [TestFixture]
    public class ExchangeServiceTests
    {
        private ExchangeService _exchangeService;
        private IMapper _mapper;
        private Mock<ISave> _saveMock;
        private Mock<SaveFactory> _saveFactoryMock;
        private Mock<IConfiguration> _configurationMock;
        private Mock<IRateService> _rateServiceMock;
        private Mock<IServiceProvider> _serviceProvider;

        [SetUp]
        public void Setup()
        {
            _serviceProvider = new Mock<IServiceProvider>();
            _mapper = new Mapper(new MapperConfiguration(cfg => cfg.AddProfiles(new List<Profile>() {
                    new ExchangeDemo.POCO.AutoProfile.ExchangeProfile(),
                    new ExchangeDemo.POCO.AutoProfile.RateProfile()}
                )
            ));

            _saveMock = new Mock<ISave>();
            _saveFactoryMock = new Mock<SaveFactory>(_serviceProvider.Object);
            _configurationMock = new Mock<IConfiguration>();
            _rateServiceMock = new Mock<IRateService>();

            _exchangeService = new ExchangeService(
                _mapper,
                _saveFactoryMock.Object,
                _configurationMock.Object,
                _rateServiceMock.Object
            );
        }

        [Test]
        public async Task Latest_SaveIsNull_ReturnsMappedExchange()
        {

            // Arrange
            _rateServiceMock
                .Setup(rateService => rateService.Get())
                .ReturnsAsync("{\"disclaimer\":\"Usage subject to terms: https://openexchangerates.org/terms\",\"license\":\"https://openexchangerates.org/license\",\"timestamp\":1701928800,\"base\":\"USD\",\"rates\":{\"AED\":3.6725,\"AFN\":69.591866,\"ALL\":94.06937,\"AMD\":402.466597,\"ANG\":1.804126,\"AOA\":830.594333,\"ARS\":363.57329,\"AUD\":1.530243,\"AWG\":1.8,\"AZN\":1.7,\"BAM\":1.816206}}");

            _saveMock
                .Setup(save => save.GetLatestRates())
                .Returns((ExchangeDemo.POCO.Exchange)null);

            // Act
            var result = await _exchangeService.Latest();

            Assert.IsNotNull(result);
        }
    }
}