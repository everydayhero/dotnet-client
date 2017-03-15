using System;
using System.IO;
using System.Net;
using System.Security;
using everydayhero.Api.Constants;
using everydayhero.Api.Exceptions;
using Newtonsoft.Json;
using RestSharp;

namespace everydayhero.Api.Authentication
{
    /// <summary>
    /// Pre-Authenticates with the EverydayHero token service and adds the token to the header of each request.
    /// </summary>
    public class EverydayHeroAuthenticator : IEverydayHeroAuthenticator
    {
        private readonly CookieContainer _cookieContainer = new CookieContainer();
        private readonly Uri _serviceUri;
        public string ClientId { get; set; }
        public SecureString ClientSecret { get; set; }
        public SecureString AccessToken { get; set; }

        public EverydayHeroAuthenticator(string serviceUrl, string accessToken)
        {
            _serviceUri = new Uri(serviceUrl);
            AccessToken = accessToken.ToSecureString();
        }

        public EverydayHeroAuthenticator(string serviceUrl, string clientId, string clientSecret) : this(new Uri(serviceUrl), clientId, clientSecret.ToSecureString())
        {
        }

        public EverydayHeroAuthenticator(Uri serviceUri, string clientId, SecureString clientSecret)
        {
            _serviceUri = serviceUri;
            if (clientSecret == null)
            {
                throw new ArgumentNullException(nameof(clientSecret));
            }
            if (serviceUri == null)
            {
                throw new ArgumentNullException(nameof(serviceUri));
            }

            ClientId = clientId;
            ClientSecret = clientSecret;
        }

      

        public void Authenticate(IRestClient client, IRestRequest request)
        {
            if (AccessToken == null)
            {
                PreAuthenticate(client.Proxy);
            }
            request.AddHeader("Authorization", $"Bearer {AccessToken.ToInsecureString()}");
        }

        public void Dispose()
        {
            if (AccessToken != null)
            {
                AccessToken.Dispose();
                AccessToken = null;
            }
        }

        public string GetAccessToken()
        {
            if (AccessToken != null)
            {
                return AccessToken.ToInsecureString();
            }
            throw new InvalidOperationException("Access token has not yet been received.  Issue a request.");
        }

        private void PreAuthenticate(IWebProxy proxy)
        {
            var authRequest = CreateAuthenticationRequest(proxy);
            try
            {
                using (var response = (HttpWebResponse) authRequest.GetResponse())
                {
                    var authResult = GetAuthResponse(response);
                    if (string.IsNullOrEmpty(authResult.AccessToken))
                    {
                        throw new InvalidOperationException("The authentication token received by the server is null or empty");
                    }
                    AccessToken = authResult.AccessToken.ToSecureString();
                }
            }
            catch (WebException ex)
            {
                var response = (HttpWebResponse) ex.Response;
                if (response != null && response.StatusCode != HttpStatusCode.InternalServerError)
                {
                    using (var data = response.GetResponseStream())
                    {
                        var errorResult = GetObjectFromResponse<AuthenticationErrorResult>(data);
                        if (errorResult == null)
                        {
                            throw;
                        }
                        throw new AuthenticationFailedException(errorResult);
                    }
                }
                throw;
            }
        }

        private static AuthenticationResult GetAuthResponse(HttpWebResponse response)
        {
            using (var data = response.GetResponseStream())
            {
                if (data == null)
                {
                    throw new InvalidOperationException("The response stream is null when attempting to authenticate");
                }
                return GetObjectFromResponse<AuthenticationResult>(data);
            }
        }

        private static T GetObjectFromResponse<T>(Stream data)
        {
            using (var reader = new StreamReader(data))
            {
                var serialiser = new JsonSerializer();
                using (var jsonTextReader = new JsonTextReader(reader))
                {
                    return serialiser.Deserialize<T>(jsonTextReader);
                }
            }
        }

        /// <summary>
        /// </summary>
        /// <param name="proxy">If using a proxy, otherwise pass in null</param>
        /// <returns></returns>
        private HttpWebRequest CreateAuthenticationRequest(IWebProxy proxy)
        {
            ServicePointManager.SecurityProtocol = SecurityProtocolType.Tls;
            ServicePointManager.ServerCertificateValidationCallback += (sender, certificate, chain, sslPolicyErrors) => true;
            var requestUri = new Uri(_serviceUri,
                $"{EndPointConstants.OAuthEndPoint}?&{EndPointConstants.OAuthUserNameKey}={ClientId}&{EndPointConstants.OAuthSecretKey}={ClientSecret.ToInsecureString()}&{EndPointConstants.GrantTypeKey}={GrantTypeConstants.ClientCredentials}");
            var request = (HttpWebRequest) WebRequest.Create(requestUri);
            request.ProtocolVersion = HttpVersion.Version10;
            request.ServicePoint.ConnectionLeaseTimeout = 5000;
            request.ServicePoint.MaxIdleTime = 5000;
            request.ServicePoint.ConnectionLimit = 1;
            request.KeepAlive = false;
            if (proxy != null)
            {
                request.Proxy = proxy;
            }
            request.Method = "POST";
            request.Accept = "application/json;";
            request.AutomaticDecompression = DecompressionMethods.None;
            request.CookieContainer = _cookieContainer;
            return request;
        }
    }
}