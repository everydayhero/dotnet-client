using System;
using System.Security;
using RestSharp.Authenticators;

namespace everydayhero.Api.Authentication
{
    public interface IEverydayHeroAuthenticator : IAuthenticator, IDisposable
    {
        SecureString AccessToken { get; set; }
    }
}