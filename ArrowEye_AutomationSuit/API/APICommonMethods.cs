using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Principal;
using System.Text;
using System.Threading.Tasks;
using ArrowEye_Automation_Framework.Common;
using RestSharp;
using RestSharp.Authenticators;
namespace ArrowEye_Automation_Framework.API
{
    public class APICommonMethods
    {
        //Get
        static public IRestResponse ResponseFromGETrequest(string baseURL, string resourceAndQuery)
        {
            var request = GETRequest(resourceAndQuery);
            IRestResponse response = GetRestClient(baseURL).Execute(request);
            return response;
        }
        static public RestRequest GETRequest(string resourceAndQuery)
        {
            var request = new RestRequest(Method.GET);
            request.Resource = resourceAndQuery;
            request.AddHeader("Authorization", "Bearer " + AppNameHelper.apiToken);
            request.AddHeader("content-type", "application/json");
            return request;
        }
        static public RestClient GetRestClient(string baseURL)
        {
            var client = new RestClient();
            client.BaseUrl = new Uri(baseURL);
            client.Authenticator = new NtlmAuthenticator();
            return client;
        }

        //Post      
        static public IRestResponse ResponseFromPostRequest(string baseURL, string resourceAndQuery, string json)
        {
            var request = PostRequest(resourceAndQuery, json);
            IRestResponse response = GetRestClient(baseURL).Execute(request);
            return response;
        }
        static public RestRequest PostRequest(string resourceAndQuery, string json)
        {
            var request = new RestRequest(Method.POST);
            request.Resource = resourceAndQuery;
            request.AddHeader("Authorization", "Bearer " + AppNameHelper.apiToken);
            request.AddHeader("content-type", "application/json");
            request.AddParameter("application/json", json, ParameterType.RequestBody);
            return request;
        }
        static public IRestResponse ResponseFromPutRequest(string baseURL, string resourceAndQuery, string jsonString)
        {
            var request = PutRequest(resourceAndQuery, jsonString);
            //Execute Request
            IRestResponse response = GetRestClient(baseURL).Execute(request);
            return response;
        }
        static public RestRequest PutRequest(string resource, string jsonString)
        {
            var request = new RestRequest(Method.PUT);
            request.Resource = resource;
            request.AddHeader("content-type", "application/json");
            request.AddHeader("Authorization", "Bearer " + AppNameHelper.apiToken);
            request.AddParameter("application/json", jsonString, ParameterType.RequestBody);
            return request;
        }
        static public IRestResponse ResponseFromDeleteRequest(string baseURL, string resource, string jsonString)
        {

            var request = DeleteRequest(resource, jsonString);
            IRestResponse response = GetRestClient(baseURL).Execute(request);
            return response;
        }
        static public RestRequest DeleteRequest(string resource, string jsonString)
        {
            var request = new RestRequest(Method.DELETE);
            request.Resource = resource;
            request.AddHeader("Authorization", "Bearer " + AppNameHelper.apiToken);
            request.AddHeader("content-type", "application/json");
            request.AddParameter("application/json", jsonString, ParameterType.RequestBody);
            return request;
        }
    }
}
