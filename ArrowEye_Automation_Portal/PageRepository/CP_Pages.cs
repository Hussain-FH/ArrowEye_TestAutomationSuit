using ArrowEye_Automation_Framework.Common;
using SeleniumExtras.PageObjects;
using OpenQA.Selenium;
using ArrowEye_Automation_Portal.PageRepository;
using OpenQA.Selenium.Support.UI;
using SeleniumExtras.WaitHelpers;
using System;
using NUnit.Framework;

namespace ArrowEye_Automation_Portal
{
    public static class CP_Pages
    {
        private static T GetPage<T>() where T : new()
        {
            var page = new T();
            IWebDriver driver = Browser._Driver;
            PageFactory.InitElements(Browser.Driver, page);

            return page;
        }

        public static CP_LoginPage Login
        {
            get { return GetPage<CP_LoginPage>(); }
        }

        public static CP_HomePage Home
        {
            get { return GetPage<CP_HomePage>(); }
        }

        public static CP_IssuersPage IssuersPage
        {
            get { return GetPage<CP_IssuersPage>(); }
        }

        public static CP_CSPSettings_BOCDynamicInfoPage BOCDynamicInfoPage
        {
            get { return GetPage<CP_CSPSettings_BOCDynamicInfoPage>(); }
        }

        public static CP_CSPSettings_FOCDynamicInfoPage FOCDynamicInfoPage
        {
            get { return GetPage<CP_CSPSettings_FOCDynamicInfoPage>(); }
        }

        public static CP_CSPSettings_EMVProfilePage EMVProfilePage
        {
            get { return GetPage<CP_CSPSettings_EMVProfilePage>(); }
        }

        public static void TosterMessage_wait()
        {

            IWebDriver _dr = Browser._Driver;
            WebDriverWait wait = new WebDriverWait(_dr, TimeSpan.FromSeconds(4));
            try
            {
                var abc = wait.Until(ExpectedConditions.TextToBePresentInElementLocated(By.Id("notistack-snackbar"), "Dynamic Info Successfully."));
                // You could add an Assert.Pass() here if you want but if you get here, you've already confirmed that the element exists with the expected text.
            }
            catch (WebDriverTimeoutException)
            {
                Assert.Fail("After clicking the save button, the toast message did not appear with the expected text.");
            }

            // return abc.ToString();
        }

        public static void waitFor_TosterMessageCapture()
        {
            try
            {

                IWebDriver _driver = Browser._Driver;
                WebDriverWait wait = new WebDriverWait(_driver, TimeSpan.FromSeconds(4));

                DefaultWait<IWebDriver> fluentWait = new DefaultWait<IWebDriver>(_driver);
                fluentWait.Timeout = TimeSpan.FromSeconds(3);
                fluentWait.PollingInterval = TimeSpan.FromMilliseconds(250);
                /* Ignore the exception - NoSuchElementException that indicates that the element is not present */
                fluentWait.IgnoreExceptionTypes(typeof(NoSuchElementException));
                fluentWait.Message = "Element to be searched not found";
            }
            catch(Exception ex)
            {
                Assert.Fail("");
            }
            }
    }
}

