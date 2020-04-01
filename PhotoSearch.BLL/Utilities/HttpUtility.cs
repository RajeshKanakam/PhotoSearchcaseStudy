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
    public class HttpUtility
    {

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

        public static string Base64Encode(string stringText)
        {
            var stringTextBytes = System.Text.Encoding.UTF8.GetBytes(stringText);
            return System.Convert.ToBase64String(stringTextBytes);
        }

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
