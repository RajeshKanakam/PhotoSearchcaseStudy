using System.Collections.Generic;
using System.Configuration;
using System.Threading.Tasks;
using Newtonsoft.Json;
using PhotoSearch.BLL.Interfaces;
using PhotoSearch.BLL.Models.TwitterSearchModels;
using PhotoSearch.BLL.Utilities;
using PhotoSearch.BLL.ViewModels;

namespace PhotoSearch.BLL.Services
{
    public class TwitterSearchService : ViewModelBase, ISearchService<Status>
    {
        private readonly string _consumerKey = ConfigurationManager.AppSettings["twitterConsumerKey"];
        private readonly string _consumerSecret = ConfigurationManager.AppSettings["twitterConsumerSecret"];
        private readonly string _searchUrl = "https://api.twitter.com/1.1/search/tweets.json?q={0}&include_entities=false&count={1}";
        private string _authorizationUrl = "https://api.twitter.com/oauth2/token";

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

        public async Task<List<Status>> ExecuteSearch()
        {
            string formattedToken = _consumerKey + ":" + _consumerSecret;
            string formattedUrl = string.Format(_searchUrl, SearchString, MaxPerPage);
            string authorizationToken = "Bearer " + HttpUtility.GetTwitterBearerToken(_authorizationUrl, formattedToken);
            string response = HttpUtility.GetTwitterSearchResponse(formattedUrl, authorizationToken);
            var twitterResponseResult = JsonConvert.DeserializeObject<TwitterSearchMetaData>(response);
            return twitterResponseResult.Statuses;
        }
    }
}
