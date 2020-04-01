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
            HttpClient client = new HttpClient();
            client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));

            return await client.GetStringAsync(url);
        }

        public static string Base64Encode(string stringText)
        {
            var stringTextBytes = System.Text.Encoding.UTF8.GetBytes(stringText);
            return System.Convert.ToBase64String(stringTextBytes);
        }

        public static string GetTwitterBearerToken(string url, string encodedPair)
        {
            ServicePointManager.Expect100Continue = false;
            ServicePointManager.SecurityProtocol = SecurityProtocolType.Tls12;
            var client = new RestClient(url);
            var request = new RestRequest(Method.POST);
            request.AddHeader("Authorization", "Basic " + Base64Encode(encodedPair));
            request.AddHeader("Content-Type", "application/x-www-form-urlencoded");
            request.AddParameter("grant_type", "client_credentials");
            IRestResponse response = client.Execute(request);
            TwitterBearerResponse bearerResponse = JsonConvert.DeserializeObject<TwitterBearerResponse>(response.Content);
             return bearerResponse.Access_Token;
        }

        public static string GetTwitterSearchResponse(string url, string bearerToken)
        {
            ServicePointManager.Expect100Continue = false;
            ServicePointManager.SecurityProtocol = SecurityProtocolType.Tls12;
            var client = new RestClient(url);
            var request = new RestRequest(Method.GET);
            request.AddHeader("Authorization", bearerToken);
            IRestResponse response = client.Execute(request);
            return response.Content;
        }
    }
}
