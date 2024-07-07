namespace ConsumeRedditWebApi.Services
{
    public interface IRedditAccountService
    {
        Task<string> GetToken(string clientId, string clientSecret);
    }
}
