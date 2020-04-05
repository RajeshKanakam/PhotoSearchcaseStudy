using System;
using PhotoSearch.BLL.Commands;
using System.Collections.ObjectModel;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;
using System.Windows.Input;
using PhotoSearch.BLL.Interfaces;
using PhotoSearch.BLL.Models;
using PhotoSearch.BLL.Models.FlickrSearchModels;
using PhotoSearch.BLL.Models.TwitterSearchModels;

namespace PhotoSearch.BLL.ViewModels
{
    /// <summary>
    /// ViewModel class to support Photo Search View
    /// </summary>
    public class PhotoSearchViewModel : ViewModelBase
    {
        public ObservableCollection<Status> TweetsList { get; set; }

        private readonly ISearchService<Photo> _flickrFeedPhotoSearchService;
        private readonly ISearchService<Status> _twitterSearchService;

        private readonly string httpRequestExceptionMsg =
            "Error while sending the Search request. Possible reason could be that your PC cannot access Internet. Check and try again.";
        private readonly string searchlabelWithNoPhotosMsg = "No Photos are found for the Search. Please try again!!";
        private readonly string searchLabelWithInvalidSearchMsg = "Search String cannot be Empty. Please try again!!";
        private readonly string searchLabelDefaultMsg = "Search results are Empty. Perform Search to view Photos here.";

        private ObservableCollection<PhotoWithTweets> _photosList;
        public ObservableCollection<PhotoWithTweets> PhotosList
        {
            get => _photosList;
            set
            {
                _photosList = value;
                NotifyPropertyChanged();
            }
        }

        private bool _canExecuteSearch;
        public bool ExecuteSearch
        {
            get => _canExecuteSearch;
            set
            {
                _canExecuteSearch = value;
                NotifyPropertyChanged();
            }
        }

        private string _searchString;
        public string SearchString
        {
            get => _searchString;
            set
            {
                _searchString = value;
                NotifyPropertyChanged();
                this._flickrFeedPhotoSearchService.SearchString = value;
                this._twitterSearchService.SearchString = value;
                ExecuteSearch = true;
            }
        }

        private bool _searchLabelVisibility;
        public bool SearchLabelVisibility
        {
            get => _searchLabelVisibility;
            set
            {
                _searchLabelVisibility = value;
                NotifyPropertyChanged();
            }
        }

        private string _searchLabel;
        public string SearchLabel
        {
            get => _searchLabel;
            set
            {
                _searchLabel = value;
                NotifyPropertyChanged();
            }
        }

        private bool _photoListVisibility;
        public bool PhotoListVisibility
        {
            get => _photoListVisibility;
            set
            {
                _photoListVisibility = value;
                NotifyPropertyChanged();
            }
        }

        private ICommand _searchCommand;

        public ICommand SearchCommand
        {
            get
            {
                return _searchCommand ?? (_searchCommand = new RelayCommand(
                    param => this.StartSearch().GetAwaiter(),
                    param => this.CanSearch()
                ));
            }
        }

        public PhotoSearchViewModel(ISearchService<Photo> flickrFeedPhotoSearchService, ISearchService<Status> tweetSearchService)
        {
            this._flickrFeedPhotoSearchService = flickrFeedPhotoSearchService;
            this._twitterSearchService = tweetSearchService;

            TweetsList = new ObservableCollection<Status>();
            PhotosList = new ObservableCollection<PhotoWithTweets>();
            SearchLabel = searchLabelDefaultMsg;
            UpdateVisibility(true, false);
        }

        private void UpdateVisibility(bool searchVisibility, bool photoVisibility)
        {
            SearchLabelVisibility = searchVisibility;
            PhotoListVisibility = photoVisibility;
        }

        /// <summary>
        /// Command is enabled based on ExecuteSearch variable
        /// </summary>
        /// <returns></returns>
        private bool CanSearch()
        {
            // Verify command can be executed here
            return ExecuteSearch;
        }

        /// <summary>
        /// Command to execute a Search query
        /// </summary>
        /// <returns></returns>
        private async Task StartSearch()
        {
            if(!IsValidSearch()) return;

            // Reset Visibility
            UpdateVisibility(false, false);

            try
            {
                Mouse.OverrideCursor = Cursors.Wait;
                ExecuteSearch = false;
                PhotosList.Clear();
                var results = await _flickrFeedPhotoSearchService.ExecuteSearch();
                var tweetsSearchResults = await _twitterSearchService.ExecuteSearch();

                if (results == null || results.Count == 0 || tweetsSearchResults == null ||
                    tweetsSearchResults.Count == 0)
                {
                    UpdateVisibility(true, false); // Enable Search label
                    SearchLabel = searchlabelWithNoPhotosMsg ;
                    return;
                }

                var result = (from photo in results
                    from tweet in tweetsSearchResults
                    where results.IndexOf(photo) == tweetsSearchResults.IndexOf(tweet)
                    select new PhotoWithTweets()
                    {
                        PhotoUrl = photo.Media.M,
                        TwitterUserName = tweet.User.Name,
                        TwitTwitterUserId = tweet.User.Screen_Name,
                        TweetTimeStamp = tweet.Created_At.Substring(4, 6),
                        TweetMessage = tweet.Text
                    });

                PhotosList = new ObservableCollection<PhotoWithTweets>(result.ToList());

                UpdateVisibility(false, true); // Make Photos Visible
            }
            catch (HttpRequestException webEx)
            {
                UpdateVisibility(true, false);
                SearchLabel = httpRequestExceptionMsg;
            }
            catch (Exception ex)
            {
                UpdateVisibility(true, false);
                SearchLabel = ex.Message;
            }
            finally
            {
                Mouse.OverrideCursor = null;
                ExecuteSearch = true;
            }
        }

        private bool IsValidSearch()
        {
            if (string.IsNullOrWhiteSpace(SearchString))
            {
                UpdateVisibility(true, false);
                SearchLabel = searchLabelWithInvalidSearchMsg;
                return false;
            }

            return true;
        }
    }
}