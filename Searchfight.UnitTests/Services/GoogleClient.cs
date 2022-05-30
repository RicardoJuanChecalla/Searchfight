using System;
using Xunit;
using Searchfight.Core.Services;
using System.Threading.Tasks;

namespace Searchfight.UnitTests.Services
{
    public class GoogleClient
    {
        private GoogleSearchClient _googleSearchClient;
        public GoogleClient()
        {
            _googleSearchClient = new GoogleSearchClient();
        }

        [Fact]
        public async Task GetResultsCountAsync_OkStatus_ShouldGenerateResultsCount()
        {
            var query = "java";
            var result = await _googleSearchClient.GetResultsCountAsync(query);
            Assert.IsType<long>(result);
        }

        [Fact]
        public void GetResultsCountAsync_NullQuery_ShouldThrow_ArgumentNullException()
        {
            string? query = null;
            var result = async () => await _googleSearchClient.GetResultsCountAsync(query!);
            Assert.ThrowsAsync<ArgumentNullException>(result);
        }
    }
}