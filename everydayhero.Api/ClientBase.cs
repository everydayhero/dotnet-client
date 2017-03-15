using System;
using RestSharp;

namespace everydayhero.Api
{
    public abstract class ClientBase
    {
        private static void SetContentType(Method method, RestRequest request)
        {
            if (method == Method.POST || method == Method.PUT)
            {
                request.AddHeader("Content-Type", "application/json");
            }
        }

        internal IRestRequest CreateRequest(string apiVersion, string path, string queryString, Method method, object body = null)
        {
            var uri = new Uri($"{apiVersion}/{path}{(string.IsNullOrEmpty(queryString) ? "" : "?")}{queryString}", UriKind.Relative);
            var request = new RestRequest(uri, method);
            SetContentType(method, request);
            SetBody(body, request);
            return request;
        }

        private static void SetBody(object body, IRestRequest request)
        {
            if (body != null)
            {
                request.AddJsonBody(body);
            }
        }
    }
}