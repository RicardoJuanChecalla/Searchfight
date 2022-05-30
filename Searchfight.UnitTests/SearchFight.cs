using System;
using Xunit;
using System.Threading.Tasks;
using System.Collections.Generic;
using System.Linq;
using Searchfight.Core.Interfaces;
using Searchfight.Infrastructure.Factorys;
using Searchfight.Core.Models;

namespace Searchfight.UnitTests
{
    public class SearchFight
    {
        private ISearchManager _searchManager;

        public SearchFight()
        {
            _searchManager = SearchFightFactory.CreateSearchManager();
        }

        [Fact]
        public void GetSearchReport_WithNullQuerys_ShouldThrowArgumentNullException()
        {
            List<string>? querys = null;
            var result =  async  () => await _searchManager.GetSearchReport(querys!);
            Assert.ThrowsAsync<ArgumentNullException>(result);
        }

        [Fact]
        public async Task GetSearchReport_WithOkQuerys_ShouldGenerateReportAsString()
        {
            var querys = new List<string>() { ".net","java" };
            var result = await _searchManager.GetSearchReport(querys);
            Assert.IsType<string>(result);
        }

        [Fact]
        public async Task GetResultsAsync_OkSearchResults_ShouldNotBeEmpty()
        {
            var querys = new List<string> { ".net", "java" };
            var results = await _searchManager.GetResultsAsync(querys);
            Assert.NotEmpty(results);
        }

        [Fact]
        public void GetWinners_WithNetAsGoogleWinner_ShouldGenerateWinners()
        {
            var searchResults = new List<SearchResult>
            {
                new SearchResult
                {
                    Query = ".net",
                    SearchClient = "Google",
                    TotalResults = 50000
                },
                new SearchResult
                {
                    Query = ".net",
                    SearchClient = "MSN Search",
                    TotalResults = 5000
                },
                new SearchResult
                {
                    Query = "java",
                    SearchClient = "Google",
                    TotalResults = 3000
                },
                new SearchResult
                {
                    Query = "java",
                    SearchClient = "MSN Search",
                    TotalResults = 5000
                },
            };
            var results = _searchManager.GetWinners(searchResults);
             Assert.Collection(results, item => Assert.Contains("Google", item.ClientName),
                              item => Assert.Contains(".net", item.WinnerQuery));
        }

        [Fact]
        public void GetTotalWinner_WithNetAsTotalWinner_ShouldGenerateWinnerString()
        {
            var searchResults = new List<SearchResult>
            {
                new SearchResult
                {
                    Query = ".net",
                    SearchClient = "Google",
                    TotalResults = 50000
                },
                new SearchResult
                {
                    Query = ".net",
                    SearchClient = "MSN Search",
                    TotalResults = 50000
                },
                new SearchResult
                {
                    Query = "java",
                    SearchClient = "Google",
                    TotalResults = 3000
                },
                new SearchResult
                {
                    Query = "java",
                    SearchClient = "MSN Search",
                    TotalResults = 5000
                },
            };
            var results = _searchManager.GetTotalWinner(searchResults);
            Assert.Equal(".net",results);
        }

        [Fact]
        public void GetTotalWinner_WithNullSearchResults_ShouldThrowArgumentNullException()
        {
            List<SearchResult>? searchResults = null;
            Action result =  () => _searchManager.GetTotalWinner(searchResults!);
            Assert.Throws<ArgumentNullException>(result);
        }

        [Fact]
        public void GetMainResults_WithGoodSearchResults_ShouldGenerateLookUpBasedOnQuery()
        {
            var searchResults = new List<SearchResult>
            {
                new SearchResult
                {
                    Query = ".net",
                    SearchClient = "Google",
                    TotalResults = 50000
                },
                new SearchResult
                {
                    Query = ".net",
                    SearchClient = "MSN Search",
                    TotalResults = 50000
                },
                new SearchResult
                {
                    Query = "java",
                    SearchClient = "Google",
                    TotalResults = 3000
                },
                new SearchResult
                {
                    Query = "java",
                    SearchClient = "MSN Search",
                    TotalResults = 5000
                },
            };
            var results = _searchManager.GetMainResults(searchResults);
            Assert.IsType<Lookup<string, SearchResult>>(results);
        }
    }
}  