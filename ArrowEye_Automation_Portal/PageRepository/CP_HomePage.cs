using SeleniumExtras.PageObjects;
using ArrowEye_Automation_Framework.Common;
using OpenQA.Selenium;
using System.Threading;
using System.Collections.Generic;
using ArrowEye_Automation_Framework;
using System;
using OpenQA.Selenium.Interactions;

namespace ArrowEye_Automation_Portal.PageRepository
{
    public class CP_HomePage :TestBase
    {
        public static string PclDynamic = "//td[contains(.,'{0}')]";
        public static string clientGallery = "//p[contains(.,'{0}')]";
        public static string clientGallerySubMenuItem = "//li[@data-testid='subMenuItems']//p[contains(text(),'{0}')]";
        public static string clientGalleryMenuItem = "//li[@data-testid='nestedMenuItem']//p[contains(text(),'{0}')]";

        [FindsBy(How = How.XPath, Using = "//p[@class='MuiTypography-root MuiTypography-body1 css-9l3uo3']")]
        public IWebElement homePageTitle;

        //[FindsBy(How = How.XPath, Using = "//p[@class='MuiTypography-root MuiTypography-body1 css-1051h91' and contains(text(),'CLIENT GALLERY') and @data-testid='secondNested']")]
        //public IWebElement clientGallery;

        [FindsBy(How = How.XPath, Using = "//p[@class='MuiTypography-root MuiTypography-body1 css-18ahme0' and contains(text(),'EMV')]")]
        public IWebElement emv;

        [FindsBy(How = How.XPath, Using = "//li[@data-testid='nestedMenuItem']//p[contains(text(),'CSP Settings')]")]
        public IWebElement CSPSettings;

        [FindsBy(How = How.XPath, Using = "//li[@data-testid='subMenuItems']//p[contains(text(),'BOC Dynamic Info')]")]
        public IWebElement BOCDynamicInfo;

        [FindsBy(How = How.XPath, Using = "//li[@data-testid='subMenuItems']//p[contains(text(),'Card Holder Agreement')]")]
        public IWebElement CardHolderAgreement;

        [FindsBy(How = How.XPath, Using = "//li[@data-testid='subMenuItems']//p[contains(text(),'Carrier Dynamic Info')]")]
        public IWebElement CarrierDynamicInfo;

        [FindsBy(How = How.XPath, Using = "//li[@data-testid='subMenuItems']//p[contains(text(),'EMV Profile')]")]
        public IWebElement EMVProfile;

        [FindsBy(How = How.XPath, Using = "//li[@data-testid='subMenuItems']//p[contains(text(),'FOC Dynamic Info')]")]
        public IWebElement FOCDynamicInfo;

        [FindsBy(How = How.XPath, Using = "//p[@class='MuiTypography-root MuiTypography-body1 css-9l3uo3' and contains(text(),'Issuers')]")]
        public IWebElement Issuers;

        [FindsBy(How = How.XPath, Using = "//p[@class='MuiTypography-root MuiTypography-body1 css-9l3uo3' and contains(text(),'Scripts')]")]
        public IWebElement Scripts;

        [FindsBy(How = How.XPath, Using = "//div[@class='MuiSelect-select MuiSelect-outlined MuiInputBase-input MuiOutlinedInput-input css-qiwgdb' and contains(text(),'Search or Select')]")]
        public IWebElement SearchOrSelect;

        [FindsBy(How = How.XPath, Using = "//li[@data-testid='subMenuItems']//p[contains(text(),'Bank ID Numbers')]")]
        public IWebElement BankIdNumbers;

        [FindsBy(How = How.XPath, Using = "//li[@data-testid='subMenuItems']//p[contains(text(),'BCSS Configurations')]")]
        public IWebElement BCSSConfigurations;

        [FindsBy(How = How.XPath, Using = "//li[@data-testid='subMenuItems']//p[contains(text(),'Client Profiles')]")]
        public IWebElement ClientProfiles;

        [FindsBy(How = How.XPath, Using = "//li[@data-testid='subMenuItems']//p[contains(text(),'Default Proof Replacements')]")]
        public IWebElement DefaultProofReplacements;

        [FindsBy(How = How.XPath, Using = "//li[@data-testid='subMenuItems']//p[contains(text(),'Embosser Field Maps')]")]
        public IWebElement EmbosserFieldMaps;

        [FindsBy(How = How.XPath, Using = "//li[@data-testid='subMenuItems']//p[contains(text(),'General Settings')]")]
        public IWebElement GeneralSettings;

        [FindsBy(How = How.XPath, Using = "//li[@data-testid='subMenuItems']//p[contains(text(),'Hot Stamps')]")]
        public IWebElement HotStamps;

        [FindsBy(How = How.XPath, Using = "//li[@data-testid='subMenuItems']//p[contains(text(),'Inventory Configuration')]")]
        public IWebElement InventoryConfiguration;

        [FindsBy(How = How.XPath, Using = "//li[@data-testid='subMenuItems']//p[contains(text(),'Issuing Banks')]")]
        public IWebElement IssuingBanks;

        [FindsBy(How = How.XPath, Using = "//li[@data-testid='subMenuItems']//p[contains(text(),'Mag Track Encodings')]")]
        public IWebElement MagTrackEncodings;

        [FindsBy(How = How.XPath, Using = "//li[@data-testid='subMenuItems']//p[contains(text(),'Configure Client')]")]
        public IWebElement ConfigureClient;

        [FindsBy(How = How.XPath, Using = "//li[@data-testid='subMenuItems']//p[contains(text(),'Number Generators')]")]
        public IWebElement NumberGenerators;

        [FindsBy(How = How.XPath, Using = "//li[@data-testid='subMenuItems']//p[contains(text(),'Platforms & Products')]")]
        public IWebElement Platforms_Products;

        [FindsBy(How = How.XPath, Using = "//li[@data-testid='subMenuItems']//p[contains(text(),'Print Tags')]")]
        public IWebElement PrintTags;

        [FindsBy(How = How.XPath, Using = "//li[@data-testid='subMenuItems']//p[contains(text(),'Print Settings')]")]
        public IWebElement PrintSettings;

        [FindsBy(How = How.XPath, Using = "//li[@data-testid='subMenuItems']//p[contains(text(),'Publishing Policies')]")]
        public IWebElement PublishingPolicies;

        [FindsBy(How = How.XPath, Using = "//li[@data-testid='subMenuItems']//p[contains(text(),'Retailer Client Configurations')]")]
        public IWebElement RetailerClient_Configurations;

        [FindsBy(How = How.XPath, Using = "//li[@data-testid='subMenuItems']//p[contains(text(),'Retailer Profiles')]")]
        public IWebElement RetailerProfiles;

        [FindsBy(How = How.XPath, Using = "//li[@data-testid='subMenuItems']//p[contains(text(),'Ship Types')]")]
        public IWebElement ShipTypes;

        [FindsBy(How = How.XPath, Using = "//li[@data-testid='subMenuItems']//p[contains(text(),'SOP Adhoc Key-Values')]")]
        public IWebElement SOPAdhocKeyValues;

        [FindsBy(How = How.XPath, Using = "//li[@data-testid='subMenuItems']//p[contains(text(),'SOP Configurations')]")]
        public IWebElement SOPConfigurations;

        public void ValidateHomePageTitle()
        {
            Thread.Sleep(5000);
            var elemnetvisible = homePageTitle.Displayed;
        }

        public void SubmenuItems_display(IWebElement element)
        {
            IWebDriver _dr = Browser._Driver;
            element = _dr.FindElement(By.XPath("//li[@data-testid='nestedMenuItem']//p[contains(text(),'CSP Settings')]"));
            IWebElement listofsubmenuItems = _dr.FindElement(By.XPath("//li[@data-testid='nestedMenuItem']//p[contains(text(),'CSP Settings')]"));
            Actions abc= new Actions(_dr);
            abc.MoveToElement(element);
            abc.Click(listofsubmenuItems);
            abc.Build().Perform();

        }

        public void NavigateToEMVSubMenu(string EMV_SubMenuName, string pclID = "9006: Pier 2")
        {
            DriverUtilities.Click(SearchOrSelect);
            Browser.ClickDynamicElement(PclDynamic, pclID);
            Browser.ClickDynamicElement(clientGallery, "CLIENT GALLERY");
            Browser.ClickDynamicElement(clientGalleryMenuItem, "EMV");

            switch (EMV_SubMenuName)
            {
                case "Issuers":
                    Browser.Click(Issuers);
                    break;
                case "Scripts":
                    Browser.Click(Scripts);
                    break;
            }
        }

        public void NavigateToCSPSettingsSubMenu(string CSPSetting_SubMenuName, string pclID = "9006: Pier 2")
        {
            DriverUtilities.Click(SearchOrSelect);
            Browser.ClickDynamicElement(PclDynamic, pclID);
            Browser.ClickDynamicElement(clientGallery, "CLIENT GALLERY");
            Browser.ClickDynamicElement(clientGalleryMenuItem, "CSP Settings");

            CSPSettings_SubmenuItems(true);
            switch (CSPSetting_SubMenuName)
            {
                case "BOC Dynamic Info":
                    Browser.Click(BOCDynamicInfo);
                    break;
                case "FOC Dynamic Info":
                    Browser.Click(FOCDynamicInfo);
                    break;
                case "Card Holder Agreement":
                    Browser.Click(CardHolderAgreement);
                    break;
                case "EMV Profile":
                    Browser.Click(EMVProfile);
                    break;
                case "Carrier Dynamic Info":
                    Browser.Click(CarrierDynamicInfo);
                    break;
            }
        }

        public void CSPSettings_SubmenuItems(Boolean CSPSetting_SubMenuName)
        {
            Browser.Click(CSPSettings);
            if (BOCDynamicInfo.Displayed&& CardHolderAgreement.Displayed && CarrierDynamicInfo.Displayed&& EMVProfile.Displayed&& FOCDynamicInfo.Displayed)
            {
                CSPSetting_SubMenuName = true;
            }
            else
            {
                Browser.Close();
            }
        }

        public void NavigateToClientSettingsSubMenu(string ClientSetting_SubMenuName, string pclID = "9006: Pier 2")
        {
            DriverUtilities.Click(SearchOrSelect);
            Browser.ClickDynamicElement(PclDynamic, pclID);
            Browser.ClickDynamicElement(clientGallery, "CLIENT GALLERY");
            Browser.ClickDynamicElement(clientGalleryMenuItem, "Client Settings");

            switch (ClientSetting_SubMenuName)
            {
                case "Bank ID Numbers":
                    Browser.Click(BankIdNumbers);
                    break;
                case "BCSS Configurations":
                    Browser.Click(BCSSConfigurations);
                    break;
                case "Client Profiles":
                    Browser.Click(ClientProfiles);
                    break;
                case "Default Proof Replacements":
                    Browser.Click(DefaultProofReplacements);
                    break;
                case "Embosser Field Maps":
                    Browser.Click(EmbosserFieldMaps);
                    break;
                case "General Settings":
                    Browser.Click(GeneralSettings);
                    break;
                case "Hot Stamps":
                    Browser.Click(HotStamps);
                    break;
                case "Inventory Configuration":
                    Browser.Click(InventoryConfiguration);
                    break;
                case "Issuing Banks":
                    Browser.Click(IssuingBanks);
                    break;
                case "Mag Track Encodings":
                    Browser.Click(MagTrackEncodings);
                    break;
                case "Configure Client":
                    Browser.Click(ConfigureClient);
                    break;
                case "Platforms Products":
                    Browser.Click(Platforms_Products);
                    break;
                case "Print Tags":
                    Browser.Click(PrintTags);
                    break;
                case "Print Settings":
                    Browser.Click(PrintSettings);
                    break;
                case "Publishing Policies":
                    Browser.Click(PublishingPolicies);
                    break;
                case "Retailer Client Configurations":
                    Browser.Click(RetailerClient_Configurations);
                    break;
                case "Retailer Profiles":
                    Browser.Click(RetailerProfiles);
                    break;
                case "Ship Types":
                    Browser.Click(ShipTypes);
                    break;
                case "SOP Adhoc Key Values":
                    Browser.Click(SOPAdhocKeyValues);
                    break;
                case "SOP Configurations":
                    Browser.Click(SOPConfigurations);
                    break;
            }
        }

    }
}
