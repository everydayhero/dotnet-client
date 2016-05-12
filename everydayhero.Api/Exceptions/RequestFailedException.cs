using System;
using System.Net;

namespace everydayhero.Api.Exceptions
{
    public class RequestFailedException : Exception
    {
        public RequestFailedException(HttpStatusCode statusCode, string description, ErrorResult result, string content, string message) : base(message)
        {
            StatusCode = statusCode;
            Description = description;
            Result = result;
            Content = content;
        }
        public HttpStatusCode StatusCode { get; set; }
        public string Description { get; set; }
        public ErrorResult Result { get; set; }
        public string Content { get; set; }
    }
}
