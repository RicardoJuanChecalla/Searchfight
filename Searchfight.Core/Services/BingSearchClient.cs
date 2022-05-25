
using Newtonsoft.Json;
using Searchfight.Core.Entities;
using Searchfight.Core.Interfaces;

namespace Searchfight.Core.Services
{
    public class BingSearchClient : ISearchClient
    {
        public string ClientName => "Bing Search";

        static BingSearchClient()
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
                    RequestUri = new Uri("https://bing-web-search1.p.rapidapi.com/search?q="+query+"&safeSearch=Off&textFormat=Raw"),
                    Headers =
                    {
                        { "X-BingApis-SDK", "true" },
                        { "X-RapidAPI-Host", "bing-web-search1.p.rapidapi.com" },
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
		            var deserializedObject = JsonConvert.DeserializeObject<SearchResultBing>(body);
		            if ( deserializedObject != null &&  deserializedObject.WebPages != null ) 
                    {
                        return deserializedObject.WebPages.TotalEstimatedMatches;
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