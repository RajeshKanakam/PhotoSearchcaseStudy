using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using PhotoSearch.BLL.Commands;
using PhotoSearch.BLL.Services;
using PhotoSearch.BLL.ViewModels;

namespace PhotoSearch.UnitTests
{
    [TestClass]
    public class PhotoSearchViewModelTests
    {
        [TestMethod]
        public void PhotoSearchVmInstaceTest()
        {
            var flickrFeedPhotoSearchService = new FlickrPublicFeedSearchService();
            var twitterSearchService = new TwitterSearchService();
            var instance = new PhotoSearchViewModel(flickrFeedPhotoSearchService, twitterSearchService);
            Assert.IsNotNull(instance);
        }

        [TestMethod]
        public void PhotoSearchVmEmptySearchTest()
        {
            var flickrFeedPhotoSearchService = new FlickrPublicFeedSearchService();
            var twitterSearchService = new TwitterSearchService();
            var instance = new PhotoSearchViewModel(flickrFeedPhotoSearchService, twitterSearchService);
            instance.SearchString = string.Empty;
            instance.SearchCommand.Execute(null);
            Assert.IsNotNull(instance);
            Assert.AreSame(instance.SearchLabel, "Search String cannot be Empty. Please try again!!");
        }

        [TestMethod]
        public void PhotoSearchVmInvalidSearchTest()
        {
            var flickrFeedPhotoSearchService = new FlickrPublicFeedSearchService();
            var twitterSearchService = new TwitterSearchService();
            var instance = new PhotoSearchViewModel(flickrFeedPhotoSearchService, twitterSearchService);
            instance.SearchString = "skhfakjshfk"; //some random string that cannot be found in our regular use.
            instance.SearchCommand.Execute(null);
            Assert.IsNotNull(instance);
            Assert.AreSame(instance.SearchLabel, "Search results are Empty. Perform Search to view Photos here.");
        }

        [TestMethod]
        public void PhotoSearchVmValidSearchTest()
        {
            Exception exception = null;
            try
            {
                var flickrFeedPhotoSearchService = new FlickrPublicFeedSearchService();
                var twitterSearchService = new TwitterSearchService();
                var instance = new PhotoSearchViewModel(flickrFeedPhotoSearchService, twitterSearchService);
                instance.SearchString = "nature"; //some random string that cannot be found in our regular use.
                instance.SearchCommand.Execute(null);
                Assert.IsNotNull(instance);
            }
            catch (Exception ex)
            {
                exception = ex;
            }

            Assert.IsNull(exception);
        }
    }
}
