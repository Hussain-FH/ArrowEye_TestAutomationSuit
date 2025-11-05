using ArrowEye_Automation_Framework.Common;
using ArrowEye_Automation_Framework;
using NUnit.Framework;
using RandomString4Net;

namespace ArrowEye_Automation_Portal.Tests.Feature.CSP_Settings
{
    internal class FOC_Dynamic_Info : TestBase
    {
        string randomString = RandomString.GetString(Types.ALPHANUMERIC_LOWERCASE, 10);

        [Test]
        [Description("FOC_Dynamic_Info_Create")]
        [Category("Smoke")]
        [TestCase("Automation_Create_Info_")]
        public void Create_New_FOC_Dynamic_Info(string CreateFOCDynamicText)
        {
            CP_Pages.Login.LogIn("shaikhussainpasha", "Shaik@12345");
            CP_Pages.Home.ValidateHomePageTitle();
            CP_Pages.Home.NavigateToCSPSettingsSubMenu("FOC Dynamic Info");
            CP_Pages.FOCDynamicInfoPage.AddNewFOCDynamicInfo(CreateFOCDynamicText + randomString);
            DriverUtilities.TakeScreenshot(@"C:\");
        }

        [Test]
        [Description("FOC_Dynamic_Info_Edit")]
        [Category("Smoke")]
        [TestCase("Automation_Edit_Info_")]
        public void Edit_FOC_Dynamic_Info(string UpdateFOCDynamicText)
        {
            CP_Pages.Login.LogIn("shaikhussainpasha", "Shaik@12345");
            CP_Pages.Home.ValidateHomePageTitle();
            CP_Pages.Home.NavigateToCSPSettingsSubMenu("FOC Dynamic Info");
            CP_Pages.FOCDynamicInfoPage.EditFOCDynamicInfo(UpdateFOCDynamicText + randomString);
            DriverUtilities.TakeScreenshot(@"C:\");
        }

        [Test]
        [Description("FOC_Dynamic_Info_Delete")]
        [Category("Smoke")]
        [TestCase("Automation_Delete_Info_")]
        public void Delete_FOC_Dynamic_Info(string DeleteBOCDynamicText)
        {
            CP_Pages.Login.LogIn("shaikhussainpasha", "Shaik@12345");
            CP_Pages.Home.ValidateHomePageTitle();
            CP_Pages.Home.NavigateToCSPSettingsSubMenu("FOC Dynamic Info");
            CP_Pages.FOCDynamicInfoPage.DeleteFOCDynamicInfo(DeleteBOCDynamicText + randomString);
            DriverUtilities.TakeScreenshot(@"C:\");
        }

        [Test]
        [Description("BOC_Dynamic_Info_View_Search")]
        [Category("Smoke")]
        [TestCase("Automation_Search_View_BOCDynamicInfo_")]
        public void View_Search_FOC_Dynamic_Info(string ViewSearchBOCDynamicText)
        {
            CP_Pages.Login.LogIn("shaikhussainpasha", "Shaik@12345");
            CP_Pages.Home.ValidateHomePageTitle();
            CP_Pages.Home.NavigateToCSPSettingsSubMenu("FOC Dynamic Info");
            CP_Pages.FOCDynamicInfoPage.ViewandSearchFOCDynamicInfo(ViewSearchBOCDynamicText + randomString);
            DriverUtilities.TakeScreenshot(@"C:\");
        }

        [Test]
        [Description("FOC_Dynamic_Info_Negative_Scenarios_Validations")]
        [Category("Smoke")]
        [TestCase("Automation_Negative_Scenarios")]
        public void Negative_Validations_FOC_Dynamic_Info(string ViewSearchBOCDynamicText)
        {
            string randomStrings = RandomString.GetString(Types.ALPHANUMERIC_LOWERCASE, 50);
            CP_Pages.Login.LogIn("shaikhussainpasha", "Shaik@12345");
            CP_Pages.Home.ValidateHomePageTitle();
            CP_Pages.Home.NavigateToCSPSettingsSubMenu("FOC Dynamic Info");
            CP_Pages.FOCDynamicInfoPage.ValidationsFOCDynamicInfo(ViewSearchBOCDynamicText + randomStrings);
            DriverUtilities.TakeScreenshot(@"C:\");
        }
    }
}

