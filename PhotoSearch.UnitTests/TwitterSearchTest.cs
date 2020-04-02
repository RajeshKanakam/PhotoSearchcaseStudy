using Microsoft.VisualStudio.TestTools.UnitTesting;
using PhotoSearch.BLL.Services;

namespace PhotoSearch.UnitTests
{
    [TestClass]
    public class TwitterSearchTest
    {
        [TestMethod]
        public void TwitterSearchInstanceTest()
        {
            var twitterSearchService = new TwitterSearchService();
            Assert.IsNotNull(twitterSearchService);
        }

        [TestMethod]
        public void FlickrSearchTest()
        {
            var twitterSearchService = new TwitterSearchService();
            twitterSearchService.SearchString = "nature";
            var result = twitterSearchService.ExecuteSearch();
            Assert.IsNotNull(twitterSearchService);
        }
    }
}
