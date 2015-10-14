using System;
using System.IO;
using System.Net;
using Newtonsoft.Json;
using CRT.DataStructure.Json;
using CRT.Helper;
using Newtonsoft.Json.Linq;

namespace CRT
{
    class RemoteControl
    {
        private string baseUrl;

        public RemoteControl(string ip, int port)
        {
            this.baseUrl = String.Format("http://{0}:{1}/api/", ip, port);
            Console.WriteLine(String.Format("Connecting to {0}:{1}", ip, port));

            RegisterRequest request = new RegisterRequest();
            request.ComputerId = Identification.GetMotherBoardID(); ;
            JObject baseInfo = apiCall("register", request);
            Console.WriteLine("State: " + baseInfo["state"]);
        }

        private JObject apiCall(string endpoint, object requestPayload)
        {
            string url = this.baseUrl + endpoint;
            var httpWebRequest = (HttpWebRequest)WebRequest.Create(url);
            httpWebRequest.ContentType = "text/json";
            httpWebRequest.Method = "POST";

            var serializer = new JsonSerializer();
            using (var streamWriter = new StreamWriter(httpWebRequest.GetRequestStream()))
            {
                using (var tw = new JsonTextWriter(streamWriter))
                {
                    serializer.Serialize(tw, requestPayload);
                }
            }

            var httpResponse = (HttpWebResponse)httpWebRequest.GetResponse();
            JObject response;
            using (var streamReader = new StreamReader(httpResponse.GetResponseStream()))
            {
                var responseText = streamReader.ReadToEnd();
                response = JObject.Parse(responseText);
                //Now you have your response.
                //or false depending on information in the response
            }
            return response;
        }
    }
}
