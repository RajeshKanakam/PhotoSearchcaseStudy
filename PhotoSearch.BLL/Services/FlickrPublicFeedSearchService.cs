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

        private async Task<string> ExecuteFlickrRequest(string url)
        {
            
            var result = await HttpUtility.GetResultAsync(url);
            result = result.Replace("jsonFlickrFeed(", "");
            result = result.Substring(0, result.Length - 1);
            return result;
        }
    }
}
