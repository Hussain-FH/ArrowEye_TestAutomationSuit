using System;
using System.IO;
using System.Threading;
using ArrowEye_Automation_Framework;
using ArrowEye_Automation_Framework.Common;
using ArrowEye_Automation_Portal.PageRepository.Objects;
using NUnit.Framework;
using OpenQA.Selenium;
using RandomString4Net;
using SeleniumExtras.PageObjects;

namespace ArrowEye_Automation_Portal.PageRepository.EMV

{
    public class EMV_Scripts_Page : TestBase
    {

        [FindsBy(How = How.XPath, Using = "//div[contains(text(),'Search or Select')]")]
        public IWebElement SearchOrSelect;

        [FindsBy(How = How.XPath, Using = "//p[contains(text(),'CLIENT GALLERY')]")]
        public IWebElement clientGallery;

        public static string PclDynamic = "//td[contains(.,'{0}')]";

        public static string clientGalleryMenuItem = "//li[@data-testid='nestedMenuItem']//p[contains(text(),'{0}')]";

        public static string clientGallerySubMenuItem = "//li[@data-testid='subMenuItems']//p[contains(text(),'{0}')]";

        [FindsBy(How = How.XPath, Using = "//label[text()='Name']/following-sibling::div/input")]
        public IWebElement inputName_EVM;

        [FindsBy(How = How.XPath, Using = "//label[text()='Description']/following-sibling::div/input")]
        public IWebElement descriptionEVM;

        [FindsBy(How = How.XPath, Using = "//label[text()='Profile Name']/following-sibling::div/input")]
        public IWebElement profileName_EVM;

        [FindsBy(How = How.XPath, Using = "//button[text()='Cancel']")]
        private IWebElement NewEMVPopup_Cancelbtn;

        [FindsBy(How = How.XPath, Using = "//button[text()='Save']")]
        private IWebElement NewEMVPopup_Savebtn;

        [FindsBy(How = How.XPath, Using = "//p[text()='New Script']")]
        private IWebElement NewEMVPopup_HeaderText;

        public static string cancelButtonIcon_EVM = "//*[name()='svg'and @data-testid='closeButtonIcon']";

        [FindsBy(How = How.XPath, Using = "//input[@placeholder='Search…']")]
        private IWebElement EMV_AutoSearch_textbox;

        [FindsBy(How = How.XPath, Using = "(//div[@data-colindex='2'])[position()=1]")]
        private IWebElement EMVScripts_created;

        [FindsBy(How = How.XPath, Using = "//div[@id='notistack-snackbar']")]
        private IWebElement EMV_recordAdd_sucessmessage;

        [FindsBy(How = How.XPath, Using = "(//button[@data-testid='edit'])[1]")]
        private IWebElement EMV_Script_Edit_Icon_button;

        [FindsBy(How = How.XPath, Using = "(//div[@class='MuiDataGrid-cell MuiDataGrid-cell--textRight'])[position()=1]")]
        private IWebElement EMV_Script_Id;

        [FindsBy(How = How.XPath, Using = "//button[@aria-label='Export']")]
        private IWebElement exportButton;

        [FindsBy(How = How.XPath, Using = "//li[text()='Download as CSV']")]
        private IWebElement downloadCSVFile;

        [FindsBy(How = How.XPath, Using = "//td[@data-testid='table-pagination']//p[2]")]
        private IWebElement rowsCount;

        [FindsBy(How = How.XPath, Using = "//button[@aria-label='Delete']")]
        private IWebElement Delete_Icon_button;

        [FindsBy(How = How.XPath, Using = "//div[@id='notistack-snackbar']")]
        private IWebElement sucessmessage;

        public static string headerText = "//p[text()='{0}']";

        public static string cancel = "//button[text()='{0}']";

        public static string save = "//button[text()='{0}']";

        public static string errormessage = "//p[text()='{0}']";

        public static string scriptfield = "//div[text()='{0}']";

        private string tableCellPath = "//div[@data-rowindex='{0}']/div[@data-colindex='{1}']";

        public static string addNewButton = "//p[contains(text(),'{0}')]";

        public static string popupheaderText = "//p[text()='{0}']";

        public static string popupbtn = "//button[text()='{0}']";

        public static string inputfield = "//label[text()='{0}']/following-sibling::div/input";

        public static string sizeLimitField = "//label[text()='{0}']/following-sibling::p";

        public static string dialogueBoxErrorMessage = "//p[text()='{0}']/following-sibling::div//li";

        public void NavigateToEMVSettings(string pclID = "9006: Pier 2")
        {
            DriverUtilities.Click(SearchOrSelect);
            Browser.ClickDynamicElement(PclDynamic, pclID);
            DriverUtilities.Click(clientGallery);
            Browser.ClickDynamicElement(clientGalleryMenuItem, "EMV");
            Browser.ClickDynamicElement(clientGallerySubMenuItem, "Scripts");
        }

        public void ValidateEMVScriptsFeild()
        {
            WebElement scriptIDELe = Browser.getDynamicElement(scriptfield, "ID");
            WebElement scriptNameEle = Browser.getDynamicElement(scriptfield, "Name");
            WebElement scriptDescriptionELe = Browser.getDynamicElement(scriptfield, "Description");
            WebElement scriptProfileNameELe = Browser.getDynamicElement(scriptfield, "Profile Name");
            WebElement actionsELe = Browser.getDynamicElement(scriptfield, "Actions");
        }

        private void validatePopupMessage(String message)
        {
            Browser.WaitForElement(EMV_recordAdd_sucessmessage, 10);
            var EMVScriptuccessMessage = EMV_recordAdd_sucessmessage.Text;
            Thread.Sleep(2000);
            var id = Browser.getDynamicElement(tableCellPath, "0", "1").Text;
            string idvalue = id.Replace(",", "");
            string expectedMessage = String.Format(message, idvalue);
            Assert.That(EMVScriptuccessMessage, Does.Contain(expectedMessage));
        }

        private void validateRecord(BcssConfigurationRecord record)
        {
            Assert.That(record.Name, Does.Contain(Browser.getDynamicElement(tableCellPath, "0", "2").Text));
            Assert.That(record.Description, Does.Contain(Browser.getDynamicElement(tableCellPath, "0", "3").Text));
            Assert.That(record.ProfileName, Does.Contain(Browser.getDynamicElement(tableCellPath, "0", "4").Text));
        }

        private void validatePopupDetails(String headerMessage)
        {
            WebElement headerEle = Browser.getDynamicElement(popupheaderText, headerMessage);
            WebElement cancelEle = Browser.getDynamicElement(popupbtn, "Cancel");
            WebElement saveEle = Browser.getDynamicElement(popupbtn, "Save");

            Assert.That(headerEle.Text, Is.EqualTo(headerMessage));
            Assert.That(cancelEle.Text, Is.EqualTo("CANCEL"));
            Assert.That(saveEle.Text, Is.EqualTo("SAVE"));
        }

        public void AddNewEMVScriptRecord(string Text)
        {
            //create new Print Settings record
            Browser.ClickDynamicElement(addNewButton, "Add New");
            WebElement PopupHeaderText = Browser.getDynamicElement(popupheaderText, "New Script");
            WebElement cancelText = Browser.getDynamicElement(popupbtn, "Cancel");
            WebElement saveText = Browser.getDynamicElement(popupbtn, "Save");

            //validate pop up
            validatePopupDetails("New Script");

            Browser.getDynamicElement(inputfield, "Name").SendKeys(Text);
            Browser.getDynamicElement(inputfield, "Description").SendKeys(Text);
            Browser.getDynamicElement(inputfield, "Profile Name").SendKeys(Text);
            Browser.ClickDynamicElement(popupbtn, "Save");
            Thread.Sleep(2000);

            //Validations
            BcssConfigurationRecord record = new BcssConfigurationRecord()
            {
                Name = Text,
                Description = Text,
                ProfileName = Text
            };
            validatePopupMessage("EMV Personalization Script {0} Added Successfully.");
            validateRecord(record);
        }

        public void EditEMVScriptRecord(string UpdateScriptText)
        {
            //adding new record
            this.AddNewEMVScriptRecord(UpdateScriptText);
            Thread.Sleep(3000);

            //click on Edit icon and verify the popup details and go for Edit
            Browser.Click(EMV_Script_Edit_Icon_button);
            Thread.Sleep(2000);

            //validate pop up
            validatePopupDetails("Update Script");

            //update
            Browser.getDynamicElement(inputfield, "Name").SendKeys("_Updated");
            Browser.getDynamicElement(inputfield, "Description").SendKeys("_Updated");
            Browser.getDynamicElement(inputfield, "Profile Name").SendKeys("_Updated");

            //save
            Browser.Click(Browser.getDynamicElement(popupbtn, "Save"));

            //Validations
            BcssConfigurationRecord record = new BcssConfigurationRecord()
            {
                Name = UpdateScriptText + "_Updated",
                Description = UpdateScriptText + "_Updated",
                ProfileName = UpdateScriptText + "_Updated"
            };
            validatePopupMessage("EMV Personalization Script {0} Updated Successfully.");
            Thread.Sleep(2000);
            validateRecord(record);
        }

        public void ValidateEMVScriptField(string Text)
        {
            //create new EMV Script record
            AddNewEMVScriptRecord(Text);

            Browser.ClickDynamicElement(addNewButton, "Add New");
            inputName_EVM.SendKeys(Text);
            descriptionEVM.SendKeys(Text.ToString());
            Browser.Click(NewEMVPopup_Savebtn);
            Thread.Sleep(500);
            var EMV_nameexists_errorMessage = Browser.getDynamicElement(dialogueBoxErrorMessage, "New Script").Text;
            Assert.That(EMV_nameexists_errorMessage, Does.Contain("EMV Script Service " + Text + " already exists."));
            Browser.Click(NewEMVPopup_Cancelbtn);
            Browser.ClickDynamicElement(addNewButton, "Add New");
            Thread.Sleep(1000);
            Browser.Click(NewEMVPopup_Savebtn);
            WebElement NameFieldErrorValue = Browser.getDynamicElement(errormessage, "Name is required.");
            var NameErrorText = NameFieldErrorValue.GetText();
            WebElement DescriptionErrorValue = Browser.getDynamicElement(errormessage, "Description is required.");
            var DescriptionErrorText = DescriptionErrorValue.GetText();
            Assert.That(NameErrorText, Does.Contain("Name is required."));
            Assert.That(DescriptionErrorText, Does.Contain("Description is required."));
            //Name already exists not done

            //Validate Character Limitation
            validateFieldLength("Name", 50);
            validateFieldLength("Description", 150);
            validateFieldLength("Profile Name", 50);
        }

        public void ExportEMVScriptPages()
        {
            DriverUtilities.Click(exportButton);
            Thread.Sleep(1000);
            Browser.Click(downloadCSVFile);
            Thread.Sleep(3000);
            string[] csvFiles = Directory.GetFiles(Browser.downloadsFolderPath, "*.csv");
            if (csvFiles.Length == 0)
            {
                throw new Exception("No CSV files found in the directory.");
            }
            else
            {
                string rowsCountText = rowsCount.GetText();
                string[] parts = rowsCountText.Split(new string[] { " of " }, StringSplitOptions.None);
                string totalCount = parts[1].Trim();
                string csvRowCount = CountRowsInCsv(csvFiles[0]).ToString();
                Assert.That(totalCount, Is.EqualTo(csvRowCount), "Total count in CSV does not match with total count on web page");
            }
        }

        static int CountRowsInCsv(string filePath)
        {
            int rowCount = 0;
            try
            {
                // Read the CSV file line by line
                using (var reader = new StreamReader(filePath))
                {
                    // Skip the first line (header)
                    if (!reader.EndOfStream)
                    {
                        reader.ReadLine();  // This skips the header line
                    }

                    // Count the remaining rows
                    while (reader.ReadLine() != null)
                    {
                        rowCount++;
                    }
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error reading the file '{filePath}': {ex.Message}");
            }
            return rowCount;
        }

        private void validateFieldLength(String fieldName, int length)
        {
            string longString = RandomString.GetString(Types.ALPHANUMERIC_LOWERCASE, length + 5);
            Browser.getDynamicElement(inputfield, fieldName).SendKeys(longString);
            Assert.That(Browser.getDynamicElement(sizeLimitField, fieldName).Text, Is.EqualTo(length + "/" + length));
        }
    }
}
