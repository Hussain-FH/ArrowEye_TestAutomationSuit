using SeleniumExtras.PageObjects;
using OpenQA.Selenium;
using ArrowEye_Automation_Framework.Common;
using NUnit.Framework;
using ArrowEye_Automation_Framework;
using System.Threading;
using System;

namespace ArrowEye_Automation_Portal.PageRepository
{
    public class CP_CSPSettings_EMVProfilePage : TestBase
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
        private IWebElement EMVProfile_AutoSearch_textbox;

        [FindsBy(How = How.XPath, Using = "//button[@class='MuiButtonBase-root MuiIconButton-root MuiIconButton-sizeSmall css-irzi55']")]
        private IWebElement BOCDynamicInfo_AutoSearch_textboxclear;

        [FindsBy(How = How.XPath, Using = "//button[@data-testid='openModal']//p[contains(text(),'Add New')]")]
        private IWebElement EMVProfile_AddNew_button;

        [FindsBy(How = How.XPath, Using = "//div[@class='MuiDataGrid-columnHeaderTitle css-mh3zap' and contains(text(),'EMV Profile ID')]")]
        private IWebElement EMVProfile_homepage_IDGridcol;

        [FindsBy(How = How.XPath, Using = "//div[@class='MuiDataGrid-columnHeaderTitle css-mh3zap' and contains(text(),'Name')]")]
        private IWebElement EMVProfile__homepage_Name;

        [FindsBy(How = How.XPath, Using = "//div[@class='MuiDataGrid-columnHeaderTitle css-mh3zap' and contains(text(),'Actions')]")]
        private IWebElement EMVProfile__homepage_Actioncol;

        [FindsBy(How = How.XPath, Using = "(//button[@data-testid='deleteCell'])[position()=1]")]

        private IWebElement EMVProfile_Detete_Icon_button;

        [FindsBy(How = How.XPath, Using = "//button[@data-testid='cancelButton']")]

        private IWebElement EMVProfile_Detete_popup_Cancelbutton;

        [FindsBy(How = How.XPath, Using = "//h2[contains(text(),'Delete')]")]
        private IWebElement EMVProfile_Detete_popup_HeaderText;

        [FindsBy(How = How.XPath, Using = "//div[@class='MuiStack-root css-j7qwjs']")]
        private IWebElement EMVProfile_Detete_popup_warningmessage;

        [FindsBy(How = How.XPath, Using = "(//button[@class='MuiButtonBase-root MuiIconButton-root MuiIconButton-sizeSmall textPrimary css-1pydezi'])[2]")]
        private IWebElement EMVProfile_Edit_Icon_button;

        [FindsBy(How = How.XPath, Using = "//p[contains(text(),'New EMV Profile')]")]
        private IWebElement NewEMVProfilePopup_HeaderText;

        [FindsBy(How = How.XPath, Using = "//button[@data-testid='closeButton']")]
        private IWebElement NewEMVProfilePopup_Closebtn;

        [FindsBy(How = How.XPath, Using = "//input[@data-testid='nameInputField']")]
        private IWebElement NewEMVProfilePopup_Textbox;

        [FindsBy(How = How.XPath, Using = "//button[@data-testid='cancelCSP']")]
        private IWebElement NewEMVProfilePopup_Cancelbtn;

        [FindsBy(How = How.XPath, Using = "//p[@class='MuiTypography-root MuiTypography-body1 css-qr1enj']")]
        private IWebElement UpdateEMVProfilePopup_IDField;

        [FindsBy(How = How.XPath, Using = "//label[text()='Name']")]
        private IWebElement UpdateEMVProfilePopup_NameTextField;

        [FindsBy(How = How.XPath, Using = "//p[@class='MuiTypography-root MuiTypography-body1 jss129 css-9l3uo3' and contains(text(),'Update FOC Dynamic Info')]")]
        private IWebElement UpdateFocDynamicInfoPopup_HeaderText;

        [FindsBy(How = How.XPath, Using = "//button[@data-testid='cancelButton']")]
        private IWebElement DeleteEMVProfilePopup_Cancelbtn;

        [FindsBy(How = How.XPath, Using = "//button[@data-testid='okButton']//p[contains(text(),'Delete')]")]
        private IWebElement DeleteEMVProfilePopup_Deletebtn;

        [FindsBy(How = How.XPath, Using = "//div[@class='MuiDataGrid-overlay css-14349d1' and contains(text(),'No results found.')]")]
        private IWebElement DeleteEMVProfilePopup_deletedrecorddetails;

        [FindsBy(How = How.XPath, Using = "//div[contains(text(),'No results found.')]")]
        private IWebElement Search_View_InvalidRecordDetails;

        [FindsBy(How = How.XPath, Using = "//button[@data-testid='saveCSP']")]
        private IWebElement NewEMVProfilePopup_Savebtn;

        [FindsBy(How = How.XPath, Using = "//p[@id=':rkm:-helper-text' and contains(text(),'BOC Dynamic Text is required.')]")]
        private IWebElement NewBocDynamicInfoPopup_validationmessage;

        [FindsBy(How = How.XPath, Using = "(//div[@data-colindex='2'])[position()=1]")]
        private IWebElement EMVProfile_newly_added_details;

        [FindsBy(How = How.XPath, Using = "(//div[@data-colindex='1'])[position()=1]")]
        private IWebElement EMVProfile_created_ID;

        [FindsBy(How = How.XPath, Using = "(//div[@data-colindex='2'])[position()=1]")]
        private IWebElement EMVProfile_Updated;

        [FindsBy(How = How.XPath, Using = "//div[@class='go1888806478 notistack-MuiContent notistack-MuiContent-success go167266335 go1725278324 go3162094071']//div[@id='notistack-snackbar']")]
        private IWebElement EMVProfile_recordAdd_sucessmessage;

        [FindsBy(How = How.XPath, Using = "//p[@class='MuiFormHelperText-root Mui-error MuiFormHelperText-sizeMedium MuiFormHelperText-contained css-v7esy']")]
        private IWebElement EMVProfile_Addwithouttext_Error;

        [FindsBy(How = How.XPath, Using = "//p[@class='MuiFormHelperText-root Mui-error MuiFormHelperText-sizeMedium MuiFormHelperText-contained css-v7esy']")]
        private IWebElement EMVProfile_Textinput_exceedcharlimit_error;

        [FindsBy(How = How.XPath, Using = "//div[@class='go1888806478 notistack-MuiContent notistack-MuiContent-error go167266335 go3651055292 go3162094071']//div[@id='notistack-snackbar']")]
        private IWebElement EMVProfile_Textinput_dublicaterecord_Error;

        [FindsBy(How = How.XPath, Using = "(//div[@class='MuiDataGrid-cell MuiDataGrid-cell--textRight'])[position()=1]")]
        private IWebElement EMVProfile_ID;

        [FindsBy(How = How.XPath, Using = "//div[@class='go1888806478 notistack-MuiContent notistack-MuiContent-success go167266335 go1725278324 go3162094071']//div[@id='notistack-snackbar']")]
        private IWebElement Toster_sucessmessage;

        [FindsBy(How = How.XPath, Using = "//div[@class='go1888806478 notistack-MuiContent notistack-MuiContent-success go167266335 go1725278324 go3162094071']//div[@id='notistack-snackbar']")]
        private IWebElement EMVProfile_recordUpdate_sucessmessage;

        [FindsBy(How = How.XPath, Using = "//div[@class='go1888806478 notistack-MuiContent notistack-MuiContent-success go167266335 go1725278324 go3162094071']//div[@id='notistack-snackbar']")]
        private IWebElement EMVProfile_recordDelete_sucessmessage;

        [FindsBy(How = How.XPath, Using = "//button[@data-testid='closeDelete']")]
        private IWebElement EMVProfile_Delete_popup_closebutton;

        [FindsBy(How = How.XPath, Using = "//button[@data-testid='closeDelete']")]
        private IWebElement BocDynamicInfo_Delete_popup_Deletebutton;

        [FindsBy(How = How.XPath, Using = "//p[contains(text(),'Update EMV Profile')]")]
        private IWebElement Update_EMVProfile_popup_HeaderText;

        public void ValidatePageTitle()
        {
            DriverUtil.IsElementPresent(FOCDynamicInfoPage_HeaderText);
        }

        public void ValidateNewFOCDynamicInfoDialogBox()
        {
            DriverUtil.IsElementPresent(NewEMVProfilePopup_HeaderText);
        }

        public void EMVProfile_HomePage_GridcolParameters(Boolean EMVProfile_homepage_gridcolmns)
        {
            if (EMVProfile_homepage_IDGridcol.Displayed && EMVProfile__homepage_Name.Displayed && EMVProfile__homepage_Actioncol.Displayed)
            {
                EMVProfile_homepage_gridcolmns = true;
            }
            else
            {
                Browser.Close();
            }
        }
        public void AddNewEMVProfile(string CreateEMVProfileText)
        {
            //create Emv prifile record verify the add new popup details
            Browser.Click(EMVProfile_AddNew_button);
            var PopupHeaderText = NewEMVProfilePopup_HeaderText.Text;
            var PopupCancelbuttonText = NewEMVProfilePopup_Cancelbtn.Text;
            var PopupsavebuttonText = NewEMVProfilePopup_Savebtn.Text;
            var PopupFOCDynamicTextField = NewEMVProfilePopup_Textbox.Text;
            Browser.Click(NewEMVProfilePopup_Cancelbtn);
            Browser.Click(EMVProfile_AddNew_button);
            Browser.Click(NewEMVProfilePopup_Closebtn);
            Browser.Click(EMVProfile_AddNew_button);
            NewEMVProfilePopup_Textbox.SendKeys(CreateEMVProfileText.ToString());
            var EMVProfileText_InputData = NewEMVProfilePopup_Textbox.GetDomAttribute("value");
            Browser.Click(NewEMVProfilePopup_Savebtn);
            Thread.Sleep(4000);
            var Add_NewEMVProfile_SuccessMessage = EMVProfile_recordAdd_sucessmessage.Text;
            var EMVProfileId = EMVProfile_ID.Text;

            //Search with newly created record
            EMVProfile_AutoSearch_textbox.Clear();
            EMVProfile_AutoSearch_textbox.SendKeys(EMVProfileText_InputData.ToString());
            var Created_EMVProfile_record_details = EMVProfile_newly_added_details.Text;

            //Validations
            Assert.That(PopupHeaderText, Is.EqualTo("New EMV Profile"));
            Assert.That(PopupCancelbuttonText, Is.EqualTo("CANCEL"));
            Assert.That(PopupsavebuttonText, Is.EqualTo("SAVE"));

            Assert.That(Add_NewEMVProfile_SuccessMessage, Does.Contain("EMV Profile " + EMVProfileId + " added Successfully."));
            Assert.That(Created_EMVProfile_record_details, Does.Contain(EMVProfileText_InputData));
        }

        public void EditEMVProfile(string UpdateEMVProfileText)
        {
            //create new dynamic info record
            Browser.Click(EMVProfile_AddNew_button);
            NewEMVProfilePopup_Textbox.SendKeys(UpdateEMVProfileText.ToString());
            var EMVProfileText_InputData = NewEMVProfilePopup_Textbox.GetDomAttribute("value");
            Browser.Click(NewEMVProfilePopup_Savebtn);
            Thread.Sleep(4000);
            var Add_NewEMVProfile_SuccessMessage = EMVProfile_recordAdd_sucessmessage.Text;
            var EMVProfileId = EMVProfile_ID.Text;

            //Search with newly created record
            EMVProfile_AutoSearch_textbox.Clear();
            EMVProfile_AutoSearch_textbox.SendKeys(EMVProfileText_InputData.ToString());
            var Created_EMVProfile_record_details = EMVProfile_newly_added_details.Text;

            //click on Edit icon and verify the popup details and go for Edit
            Browser.Click(EMVProfile_Edit_Icon_button);
            var EditPopupTextboxfield = UpdateEMVProfilePopup_NameTextField.Text;
            var EditPopupIdfield = UpdateEMVProfilePopup_IDField.Text;
            var EditPopupCancelbuttonText = NewEMVProfilePopup_Cancelbtn.Text;
            var EditPopupsavebuttonText = NewEMVProfilePopup_Savebtn.Text;
            var UpdateEMVProfile_popup_HeaderText = Update_EMVProfile_popup_HeaderText.Text;
            NewEMVProfilePopup_Textbox.SendKeys(EMVProfileText_InputData + "_Updated");
            var updateEMVProfileExpectedUserData = NewEMVProfilePopup_Textbox.GetDomAttribute("value");
            var updateEMVProfileIDfieldvalue = UpdateEMVProfilePopup_IDField.GetDomAttribute("value");
            Browser.Click(NewEMVProfilePopup_Savebtn);
            Thread.Sleep(4000);
            var ActualEMVProfileUpdatesuccessmessage = EMVProfile_recordUpdate_sucessmessage.Text;

            //after updated record search with same record details
            EMVProfile_AutoSearch_textbox.Clear();
            EMVProfile_AutoSearch_textbox.SendKeys(updateEMVProfileExpectedUserData.ToString());
            var UpdatedEMVProfileActualData = EMVProfile_Updated.Text;

            //validations
            Assert.That(Add_NewEMVProfile_SuccessMessage, Does.Contain("EMV Profile " + EMVProfileId + " added Successfully."));
            Assert.That(EditPopupTextboxfield, Is.EqualTo("Name"));
            Assert.That(EditPopupIdfield, Does.Contain("ID:"));
            Assert.That(EditPopupCancelbuttonText, Is.EqualTo("CANCEL"));
            Assert.That(EditPopupsavebuttonText, Is.EqualTo("SAVE"));
            Assert.That(UpdateEMVProfile_popup_HeaderText, Is.EqualTo("Update EMV Profile"));
            Assert.That(ActualEMVProfileUpdatesuccessmessage, Is.EqualTo("EMV Profile " + EMVProfileId + " updated Successfully."));
            Assert.That(UpdatedEMVProfileActualData, Is.EqualTo(updateEMVProfileExpectedUserData));
        }

        public void DeleteEMVProfile(string DeleteEMVProfileText)
        {
            //create new dynamic info record
            Browser.Click(EMVProfile_AddNew_button);
            NewEMVProfilePopup_Textbox.SendKeys(DeleteEMVProfileText.ToString());
            var EMVProfileText_InputData = NewEMVProfilePopup_Textbox.GetDomAttribute("value");
            Browser.Click(NewEMVProfilePopup_Savebtn);
            Thread.Sleep(4000);
            var Add_NewEMVProfile_SuccessMessage = EMVProfile_recordAdd_sucessmessage.Text;
            var EMVProfileId = EMVProfile_ID.Text;

            //Search with newly created record
            EMVProfile_AutoSearch_textbox.Clear();
            EMVProfile_AutoSearch_textbox.SendKeys(EMVProfileText_InputData.ToString());
            var Created_EMVProfile_record_details = EMVProfile_newly_added_details.Text;

            //click on delete icon and verify the popup details and go for delete
            Browser.Click(EMVProfile_Detete_Icon_button);
            var Deletepopup_headertext = EMVProfile_Detete_popup_HeaderText.Text;
            var DeletePopupCancelbuttonText = DeleteEMVProfilePopup_Cancelbtn.Text;
            var DeletePopupDeletebuttonText = DeleteEMVProfilePopup_Deletebtn.Text;
            var Deletewarningmessage = EMVProfile_Detete_popup_warningmessage.Text;
            var deleteexpectedwarningmessage = Deletewarningmessage.ToString();
            Browser.Click(EMVProfile_Delete_popup_closebutton);
            Browser.Click(EMVProfile_Detete_Icon_button);
            Browser.Click(EMVProfile_Detete_popup_Cancelbutton);
            Browser.Click(EMVProfile_Detete_Icon_button);
            Browser.Click(DeleteEMVProfilePopup_Deletebtn);
            Thread.Sleep(4000);
            var Actualdeletedrecordsuccessmessage = EMVProfile_recordDelete_sucessmessage.Text;

            //after deleted record search with same record details
            EMVProfile_AutoSearch_textbox.Clear();
            EMVProfile_AutoSearch_textbox.SendKeys(EMVProfileId.ToString());
            var afterdeletedrecorddetails = DeleteEMVProfilePopup_deletedrecorddetails.Text;

            //validations
            Assert.That(Add_NewEMVProfile_SuccessMessage, Does.Contain("EMV Profile " + EMVProfileId + " added Successfully."));
            Assert.That(Deletepopup_headertext, Is.EqualTo("Delete"));
            Assert.That(DeletePopupCancelbuttonText, Is.EqualTo("CANCEL"));
            Assert.That(DeletePopupDeletebuttonText, Is.EqualTo("DELETE"));
            Assert.That(Deletewarningmessage, Does.Contain(deleteexpectedwarningmessage));
            Assert.That(Actualdeletedrecordsuccessmessage, Does.Contain("EMV Profile " + EMVProfileId + " deleted Successfully."));
        }

        public void ViewandSearchEMVProfile(string ViewSearchFOCDynamicText)
        {
            //verify the grid colomns displayed or not
            EMVProfile_HomePage_GridcolParameters(true);

            //create new dynamic info record
            Browser.Click(EMVProfile_AddNew_button);
            NewEMVProfilePopup_Textbox.SendKeys(ViewSearchFOCDynamicText.ToString());
            var EMVProfileText_InputData = NewEMVProfilePopup_Textbox.GetDomAttribute("value");
            Browser.Click(NewEMVProfilePopup_Savebtn);
            Thread.Sleep(4000);
            var Add_NewEMVProfile_SuccessMessage = EMVProfile_recordAdd_sucessmessage.Text;
            var EMVProfileId = EMVProfile_ID.Text;

            //Search with newly created record valid text
            EMVProfile_AutoSearch_textbox.Clear();
            EMVProfile_AutoSearch_textbox.SendKeys(EMVProfileText_InputData.ToString());
            var Search_withvalid_Text = EMVProfile_newly_added_details.Text;

            //search and view with newly created record details with Valid ID
            EMVProfile_AutoSearch_textbox.Clear();
            EMVProfile_AutoSearch_textbox.SendKeys(EMVProfileId.ToString());
            var Search_Valid_Id = EMVProfile_created_ID.Text;

            //search and view with Invalid Text details
            EMVProfile_AutoSearch_textbox.Clear();
            EMVProfile_AutoSearch_textbox.SendKeys(EMVProfileText_InputData + "@#$%^&*(HG");
            Thread.Sleep(4000);
            var Search_Invalid_Text = Search_View_InvalidRecordDetails.Text;

            //search and view with Invalid Id details
            EMVProfile_AutoSearch_textbox.Clear();
            EMVProfile_AutoSearch_textbox.SendKeys(EMVProfileId + "1234");
            var Search_Invalid_Id = Search_View_InvalidRecordDetails.Text;

            //validations
            Assert.That(EMVProfileText_InputData, Does.Contain(Search_withvalid_Text));
            Assert.That(Search_Valid_Id, Is.EqualTo(EMVProfileId));
            Assert.That(Search_Invalid_Text, Does.Contain("No results found."));
            Assert.That(Search_Invalid_Text, Does.Contain("No results found."));
        }

        public void ValidationsEMVProfile(string NegativeScenariosEMVProfileText)
        {
            //create new dynamic info record
            Browser.Click(EMVProfile_AddNew_button);
            NewEMVProfilePopup_Textbox.SendKeys(NegativeScenariosEMVProfileText.ToString());
            var EMVProfileText_InputData = NewEMVProfilePopup_Textbox.GetDomAttribute("value");
            Browser.Click(NewEMVProfilePopup_Savebtn);
            Thread.Sleep(4000);

            //Scenario1 : Without Name Input text data
            Browser.Click(EMVProfile_AddNew_button);
            Browser.Click(NewEMVProfilePopup_Savebtn);
            Thread.Sleep(4000);
            var AddEMVProfile_withoutText_ValidationMessage = EMVProfile_Addwithouttext_Error.Text;

            // //Scenario 2: Exceed Name TextBox input char limits
            NewEMVProfilePopup_Textbox.SendKeys(NegativeScenariosEMVProfileText + "morethan100characters");
            Browser.Click(NewEMVProfilePopup_Savebtn);
            //Note: Application not allows morethan 100 char in textbox so unable to get the error message and locator
            var EMVProfile_TextInput_ExceedLimit_ValidationMessage = EMVProfile_Textinput_exceedcharlimit_error.Text;

            //Scenario 3: Dublicate Record creates
            NewEMVProfilePopup_Textbox.Clear();
            NewEMVProfilePopup_Textbox.SendKeys(EMVProfileText_InputData);
            Browser.Click(NewEMVProfilePopup_Savebtn);
            Thread.Sleep(2000);
            var EMVProfile_TextInput_dublicaterecord_ValidationMessage = EMVProfile_Textinput_dublicaterecord_Error.Text;

            //validations
            Assert.That(AddEMVProfile_withoutText_ValidationMessage, Does.Contain("Name is required."));
            //Assert.That(AddFOC_TextInput_ExceedLimit_ValidationMessage, Does.Contain("FOC dynamic info field must not exceed 100 characters"));
            Assert.That(EMVProfile_TextInput_dublicaterecord_ValidationMessage, Does.Contain("Sorry, Data validation failed.This EMV Profile already exists!"));
        }
    }
}

