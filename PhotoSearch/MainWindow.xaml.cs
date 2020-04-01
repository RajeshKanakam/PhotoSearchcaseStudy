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
            var flickrFeedPhotoSearchService = new FlickrPublicFeedSearchService();
            PhotoSearchViewModel = new PhotoSearchViewModel(flickrFeedPhotoSearchService);

            this.DataContext = PhotoSearchViewModel;
        }
    }
}
