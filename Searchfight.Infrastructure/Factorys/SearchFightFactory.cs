using Searchfight.Core.Interfaces;
using Searchfight.Core.Logic;
using Searchfight.Core.Services;

namespace Searchfight.Infrastructure.Factorys
{
    public class SearchFightFactory
    {
        public static ISearchManager CreateSearchManager() => CreateSearchClients();

        private static SearchManager CreateSearchClients()
        {
            ISearchClient[] searchClients = { new GoogleSearchClient(), new BingSearchClient()};
            return new SearchManager(searchClients);
        }
    }
} 