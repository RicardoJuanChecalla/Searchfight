using System;
using Xunit;
using System.Collections.Generic;
using Searchfight.Core.Interfaces;
using Searchfight.Infrastructure.Factorys;

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
            var querys = new List<string>();
            var result =  async  () => await _searchManager.GetSearchReport(querys);
            Assert.ThrowsAsync<ArgumentNullException>(result);
        }
    }
}  