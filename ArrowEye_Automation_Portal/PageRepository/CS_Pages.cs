using ArrowEye_Automation_Framework.Common;
using SeleniumExtras.PageObjects;
using OpenQA.Selenium;
using ArrowEye_Automation_Portal.PageRepository;
using ArrowEye_Automation_Portal.PageRepository.EMV;

namespace ArrowEye_Automation_Portal
{
    public static class CS_Pages
    {
        private static T GetPage<T>() where T : new()
        {
            var page = new T();
            IWebDriver driver = Browser._Driver;
            PageFactory.InitElements(Browser.Driver, page);
            return page;
        }        

        public static CS_PrintSettingPage CSPrintSettingPage
        {
            get { return GetPage<CS_PrintSettingPage>(); }
        }

        public static CS_BCSS_ConfigurationPage BCSSConfigurationPage
        {
            get { return GetPage<CS_BCSS_ConfigurationPage>(); }
        }


    }
}
