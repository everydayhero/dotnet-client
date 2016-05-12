using System.Configuration;

namespace everydayhero.Api.Tests
{
    /* Example app.config
       <appSettings>
        <!-- Sandbox address: https://edheroz.com -->
        <add key="BaseServiceUrl" value="https://edheroz.com"/>
        <!-- UseProxy : Set to True to use the default fiddler proxy-->
        <add key="UseProxy" value="True"/>
        <!-- Get client secret and id: https://gist.github.com/bradparker/b5074aae4b9a13e5dc7032f703ab4410#step-52-creating-a-supporter-page -->
        <add key="ClientId" value="CLIENT_ID"/>
        <add key="ClientSecret" value="CLIENT_SECRET"/>
      </appSettings>
    */
    public static class TestConfig
    {
        public static string ClientId
        {
            get
            {
                return GetString("ClientId");
            }
        }

        public static string ClientSecret
        {
            get { return GetString("ClientSecret"); }
        }
        public static bool UseProxy
        {
            get { return GetBool("UseProxy"); }
        }

        public static string BaseServiceUrl
        {
            get { return GetString("BaseServiceUrl"); }
        }

        public static string TestData_Charity_Uid
        {
            get { return GetString("TestData_Charity_Uid"); }
        }

        public static string TestData_Campaign_Uid
        {
            get { return GetString("TestData_Campaign_Uid"); }
        }
        public static string TestData_UserEmail
        {
            get { return GetString("TestData_UserEmail"); }
        }


        private static bool GetBool(string key)
        {
            if (ConfigurationManager.AppSettings[key] != null)
            {
                return bool.Parse(ConfigurationManager.AppSettings[key]);
            }
            throw new ConfigurationErrorsException(string.Format("Update the app.config for the unit test project and add the boolean key {0}", key));
        }

        private static string GetString(string key)
        {
            if (ConfigurationManager.AppSettings[key] != null)
            {
                return ConfigurationManager.AppSettings[key];
            }
            throw new ConfigurationErrorsException(string.Format("Update the app.config for the unit test project and add the string key {0}", key));
        }
    }
}