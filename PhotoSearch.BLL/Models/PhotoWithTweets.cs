namespace PhotoSearch.BLL.Models
{
    public class PhotoWithTweets
    {
        public string PhotoUrl { get; set; }

        public string TwitterUserName { get; set; }

        public string TwitterUserId { get; set; }

        public string TweetTimeStamp { get; set; }

        public string TweetMessage { get; set; }
    }
    
}
