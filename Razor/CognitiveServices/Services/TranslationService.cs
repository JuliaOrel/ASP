using CognitiveServices.Models;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;

namespace CognitiveServices.Services
{
    public class TranslationService
    {
        private readonly string _key;
        private readonly string _region;
        private readonly string _endpoint;

        public static readonly Dictionary<Languages, string> languagesCodes;

        public TranslationService(string key, string region, string endpoint)
        {
            _key = key;
            _region = region;
            _endpoint = endpoint;

        }

        static TranslationService()
        {
            languagesCodes = new Dictionary<Languages, string>();
            languagesCodes.Add(Languages.Ukrainian, "uk");
            languagesCodes.Add(Languages.English, "en");
            languagesCodes.Add(Languages.Polish, "pl");
        }

        public async Task<TranslationResult[]> Translate(string text, Languages[] to, 
            Dictionary<string, string> requestParameters=null)
        {
            using(var httpClient=new HttpClient())
            {
                using (var request = new HttpRequestMessage())
                {
                    request.Method = HttpMethod.Post;
                    request.RequestUri = BuildRequestUri(to,requestParameters);
                    request.Headers.Add("Ocp-Apim-Subscription-Key", _key);
                    request.Headers.Add("Ocp-Apim-Subscription-Region", _region);
                    object[] body = new object[]
                    {
                        new {Text=text}
                    };

                    string requestContent = JsonConvert.SerializeObject(body, Formatting.Indented);
                    request.Content = new StringContent(
                        requestContent, System.Text.Encoding.Default, "application/json");

                    HttpResponseMessage response = await httpClient
                        .SendAsync(request)
                        .ConfigureAwait(false);
                    if(response.IsSuccessStatusCode==false)
                    {
                        throw new Exception("Bad request");
                    }
                    string responseString = await response.Content.ReadAsStringAsync();
                    TranslationResult[] translationResults = JsonConvert
                        .DeserializeObject<TranslationResult[]>(responseString);
                    return translationResults;

                }
            }
        }

        private Uri BuildRequestUri(
            Languages[]to, 
            //Languages? from,
            Dictionary<string,string> requestParameters)
        {
            string route = _endpoint + "translate?api-version=3.0";
            //if(from is not null)
            //{
            //    route += $"&from={languagesCodes[from.Value]}";
            //}
            foreach (Languages languages in to)
            {
                route += $"&to={languagesCodes[languages]}";
            }
            if(requestParameters is not null)
            {
                foreach (var parametr in requestParameters)
                {
                    route += $"&{parametr.Key}={parametr.Value}";
                }
            }
            return new Uri(route);
        }
        
    }
}
