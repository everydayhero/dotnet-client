using System;
using System.Net;
using everydayhero.Api.v3;

namespace everydayhero.Api.Tests
{
    public abstract class UnitTestBase
    {
        protected Client GetClient()
        {
            var client = new Client(TestConfig.BaseServiceUrl, TestConfig.ClientId, TestConfig.ClientSecret);
            client.IgnoreSslErrors();
            if (TestConfig.UseProxy)//For use with Fiddler
            {
                client.Proxy = new WebProxy {Address = new Uri("http://localhost:8888", UriKind.Absolute)};
            }
            return client;
        }
    }
}