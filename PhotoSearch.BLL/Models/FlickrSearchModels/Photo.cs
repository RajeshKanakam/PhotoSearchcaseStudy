namespace PhotoSearch.BLL.Models.FlickrSearchModels
{
    /// <summary>
    /// Photo model class. This is returned in JSON result from Flickr Feed Search API.
    /// The Photo class provides a Media object that provides Url for image
    /// </summary>
    public class Photo
    {
        public Media Media { get; set; }
    }
}
