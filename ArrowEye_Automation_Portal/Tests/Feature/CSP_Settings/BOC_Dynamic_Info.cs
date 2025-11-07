using ArrowEye_Automation_Framework;
using ArrowEye_Automation_Framework.Common;
using NUnit.Framework;
using RandomString4Net;

namespace ArrowEye_Automation_Portal.Tests.Feature.CSP_Settings
{
    [TestFixture]
    public class BOC_Dynamic_Info : TestBase
    {
        string randomString = RandomString.GetString(Types.ALPHANUMERIC_LOWERCASE, 10);

        [Test]
        [Description("BOC_Dynamic_Info_Create")]
        [Category("Smoke")]
        [TestCase("Automation_Create_Info")]
        public void Create_New_BOC_Dynamic_Info(string CreateBOCDynamicText)
        {
            CP_Pages.Login.LogIn("shaikhussainpasha", "Shaik@12345");
            CP_Pages.Home.ValidateHomePageTitle();            
            CP_Pages.Home.NavigateToCSPSettingsSubMenu("BOC Dynamic Info");
            CP_Pages.BOCDynamicInfoPage.AddNewBOCDynamicInfo(CreateBOCDynamicText + randomString);
            DriverUtilities.TakeScreenshot(@"C:\");
        }

        [Test]
        [Description("BOC_Dynamic_Info_Edit")]
        [Category("Smoke")]
        [TestCase("Automation_Edit_Info_")]
        public void Edit_BOC_Dynamic_Info(string UpdateBOCDynamicText)
        {
            CP_Pages.Login.LogIn("shaikhussainpasha", "Shaik@12345");
            CP_Pages.Home.ValidateHomePageTitle();
            CP_Pages.Home.NavigateToCSPSettingsSubMenu("BOC Dynamic Info");
            CP_Pages.BOCDynamicInfoPage.EditBOCDynamicInfo(UpdateBOCDynamicText + randomString);
            DriverUtilities.TakeScreenshot(@"C:\");
        }

        [Test]
        [Description("BOC_Dynamic_Info_Delete")]
        [Category("Smoke")]
        [TestCase("Automation_Delete_Info_")]
        public void Delete_BOC_Dynamic_Info(string DeleteBOCDynamicText)
        {
            CP_Pages.Login.LogIn("shaikhussainpasha", "Shaik@12345");
            CP_Pages.Home.ValidateHomePageTitle();
            CP_Pages.Home.NavigateToCSPSettingsSubMenu("BOC Dynamic Info");
            CP_Pages.BOCDynamicInfoPage.DeleteBOCDynamicInfo(DeleteBOCDynamicText + randomString);
            DriverUtilities.TakeScreenshot(@"C:\");
        }

        [Test]
        [Description("BOC_Dynamic_Info_View_Search")]
        [Category("Smoke")]
        [TestCase("Automation_Search_View_BOCDynamicInfo_")]
        public void View_Search_BOC_Dynamic_Info(string ViewSearchBOCDynamicText)
        {
            CP_Pages.Login.LogIn("shaikhussainpasha", "Shaik@12345");
            CP_Pages.Home.ValidateHomePageTitle();
            CP_Pages.Home.NavigateToCSPSettingsSubMenu("BOC Dynamic Info");
            CP_Pages.BOCDynamicInfoPage.ViewandSearchBOCDynamicInfo(ViewSearchBOCDynamicText + randomString);
            DriverUtilities.TakeScreenshot(@"C:\");
        }
    }
}

