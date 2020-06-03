using System;
using System.Configuration;
using System.Threading.Tasks;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Newtonsoft.Json;
using PhotoSearch.BLL.Models.FlickrSearchModels;
using PhotoSearch.BLL.Models.TwitterSearchModels;
using PhotoSearch.BLL.Utilities;

namespace PhotoSearch.UnitTests
{
    [TestClass]
    public class HttpUtilityTests
    {
        private readonly string _flickrSearchUrl = ConfigurationManager.AppSettings["flickrSearchUrl"];
        private readonly string _consumerKey = ConfigurationManager.AppSettings["twitterConsumerKey"];
        private readonly string _consumerSecret = ConfigurationManager.AppSettings["twitterConsumerSecret"];
        private readonly string _searchUrl = ConfigurationManager.AppSettings["twitterSearchUrl"];
        private readonly string _authorizationUrl = ConfigurationManager.AppSettings["twitterAuthorizationUrl"];

        [TestMethod]
        public async Task FlickrSearchTest()
        {
            var result = await HttpUtility.GetResultAsync(string.Format(_flickrSearchUrl, "nature"));
            result = result.Replace("jsonFlickrFeed(", "");
            result = result.Substring(0, result.Length - 1);
            var photoMetaData = JsonConvert.DeserializeObject<PhotosMetaData>(result);
            Assert.IsNotNull(photoMetaData);
        }

        [ExpectedException(typeof(InvalidOperationException))]
        [TestMethod]
        public async Task FlickrSearchInvalidTest()
        {
            var result = await HttpUtility.GetResultAsync("www.invalidwebsite.com");
            result = result.Replace("jsonFlickrFeed(", "");
            result = result.Substring(0, result.Length - 1);
            var photoMetaData = JsonConvert.DeserializeObject<PhotosMetaData>(result);
            Assert.IsNull(photoMetaData);
        }

        [TestMethod]
        public void GetBase64EncodedValueTest()
        {
            var result = EncryptionUtility.Base64Encode("photo search");
            Assert.AreEqual(result, "cGhvdG8gc2VhcmNo");
        }

        [TestMethod]
        public async Task GetBearerTokenTest()
        {
            string formattedToken = EncryptionUtility.DecodeBase64(_consumerKey) + ":" + EncryptionUtility.DecodeBase64(_consumerSecret);
            string formattedUrl = string.Format(_searchUrl, "nature", 20);
            string authorizationToken = "Bearer " + await HttpUtility.GetTwitterBearerToken(_authorizationUrl, formattedToken);
            Assert.IsTrue(!string.IsNullOrWhiteSpace(authorizationToken));
        }

        [TestMethod]
        public async Task GetTwitterResponseTest()
        {
            string formattedToken = EncryptionUtility.DecodeBase64(_consumerKey) + ":" + EncryptionUtility.DecodeBase64(_consumerSecret);
            string formattedUrl = string.Format(_searchUrl, "nature", 20);
            string authorizationToken = "Bearer " + await HttpUtility.GetTwitterBearerToken(_authorizationUrl, formattedToken);
            string response = await HttpUtility.GetTwitterSearchResponse(formattedUrl, authorizationToken);
            var twitterResponseResult = JsonConvert.DeserializeObject<TwitterSearchMetaData>(response);
            Assert.IsNotNull(twitterResponseResult);
        }
    }
}
