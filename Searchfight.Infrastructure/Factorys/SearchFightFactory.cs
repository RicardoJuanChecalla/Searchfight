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
            // var loadedAssemblies = AppDomain.CurrentDomain.GetAssemblies()
            //     ?.Where(assembly => assembly.FullName.StartsWith("SearchFight"));

            // var searchClients = loadedAssemblies
            //     .SelectMany(assembly => assembly.GetTypes())
            //     .Where(type => type.GetInterface(typeof(ISearchClient).ToString()) != null)
            //     .Select(type => Activator.CreateInstance(type) as ISearchClient);

            
            ISearchClient[] searchClients = { new GoogleSearchClient(), new BingSearchClient()};
            return new SearchManager(searchClients);
        }
    }
} 