using System;
using System.Collections.Generic;
using everydayhero.Api.v3;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace everydayhero.Api.Tests
{
    [TestClass]
    public class PagesTests : UnitTestBase
    {
        [TestMethod]
        public void CreateSupporterPage()
        {
            var client = GetClient();
            var newPageInfo = GetNewPageInfo();
            var page = client.Pages.CreateSupporterPage(newPageInfo);
            Assert.IsNotNull(page);
        }

        private static PageCreationFields GetNewPageInfo()
        {
            var newPageInfo = new PageCreationFields
            {
                target = "500",
                name = "Test Developer",
                expires_at = DateTime.Now.AddDays(14).ToString("O"),
                slug = "Test" + Guid.NewGuid(),
                birthday = "1980-01-01",
                campaign_id = TestConfig.TestData_Campaign_Uid,
                charity_id = TestConfig.TestData_Charity_Uid,
                user_email = TestConfig.TestData_UserEmail,
                user_name = "Test Dev"
            };
            return newPageInfo;
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
            var createdPage = client.Pages.CreateSupporterPage(newPageInfo);
            Assert.IsNotNull(createdPage);

            List<Page> pages = client.Pages.GetSupporterPages(new[] {createdPage.id});
            Assert.IsNotNull(pages);
            Assert.AreEqual(1, pages.Count);
            Assert.AreEqual(createdPage.id, pages[0].id);

            newPageInfo.name = "Test1Update";
            var updatedPage = client.Pages.UpdateSupporterPage(pages[0].id, newPageInfo);
            Assert.AreEqual(newPageInfo.name, updatedPage.name, "The name did not update");
        }
    }
}
