using SantanderCodingTest.ExternalApiClients.HackerNews.Models;

namespace SantanderCodingTest.ExternalApiClients.HackerNews
{
    public interface IHackerNewsClient
    {
        Task<IEnumerable<int>> GetBestStoriesIds();


        Task<HackerStory> GetBestStoryById(int id);
    }
}
