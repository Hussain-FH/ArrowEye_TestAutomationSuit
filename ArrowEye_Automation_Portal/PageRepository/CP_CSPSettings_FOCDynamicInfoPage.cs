using ArrowEye_Automation_Framework.Common;
using ArrowEye_Automation_Framework;
using NUnit.Framework;
using OpenQA.Selenium;
using SeleniumExtras.PageObjects;
using System;
using System.Threading;

namespace ArrowEye_Automation_Portal.PageRepository
{
    public class CP_CSPSettings_FOCDynamicInfoPage : TestBase
    {

        [FindsBy(How = How.XPath, Using = "//li[@data-testid='nestedMenuItem']//p[contains(text(),'CSP Settings')]")]
        public IWebElement CSVSettings_menu;

        [FindsBy(How = How.XPath, Using = "//li[@data-testid='subMenuItems']//p[contains(text(),'BOC Dynamic Info')]")]
        public IWebElement BOC_Dynamic_Info_submenu;

        [FindsBy(How = How.XPath, Using = "//li[@data-testid='subMenuItems']//p[contains(text(),'Card Holder Agreement')]")]
        public IWebElement Card_Holder_Agreement_submenu;

        [FindsBy(How = How.XPath, Using = "//li[@data-testid='subMenuItems']//p[contains(text(),'Carrier Dynamic Info')]")]
        public IWebElement Carrier_Dynamic_Info_submenu;

        [FindsBy(How = How.XPath, Using = "//li[@data-testid='subMenuItems']//p[contains(text(),'EMV Profile')]")]
        public IWebElement EMV_Profile_submenu;

        [FindsBy(How = How.XPath, Using = "//li[@data-testid='subMenuItems']//p[contains(text(),'FOC Dynamic Info')]")]
        public IWebElement FOC_Dynamic_Info_submenu;

        [FindsBy(How = How.XPath, Using = "//p[@data-testid='secondNested' and contains(text(),'BOC Dynamic Info')]")]
        private IWebElement BOCDynamicInfo_menudropdowntext;

        [FindsBy(How = How.XPath, Using = "//div[@class='jss13 MuiBox-root css-0']//p[contains(text(),'FOC Dynamic Info')]")]
        private IWebElement FOCDynamicInfoPage_HeaderText;

        [FindsBy(How = How.XPath, Using = "//div[@class='MuiDataGrid-columnHeaderTitleContainerContent']//div[contains(text(),'BOC Info ID')]")]
        private IWebElement BOCInfoID_GridColoumn;

        [FindsBy(How = How.XPath, Using = "//div[@class='MuiDataGrid-columnHeaderTitleContainerContent']//div[contains(text(),'BOC Dynamic Text')]")]
        private IWebElement BOCDynamicText_GridColoumn;

        [FindsBy(How = How.XPath, Using = "//div[@class='MuiDataGrid-columnHeaderTitleContainerContent']//div[contains(text(),'Last Updated')]")]
        private IWebElement LastUpdated_GridColoumn;

        [FindsBy(How = How.XPath, Using = "//div[@class='MuiDataGrid-columnHeaderTitleContainerContent']//div[contains(text(),'Actions')]")]
        private IWebElement Actions_GridColoumn;

        [FindsBy(How = How.XPath, Using = "//button[@aria-haspopup='menu' and contains(text(),'Columns')]")]
        private IWebElement BOCDynamicInfo_Columns_button;

        [FindsBy(How = How.XPath, Using = "//div[@class='MuiDataGrid-columnsManagementHeader css-1c0mwkm']//input[@type='text']")]
        private IWebElement BOCDynamicInfo_Columnspopup_Searchtextbox;

        [FindsBy(How = How.XPath, Using = "//span[contains(text(),'Row reordering')]")]
        private IWebElement BOCDynamicInfo_Columnspopup_Rowreordering_btn;

        [FindsBy(How = How.XPath, Using = "//span[contains(text(),'BOC Info ID')]")]
        private IWebElement BOCDynamicInfo_Columnspopup_BOCInfoID_btn;

        [FindsBy(How = How.XPath, Using = "//span[contains(text(),'BOC Dynamic Text')]")]
        private IWebElement BOCDynamicInfo_Columnspopup_BOCDynamicText_btn;

        [FindsBy(How = How.XPath, Using = "//span[contains(text(),'Last Updated')]")]
        private IWebElement BOCDynamicInfo_Columnspopup_LastUpdated_btn;

        [FindsBy(How = How.XPath, Using = "//span[contains(text(),'Actions')]")]
        private IWebElement BOCDynamicInfo_Columnspopup_Actions_btn;

        [FindsBy(How = How.XPath, Using = "//span[contains(text(),'Show/Hide All')]")]
        private IWebElement BOCDynamicInfo_Columnspopup_Show_HideAll_btn;

        [FindsBy(How = How.XPath, Using = "//button[contains(text(),'Reset')]")]
        private IWebElement BOCDynamicInfo_Columnspopup_Resetbtn;

        [FindsBy(How = How.XPath, Using = "//button[@id=':rge:']")]
        private IWebElement BOCDynamicInfo_Filters_button;

        [FindsBy(How = How.XPath, Using = "//button[@id=':rgh:']")]
        private IWebElement BOCDynamicInfo_Export_button;

        [FindsBy(How = How.XPath, Using = "//button[contains(text(),'Print')]")]
        private IWebElement BOCDynamicInfo_Print_button;

        [FindsBy(How = How.XPath, Using = "//input[@placeholder='Search…']")]
        private IWebElement FOCDynamicInfo_AutoSearch_textbox;

        [FindsBy(How = How.XPath, Using = "//button[@class='MuiButtonBase-root MuiIconButton-root MuiIconButton-sizeSmall css-irzi55']")]
        private IWebElement BOCDynamicInfo_AutoSearch_textboxclear;

        [FindsBy(How = How.XPath, Using = "//button[@data-testid='openModal']//p[contains(text(),'Add New')]")]
        private IWebElement FOCDynamicInfo_AddNew_button;

        [FindsBy(How = How.XPath, Using = "//div[@class='MuiDataGrid-columnHeaderTitle css-mh3zap' and contains(text(),'FOC Info ID')]")]
        private IWebElement FOC_homepage_IDGridcol;

        [FindsBy(How = How.XPath, Using = "//div[@class='MuiDataGrid-columnHeaderTitle css-mh3zap' and contains(text(),'FOC Dynamic Text')]")]
        private IWebElement FOC_homepage_FOCDynamicTextcol;

        [FindsBy(How = How.XPath, Using = "//div[@class='MuiDataGrid-columnHeaderTitle css-mh3zap' and contains(text(),'Last Updated')]")]
        private IWebElement FOC_homepage_LastUpdatedcol;

        [FindsBy(How = How.XPath, Using = "//div[@class='MuiDataGrid-columnHeaderTitle css-mh3zap' and contains(text(),'Actions')]")]
        private IWebElement FOCD_homepage_Actioncol;

        [FindsBy(How = How.XPath, Using = "(//button[@data-testid='deleteFocd'])[position()=1]")]

        private IWebElement FOCDynamicInfo_Detete_Icon_button;

        [FindsBy(How = How.XPath, Using = "//button[@data-testid='cancelButton']")]

        private IWebElement FOCDynamicInfo_Detete_popup_Cancelbutton;

        [FindsBy(How = How.XPath, Using = "//h2[contains(text(),'Delete')]")]
        private IWebElement FOCDynamicInfo_Detete_popup_HeaderText;

        [FindsBy(How = How.XPath, Using = "//div[@class='MuiStack-root css-j7qwjs']")]
        private IWebElement FOCDynamicInfo_Detete_popup_warningmessage;

        [FindsBy(How = How.XPath, Using = "(//button[@class='MuiButtonBase-root MuiIconButton-root MuiIconButton-sizeSmall textPrimary css-1pydezi'])[2]")]
        private IWebElement FOCDynamicInfo_Edit_Icon_button;

        [FindsBy(How = How.XPath, Using = "//p[contains(text(),'New FOC Dynamic Info')]")]
        private IWebElement NewFocDynamicInfoPopup_HeaderText;

        [FindsBy(How = How.XPath, Using = "//button[@data-testid='closeButton']")]
        private IWebElement NewBocDynamicInfoPopup_Closebtn;

        [FindsBy(How = How.XPath, Using = "//input[@data-testid='nameInputField']")]
        private IWebElement NewFocDynamicInfoPopup_Textbox;

        [FindsBy(How = How.XPath, Using = "//button[@data-testid='cancelCSP']")]
        private IWebElement NewFocDynamicInfoPopup_Cancelbtn;

        [FindsBy(How = How.XPath, Using = "//p[@class='MuiTypography-root MuiTypography-body1 css-qr1enj']")]
        private IWebElement UpdateFocDynamicInfoPopup_IDField;

        [FindsBy(How = How.XPath, Using = "//label[text()='FOC Dynamic Text']")]
        private IWebElement UpdateFocDynamicInfoPopup_FOCDynamicTextField;

        [FindsBy(How = How.XPath, Using = "//p[@class='MuiTypography-root MuiTypography-body1 jss129 css-9l3uo3' and contains(text(),'Update FOC Dynamic Info')]")]
        private IWebElement UpdateFocDynamicInfoPopup_HeaderText;

        [FindsBy(How = How.XPath, Using = "//button[@data-testid='cancelButton']")]
        private IWebElement DeleteFocDynamicInfoPopup_Cancelbtn;

        [FindsBy(How = How.XPath, Using = "//button[@data-testid='okButton']//p[contains(text(),'Delete')]")]
        private IWebElement DeleteFocDynamicInfoPopup_Deletebtn;

        [FindsBy(How = How.XPath, Using = "//div[@class='MuiDataGrid-overlay css-14349d1' and contains(text(),'No results found.')]")]
        private IWebElement DeleteFocDynamicInfoPopup_deletedrecorddetails;

        [FindsBy(How = How.XPath, Using = "//div[@class='MuiDataGrid-overlayWrapperInner css-0']//div[ contains(text(),'No results found.')]")]
        private IWebElement Search_View_InvalidRecordDetails;

        [FindsBy(How = How.XPath, Using = "//button[@data-testid='saveCSP']")]
        private IWebElement NewFocDynamicInfoPopup_Savebtn;

        [FindsBy(How = How.XPath, Using = "//p[@id=':rkm:-helper-text' and contains(text(),'BOC Dynamic Text is required.')]")]
        private IWebElement NewBocDynamicInfoPopup_validationmessage;

        [FindsBy(How = How.XPath, Using = "(//div[@data-colindex='2'])[position()=1]")]
        private IWebElement FocDynamicInfo_created;

        [FindsBy(How = How.XPath, Using = "(//div[@data-colindex='1'])[position()=1]")]
        private IWebElement FocDynamicInfo_created_ID;

        [FindsBy(How = How.XPath, Using = "(//div[@data-colindex='2'])[position()=1]")]
        private IWebElement FocDynamicInfo_Updated;

        [FindsBy(How = How.XPath, Using = "//div[@class='go1888806478 notistack-MuiContent notistack-MuiContent-success go167266335 go1725278324 go3162094071']//div[@id='notistack-snackbar']")]
        private IWebElement FocDynamicInfo_recordAdd_sucessmessage;

        [FindsBy(How = How.XPath, Using = "//p[@class='MuiFormHelperText-root Mui-error MuiFormHelperText-sizeMedium MuiFormHelperText-contained css-v7esy']")]
        private IWebElement FocDynamicInfo_Addwithouttext_Error;

        [FindsBy(How = How.XPath, Using = "//p[@class='MuiFormHelperText-root Mui-error MuiFormHelperText-sizeMedium MuiFormHelperText-contained css-v7esy']")]
        private IWebElement FocDynamicInfo_Textinput_exceedcharlimit_error;

        [FindsBy(How = How.XPath, Using = "//div[@class='go1888806478 notistack-MuiContent notistack-MuiContent-error go167266335 go3651055292 go3162094071']//div[@id='notistack-snackbar']")]
        private IWebElement FocDynamicInfo_Textinput_dublicaterecord_Error;

        [FindsBy(How = How.XPath, Using = "(//div[@class='MuiDataGrid-cell MuiDataGrid-cell--textRight'])[position()=1]")]
        private IWebElement FocDynamicInfo_ID;

        [FindsBy(How = How.XPath, Using = "//div[@class='go1888806478 notistack-MuiContent notistack-MuiContent-success go167266335 go1725278324 go3162094071']//div[@id='notistack-snackbar']")]
        private IWebElement Toster_sucessmessage;

        [FindsBy(How = How.XPath, Using = "//div[@class='go1888806478 notistack-MuiContent notistack-MuiContent-success go167266335 go1725278324 go3162094071']//div[@id='notistack-snackbar']")]
        private IWebElement FocDynamicInfo_recordUpdate_sucessmessage;

        [FindsBy(How = How.XPath, Using = "//div[@class='go1888806478 notistack-MuiContent notistack-MuiContent-success go167266335 go1725278324 go3162094071']//div[@id='notistack-snackbar']")]
        private IWebElement FocDynamicInfo_recordDelete_sucessmessage;

        [FindsBy(How = How.XPath, Using = "//button[@data-testid='closeDelete']")]
        private IWebElement FocDynamicInfo_Delete_popup_closebutton;

        [FindsBy(How = How.XPath, Using = "//button[@data-testid='closeDelete']")]
        private IWebElement BocDynamicInfo_Delete_popup_Deletebutton;

        [FindsBy(How = How.XPath, Using = "//p[contains(text(),'Update FOC Dynamic Info')]")]
        private IWebElement Update_FOCInfo_popup_HeaderText;

        public void ValidatePageTitle()
        {
            DriverUtil.IsElementPresent(FOCDynamicInfoPage_HeaderText);
        }

        public void ValidateNewFOCDynamicInfoDialogBox()
        {
            DriverUtil.IsElementPresent(NewFocDynamicInfoPopup_HeaderText);
        }

        public void FOCDynamicInfo_HomePage_GridcolParameters(Boolean FOCInfo_homepage_gridcolmns)
        {

            if (FOC_homepage_IDGridcol.Displayed && FOC_homepage_FOCDynamicTextcol.Displayed && FOC_homepage_LastUpdatedcol.Displayed && FOCD_homepage_Actioncol.Displayed)
            {
                FOCInfo_homepage_gridcolmns = true;
            }
            else
            {
                Browser.Close();
            }
        }
        public void AddNewFOCDynamicInfo(string FOCDynamicText)
        {
            //create new FOC dynamic info record verify the add new popup details
            Browser.Click(FOCDynamicInfo_AddNew_button);
            var PopupHeaderText = NewFocDynamicInfoPopup_HeaderText.Text;
            var PopupCancelbuttonText = NewFocDynamicInfoPopup_Cancelbtn.Text;
            var PopupsavebuttonText = NewFocDynamicInfoPopup_Savebtn.Text;
            var PopupFOCDynamicTextField = NewFocDynamicInfoPopup_Textbox.Text;
            Browser.Click(NewFocDynamicInfoPopup_Cancelbtn);
            Browser.Click(FOCDynamicInfo_AddNew_button);
            Browser.Click(NewBocDynamicInfoPopup_Closebtn);
            Browser.Click(FOCDynamicInfo_AddNew_button);
            NewFocDynamicInfoPopup_Textbox.SendKeys(FOCDynamicText.ToString());
            var FOCDynamicText_InputData = NewFocDynamicInfoPopup_Textbox.GetDomAttribute("value");
            Browser.Click(NewFocDynamicInfoPopup_Savebtn);
            Thread.Sleep(4000);
            var Add_FocDynamicInfo_SuccessMessage = FocDynamicInfo_recordAdd_sucessmessage.Text;
            var FOCDynamicInfo_Id = FocDynamicInfo_ID.Text;

            //Search with newly created record
            FOCDynamicInfo_AutoSearch_textbox.Clear();
            FOCDynamicInfo_AutoSearch_textbox.SendKeys(FOCDynamicText_InputData.ToString());
            var Created_FOCInfo_record_details = FocDynamicInfo_created.Text;

            //Validations
            Assert.That(PopupHeaderText, Is.EqualTo("New FOC Dynamic Info"));
            Assert.That(PopupCancelbuttonText, Is.EqualTo("CANCEL"));
            Assert.That(PopupsavebuttonText, Is.EqualTo("SAVE"));
            Assert.That(Add_FocDynamicInfo_SuccessMessage, Does.Contain("FOC Dynamic Info " + FOCDynamicInfo_Id + " added Successfully."));
            Assert.That(Created_FOCInfo_record_details, Does.Contain(FOCDynamicText_InputData));
        }

        public void EditFOCDynamicInfo(string UpdateFOCDynamicText)
        {

            //create new dynamic info record
            Browser.Click(FOCDynamicInfo_AddNew_button);
            NewFocDynamicInfoPopup_Textbox.SendKeys(UpdateFOCDynamicText.ToString());
            var FOCDynamicText_InputData = NewFocDynamicInfoPopup_Textbox.GetDomAttribute("value");
            Browser.Click(NewFocDynamicInfoPopup_Savebtn);
            Thread.Sleep(4000);
            var Add_FocDynamicInfo_SuccessMessage = FocDynamicInfo_recordAdd_sucessmessage.Text;
            var FOCDynamicInfo_Id = FocDynamicInfo_ID.Text;

            //search with newly added record
            FOCDynamicInfo_AutoSearch_textbox.Clear();
            FOCDynamicInfo_AutoSearch_textbox.SendKeys(FOCDynamicText_InputData.ToString());


            //click on Edit icon and verify the popup details and go for Edit
            Browser.Click(FOCDynamicInfo_Edit_Icon_button);
            var EditPopupTextboxfield = UpdateFocDynamicInfoPopup_FOCDynamicTextField.Text;
            var EditPopupIdfield = UpdateFocDynamicInfoPopup_IDField.Text;
            var EditPopupCancelbuttonText = NewFocDynamicInfoPopup_Cancelbtn.Text;
            var EditPopupsavebuttonText = NewFocDynamicInfoPopup_Savebtn.Text;
            var UpdateFOCInfo_popup_HeaderText = Update_FOCInfo_popup_HeaderText.Text;
            NewFocDynamicInfoPopup_Textbox.SendKeys(FOCDynamicText_InputData + "_Updated");
            var updateFOCInfoExpectedUserData = NewFocDynamicInfoPopup_Textbox.GetDomAttribute("value");
            var updateFOCInfoIDfieldvalue = UpdateFocDynamicInfoPopup_IDField.GetDomAttribute("value");
            Browser.Click(NewFocDynamicInfoPopup_Savebtn);
            Thread.Sleep(4000);
            var ActualFocDynmaicInfoUpdatesuccessmessage = FocDynamicInfo_recordUpdate_sucessmessage.Text;

            //after updated record search with same record details
            FOCDynamicInfo_AutoSearch_textbox.Clear();
            FOCDynamicInfo_AutoSearch_textbox.SendKeys(updateFOCInfoExpectedUserData.ToString());
            var UpdatedFocInfoActualData = FocDynamicInfo_Updated.Text;

            //validations
            Assert.That(Add_FocDynamicInfo_SuccessMessage, Does.Contain("FOC Dynamic Info " + FOCDynamicInfo_Id + " added Successfully."));
            Assert.That(EditPopupTextboxfield, Is.EqualTo("FOC Dynamic Text"));
            Assert.That(EditPopupIdfield, Does.Contain("ID:"));
            Assert.That(EditPopupCancelbuttonText, Is.EqualTo("CANCEL"));
            Assert.That(EditPopupsavebuttonText, Is.EqualTo("SAVE"));
            Assert.That(UpdateFOCInfo_popup_HeaderText, Is.EqualTo("Update FOC Dynamic Info"));
            Assert.That(ActualFocDynmaicInfoUpdatesuccessmessage, Is.EqualTo("FOC Dynamic Info " + FOCDynamicInfo_Id + " updated Successfully."));
            Assert.That(UpdatedFocInfoActualData, Is.EqualTo(updateFOCInfoExpectedUserData));


        }


        public void DeleteFOCDynamicInfo(string DeleteFOCDynamicText)
        {
            //create new dynamic info record
            Browser.Click(FOCDynamicInfo_AddNew_button);
            NewFocDynamicInfoPopup_Textbox.SendKeys(DeleteFOCDynamicText.ToString());
            var FOCDynamicText_InputData = NewFocDynamicInfoPopup_Textbox.GetDomAttribute("value");
            Browser.Click(NewFocDynamicInfoPopup_Savebtn);
            Thread.Sleep(4000);
            var Add_FocDynamicInfo_SuccessMessage = FocDynamicInfo_recordAdd_sucessmessage.Text;
            var FOCDynamicInfo_Id = FocDynamicInfo_ID.Text;

            //search with newly added record
            FOCDynamicInfo_AutoSearch_textbox.Clear();
            FOCDynamicInfo_AutoSearch_textbox.SendKeys(FOCDynamicText_InputData.ToString());

            //click on delete icon and verify the popup details and go for delete
            Browser.Click(FOCDynamicInfo_Detete_Icon_button);
            var Deletepopup_headertext = FOCDynamicInfo_Detete_popup_HeaderText.Text;
            var DeletePopupCancelbuttonText = DeleteFocDynamicInfoPopup_Cancelbtn.Text;
            var DeletePopupDeletebuttonText = DeleteFocDynamicInfoPopup_Deletebtn.Text;
            var Deletewarningmessage = FOCDynamicInfo_Detete_popup_warningmessage.Text;
            var deleteexpectedwarningmessage = Deletewarningmessage.ToString();
            Browser.Click(FocDynamicInfo_Delete_popup_closebutton);
            Browser.Click(FOCDynamicInfo_Detete_Icon_button);
            Browser.Click(FOCDynamicInfo_Detete_popup_Cancelbutton);
            Browser.Click(FOCDynamicInfo_Detete_Icon_button);
            Browser.Click(DeleteFocDynamicInfoPopup_Deletebtn);
            Thread.Sleep(4000);
            var Actualdeletedrecordsuccessmessage = FocDynamicInfo_recordDelete_sucessmessage.Text;

            //after deleted record search with same record details
            FOCDynamicInfo_AutoSearch_textbox.Clear();
            FOCDynamicInfo_AutoSearch_textbox.SendKeys(FOCDynamicText_InputData.ToString());
            var afterdeletedrecorddetails = DeleteFocDynamicInfoPopup_deletedrecorddetails.Text;


            //validations
            Assert.That(Add_FocDynamicInfo_SuccessMessage, Does.Contain("FOC Dynamic Info " + FOCDynamicInfo_Id + " added Successfully."));
            Assert.That(Deletepopup_headertext, Is.EqualTo("Delete"));
            Assert.That(DeletePopupCancelbuttonText, Is.EqualTo("CANCEL"));
            Assert.That(DeletePopupDeletebuttonText, Is.EqualTo("DELETE"));
            Assert.That(Deletewarningmessage, Does.Contain(deleteexpectedwarningmessage));
            Assert.That(Actualdeletedrecordsuccessmessage, Does.Contain("FOC Dynamic Info " + FOCDynamicInfo_Id + " deleted Successfully."));
        }

        public void ViewandSearchFOCDynamicInfo(string ViewSearchFOCDynamicText)
        {
            //verify the grid colomns displayed or not
            FOCDynamicInfo_HomePage_GridcolParameters(true);

            //create new dynamic info record
            Browser.Click(FOCDynamicInfo_AddNew_button);
            NewFocDynamicInfoPopup_Textbox.SendKeys(ViewSearchFOCDynamicText.ToString());
            var BOCDynamicText_InputData = NewFocDynamicInfoPopup_Textbox.GetDomAttribute("value");
            Browser.Click(NewFocDynamicInfoPopup_Savebtn);
            Thread.Sleep(4000);
            var Add_BocDynamicInfo_SuccessMessage = FocDynamicInfo_recordAdd_sucessmessage.Text;
            var FOCDynamicInfo_Id = FocDynamicInfo_ID.Text;

            //search and view with newly created record details with valid Text
            FOCDynamicInfo_AutoSearch_textbox.Clear();
            FOCDynamicInfo_AutoSearch_textbox.SendKeys(BOCDynamicText_InputData.ToString());
            var Search_withvalid_Text = FocDynamicInfo_created.Text;

            //search and view with newly created record details with Valid ID
            FOCDynamicInfo_AutoSearch_textbox.Clear();
            FOCDynamicInfo_AutoSearch_textbox.SendKeys(FocDynamicInfo_ID.ToString());
            var Search_Valid_Id = FocDynamicInfo_created_ID.Text;

            //search and view with Invalid Text details
            FOCDynamicInfo_AutoSearch_textbox.Clear();
            FOCDynamicInfo_AutoSearch_textbox.SendKeys(BOCDynamicText_InputData + "@#$%^&*(HG");
            var Search_Invalid_Text = Search_View_InvalidRecordDetails.Text;

            //search and view with Invalid Id details
            FOCDynamicInfo_AutoSearch_textbox.Clear();
            FOCDynamicInfo_AutoSearch_textbox.SendKeys(BOCDynamicText_InputData + "1234");
            var Search_Invalid_Id = Search_View_InvalidRecordDetails.Text;



            //validations
            Assert.That(BOCDynamicText_InputData, Does.Contain(Search_withvalid_Text));
            Assert.That(Search_Valid_Id, Is.EqualTo(FOCDynamicInfo_Id));
            Assert.That(Search_Invalid_Text, Does.Contain("No results found."));
            Assert.That(Search_Invalid_Text, Does.Contain("No results found."));
        }


        public void ValidationsFOCDynamicInfo(string NegativeScenariosFOCDynamicText)
        {

            //create new dynamic info record
            Browser.Click(FOCDynamicInfo_AddNew_button);
            NewFocDynamicInfoPopup_Textbox.SendKeys(NegativeScenariosFOCDynamicText.ToString());
            var BOCDynamicText_InputData = NewFocDynamicInfoPopup_Textbox.GetDomAttribute("value");
            Browser.Click(NewFocDynamicInfoPopup_Savebtn);
            Thread.Sleep(4000);

            //Scenario1 : Without Input text data
            Browser.Click(FOCDynamicInfo_AddNew_button);
            Browser.Click(NewFocDynamicInfoPopup_Savebtn);
            Thread.Sleep(4000);
            var AddFOC_withoutText_ValidationMessage = FocDynamicInfo_Addwithouttext_Error.Text;

            //Scenario 2: Exceed TextBox input char limits
            NewFocDynamicInfoPopup_Textbox.SendKeys(NegativeScenariosFOCDynamicText + "morethan100characters");
            Browser.Click(NewFocDynamicInfoPopup_Savebtn);
            //Note: Application not allows morethan 100 char in textbox so unable to get the error message and locator
            var AddFOC_TextInput_ExceedLimit_ValidationMessage = FocDynamicInfo_Textinput_exceedcharlimit_error.Text;

            //Scenario 3: Dublicate Record creates
            NewFocDynamicInfoPopup_Textbox.Clear();
            NewFocDynamicInfoPopup_Textbox.SendKeys(BOCDynamicText_InputData);
            Browser.Click(NewFocDynamicInfoPopup_Savebtn);
            Thread.Sleep(4000);
            var AddFOC_TextInput_dublicaterecord_ValidationMessage = FocDynamicInfo_Textinput_dublicaterecord_Error.Text;

            //validations
            Assert.That(AddFOC_withoutText_ValidationMessage, Does.Contain("FOC Dynamic Text is required."));
            //Assert.That(AddFOC_TextInput_ExceedLimit_ValidationMessage, Does.Contain("FOC dynamic info field must not exceed 100 characters"));
            Assert.That(AddFOC_TextInput_dublicaterecord_ValidationMessage, Does.Contain("Sorry, Data validation failed.This FOC Dynamic Info already exists!"));
        }
    }
}

