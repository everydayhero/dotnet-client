using Newtonsoft.Json;

namespace everydayhero.Api.Authentication
{
    /// <summary>
    ///     Contains a successful authentication request.
    /// </summary>
    /// <remarks>
    ///     Used by the <see cref="EverydayHeroAuthenticator" /> IAuthenticator
    /// </remarks>
    public class AuthenticationResult
    {
        [JsonProperty(PropertyName = "access_token")]
        public string AccessToken { get; set; }

        [JsonProperty(PropertyName = "token_type")]
        public string TokenType { get; set; }

        [JsonProperty(PropertyName = "created_at")]
        public string CreatedAd { get; set; }
    }
}