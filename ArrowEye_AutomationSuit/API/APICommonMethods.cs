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
        //public static string APItoken= "eyJraWQiOiJaTGJkVTczTk9YZEZCU29CZE13eWtyOTZaT2xSSWFHRWJlZGN4XC8yb2p0Zz0iLCJhbGciOiJSUzI1NiJ9.eyJzdWIiOiI4MTA3YWRhOS04YTRjLTQ1NTYtOTM3Yy1lMjI2ZGRiMDhkZjMiLCJlbWFpbF92ZXJpZmllZCI6dHJ1ZSwiaXNzIjoiaHR0cHM6XC9cL2NvZ25pdG8taWRwLnVzLXdlc3QtMi5hbWF6b25hd3MuY29tXC91cy13ZXN0LTJfM3p4Zk1GempvIiwiY29nbml0bzp1c2VybmFtZSI6InNiYWJ1IiwiYXVkIjoiMWZmNDFnaDVhaWp2OTA0amJubzMxNzU5MzEiLCJldmVudF9pZCI6IjgwMWY5NGVhLTFjMWItNDRlZS05YWZmLTMzMGI4YzAwYTdlYiIsInRva2VuX3VzZSI6ImlkIiwiYXV0aF90aW1lIjoxNzM3NzA1MDI3LCJuYW1lIjoiIFN1ZGFyc2hhbiBCYWJ1IFNlZ2dhcmkiLCJleHAiOjE3Mzc3OTE0MjcsImlhdCI6MTczNzcwNTAyNywiZW1haWwiOiJzLnN1ZGFyc2hhbkB6ZW5zYXIuY29tIn0.UGAKY-dj9OEd3-O4MRlvhVo5B9dH8Za97xiDUYy23Hx8WPn5qaBf1wK0Oi3lmHc1_w8vMSgSJbkqEyVYw_IJbSs2ZF8q12dXzY7aFwv5Lv98S2hm33swkbNMzEVLXTlSbhY4LlVOpLi6bpCSUHnUhs1EJwAPoO7V7b9qmzsc_h6mDY18Z0oRNUZBK8O5nHqNmwgMracxI7far5calqwzc7JOdZefVtCrPXNgbvp_udsk4xmtRkOddncw0ITUI1n4pOuSxeFWCwcf5PzfsmXtfX-7-0Xkx1PjcGF4c_WtMUbhslH92MR1STLW5QzMIanYoxWAEO82t4WGjW1DYJOT7A";        
        //Get
        static public IRestResponse ResponseFromGETrequest(string baseURL,string resourceAndQuery)
        {
            var request = GETRequest(resourceAndQuery);            
            //Execute Request
            IRestResponse response = GetRestClient(baseURL).Execute(request);
            return response;
        }
        static public RestRequest GETRequest(string resourceAndQuery)
        {
            var request = new RestRequest(Method.GET);
            request.Resource = resourceAndQuery;
            //APItoken = APIConstants.APItoken;
            request.AddHeader("Authorization", "Bearer "+ AppNameHelper.apiToken);
            request.AddHeader("content-type", "application/json");
            return request;
        }
        static public RestClient GetRestClient(string baseURL)
        {
            var client = new RestClient();
            client.BaseUrl = new Uri(baseURL);
            //client.Authenticator = new NtlmAuthenticator(userName, passWord);
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
            request.AddHeader("Authorization","Bearer "+AppNameHelper.apiToken);
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

        static public IRestResponse ResponseFromDeleteRequest(string baseURL,string resource, string jsonString)
        {

            var request = DeleteRequest(resource,jsonString);
            //Execute Request
            IRestResponse response = GetRestClient(baseURL).Execute(request);
            return response;
        }

        static public RestRequest DeleteRequest(string resource,string jsonString)
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
