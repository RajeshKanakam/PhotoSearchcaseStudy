using System;
using System.Collections.Generic;

namespace PhotoSearch.BLL.Models.FlickrSearchModels
{
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
