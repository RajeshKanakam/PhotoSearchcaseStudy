namespace PhotoSearch.BLL.Models.FlickrSearchModels
{
    /// <summary>
    /// Media model class. This is returned in JSON result from Flickr Feed Search API.
    /// The Media class provides a string 'M' that provides Url for image
    /// </summary>
    public class Media
    {
        public string M { get; set; }
    }
}
