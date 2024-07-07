namespace ConsumeRedditWebApi.Models
{
    public class User
    {
        public string comment_karma { get; set; }
        public string created { get; set; }
        public string created_utc { get; set; }
        public string has_mail { get; set; }
        public string has_mod_mail { get; set; }
        public string has_verified_email { get; set; }
        public string id { get; set; }
        public string is_gold { get; set; }
        public string is_mod { get; set; }
        public string link_karma { get; set; }
        public string name { get; set; }
        public string over_18 { get; set; }
    }
}
