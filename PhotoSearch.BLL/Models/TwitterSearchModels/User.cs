namespace PhotoSearch.BLL.Models.TwitterSearchModels
{

    public class User
    {
        public string Name { get; set; }
        public string Screen_Name { get; set; }
        public string Location { get; set; }
        public string Description { get; set; }
        public string Url { get; set; }
        public Entities Entities { get; set; }
    }
}
