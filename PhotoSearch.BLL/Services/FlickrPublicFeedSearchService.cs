using System;
using System.Collections.Generic;
using System.Configuration;
using System.Threading.Tasks;
using Newtonsoft.Json;
using PhotoSearch.BLL.Interfaces;
using PhotoSearch.BLL.Models.FlickrSearchModels;
using PhotoSearch.BLL.Utilities;
using PhotoSearch.BLL.ViewModels;


namespace PhotoSearch.BLL.Services
{
    /// <summary>
    /// FlickrPublicFeedSearchService service class. This service class provides methods to use Flickr Feed Search APIs.
    /// search result provides a list of Photos
    /// </summary>
    public class FlickrPublicFeedSearchService : ViewModelBase, ISearchService<Photo>
    {
        private readonly string _flickrSearchRequestUrl = ConfigurationManager.AppSettings["flickrSearchUrl"];

        private string _searchString;
        public string SearchString
        {
            get => _searchString;
            set
            {
                _searchString = value;
                NotifyPropertyChanged();
            }
        }

        public int MaxPerPage { get; set; } = 20;

        /// <summary>
        /// Method to convert resulted raw JSON of Flickr Feed Search query to PhotoMetaData
        /// </summary>
        /// <returns>A list of Photos</returns>
        public async Task<List<Photo>> ExecuteSearch()
        {
            try
            {
                string result = await ExecuteFlickrRequest(string.Format(_flickrSearchRequestUrl, SearchString));
                var photoMetaData = JsonConvert.DeserializeObject<PhotosMetaData>(result);
                return photoMetaData.Items;
            }
            catch (Exception)
            {
                throw;
            }
        }

        /// <summary>
        /// Method to execute Flickr Feed Search query
        /// </summary>
        /// <param name="url"></param>
        /// <returns>Deserialized JSON data</returns>
        private async Task<string> ExecuteFlickrRequest(string url)
        {
            var result = await HttpUtility.GetResultAsync(url);
            result = result.Replace("jsonFlickrFeed(", "");
            result = result.Substring(0, result.Length - 1);
            return result;
        }
    }
}
