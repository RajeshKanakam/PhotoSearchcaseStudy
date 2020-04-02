using System;
using System.Collections.Generic;

namespace PhotoSearch.BLL.Models.FlickrSearchModels
{
    /// <summary>
    /// PhotosMetaData model class. This is returned in JSON result from Flickr Feed Search API.
    /// The PhotosMetaData class provides metadata for each resulted image
    /// </summary>
    public class PhotosMetaData
    {
        public string Title { get; set; }
        public string Link { get; set; }
        public string Description { get; set; }
        public DateTime Modified { get; set; }
        public string Generator { get; set; }
        public List<Photo> Items { get; set; }
    }
}
