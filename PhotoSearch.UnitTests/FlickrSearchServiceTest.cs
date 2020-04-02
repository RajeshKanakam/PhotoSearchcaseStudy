using Microsoft.VisualStudio.TestTools.UnitTesting;
using PhotoSearch.BLL.Services;

namespace PhotoSearch.UnitTests
{
    [TestClass]
    public class FlickrSearchServiceTest
    {
        [TestMethod]
        public void FlickrSearchInstanceTest()
        {
            var flickrFeedPhotoSearchService = new FlickrPublicFeedSearchService();
            Assert.IsNotNull(flickrFeedPhotoSearchService);
        }

        [TestMethod]
        public void FlickrSearchTest()
        {
            var flickrFeedPhotoSearchService = new FlickrPublicFeedSearchService();
            flickrFeedPhotoSearchService.SearchString = "nature";
            var result = flickrFeedPhotoSearchService.ExecuteSearch();
            Assert.IsNotNull(flickrFeedPhotoSearchService);
        }
    }
}
