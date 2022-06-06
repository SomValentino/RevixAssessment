using Moq;
using Revix.Rate.Application.Contracts.Persistence;
using Revix.Rate.Application.Features.Services;
using Revix.Rate.Tests.Setup;
using System.Linq;
using System.Threading.Tasks;
using Xunit;

namespace Revix.Rate.Tests
{
    public class RateServiceTests
    {
        [Fact]
        public async Task GetDailyRate_FetchesDailyRate_ReturnsStatusCode200()
        {
            var rateService = new RateService(new Mock<IRateRepository>().Object, HttpClientMock.CreateHttpClientFactory());

            var coinRate = await rateService.GetDailyRate("https://sandbox-api.coinmarketcap.com/v1/cryptocurrency/listings/latest");

            Assert.NotNull(coinRate);
            Assert.True(coinRate.Data.Count() > 0);
        }
    }
}