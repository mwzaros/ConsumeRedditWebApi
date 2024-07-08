using ConsumeRedditWebApi.Models;
using System.Net.Http.Headers;
using System.Net.Http;
using System.Text.Json;
using static System.Net.WebRequestMethods;

namespace ConsumeRedditWebApi.Services
{
    public class RedditService : IRedditService
    {
        private readonly HttpClient _httpClient;

        public RedditService(HttpClient httpClient)
        {
            _httpClient = httpClient;
            if (httpClient.BaseAddress == null)
                httpClient.BaseAddress = new Uri("https://oauth.reddit.com/");
        }
        public async Task<IEnumerable<SubReddit>> GetMostUpVotes(string subject, int limit, string accessToken)
        {
            _httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("bearer", accessToken);
            _httpClient.DefaultRequestHeaders.UserAgent.Add(new ProductInfoHeaderValue("(+True_Fortune_9768)"));

            var response = await _httpClient.GetAsync($"r/" + subject + "/new.json?limit="+ limit.ToString());

            response.EnsureSuccessStatusCode();

            using var responseStream = await response.Content.ReadAsStreamAsync();
            var responseObject = await JsonSerializer.DeserializeAsync<SubRedditResult>(responseStream);

            var data = responseObject?.data?.children?
           .OrderByDescending(x => x.data.ups)
           .ToList();

            return data.Select(i => new SubReddit
            {
                User = i.data.author,
                UpVotes = i.data.ups,
                Title = i.data.title
            });
        }
        public async Task<IEnumerable<SubReddit>> GetMostUserPosts(string subject, int limit, string accessToken)
        {
            _httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("bearer", accessToken);
            _httpClient.DefaultRequestHeaders.UserAgent.Add(new ProductInfoHeaderValue("(+True_Fortune_9768)"));

            var response = await _httpClient.GetAsync($"r/" + subject + "/new.json?limit=100");

            response.EnsureSuccessStatusCode();

            using var responseStream = await response.Content.ReadAsStreamAsync();
            var responseObject = await JsonSerializer.DeserializeAsync<SubRedditResult>(responseStream);

            var data = responseObject?.data?.children?
           .GroupBy(x => x.data.author)
           .Select(x => new
           {
               User = x.Key,
               Count = x.Count()
           })
           .OrderByDescending(x => x.Count)
           .ToList();

            return data.Select(i => new SubReddit
            {
                User = i.User,
                NumberOfPosts = i.Count
            });

        }
    }
}
