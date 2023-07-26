using Microsoft.AspNetCore.Mvc;
using SantanderCodingTest.Exceptions;
using SantanderCodingTest.Models;
using SantanderCodingTest.Services;

namespace SantanderCodingTest.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class HackerStoriesController : ControllerBase
    {
        private readonly IHackerStoriesService _hackerStoriesService;

        public HackerStoriesController(IHackerStoriesService hackerStoriesService)
        {
            _hackerStoriesService = hackerStoriesService;
        }

        [HttpGet]
        public async Task<IEnumerable<HackerStory>> GetBestStories(int quantity)
        {
            if (quantity <= 0)
                throw new BadRequestException("quantity should be greater then 0");

            return await _hackerStoriesService.GetBestStories(quantity);
        }
    }
}