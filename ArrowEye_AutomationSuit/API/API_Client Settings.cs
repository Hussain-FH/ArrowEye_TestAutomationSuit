using System;
using System.Collections;
using System.Collections.Generic;
using System.Diagnostics.Metrics;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using ArrowEye_Automation_Framework.Common;
using Newtonsoft.Json.Linq;
using NUnit.Framework;
using RandomString4Net;
using static System.Net.Mime.MediaTypeNames;

namespace ArrowEye_Automation_Framework.API
{
    internal class API_Client_Settings
    {

        [Test]
        [Description("ClientSetting BCSS Config Create")]
        [Category("Smoke")]
        [TestCase("Automation_ClientSetting_BCSS_Config_Create")]
        public void ClientSetting_BCSS_Config_Create(string APItoken)
        {
            string randomString = RandomString.GetString(Types.ALPHANUMERIC_MIXEDCASE, 15);
            string Jsonbody = "{\"name\":\"" + randomString + "\",\"description\": \"Desc\",\"profileName\":\"ProfileName\",\"cvvDateFormatId\": 30,\"cvV2DateFormatId\": 50,\"cvvKeyPairId\": 0,\"cvV2KeyPairId\": 0,\"serviceCodeId\": 0, \"useServiceCodeCVVId\": 0,\"useServiceCodeCVV2Id\": 0,\"pvkiCode\": 10,\"cvkType\": 10, \"userName\": \"UserName\",\"pclid\": 100}";
            var response = APICommonMethods.ResponseFromPostRequest(AppNameHelper.ApiBaseUrl, "/issuers", Jsonbody);
            Console.WriteLine(response.Content);
            var toasterMessageID = Regex.Match(response.Content, @"\d+").Value;
            String OutputVal = response.Content.ToString().Replace("\"", "");
            String txt = "EMV Issuer " + toasterMessageID + " Added Successfully.";
            if (response.IsSuccessful)
            {
                Assert.That((int)response.StatusCode, Is.EqualTo(201));
                Assert.That(OutputVal, Is.EqualTo(txt));
            }
            else
            {
                Console.WriteLine((int)response.StatusCode);
                Assert.Fail("Test Fail");
            }
        }

        //Name already exist 
        [Test]
        [Description("ClientSetting BCSS Config Name AlreadyExist")]
        [Category("Smoke")]
        [TestCase("Automation_ClientSetting_BCSS_Config_Name_AlreadyExist")]
        public void ClientSetting_BCSS_Config_Create_Name_AlreadyExist(string APItoken)
        {
            string randomString = RandomString.GetString(Types.ALPHANUMERIC_MIXEDCASE, 15);
            String tempVal = randomString;
            string Jsonbody = "{\"name\": \"" + tempVal + "\",\"description\": \"Desc\",\"profileName\":\"ProfileName\",\"cvvDateFormatId\": 30,\"cvV2DateFormatId\": 50,\"cvvKeyPairId\": 10,\"cvV2KeyPairId\": 20,\"serviceCodeId\": 30, \"useServiceCodeCVVId\": 40,\"useServiceCodeCVV2Id\": 50,\"pvkiCode\": 10,\"cvkType\": 10, \"userName\": \"UserName\",\"pclid\": 100}";
            var response = APICommonMethods.ResponseFromPostRequest(AppNameHelper.ApiBaseUrl, "/bcssconfigurations", Jsonbody);
            Console.WriteLine(response.Content);
            string Jsonbody2 = "{\"name\": \"" + tempVal + "\",\"description\": \"Desc\",\"profileName\":\"ProfileName\",\"cvvDateFormatId\": 30,\"cvV2DateFormatId\": 50,\"cvvKeyPairId\": 10,\"cvV2KeyPairId\": 20,\"serviceCodeId\": 30, \"useServiceCodeCVVId\": 40,\"useServiceCodeCVV2Id\": 50,\"pvkiCode\": 10,\"cvkType\": 10, \"userName\": \"UserName\",\"pclid\": 100}";
            var response2 = APICommonMethods.ResponseFromPostRequest(AppNameHelper.ApiBaseUrl, "/bcssconfigurations", Jsonbody2);
            dynamic jsonResponse = JObject.Parse(response2.Content);
            String ErrorMsg = jsonResponse.error[0].message;
            Console.WriteLine(response2.Content);
            String txt = "The BCSS Configuration " + randomString + " already exists.";
            Assert.That(ErrorMsg, Is.EqualTo(txt));
            Assert.That((int)response2.StatusCode, Is.EqualTo(400));
        }

        // Name field value is not entered 
        [Test]
        [Description("ClientSetting BCSS Config Create blank Name")]
        [Category("Smoke")]
        [TestCase("Automation_ClientSetting_BCSS_Config_Create_Blank Name")]
        public void ClientSetting_BCSS_Config_Create_Blank_Name(string APItoken)
        {
            string randomString = RandomString.GetString(Types.ALPHANUMERIC_MIXEDCASE, 15);
            string Jsonbody = "{\"name\": \"\",\"description\": \"Desc\",\"profileName\":\"ProfileName\",\"cvvDateFormatId\": 30,\"cvV2DateFormatId\": 50,\"cvvKeyPairId\": 0,\"cvV2KeyPairId\": 0,\"serviceCodeId\": 0, \"useServiceCodeCVVId\": 0,\"useServiceCodeCVV2Id\": 0,\"pvkiCode\": 10,\"cvkType\": 10, \"userName\": \"UserName\",\"pclid\": 100}";
            var response = APICommonMethods.ResponseFromPostRequest(AppNameHelper.ApiBaseUrl, "/bcssconfigurations", Jsonbody);
            dynamic jsonResponse = JObject.Parse(response.Content);
            String ErrorMsg = jsonResponse.errors.Name[0];
            Console.WriteLine(jsonResponse.errors.Name[0]);
            Assert.That(ErrorMsg, Is.EqualTo("The Name field is required."));
            Assert.That((int)response.StatusCode, Is.EqualTo(400));
        }

        //more than 100 in Name field
        [Test]
        [Description("ClientSetting BCSS Config Create Name More100")]
        [Category("Smoke")]
        [TestCase("Automation_ClientSetting_BCSS_Config_Create_Name_More100")]
        public void ClientSetting_BCSS_Config_Create_Name_More100(string APItoken)
        {
            string randomString = RandomString.GetString(Types.ALPHANUMERIC_MIXEDCASE, 15);
            string Jsonbody = "{\"name\": \"apitestingapitestingapitestingapitestingapitestingapitestingapitestingapitestingapitestingapitestinga\",\"description\": \"Desc\",\"profileName\":\"ProfileName\",\"cvvDateFormatId\": 30,\"cvV2DateFormatId\": 50,\"cvvKeyPairId\": 0,\"cvV2KeyPairId\": 0,\"serviceCodeId\": 0, \"useServiceCodeCVVId\": 0,\"useServiceCodeCVV2Id\": 0,\"pvkiCode\": 10,\"cvkType\": 10, \"userName\": \"UserName\",\"pclid\": 100}";
            var response = APICommonMethods.ResponseFromPostRequest(AppNameHelper.ApiBaseUrl, "/bcssconfigurations", Jsonbody);
            dynamic jsonResponse = JObject.Parse(response.Content);
            String ErrorMsg = jsonResponse.errors.Name[0];
            Console.WriteLine(jsonResponse.errors.Name[0]);
            Assert.That(ErrorMsg, Is.EqualTo("Name can accept 100 characters or fewer. You entered 101 characters."));
            Assert.That((int)response.StatusCode, Is.EqualTo(400));
        }

        //profile name field value is not entered
        [Test]
        [Description("ClientSetting BCSS Config Create Blank_ProfileName")]
        [Category("Smoke")]
        [TestCase("Automation_ClientSetting_BCSS_Config_Create_Blank_ProfileName")]
        public void ClientSetting_BCSS_Config_Create_Blank_ProfileName(string APItoken)
        {
            string randomString = RandomString.GetString(Types.ALPHANUMERIC_MIXEDCASE, 15);
            string Jsonbody = "{\"name\": \"" + randomString + "\",\"description\": \"Desc\",\"profileName\":\"\",\"cvvDateFormatId\": 30,\"cvV2DateFormatId\": 50,\"cvvKeyPairId\": 0,\"cvV2KeyPairId\": 0,\"serviceCodeId\": 0, \"useServiceCodeCVVId\": 0,\"useServiceCodeCVV2Id\": 0,\"pvkiCode\": 10,\"cvkType\": 10, \"userName\": \"UserName\",\"pclid\": 100}";
            var response = APICommonMethods.ResponseFromPostRequest(AppNameHelper.ApiBaseUrl, "/bcssconfigurations", Jsonbody);
            dynamic jsonResponse = JObject.Parse(response.Content);
            String ErrorMsg = jsonResponse.errors.ProfileName[0];
            Console.WriteLine(jsonResponse.errors.ProfileName[0]);
            Assert.That(ErrorMsg, Is.EqualTo("The ProfileName field is required."));
            Assert.That((int)response.StatusCode, Is.EqualTo(400));
        }

        //more than 50 in profileName field
        [Test]
        [Description("ClientSetting BCSS Config Create profileName More50")]
        [Category("Smoke")]
        [TestCase("Automation_ClientSetting_BCSS_Config_Create_ProfileName_More50")]
        public void ClientSetting_BCSS_Config_Create_ProfileName_More50Char(string APItoken)
        {
            string randomString = RandomString.GetString(Types.ALPHANUMERIC_MIXEDCASE, 15);
            string Jsonbody = "{\"name\": \"" + randomString + "\",\"description\": \"Desc\",\"profileName\":\"apitestingapitestingapitestingapitestingapitestinga\",\"cvvDateFormatId\": 30,\"cvV2DateFormatId\": 50,\"cvvKeyPairId\": 0,\"cvV2KeyPairId\": 0,\"serviceCodeId\": 0, \"useServiceCodeCVVId\": 0,\"useServiceCodeCVV2Id\": 0,\"pvkiCode\": 10,\"cvkType\": 10, \"userName\": \"UserName\",\"pclid\": 100}";
            var response = APICommonMethods.ResponseFromPostRequest(AppNameHelper.ApiBaseUrl, "/bcssconfigurations", Jsonbody);
            dynamic jsonResponse = JObject.Parse(response.Content);
            String ErrorMsg = jsonResponse.errors.ProfileName[0];
            Console.WriteLine(jsonResponse.errors.ProfileName[0]);
            Assert.That(ErrorMsg, Is.EqualTo("ProfileName can accept 50 characters or fewer. You entered 51 characters."));
            Assert.That((int)response.StatusCode, Is.EqualTo(400));
        }

        //Update
        [Test]
        [Description("ClientSetting BCSS Config Update")]
        [Category("Smoke")]
        [TestCase("Automation_ClientSetting_BCSS_Config_Update")]
        public void ClientSetting_BCSS_Config_Update(string APItoken)
        {
            string randomString = RandomString.GetString(Types.ALPHANUMERIC_MIXEDCASE, 15);
            string Jsonbody = "{\"name\": \"bsscong37\",\"description\": \"Desc\", \"profileName\": \"apitesting\",\"cvvDateFormatId\": 45,\"cvV2DateFormatId\": 48,\"cvvKeyPairId\": 10,\"cvV2KeyPairId\": 20,\"serviceCodeId\": 30,\n  \"useServiceCodeCVVId\": 20,\"useServiceCodeCVV2Id\": 10,\"pvkiCode\": 26,\"cvkType\": 76,\n  \"userName\": \"UserName\",\"pclid\": 1,\"id\": 1}";
            var response = APICommonMethods.ResponseFromPutRequest(AppNameHelper.ApiBaseUrl, "/bcssconfigurations", Jsonbody);
            Console.WriteLine(response.Content);
            var toasterMessageID = Regex.Match(response.Content, @"\d+").Value;
            String OutputVal = response.Content.ToString().Replace("\"", "");
            String txt = "BCSS Configuration " + toasterMessageID + " Updated Successfully.";
            if (response.IsSuccessful)
            {
                Assert.That((int)response.StatusCode, Is.EqualTo(200));
                Assert.That(OutputVal, Is.EqualTo(txt));
            }
            else
            {
                Console.WriteLine((int)response.StatusCode);
                Assert.Fail("Test Fail");
            }
        }

        // Name field value is not entered 
        [Test]
        [Description("ClientSetting BCSS Config Update blank Name")]
        [Category("Smoke")]
        [TestCase("Automation_ClientSetting_BCSS_Config_Update_Blank Name")]
        public void ClientSetting_BCSS_Config_Update_Blank_Name(string APItoken)
        {
            string randomString = RandomString.GetString(Types.ALPHANUMERIC_MIXEDCASE, 15);
            string Jsonbody = "{\"name\": \"\",\"description\": \"Desc\",\"profileName\":\"ProfileName\",\"cvvDateFormatId\": 30,\"cvV2DateFormatId\": 50,\"cvvKeyPairId\": 0,\"cvV2KeyPairId\": 0,\"serviceCodeId\": 0, \"useServiceCodeCVVId\": 0,\"useServiceCodeCVV2Id\": 0,\"pvkiCode\": 10,\"cvkType\": 10, \"userName\": \"UserName\",\"pclid\": 12,\"id\": 1}";
            var response = APICommonMethods.ResponseFromPutRequest(AppNameHelper.ApiBaseUrl, "/bcssconfigurations", Jsonbody);
            dynamic jsonResponse = JObject.Parse(response.Content);
            String ErrorMsg = jsonResponse.errors.Name[0];
            Console.WriteLine(jsonResponse.errors.Name[0]);
            Assert.That(ErrorMsg, Is.EqualTo("The Name field is required."));
            Assert.That((int)response.StatusCode, Is.EqualTo(400));
        }

        //more than 100 in Name field
        [Test]
        [Description("ClientSetting BCSS Config Update Name More100")]
        [Category("Smoke")]
        [TestCase("Automation_ClientSetting_BCSS_Config_Update_Name_More100")]
        public void ClientSetting_BCSS_Config_Update_Name_More100(string APItoken)
        {
            string randomString = RandomString.GetString(Types.ALPHANUMERIC_MIXEDCASE, 15);
            string Jsonbody = "{\"name\": \"apitestingapitestingapitestingapitestingapitestingapitestingapitestingapitestingapitestingapitestinga\",\"description\": \"Desc\",\"profileName\":\"ProfileName\",\"cvvDateFormatId\": 30,\"cvV2DateFormatId\": 50,\"cvvKeyPairId\": 0,\"cvV2KeyPairId\": 0,\"serviceCodeId\": 0, \"useServiceCodeCVVId\": 0,\"useServiceCodeCVV2Id\": 0,\"pvkiCode\": 10,\"cvkType\": 10, \"userName\": \"UserName\",\"pclid\": 12,\"id\": 1}";
            var response = APICommonMethods.ResponseFromPutRequest(AppNameHelper.ApiBaseUrl, "/bcssconfigurations", Jsonbody);
            dynamic jsonResponse = JObject.Parse(response.Content);
            String ErrorMsg = jsonResponse.errors.Name[0];
            Console.WriteLine(jsonResponse.errors.Name[0]);
            Assert.That(ErrorMsg, Is.EqualTo("Name can accept 100 characters or fewer. You entered 101 characters."));
            Assert.That((int)response.StatusCode, Is.EqualTo(400));
        }

        //Name already exists
        [Test]
        [Description("ClientSetting BCSS Config Update Name Already Exist")]
        [Category("Smoke")]
        [TestCase("Automation_ClientSetting_BCSS_Config_Update_Name_Already Exist")]
        public void ClientSetting_BCSS_Config_Update_Name_Already_Exist(string APItoken)
        {
            string randomString = RandomString.GetString(Types.ALPHANUMERIC_MIXEDCASE, 15);
            String tempVal = randomString;
            string Jsonbody = "{\"name\": \"" + tempVal + "\",\"description\": \"Desc\",\"profileName\":\"ProfileName\",\"cvvDateFormatId\": 30,\"cvV2DateFormatId\": 50,\"cvvKeyPairId\": 10,\"cvV2KeyPairId\": 20,\"serviceCodeId\": 30, \"useServiceCodeCVVId\": 40,\"useServiceCodeCVV2Id\":50,\"pvkiCode\": 10,\"cvkType\": 10, \"userName\": \"UserName\",\"pclid\": 100}";
            var response = APICommonMethods.ResponseFromPostRequest(AppNameHelper.ApiBaseUrl, "/bcssconfigurations", Jsonbody);
            Console.WriteLine(response.Content);
            string Jsonbody2 = "{\"name\": \"" + tempVal + "\",\"description\": \"Desc\",\"profileName\":\"ProfileName\",\"cvvDateFormatId\": 30,\"cvV2DateFormatId\": 50,\"cvvKeyPairId\": 10,\"cvV2KeyPairId\": 20,\"serviceCodeId\": 30, \"useServiceCodeCVVId\": 40,\"useServiceCodeCVV2Id\": 50,\"pvkiCode\": 10,\"cvkType\": 10, \"userName\": \"UserName\",\"pclid\": 100,\"id\": 1}";
            var response2 = APICommonMethods.ResponseFromPutRequest(AppNameHelper.ApiBaseUrl, "/bcssconfigurations", Jsonbody2);
            dynamic jsonResponse = JObject.Parse(response2.Content);
            String ErrorMsg = jsonResponse.error[0].message;
            Console.WriteLine(jsonResponse.error[0].message);
            String txt = "The BCSS Configuration " + randomString + " already exists.";
            Assert.That(ErrorMsg, Is.EqualTo(txt));
            Assert.That((int)response2.StatusCode, Is.EqualTo(400));
        }

        //profile name field value is not entered
        [Test]
        [Description("ClientSetting BCSS Config Update Blank_ProfileName")]
        [Category("Smoke")]
        [TestCase("Automation_ClientSetting_BCSS_Config_Update_Blank_ProfileName")]
        public void ClientSetting_BCSS_Config_Update_Blank_ProfileName(string APItoken)
        {
            string randomString = RandomString.GetString(Types.ALPHANUMERIC_MIXEDCASE, 15);
            string Jsonbody = "{\"name\": \"" + randomString + "\",\"description\": \"Desc\",\"profileName\":\"\",\"cvvDateFormatId\": 30,\"cvV2DateFormatId\": 50,\"cvvKeyPairId\": 0,\"cvV2KeyPairId\": 0,\"serviceCodeId\": 0, \"useServiceCodeCVVId\": 0,\"useServiceCodeCVV2Id\": 0,\"pvkiCode\": 10,\"cvkType\": 10, \"userName\": \"UserName\",\"pclid\": 12,\"id\": 1}";
            var response = APICommonMethods.ResponseFromPutRequest(AppNameHelper.ApiBaseUrl, "/bcssconfigurations", Jsonbody);
            dynamic jsonResponse = JObject.Parse(response.Content);
            String ErrorMsg = jsonResponse.errors.ProfileName[0];
            Console.WriteLine(jsonResponse.errors.ProfileName[0]);
            Assert.That(ErrorMsg, Is.EqualTo("The ProfileName field is required."));
            Assert.That((int)response.StatusCode, Is.EqualTo(400));
        }

        //more than 50 in profileName field
        [Test]
        [Description("ClientSetting BCSS Config Update profileName More50")]
        [Category("Smoke")]
        [TestCase("Automation_ClientSetting_BCSS_Config_Update_ProfileName_More50")]
        public void ClientSetting_BCSS_Config_Update_ProfileName_More50Char(string APItoken)
        {
            string randomString = RandomString.GetString(Types.ALPHANUMERIC_MIXEDCASE, 15);
            string Jsonbody = "{\"name\": \"" + randomString + "\",\"description\": \"Desc\",\"profileName\":\"apitestingapitestingapitestingapitestingapitestinga\",\"cvvDateFormatId\": 30,\"cvV2DateFormatId\": 50,\"cvvKeyPairId\": 0,\"cvV2KeyPairId\": 0,\"serviceCodeId\": 0, \"useServiceCodeCVVId\": 0,\"useServiceCodeCVV2Id\": 0,\"pvkiCode\": 10,\"cvkType\": 10, \"userName\": \"UserName\",\"pclid\": 12,\"id\": 1}";
            var response = APICommonMethods.ResponseFromPutRequest(AppNameHelper.ApiBaseUrl, "/bcssconfigurations", Jsonbody);
            dynamic jsonResponse = JObject.Parse(response.Content);
            String ErrorMsg = jsonResponse.errors.ProfileName[0];
            Console.WriteLine(jsonResponse.errors.ProfileName[0]);
            Assert.That(ErrorMsg, Is.EqualTo("ProfileName can accept 50 characters or fewer. You entered 51 characters."));
            Assert.That((int)response.StatusCode, Is.EqualTo(400));
        }

        //Hot Stamps
        [Test]
        [Description("ClientSetting Hot Stamps Create")]
        [Category("Smoke")]
        [TestCase("Automation_ClientSetting_Hot_Stamps_Create")]
        public void ClientSetting_Hot_Stamps_Post(string APItoken)
        {
            string randomString = RandomString.GetString(Types.ALPHANUMERIC_MIXEDCASE, 15);
            string Jsonbody = "{\"pclId\": 120,\"name\": \"Name\",\"description\": \"Desc\",\"userName\": \"UN\"}";
            var response = APICommonMethods.ResponseFromPostRequest(AppNameHelper.ApiBaseUrl, "/hotstamps", Jsonbody);
            Console.WriteLine(response.Content);
            var toasterMessageID = Regex.Match(response.Content, @"\d+").Value;
            String OutputVal = response.Content.ToString().Replace("\"", "");
            String txt = "Hot Stamp " + toasterMessageID + " Added Successfully.";
            if (response.IsSuccessful)
            {
                Assert.That((int)response.StatusCode, Is.EqualTo(201));
                Assert.That(OutputVal, Is.EqualTo(txt));
            }
            else
            {
                Console.WriteLine((int)response.StatusCode);
                Assert.Fail("Test Fail");
            }
        }

        // Name value is not entered
        [Test]
        [Description("ClientSetting Hot Stamps Blank Name")]
        [Category("Smoke")]
        [TestCase("Automation_ClientSetting_Hot_Stamps_Blank_Name")]
        public void ClientSetting_Hot_Stamps_Post_Blank_Name(string APItoken)
        {

            string randomString = RandomString.GetString(Types.ALPHANUMERIC_MIXEDCASE, 15);
            string Jsonbody = "{\"pclId\": 120,\"name\": \"\",\"description\": \"Desc\",\"userName\": \"UN\"}";
            var response = APICommonMethods.ResponseFromPostRequest(AppNameHelper.ApiBaseUrl, "/hotstamps", Jsonbody);
            //Console.WriteLine(response.Content);
            dynamic jsonResponse = JObject.Parse(response.Content);
            String ErrorMsg = jsonResponse.errors.Name[0];
            Console.WriteLine(jsonResponse.errors.Name[0]);
            Assert.That(ErrorMsg, Is.EqualTo("The Name field is required."));
            Assert.That((int)response.StatusCode, Is.EqualTo(400));
        }

        //character are more than 100 in Name field
        [Test]
        [Description("ClientSetting Hot Stamps Name MoreThan100")]
        [Category("Smoke")]
        [TestCase("Automation_ClientSetting_Hot_Stamps_Name_MoreThan100")]
        public void ClientSetting_Hot_Stamps_Post_Name_MoreThan100(string APItoken)
        {

            string randomString = RandomString.GetString(Types.ALPHANUMERIC_MIXEDCASE, 15);
            string Jsonbody = "{\"pclId\": 120,\"name\": \"apitestingapitestingapitestingapitestingapitestingapitestingapitestingapitestingapitestingapitestinga\",\"description\": \"Desc\",\"userName\": \"UN\"}";
            var response = APICommonMethods.ResponseFromPostRequest(AppNameHelper.ApiBaseUrl, "/hotstamps", Jsonbody);
            //Console.WriteLine(response.Content);
            dynamic jsonResponse = JObject.Parse(response.Content);
            String ErrorMsg = jsonResponse.errors.Name[0];
            Console.WriteLine(jsonResponse.errors.Name[0]);
            Assert.That(ErrorMsg, Is.EqualTo("Name can accept 100 characters or fewer. You entered 101 characters."));
            Assert.That((int)response.StatusCode, Is.EqualTo(400));
        }

        //character are more than 500 in description field
        [Test]
        [Description("ClientSetting Hot Stamps Name MoreThan500")]
        [Category("Smoke")]
        [TestCase("Automation_ClientSetting_Hot_Stamps_Name_MoreThan500")]
        public void ClientSetting_Hot_Stamps_Post_description_MoreThan500(string APItoken)
        {

            string randomString = RandomString.GetString(Types.ALPHANUMERIC_MIXEDCASE, 15);
            string Jsonbody = "{\"pclId\": 120,\"name\": \"apitesting\",\"description\": \"apitestingapitestingapitestingapitestingapitestingapitestingapitestingapitestingapitestingapitestinghkkkkkkkkkkkkkkkkkkkkkkkkkkkkkkkkkkkkkkkkkkkkkkkkkkkkkkkkkkkkkkkkkkkkkkkkkkkkkkkkkkkkkkkkkkkkkkkkkkkkkkkkkkkkkkkkkkkkkkkkkkkkkkkkkkkkkkkkkkkkkkkkkkkkkkkkkkkkkkkkkkkkkkkkkkkkkkkkkkkkkkkkkkkkkkkkkkkkkkkkkkkkkkkkkkkkkkkkkkkkkkkkkkkkkkkkkkkkkkkkkkkkkkkkkkkkkkkkkkkkkkkkkkkkkkkkkkkkkkkkkkkkkkkkkkkkkkkkkkkkkkkkkkkkkkkkkkkkkkkkkkkkkkkkkkkkkkkkkkkkkkkkkkkkkkkkkkkkkkkkkkkkkkkkkkkkkkkkkkkkkkkkkkkkkkkkkkkkkkkk\",\"userName\": \"UN\"}";
            var response = APICommonMethods.ResponseFromPostRequest(AppNameHelper.ApiBaseUrl, "/hotstamps", Jsonbody);
            //Console.WriteLine(response.Content);
            dynamic jsonResponse = JObject.Parse(response.Content);
            String ErrorMsg = jsonResponse.errors.Description[0];
            Console.WriteLine(jsonResponse.errors.Description[0]);
            Assert.That(ErrorMsg, Is.EqualTo("Description can accept 500 characters or fewer. You entered 501 characters."));
            Assert.That((int)response.StatusCode, Is.EqualTo(400));
        }

        //PUT
        [Test]
        [Description("ClientSetting HotStamps Put")]
        [Category("Smoke")]
        [TestCase("Automation_ClientSetting_Hot_Stamps_Put")]
        public void ClientSetting_Hot_Stamps_Update(string APItoken)
        {
            string Jsonbody = "{\"name\": \"name Demo 16th\",\"description\": \"Desc Demo 16th\",\"id\":1}";
            var response = APICommonMethods.ResponseFromPutRequest(AppNameHelper.ApiBaseUrl, "/hotstamps", Jsonbody);
            //Console.WriteLine(response.Content);
            var toasterMessageID = Regex.Match(response.Content, @"\d+").Value;
            String OutputVal = response.Content.ToString().Replace("\"", "");
            String txt = "Hot Stamp " + toasterMessageID + " Updated Successfully.";

            if (response.IsSuccessful)
            {
                Assert.That((int)response.StatusCode, Is.EqualTo(200));
                Assert.That(OutputVal, Is.EqualTo(txt));
            }
            else
            {
                Console.WriteLine((int)response.StatusCode);
                Assert.Fail("Test Fail");
            }
        }

        // Name value is not entered
        [Test]
        [Description("ClientSetting Hot Stamps Blank Name")]
        [Category("Smoke")]
        [TestCase("Automation_ClientSetting_Hot_Stamps_Blank_Name")]
        public void ClientSetting_Hot_Stamps_Put_Blank_Name(string APItoken)
        {
            //string randomString = RandomString.GetString(Types.ALPHANUMERIC_MIXEDCASE, 15);
            string Jsonbody = "{\"name\": \"\",\"description\": \"Desc Demo 16th\",\"id\":1}";
            var response = APICommonMethods.ResponseFromPutRequest(AppNameHelper.ApiBaseUrl, "/hotstamps", Jsonbody);
            dynamic jsonResponse = JObject.Parse(response.Content);
            String ErrorMsg = jsonResponse.errors.Name[0];
            Console.WriteLine(jsonResponse.errors.Name[0]);
            Assert.That(ErrorMsg, Is.EqualTo("The Name field is required."));
            Assert.That((int)response.StatusCode, Is.EqualTo(400));
        }

        //character are more than 100 in Name field
        [Test]
        [Description("ClientSetting Hot Stamps Name MoreThan100")]
        [Category("Smoke")]
        [TestCase("Automation_ClientSetting_Hot_Stamps_Name_MoreThan100")]
        public void ClientSetting_Hot_Stamps_Put_Name_MoreThan100(string APItoken)
        {

            string randomString = RandomString.GetString(Types.ALPHANUMERIC_MIXEDCASE, 15);
            string Jsonbody = "{\"name\": \"apitestingapitestingapitestingapitestingapitestingapitestingapitestingapitestingapitestingapitestinga\",\"description\": \"apitesting\",\"id\":1}";
            var response = APICommonMethods.ResponseFromPutRequest(AppNameHelper.ApiBaseUrl, "/hotstamps", Jsonbody);
            dynamic jsonResponse = JObject.Parse(response.Content);
            String ErrorMsg = jsonResponse.errors.Name[0];
            Console.WriteLine(jsonResponse.errors.Name[0]);
            Assert.That(ErrorMsg, Is.EqualTo("Name can accept 100 characters or fewer. You entered 101 characters."));
            Assert.That((int)response.StatusCode, Is.EqualTo(400));
        }

        //character are more than 500 in description field
        [Test]
        [Description("ClientSetting Hot Stamps Name MoreThan500")]
        [Category("Smoke")]
        [TestCase("Automation_ClientSetting_Hot_Stamps_Name_MoreThan500")]
        public void ClientSetting_Hot_Stamps_Put_description_MoreThan500(string APItoken)
        {
            string randomString = RandomString.GetString(Types.ALPHANUMERIC_MIXEDCASE, 15);
            string Jsonbody = "{\"name\": \"name Demo 16th\",\"description\": \"apitestingapitestingapitestingapitestingapitestingapitestingapitestingapitestingapitestingapitestinghkkkkkkkkkkkkkkkkkkkkkkkkkkkkkkkkkkkkkkkkkkkkkkkkkkkkkkkkkkkkkkkkkkkkkkkkkkkkkkkkkkkkkkkkkkkkkkkkkkkkkkkkkkkkkkkkkkkkkkkkkkkkkkkkkkkkkkkkkkkkkkkkkkkkkkkkkkkkkkkkkkkkkkkkkkkkkkkkkkkkkkkkkkkkkkkkkkkkkkkkkkkkkkkkkkkkkkkkkkkkkkkkkkkkkkkkkkkkkkkkkkkkkkkkkkkkkkkkkkkkkkkkkkkkkkkkkkkkkkkkkkkkkkkkkkkkkkkkkkkkkkkkkkkkkkkkkkkkkkkkkkkkkkkkkkkkkkkkkkkkkkkkkkkkkkkkkkkkkkkkkkkkkkkkkkkkkkkkkkkkkkkkkkkkkkkkkkkkkkkk\",\"id\":1}";
            var response = APICommonMethods.ResponseFromPutRequest(AppNameHelper.ApiBaseUrl, "/hotstamps", Jsonbody);
            //Console.WriteLine(response.Content);
            dynamic jsonResponse = JObject.Parse(response.Content);
            String ErrorMsg = jsonResponse.errors.Description[0];
            Console.WriteLine(jsonResponse.errors.Description[0]);
            Assert.That(ErrorMsg, Is.EqualTo("Description can accept 500 characters or fewer. You entered 501 characters."));
            Assert.That((int)response.StatusCode, Is.EqualTo(400));
        }

        //GET
        [Test]
        [Description("ClientSetting HotStamps Get")]
        [Category("Smoke")]
        [TestCase("Automation_ClientSetting_HotStamps_Get")]
        public void ClientSetting_HotStamps_Get(string APItoken)
        {
            var response = APICommonMethods.ResponseFromGETrequest(AppNameHelper.ApiBaseUrl, "/hotstamps?pclid=120");
            if (response.IsSuccessful)
            {
                Assert.That((int)response.StatusCode, Is.EqualTo(200));
                Assert.That(response.Content, Does.Not.Contain("One or more validation errors occurred."));
            }
            else
            {
                Console.WriteLine((int)response.StatusCode);
                Assert.Fail("Test Fail");
            }
        }

        [Test]
        [Description("ClientSetting Hot Stamps Get Ids Match")]
        [Category("Smoke")]
        [TestCase("Automation_ClientSetting_HotStampsGet_IDMatch_DB")]
        public void ClientSetting_HotStamps_Get_IDMatch_DB(string APItoken)
        {
            var response = APICommonMethods.ResponseFromGETrequest(AppNameHelper.ApiBaseUrl, "/hotstamps?pclid=120");
            JArray jsonArray = JArray.Parse(response.Content);
            ArrayList ApiIDValue = new ArrayList();
            foreach (JObject var in jsonArray)
            {
                ApiIDValue.Add((int)var.GetValue("id"));
            }
            ApiIDValue.Sort();
            foreach (var it in ApiIDValue)
            {
                Console.WriteLine(it);
            }
            string sql = "select * from dbo.hotstampdie h where h.pclid =120  order by id";
            ArrayList ls = DBConnect_Methods.SelectMethod(sql);
            //Console.WriteLine(ls);
            foreach (var item in ls)
            {
                Console.WriteLine(item);
            }
            if ((ApiIDValue.ToArray() as IStructuralEquatable).Equals(ls.ToArray(), EqualityComparer<int>.Default))
            {
                Assert.Pass();
            }
            else
            {
                Assert.Fail();
            }

        }

        // Client profile 
        [Test]
        [Description("ClientSetting clientProfiles Post")]
        [Category("Smoke")]
        [TestCase("Automation_ClientProfile_Post")]
        public void ClientSetting_clientProfiles_Post(string APItoken)
        {
            //string randomString = RandomString.GetString(Types.ALPHANUMERIC_MIXEDCASE, 15);
            string Jsonbody = "{\"value\": \"1\",\"userName\": \"Test123\",\"pclId\": 108,\"keyId\": 4}";
            var response = APICommonMethods.ResponseFromPostRequest(AppNameHelper.ApiBaseUrl, "/clientprofiles", Jsonbody);
            var toasterMessageID = Regex.Match(response.Content, @"\d+").Value;
            String OutputVal = response.Content.ToString().Replace("\"", "");
            Console.WriteLine(OutputVal);
            String txt = "Client Profile " + toasterMessageID + " Updated Successfully.";
            if (response.IsSuccessful)
            {
                Assert.That((int)response.StatusCode, Is.EqualTo(200));
                Assert.That(OutputVal, Is.EqualTo(txt));
            }
            else
            {
                Console.WriteLine((int)response.StatusCode);
                Assert.Fail("Test Fail");
            }
            string Jsonbody2 = "{\"id\":\"" + toasterMessageID + "\",\"pclId\": 108,\"userName\": \"Test123\"}";
            var response2 = APICommonMethods.ResponseFromDeleteRequest(AppNameHelper.ApiBaseUrl, "/clientprofiles", Jsonbody2);
            Console.WriteLine(response2.Content);
        }

        // Key is not entered
        [Test]
        [Description("ClientSetting clientProfiles Post Blank Value")]
        [Category("Smoke")]
        [TestCase("Automation_ClientProfile_Post_Blank_Value")]
        public void ClientSetting_clientProfiles_Post_Blank_Key(string APItoken)
        {

            //string randomString = RandomString.GetString(Types.ALPHANUMERIC_MIXEDCASE, 15);
            string Jsonbody = "{\"value\": \"8\",\"userName\": \"Test123\",\"pclId\": 108, \"keyId\":0}";
            var response = APICommonMethods.ResponseFromPostRequest(AppNameHelper.ApiBaseUrl, "/clientprofiles", Jsonbody);
            dynamic jsonResponse = JObject.Parse(response.Content);
            String ErrorMsg = jsonResponse.errors.KeyId[0];
            Console.WriteLine(jsonResponse.errors.KeyId[0]);
            Assert.That(ErrorMsg, Is.EqualTo("The KeyId field is required."));
            Assert.That((int)response.StatusCode, Is.EqualTo(400));
        }

        // value is not entered
        [Test]
        [Description("ClientSetting clientProfiles Post Blank Value")]
        [Category("Smoke")]
        [TestCase("Automation_ClientProfile_Post_Blank_Value")]
        public void ClientSetting_clientProfiles_Post_Blank_Value(string APItoken)
        {

            //string randomString = RandomString.GetString(Types.ALPHANUMERIC_MIXEDCASE, 15);
            string Jsonbody = "{\"value\": \"\",\"userName\": \"Test123\",\"pclId\": 108, \"keyId\": 43}";
            var response = APICommonMethods.ResponseFromPostRequest(AppNameHelper.ApiBaseUrl, "/clientprofiles", Jsonbody);
            dynamic jsonResponse = JObject.Parse(response.Content);
            String ErrorMsg = jsonResponse.errors.Value[0];
            Console.WriteLine(jsonResponse.errors.Value[0]);
            Assert.That(ErrorMsg, Is.EqualTo("The Value field is required."));
            Assert.That((int)response.StatusCode, Is.EqualTo(400));
        }

        // Invalid value for Key
        [Test]
        [Description("ClientSetting clientProfiles Post Invalid KeyId")]
        [Category("Smoke")]
        [TestCase("Automation_ClientProfile_Post_Invalid_KeyId")]
        public void ClientSetting_clientProfiles_Post_Invalid_KeyId(string APItoken)
        {
            //string randomString = RandomString.GetString(Types.ALPHANUMERIC_MIXEDCASE, 15);
            string Jsonbody = "{\"value\": \"5\",\"userName\": \"Test123\",\"pclId\": 102, \"keyId\": 4889990}";
            var response = APICommonMethods.ResponseFromPostRequest(AppNameHelper.ApiBaseUrl, "/clientprofiles", Jsonbody);
            dynamic jsonResponse = JObject.Parse(response.Content);
            String ErrorMsg = jsonResponse.error[0].message;
            Console.WriteLine(jsonResponse.error[0].message);
            Assert.That(ErrorMsg, Is.EqualTo("The KeyId 4889990 doesn't exists."));
            Assert.That((int)response.StatusCode, Is.EqualTo(400));
        }

        // When Key already exists
        [Test]
        [Description("ClientSetting clientProfiles Post Invalid KeyId")]
        [Category("Smoke")]
        [TestCase("Automation_ClientProfile_Post_Invalid_KeyId")]
        public void ClientSetting_clientProfiles_Post_Key_AlreadyExist(string APItoken)
        {

            //string randomString = RandomString.GetString(Types.ALPHANUMERIC_MIXEDCASE, 15);
            string Jsonbody = "{\"value\": \"5\",\"userName\": \"Test123\",\"pclId\": 108, \"keyId\": 48}";
            var response = APICommonMethods.ResponseFromPostRequest(AppNameHelper.ApiBaseUrl, "/clientprofiles", Jsonbody);
            dynamic jsonResponse = JObject.Parse(response.Content);
            String ErrorMsg = jsonResponse.error[0].message;
            Console.WriteLine(jsonResponse.error[0].message);
            Assert.That(ErrorMsg, Is.EqualTo("The Key Program Profile Test already exists."));
            Assert.That((int)response.StatusCode, Is.EqualTo(400));
        }

        //character are more than 200 in Value field
        [Test]
        [Description("ClientSetting clientProfiles Post Value More200Char")]
        [Category("Smoke")]
        [TestCase("Automation_ClientProfile_Post_Value_More200Char")]
        public void ClientSetting_clientProfiles_Post_Value_More200Char(string APItoken)
        {

            //string randomString = RandomString.GetString(Types.ALPHANUMERIC_MIXEDCASE, 15);
            string Jsonbody = "{\"value\": \"apitestingapitestingapitestingapitestingapitestingapitestingapitestingapitestingapitestingapitestingapitestingapitestingapitestingapitestingapitestingapitestingapitestingapitestingapitestingapitestingj\",\"userName\": \"Test123\",\"pclId\": 108, \"keyId\": 48}";
            var response = APICommonMethods.ResponseFromPostRequest(AppNameHelper.ApiBaseUrl, "/clientprofiles", Jsonbody);
            dynamic jsonResponse = JObject.Parse(response.Content);
            String ErrorMsg = jsonResponse.errors.Value[0];
            Console.WriteLine(jsonResponse.errors.Value[0]);
            Assert.That(ErrorMsg, Is.EqualTo("Value can accept 200 characters or fewer. You entered 201 characters."));
            Assert.That((int)response.StatusCode, Is.EqualTo(400));

        }

        //Put
        [Test]
        [Description("ClientSetting clientProfiles Update")]
        [Category("Smoke")]
        [TestCase("Automation_ClientSetting_ClientProfiles_Update")]
        public void ClientSetting_ClientProfiles_Update(string APItoken)
        {
            string Jsonbody = "{\"value\": \"1\",\"userName\": \"user name up\",\"id\": 1}";
            var response = APICommonMethods.ResponseFromPutRequest(AppNameHelper.ApiBaseUrl, "/clientprofiles", Jsonbody);
            var toasterMessageID = Regex.Match(response.Content, @"\d+").Value;
            String OutputVal = response.Content.ToString().Replace("\"", "");
            String txt = "Client Profile " + toasterMessageID + " Updated Successfully.";

            if (response.IsSuccessful)
            {
                Assert.That((int)response.StatusCode, Is.EqualTo(200));
                Assert.That(OutputVal, Is.EqualTo(txt));
            }
            else
            {
                Console.WriteLine((int)response.StatusCode);
                Assert.Fail("Test Fail");
            }
        }

        // value is not entered
        [Test]
        [Description("ClientSetting clientProfiles Put Blank Value")]
        [Category("Smoke")]
        [TestCase("Automation_ClientProfile_Put_Blank_Value")]
        public void ClientSetting_clientProfiles_Put_Blank_Value(string APItoken)
        {
            //string randomString = RandomString.GetString(Types.ALPHANUMERIC_MIXEDCASE, 15);
            string Jsonbody = "{\"value\": \"\",\"userName\": \"Test123\",\"id\": 101   }";
            var response = APICommonMethods.ResponseFromPutRequest(AppNameHelper.ApiBaseUrl, "/clientprofiles", Jsonbody);
            dynamic jsonResponse = JObject.Parse(response.Content);
            String ErrorMsg = jsonResponse.errors.Value[0];
            Console.WriteLine(jsonResponse.errors.Value[0]);
            Assert.That(ErrorMsg, Is.EqualTo("The Value field is required."));
            Assert.That((int)response.StatusCode, Is.EqualTo(400));
        }

        //character are more than 200 in Value field
        [Test]
        [Description("ClientSetting clientProfiles Put Value More200Char")]
        [Category("Smoke")]
        [TestCase("Automation_ClientProfile_Put_Value_More200Char")]
        public void ClientSetting_clientProfiles_Put_Value_More200Char(string APItoken)
        {
            //string randomString = RandomString.GetString(Types.ALPHANUMERIC_MIXEDCASE, 15);
            string Jsonbody = "{\"value\": \"apitestingapitestingapitestingapitestingapitestingapitestingapitestingapitestingapitestingapitestingapitestingapitestingapitestingapitestingapitestingapitestingapitestingapitestingapitestingapitestingj\",\"userName\": \"Test123\",\"Id\": 1}";
            var response = APICommonMethods.ResponseFromPutRequest(AppNameHelper.ApiBaseUrl, "/clientprofiles", Jsonbody);
            dynamic jsonResponse = JObject.Parse(response.Content);
            String ErrorMsg = jsonResponse.errors.Value[0];
            Console.WriteLine(jsonResponse.errors.Value[0]);
            Assert.That(ErrorMsg, Is.EqualTo("Value can accept 200 characters or fewer. You entered 201 characters."));
            Assert.That((int)response.StatusCode, Is.EqualTo(400));
        }

        //Blank Id
        [Test]
        [Description("ClientSetting clientProfiles Put Value BlankId")]
        [Category("Smoke")]
        [TestCase("Automation_ClientProfile_Put_Value_BlankID")]
        public void ClientSetting_clientProfiles_Put_Value_Blank_ID(string APItoken)
        {
            //string randomString = RandomString.GetString(Types.ALPHANUMERIC_MIXEDCASE, 15);
            string Jsonbody = "{\"value\": \"apitestitingapitestin\",\"userName\": \"Test123\",\"Id\": 0}";
            var response = APICommonMethods.ResponseFromPutRequest(AppNameHelper.ApiBaseUrl, "/clientprofiles", Jsonbody);
            dynamic jsonResponse = JObject.Parse(response.Content);
            String ErrorMsg = jsonResponse.errors.Id[0];
            Console.WriteLine(jsonResponse.errors.Id[0]);
            Assert.That(ErrorMsg, Is.EqualTo("The Id field is required."));
            Assert.That((int)response.StatusCode, Is.EqualTo(400));
        }

        [Category("Smoke")]
        [TestCase("Automation_ClientSetting_HotStampsGet_IDMatch_DB")]
        public void ClientSetting_ClientProfile_Delete(string APItoken)
        {
            //string randomString = RandomString.GetString(Types.ALPHANUMERIC_MIXEDCASE, 15);
            string Jsonbody = "{\"value\": \"1\",\"userName\": \"Test123\",\"pclId\": 102,\"keyId\": 3}";
            var response = APICommonMethods.ResponseFromPostRequest(AppNameHelper.ApiBaseUrl, "/clientprofiles", Jsonbody);
            var toasterMessageID = Regex.Match(response.Content, @"\d+").Value;
            String OutputVal = response.Content.ToString().Replace("\"", "");
            Console.WriteLine(OutputVal);
            String txt = "Client Profile " + toasterMessageID + " Updated Successfully.";
            if (response.IsSuccessful)
            {
                Assert.That((int)response.StatusCode, Is.EqualTo(200));
                Assert.That(OutputVal, Is.EqualTo(txt));
            }
            else
            {
                Console.WriteLine((int)response.StatusCode);
                Assert.Fail("Test Fail");
            }
            string Jsonbody2 = "{\"id\":\"" + toasterMessageID + "\",\"pclId\": 102,\"userName\": \"Test123\"}";
            var response2 = APICommonMethods.ResponseFromDeleteRequest(AppNameHelper.ApiBaseUrl, "/clientprofiles", Jsonbody2);
            Console.WriteLine(response2.Content);
        }

        [Category("Smoke")]
        [TestCase("Automation_ClientSetting_HotStampsGet_IDMatch_DB")]
        public void ClientSetting_ClientProfile_GetMaster_DB(string APItoken)
        {
            var response = APICommonMethods.ResponseFromGETrequest(AppNameHelper.ApiBaseUrl, "/clientprofiles/getmasterdata");
            JArray jsonArray = JArray.Parse(response.Content);
            ArrayList ApiIDValue = new ArrayList();
            foreach (JObject var in jsonArray)
            {
                ApiIDValue.Add((int)var.GetValue("keyId"));
            }
            ApiIDValue.Sort();
            foreach (var it in ApiIDValue)
            {
                Console.WriteLine(it);
            }
            string sql = "select a.id as keyid from dbo.applicationdatadictionary a join dbo.code c on a.datasourceid = c.codeid and c.codetypeid = 2 left join dbo.applicationconfigurationset acs on a.id = acs.applicationdatadictionaryid left join dbo.configurationset cs on acs.configurationsetid = cs.id order by keyid";
            ArrayList ls = DBConnect_Methods.SelectMethod(sql);
            Console.WriteLine("DBBBBBBBBBBBBBBBBBB vales");
            Console.WriteLine("api" + ApiIDValue.Count);
            Console.WriteLine(ls.Count);
            foreach (var item in ls)
            {
                Console.WriteLine(item);
            }
            if ((ApiIDValue.ToArray() as IStructuralEquatable).Equals(ls.ToArray(), EqualityComparer<int>.Default))
            {
                Assert.Pass();
            }
            else
            {
                Assert.Fail();
            }
        }

        //Get
        [Category("Smoke")]
        [TestCase("Automation_ClientSetting_HotStampsGet_IDMatch_DB")]
        public void ClientSetting_ClientProfile_BankIDNumber_DB(string APItoken)
        {
            var response = APICommonMethods.ResponseFromGETrequest(AppNameHelper.ApiBaseUrl, "/bankidnumbers?pclid=102");
            JArray jsonArray = JArray.Parse(response.Content);
            ArrayList ApiIDValue = new ArrayList();
            foreach (JObject var in jsonArray)
            {
                ApiIDValue.Add((int)var.GetValue("id"));
            }
            foreach (var it in ApiIDValue)
            {
                Console.WriteLine(it);
            }
            string sql = "select (column_2).id  from dbo.portal_getbin(102) \r\n";
            ArrayList ls = DBConnect_Methods.SelectMethod(sql);
            Console.WriteLine("DB vales");
            Console.WriteLine("api" + ApiIDValue.Count);
            Console.WriteLine(ls.Count);
            foreach (var item in ls)
            {
                Console.WriteLine(item);
            }
            if ((ApiIDValue.ToArray() as IStructuralEquatable).Equals(ls.ToArray(), EqualityComparer<int>.Default))
            {
                Assert.Pass();
            }
            else
            {
                Assert.Fail();
            }
        }

        //Client Settings - General Settings POST - Contact Info
        [Test]
        [Description("ClientSetting clientProfiles Post")]
        [Category("Smoke")]
        [TestCase("Automation_ClientProfile_Post")]
        public void ClientSetting_GeneralSettings_ContactInfo_POST(string APItoken)
        {
            string randomString = RandomString.GetString(Types.ALPHANUMERIC_MIXEDCASE, 15);
            string Jsonbody = "{\"name\": \"name1\",\"title\": \"title1\",\"email\": \"" + randomString + "@gmail.com\",\"officeNo\": \"12345678\", \"mobileNo\": \"123456789\",\"otherNo\": \"12345678910\",\"note\": \"note\",\"addressesList\": [ {\"street1\": \"street1\",\"street2\": \"street2\",\"city\": \"city1\",\"state\": \"s1\",\"zip\": \"123\",\"countryId\": 21}], \"pclId\": 100}";
            var response = APICommonMethods.ResponseFromPostRequest(AppNameHelper.ApiBaseUrl, "/generalsettings/contactinfo", Jsonbody);
            Console.WriteLine(response.Content);
            var toasterMessageID = Regex.Match(response.Content, @"\d+").Value;
            String OutputVal = response.Content.ToString().Replace("\"", "");
            String txt = "Contact Information for " + toasterMessageID + " Added Successfully.";
            if (response.IsSuccessful)
            {
                Assert.That((int)response.StatusCode, Is.EqualTo(201));
                Assert.That(OutputVal, Is.EqualTo(txt));
            }
            else
            {
                Console.WriteLine((int)response.StatusCode);
                Assert.Fail("Test Fail");
            }

        }

        // character are more than 30 in Name
        [Test]
        [Description("ClientSetting clientProfiles Post")]
        [Category("Smoke")]
        [TestCase("Automation_ClientProfile_Post")]
        public void ClientSetting_GeneralSettings_ContactInfo_Post_Name_More30Char(string APItoken)
        {

            string randomString = RandomString.GetString(Types.ALPHANUMERIC_MIXEDCASE, 15);
            string Jsonbody = "{\"name\": \"apitestingapitestingapitestinga\",\"title\": \"title1\",\"email\": \"" + randomString + "@gmail.com\",\"officeNo\": \"12345678\", \"mobileNo\": \"123456789\",\"otherNo\": \"12345678910\",\"note\": \"note\",\"addressesList\": [ {\"street1\": \"street1\",\"street2\": \"street2\",\"city\": \"city1\",\"state\": \"s1\",\"zip\": \"123\",\"countryId\": 21}], \"pclId\": 100}";
            var response = APICommonMethods.ResponseFromPostRequest(AppNameHelper.ApiBaseUrl, "/generalsettings/contactinfo", Jsonbody);
            Console.WriteLine(response.Content);
            dynamic jsonResponse = JObject.Parse(response.Content);
            String ErrorMsg = jsonResponse.errors.Name[0];
            Console.WriteLine(jsonResponse.errors.Name[0]);
            Assert.That(ErrorMsg, Is.EqualTo("Name can accept 30 characters or fewer. You entered 31 characters."));
            Assert.That((int)response.StatusCode, Is.EqualTo(400));
        }

        //User Doesn't enter value for Name
        [Test]
        [Description("ClientSetting clientProfiles Post Blank_Name")]
        [Category("Smoke")]
        [TestCase("Automation_ClientProfile_Post Blank_Name")]
        public void ClientSetting_GeneralSettings_ContactInfo_Post_Blank_Name(string APItoken)
        {
            string randomString = RandomString.GetString(Types.ALPHANUMERIC_MIXEDCASE, 15);
            string Jsonbody = "{\"name\": \"\",\"title\": \"title1\",\"email\": \"" + randomString + "@gmail.com\",\"officeNo\": \"12345678\", \"mobileNo\": \"123456789\",\"otherNo\": \"12345678910\",\"note\": \"note\",\"addressesList\": [ {\"street1\": \"street1\",\"street2\": \"street2\",\"city\": \"city1\",\"state\": \"s1\",\"zip\": \"123\",\"countryId\": 21}], \"pclId\": 100}";
            var response = APICommonMethods.ResponseFromPostRequest(AppNameHelper.ApiBaseUrl, "/generalsettings/contactinfo", Jsonbody);
            Console.WriteLine(response.Content);
            dynamic jsonResponse = JObject.Parse(response.Content);
            String ErrorMsg = jsonResponse.errors.Name[0];
            Console.WriteLine(jsonResponse.errors.Name[0]);
            Assert.That(ErrorMsg, Is.EqualTo("The Name field is required."));
            Assert.That((int)response.StatusCode, Is.EqualTo(400));
        }

        //character are more than 50 in Title
        [Test]
        [Description("ClientSetting clientProfiles Post Title more50Char ")]
        [Category("Smoke")]
        [TestCase("Automation_ClientProfile_Post Blank Title More50Char")]
        public void ClientSetting_GeneralSettings_ContactInfo_Post_Title_more50Char(string APItoken)
        {
            string randomString = RandomString.GetString(Types.ALPHANUMERIC_MIXEDCASE, 15);
            string Jsonbody = "{\"name\": \"APITesting\",\"title\": \"apitestingapitestingapitestingapitestingapitestinga\",\"email\": \"" + randomString + "@gmail.com\",\"officeNo\": \"12345678\", \"mobileNo\": \"123456789\",\"otherNo\": \"12345678910\",\"note\": \"note\",\"addressesList\": [ {\"street1\": \"street1\",\"street2\": \"street2\",\"city\": \"city1\",\"state\": \"s1\",\"zip\": \"123\",\"countryId\": 21}], \"pclId\": 100}";
            var response = APICommonMethods.ResponseFromPostRequest(AppNameHelper.ApiBaseUrl, "/generalsettings/contactinfo", Jsonbody);
            Console.WriteLine(response.Content);
            dynamic jsonResponse = JObject.Parse(response.Content);
            String ErrorMsg = jsonResponse.errors.Title[0];
            Console.WriteLine(jsonResponse.errors.Title[0]);
            Assert.That(ErrorMsg, Is.EqualTo("Title can accept 50 characters or fewer. You entered 51 characters."));
            Assert.That((int)response.StatusCode, Is.EqualTo(400));
        }

        //character are more than 20 in City
        [Test]
        [Description("ClientSetting clientProfiles Post City more20Char")]
        [Category("Smoke")]
        [TestCase("Automation_ClientProfile_Post Blank City More20Char")]
        public void ClientSetting_GeneralSettings_ContactInfo_Post_City_more20Char(string APItoken)
        {

            string randomString = RandomString.GetString(Types.ALPHANUMERIC_MIXEDCASE, 15);
            string Jsonbody = "{\"name\": \"apitest\",\"title\": \"apitesting\",\"email\": \"" + randomString + "@gmail.com\",\"officeNo\": \"12345678\", \"mobileNo\": \"123456789\",\"otherNo\": \"12345678910\",\"note\": \"note\",\"addressesList\": [ {\"street1\": \"street1\",\"street2\": \"street2\",\"city\": \"apitestingapitestinga\",\"state\": \"s1\",\"zip\": \"123\",\"countryId\": 21}], \"pclId\": 100}";
            var response = APICommonMethods.ResponseFromPostRequest(AppNameHelper.ApiBaseUrl, "/generalsettings/contactinfo", Jsonbody);
            Console.WriteLine(response.Content);
            dynamic jsonResponse = JObject.Parse(response.Content);
            Console.WriteLine(jsonResponse);
            String ErrorMsg = jsonResponse.errors["AddressesList[0].City"][0];
            Assert.That(ErrorMsg, Is.EqualTo("City can accept 20 characters or fewer. You entered 21 characters."));
            Assert.That((int)response.StatusCode, Is.EqualTo(400));
        }

        //character are more than 50 in Email
        [Test]
        [Description("ClientSetting clientProfiles Post Email more50Char")]
        [Category("Smoke")]
        [TestCase("Automation_ClientProfile_Post Blank City More50Char")]
        public void ClientSetting_GeneralSettings_ContactInfo_Post_Email_more50Char(string APItoken)
        {
            string randomString = RandomString.GetString(Types.ALPHANUMERIC_MIXEDCASE, 15);
            string Jsonbody = "{\"name\": \"apitest\",\"title\": \"apitesting\",\"email\": \"apitestingapitestingapitestingapitestingapitestinga@gmail.com\",\"officeNo\": \"12345678\", \"mobileNo\": \"123456789\",\"otherNo\": \"12345678910\",\"note\": \"note\",\"addressesList\": [ {\"street1\": \"street1\",\"street2\": \"street2\",\"city\": \"apitesting\",\"state\": \"s1\",\"zip\": \"123\",\"countryId\": 21}], \"pclId\": 100}";
            var response = APICommonMethods.ResponseFromPostRequest(AppNameHelper.ApiBaseUrl, "/generalsettings/contactinfo", Jsonbody);
            Console.WriteLine(response.Content);
            dynamic jsonResponse = JObject.Parse(response.Content);
            Console.WriteLine(jsonResponse);
            String ErrorMsg = jsonResponse.errors.Email[0];
            Assert.That(ErrorMsg, Is.EqualTo("Email can accept 50 characters or fewer. You entered 61 characters."));
            Assert.That((int)response.StatusCode, Is.EqualTo(400));
        }

        //Blank Email
        [Test]
        [Description("ClientSetting clientProfiles Post Blank Email")]
        [Category("Smoke")]
        [TestCase("Automation_ClientProfile_Post Blank Email")]
        public void ClientSetting_GeneralSettings_ContactInfo_Post_Blank_Email(string APItoken)
        {
            string randomString = RandomString.GetString(Types.ALPHANUMERIC_MIXEDCASE, 15);
            string Jsonbody = "{\"name\": \"apitest\",\"title\": \"apitesting\",\"email\": \"\",\"officeNo\": \"12345678\", \"mobileNo\": \"123456789\",\"otherNo\": \"12345678910\",\"note\": \"note\",\"addressesList\": [ {\"street1\": \"street1\",\"street2\": \"street2\",\"city\": \"apitesting\",\"state\": \"s1\",\"zip\": \"123\",\"countryId\": 21}], \"pclId\": 100}";
            var response = APICommonMethods.ResponseFromPostRequest(AppNameHelper.ApiBaseUrl, "/generalsettings/contactinfo", Jsonbody);
            Console.WriteLine(response.Content);
            dynamic jsonResponse = JObject.Parse(response.Content);
            Console.WriteLine(jsonResponse);
            String ErrorMsg = jsonResponse.errors.Email[0];
            Assert.That(ErrorMsg, Is.EqualTo("The Email field is required."));
            Assert.That((int)response.StatusCode, Is.EqualTo(400));
        }

        //Email which is not in the Standard Format
        [Test]
        [Description("ClientSetting clientProfiles Post Email Format")]
        [Category("Smoke")]
        [TestCase("Automation_ClientProfile_Post Email Format")]
        public void ClientSetting_GeneralSettings_ContactInfo_Post_Email_Format(string APItoken)
        {
            string randomString = RandomString.GetString(Types.ALPHANUMERIC_MIXEDCASE, 15);
            string Jsonbody = "{\"name\": \"apitest\",\"title\": \"apitesting\",\"email\": \"Apitesting.com\",\"officeNo\": \"12345678\", \"mobileNo\": \"123456789\",\"otherNo\": \"12345678910\",\"note\": \"note\",\"addressesList\": [ {\"street1\": \"street1\",\"street2\": \"street2\",\"city\": \"apitesting\",\"state\": \"s1\",\"zip\": \"123\",\"countryId\": 21}], \"pclId\": 100}";
            var response = APICommonMethods.ResponseFromPostRequest(AppNameHelper.ApiBaseUrl, "/generalsettings/contactinfo", Jsonbody);
            Console.WriteLine(response.Content);
            dynamic jsonResponse = JObject.Parse(response.Content);
            Console.WriteLine(jsonResponse);
            String ErrorMsg = jsonResponse.errors.Email[0];
            Assert.That(ErrorMsg, Is.EqualTo("Ensure that the email address conforms to standard formats (e.g., user@example.com)."));
            Assert.That((int)response.StatusCode, Is.EqualTo(400));
        }

        // Email Already Exist
        [Test]
        [Description("ClientSetting clientProfiles Post Email Already Exist")]
        [Category("Smoke")]
        [TestCase("Automation_ClientProfile_Post Email Already Exist ")]
        public void ClientSetting_GeneralSettings_ContactInfo_Post_Email_AlreadyExist(string APItoken)
        {
            string randomString = RandomString.GetString(Types.ALPHANUMERIC_MIXEDCASE, 15);
            string Jsonbody = "{\"name\": \"apitest\",\"title\": \"apitesting\",\"email\": \"apitesting@test.com\",\"officeNo\": \"12345678\", \"mobileNo\": \"123456789\",\"otherNo\": \"12345678910\",\"note\": \"note\",\"addressesList\": [ {\"street1\": \"street1\",\"street2\": \"street2\",\"city\": \"apitesting\",\"state\": \"s1\",\"zip\": \"123\",\"countryId\": 21}], \"pclId\": 100}";
            var response = APICommonMethods.ResponseFromPostRequest(AppNameHelper.ApiBaseUrl, "/generalsettings/contactinfo", Jsonbody);
            Console.WriteLine(response.Content);
            dynamic jsonResponse = JObject.Parse(response.Content);
            Console.WriteLine(jsonResponse);
            String ErrorMsg = jsonResponse.error[0].message;
            Assert.That(ErrorMsg, Is.EqualTo("The EmailId apitesting@test.com already exists."));
            Assert.That((int)response.StatusCode, Is.EqualTo(400));
        }

        //less than 7 in Office/ Mobile/Other
        [Test]
        [Description("ClientSetting clientProfiles Post Char lessthan7")]
        [Category("Smoke")]
        [TestCase("Automation_ClientProfile_Post_Char_lessthan7")]
        public void ClientSetting_GeneralSettings_ContactInfo_POST_Char_lessthan7(string APItoken)
        {
            string randomString = RandomString.GetString(Types.ALPHANUMERIC_MIXEDCASE, 15);
            string Jsonbody = "{\"name\": \"name1\",\"title\": \"title1\",\"email\": \"" + randomString + "@gmail.com\",\"officeNo\": \"12345\", \"mobileNo\": \"123456\",\"otherNo\": \"1234\",\"note\": \"note\",\"addressesList\": [ {\"street1\": \"street1\",\"street2\": \"street2\",\"city\": \"city1\",\"state\": \"s1\",\"zip\": \"123\",\"countryId\": 21}], \"pclId\": 100}";
            var response = APICommonMethods.ResponseFromPostRequest(AppNameHelper.ApiBaseUrl, "/generalsettings/contactinfo", Jsonbody);
            Console.WriteLine(response.Content);
            dynamic jsonResponse = JObject.Parse(response.Content);
            Console.WriteLine(jsonResponse);
            String ErrorMsg = jsonResponse.errors.OtherNo[0];
            String ErrorMsg1 = jsonResponse.errors.MobileNo[0];
            String ErrorMsg2 = jsonResponse.errors.OfficeNo[0];
            Assert.That(ErrorMsg, Is.EqualTo("OtherNo must not be less than 7 characters."));
            Assert.That((int)response.StatusCode, Is.EqualTo(400));
            Assert.That(ErrorMsg1, Is.EqualTo("MobileNo must not be less than 7 characters."));
            Assert.That(ErrorMsg2, Is.EqualTo("OfficeNo must not be less than 7 characters."));

        }

        //Morethan15 in Office/ Mobile/Other
        [Test]
        [Description("ClientSetting clientProfiles Post Char Morethan15")]
        [Category("Smoke")]
        [TestCase("Automation_ClientProfile_Post_Char_Morethan15")]
        public void ClientSetting_GeneralSettings_ContactInfo_POST_Char_Morethan15(string APItoken)
        {
            string randomString = RandomString.GetString(Types.ALPHANUMERIC_MIXEDCASE, 15);
            string Jsonbody = "{\"name\": \"name1\",\"title\": \"title1\",\"email\": \"" + randomString + "@gmail.com\",\"officeNo\": \"1234567891024567\", \"mobileNo\": \"1234567891024567\",\"otherNo\": \"1234567891024567\",\"note\": \"note\",\"addressesList\": [ {\"street1\": \"street1\",\"street2\": \"street2\",\"city\": \"city1\",\"state\": \"s1\",\"zip\": \"123\",\"countryId\": 21}], \"pclId\": 100}";
            var response = APICommonMethods.ResponseFromPostRequest(AppNameHelper.ApiBaseUrl, "/generalsettings/contactinfo", Jsonbody);
            Console.WriteLine(response.Content);
            dynamic jsonResponse = JObject.Parse(response.Content);
            Console.WriteLine(jsonResponse);
            String ErrorMsg = jsonResponse.errors.OtherNo[0];
            String ErrorMsg1 = jsonResponse.errors.MobileNo[0];
            String ErrorMsg2 = jsonResponse.errors.OfficeNo[0];
            Assert.That(ErrorMsg, Is.EqualTo("OtherNo must not exceed 15 characters."));
            Assert.That((int)response.StatusCode, Is.EqualTo(400));
            Assert.That(ErrorMsg1, Is.EqualTo("MobileNo must not exceed 15 characters."));
            Assert.That(ErrorMsg2, Is.EqualTo("OfficeNo must not exceed 15 characters."));

        }

        //Special characters +, -, &, () are allowed in Office/ Mobile/Other No.
        [Test]
        [Description("ClientSetting clientProfiles Post Char SpecialChar")]
        [Category("Smoke")]
        [TestCase("Automation_ClientProfile_Post_Char_SpecialChar")]
        public void ClientSetting_GeneralSettings_ContactInfo_PosT_SpecialChar(string APItoken)
        {
            string randomString = RandomString.GetString(Types.ALPHANUMERIC_MIXEDCASE, 15);
            string Jsonbody = "{\"name\": \"name1\",\"title\": \"title1\",\"email\": \"" + randomString + "@gmail.com\",\"officeNo\": \"(-1234&567+)\", \"mobileNo\": \"(-1234&567+)\",\"otherNo\": \"(-1234&567+)\",\"note\": \"note\",\"addressesList\": [ {\"street1\": \"street1\",\"street2\": \"street2\",\"city\": \"city1\",\"state\": \"s1\",\"zip\": \"123\",\"countryId\": 21}], \"pclId\": 100}";
            var response = APICommonMethods.ResponseFromPostRequest(AppNameHelper.ApiBaseUrl, "/generalsettings/contactinfo", Jsonbody);
            Console.WriteLine(response.Content);
            var toasterMessageID = Regex.Match(response.Content, @"\d+").Value;
            String OutputVal = response.Content.ToString().Replace("\"", "");
            String txt = "Contact Information for " + toasterMessageID + " Added Successfully.";
            if (response.IsSuccessful)
            {
                Assert.That((int)response.StatusCode, Is.EqualTo(201));
                Assert.That(OutputVal, Is.EqualTo(txt));
            }
            else
            {
                Console.WriteLine((int)response.StatusCode);
                Assert.Fail("Test Fail");
            }
        }

        // character are more than 100 in Note
        [Test]
        [Description("ClientSetting clientProfiles Post Note more50Char")]
        [Category("Smoke")]
        [TestCase("Automation_ClientProfile_Post Note More50Char")]
        public void ClientSetting_GeneralSettings_ContactInfo_Post_Note_more50Char(string APItoken)
        {
            string randomString = RandomString.GetString(Types.ALPHANUMERIC_MIXEDCASE, 15);
            string Jsonbody = "{\"name\": \"apitest\",\"title\": \"apitesting\",\"email\": \"api@gmail.com\",\"officeNo\": \"12345678\", \"mobileNo\": \"123456789\",\"otherNo\": \"12345678910\",\"note\": \"apitestingapitestingapitestingapitestingapitestingapitestingapitestingapitestingapitestingapitestinga\",\"addressesList\": [ {\"street1\": \"street1\",\"street2\": \"street2\",\"city\": \"apitesting\",\"state\": \"s1\",\"zip\": \"123\",\"countryId\": 21}], \"pclId\": 100}";
            var response = APICommonMethods.ResponseFromPostRequest(AppNameHelper.ApiBaseUrl, "/generalsettings/contactinfo", Jsonbody);
            Console.WriteLine(response.Content);
            dynamic jsonResponse = JObject.Parse(response.Content);
            Console.WriteLine(jsonResponse);
            String ErrorMsg = jsonResponse.errors.Note[0];
            Assert.That(ErrorMsg, Is.EqualTo("Note can accept 100 characters or fewer. You entered 101 characters."));
            Assert.That((int)response.StatusCode, Is.EqualTo(400));
        }

        //character are more than 30 in Street 1/Street 2
        [Test]
        [Description("ClientSetting clientProfiles Post Street more30Char")]
        [Category("Smoke")]
        [TestCase("Automation_ClientProfile_Post_Street_More30Char")]
        public void ClientSetting_GeneralSettings_ContactInfo_Post_Street_more30Char(string APItoken)
        {
            string randomString = RandomString.GetString(Types.ALPHANUMERIC_MIXEDCASE, 15);
            string Jsonbody = "{\"name\": \"apitest\",\"title\": \"apitesting\",\"email\": \"api@gmail.com\",\"officeNo\": \"12345678\", \"mobileNo\": \"123456789\",\"otherNo\": \"12345678910\",\"note\": \"apitest\",\"addressesList\": [ {\"street1\": \"apitestingapitestingapitestinga\",\"street2\": \"apitestingapitestingapitestinga\",\"city\": \"apitesting\",\"state\": \"s1\",\"zip\": \"123\",\"countryId\": 21}], \"pclId\": 100}";
            var response = APICommonMethods.ResponseFromPostRequest(AppNameHelper.ApiBaseUrl, "/generalsettings/contactinfo", Jsonbody);
            Console.WriteLine(response.Content);
            dynamic jsonResponse = JObject.Parse(response.Content);
            String ErrorMsg = jsonResponse.errors["AddressesList[0].Street1"][0];
            String ErrorMsg1 = jsonResponse.errors["AddressesList[0].Street2"][0];
            Assert.That(ErrorMsg, Is.EqualTo("Street1 can accept 30 characters or fewer. You entered 31 characters."));
            Assert.That((int)response.StatusCode, Is.EqualTo(400));
            Assert.That(ErrorMsg1, Is.EqualTo("Street2 can accept 30 characters or fewer. You entered 31 characters."));
        }

        //character are more than 2 in State
        [Test]
        [Description("ClientSetting clientProfiles Post State more2Char")]
        [Category("Smoke")]
        [TestCase("Automation_ClientProfile_Post_State_More2Char")]
        public void ClientSetting_GeneralSettings_ContactInfo_Post_State_more2Char(string APItoken)
        {
            string randomString = RandomString.GetString(Types.ALPHANUMERIC_MIXEDCASE, 15);
            string Jsonbody = "{\"name\": \"apitest\",\"title\": \"apitesting\",\"email\": \"api@gmail.com\",\"officeNo\": \"12345678\", \"mobileNo\": \"123456789\",\"otherNo\": \"12345678910\",\"note\": \"apitest\",\"addressesList\": [ {\"street1\": \"apitesting\",\"street2\": \"street2\",\"city\": \"apitesting\",\"state\": \"mh1\",\"zip\": \"123\",\"countryId\": 21}], \"pclId\": 100}";
            var response = APICommonMethods.ResponseFromPostRequest(AppNameHelper.ApiBaseUrl, "/generalsettings/contactinfo", Jsonbody);
            Console.WriteLine(response.Content);
            dynamic jsonResponse = JObject.Parse(response.Content);
            Console.WriteLine(jsonResponse);
            String ErrorMsg = jsonResponse.errors["AddressesList[0].State"][0];
            Assert.That(ErrorMsg, Is.EqualTo("State can accept 2 characters or fewer. You entered 3 characters."));
            Assert.That((int)response.StatusCode, Is.EqualTo(400));
        }

        //character are more than 10 in Zip
        [Test]
        [Description("ClientSetting clientProfiles Post ZIP more10Char")]
        [Category("Smoke")]
        [TestCase("Automation_ClientProfile_Post_Zip_More10Char")]
        public void ClientSetting_GeneralSettings_ContactInfo_Post_Zip_more10Char(string APItoken)
        {
            string randomString = RandomString.GetString(Types.ALPHANUMERIC_MIXEDCASE, 15);
            string Jsonbody = "{\"name\": \"apitest\",\"title\": \"apitesting\",\"email\": \"api@gmail.com\",\"officeNo\": \"12345678\", \"mobileNo\": \"123456789\",\"otherNo\": \"12345678910\",\"note\": \"apitest\",\"addressesList\": [ {\"street1\": \"apitesting\",\"street2\": \"street2\",\"city\": \"apitesting\",\"state\": \"mh\",\"zip\": \"12345678910\",\"countryId\": 21}], \"pclId\": 100}";
            var response = APICommonMethods.ResponseFromPostRequest(AppNameHelper.ApiBaseUrl, "/generalsettings/contactinfo", Jsonbody);
            Console.WriteLine(response.Content);
            dynamic jsonResponse = JObject.Parse(response.Content);
            Console.WriteLine(jsonResponse);
            String ErrorMsg = jsonResponse.errors["AddressesList[0].Zip"][0];
            Assert.That(ErrorMsg, Is.EqualTo("Zip can accept 10 characters or fewer. You entered 11 characters."));
            Assert.That((int)response.StatusCode, Is.EqualTo(400));
        }

        //ClientSetting clientProfiles Put
        [Test]
        [Description("ClientSetting clientProfiles Put")]
        [Category("Smoke")]
        [TestCase("Automation_ClientProfile_Put")]
        public void ClientSetting_GeneralSettings_ContactInfo_Put(string APItoken)
        {
            string randomString = RandomString.GetString(Types.ALPHANUMERIC_MIXEDCASE, 15);
            string Jsonbody = "{\"contactId\":1,\"name\": \"name1\",\"title\": \"title1\",\"email\": \"" + randomString + "@gmail.com\",\"officeNo\": \"12345678\", \"mobileNo\": \"123456789\",\"otherNo\": \"12345678910\",\"note\": \"note\",\"addressesList\": [ {\"street1\": \"street1\",\"street2\": \"street2\",\"city\": \"city1\",\"state\": \"s1\",\"zip\": \"123\",\"countryId\": 21}], \"pclId\": 100}";
            var response = APICommonMethods.ResponseFromPutRequest(AppNameHelper.ApiBaseUrl, "/generalsettings/contactinfo", Jsonbody);
            Console.WriteLine(response.Content);
            var toasterMessageID = Regex.Match(response.Content, @"\d+").Value;
            String OutputVal = response.Content.ToString().Replace("\"", "");
            String txt = "Contact Information for " + toasterMessageID + " Updated Successfully.";
            if (response.IsSuccessful)
            {
                Assert.That((int)response.StatusCode, Is.EqualTo(200));
                Assert.That(OutputVal, Is.EqualTo(txt));
            }
            else
            {
                Console.WriteLine((int)response.StatusCode);
                Assert.Fail("Test Fail");
            }

        }

        //Blank Email
        [Test]
        [Description("ClientSetting clientProfiles Post Blank Email")]
        [Category("Smoke")]
        [TestCase("Automation_ClientProfile_Post Blank Email")]
        public void ClientSetting_GeneralSettings_ContactInfo_Put_Blank_Email(string APItoken)
        {
            string randomString = RandomString.GetString(Types.ALPHANUMERIC_MIXEDCASE, 15);
            string Jsonbody = "{\"contactId\":1,\"name\": \"apitest\",\"title\": \"apitesting\",\"email\": \"\",\"officeNo\": \"12345678\", \"mobileNo\": \"123456789\",\"otherNo\": \"12345678910\",\"note\": \"note\",\"addressesList\": [ {\"street1\": \"street1\",\"street2\": \"street2\",\"city\": \"apitesting\",\"state\": \"s1\",\"zip\": \"123\",\"countryId\": 21}], \"pclId\": 100}";
            var response = APICommonMethods.ResponseFromPutRequest(AppNameHelper.ApiBaseUrl, "/generalsettings/contactinfo", Jsonbody);
            Console.WriteLine(response.Content);
            dynamic jsonResponse = JObject.Parse(response.Content);
            Console.WriteLine(jsonResponse);
            String ErrorMsg = jsonResponse.errors.Email[0];
            Assert.That(ErrorMsg, Is.EqualTo("The Email field is required."));
            Assert.That((int)response.StatusCode, Is.EqualTo(400));
        }

        //Email which is not in the Standard Format
        [Test]
        [Description("ClientSetting clientProfiles Post Email Format")]
        [Category("Smoke")]
        [TestCase("Automation_ClientProfile_Post Email Format")]
        public void ClientSetting_GeneralSettings_ContactInfo_Put_Email_Format(string APItoken)
        {
            string randomString = RandomString.GetString(Types.ALPHANUMERIC_MIXEDCASE, 15);
            string Jsonbody = "{\"contactId\":1,\"name\": \"apitest\",\"title\": \"apitesting\",\"email\": \"Apitesting.com\",\"officeNo\": \"12345678\", \"mobileNo\": \"123456789\",\"otherNo\": \"12345678910\",\"note\": \"note\",\"addressesList\": [ {\"street1\": \"street1\",\"street2\": \"street2\",\"city\": \"apitesting\",\"state\": \"s1\",\"zip\": \"123\",\"countryId\": 21}], \"pclId\": 100}";
            var response = APICommonMethods.ResponseFromPutRequest(AppNameHelper.ApiBaseUrl, "/generalsettings/contactinfo", Jsonbody);
            Console.WriteLine(response.Content);
            dynamic jsonResponse = JObject.Parse(response.Content);
            Console.WriteLine(jsonResponse);
            String ErrorMsg = jsonResponse.errors.Email[0];
            Assert.That(ErrorMsg, Is.EqualTo("Ensure that the email address conforms to standard formats (e.g., user@example.com)."));
            Assert.That((int)response.StatusCode, Is.EqualTo(400));
        }

        // Email Already Exist
        [Test]
        [Description("ClientSetting clientProfiles Post Email Already Exist")]
        [Category("Smoke")]
        [TestCase("Automation_ClientProfile_Post Email Already Exist ")]
        public void ClientSetting_GeneralSettings_ContactInfo_Put_Email_AlreadyExist(string APItoken)
        {
            string randomString = RandomString.GetString(Types.ALPHANUMERIC_MIXEDCASE, 15);
            string Jsonbody = "{\"contactId\":1,\"name\": \"apitest\",\"title\": \"apitesting\",\"email\": \"apitesting@test.com\",\"officeNo\": \"12345678\", \"mobileNo\": \"123456789\",\"otherNo\": \"12345678910\",\"note\": \"note\",\"addressesList\": [ {\"street1\": \"street1\",\"street2\": \"street2\",\"city\": \"apitesting\",\"state\": \"s1\",\"zip\": \"123\",\"countryId\": 21}], \"pclId\": 100}";
            var response = APICommonMethods.ResponseFromPutRequest(AppNameHelper.ApiBaseUrl, "/generalsettings/contactinfo", Jsonbody);
            Console.WriteLine(response.Content);
            dynamic jsonResponse = JObject.Parse(response.Content);
            Console.WriteLine(jsonResponse);
            String ErrorMsg = jsonResponse.error[0].message;
            Assert.That(ErrorMsg, Is.EqualTo("The EmailId apitesting@test.com already exists."));
            Assert.That((int)response.StatusCode, Is.EqualTo(400));
        }

        //less than 7 in Office/ Mobile/Other
        [Test]
        [Description("ClientSetting clientProfiles Post Char lessthan7")]
        [Category("Smoke")]
        [TestCase("Automation_ClientProfile_Post_Char_lessthan7")]
        public void ClientSetting_GeneralSettings_ContactInfo_Put_Char_lessthan7(string APItoken)
        {
            string randomString = RandomString.GetString(Types.ALPHANUMERIC_MIXEDCASE, 15);
            string Jsonbody = "{\"contactId\":1,\"name\": \"name1\",\"title\": \"title1\",\"email\": \"" + randomString + "@gmail.com\",\"officeNo\": \"12345\", \"mobileNo\": \"123456\",\"otherNo\": \"1234\",\"note\": \"note\",\"addressesList\": [ {\"street1\": \"street1\",\"street2\": \"street2\",\"city\": \"city1\",\"state\": \"s1\",\"zip\": \"123\",\"countryId\": 21}], \"pclId\": 100}";
            var response = APICommonMethods.ResponseFromPutRequest(AppNameHelper.ApiBaseUrl, "/generalsettings/contactinfo", Jsonbody);
            Console.WriteLine(response.Content);
            dynamic jsonResponse = JObject.Parse(response.Content);
            Console.WriteLine(jsonResponse);
            String ErrorMsg = jsonResponse.errors.OtherNo[0];
            String ErrorMsg1 = jsonResponse.errors.MobileNo[0];
            String ErrorMsg2 = jsonResponse.errors.OfficeNo[0];
            Assert.That(ErrorMsg, Is.EqualTo("OtherNo must not be less than 7 characters."));
            Assert.That((int)response.StatusCode, Is.EqualTo(400));
            Assert.That(ErrorMsg1, Is.EqualTo("MobileNo must not be less than 7 characters."));
            Assert.That(ErrorMsg2, Is.EqualTo("OfficeNo must not be less than 7 characters."));

        }

        //Morethan15 in Office/ Mobile/Other
        [Test]
        [Description("ClientSetting clientProfiles Put Char Morethan15")]
        [Category("Smoke")]
        [TestCase("Automation_ClientProfile_Put_Char_Morethan15")]
        public void ClientSetting_GeneralSettings_ContactInfo_Put_Char_Morethan15(string APItoken)
        {
            string randomString = RandomString.GetString(Types.ALPHANUMERIC_MIXEDCASE, 15);
            string Jsonbody = "{\"contactId\":1,\"name\": \"name1\",\"title\": \"title1\",\"email\": \"" + randomString + "@gmail.com\",\"officeNo\": \"1234567891024567\", \"mobileNo\": \"1234567891024567\",\"otherNo\": \"1234567891024567\",\"note\": \"note\",\"addressesList\": [ {\"street1\": \"street1\",\"street2\": \"street2\",\"city\": \"city1\",\"state\": \"s1\",\"zip\": \"123\",\"countryId\": 21}], \"pclId\": 100}";
            var response = APICommonMethods.ResponseFromPutRequest(AppNameHelper.ApiBaseUrl, "/generalsettings/contactinfo", Jsonbody);
            Console.WriteLine(response.Content);
            dynamic jsonResponse = JObject.Parse(response.Content);
            Console.WriteLine(jsonResponse);
            String ErrorMsg = jsonResponse.errors.OtherNo[0];
            String ErrorMsg1 = jsonResponse.errors.MobileNo[0];
            String ErrorMsg2 = jsonResponse.errors.OfficeNo[0];
            Assert.That(ErrorMsg, Is.EqualTo("OtherNo must not exceed 15 characters."));
            Assert.That((int)response.StatusCode, Is.EqualTo(400));
            Assert.That(ErrorMsg1, Is.EqualTo("MobileNo must not exceed 15 characters."));
            Assert.That(ErrorMsg2, Is.EqualTo("OfficeNo must not exceed 15 characters."));
        }

        //Special characters +, -, &, () are allowed in Office/ Mobile/Other No.
        [Test]
        [Description("ClientSetting clientProfiles Put Char SpecialChar")]
        [Category("Smoke")]
        [TestCase("Automation_ClientProfile_Put_Char_SpecialChar")]
        public void ClientSetting_GeneralSettings_ContactInfo_Put_SpecialChar(string APItoken)
        {
            string randomString = RandomString.GetString(Types.ALPHANUMERIC_MIXEDCASE, 15);
            string Jsonbody = "{\"contactId\":1,\"name\": \"name1\",\"title\": \"title1\",\"email\": \"" + randomString + "@gmail.com\",\"officeNo\": \"(-1234&567+)\", \"mobileNo\": \"(-1234&567+)\",\"otherNo\": \"(-1234&567+)\",\"note\": \"note\",\"addressesList\": [ {\"street1\": \"street1\",\"street2\": \"street2\",\"city\": \"city1\",\"state\": \"s1\",\"zip\": \"123\",\"countryId\": 21}], \"pclId\": 100}";
            var response = APICommonMethods.ResponseFromPutRequest(AppNameHelper.ApiBaseUrl, "/generalsettings/contactinfo", Jsonbody);
            Console.WriteLine(response.Content);
            var toasterMessageID = Regex.Match(response.Content, @"\d+").Value;
            String OutputVal = response.Content.ToString().Replace("\"", "");
            String txt = "Contact Information for " + toasterMessageID + " Updated Successfully.";
            if (response.IsSuccessful)
            {
                Assert.That((int)response.StatusCode, Is.EqualTo(200));
                Assert.That(OutputVal, Is.EqualTo(txt));
            }
            else
            {
                Console.WriteLine((int)response.StatusCode);
                Assert.Fail("Test Fail");
            }
        }

        // character are more than 100 in Note
        [Test]
        [Description("ClientSetting clientProfiles Put Note more50Char")]
        [Category("Smoke")]
        [TestCase("Automation_ClientProfile_Put Note More50Char")]
        public void ClientSetting_GeneralSettings_ContactInfo_Put_Note_more50Char(string APItoken)
        {
            string randomString = RandomString.GetString(Types.ALPHANUMERIC_MIXEDCASE, 15);
            string Jsonbody = "{\"contactId\":1,\"name\": \"apitest\",\"title\": \"apitesting\",\"email\": \"api@gmail.com\",\"officeNo\": \"12345678\", \"mobileNo\": \"123456789\",\"otherNo\": \"12345678910\",\"note\": \"apitestingapitestingapitestingapitestingapitestingapitestingapitestingapitestingapitestingapitestinga\",\"addressesList\": [ {\"street1\": \"street1\",\"street2\": \"street2\",\"city\": \"apitesting\",\"state\": \"s1\",\"zip\": \"123\",\"countryId\": 21}], \"pclId\": 100}";
            var response = APICommonMethods.ResponseFromPostRequest(AppNameHelper.ApiBaseUrl, "/generalsettings/contactinfo", Jsonbody);
            Console.WriteLine(response.Content);
            dynamic jsonResponse = JObject.Parse(response.Content);
            Console.WriteLine(jsonResponse);
            String ErrorMsg = jsonResponse.errors.Note[0];
            Assert.That(ErrorMsg, Is.EqualTo("Note can accept 100 characters or fewer. You entered 101 characters."));
            Assert.That((int)response.StatusCode, Is.EqualTo(400));
        }

        //character are more than 30 in Street 1/Street 2
        [Test]
        [Description("ClientSetting clientProfiles Put Street more30Char")]
        [Category("Smoke")]
        [TestCase("Automation_ClientProfile_Post_Street_More30Char")]
        public void ClientSetting_GeneralSettings_ContactInfo_Put_Street_more30Char(string APItoken)
        {
            string randomString = RandomString.GetString(Types.ALPHANUMERIC_MIXEDCASE, 15);
            string Jsonbody = "{\"contactId\":1,\"name\": \"apitest\",\"title\": \"apitesting\",\"email\": \"api@gmail.com\",\"officeNo\": \"12345678\", \"mobileNo\": \"123456789\",\"otherNo\": \"12345678910\",\"note\": \"apitest\",\"addressesList\": [ {\"street1\": \"apitestingapitestingapitestinga\",\"street2\": \"apitestingapitestingapitestinga\",\"city\": \"apitesting\",\"state\": \"s1\",\"zip\": \"123\",\"countryId\": 21}], \"pclId\": 100}";
            var response = APICommonMethods.ResponseFromPutRequest(AppNameHelper.ApiBaseUrl, "/generalsettings/contactinfo", Jsonbody);
            Console.WriteLine(response.Content);
            dynamic jsonResponse = JObject.Parse(response.Content);
            String ErrorMsg = jsonResponse.errors["AddressesList[0].Street1"][0];
            String ErrorMsg1 = jsonResponse.errors["AddressesList[0].Street2"][0];
            Assert.That(ErrorMsg, Is.EqualTo("Street1 can accept 30 characters or fewer. You entered 31 characters."));
            Assert.That((int)response.StatusCode, Is.EqualTo(400));
            Assert.That(ErrorMsg1, Is.EqualTo("Street2 can accept 30 characters or fewer. You entered 31 characters."));

        }

        //character are more than 2 in State
        [Test]
        [Description("ClientSetting clientProfiles Put State more2Char")]
        [Category("Smoke")]
        [TestCase("Automation_ClientProfile_Put_State_More2Char")]
        public void ClientSetting_GeneralSettings_ContactInfo_Put_State_more2Char(string APItoken)
        {
            string randomString = RandomString.GetString(Types.ALPHANUMERIC_MIXEDCASE, 15);
            string Jsonbody = "{\"contactId\":1,\"name\": \"apitest\",\"title\": \"apitesting\",\"email\": \"api@gmail.com\",\"officeNo\": \"12345678\", \"mobileNo\": \"123456789\",\"otherNo\": \"12345678910\",\"note\": \"apitest\",\"addressesList\": [ {\"street1\": \"apitesting\",\"street2\": \"street2\",\"city\": \"apitesting\",\"state\": \"mh1\",\"zip\": \"123\",\"countryId\": 21}], \"pclId\": 100}";
            var response = APICommonMethods.ResponseFromPutRequest(AppNameHelper.ApiBaseUrl, "/generalsettings/contactinfo", Jsonbody);
            Console.WriteLine(response.Content);
            dynamic jsonResponse = JObject.Parse(response.Content);
            Console.WriteLine(jsonResponse);
            String ErrorMsg = jsonResponse.errors["AddressesList[0].State"][0];
            Assert.That(ErrorMsg, Is.EqualTo("State can accept 2 characters or fewer. You entered 3 characters."));
            Assert.That((int)response.StatusCode, Is.EqualTo(400));
        }

        //character are more than 10 in Zip
        [Test]
        [Description("ClientSetting clientProfiles Put ZIP more10Char")]
        [Category("Smoke")]
        [TestCase("Automation_ClientProfile_Put_Zip_More10Char")]
        public void ClientSetting_GeneralSettings_ContactInfo_Put_Zip_more10Char(string APItoken)
        {
            string randomString = RandomString.GetString(Types.ALPHANUMERIC_MIXEDCASE, 15);
            string Jsonbody = "{\"contactId\":1,\"name\": \"apitest\",\"title\": \"apitesting\",\"email\": \"api@gmail.com\",\"officeNo\": \"12345678\", \"mobileNo\": \"123456789\",\"otherNo\": \"12345678910\",\"note\": \"apitest\",\"addressesList\": [ {\"street1\": \"apitesting\",\"street2\": \"street2\",\"city\": \"apitesting\",\"state\": \"mh\",\"zip\": \"12345678910\",\"countryId\": 21}], \"pclId\": 100}";
            var response = APICommonMethods.ResponseFromPutRequest(AppNameHelper.ApiBaseUrl, "/generalsettings/contactinfo", Jsonbody);
            Console.WriteLine(response.Content);
            dynamic jsonResponse = JObject.Parse(response.Content);
            Console.WriteLine(jsonResponse);
            String ErrorMsg = jsonResponse.errors["AddressesList[0].Zip"][0];
            Assert.That(ErrorMsg, Is.EqualTo("Zip can accept 10 characters or fewer. You entered 11 characters."));
            Assert.That((int)response.StatusCode, Is.EqualTo(400));
        }

        //TEst method
        [Test]
        [Description("ClientSetting clientProfiles Put ZIP more10Char")]
        [Category("Smoke")]
        [TestCase("Automation_ClientProfile_Put_Zip_More10Char")]
        public void ClientSetting_DummyTestMethod(string APItoken)
        {
            string randomString = RandomString.GetString(Types.ALPHANUMERIC_MIXEDCASE, 15);
            string Jsonbody = "{\"contactId\":1,\"name\": \"apitest\",\"title\": \"apitesting\",\"email\": \"api@gmail.com\",\"officeNo\": \"12345678\", \"mobileNo\": \"123456789\",\"otherNo\": \"12345678910\",\"note\": \"apitest\",\"addressesList\": [ {\"street1\": \"apitesting\",\"street2\": \"street2\",\"city\": \"apitesting\",\"state\": \"mh\",\"zip\": \"12345678910\",\"countryId\": 21}], \"pclId\": 100}";
            var response = APICommonMethods.ResponseFromPutRequest(AppNameHelper.ApiBaseUrl, "/generalsettings/contactinfo", Jsonbody);
            Console.WriteLine(response.Content);
            dynamic jsonResponse = JObject.Parse(response.Content);
            Console.WriteLine(jsonResponse);
            String ErrorMsg = jsonResponse.errors["AddressesList[0].Zip"][0];
            Assert.That(ErrorMsg, Is.EqualTo("Zip can accept 10 characters or fewer. You entered 11 characters."));
            Assert.That((int)response.StatusCode, Is.EqualTo(400));
        }
    }
}

