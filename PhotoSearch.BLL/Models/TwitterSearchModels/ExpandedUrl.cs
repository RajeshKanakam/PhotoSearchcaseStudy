namespace PhotoSearch.BLL.Models.TwitterSearchModels
{
    public class ExpandedUrl
    {
        public string Url { get; set; }
        public string Expanded_Url { get; set; }
        public string Display_Url { get; set; }
        public int[] Indices { get; set; }
    }
}
