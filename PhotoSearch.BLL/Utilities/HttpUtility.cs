using System;
using System.Net;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Threading.Tasks;
using Newtonsoft.Json;
using PhotoSearch.BLL.Models.TwitterSearchModels;
using RestSharp;

namespace PhotoSearch.BLL.Utilities
{
    /// <summary>
    /// HttpUtility class provides methods to execute HTTP requests like REST API calls
    /// </summary>
    public class HttpUtility
    {
        /// <summary>
        /// Method to perform HTTP Get request
        /// </summary>
        /// <param name="url"></param>
        /// <returns>JSON data</returns>
        public static async Task<string> GetResultAsync(string url)
        {
            try
            {
                using (HttpClient client = new HttpClient())
                {
                    client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));

                    return await client.GetStringAsync(url);
                }
            }
            catch (HttpRequestException ex)
            {
                throw;
            }
            catch (Exception)
            {
                throw;
            }
        }

        /// <summary>
        /// Method to encode given string to Base64
        /// </summary>
        /// <param name="stringText"></param>
        /// <returns></returns>
        public static string Base64Encode(string stringText)
        {
            var stringTextBytes = System.Text.Encoding.UTF8.GetBytes(stringText);
            return System.Convert.ToBase64String(stringTextBytes);
        }

        /// <summary>
        /// Method to retrieve Bearer Token for Twitter API Authorization
        /// </summary>
        /// <param name="url"></param>
        /// <param name="encodedPair"></param>
        /// <returns>Bearer Token</returns>
        public static async Task<string> GetTwitterBearerToken(string url, string encodedPair)
        {
            try
            {
                ServicePointManager.Expect100Continue = false;
                ServicePointManager.SecurityProtocol = SecurityProtocolType.Tls12;
                var client = new RestClient(url);
                var request = new RestRequest(Method.POST);
                request.AddHeader("Authorization", "Basic " + Base64Encode(encodedPair));
                request.AddHeader("Content-Type", "application/x-www-form-urlencoded");
                request.AddParameter("grant_type", "client_credentials");
                IRestResponse response = client.Execute(request);
                TwitterBearerResponse bearerResponse =
                    JsonConvert.DeserializeObject<TwitterBearerResponse>(response.Content);
                await Task.Yield();
                return bearerResponse.Access_Token;
            }
            catch (WebException)
            {
                throw;
            }
            catch (Exception)
            {
                throw;
            }
        }

        /// <summary>
        /// Method to retrieve Twitter tweet search results
        /// </summary>
        /// <param name="url"></param>
        /// <param name="bearerToken"></param>
        /// <returns>Tweet results</returns>
        public static async Task<string> GetTwitterSearchResponse(string url, string bearerToken)
        {
            try
            {
                ServicePointManager.Expect100Continue = false;
                ServicePointManager.SecurityProtocol = SecurityProtocolType.Tls12;
                var client = new RestClient(url);
                var request = new RestRequest(Method.GET);
                request.AddHeader("Authorization", bearerToken);
                IRestResponse response = client.Execute(request);
                await Task.Yield();
                return response.Content;
            }
            catch (WebException)
            {
                throw;
            }
            catch (Exception)
            {
                throw;
            }
        }
    }
}
