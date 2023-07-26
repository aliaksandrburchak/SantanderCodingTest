using AutoMapper;
using SantanderCodingTest.Exceptions;
using SantanderCodingTest.ExternalApiClients.HackerNews;
using SantanderCodingTest.Models;

namespace SantanderCodingTest.Services
{
    public class HackerStoriesService : IHackerStoriesService
    {
        private readonly IHackerNewsClient _hackerNewsClient;
        private readonly IConfiguration _configuration;
        private readonly IMapper _mapper;
        private readonly ILogger<HackerStoriesService> _logger;

        public HackerStoriesService(
            IHackerNewsClient hackerNewsClient, 
            IConfiguration configuration,
            IMapper mapper,
            ILogger<HackerStoriesService> logger) 
        {
            _hackerNewsClient = hackerNewsClient;
            _configuration = configuration;
            _mapper = mapper;
            _logger = logger;
        }

        public async Task<IEnumerable<HackerStory>> GetBestStories(int quantity)
        {
            var storiesIds = await _hackerNewsClient.GetBestStoriesIds();
            storiesIds = storiesIds.Take(quantity);
            
            var result = Enumerable.Empty<HackerStory>();

            var batchSize = GetBatchSize();

            if (storiesIds.Any())
            {
                for (var batchNumber = 1; batchNumber <= (storiesIds.Count() - 1) / batchSize + 1; batchNumber++)
                {
                    var batchIds = storiesIds.Skip((batchNumber - 1) * batchSize).Take(batchSize);
                    var stories = await GetBestStoriesByIds(batchIds);
                    result = result.Concat(stories);
                }

            }

            return result.OrderByDescending(x => x.Score);
        }

        private async Task<IEnumerable<HackerStory>> GetBestStoriesByIds(IEnumerable<int> ids)
        {
            var getStoryTasks = ids.Select(x => Task.Run(() => _hackerNewsClient.GetBestStoryById(x)));
            var stories = await Task.WhenAll(getStoryTasks);
            return _mapper.Map<IEnumerable<HackerStory>>(stories);
        }

        private int GetBatchSize() 
        {
            try 
            {
                var settingValue = _configuration["AppSettings:BatchSize"];
                return Convert.ToInt32(settingValue);
            }
            catch (Exception ex) 
            {
                _logger.LogError(ex, ex.Message);
                throw new InvalidConfigurationException("Invalid configuration");
            }
        }

    }
}