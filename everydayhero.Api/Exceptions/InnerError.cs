using Newtonsoft.Json;

namespace everydayhero.Api.Exceptions
{
    public class InnerError
    {
        [JsonProperty(PropertyName = "field")]
        public string Field { get; set; }
        [JsonProperty(PropertyName = "code")]
        public string Code { get; set; }
    }
}