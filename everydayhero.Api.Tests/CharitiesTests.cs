using System.IO;
using System.Net;
using System.Reflection;
using everydayhero.Api.Exceptions;
using everydayhero.Api.v3;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using RestSharp;
using RestSharp.Deserializers;

namespace everydayhero.Api.Tests
{

    [TestClass]
    public class CharitiesTests : UnitTestBase
    {
        [TestMethod]
        public void GetCharities()
        {
            var client = GetClient();
            var charities = client.Charity.GetCharities(1, 10, new[] { TestConfig.TestData_Campaign_Uid });
            Assert.IsNotNull(charities);
            Assert.IsTrue(charities.Count > 0, "No charities found");
        }

        [TestMethod]
        public void GetOneThousandCharities()
        {
            var client = GetClient();
            var charities = client.Charity.GetCharities(1, 1000, new[] { TestConfig.TestData_Campaign_Uid });
            Assert.IsNotNull(charities);
            Assert.IsTrue(charities.Count > 0, "No charities found");
        }

        [TestMethod]
        public void GetTwoThousandCharities()
        {
            var client = GetClient();
            client.RestClient.Timeout = 600000; //10 mins
            var charities = client.Charity.GetCharities(1, 2000, new[] { TestConfig.TestData_Campaign_Uid });
            Assert.IsNotNull(charities);
            Assert.IsTrue(charities.Count > 0, "No charities found");
        }

        [TestMethod]
        public void GetCharity()
        {
            var client = GetClient();
            var charities = client.Charity.GetCharities(1, 10, new[] { TestConfig.TestData_Campaign_Uid });
            Assert.IsNotNull(charities);
            Assert.IsTrue(charities.Count > 0, "No charities found");

            var charity = charities[0];
            var verify = client.Charity.GetCharity(charity.id);
            Assert.IsNotNull(verify, "Charity could not be retrieved");
            Assert.AreEqual(charity.id, verify.id, "Verify failed");
        }

        [TestMethod, ExpectedException(typeof(RequestFailedException))]
        public void GetCharity_Fail()
        {
            var client = GetClient();
            try
            {
                client.Charity.GetCharity("abc");
                Assert.Fail("Error was not thrown");
            }
            catch (RequestFailedException ex)
            {
                Assert.IsTrue(ex.StatusCode == HttpStatusCode.NotFound, "Invalid status code returned: " + ex.StatusCode);
                throw;
            }
        }

        public static string GetSampleDataString(string resourceName)
        {
            var assembly = Assembly.GetExecutingAssembly();

            using (var stream = assembly.GetManifestResourceStream(resourceName))
            {
                if (stream != null)
                {
                    using (var reader = new StreamReader(stream))
                    {
                        return reader.ReadToEnd();
                    }
                }
            }

            return null;
        }
    }
}