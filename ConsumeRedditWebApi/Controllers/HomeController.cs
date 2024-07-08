using ConsumeRedditWebApi.Models;
using ConsumeRedditWebApi.Services;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Diagnostics;
using System.Net.Http;
using System.Text.Json;
using System.Text;
using Microsoft.Extensions.Hosting;
using Microsoft.AspNetCore.OutputCaching;
//using AspNetCore;

namespace ConsumeRedditWebApi.Controllers
{
    public class HomeController : Controller
    {
        private readonly IRedditAccountService _redditAccountService;
        private readonly IRedditService _redditService;
        private readonly IConfiguration _configuration;

        public HomeController(IRedditAccountService redditAccountService, IConfiguration configuration, IRedditService redditService)
        {
            _redditAccountService = redditAccountService;
            _configuration = configuration;
            _redditService = redditService;
        }

        public IActionResult Index()
        { 
            return View();
        }

        public async Task<IActionResult> SubredditTopPostsPartialView()
        {
            var subReddits = await GetTopPosts("OUTFITS", 100);
            return PartialView("_partialView1", subReddits);
        }
        public async Task<IActionResult> SubredditTopUserPostsPartialView()
        {
            var subReddits = await GetTopUserPosts("OUTFITS", 100);
            return PartialView("_partialView2", subReddits);
        }
        private async Task<IEnumerable<SubReddit>> GetTopPosts(string subject, int limit)
        {
            try
            {
                var token = await _redditAccountService.GetToken(_configuration["Reddit:AppId"], _configuration["Reddit:AppSecret"]);

                var subReddits = await _redditService.GetMostUpVotes(subject,limit, token);

                return subReddits;
            }
            catch (Exception ex)
            {
                Debug.Write(ex);

                return Enumerable.Empty<SubReddit>();
            }
        }
        private async Task<IEnumerable<SubReddit>> GetTopUserPosts(string subject, int limit)
        {
            try
            {
                var token = await _redditAccountService.GetToken(_configuration["Reddit:AppId"], _configuration["Reddit:AppSecret"]);

                var subReddits = await _redditService.GetMostUserPosts(subject, limit, token);

                return subReddits;
            }
            catch (Exception ex)
            {
                Debug.Write(ex);

                return Enumerable.Empty<SubReddit>();
            }
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }

    }
}
