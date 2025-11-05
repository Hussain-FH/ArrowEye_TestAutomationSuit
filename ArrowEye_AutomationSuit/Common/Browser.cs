using OpenQA.Selenium.Firefox;
using OpenQA.Selenium;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Configuration;
using OpenQA.Selenium.Chrome;
using System.Threading;
using System.Security.Policy;
using OpenQA.Selenium.Support.UI;
using static OpenQA.Selenium.BiDi.Modules.Script.RemoteValue;
using System.IO;
using OpenQA.Selenium.BiDi.Modules.BrowsingContext;

namespace ArrowEye_Automation_Framework.Common
{
    public class Browser
    {
        public IWebDriver WebDriver { get; set; }
        public string environmentURL { get; set; }

        private static IWebDriver webDriver;

        public static string downloadsFolderPath;

        public Browser(IWebDriver webDriver)
        {
            //environmentURL = AppNameHelper.appBaseURL;
            WebDriver = webDriver;
        }

        public static void Initialize()
        {
            string downloadsPath = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.UserProfile), "Downloads");
            downloadsFolderPath = downloadsPath + @"\automation";

            if (Directory.Exists(downloadsFolderPath))
            {
                Directory.Delete(downloadsFolderPath, true);
            }
            Directory.CreateDirectory(downloadsFolderPath);

            // Create ChromeOptions instance
            ChromeOptions options = new ChromeOptions();

            // Set the download directory
            options.AddUserProfilePreference("download.default_directory", downloadsFolderPath);
            options.AddUserProfilePreference("download.prompt_for_download", false);
            options.AddUserProfilePreference("profile.default_content_settings.popups", 0);            
            webDriver = new ChromeDriver(options);
            webDriver.Url = AppNameHelper.appBaseURL;
            Thread.Sleep(5000);
            webDriver.Manage().Window.Maximize();
        }

        public static string Title
        {
            get { return webDriver.Title; }
        }

        public static string CurrentURL
        {
            get { return webDriver.Url; }
        }

        public static ISearchContext Driver
        {
            get { return webDriver; }
        }

        public static IWebDriver _Driver
        {
            get { return webDriver; }
        }

        public static void Click(IWebElement element)
        {
            Thread.Sleep(3000);
            element.Click();
        }

        public static void GetText(IWebElement element, string text)
        {
            Thread.Sleep(3000);
            text = element.Text;
        }

        public static void Close()
        {
            
             webDriver.Close();
            //webDriver.Quit();
        }

        public static void ClickDynamicElement(string xpath, string text)
        {
            try
            {
                xpath = string.Format(xpath, text);
                var element = webDriver.FindElement(By.XPath(xpath));
                Click(element);
            }
            catch (Exception)
            {
                throw;
            }
        }

        public static WebElement getDynamicElement(string xpath, string text)
        {
            xpath = string.Format(xpath, text);
            var element = webDriver.FindElement(By.XPath(xpath));
            return (WebElement)element;
        }

        public static WebElement getDynamicElement(string xpath, string text1, string text2)
        {
            xpath = string.Format(xpath, text1, text2);
            var element = webDriver.FindElement(By.XPath(xpath));
            return (WebElement)element;
        }

        public static bool isElementPresent( WebElement element)
        {
            try
            {
                // Check if the element is displayed
                return element != null && element.Displayed;
            }
            catch (NoSuchElementException e)
            {
                // Element is not found
                return false;
            }
            catch (Exception e)
            {
               
                return false;
            }
        }

        public static bool WaitForElement(IWebElement element, int timeoutInSeconds)
        {
            try
            {
                // Create a WebDriverWait instance with the provided timeout
                WebDriverWait wait = new WebDriverWait(webDriver, TimeSpan.FromSeconds(timeoutInSeconds));

                // Wait until the element is visible (i.e., displayed)
                // wait.Until(d => element.Displayed);

                wait.Until(d =>
                {
                    try
                    {
                        // If the element is found, return it
                        return element.Displayed ? element : null;
                    }
                    catch (NoSuchElementException)
                    {
                        // Ignore NoSuchElementException and continue waiting
                        return null;
                    }
                    catch (StaleElementReferenceException)
                    {
                        // Ignore StaleElementReferenceException and continue waiting
                        return null;
                    }
                });            

                return true; // Element is visible
            }
            catch (WebDriverTimeoutException)
            {
                Console.WriteLine("Timeout: Element not found or not visible.");
                return false; // Element not found within the timeout
            }
            catch (Exception ex)
            {
                Console.WriteLine($"An error occurred: {ex.Message}");
                return false;
            }
        }

        public static void SelectDropdownValue(IWebElement dropdownElement, string valueToSelect, string selectBy = "text")
        {
            try
            {
                // Wait for the dropdown to be visible
                //WebDriverWait wait = new WebDriverWait(webDriver, TimeSpan.FromSeconds(10));
                //IWebElement dropdownElement = wait.Until(SeleniumExtras.WaitHelpers.ExpectedConditions.ElementIsVisible(By.XPath(dropdownLocator)));

                // Create a SelectElement object for the dropdown
                SelectElement dropdown = new SelectElement(dropdownElement);

                // Select by the specified method
                if (selectBy == "text")
                {
                    dropdown.SelectByText(valueToSelect);
                }
                else if (selectBy == "value")
                {
                    dropdown.SelectByValue(valueToSelect);
                }
                else if (selectBy == "index")
                {
                    if (int.TryParse(valueToSelect, out int index))
                    {
                        dropdown.SelectByIndex(index);
                    }
                    else
                    {
                        throw new ArgumentException("Value for index should be a valid integer.");
                    }
                }
                else
                {
                    throw new ArgumentException("Invalid 'selectBy' argument. Use 'text', 'value', or 'index'.");
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine("Error while selecting value from dropdown: " + ex.Message);
            }
        }
    }
}

