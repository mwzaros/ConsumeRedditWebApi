using ConsumeRedditWebApi.Models;
using ConsumeRedditWebApi.Services;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;


namespace ConsumeRedditWebApi.Tests
{
    public class Tests
    {
        private IRedditAccountService _redditAccountService { get; set; }
        private IRedditService _redditService { get; set; }
        private HttpClient _httpClient { get; set; }
        private IConfiguration _configuration { get; set; }


        [SetUp]
        public void Setup()
        {
            _httpClient = new HttpClient();
            _redditAccountService = new RedditAccountService(_httpClient);
            _redditService = new RedditService(_httpClient);

            var builder = new ConfigurationBuilder()
                .SetBasePath(Directory.GetCurrentDirectory())
                .AddJsonFile("appsettings.json", optional: false, reloadOnChange: true)
                .AddEnvironmentVariables();

            _configuration = builder.Build();

        }
        [TearDown]
        public void Cleanup()
        { 
            _httpClient.Dispose();
        }

        [Test]
        public async Task Test1()
        {
            var token = await _redditAccountService.GetToken(_configuration["Reddit:AppId"], _configuration["Reddit:AppSecret"]);
            Assert.True(!String.IsNullOrEmpty(token));
            var response = _redditService.GetMostUserPosts("OUTFITS", 100, token);
            // Assert

            Assert.IsTrue(response != null);

        }
        [Test]
        public async Task Test2()
        {
            var token = await _redditAccountService.GetToken(_configuration["Reddit:AppId"], _configuration["Reddit:AppSecret"]);
            Assert.True(!String.IsNullOrEmpty(token));
            var response = _redditService.GetMostUserPosts("OUTFITS", 100, token);
            // Assert

            Assert.IsTrue(response != null);

        }
        public static IConfiguration InitConfiguration()
        {
            var config = new ConfigurationBuilder()
               .AddJsonFile("appsettings.test.json")
                .AddEnvironmentVariables()
                .Build();
            return config;
        }
    }
}