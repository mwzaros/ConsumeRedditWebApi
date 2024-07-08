using System.Text;
using System.Text.Json;
using ConsumeRedditWebApi.Models;
using System.Net;
using Microsoft.AspNetCore.Http;

namespace ConsumeRedditWebApi.Services
{
    public class RedditAccountService : IRedditAccountService
    {
        private readonly HttpClient _httpClient;

        public RedditAccountService(HttpClient httpClient)
        {
            _httpClient = httpClient;
            if (httpClient.BaseAddress == null)
                httpClient.BaseAddress = new Uri("https://www.reddit.com/api/v1/");

        }
        public async Task<string> GetToken(string clientId, string clientSecret)
        {
            var request = new HttpRequestMessage(HttpMethod.Post, "access_token");

            request.Headers.Authorization = new System.Net.Http.Headers.AuthenticationHeaderValue(
                "Basic", Convert.ToBase64String(Encoding.UTF8.GetBytes($"{clientId}:{clientSecret}")));

            request.Headers.Add("User-Agent", "True_Fortune_9768");
            request.Content = new FormUrlEncodedContent(new Dictionary<string, string>
            {
                { "grant_type", "password"},
                    { "username", "True_Fortune_9768"},
                    { "password", "xU0ZXetxlOebA6G1" }
            });

            var response = await _httpClient.SendAsync(request);

            response.EnsureSuccessStatusCode();

            using var responseStream = await response.Content.ReadAsStreamAsync();
            var authResult = await JsonSerializer.DeserializeAsync<AuthResult>(responseStream);

            return authResult.access_token;

        }
    }
}
