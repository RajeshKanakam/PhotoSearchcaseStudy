using System.Windows;
using PhotoSearch.BLL.Services;
using PhotoSearch.BLL.ViewModels;

namespace PhotoSearch
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public PhotoSearchViewModel PhotoSearchViewModel { get; set; }

        public MainWindow()
        {
            InitializeComponent();
            AddDataContext();
        }

        private void AddDataContext()
        {
            var flickrFeedPhotoSearchService = new FlickrPublicFeedSearchService();
            var twitterSearchService = new TwitterSearchService();
            PhotoSearchViewModel = new PhotoSearchViewModel(flickrFeedPhotoSearchService, twitterSearchService);

            this.DataContext = PhotoSearchViewModel;
        }
    }
}
