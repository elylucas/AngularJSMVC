using System.ComponentModel.DataAnnotations;

namespace AngularJSMVC.Models
{
    public class Tweet
    {
        [Key]
        public string statusId { get; set; }
        public string name { get; set; }
        public string screenName { get; set; }
        public string createdAt { get; set; }
        public string text { get; set; }
        public string imageUrl { get; set; }
        public bool isFavorite { get; set; }
    }
}