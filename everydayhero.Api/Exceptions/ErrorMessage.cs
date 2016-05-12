using System.Collections.Generic;
using Newtonsoft.Json;

namespace everydayhero.Api.Exceptions
{
    public class ErrorMessage
    {
        [JsonProperty(PropertyName = "message")]
        public string Message { get; set; }
        [JsonProperty(PropertyName = "errors")]
        public List<InnerError> Errors { get; set; }
    }
}