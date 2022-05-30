using System;
using Xunit;
using Searchfight.Core.Services;
using System.Threading.Tasks;

namespace Searchfight.UnitTests.Services
{
    public class BingClient
    {
        private BingSearchClient _bingSearchClient;

        public BingClient()
        {
            _bingSearchClient = new BingSearchClient();
        }

        [Fact]
        public async Task GetResultsCountAsync_OkStatus_ShouldGenerateResultsCount()
        {
            var query = ".net";
            var result = await _bingSearchClient.GetResultsCountAsync(query);
            Assert.IsType<long>(result);
        }

        [Fact]
        public void GetResultsCountAsync_NullQuery_ShouldThrowArgumentNullException()
        {
            string? query = null;
            var result = async () => await _bingSearchClient.GetResultsCountAsync(query!);
            Assert.ThrowsAsync<ArgumentNullException>(result);
        }
    }
}