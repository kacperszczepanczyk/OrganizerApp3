using Newtonsoft.Json;
using OrganizerApp.WebUI.Helpers.Json;
using OrganizerApp.WebUI.Infrastructure;
using RestSharp;
using System;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.Linq;
using System.Threading.Tasks;
using System.Web;

namespace OrganizerApp.WebUI.Helpers.Api
{
    public class ApiRequestHandler : IApiRequestHandler
    {
        private readonly IRestClient _defaultClient;


        public ApiRequestHandler(string baseUri)
        {
            if (String.IsNullOrWhiteSpace(baseUri)) { throw new ArgumentException("Niepoprawna wartość argumentu. Argument \"baseUri\" nie może być null'em, pustym stringiem, lub zawierać samych białych spacji"); }
            _defaultClient = new RestClient(baseUri);
            ConfigureClient(baseUri);
        }


        public async Task<T> ExecuteGetAsync<T>(string resourceUri, NameValueCollection parameters = null) where T : new()
        {
            if (String.IsNullOrWhiteSpace(resourceUri)) { throw new ArgumentException("Niepoprawna wartośc argumentu. Argument \"resourceUri\" nie może być null'em, pustym stringiem, lub zawierać samych białych spacji"); }

            IRestRequest restRequest = new RestRequest(resourceUri, Method.GET);

            NameValueCollection requestParameters = new NameValueCollection();

            if (parameters != null)
            {
                requestParameters.Add(parameters);
            }

            foreach (string key in requestParameters)
            {
                if(key != null && requestParameters[key] != null)
                {
                    restRequest.AddParameter(key, requestParameters[key]);
                }
            }

            var response = await _defaultClient.ExecuteTaskAsync<T>(restRequest);

            if (!response.IsSuccessful)
            {
                throw new ExternalDataCirculationException("Uzyskanie danych od zewnętrznego dostawcy zakończyło się niepowodzeniem. Sprawdź wewnętrzny wyjątek, aby dowiedzieć się więcej.", response.ErrorException);
            }

            return response.Data;
        }

        public async Task ExecutePostAsync<T>(string resourceUri, T objectToSerialize)
        {
            if (String.IsNullOrWhiteSpace(resourceUri)) { throw new ArgumentException("Argument \"resourceUri\" nie może być null'em, pustym stringiem, lub zawierać samych białych spacji"); }
            if (objectToSerialize == null) { throw new ArgumentNullException("Argument \"objectToSerialize\" nie może być null'em"); }

            IRestRequest restRequest = new RestRequest(resourceUri, Method.POST)
            {
                RequestFormat = DataFormat.Json
            };

            var projectAsJson = JsonConvert.SerializeObject(objectToSerialize);
            restRequest.AddParameter("application/json", projectAsJson , ParameterType.RequestBody);

            var response = await _defaultClient.ExecuteTaskAsync<T>(restRequest);

            if (!response.IsSuccessful)
            {
                throw new ExternalDataCirculationException("Wysłanie danych do zewnętrznego dostawcy zakończyło się niepowodzeniem. Sprawdź wewnętrzny wyjątek, aby dowiedzieć się więcej.", response.ErrorException);
            }
        }

        private void ConfigureClient(string baseUri)
        {
            _defaultClient.AddHandler("application/json", NewtonsoftJsonSerializer.Default);
            _defaultClient.AddHandler("text/json", NewtonsoftJsonSerializer.Default);
            _defaultClient.AddHandler("text/x-json", NewtonsoftJsonSerializer.Default);
            _defaultClient.AddHandler("text/javascript", NewtonsoftJsonSerializer.Default);
            _defaultClient.AddHandler("*+json", NewtonsoftJsonSerializer.Default);
        }
    }
}