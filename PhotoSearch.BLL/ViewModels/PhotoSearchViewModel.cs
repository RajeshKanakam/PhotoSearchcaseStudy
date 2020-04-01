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
    public class PhotoSearchViewModel : ViewModelBase
    {
        public ObservableCollection<Status> TweetsList { get; set; }

        private readonly ISearchService<Photo> _flickrFeedPhotoSearchService;
        private readonly ISearchService<Status> _twitterSearchService;
        private bool _canExecuteSearch;
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

        private string _searchString;
        public string SearchString
        {
            get => _searchString;
            set
            {
                _searchString = value;
                this._flickrFeedPhotoSearchService.SearchString = value;
                this._twitterSearchService.SearchString = value;
                _canExecuteSearch = true;
                NotifyPropertyChanged();
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
            SearchLabel = "Search results are Empty. Perform Search to view Photos here.";
            SearchLabelVisibility = true;
            PhotoListVisibility = false;
        }

        private bool CanSearch()
        {
            // Verify command can be executed here
            return _canExecuteSearch;
        }

        private async Task StartSearch()
        {
            if (string.IsNullOrWhiteSpace(SearchString))
            {
                SearchLabelVisibility = true;
                PhotoListVisibility = false;
                SearchLabel = "Seach String cannot be Empty. Please try again!!";
                return;
            }
            // Save command execution logic
            SearchLabelVisibility = false;
            PhotoListVisibility = false;
            try
            {
                Mouse.OverrideCursor = Cursors.Wait;
                _canExecuteSearch = false;
                PhotosList.Clear();
                var results = await _flickrFeedPhotoSearchService.ExecuteSearch();
                var tweetsSearchResults = await _twitterSearchService.ExecuteSearch();

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
                    })?.ToList();

                var temPhotoList = new ObservableCollection<PhotoWithTweets>();
                foreach (var photoWithTweet in result)
                {
                    temPhotoList.Add(photoWithTweet);
                }

                PhotosList = temPhotoList;
                if (PhotosList.Count > 0)
                {
                    PhotoListVisibility = true;
                    SearchLabelVisibility = false;
                }
                else
                {
                    PhotoListVisibility = false;
                    SearchLabelVisibility = true;

                    SearchLabel = "No Photos are found for the Search. Please try again!!";
                }
            }
            catch (HttpRequestException webEx)
            {
                PhotoListVisibility = false;
                SearchLabelVisibility = true;
                SearchLabel = "Error while sending the Search request. Possible reason could be that your PC cannot access Internet. Check and try again.";
            }
            catch (Exception ex)
            {
                PhotoListVisibility = false;
                SearchLabelVisibility = true;
                SearchLabel = ex.Message;
            }
            finally
            {
                Mouse.OverrideCursor = null;
                _canExecuteSearch = true;
            }
        }
    }
}
