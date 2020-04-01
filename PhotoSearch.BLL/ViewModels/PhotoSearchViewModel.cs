using System;
using PhotoSearch.BLL.Commands;
using System.Collections.ObjectModel;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Input;
using PhotoSearch.BLL.Interfaces;
using PhotoSearch.BLL.Models;
using PhotoSearch.BLL.Models.FlickrSearchModels;
using PhotoSearch.BLL.Models.TwitterSearchModels;
using PhotoSearch.BLL.Services;

namespace PhotoSearch.BLL.ViewModels
{
    public class PhotoSearchViewModel : ViewModelBase
    {
        public ObservableCollection<PhotoWithTweets> PhotosList { get; set; }
        public ObservableCollection<Status> TweetsList { get; set; }

        private readonly ISearchService<Photo> _flickrFeedPhotoSearchService;
        private readonly ISearchService<Status> _twitterSearchService;

        private string _searchString;
        public string SearchString
        {
            get => _searchString;
            set
            {
                _searchString = value;
                this._flickrFeedPhotoSearchService.SearchString = value;
                this._twitterSearchService.SearchString = value;
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

        private bool _progressBarVisibility;
        public bool ProgressBarVisibility
        {
            get => _progressBarVisibility;
            set
            {
                _progressBarVisibility = value;
                NotifyPropertyChanged();
            }
        }

        private double _progressBarValue;
        public double ProgressBarValue
        {
            get => _progressBarValue;
            set
            {
                _progressBarValue = value;
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

        public PhotoSearchViewModel(ISearchService<Photo> flickrFeedPhotoSearchService)
        {
            this._flickrFeedPhotoSearchService = flickrFeedPhotoSearchService;
            TweetsList = new ObservableCollection<Status>();
            PhotosList = new ObservableCollection<PhotoWithTweets>();
            this._twitterSearchService = new TwitterSearchService();
            SearchLabel = "Search results are Empty. Perform Search to view Photos here.";
            SearchLabelVisibility = true;
            PhotoListVisibility = false;
            ProgressBarVisibility = false;
        }

        private bool CanSearch()
        {
            // Verify command can be executed here
            return true;

        }

        private async Task StartSearch()
        {
            // Save command execution logic
            SearchLabelVisibility = false;
            PhotoListVisibility = false;
            ProgressBarVisibility = true;
            try
            {
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

                foreach (var photoWithTweet in result)
                {
                    PhotosList.Add(photoWithTweet);
                    ProgressBarValue = (PhotosList.IndexOf(photoWithTweet) + 1) * 100 / result.ToList().Count;
                }

                ProgressBarVisibility = false;
                if (PhotosList.Count > 0)
                {
                    PhotoListVisibility = true;
                    SearchLabelVisibility = false;
                }
                else
                {
                    PhotoListVisibility = false;
                    SearchLabelVisibility = true;

                    SearchLabel = "No Photos available for the Search!!";
                }
            }
            catch (Exception ex)
            {

            }
        }
    }
}
