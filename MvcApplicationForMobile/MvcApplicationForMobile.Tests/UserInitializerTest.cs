using MvcApplicationForMobile.DAL;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using Microsoft.VisualStudio.TestTools.UnitTesting.Web;
using MvcApplicationForMobile.Models;

namespace MvcApplicationForMobile.Tests
{
    
    
    /// <summary>
    ///This is a test class for UserInitializerTest and is intended
    ///to contain all UserInitializerTest Unit Tests
    ///</summary>
    [TestClass()]
    public class UserInitializerTest
    {


        private TestContext testContextInstance;

        /// <summary>
        ///Gets or sets the test context which provides
        ///information about and functionality for the current test run.
        ///</summary>
        public TestContext TestContext
        {
            get
            {
                return testContextInstance;
            }
            set
            {
                testContextInstance = value;
            }
        }

        #region Additional test attributes
        // 
        //You can use the following additional attributes as you write your tests:
        //
        //Use ClassInitialize to run code before running the first test in the class
        //[ClassInitialize()]
        //public static void MyClassInitialize(TestContext testContext)
        //{
        //}
        //
        //Use ClassCleanup to run code after all tests in a class have run
        //[ClassCleanup()]
        //public static void MyClassCleanup()
        //{
        //}
        //
        //Use TestInitialize to run code before running each test
        //[TestInitialize()]
        //public void MyTestInitialize()
        //{
        //}
        //
        //Use TestCleanup to run code after each test has run
        //[TestCleanup()]
        //public void MyTestCleanup()
        //{
        //}
        //
        #endregion


        /// <summary>
        ///A test for UserInitializer Constructor
        ///</summary>
        // TODO: Ensure that the UrlToTest attribute specifies a URL to an ASP.NET page (for example,
        // http://.../Default.aspx). This is necessary for the unit test to be executed on the web server,
        // whether you are testing a page, web service, or a WCF service.
        [TestMethod()]
        [HostType("ASP.NET")]
        [AspNetDevelopmentServerHost("C:\\Visual Studio\\MVCMobileForMobile\\MvcApplicationForMobile\\MvcApplicationForMobile", "/")]
        [UrlToTest("http://localhost:1094/")]
        public void UserInitializerConstructorTest()
        {
            UserInitializer target = new UserInitializer();
            Assert.Inconclusive("TODO: Implement code to verify target");
        }

        /// <summary>
        ///A test for Seed
        ///</summary>
        // TODO: Ensure that the UrlToTest attribute specifies a URL to an ASP.NET page (for example,
        // http://.../Default.aspx). This is necessary for the unit test to be executed on the web server,
        // whether you are testing a page, web service, or a WCF service.
        [TestMethod()]
        [HostType("ASP.NET")]
        [AspNetDevelopmentServerHost("C:\\Visual Studio\\MVCMobileForMobile\\MvcApplicationForMobile\\MvcApplicationForMobile", "/")]
        [UrlToTest("http://localhost:1094/")]
        [DeploymentItem("MvcApplicationForMobile.dll")]
        public void SeedTest()
        {
            UserInitializer_Accessor target = new UserInitializer_Accessor(); // TODO: Initialize to an appropriate value
            UserContext context = null; // TODO: Initialize to an appropriate value
            target.Seed(context);
            Assert.Inconclusive("A method that does not return a value cannot be verified.");
        }
    }
}
