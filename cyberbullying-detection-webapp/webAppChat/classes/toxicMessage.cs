using AjaxControlToolkit;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Runtime.Remoting.Messaging;
using System.Web;
using System.Threading.Tasks;

namespace webAppChat.classes
{
    public class toxicMessage
    {
        private const string strURL = "https://54.151.104.207/api/predict";

        private string userFrom;
        private string userTo = "all";
        private string message;
        private decimal resAllMsgs;
        private decimal resThisMsg;

        public decimal resultAllMsgs 
        {
            get => resAllMsgs;
        }

        public decimal resultThisMsg
        {
            get => resThisMsg;
        }

        public toxicMessage(string pUserFrom)
        {
            userFrom = pUserFrom;
        }

        public toxicMessage(string pUserFrom, string pMessage) 
        {
           userFrom = pUserFrom;
           message = pMessage;
        }

        /// <summary>
        /// Call to API Rest service where the NLP model is waiting for requests
        /// </summary>
        public bool validate()
        {
            bool result = false;

            string jsonRequest = $"{{\"userFrom\":\"{userFrom}\"," +
                                 $"\"userTo\":\"{userTo}\"," +
                                 $"\"message\":\"{message}\"}}";

            StringContent content = new StringContent(jsonRequest, null, "application/json");

            HttpClientHandler handler = new HttpClientHandler();
            handler.ServerCertificateCustomValidationCallback = (HttpRequestMessage, cert, chain, sslPolicyErrors) => true;

            HttpClient httpClient = new HttpClient(handler);

            HttpRequestMessage httpRequest = new HttpRequestMessage(HttpMethod.Post, strURL);
            httpRequest.Content = content;

            Task<HttpResponseMessage> httpResponse = httpClient.SendAsync (httpRequest);
            httpResponse.Wait();
            HttpResponseMessage httpResponseResult = httpResponse.Result;

            if (httpResponseResult.IsSuccessStatusCode)
            {
                Task<string> jsonString = httpResponseResult.Content.ReadAsStringAsync();
                jsonString.Wait();
                string jsonResult = jsonString.Result;

                clsResult toxLevel = JsonConvert.DeserializeObject<clsResult>(jsonResult);

                resAllMsgs = toxLevel.resultAllMsgs * 100;
                resThisMsg = toxLevel.resultThisMsg * 100;
                result = true;
            }

            return result;
        }

        public bool reset(bool pAll)
        {
            bool result = false;

            message = pAll ? "reset all" : "reset";

            string jsonRequest = $"{{\"userFrom\":\"{userFrom}\"," +
                                 $"\"userTo\":\"{userTo}\"," +
                                 $"\"message\":\"{message}\"}}";

            StringContent content = new StringContent(jsonRequest, null, "application/json");

            HttpClientHandler handler = new HttpClientHandler();
            handler.ServerCertificateCustomValidationCallback = (HttpRequestMessage, cert, chain, sslPolicyErrors) => true;

            HttpClient httpClient = new HttpClient(handler);

            HttpRequestMessage httpRequest = new HttpRequestMessage(HttpMethod.Post, strURL);
            httpRequest.Content = content;

            Task<HttpResponseMessage> httpResponse = httpClient.SendAsync(httpRequest);
            httpResponse.Wait();
            HttpResponseMessage httpResponseResult = httpResponse.Result;

            if (httpResponseResult.IsSuccessStatusCode)
            {
                result = true;
            }

            return result;
        }

    }
}