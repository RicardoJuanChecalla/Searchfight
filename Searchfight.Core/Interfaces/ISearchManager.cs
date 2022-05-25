using Searchfight.Core.Models;

namespace Searchfight.Core.Interfaces
{
    public interface ISearchManager
    {
        Task<string> GetSearchReport(List<string> querys);
        Task<List<SearchResult>> GetResultsAsync(IEnumerable<string> querys);
        IEnumerable<Winner> GetWinners(List<SearchResult> searchResults);
        IEnumerable<IGrouping<string, SearchResult>> GetMainResults(List<SearchResult> searchResults);
    }
} 