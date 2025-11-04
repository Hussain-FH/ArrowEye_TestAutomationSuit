using ArrowEye_Automation_Framework;
using ArrowEye_Automation_Framework.Excel;
using ArrowEye_Automation_Portal.PageRepository.EMV;
using NUnit.Framework;
using RandomString4Net;

namespace ArrowEye_Automation_Portal.Tests.Feature.EMV
{
    [TestFixture]
    public class Scripts :TestBase
    {
        string randomString = RandomString.GetString(Types.ALPHANUMERIC_LOWERCASE, 10);
        
        [Test]
        [Description("EMV_Scripts_Records_Create")]
        [Category("Smoke")]
        [TestCaseSource(typeof(ExcelDataParser), "TestData", new object[] { "LoginData", new int[] { 1 } })]
        public void Add_EMV_NewRecord(ArrowEye_Automation_Framework.Excel.TestCaseData TestCaseData)
        {
            CP_Pages.Login.LogIn(TestCaseData.GetValue("Username"), TestCaseData.GetValue("Password"));
            CP_Pages.Home.ValidateHomePageTitle();
            CP_Pages.Home.NavigateToEMVSubMenu("Scripts");
            EMV_Pages.EMVScriptsPage.ValidateEMVScriptsFeild();
            EMV_Pages.EMVScriptsPage.AddNewEMVScriptRecord("Automation_Create_New_Script" + randomString);
        }

        [Test]
        [Description("EMV_Script_Records_Edit")]
        [Category("Smoke")]
        [TestCaseSource(typeof(ExcelDataParser), "TestData", new object[] { "LoginData", new int[] { 1 } })]
        public void Edit_EMV_Record(ArrowEye_Automation_Framework.Excel.TestCaseData TestCaseData)
        {
            CP_Pages.Login.LogIn(TestCaseData.GetValue("Username"), TestCaseData.GetValue("Password"));
            CP_Pages.Home.ValidateHomePageTitle();
            CP_Pages.Home.NavigateToEMVSubMenu("Scripts");
            EMV_Pages.EMVScriptsPage.EditEMVScriptRecord("Automation_Edit_Script" + randomString);
        }

        [Test]
        [Description("EMV_Script_Records_fieldValidation")]
        [Category("Smoke")]
        [TestCaseSource(typeof(ExcelDataParser), "TestData", new object[] { "LoginData", new int[] { 1 } })]
        public void Validation_EMV_Script_Field(ArrowEye_Automation_Framework.Excel.TestCaseData TestCaseData)
        {
            CP_Pages.Login.LogIn(TestCaseData.GetValue("Username"), TestCaseData.GetValue("Password"));
            CP_Pages.Home.ValidateHomePageTitle();
            CP_Pages.Home.NavigateToEMVSubMenu("Scripts");
            EMV_Pages.EMVScriptsPage.ValidateEMVScriptField("Automation_Edit_Script" + randomString);
        }

        [Test]
        [Description("EMV_Script_Records_Export")]
        [Category("Smoke")]
        [TestCaseSource(typeof(ExcelDataParser), "TestData", new object[] { "LoginData", new int[] { 1 } })]
        public void Export_EMV_Script_File(ArrowEye_Automation_Framework.Excel.TestCaseData TestCaseData)
        {
            CP_Pages.Login.LogIn(TestCaseData.GetValue("Username"), TestCaseData.GetValue("Password"));
            CP_Pages.Home.ValidateHomePageTitle();
            CP_Pages.Home.NavigateToEMVSubMenu("Scripts");
            EMV_Pages.EMVScriptsPage.ValidateEMVScriptsFeild();
            EMV_Pages.EMVScriptsPage.ExportEMVScriptPages();
        }
    }
}
