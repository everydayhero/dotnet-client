using Newtonsoft.Json;
using RestSharp;

namespace everydayhero.Api.Exceptions
{
    public static class ExceptionHandler
    {
        public static RequestFailedException GetFailedRequestException(IRestResponse response)
        {
            ErrorResult errorResult = null;
            try
            {
                errorResult = JsonConvert.DeserializeObject<ErrorResult>(response.Content);
            }
            catch
            {
                //Intentionally ignored
            }
            string message = string.Empty;
            if (errorResult != null && errorResult.Error != null)
            {
                message = errorResult.Error.Message;
            }
            if (string.IsNullOrEmpty(message))
            {
                message = response.StatusDescription;
            }
            return new RequestFailedException(response.StatusCode, response.StatusDescription, errorResult, response.Content, message);
        }
    }
}