using SeleniumExtras.PageObjects;
using OpenQA.Selenium;
using ArrowEye_Automation_Framework.Common;
using NUnit.Framework;
using ArrowEye_Automation_Framework;
using System.Threading;

namespace ArrowEye_Automation_Portal.PageRepository
{
    public class CP_CSPSettings_BOCDynamicInfoPage : TestBase
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

        [FindsBy(How = How.XPath, Using = "//div[@class='jss13 MuiBox-root css-0']//p[contains(text(),'BOC Dynamic Info')]")]
        private IWebElement BOCDynamicInfoPage_HeaderText;

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
        private IWebElement BOCDynamicInfo_AutoSearch_textbox;

        [FindsBy(How = How.XPath, Using = "//button[@class='MuiButtonBase-root MuiIconButton-root MuiIconButton-sizeSmall css-irzi55']")]
        private IWebElement BOCDynamicInfo_AutoSearch_textboxclear;

        [FindsBy(How = How.XPath, Using = "//button[@data-testid='openModal']//p[contains(text(),'Add New')]")]
        private IWebElement BOCDynamicInfo_AddNew_button;

        [FindsBy(How = How.XPath, Using = "(//button[@data-testid='DeleteButton'])[position()=1]")]

        private IWebElement BOCDynamicInfo_Detete_Icon_button;

        [FindsBy(How = How.XPath, Using = "//button[@data-testid='cancelButton']")]

        private IWebElement BOCDynamicInfo_Detete_popup_Cancelbutton;

        [FindsBy(How = How.XPath, Using = "//h2[@class='MuiTypography-root MuiTypography-h6 MuiDialogTitle-root jss24 css-ohyacs' and contains(text(),'Delete')]")]

        private IWebElement BOCDynamicInfo_Detete_popup_HeaderText;

        [FindsBy(How = How.XPath, Using = "//p[@class='MuiTypography-root MuiTypography-body1 jss25 css-q3n577']")]

        private IWebElement BOCDynamicInfo_Detete_popup_warningmessage;

        [FindsBy(How = How.XPath, Using = "(//button[@data-testid='editIcon'])[position()=1]")]
        private IWebElement BOCDynamicInfo_Edit_Icon_button;

        [FindsBy(How = How.XPath, Using = "//p[contains(text(),'New BOC Dynamic Info')]")]
        private IWebElement NewBocDynamicInfoPopup_HeaderText;

        [FindsBy(How = How.XPath, Using = "//button[@data-testid='closeButton']")]
        private IWebElement NewBocDynamicInfoPopup_Closebtn;

        [FindsBy(How = How.XPath, Using = "//input[@data-testid='nameInputField']")]
        private IWebElement NewBocDynamicInfoPopup_Textbox;

        [FindsBy(How = How.XPath, Using = "//button[@data-testid='cancelCSP']")]
        private IWebElement NewBocDynamicInfoPopup_Cancelbtn;

        [FindsBy(How = How.XPath, Using = "//p[@class='MuiTypography-root MuiTypography-body1 css-qr1enj']")]
        private IWebElement UpdateBocDynamicInfoPopup_IDField;

        [FindsBy(How = How.XPath, Using = "//label[contains(text(),'BOC Dynamic Text')]")]
        private IWebElement UpdateBocDynamicInfoPopup_BOCDynamicTextField;

        [FindsBy(How = How.XPath, Using = "//button[@data-testid='cancelButton']")]
        private IWebElement DeleteBocDynamicInfoPopup_Cancelbtn;

        [FindsBy(How = How.XPath, Using = "//button[@data-testid='okButton']//p[contains(text(),'Delete')]")]
        private IWebElement DeleteBocDynamicInfoPopup_Deletebtn;

        [FindsBy(How = How.XPath, Using = "//div[@class='MuiDataGrid-overlay css-14349d1'][contains(text(),'No results found.')]")]
        private IWebElement DeleteBocDynamicInfoPopup_deletedrecorddetails;

        [FindsBy(How = How.XPath, Using = "//div[@class='MuiDataGrid-overlayWrapperInner css-0']//div[contains(text(),'No results found.')]")]
        private IWebElement Search_View_InvalidRecordDetails;

        [FindsBy(How = How.XPath, Using = "//button[@data-testid='saveCSP']")]
        private IWebElement NewBocDynamicInfoPopup_Savebtn;

        [FindsBy(How = How.XPath, Using = "//p[@id=':rkm:-helper-text' and contains(text(),'BOC Dynamic Text is required.')]")]
        private IWebElement NewBocDynamicInfoPopup_validationmessage;

        [FindsBy(How = How.XPath, Using = "(//div[@data-colindex='2'])[position()=1]")]
        private IWebElement BocDynamicInfo_created;

        [FindsBy(How = How.XPath, Using = "(//div[@data-colindex='2'])[position()=1]")]
        private IWebElement BocDynamicInfo_Updated;

        [FindsBy(How = How.XPath, Using = "//div[@class='go1888806478 notistack-MuiContent notistack-MuiContent-success go167266335 go1725278324 go3162094071']//div[@id='notistack-snackbar']")]
        private IWebElement BocDynamicInfo_recordAdd_sucessmessage;

        [FindsBy(How = How.XPath, Using = "(//div[@class='MuiDataGrid-cell MuiDataGrid-cell--textRight'])[position()=1]")]
        private IWebElement BocDynamicInfo_ID;

        [FindsBy(How = How.XPath, Using = "//div[@class='go1888806478 notistack-MuiContent notistack-MuiContent-success go167266335 go1725278324 go3162094071']//div[@id='notistack-snackbar']")]
        private IWebElement Toster_sucessmessage;

        [FindsBy(How = How.XPath, Using = "//div[@class='go1888806478 notistack-MuiContent notistack-MuiContent-success go167266335 go1725278324 go3162094071']//div[@id='notistack-snackbar']")]
        private IWebElement BocDynamicInfo_recordUpdate_sucessmessage;

        [FindsBy(How = How.XPath, Using = "//div[@class='go1888806478 notistack-MuiContent notistack-MuiContent-success go167266335 go1725278324 go3162094071']//div[@id='notistack-snackbar']")]
        private IWebElement BocDynamicInfo_recordDelete_sucessmessage;

        [FindsBy(How = How.XPath, Using = "//button[@data-testid='closeDelete']")]
        private IWebElement BocDynamicInfo_Delete_popup_closebutton;

        [FindsBy(How = How.XPath, Using = "//button[@data-testid='closeDelete']")]
        private IWebElement BocDynamicInfo_Delete_popup_Deletebutton;

        [FindsBy(How = How.XPath, Using = "//p[contains(text(),'Update BOC Dynamic Info')]")]
        private IWebElement Update_BOCInfo_popup_HeaderText;

        public void ValidatePageTitle()
        {
            DriverUtil.IsElementPresent(BOCDynamicInfoPage_HeaderText);
        }

        public void ValidateNewIssuerDialogBox()
        {
            DriverUtil.IsElementPresent(NewBocDynamicInfoPopup_HeaderText);
        }

        public void AddNewBOCDynamicInfo(string BOCDynamicText)
        {
            //create new dynamic info record verify the add new popup details
            Browser.Click(BOCDynamicInfo_AddNew_button);
            var PopupHeaderText = NewBocDynamicInfoPopup_HeaderText.Text;
            var PopupCancelbuttonText = NewBocDynamicInfoPopup_Cancelbtn.Text;
            var PopupsavebuttonText = NewBocDynamicInfoPopup_Savebtn.Text;
            Browser.Click(NewBocDynamicInfoPopup_Cancelbtn);
            Browser.Click(BOCDynamicInfo_AddNew_button);
            Browser.Click(NewBocDynamicInfoPopup_Closebtn);
            Browser.Click(BOCDynamicInfo_AddNew_button);
            NewBocDynamicInfoPopup_Textbox.SendKeys(BOCDynamicText.ToString());
            var BOCDynamicText_InputData = NewBocDynamicInfoPopup_Textbox.GetDomAttribute("value");
            Browser.Click(NewBocDynamicInfoPopup_Savebtn);
            Thread.Sleep(4000);
            //CP_Pages.TosterMessage_wait();
            var Add_BocDynamicInfo_SuccessMessage = BocDynamicInfo_recordAdd_sucessmessage.Text;
            var BOCDynamicInfo_Id = BocDynamicInfo_ID.Text;
            //Search with newly created record
            BOCDynamicInfo_AutoSearch_textbox.Clear();
            BOCDynamicInfo_AutoSearch_textbox.SendKeys(BOCDynamicText_InputData.ToString());
            var Created_BOCInfo_record_details = BocDynamicInfo_created.Text;

            //Validations
            Assert.That(PopupHeaderText, Is.EqualTo("New BOC Dynamic Info"));
            Assert.That(PopupCancelbuttonText, Is.EqualTo("CANCEL"));
            Assert.That(PopupsavebuttonText, Is.EqualTo("SAVE"));
            Assert.That(Add_BocDynamicInfo_SuccessMessage, Does.Contain("BOC Dynamic Info " + BOCDynamicInfo_Id + " added Successfully."));
            Assert.That(Created_BOCInfo_record_details, Does.Contain(BOCDynamicText_InputData));
        }

        public void EditBOCDynamicInfo(string UpdateBOCDynamicText)
        {
            //create new dynamic info record
            Browser.Click(BOCDynamicInfo_AddNew_button);
            NewBocDynamicInfoPopup_Textbox.SendKeys(UpdateBOCDynamicText.ToString());
            var BOCDynamicText_InputData = NewBocDynamicInfoPopup_Textbox.GetDomAttribute("value");
            Browser.Click(NewBocDynamicInfoPopup_Savebtn);
            Thread.Sleep(4000);
            var Add_BocDynamicInfo_SuccessMessage = BocDynamicInfo_recordAdd_sucessmessage.Text;
            var BOCDynamicInfo_Id = BocDynamicInfo_ID.Text;
            //search with newly added record
            BOCDynamicInfo_AutoSearch_textbox.Clear();
            BOCDynamicInfo_AutoSearch_textbox.SendKeys(BOCDynamicText_InputData.ToString());

            //click on Edit icon and verify the popup details and go for Edit
            Browser.Click(BOCDynamicInfo_Edit_Icon_button);
            var EditPopupTextboxfield = UpdateBocDynamicInfoPopup_BOCDynamicTextField.Text;
            var EditPopupIdfield = UpdateBocDynamicInfoPopup_IDField.Text;
            var EditPopupCancelbuttonText = NewBocDynamicInfoPopup_Cancelbtn.Text;
            var EditPopupsavebuttonText = NewBocDynamicInfoPopup_Savebtn.Text;
            var UpdateBOCInfo_popup_HeaderText = Update_BOCInfo_popup_HeaderText.Text;
            NewBocDynamicInfoPopup_Textbox.SendKeys(BOCDynamicText_InputData + "_Updated");
            var updateBOCInfoExpectedUserData = NewBocDynamicInfoPopup_Textbox.GetDomAttribute("value");
            var updateBOCInfoIDfieldvalue = UpdateBocDynamicInfoPopup_IDField.GetDomAttribute("value");
            Browser.Click(NewBocDynamicInfoPopup_Savebtn);
            Thread.Sleep(4000);
            var ActualBocDynmaicInfoUpdatesuccessmessage = BocDynamicInfo_recordUpdate_sucessmessage.Text;

            //after updated record search with same record details
            BOCDynamicInfo_AutoSearch_textbox.Clear();
            BOCDynamicInfo_AutoSearch_textbox.SendKeys(updateBOCInfoExpectedUserData.ToString());
            var UpdatedBocInfoActualData = BocDynamicInfo_Updated.Text;

            //validations
            Assert.That(Add_BocDynamicInfo_SuccessMessage, Does.Contain("BOC Dynamic Info " + BOCDynamicInfo_Id + " added Successfully."));
            Assert.That(EditPopupTextboxfield, Is.EqualTo("BOC Dynamic Text"));
            Assert.That(EditPopupIdfield, Does.Contain("ID:"));
            Assert.That(EditPopupCancelbuttonText, Is.EqualTo("CANCEL"));
            Assert.That(EditPopupsavebuttonText, Is.EqualTo("SAVE"));
            Assert.That(UpdateBOCInfo_popup_HeaderText, Is.EqualTo("Update BOC Dynamic Info"));
            Assert.That(ActualBocDynmaicInfoUpdatesuccessmessage, Is.EqualTo("BOC Dynamic Info " + BOCDynamicInfo_Id + " updated Successfully."));
            Assert.That(UpdatedBocInfoActualData, Is.EqualTo(updateBOCInfoExpectedUserData));
        }

        public void DeleteBOCDynamicInfo(string DeleteBOCDynamicText)
        {
            //create new dynamic info record
            Browser.Click(BOCDynamicInfo_AddNew_button);
            NewBocDynamicInfoPopup_Textbox.SendKeys(DeleteBOCDynamicText.ToString());
            var BOCDynamicText_InputData = NewBocDynamicInfoPopup_Textbox.GetDomAttribute("value");
            Browser.Click(NewBocDynamicInfoPopup_Savebtn);
            Thread.Sleep(4000);
            var Add_BocDynamicInfo_SuccessMessage = BocDynamicInfo_recordAdd_sucessmessage.Text;
            var BOCDynamicInfo_Id = BocDynamicInfo_ID.Text;
            //search with newly added record
            BOCDynamicInfo_AutoSearch_textbox.Clear();
            BOCDynamicInfo_AutoSearch_textbox.SendKeys(BOCDynamicText_InputData.ToString());

            //click on delete icon and verify the popup details and go for delete
            Browser.Click(BOCDynamicInfo_Detete_Icon_button);
            var Deletepopup_headertext = BOCDynamicInfo_Detete_popup_HeaderText.Text;
            var DeletePopupCancelbuttonText = DeleteBocDynamicInfoPopup_Cancelbtn.Text;
            var DeletePopupDeletebuttonText = DeleteBocDynamicInfoPopup_Deletebtn.Text;
            var Deletewarningmessage = BOCDynamicInfo_Detete_popup_warningmessage.Text;
            var deleteexpectedwarningmessage = Deletewarningmessage.ToString();
            Browser.Click(BocDynamicInfo_Delete_popup_closebutton);
            Browser.Click(BOCDynamicInfo_Detete_Icon_button);
            Browser.Click(BOCDynamicInfo_Detete_popup_Cancelbutton);
            Browser.Click(BOCDynamicInfo_Detete_Icon_button);
            Browser.Click(DeleteBocDynamicInfoPopup_Deletebtn);
            Thread.Sleep(4000);
            var Actualdeletedrecordsuccessmessage = BocDynamicInfo_recordDelete_sucessmessage.Text;

            //after deleted record search with same record details
            BOCDynamicInfo_AutoSearch_textbox.Clear();
            BOCDynamicInfo_AutoSearch_textbox.SendKeys(BOCDynamicText_InputData.ToString());
            var afterdeletedrecorddetails = DeleteBocDynamicInfoPopup_deletedrecorddetails.Text;

            //validations
            Assert.That(Add_BocDynamicInfo_SuccessMessage, Does.Contain("BOC Dynamic Info " + BOCDynamicInfo_Id + " added Successfully."));
            Assert.That(Deletepopup_headertext, Is.EqualTo("Delete"));
            Assert.That(DeletePopupCancelbuttonText, Is.EqualTo("CANCEL"));
            Assert.That(DeletePopupDeletebuttonText, Is.EqualTo("DELETE"));
            Assert.That(Deletewarningmessage, Does.Contain(deleteexpectedwarningmessage));
            Assert.That(Actualdeletedrecordsuccessmessage, Does.Contain("BOC Dynamic Info " + BOCDynamicInfo_Id + " deleted Successfully."));
        }

        public void ViewandSearchBOCDynamicInfo(string ViewSearchBOCDynamicText)
        {
            //create new dynamic info record
            Browser.Click(BOCDynamicInfo_AddNew_button);
            NewBocDynamicInfoPopup_Textbox.SendKeys(ViewSearchBOCDynamicText.ToString());
            var BOCDynamicText_InputData = NewBocDynamicInfoPopup_Textbox.GetDomAttribute("value");
            Browser.Click(NewBocDynamicInfoPopup_Savebtn);
            Thread.Sleep(4000);
            var Add_BocDynamicInfo_SuccessMessage = BocDynamicInfo_recordAdd_sucessmessage.Text;
            var BOCDynamicInfo_Id = BocDynamicInfo_ID.Text;

            //search and view with newly created record details
            BOCDynamicInfo_AutoSearch_textbox.Clear();
            BOCDynamicInfo_AutoSearch_textbox.SendKeys(ViewSearchBOCDynamicText.ToString());
            var Created_BOCInfo_record_details = BocDynamicInfo_created.Text;

            //search and view with Invalid  record details
            BOCDynamicInfo_AutoSearch_textbox.Clear();
            BOCDynamicInfo_AutoSearch_textbox.SendKeys(ViewSearchBOCDynamicText.ToString() + "InvalidRecords_Search");
            Thread.Sleep(4000);
            var invalidsearchresult = Search_View_InvalidRecordDetails.Text;

            //validations
            Assert.That(Add_BocDynamicInfo_SuccessMessage, Does.Contain("BOC Dynamic Info " + BOCDynamicInfo_Id + " added Successfully."));
            Assert.That(invalidsearchresult, Is.EqualTo("No results found."));
            Assert.That(BOCDynamicText_InputData, Does.Contain(Created_BOCInfo_record_details));
        }
    }
}

