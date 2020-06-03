using PhotoSearch.BLL.Interfaces;
using PhotoSearch.BLL.Models.FlickrSearchModels;
using PhotoSearch.BLL.Models.TwitterSearchModels;
using PhotoSearch.BLL.Services;
using PhotoSearch.BLL.ViewModels;
using System.Windows;
using Unity;

namespace PhotoSearch
{
    /// <summary>
    /// Interaction logic for App.xaml
    /// </summary>
    public partial class App : Application
    {
        public static IUnityContainer PhotoSearchRegistry { get; set; }
        protected override void OnStartup(StartupEventArgs e)
        {
            base.OnStartup(e);

            InitializeContainer();
        }

        private void InitializeContainer()
        {
            var PhotoSearchRegistry = new UnityContainer();

            PhotoSearchRegistry.RegisterType<ISearchService<Photo>, FlickrPublicFeedSearchService>();
            PhotoSearchRegistry.RegisterType<ISearchService<Status>, TwitterSearchService>();

            var mainWindow = new MainWindow();
            mainWindow.DataContext = PhotoSearchRegistry.Resolve<PhotoSearchViewModel>();
            mainWindow.Show();
        }
    }
}
