using System;
using System.Net;
using System.Security;
using everydayhero.Api.Authentication;
using RestSharp;

namespace everydayhero.Api.v3
{
    /// <summary>
    ///     Provides integration with EverydayHero REST Api.
    ///     Currently supports:
    ///     - OAuth2 Authentication
    /// </summary>
    /// <remarks>
    /// You can pass in the access token, or a Client Id and Secret using the constructors
    /// </remarks>
    public class Client : ClientBase, IDisposable
    {
        /// <summary>
        /// Access with pre known access token
        /// </summary>
        /// <param name="serviceUrl"></param>
        /// <param name="accessToken"></param>
        public Client(string serviceUrl, string accessToken) 
        {
            ValidateIsNotNull(serviceUrl, accessToken);
            Authenticator = new EverydayHeroAuthenticator(serviceUrl, accessToken);
            Initialise(new Uri(serviceUrl));
        }
        /// <summary>
        /// Access with client id and secret
        /// </summary>
        /// <param name="serviceUrl"></param>
        /// <param name="clientId"></param>
        /// <param name="clientSecret"></param>
        public Client(string serviceUrl, string clientId, string clientSecret) : this(new Uri(serviceUrl), clientId, clientSecret.ToSecureString())
        {
        }
        /// <summary>
        /// Access with client id and secret
        /// </summary>
        /// <param name="serviceUri"></param>
        /// <param name="clientId"></param>
        /// <param name="clientSecret"></param>
        public Client(Uri serviceUri, string clientId, SecureString clientSecret)
        {
            ValidateIsNotNull(serviceUri, clientId, clientSecret);
            Authenticator = new EverydayHeroAuthenticator(serviceUri, clientId, clientSecret);
            Initialise(serviceUri);
        }

        public RestClient RestClient { get; private set; }
        public IEverydayHeroAuthenticator Authenticator { get; private set; }

        public IWebProxy Proxy
        {
            get { return RestClient.Proxy; }
            set { RestClient.Proxy = value; }
        }

        /// <summary>
        /// For interacting with the Charities service.
        /// </summary>
        public CharitiesClient Charity { get; private set; }
        /// <summary>
        /// For interacting with Pages service.  
        /// </summary>
        public PagesClient Pages { get; private set; }

        private void Initialise(Uri serviceUri)
        {
            RestClient = new RestClient(serviceUri)
            {
                CookieContainer = new CookieContainer(),
                Authenticator = Authenticator,
                BaseUrl = serviceUri,
                PreAuthenticate = true
            };
            Charity = new CharitiesClient(RestClient);
            Pages = new PagesClient(RestClient);
        }

      

        public void Dispose()
        {
            if (Authenticator != null)
            {
                Authenticator.Dispose();
                Authenticator = null;
            }
        }
        /// <summary>
        /// Constructor parameter validation
        /// </summary>
        /// <param name="serviceUrl"></param>
        /// <param name="accessToken"></param>
        private static void ValidateIsNotNull(string serviceUrl, string accessToken)
        {
            if (serviceUrl == null)
            {
                throw new ArgumentNullException("serviceUrl");
            }
            if (accessToken == null)
            {
                throw new ArgumentNullException("accessToken");
            }
        }
        /// <summary>
        /// Constructor parameter validation
        /// </summary>
        /// <param name="serviceUri"></param>
        /// <param name="clientId"></param>
        /// <param name="clientSecret"></param>
        private static void ValidateIsNotNull(Uri serviceUri, string clientId, SecureString clientSecret)
        {
            if (clientId == null)
            {
                throw new ArgumentNullException("clientId");
            }
            if (clientSecret == null)
            {
                throw new ArgumentNullException("clientSecret");
            }
            if (serviceUri == null)
            {
                throw new ArgumentNullException("serviceUri");
            }
        }

        /// <summary>
        ///     Should be used in Development Only.  Ignores ssl errors allowing for analysis of request and response.
        /// </summary>
        /// <returns></returns>
        public void IgnoreSslErrors()
        {
            ServicePointManager.ServerCertificateValidationCallback +=
                (sender, certificate, chain, sslPolicyErrors) => true;
        }
    }
}