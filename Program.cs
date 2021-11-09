using System;
using System.Text;
using System.Net;
using System.IO;
using System.Collections.Generic;
using Newtonsoft.Json;

namespace BingSearch
{
    class Program
    {

        static string subscriptionKey = "XXXXXXXXX";
        static string endpoint = "XXXXXXX" + "/v7.0/search";

        const string query = "Stop Covid-19";


        static void Main(string[] args)
        {

            // Create a dictionary to store relevant headers
            Dictionary<string, string> relevantHeaders = new Dictionary<string, string>();
           
            Console.OutputEncoding = Encoding.UTF8;

            Console.WriteLine("Searching to Web for: " + query);

            // Construct the URI of the search request

            var uriQuery = endpoint + "?q=" + Uri.EscapeDataString(query);

            // Perform the Web request and get the response
            WebRequest request = HttpWebRequest.Create(uriQuery);
            request.Headers["Ocp-Apim-Subscription-Key"] = subscriptionKey;
            HttpWebResponse response = (HttpWebResponse)request.GetResponseAsync().Result;
            string json = new StreamReader(response.GetResponseStream()).ReadToEnd();

            // Extract Bing Http headers
            foreach(String header in response.Headers)
            {
                if (header.StartsWith("BingAPIs-") || header.StartsWith("X-MSEdge-"))
                    relevantHeaders[header] = response.Headers[header];
            }

            // Show headers
            Console.WriteLine("Relevant Http Headers: ");
            foreach(var header in relevantHeaders)
            {
                Console.WriteLine(header.Key + ": " + header.Value);

                Console.WriteLine("JSON Response: ");
                dynamic parsedJson = JsonConvert.DeserializeObject(json);
                Console.WriteLine(JsonConvert.SerializeObject(parsedJson, Formatting.Indented));
            }


        }
    }
}
