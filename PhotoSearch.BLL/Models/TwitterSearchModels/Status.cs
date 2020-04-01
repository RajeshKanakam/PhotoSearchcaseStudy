namespace PhotoSearch.BLL.Models.TwitterSearchModels
{

    public class Status
    {
        public string Created_At { get; set; }
        public string Text { get; set; }
        public string Source { get; set; }
        public User User { get; set; }
    }
}
