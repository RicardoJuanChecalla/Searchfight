using System.Text;
using Searchfight.Core.Interfaces;
using Searchfight.Core.Models;

namespace Searchfight.Core.Logic
{
    public class SearchManager : ISearchManager
    {        
        private readonly IEnumerable<ISearchClient> _searchClients;
        private readonly StringBuilder _stringBuilder;

        public SearchManager(IEnumerable<ISearchClient> searchClients)
        {
            _searchClients = searchClients;
            _stringBuilder = new StringBuilder();
        }

        public async Task<string> GetSearchReport(List<string> querys)
        {
            if (querys == null)
                throw new ArgumentNullException(nameof(querys));

            try
            {
                var searchResults = await GetResultsAsync(querys.Distinct());

                var winnners = GetWinners(searchResults);
                var mainResults = GetMainResults(searchResults);


                var clientResultsString = mainResults
                    .Select(resultsGroup =>
                        $"{resultsGroup.Key}: {string.Join(" ", resultsGroup.Select(client => $"{client.SearchClient}: {client.TotalResults}"))}")
                    .ToList();

                var winnerString = winnners.Select(client => $"{client.ClientName} winner: {client.WinnerQuery}")
                    .ToList();

                clientResultsString.ForEach(queryResults => _stringBuilder.AppendLine(queryResults));
                winnerString.ForEach(winners => _stringBuilder.AppendLine(winners));

                return _stringBuilder.ToString();
            }
            catch (Exception e)
            {
                throw new ArgumentNullException("Error processing results,please try again later", e);
            }
        }

        public IEnumerable<Winner> GetWinners(List<SearchResult> searchResults)
        {
            if (searchResults == null)
                throw new ArgumentNullException(nameof(searchResults));

            var winners = searchResults
                .OrderBy(result => result.SearchClient)
                .GroupBy(result => result.SearchClient, result => result,
                    (client, result) => new Winner
                    {
                        ClientName = client,
                        WinnerQuery = result.Max(r => r.TotalResults).ToString()
                    });

            return winners;
        }

        public string GetTotalWinner(List<SearchResult> searchResults)
        {
            if (searchResults == null)
                throw new ArgumentNullException(nameof(searchResults));

            var totalWinner = searchResults
                .OrderBy(result => result.SearchClient)
                .GroupBy(result => result.Query, result => result,
                    (query, result) => new { Query = query, Total = result.Sum(r => r.TotalResults) })
                .Max(r => r.Total).ToString();

            return totalWinner;
        }

        public IEnumerable<IGrouping<string, SearchResult>> GetMainResults(List<SearchResult> searchResults)
        {
            if (searchResults == null)
                throw new ArgumentNullException(nameof(searchResults));
            
            var results = searchResults 
                .OrderBy(result => result.SearchClient)
                .ToLookup(result => result.Query, result => result);

            return results;
        }
        public async Task<List<SearchResult>> GetResultsAsync(IEnumerable<string> querys)
        {
            var results = new List<SearchResult>();

            foreach (var keyword in querys)
            {
                foreach (var searchClient in _searchClients)
                {
                    results.Add(new SearchResult
                    {
                        SearchClient = searchClient.ClientName,
                        Query = keyword,
                        TotalResults = await searchClient.GetResultsCountAsync(keyword)
                    });
                }
            }

            return results;
        }

    }
}  