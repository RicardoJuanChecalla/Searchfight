using Newtonsoft.Json;
using Searchfight.Core.Entities;
using Searchfight.Core.Interfaces;

namespace Searchfight.Core.Services
{
    public class GoogleSearchClient : ISearchClient
    {
        public string ClientName => "Google Search";
        static GoogleSearchClient()
        {
        }

        public async Task<long> GetResultsCountAsync(string query)
        {
            if (string.IsNullOrWhiteSpace(query))
                throw new ArgumentNullException(nameof(query));
            try
            {
                var client = new HttpClient();
                var httpRequestMessage = new HttpRequestMessage{
                    Method = HttpMethod.Get,
                    RequestUri = new Uri("https://google-search3.p.rapidapi.com/api/v1/search/q="+ query),
                    Headers =   {
                        { "X-User-Agent", "desktop" },
                        { "X-Proxy-Location", "IE" },
                        { "X-RapidAPI-Host", "google-search3.p.rapidapi.com" },
                        { "X-RapidAPI-Key", "c2661271f6msh7ebba2a4bd731f7p1a269djsna5fcdcc18848" },
                        },
	            };
                using (var response = await client.SendAsync(httpRequestMessage))
                {
                    if (!response.IsSuccessStatusCode)
                        throw new ArgumentNullException(
                            "There was an error processing your request. Please try again later...");

                    response.EnsureSuccessStatusCode();
                    var body = await response.Content.ReadAsStringAsync();
                    var deserializedObject = JsonConvert.DeserializeObject<SearchResultGoogle>(body);
                    if ( deserializedObject != null) 
                    {
                        return deserializedObject.Total;
                    }
                    else
                    {
                        return 0;
                    }
                }
            }
            catch (Exception ex)
            {
                throw new ArgumentNullException(ex.Message);
            }
        }
    }
}