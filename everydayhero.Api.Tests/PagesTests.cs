using System;
using System.Collections.Generic;
using everydayhero.Api.Constants;
using everydayhero.Api.v3;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using RestSharp;

namespace everydayhero.Api.Tests
{
    [TestClass]
    public class PagesTests : UnitTestBase
    {
        private static PageCreationFields<string> GetNewPageInfo()
        {
            var newPageInfo = new PageCreationFields<string>
            {
                target = "500",
                name = "Test Developer",
                expires_at = DateTime.Now.AddDays(14).ToString("O"),
                slug = "Test" + Guid.NewGuid(),
                birthday = "1980-01-01",
                campaign_id = TestConfig.TestData_Campaign_Uid,
                charity_id = TestConfig.TestData_Charity_Uid,
                user_email = TestConfig.TestData_UserEmail,
                user_phone = "55556666",
                user_name = "Test Dev",
                user_address = "Test address"
            };
            return newPageInfo;
        }

        private static PageCreationFields<UserAddress> GetNewPageInfoWithComplexAddress()
        {
            var newPageInfo = new PageCreationFields<UserAddress>
            {
                target = "500",
                name = "Test Developer",
                expires_at = DateTime.Now.AddDays(14).ToString("O"),
                slug = "Test" + Guid.NewGuid(),
                birthday = "1980-01-01",
                campaign_id = TestConfig.TestData_Campaign_Uid,
                charity_id = TestConfig.TestData_Charity_Uid,
                user_email = TestConfig.TestData_UserEmail,
                user_phone = "55556666",
                user_name = "Test Dev",
                user_address = new UserAddress
                {
                    user_street_address = "Test street address",
                    user_extended_address = "Test extended address",
                    user_locality = "Test locality",
                    user_region = "Test region",
                    user_postal_code = "Test postal code",
                    user_country_name = "Test country name"
                }
            };
            return newPageInfo;
        }

        [TestMethod]
        public void CreateSupporterPage()
        {
            var client = GetClient();
            var newPageInfo = GetNewPageInfo();
            var pageCreatedResult = client.Pages.CreateSupporterPage(newPageInfo);
            Assert.IsNotNull(pageCreatedResult);
            Assert.IsFalse(string.IsNullOrEmpty(pageCreatedResult.activation_url));
        }

        [TestMethod]
        public void CreateSupporterPageWithComplexAddress()
        {
            var client = GetClient();
            var newPageInfo = GetNewPageInfoWithComplexAddress();
            var pageCreatedResult = client.Pages.CreateSupporterPage(newPageInfo);
            Assert.IsNotNull(pageCreatedResult);
            Assert.IsFalse(string.IsNullOrEmpty(pageCreatedResult.activation_url));
        }

        [TestMethod]
        public void GetSupporterPages()
        {
            var client = GetClient();
            var result = client.Pages.GetSupporterPages(1, 10, TestConfig.TestData_Campaign_Uid);
            Assert.IsNotNull(result);
            if (result.Count == 0)
            {
                Assert.Inconclusive("No data available");
            }
            var pages = client.Pages.GetSupporterPages(new[] { result[0].id, result[1].id });
            Assert.IsTrue(result[0].id == pages[0].id || result[0].id == pages[1].id, "Data differs");
        }


        [TestMethod]
        public void UpdateSupporterPage()
        {
            var client = GetClient();
            var newPageInfo = GetNewPageInfo();
            var pageCreatedResult = client.Pages.CreateSupporterPage(newPageInfo);
            Assert.IsNotNull(pageCreatedResult);
            Assert.IsFalse(string.IsNullOrEmpty(pageCreatedResult.activation_url));

            var pages = client.Pages.GetSupporterPages(new[] { pageCreatedResult.page.id });
            Assert.IsNotNull(pages);
            Assert.AreEqual(1, pages.Count);
            Assert.AreEqual(pageCreatedResult.page.id, pages[0].id);

            newPageInfo.name = "Test1Update";
            newPageInfo.slug = pageCreatedResult.page.slug;
            var updatedPage = client.Pages.UpdateSupporterPage(pages[0].id, newPageInfo);
            Assert.AreEqual(newPageInfo.name, updatedPage.name, "The name did not update");
        }

        [TestMethod]
        public void UpdateSupporterPageWithComplexAddress()
        {
            var client = GetClient();
            var newPageInfo = GetNewPageInfoWithComplexAddress();
            var pageCreatedResult = client.Pages.CreateSupporterPage(newPageInfo);
            Assert.IsNotNull(pageCreatedResult);
            Assert.IsFalse(string.IsNullOrEmpty(pageCreatedResult.activation_url));

            var pages = client.Pages.GetSupporterPages(new[] { pageCreatedResult.page.id });
            Assert.IsNotNull(pages);
            Assert.AreEqual(1, pages.Count);
            Assert.AreEqual(pageCreatedResult.page.id, pages[0].id);

            newPageInfo.name = "Test1Update";
            newPageInfo.slug = pageCreatedResult.page.slug;
            var updatedPage = client.Pages.UpdateSupporterPage(pages[0].id, newPageInfo);
            Assert.AreEqual(newPageInfo.name, updatedPage.name, "The name did not update");
        }
    }
}
