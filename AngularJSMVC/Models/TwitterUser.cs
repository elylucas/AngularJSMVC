namespace AngularJSMVC.Models
{
    public class TwitterUser
    {
        public string screenName { get; set; }
        public string name { get; set; }
        public string description { get; set; }
        public int followers { get; set; }
        public int following { get; set; }
        public string imageUrl { get; set; }
        public int tweets { get; set; }
    }
}