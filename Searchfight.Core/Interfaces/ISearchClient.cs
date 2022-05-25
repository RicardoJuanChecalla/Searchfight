namespace Searchfight.Core.Interfaces
{
    public interface ISearchClient
    {
        string ClientName { get; }
        Task<long> GetResultsCountAsync(string query);
    }
} 