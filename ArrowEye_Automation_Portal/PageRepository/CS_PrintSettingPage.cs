using System;
using System.IO;
using System.Threading;
using ArrowEye_Automation_Framework;
using ArrowEye_Automation_Framework.Common;
using ArrowEye_Automation_Portal.PageRepository.Objects;
using NUnit.Framework;
using OpenQA.Selenium;
using SeleniumExtras.PageObjects;

namespace ArrowEye_Automation_Portal.PageRepository

{
    public class CS_PrintSettingPage : TestBase
    {
        [FindsBy(How = How.XPath, Using = "//div[contains(text(),'Search or Select')]")]
        public IWebElement SearchOrSelect;

        public static string PclDynamic = "//td[contains(.,'{0}')]";

        public static string clientGallery = "//p[contains(.,'{0}')]";

        public static string clientGallerySubMenuItem = "//li[@data-testid='subMenuItems']//p[contains(text(),'{0}')]";

        public static string clientGalleryMenuItem = "//li[@data-testid='nestedMenuItem']//p[contains(text(),'{0}')]";

        public static string printsettingsfield = "//div[text()='{0}']";

        public static string addNewButton = "//p[contains(text(),'{0}')]";

        public static string popupheaderText = "//p[text()='{0}']";

        public static string popupbtn = "//button[text()='{0}']";

        public static string inputfield = "//label[text()='{0}']/following-sibling::div/input";

        [FindsBy(How = How.XPath, Using = "(//div[@class='MuiDataGrid-cell MuiDataGrid-cell--textRight'])[position()=1]")]
        private IWebElement Print_Setting_Id;

        [FindsBy(How = How.XPath, Using = "//div[@id='notistack-snackbar']")]
        private IWebElement sucessmessage;

        [FindsBy(How = How.XPath, Using = "//button[@aria-label='Export']")]
        private IWebElement exportButton;

        [FindsBy(How = How.XPath, Using = "//li[text()='Download as CSV']")]
        private IWebElement downloadCSVFile;
        
        [FindsBy(How = How.XPath, Using = "//td[@data-testid='table-pagination']//p[2]")]
        private IWebElement rowsCount;

        [FindsBy(How = How.XPath, Using = "//button[@aria-label='Delete']")]
        private IWebElement Delete_Icon_button;

        [FindsBy(How = How.XPath, Using = "//button[@aria-label='Edit']")]
        private IWebElement Edit_Icon_button;

        private string tableCellPath = "//div[@data-rowindex='{0}']/div[@data-colindex='{1}']";

        private string radioInput = "//label[@data-testid='{0}']";

        private string searchInput = "(//div[@aria-rowindex='{0}']//input)[{0}]";

        private string dropDown = "(//li[text()='{0}'])[1]";

        private string errormessage = "//p[text()='{0}']";

        private string duplicateKeyErrorMessage = "//li[contains(text(),'{0}')]";

        public void NavigateToPrintSettings(string pclID = "9006: Pier 2")
        {
            DriverUtilities.Click(SearchOrSelect);
            Browser.ClickDynamicElement(PclDynamic, pclID);
            Browser.ClickDynamicElement(clientGallery, "CLIENT GALLERY");
            Browser.ClickDynamicElement(clientGalleryMenuItem, "Client Settings");
            Browser.ClickDynamicElement(clientGallerySubMenuItem, "Print Settings");
        }

        public void ExportPrintSettings()
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

        public void ValidatePrintSettingFeild()
        {
            WebElement printsettingsIDELe = Browser.getDynamicElement(printsettingsfield, "ID");
            WebElement printsettingKeyEle = Browser.getDynamicElement(printsettingsfield, "Key");
            WebElement printsettingDescriptionELe = Browser.getDynamicElement(printsettingsfield, "Description");
            WebElement printsettingValueTypeEle = Browser.getDynamicElement(printsettingsfield, "Value Type");
            WebElement printsettingValueEle = Browser.getDynamicElement(printsettingsfield, "Value");
            Browser.isElementPresent(printsettingsIDELe);
            Browser.isElementPresent(printsettingKeyEle);
            Browser.isElementPresent(printsettingDescriptionELe);
            Browser.isElementPresent(printsettingValueTypeEle);
            Browser.isElementPresent(printsettingValueEle);
        }

        public void AddNewPrintSettingRecord(string key)
        {
            //Delete Key if already present
            searchAndDeleteRecordBasedOnKeyName(key);

            //create new Print Settings record
            Browser.ClickDynamicElement(addNewButton, "Add New");

            validatePopupDetails("New Print Setting");

            Thread.Sleep(2000);
            Browser.getDynamicElement(inputfield, "Key").SendKeys(key);
            Browser.getDynamicElement(dropDown, key).Click();

            PrintSettingsRecord record = new PrintSettingsRecord()
            {
                Key = key,
                Description = Browser.getDynamicElement(inputfield, "Description").GetAttribute("value")
            };
            populateBasedOnValueType(record);
            
            Browser.ClickDynamicElement(popupbtn, "Save");

            validatePopupMessage("Print Settings {0} Added Successfully.", "Print Settings {0} Updated Successfully.");

            validateRecord(record);
        }

        public void EditPrintSettingRecord(string key)
        {
            AddNewPrintSettingRecord(key);

            var id = Browser.getDynamicElement(tableCellPath, "0", "1").Text;
            Browser.Click(Edit_Icon_button);

            Thread.Sleep(2000);

            //validate pop up
            validatePopupDetails("Edit Print Setting");

            PrintSettingsRecord record = new PrintSettingsRecord()
            {
                Key = key,
                Description = Browser.getDynamicElement(inputfield, "Description").GetAttribute("value")
            };

            //update
            editBasedOnValueType(record);

            //save
            Browser.Click(Browser.getDynamicElement(popupbtn, "Save"));

            validatePopupMessage("Print Settings {0} Updated Successfully.");

            Thread.Sleep(2000);
            validateRecord(record);
        }

        public void DeletePrintSettingRecord()
        {
            var id = Browser.getDynamicElement(tableCellPath, "0", "1").Text;
            Browser.Click(Delete_Icon_button);
            Browser.ClickDynamicElement(popupheaderText, "Delete");
            Browser.WaitForElement(sucessmessage, 10);
            var updateSuccessMessage = sucessmessage.Text;

            string idvalue = id.Replace(",", "");
            Assert.That(updateSuccessMessage, Does.Contain("Print Settings " + idvalue + " Deleted Successfully."));
        }

        public void validatePrintSettings(string key)
        {
            //Delete Key if already present
            searchAndDeleteRecordBasedOnKeyName(key);

            //create new Print Settings record
            Browser.ClickDynamicElement(addNewButton, "Add New");

            validatePopupDetails("New Print Setting");

            Thread.Sleep(2000);            

            Browser.ClickDynamicElement(popupbtn, "Save");

            var valueFieldErrorText = Browser.getDynamicElement(errormessage, "Value is required.").GetText();
            var keyFieldErrorText = Browser.getDynamicElement(errormessage, "Key is required.").GetText();

            Assert.That(keyFieldErrorText, Does.Contain("Key is required."));

            Assert.That(valueFieldErrorText, Does.Contain("Value is required."));

            Browser.getDynamicElement(inputfield, "Key").SendKeys(key);
            Browser.getDynamicElement(dropDown, key).Click();
            PrintSettingsRecord record = new PrintSettingsRecord()
            {
                Key = key,
                Description = Browser.getDynamicElement(inputfield, "Description").GetAttribute("value")
            };
            populateBasedOnValueType(record);

            Browser.ClickDynamicElement(popupbtn, "Save");

            Thread.Sleep(2000);

            //Try to create Print Settings record with same key
            Browser.ClickDynamicElement(addNewButton, "Add New");
            Browser.getDynamicElement(inputfield, "Key").SendKeys(key);
            Browser.getDynamicElement(dropDown, key).Click();
            record = new PrintSettingsRecord()
            {
                Key = key,
                Description = Browser.getDynamicElement(inputfield, "Description").GetAttribute("value")
            };
            populateBasedOnValueType(record);

            Browser.ClickDynamicElement(popupbtn, "Save");
            Thread.Sleep(2000);
            var duplicateKeyFieldErrorText = Browser.getDynamicElement(duplicateKeyErrorMessage, "Given Combination of PclId and KeyId already exists").GetText();

            Assert.That(duplicateKeyFieldErrorText, Does.Contain("Given Combination of PclId and KeyId already exists"));
        }

        private void populateBasedOnValueType(PrintSettingsRecord record)
        {
            var valueType = Browser.getDynamicElement(inputfield, "Value Type").GetAttribute("value");
            record.ValueType = valueType;
            switch (valueType)
            {
                case "string":
                    //Program Profile Test
                    WebElement valueEle = Browser.getDynamicElement(inputfield, "Value");
                    valueEle.SendKeys("Test");
                    record.Value = "Test";
                    break;
                case "bit":
                    //cssbodytextcolor
                    Browser.getDynamicElement(radioInput, "YesInput").Click();
                    record.Value = "Yes";
                    break;

                case "float":
                    //cltnotifyhttp_get:allcomments
                    WebElement floatValueEle = Browser.getDynamicElement(inputfield, "Value");
                    floatValueEle.SendKeys("255.00");
                    record.Value = "255.00";
                    break;
                case "int":
                    //cssheadertextfont
                    WebElement intValueEle = Browser.getDynamicElement(inputfield, "Value");
                    intValueEle.SendKeys("1234");
                    record.Value = "1234";
                    break;
            }
        }

        private void editBasedOnValueType(PrintSettingsRecord record)
        {
            var valueType = Browser.getDynamicElement(inputfield, "Value Type").GetAttribute("value");
            record.ValueType = valueType;
            switch (valueType)
            {
                case "string":
                    //Program Profile Test
                    WebElement valueEle = Browser.getDynamicElement(inputfield, "Value");
                    DriverUtilities.clearText(valueEle);
                    valueEle.SendKeys("Test_Updated");
                    record.Value = "Test_Updated";
                    break;
                case "bit":
                    //cssbodytextcolor
                    Browser.getDynamicElement(radioInput, "NoInput").Click();
                    record.Value = "No";
                    break;

                case "float":
                    //cltnotifyhttp_get:allcomments
                    WebElement floatValueEle = Browser.getDynamicElement(inputfield, "Value");
                    DriverUtilities.clearText(floatValueEle);
                    floatValueEle.SendKeys("522.00");
                    record.Value = "522.00";
                    break;
                case "int":
                    //cssheadertextfont
                    WebElement intValueEle = Browser.getDynamicElement(inputfield, "Value");
                    DriverUtilities.clearText(intValueEle);
                    intValueEle.SendKeys("4321");
                    record.Value = "4321";
                    break;
            }
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

        private void validatePopupMessage(String message1, String message2)
        {
            Browser.WaitForElement(sucessmessage, 10);
            var successMessage = sucessmessage.Text;
            Thread.Sleep(2000);
            var id = Browser.getDynamicElement(tableCellPath, "0", "1").Text;
            string idvalue = id.Replace(",", "");
            string expectedMessage1 = String.Format(message1, idvalue);
            string expectedMessage2 = String.Format(message2, idvalue);
            Assert.That(successMessage, Is.EqualTo(expectedMessage1).Or.EqualTo(expectedMessage2));
        }

        private void validatePopupMessage(String message)
        {
            Browser.WaitForElement(sucessmessage, 10);
            var updateSuccessMessage = sucessmessage.Text;
            Thread.Sleep(2000);
            var id = Browser.getDynamicElement(tableCellPath, "0", "1").Text;
            string idvalue = id.Replace(",", "");
            string expectedMessage = String.Format(message, idvalue);
            Assert.That(updateSuccessMessage, Does.Contain(expectedMessage));
        }

        private void validateRecord(PrintSettingsRecord record)
        {
            Assert.That(record.Key, Does.Contain(Browser.getDynamicElement(tableCellPath, "0", "2").Text));
            Assert.That(record.Description, Does.Contain(Browser.getDynamicElement(tableCellPath, "0", "3").Text));
            Assert.That(record.ValueType, Does.Contain(Browser.getDynamicElement(tableCellPath, "0", "4").Text));
            Assert.That(record.Value, Does.Contain(Browser.getDynamicElement(tableCellPath, "0", "5").Text));
        }

        private void searchAndDeleteRecordBasedOnKeyName(string keyName)
        {
            Browser.getDynamicElement(searchInput, "2").SendKeys(keyName);
            bool elementPresent = false;
            try
            {
                Browser.getDynamicElement(tableCellPath, "0", "2");
                elementPresent = true;
            }
            catch (NoSuchElementException ex)
            {
                //Ignore
            }
            if (elementPresent)
            {
                DeletePrintSettingRecord();
            }
            
        }
    }
}