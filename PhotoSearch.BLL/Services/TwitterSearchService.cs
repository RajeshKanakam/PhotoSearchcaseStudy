using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
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
        private readonly string _searchUrl = ConfigurationManager.AppSettings["twitterSearchUrl"];
        private string _authorizationUrl = ConfigurationManager.AppSettings["twitterAuthorizationUrl"];

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
            try
            {
                string formattedToken = _consumerKey + ":" + _consumerSecret;
                string formattedUrl = string.Format(_searchUrl, SearchString, MaxPerPage);
                string authorizationToken = "Bearer " + await HttpUtility.GetTwitterBearerToken(_authorizationUrl, formattedToken);
                string response = await HttpUtility.GetTwitterSearchResponse(formattedUrl, authorizationToken);
                var twitterResponseResult = JsonConvert.DeserializeObject<TwitterSearchMetaData>(response);
                var twitterErrorResults = JsonConvert.DeserializeObject<TwitterError>(response);
                if ((twitterErrorResults == null || twitterErrorResults.Errors == null) && twitterResponseResult != null)
                {
                    return twitterResponseResult.Statuses;
                }
                else
                {
                    string queryErrorMessage = "An error occured while searching for tweets with specified Search String. Please check with Administrator for any support.";
                    string accessErrorMessage = twitterErrorResults != null ? "Possible Reasons are\n" : "";
                    if (twitterErrorResults != null && twitterErrorResults.Errors != null) 
                    {
                        for (int counter = 1; counter <= twitterErrorResults.Errors.Count(); counter++)
                        {
                            accessErrorMessage += (counter + $". {twitterErrorResults.Errors[counter - 1].Message}\n");
                        }
                    }

                    throw new Exception(queryErrorMessage + accessErrorMessage);
                }
            }
            catch (Exception)
            {
                throw;
            }
        }
    }
}

