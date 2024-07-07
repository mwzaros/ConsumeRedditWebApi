using ConsumeRedditWebApi.Models;

namespace ConsumeRedditWebApi.Services
{
    public interface IRedditService
    {
        Task<IEnumerable<SubReddit>> GetMostUpVotes(string subject, int limit, string accessToken);
        Task<IEnumerable<SubReddit>> GetMostUserPosts(string subject, int limit, string accessToken);
    }
}
