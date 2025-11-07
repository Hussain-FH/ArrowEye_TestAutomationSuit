using ArrowEye_Automation_Framework.Common;
using ArrowEye_Automation_Framework;
using NUnit.Framework;
using RandomString4Net;

namespace ArrowEye_Automation_Portal.Tests.Feature.CSP_Settings
{
    public class EMV_Profile : TestBase
    {
        string randomString = RandomString.GetString(Types.ALPHANUMERIC_LOWERCASE, 10);

        [Test]
        [Description("EMVProfile_Create")]
        [Category("Smoke")]
        [TestCase("Automation_Create_EMVProfile")]
        public void Create_EMVProfile(string CreateEMVProfileText)
        {
            CP_Pages.Login.LogIn("shaikhussainpasha", "Shaik@12345");
            CP_Pages.Home.ValidateHomePageTitle();
            CP_Pages.Home.NavigateToCSPSettingsSubMenu("EMV Profile");
            CP_Pages.EMVProfilePage.AddNewEMVProfile(CreateEMVProfileText + randomString);
            DriverUtilities.TakeScreenshot(@"C:\");
        }

        [Test]
        [Description("EMVProfile_Update")]
        [Category("Smoke")]
        [TestCase("Automation_Edit_EMVProfile")]
        public void Edit_EMVProfile(string UpdateEMVProfileText)
        {
            CP_Pages.Login.LogIn("shaikhussainpasha", "Shaik@12345");
            CP_Pages.Home.ValidateHomePageTitle();
            CP_Pages.Home.NavigateToCSPSettingsSubMenu("EMV Profile");
            CP_Pages.EMVProfilePage.EditEMVProfile(UpdateEMVProfileText + randomString);
            DriverUtilities.TakeScreenshot(@"C:\");
        }

        [Test]
        [Description("EMVProfile_Delete")]
        [Category("Smoke")]
        [TestCase("Automation_Delete_EMVProfile")]
        public void Delete_EMVProfile(string DeleteEMVProfileText)
        {
            CP_Pages.Login.LogIn("shaikhussainpasha", "Shaik@12345");
            CP_Pages.Home.ValidateHomePageTitle();
            CP_Pages.Home.NavigateToCSPSettingsSubMenu("EMV Profile");
            CP_Pages.EMVProfilePage.DeleteEMVProfile(DeleteEMVProfileText + randomString);
            DriverUtilities.TakeScreenshot(@"C:\");
        }

        [Test]
        [Description("EMVProfile_Create")]
        [Category("Smoke")]
        [TestCase("Automation_Create_EMVProfile")]
        public void View_Search_EMVProfile(string ViewSearchEMVProfileText)
        {
            CP_Pages.Login.LogIn("shaikhussainpasha", "Shaik@12345");
            CP_Pages.Home.ValidateHomePageTitle();
            CP_Pages.Home.NavigateToCSPSettingsSubMenu("EMV Profile");
            CP_Pages.EMVProfilePage.ViewandSearchEMVProfile(ViewSearchEMVProfileText + randomString);
            DriverUtilities.TakeScreenshot(@"C:\");
        }

        [Test]
        [Description("EMVProfile_Negative_Scenarios_Validations")]
        [Category("Smoke")]
        [TestCase("Automation_Negative_Scenarios")]
        public void Negative_Validations_EMVProfile(string Invalid_EMVProfile_InputsText)
        {
            string randomStrings = RandomString.GetString(Types.ALPHANUMERIC_LOWERCASE, 10);
            CP_Pages.Login.LogIn("shaikhussainpasha", "Shaik@12345");
            CP_Pages.Home.ValidateHomePageTitle();
            CP_Pages.Home.NavigateToCSPSettingsSubMenu("EMV Profile");
            CP_Pages.EMVProfilePage.ValidationsEMVProfile(Invalid_EMVProfile_InputsText + randomStrings);
            DriverUtilities.TakeScreenshot(@"C:\");
        }
    }
}

