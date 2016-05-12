using everydayhero.Api.Authentication;
using Newtonsoft.Json;

namespace everydayhero.Api.Exceptions
{
    /// <summary>
    ///     Standard error result when authenticating with the token service
    /// </summary>
    /// <remarks>
    ///     Used by the <see cref="EverydayHeroAuthenticator" /> IAuthenticator when server returns an error
    /// </remarks>
    public class AuthenticationErrorResult
    {
        [JsonProperty(PropertyName = "error")]
        public string Error { get; set; }

        [JsonProperty(PropertyName = "error_description")]
        public string ErrorDescription { get; set; }
    }
}