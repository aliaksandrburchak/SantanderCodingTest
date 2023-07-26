using SantanderCodingTest.Models;

namespace SantanderCodingTest.Services
{
    public interface IHackerStoriesService
    {
        Task<IEnumerable<HackerStory>> GetBestStories(int quantity);
    }
}
