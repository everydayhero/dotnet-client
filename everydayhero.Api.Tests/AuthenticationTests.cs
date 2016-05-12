using System;
using System.Net;
using everydayhero.Api.Exceptions;
using everydayhero.Api.v3;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using RestSharp;

namespace everydayhero.Api.Tests
{
    [TestClass]
    public class AuthenticationTests : UnitTestBase
    {
       
        [TestMethod]
        public void Authenticate()
        {
            var client = GetClient();
            ExecuteSampleQuery(client);
        }
        [TestMethod]
        public void GetAccessToken()
        {
            var client = GetClient();
            ExecuteSampleQuery(client);
            Assert.IsNotNull(client.Authenticator.AccessToken);
            Console.WriteLine("AccessToken: " + client.Authenticator.AccessToken.ToInsecureString());
        }
        [TestMethod]
        public void GetAccessToken_Fail()
        {
            var client = GetClient();
            Assert.IsNull(client.Authenticator.AccessToken);//No request issued to server yet.  So will be null
        }

        private static void ExecuteSampleQuery(Client client)
        {
            var request = new RestRequest(string.Format("/api/v2/charities?campaign_ids={0}", TestConfig.TestData_Campaign_Uid), Method.GET);
            IRestResponse result = client.RestClient.Execute(request);
            Assert.IsTrue(result.StatusCode == HttpStatusCode.OK, "Didn't receive appropriate status code: " + result.StatusCode);
        }

        [TestMethod, ExpectedException(typeof(AuthenticationFailedException))]
        public void Authenticate_Fail()
        {
            try
            {
                var client = new Client(TestConfig.BaseServiceUrl, "BADDATA", "BADDATA");
                ExecuteSampleQuery(client);
            }
            catch (AuthenticationFailedException ex)
            {
                Assert.IsNotNull(ex.Result, "The Result has not been set by the authenticator");
                Assert.IsFalse(string.IsNullOrEmpty(ex.Result.Error),"The Result.Error has not been set the by authenticator");
                Assert.IsFalse(string.IsNullOrEmpty(ex.Result.ErrorDescription), "The Result.ErrorDescription has not been set the by authenticator");
                throw;
            }
        }
    }
}
