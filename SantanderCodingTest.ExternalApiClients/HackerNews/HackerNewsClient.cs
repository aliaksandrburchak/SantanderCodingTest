using Microsoft.Extensions.Logging;
using Newtonsoft.Json;
using SantanderCodingTest.ExternalApiClients.Exceptions;
using SantanderCodingTest.ExternalApiClients.HackerNews.Models;
using System.Net;

namespace SantanderCodingTest.ExternalApiClients.HackerNews
{
    public class HackerNewsClient : IHackerNewsClient
    {
        private readonly HttpClient _httpClient;
        private readonly ILogger<HackerNewsClient> _logger;

        public HackerNewsClient(HttpClient httpClient, ILogger<HackerNewsClient> logger) 
        { 
            _httpClient = httpClient;
            _logger = logger;
        }

        public async Task<IEnumerable<int>> GetBestStoriesIds()
        {
            return await GetAsync<IEnumerable<int>>("https://hacker-news.firebaseio.com/v0/beststories.json");
        }

        public async Task<HackerStory> GetBestStoryById(int id)
        {
            return await GetAsync<HackerStory>($"https://hacker-news.firebaseio.com/v0/item/{id}.json");
        }

        private async Task<T> GetAsync<T>(string uri) {
            _logger.LogInformation("Sending request to {0}", uri);

            HttpResponseMessage response = null;

            try
            {
                response = await _httpClient.GetAsync(uri);
            }

            catch (Exception ex)
            {
                _logger.LogError("Error while sending request to {0}", uri);
                _logger.LogError(ex, ex.Message);
                throw;
            }

            if (response.StatusCode != HttpStatusCode.OK)
            {
                _logger.LogError("Failure Status Code.{0} returned {1}", uri, (int)response.StatusCode);
                throw new ExternalApiException($"{uri} returned {(int)response.StatusCode}");
            }

            try
            {
                var content = await response.Content.ReadAsStringAsync();
                return JsonConvert.DeserializeObject<T>(content);
            }
            catch (Exception ex)
            {
                _logger.LogError("Error deserializing content to {0}", typeof(T));
                _logger.LogError(ex, ex.Message);
                throw;
            }
        }
    }
}