namespace ConsumeRedditWebApi.Models
{
    public class SubReddit
    {
        public string User { get; set; }
        public int UpVotes { get; set; }
        public string Title { get; set; }
        public int NumberOfPosts { get; set; }
    }
}
