using everydayhero.Api.Authentication;
using Newtonsoft.Json;

namespace everydayhero.Api.Exceptions
{
    /// <summary>
    ///     Standard error result
    /// </summary>
    /// <remarks>
    ///     Used by the <see cref="EverydayHeroAuthenticator" /> IAuthenticator when server returns an error
    /// </remarks>
    public class ErrorResult
    {
        [JsonProperty(PropertyName = "error")]
        public ErrorMessage Error { get; set; }
    }
}