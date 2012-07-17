using Microsoft.Web.Mvc;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using Microsoft.VisualStudio.TestTools.UnitTesting.Web;
using System.Web.Mvc;

namespace MvcApplicationForMobile.Tests
{
    
    
    /// <summary>
    ///This is a test class for MobileCapableWebFormViewEngineTest and is intended
    ///to contain all MobileCapableWebFormViewEngineTest Unit Tests
    ///</summary>
    [TestClass()]
    public class MobileCapableWebFormViewEngineTest
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
        ///A test for MobileCapableWebFormViewEngine Constructor
        ///</summary>
        // TODO: Ensure that the UrlToTest attribute specifies a URL to an ASP.NET page (for example,
        // http://.../Default.aspx). This is necessary for the unit test to be executed on the web server,
        // whether you are testing a page, web service, or a WCF service.
        [TestMethod()]
        [HostType("ASP.NET")]
        [AspNetDevelopmentServerHost("C:\\Visual Studio\\MVCMobileForMobile\\MvcApplicationForMobile\\MvcApplicationForMobile", "/")]
        [UrlToTest("http://localhost:1094/")]
        public void MobileCapableWebFormViewEngineConstructorTest()
        {
            MobileCapableWebFormViewEngine target = new MobileCapableWebFormViewEngine();
            Assert.Inconclusive("TODO: Implement code to verify target");
        }

        /// <summary>
        ///A test for FindView
        ///</summary>
        // TODO: Ensure that the UrlToTest attribute specifies a URL to an ASP.NET page (for example,
        // http://.../Default.aspx). This is necessary for the unit test to be executed on the web server,
        // whether you are testing a page, web service, or a WCF service.
        [TestMethod()]
        [HostType("ASP.NET")]
        [AspNetDevelopmentServerHost("C:\\Visual Studio\\MVCMobileForMobile\\MvcApplicationForMobile\\MvcApplicationForMobile", "/")]
        [UrlToTest("http://localhost:1094/")]
        public void FindViewTest()
        {
            MobileCapableWebFormViewEngine target = new MobileCapableWebFormViewEngine(); // TODO: Initialize to an appropriate value
            ControllerContext controllerContext = null; // TODO: Initialize to an appropriate value
            string viewName = string.Empty; // TODO: Initialize to an appropriate value
            string masterName = string.Empty; // TODO: Initialize to an appropriate value
            bool useCache = false; // TODO: Initialize to an appropriate value
            ViewEngineResult expected = null; // TODO: Initialize to an appropriate value
            ViewEngineResult actual;
            actual = target.FindView(controllerContext, viewName, masterName, useCache);
            Assert.AreEqual(expected, actual);
            Assert.Inconclusive("Verify the correctness of this test method.");
        }

        /// <summary>
        ///A test for NewFindView
        ///</summary>
        // TODO: Ensure that the UrlToTest attribute specifies a URL to an ASP.NET page (for example,
        // http://.../Default.aspx). This is necessary for the unit test to be executed on the web server,
        // whether you are testing a page, web service, or a WCF service.
        [TestMethod()]
        [HostType("ASP.NET")]
        [AspNetDevelopmentServerHost("C:\\Visual Studio\\MVCMobileForMobile\\MvcApplicationForMobile\\MvcApplicationForMobile", "/")]
        [UrlToTest("http://localhost:1094/")]
        [DeploymentItem("MvcApplicationForMobile.dll")]
        public void NewFindViewTest()
        {
            MobileCapableWebFormViewEngine_Accessor target = new MobileCapableWebFormViewEngine_Accessor(); // TODO: Initialize to an appropriate value
            ControllerContext controllerContext = null; // TODO: Initialize to an appropriate value
            string viewName = string.Empty; // TODO: Initialize to an appropriate value
            string masterName = string.Empty; // TODO: Initialize to an appropriate value
            bool useCache = false; // TODO: Initialize to an appropriate value
            ViewEngineResult expected = null; // TODO: Initialize to an appropriate value
            ViewEngineResult actual;
            actual = target.NewFindView(controllerContext, viewName, masterName, useCache);
            Assert.AreEqual(expected, actual);
            Assert.Inconclusive("Verify the correctness of this test method.");
        }
    }
}
