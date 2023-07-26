using AutoMapper;
using SantanderCodingTest.Utils;

namespace SantanderCodingTest.AutoMapper
{
    public class HackerStoryProfile : Profile
    {
        public HackerStoryProfile() 
        {
            CreateMap<ExternalApiClients.HackerNews.Models.HackerStory, Models.HackerStory>()
                .ForMember(x => x.CommentCount, opt => opt.MapFrom(s => s.Descendants))
                .ForMember(x => x.PostedBy, opt => opt.MapFrom(s => s.By))
                .ForMember(x => x.Uri, opt => opt.MapFrom(s => s.Url))
                .ForMember(x => x.Time, opt => opt.MapFrom(s => DateTimeUtils.UnixTimeStampToDateTime(s.Time)));
        }
    }
}
