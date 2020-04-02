using System.Threading.Tasks;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Newtonsoft.Json;
using PhotoSearch.BLL.Models.FlickrSearchModels;
using PhotoSearch.BLL.Utilities;

namespace PhotoSearch.UnitTests
{
    [TestClass]
    public class HttpUtilityTests
    {
        private string _flickrSearchUrl =
            "https://www.flickr.com/services/feeds/photos_public.gne?tags=nature&format=json";

        [TestMethod]
        public async Task FlickrSearchTest()
        {
            var result = await HttpUtility.GetResultAsync(_flickrSearchUrl);
            result = result.Replace("jsonFlickrFeed(", "");
            result = result.Substring(0, result.Length - 1);
            var photoMetaData = JsonConvert.DeserializeObject<PhotosMetaData>(result);
            Assert.IsNotNull(photoMetaData);
        }
    }
}
