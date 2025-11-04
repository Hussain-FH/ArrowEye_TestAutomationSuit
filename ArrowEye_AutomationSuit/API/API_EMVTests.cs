using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ArrowEye_Automation_Framework.Common;
using Newtonsoft.Json.Linq;
using NUnit.Framework;
using RandomString4Net;
using RestSharp.Authenticators;
using RestSharp;
using Newtonsoft.Json;
using System.Collections;
using System.Text.RegularExpressions;

namespace ArrowEye_Automation_Framework.API
{
    public class API_EMVTests
    {
        //string BaseURL = "https://62fush641m.execute-api.us-west-2.amazonaws.com";

        [Test]
        [Description("EMV Authentication GetMethod")]
        [Category("Smoke")]
        [TestCase("Automation_EMV_Authentication_GetRequestMethod")]
        public void EMV_Authentication_Get_RequestMethod(string APItoken)
        {

            var response = APICommonMethods.ResponseFromGETrequest(AppNameHelper.ApiBaseUrl, "/emvauthentications?pclId=108");
            //Console.WriteLine((int)response.StatusCode);
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


        //EMV Authentication Id Values from response and DB
        [Test]
        [Description("EMV Authentication Get by PclId with DB")]
        [Category("Smoke")]
        [TestCase("Automation_EMV Authentication_Get_PclIds_DB")]
        public void EMV_Authentication_Get_PclIds_With_DB(string APItoken)
        {
            var response = APICommonMethods.ResponseFromGETrequest(AppNameHelper.ApiBaseUrl, "/emvauthentications?pclId=108");
            //Console.WriteLine(response.Content);
            JArray jsonArray = JArray.Parse(response.Content);
            ArrayList ApiIDValue = new ArrayList();
            foreach (JObject var in jsonArray)
            {
                ApiIDValue.Add((int)var.GetValue("id"));
            }
            ApiIDValue.Sort();
            foreach (var it in ApiIDValue)
            {
                //jsonArray[var]["id"];
                //var var = va;
                Console.WriteLine(it);
            }
            string sql = "SELECT distinct ea.codeid AS authenticationtypeid FROM dbo.emvconfiguration ec INNER JOIN dbo.pclmap pc ON pc.pcl_mapid = ec.pclid INNER JOIN dbo.emvmodule em ON em.id = ec.moduleid INNER JOIN dbo.emvpersonalizationscript eps ON eps.id = ec.personalizationscriptid INNER JOIN dbo.emvcardprofile ep ON ep.id = ec.cardprofileid INNER JOIN dbo.emvissuer ei ON ei.id = ep.emvissuerid INNER JOIN dbo.code ea ON ea.codeid = ec.authenticationtypeid order by authenticationtypeid";
            ArrayList ls = DBConnect_Methods.SelectMethod(sql);
            Console.WriteLine("DB valuesssssss");
            Console.WriteLine("APICount"+ ApiIDValue.Count);
            Console.WriteLine("DB Count"+ls.Count);

            foreach (var item in ls)
            {
                //jsonArray[var]["id"];
                //var var = va;
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

        //Incorrect query parameter value
        [Test]
        [Description("EMV Authentication GetMethod")]
        [Category("Smoke")]
        [TestCase("Automation_EMV_Authentication_GetRequestMethod")]
        public void EMV_Authentication_Get_Invalid_Parameter(string APItoken)
        {

            var response = APICommonMethods.ResponseFromGETrequest(AppNameHelper.ApiBaseUrl, "/emvauthentications?pclId=345$kkk");
            //Console.WriteLine((int)response.StatusCode);
            Assert.That((int)response.StatusCode, Is.EqualTo(400));
            Console.WriteLine(response.Content);
        }

        //https://62fush641m.execute-api.us-west-2.amazonaws.com/emvauthentications
        [Test]
        [Description("EMV Authentication PostMethod")]
        [Category("Smoke")]
        [TestCase("Automation_EMV_Authentication_PostRequestMethod")]
        public void EMV_Authentication_CreateMethod(string APItoken)
        {
            string randomString = RandomString.GetString(Types.ALPHANUMERIC_MIXEDCASE, 15);
            string Jsonbody = "{\"authenticationType\":\"" + randomString + "\"}";
            var response = APICommonMethods.ResponseFromPostRequest(AppNameHelper.ApiBaseUrl, "/emvauthentications", Jsonbody);
            //Console.WriteLine(response.Content);
            var toasterMessageID = Regex.Match(response.Content, @"\d+").Value;
            String OutputVal = response.Content.ToString().Replace("\"", "");
            String txt = "EMV Authentication Type " + toasterMessageID + " Added Successfully.";

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

        // Authentication Type field value is not entered
        [Test]
        [Description("EMV Authentication Post Blank AuthenticationType")]
        [Category("Smoke")]
        [TestCase("Automation_EMV_Authentication_Blank_Post_AuthenticationType")]
        public void EMV_Authentication_Post_Blank_AuthenticationType(string APItoken)
        {
            //string randomString = RandomString.GetString(Types.ALPHANUMERIC_MIXEDCASE, 15);
            string Jsonbody = "{\"authenticationType\":\"\"}";
            var response = APICommonMethods.ResponseFromPostRequest(AppNameHelper.ApiBaseUrl, "/emvauthentications", Jsonbody);
            //Console.WriteLine(response.Content);
            dynamic jsonResponse = JObject.Parse(response.Content);
            String ErrorMsg = jsonResponse.errors.AuthenticationType[0];
            //dynamic jsonResponse =JsonConvert.DeserializeObject(response.Content);
            Console.WriteLine(jsonResponse.errors.AuthenticationType[0]);
            Assert.That(ErrorMsg, Is.EqualTo("The AuthenticationType field is required."));
            Assert.That((int)response.StatusCode, Is.EqualTo(400));
        }

        // Enters a Authentication value that already exists
        //Need to change
        [Test]
        [Description("EMV Authentication already exists AuthenticationType")]
        [Category("Smoke")]
        [TestCase("Automation_EMV_Authentication_already_exists_AuthenticationType")]
        public void EMV_Authentication_Post_already_exists_AuthenticationType(string APItoken)
        {
            //string randomString = RandomString.GetString(Types.ALPHANUMERIC_MIXEDCASE, 15);
            string Jsonbody = "{\"authenticationType\":\"EMV12\"}";
            var response = APICommonMethods.ResponseFromPostRequest(AppNameHelper.ApiBaseUrl, "/emvauthentications", Jsonbody);
            //Console.WriteLine(response.Content);
            dynamic jsonResponse = JObject.Parse(response.Content);
            String ErrorMsg = jsonResponse.errors.CodeId[0];
            //dynamic jsonResponse =JsonConvert.DeserializeObject(response.Content);
            Console.WriteLine(jsonResponse.errors.CodeId[0]);
            Assert.That(ErrorMsg, Is.EqualTo("EMV Authentication Type  EMV12 already exists."));
            Assert.That((int)response.StatusCode, Is.EqualTo(404));

        }
        //AuthenticationType have more than 50 Char
        [Test]
            [Description("EMV Authentication More than 50 Char AuthenticationType")]
            [Category("Smoke")]
            [TestCase("Automation_EMV_Authentication_More than 50 Char AuthenticationType")]
            public void EMV_Authentication_Post_More_than50Char_AuthenType(string APItoken)
            {
                //string randomString = RandomString.GetString(Types.ALPHANUMERIC_MIXEDCASE, 15);
                string Jsonbody = "{\"authenticationType\":\"apitestingapitestingapitestingapitestingapitestinga\"}";
                var response = APICommonMethods.ResponseFromPostRequest(AppNameHelper.ApiBaseUrl, "/emvauthentications", Jsonbody);
                //Console.WriteLine(response.Content);
                dynamic jsonResponse = JObject.Parse(response.Content);
                String ErrorMsg = jsonResponse.errors.AuthenticationType[0];
                //dynamic jsonResponse =JsonConvert.DeserializeObject(response.Content);
                Console.WriteLine(jsonResponse.errors.AuthenticationType[0]);
                Assert.That(ErrorMsg, Is.EqualTo("AuthenticationType can accept 50 characters or fewer. You entered 51 characters."));
                Assert.That((int)response.StatusCode, Is.EqualTo(400));
            }


        [Test]
        [Description("EMV Authentication PutMethod")]
        [Category("Smoke")]
        [TestCase("Automation_EMV_Authentication_PutRequestMethod")]
        public void EMV_Authentication_PutRequestMethod(string APItoken)
        {
            string Jsonbody = "{\"authenticationType\":\"EMV11\",\"id\":11}";
            var response = APICommonMethods.ResponseFromPutRequest(AppNameHelper.ApiBaseUrl, "/emvauthentications", Jsonbody);
            var toasterMessageID = Regex.Match(response.Content, @"\d+").Value;
            String OutputVal = response.Content.ToString().Replace("\"", "");
            String txt = "EMV Authentication Type " + toasterMessageID + " Updated Successfully.";

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

        //Authentication Delete
        [Test]
        [Description("EMV Authentication DeleteMethod")]
        [Category("Smoke")]
        [TestCase("Automation_EMV_Authentication_DeleteRequestMethod")]
        public void EMV_Authentication_Delete_AfterCreate(string APItoken)
        {

            string randomString = RandomString.GetString(Types.ALPHANUMERIC_MIXEDCASE, 15);
            string Jsonbody = "{\"authenticationType\":\"" + randomString + "\"}";
            var response = APICommonMethods.ResponseFromPostRequest(AppNameHelper.ApiBaseUrl, "/emvauthentications", Jsonbody);
            var toasterMessage_Text = response.Content;
            var toasterMessageID = Regex.Match(toasterMessage_Text, @"\d+").Value;
            Console.WriteLine(toasterMessageID);
            string JsonbodyDel = "{\"id\":\"" + toasterMessageID + "\"}";
            var responseDel = APICommonMethods.ResponseFromDeleteRequest(AppNameHelper.ApiBaseUrl, "/emvauthentications", JsonbodyDel);
            //Console.WriteLine(responseDel.Content);
            String OutputVal = responseDel.Content.ToString().Replace("\"","");
             //Console.WriteLine(OutputVal);
            //Console.WriteLine((int)responseDel.StatusCode);
            String txt = "EMV Authentication type "+ toasterMessageID+" deleted Successfully.";
            Console.WriteLine(txt);
            if (response.IsSuccessful)
            {
                Assert.That((int)responseDel.StatusCode, Is.EqualTo(200));
                //Assert.That(response.Content, Does.Not.Contain("One or more validation errors occurred."));
                Assert.That(OutputVal, Is.EqualTo(txt));

            }
            else
            {
                Console.WriteLine((int)response.StatusCode);
                Assert.Fail("Test Fail");
            }

        }


        //Deleting already deleted Entry
        [Test]
        [Description("EMV Authentication Delete_AlreadyDelMethod")]
        [Category("Smoke")]
        [TestCase("Automation_EMV_Authentication_Delete_AlreadyDelMethod")]        
        public void EMV_Authentication_Delete_AlreadyDeleted(string APItoken)
        {

            string randomString = RandomString.GetString(Types.ALPHANUMERIC_MIXEDCASE, 15);
            string Jsonbody = "{\"authenticationType\":\"" + randomString + "\"}";
            var response = APICommonMethods.ResponseFromPostRequest(AppNameHelper.ApiBaseUrl, "/emvauthentications", Jsonbody);
            var toasterMessage_Text = response.Content;
            var toasterMessageID = Regex.Match(toasterMessage_Text, @"\d+").Value;
            Console.WriteLine(toasterMessageID);
            string JsonbodyDel = "{\"id\":\"" + toasterMessageID + "\"}";
            var responseDel = APICommonMethods.ResponseFromDeleteRequest(AppNameHelper.ApiBaseUrl, "/emvauthentications", JsonbodyDel);
            String OutputVal = responseDel.Content.ToString().Replace("\"", "");
            //Console.WriteLine(OutputVal);
            String txt = "EMV Authentication type " + toasterMessageID + " deleted Successfully.";
            Console.WriteLine(txt);
            Assert.That(OutputVal, Is.EqualTo(txt));

            var responseAlreadyDel = APICommonMethods.ResponseFromDeleteRequest(AppNameHelper.ApiBaseUrl, "/emvauthentications", JsonbodyDel);
            String OutputAlreadyVal = responseAlreadyDel.Content.ToString().Replace("\"", "");
            Console.WriteLine(OutputAlreadyVal);

            String txtAlreadyDel = "EMV Authentication with Id:"+toasterMessageID +" doesn't exist!";
            Console.WriteLine(txtAlreadyDel);

            if (response.IsSuccessful)
            {
                Assert.That((int)responseDel.StatusCode, Is.EqualTo(200));
                //Assert.That(response.Content, Does.Not.Contain("One or more validation errors occurred."));
                Assert.That(OutputAlreadyVal, Is.EqualTo(txtAlreadyDel));

            }
            else
            {
                Console.WriteLine((int)response.StatusCode);
                Assert.Fail("Test Fail");
            }

        }

        //https://62fush641m.execute-api.us-west-2.amazonaws.com/emvauthentications
        //https://62fush641m.execute-api.us-west-2.amazonaws.com/cardprofiles
        [Test]
        [Description("EMV Card Profile Create")]
        [Category("Smoke")]
        [TestCase("Automation_EMV_CardProfile_Create")]
        public void EMV_CardProfile_Create(string APItoken)
        {
            //string randomString = RandomString.GetString(Types.ALPHANUMERIC_MIXEDCASE, 15);
            string Jsonbody = "{\"name\": \"Profile12\",\"description\": \"Desc-Profile12\",\"issuerId\": 28,\"expirationDate\": \"2024-07-25\"}";
            var response = APICommonMethods.ResponseFromPostRequest(AppNameHelper.ApiBaseUrl, "/cardprofiles", Jsonbody);
            Console.WriteLine(response.Content);
            Assert.That((int)response.StatusCode, Is.EqualTo(201));
            Console.WriteLine(response.StatusDescription);            
            var toasterMessageID = Regex.Match(response.Content, @"\d+").Value;
            String OutputVal = response.Content.ToString().Replace("\"", "");
            String txt = "EMV Card Profile "+ toasterMessageID +" added Successfully.";

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

        //Blank Name
        [Test]
        [Description("EMV Card Profile Create BlankName")]
        [Category("Smoke")]
        [TestCase("Automation_EMV_Card_Profile_Create_BlankName")]
        public void EMV_Card_Profile_Create_BlankName(string APItoken)
        {

            //string randomString = RandomString.GetString(Types.ALPHANUMERIC_MIXEDCASE, 15);
            string Jsonbody = "{\"name\": \"\",\"description\": \"Desc-Profile12\",\"issuerId\": 28,\"expirationDate\": \"2024-07-25\"}";
            var response = APICommonMethods.ResponseFromPostRequest(AppNameHelper.ApiBaseUrl, "/cardprofiles", Jsonbody);
            Console.WriteLine(response.Content);
            dynamic jsonResponse = JObject.Parse(response.Content);
            String ErrorMsg = jsonResponse.errors.Name[0];
            Console.WriteLine(jsonResponse);
            //dynamic jsonResponse =JsonConvert.DeserializeObject(response.Content);
            Console.WriteLine(jsonResponse.errors.Name[0]);
            Assert.That(ErrorMsg, Is.EqualTo("The Name field is required."));
            Assert.That((int)response.StatusCode, Is.EqualTo(400));

        }
        //Name_MoreThan50Char
        [Test]
        [Description("EMV Card Profile Create Name_MoreThan50Char")]
        [Category("Smoke")]
        [TestCase("Automation_EMV_Card_Profile_Create_Name_MoreThan50Char")]
        public void EMV_Card_Profile_Create_Name_MoreThan50Char(string APItoken)
        {

            //string randomString = RandomString.GetString(Types.ALPHANUMERIC_MIXEDCASE, 15);
            string Jsonbody = "{\"name\": \"apitestingapitestingapitestingapitestingapitestinga\",\"description\": \"Desc-Profile12\",\"issuerId\": 28,\"expirationDate\": \"2024-07-25\"}";
            var response = APICommonMethods.ResponseFromPostRequest(AppNameHelper.ApiBaseUrl, "/cardprofiles", Jsonbody);
            Console.WriteLine(response.Content);
            dynamic jsonResponse = JObject.Parse(response.Content);
            String ErrorMsg = jsonResponse.errors.Name[0];
            Console.WriteLine(jsonResponse);
            //dynamic jsonResponse =JsonConvert.DeserializeObject(response.Content);
            Console.WriteLine(jsonResponse.errors.Name[0]);
            Assert.That(ErrorMsg, Is.EqualTo("Name can accept 50 characters or fewer. You entered 51 characters."));
            Assert.That((int)response.StatusCode, Is.EqualTo(400));

        }

        //Blank Issuer Field
        
        [Test]
        [Description("EMV Card Profile Create Blank_IssuerField")]
        [Category("Smoke")]
        [TestCase("Automation_EMV_Card_Profile_Create_Blank_IssuerField")]
        public void EMV_Card_Profile_Create_Blank_IssuerField(string APItoken)
        {

            //string randomString = RandomString.GetString(Types.ALPHANUMERIC_MIXEDCASE, 15);
            string Jsonbody = "{\"name\":\"Profile12\",\"description\": \"Desc-Profile12\",\"issuerId\":\"0\",\"expirationDate\": \"2024-07-25\"}";
            var response = APICommonMethods.ResponseFromPostRequest(AppNameHelper.ApiBaseUrl, "/cardprofiles", Jsonbody);
            Console.WriteLine(response.Content);
            dynamic jsonResponse = JObject.Parse(response.Content);
            String ErrorMsg = jsonResponse.errors.IssuerId[0];
            Console.WriteLine(jsonResponse);
            //dynamic jsonResponse =JsonConvert.DeserializeObject(response.Content); 
            Assert.That(ErrorMsg, Is.EqualTo("The IssuerId field is required."));
            Assert.That((int)response.StatusCode, Is.EqualTo(400));

        }



        [Test]
        [Description("EMV Card Profile Update")]
        [Category("Smoke")]
        [TestCase("Automation_EMV_CardProfile_Update")]
        public void EMV_CardProfile_Update(string APItoken)
        {
            string Jsonbody = "{\"name\":\"Profile85\",\"description\":\"Desc-Profile85\",\"issuerId\":28,\"expirationDate\":\"2024-07-25\",\"cardProfileId\":85}";
            var response = APICommonMethods.ResponseFromPutRequest(AppNameHelper.ApiBaseUrl, "/cardprofiles", Jsonbody);
            Console.WriteLine(response.Content);
            var toasterMessageID = Regex.Match(response.Content, @"\d+").Value;
            String OutputVal = response.Content.ToString().Replace("\"", "");
            String txt = "EMV Card Profile " +toasterMessageID +" updated Successfully.";

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

        //Blank Name
        [Test]
        [Description("EMV Card Profile Update BlankName")]
        [Category("Smoke")]
        [TestCase("Automation_EMV_Card_Profile_Update_BlankName")]
        public void EMV_Card_Profile_Update_BlankName(string APItoken)
        {

            //string randomString = RandomString.GetString(Types.ALPHANUMERIC_MIXEDCASE, 15);
            string Jsonbody = "{\"name\": \"\",\"description\": \"Desc-Profile12\",\"issuerId\": 28,\"expirationDate\": \"2024-07-25\",\"cardProfileId\":85}";
            var response = APICommonMethods.ResponseFromPutRequest(AppNameHelper.ApiBaseUrl, "/cardprofiles", Jsonbody);
            Console.WriteLine(response.Content);
            dynamic jsonResponse = JObject.Parse(response.Content);
            String ErrorMsg = jsonResponse.errors.Name[0];
            Console.WriteLine(jsonResponse);
            //dynamic jsonResponse =JsonConvert.DeserializeObject(response.Content);
            Console.WriteLine(jsonResponse.errors.Name[0]);
            Assert.That(ErrorMsg, Is.EqualTo("The Name field is required."));
            Assert.That((int)response.StatusCode, Is.EqualTo(400));

        }
        //Name_MoreThan50Char
        [Test]
        [Description("EMV Card Profile Update Name_MoreThan50Char")]
        [Category("Smoke")]
        [TestCase("Automation_EMV_Card_Profile_Update_Name_MoreThan50Char")]
        public void EMV_Card_Profile_Update_Name_MoreThan50Char(string APItoken)
        {

            //string randomString = RandomString.GetString(Types.ALPHANUMERIC_MIXEDCASE, 15);
            string Jsonbody = "{\"name\": \"apitestingapitestingapitestingapitestingapitestinga\",\"description\": \"Desc-Profile12\",\"issuerId\": 28,\"expirationDate\": \"2024-07-25\",\"cardProfileId\":85}";
            var response = APICommonMethods.ResponseFromPutRequest(AppNameHelper.ApiBaseUrl, "/cardprofiles", Jsonbody);
            Console.WriteLine(response.Content);
            dynamic jsonResponse = JObject.Parse(response.Content);
            String ErrorMsg = jsonResponse.errors.Name[0];
            Console.WriteLine(jsonResponse);
            //dynamic jsonResponse =JsonConvert.DeserializeObject(response.Content);
            Console.WriteLine(jsonResponse.errors.Name[0]);
            Assert.That(ErrorMsg, Is.EqualTo("Name can accept 50 characters or fewer. You entered 51 characters."));
            Assert.That((int)response.StatusCode, Is.EqualTo(400));

        }

        //Blank Issuer Field

        [Test]
        [Description("EMV Card Profile Update Name_Blank_IssuerField")]
        [Category("Smoke")]
        [TestCase("Automation_EMV_Card_Profile_Update_Blank_IssuerField")]
        public void EMV_Card_Profile_Update_Blank_IssuerField(string APItoken)
        {

            //string randomString = RandomString.GetString(Types.ALPHANUMERIC_MIXEDCASE, 15);
            string Jsonbody = "{\"name\":\"Profile12\",\"description\": \"Desc-Profile12\",\"issuerId\":\"0\",\"expirationDate\": \"2024-07-25\",\"cardProfileId\":85}";
            var response = APICommonMethods.ResponseFromPutRequest(AppNameHelper.ApiBaseUrl, "/cardprofiles", Jsonbody);
            Console.WriteLine(response.Content);
            dynamic jsonResponse = JObject.Parse(response.Content);
            String ErrorMsg = jsonResponse.errors.IssuerId[0];
            Console.WriteLine(jsonResponse);
            //dynamic jsonResponse =JsonConvert.DeserializeObject(response.Content); 
            Assert.That(ErrorMsg, Is.EqualTo("The IssuerId field is required."));
            Assert.That((int)response.StatusCode, Is.EqualTo(400));

        }

        [Test]
        [Description("EMV Card Profile Get")]
        [Category("Smoke")]
        [TestCase("Automation_EMV_Card_Profile_Get")]
        public void EMV_Card_Profile_Get(string APItoken)
        {

            var response = APICommonMethods.ResponseFromGETrequest(AppNameHelper.ApiBaseUrl, "/cardprofiles");
            //Console.WriteLine(response.Content);
            Assert.That((int)response.StatusCode, Is.EqualTo(200));

        }


        [Test]
        [Description("EMV Configurations Create")]
        [Category("Smoke")]
        [TestCase("Automation_EMV_Configurations_Create")]
        public void EMV_Configurations_Create(string APItoken)
        {
            //string randomString = RandomString.GetString(Types.ALPHANUMERIC_MIXEDCASE, 15);
            string Jsonbody = "{\"name\": \"Config12\",\"issuerId\": 28,\"cardProfileId\": 85,\"personalizationScriptId\": 60,\"moduleId\": 8,\"authenticationTypeId\": 1021,\"inTest\": true,\"pclId\": 108,\"isCreate\": true,\"id\": 2}";
            var response = APICommonMethods.ResponseFromPostRequest(AppNameHelper.ApiBaseUrl, "/configurations", Jsonbody);
            //Console.WriteLine(response.Content);
            var toasterMessageID = Regex.Match(response.Content, @"\d+").Value;
            String OutputVal = response.Content.ToString().Replace("\"", "");
            String txt = "EMV Configuration " + toasterMessageID + " Added Successfully.";

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

        //Name field value is not entered
        [Test]
        [Description("EMV Configurations Create Blank Name")]
        [Category("Smoke")]
        [TestCase("Automation_EMV_Configurations_Create_Blank Name")]
        public void EMV_Configurations_Create_Blank_Name(string APItoken)
        {
            //string randomString = RandomString.GetString(Types.ALPHANUMERIC_MIXEDCASE, 15);
            string Jsonbody = "{\"name\": \"\",\"issuerId\": 28,\"cardProfileId\": 85,\"personalizationScriptId\": 60,\"moduleId\": 8,\"authenticationTypeId\": 1021,\"inTest\": true,\"pclId\": 108,\"isCreate\": true,\"id\": 2}";
            var response = APICommonMethods.ResponseFromPostRequest(AppNameHelper.ApiBaseUrl, "/configurations", Jsonbody);
            Console.WriteLine(response.Content);
            dynamic jsonResponse = JObject.Parse(response.Content);
            String ErrorMsg = jsonResponse.errors.Name[0];
            //dynamic jsonResponse =JsonConvert.DeserializeObject(response.Content);
            Console.WriteLine(jsonResponse.errors.Name[0]);
            Assert.That(ErrorMsg, Is.EqualTo("The Name field is required."));
            Assert.That((int)response.StatusCode, Is.EqualTo(400));
        }

        //Name field Character limitations of 150 char
        [Test]
        [Description("EMV Configurations Create Name greater 150Char")]
        [Category("Smoke")]
        [TestCase("Automation_EMV_Configurations_Create_More_150Char")]
        public void EMV_Configurations_Create_Greater_150Char(string APItoken)
        {
            //string randomString = RandomString.GetString(Types.ALPHANUMERIC_MIXEDCASE, 15);
            string Jsonbody = "{\"name\": \"apitestingapitestingapitestingapitestingapitestingapitestingapitestingapitestingapitestingapitestingapitestingapitestingapitestingapitestingapitestingh\",\"issuerId\": 28,\"cardProfileId\": 85,\"personalizationScriptId\": 60,\"moduleId\": 8,\"authenticationTypeId\": 1021,\"inTest\": true,\"pclId\": 108,\"isCreate\": true,\"id\": 2}";
            var response = APICommonMethods.ResponseFromPostRequest(AppNameHelper.ApiBaseUrl, "/configurations", Jsonbody);
            Console.WriteLine(response.Content);
            dynamic jsonResponse = JObject.Parse(response.Content);
            String ErrorMsg = jsonResponse.errors.Name[0];
            //dynamic jsonResponse =JsonConvert.DeserializeObject(response.Content);
            Console.WriteLine(jsonResponse.errors.Name[0]);
            Assert.That(ErrorMsg, Is.EqualTo("Name can accept 150 characters or fewer. You entered 151 characters."));
            Assert.That((int)response.StatusCode, Is.EqualTo(400));
        }

        // Issuer field value is not entered
        [Test]
        [Description("EMV Configurations Create Blank Issuer field")]
        [Category("Smoke")]
        [TestCase("Automation_EMV_Configurations_Create_Blank_Issuer_field")]
        public void EMV_Configurations_Create_Blank_Issuer(string APItoken)
        {
            //string randomString = RandomString.GetString(Types.ALPHANUMERIC_MIXEDCASE, 15);
            string Jsonbody = "{\"name\": \"apitesting\",\"issuerId\": 0,\"cardProfileId\": 85,\"personalizationScriptId\": 60,\"moduleId\": 8,\"authenticationTypeId\": 1021,\"inTest\": true,\"pclId\": 108,\"isCreate\": true,\"id\": 2}";
            var response = APICommonMethods.ResponseFromPostRequest(AppNameHelper.ApiBaseUrl, "/configurations", Jsonbody);
            Console.WriteLine(response.Content);
            dynamic jsonResponse = JObject.Parse(response.Content);
            String ErrorMsg = jsonResponse.errors.IssuerId[0];
            //dynamic jsonResponse =JsonConvert.DeserializeObject(response.Content);
            Console.WriteLine(jsonResponse.errors.IssuerId[0]);
            Assert.That(ErrorMsg, Is.EqualTo("The IssuerId field is required."));
            Assert.That((int)response.StatusCode, Is.EqualTo(400));
        }


        //Card Profile field value is not entered
        [Test]
        [Description("EMV Configurations Create Blank Card Profile")]
        [Category("Smoke")]
        [TestCase("Automation_EMV_Configurations_Create_Blank_CardProfile")]
        public void EMV_Configurations_Create_Blank_CardProfile(string APItoken)
        {
            //string randomString = RandomString.GetString(Types.ALPHANUMERIC_MIXEDCASE, 15);
            string Jsonbody = "{\"name\": \"Config12\",\"issuerId\": 28,\"cardProfileId\": 0,\"personalizationScriptId\": 60,\"moduleId\": 8,\"authenticationTypeId\": 1021,\"inTest\": true,\"pclId\": 108,\"isCreate\": true,\"id\": 2}";
            var response = APICommonMethods.ResponseFromPostRequest(AppNameHelper.ApiBaseUrl, "/configurations", Jsonbody);
            Console.WriteLine(response.Content);
            dynamic jsonResponse = JObject.Parse(response.Content);
            String ErrorMsg = jsonResponse.errors.CardProfileId[0];
            //dynamic jsonResponse =JsonConvert.DeserializeObject(response.Content);
            Console.WriteLine(jsonResponse.errors.CardProfileId[0]);
            Assert.That(ErrorMsg, Is.EqualTo("The CardProfileId field is required."));
            Assert.That((int)response.StatusCode, Is.EqualTo(400));
        }

        //Personalization Script field value is not entered
        [Test]
        [Description("EMV Configurations Create Blank PersonalizationID")]
        [Category("Smoke")]
        [TestCase("Automation_EMV_Configurations_Create_Blank_PersonalizationID")]
        public void EMV_Configurations_Create_Blank_PersonalizationIDe(string APItoken)
        {
            //string randomString = RandomString.GetString(Types.ALPHANUMERIC_MIXEDCASE, 15);
            string Jsonbody = "{\"name\": \"Config12\",\"issuerId\": 28,\"cardProfileId\": 85,\"personalizationScriptId\": 0,\"moduleId\": 8,\"authenticationTypeId\": 1021,\"inTest\": true,\"pclId\": 108,\"isCreate\": true,\"id\": 2}";
            var response = APICommonMethods.ResponseFromPostRequest(AppNameHelper.ApiBaseUrl, "/configurations", Jsonbody);
            Console.WriteLine(response.Content);
            dynamic jsonResponse = JObject.Parse(response.Content);
            String ErrorMsg = jsonResponse.errors.PersonalizationScriptId[0];
            //dynamic jsonResponse =JsonConvert.DeserializeObject(response.Content);
            Console.WriteLine(jsonResponse.errors.PersonalizationScriptId[0]);
            Assert.That(ErrorMsg, Is.EqualTo("The PersonalizationScriptId field is required."));
            Assert.That((int)response.StatusCode, Is.EqualTo(400));
        }

        //Module field value is not entered

        [Test]
        [Description("EMV Configurations Create Blank Module field")]
        [Category("Smoke")]
        [TestCase("Automation_EMV_Configurations_Create_Blank_Module_field")]
        public void EMV_Configurations_Create_Blank_Module_field(string APItoken)
        {
            //string randomString = RandomString.GetString(Types.ALPHANUMERIC_MIXEDCASE, 15);
            string Jsonbody = "{\"name\": \"Config12\",\"issuerId\": 28,\"cardProfileId\": 85,\"personalizationScriptId\": 60,\"moduleId\": 0,\"authenticationTypeId\": 1021,\"inTest\": true,\"pclId\": 108,\"isCreate\": true,\"id\": 2}";
            var response = APICommonMethods.ResponseFromPostRequest(AppNameHelper.ApiBaseUrl, "/configurations", Jsonbody);
            Console.WriteLine(response.Content);
            dynamic jsonResponse = JObject.Parse(response.Content);
            String ErrorMsg = jsonResponse.errors.ModuleId[0];
            //dynamic jsonResponse =JsonConvert.DeserializeObject(response.Content);
            Console.WriteLine(jsonResponse.errors.ModuleId[0]);
            Assert.That(ErrorMsg, Is.EqualTo("The ModuleId field is required."));
            Assert.That((int)response.StatusCode, Is.EqualTo(400));
        }

        //AuthenticationTypeId field value is not entered

        [Test]
        [Description("EMV Configurations Create Blank AuthenticationTypeId")]
        [Category("Smoke")]
        [TestCase("Automation_EMV_Configurations_Create_Blank_AuthenticationTypeId")]
        public void EMV_Configurations_Create_Blank_AuthenticationTypeId(string APItoken)
        {
            //string randomString = RandomString.GetString(Types.ALPHANUMERIC_MIXEDCASE, 15);
            string Jsonbody = "{\"name\": \"Config12\",\"issuerId\": 28,\"cardProfileId\": 85,\"personalizationScriptId\": 60,\"moduleId\": 8,\"authenticationTypeId\": 0,\"inTest\": true,\"pclId\": 108,\"isCreate\": true,\"id\": 2}";
            var response = APICommonMethods.ResponseFromPostRequest(AppNameHelper.ApiBaseUrl, "/configurations", Jsonbody);
            Console.WriteLine(response.Content);
            dynamic jsonResponse = JObject.Parse(response.Content);
            String ErrorMsg = jsonResponse.errors.AuthenticationTypeId[0];
            //dynamic jsonResponse =JsonConvert.DeserializeObject(response.Content);
            Console.WriteLine(jsonResponse.errors.AuthenticationTypeId[0]);
            Assert.That(ErrorMsg, Is.EqualTo("The AuthenticationTypeId field is required."));
            Assert.That((int)response.StatusCode, Is.EqualTo(400));
        }


        [Test]
        [Description("EMV Configurations Update")]
        [Category("Smoke")]
        [TestCase("Automation_EMV_Configurations_Update")]
        public void EMV_Configurations_Update(string APItoken)
        {
            //string randomString = RandomString.GetString(Types.ALPHANUMERIC_MIXEDCASE, 15);
            string Jsonbody = "{\"name\": \"Config2\",\"issuerId\": 28,\"cardProfileId\": 85,\"personalizationScriptId\": 60,\"moduleId\": 8,\"authenticationTypeId\": 1021,\"inTest\": true,\"pclId\": 108,\"isCreate\": true,\"id\": 2}";
            var response = APICommonMethods.ResponseFromPutRequest(AppNameHelper.ApiBaseUrl, "/configurations", Jsonbody);
            //Console.WriteLine(response.Content);
            var toasterMessageID = Regex.Match(response.Content, @"\d+").Value;
            String OutputVal = response.Content.ToString().Replace("\"", "");
            String txt = "EMV Configuration " + toasterMessageID + " Updated Successfully.";

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

        //Name field value is not entered
        [Test]
        [Description("EMV Configurations Update Blank Name")]
        [Category("Smoke")]
        [TestCase("Automation_EMV_Configurations_Update_Blank Name")]
        public void EMV_Configurations_Update_Blank_Name(string APItoken)
        {
            //string randomString = RandomString.GetString(Types.ALPHANUMERIC_MIXEDCASE, 15);
            string Jsonbody = "{\"name\": \"\",\"issuerId\": 28,\"cardProfileId\": 85,\"personalizationScriptId\": 60,\"moduleId\": 8,\"authenticationTypeId\": 1021,\"inTest\": true,\"pclId\": 108,\"isCreate\": true,\"id\": 2}";
            var response = APICommonMethods.ResponseFromPutRequest(AppNameHelper.ApiBaseUrl, "/configurations", Jsonbody);
            Console.WriteLine(response.Content);
            dynamic jsonResponse = JObject.Parse(response.Content);
            String ErrorMsg = jsonResponse.errors.Name[0];
            //dynamic jsonResponse =JsonConvert.DeserializeObject(response.Content);
            Console.WriteLine(jsonResponse.errors.Name[0]);
            Assert.That(ErrorMsg, Is.EqualTo("The Name field is required."));
            Assert.That((int)response.StatusCode, Is.EqualTo(400));
        }

        //Name field Character limitations of 150 char
        [Test]
        [Description("EMV Configurations Update Name greater 150Char")]
        [Category("Smoke")]
        [TestCase("Automation_EMV_Configurations_Update_More_150Char")]
        public void EMV_Configurations_Update_Greater_150Char(string APItoken)
        {
            //string randomString = RandomString.GetString(Types.ALPHANUMERIC_MIXEDCASE, 15);
            string Jsonbody = "{\"name\": \"apitestingapitestingapitestingapitestingapitestingapitestingapitestingapitestingapitestingapitestingapitestingapitestingapitestingapitestingapitestingh\",\"issuerId\": 28,\"cardProfileId\": 85,\"personalizationScriptId\": 60,\"moduleId\": 8,\"authenticationTypeId\": 1021,\"inTest\": true,\"pclId\": 108,\"isCreate\": true,\"id\": 2}";
            var response = APICommonMethods.ResponseFromPutRequest(AppNameHelper.ApiBaseUrl, "/configurations", Jsonbody);
            Console.WriteLine(response.Content);
            dynamic jsonResponse = JObject.Parse(response.Content);
            String ErrorMsg = jsonResponse.errors.Name[0];
            //dynamic jsonResponse =JsonConvert.DeserializeObject(response.Content);
            Console.WriteLine(jsonResponse.errors.Name[0]);
            Assert.That(ErrorMsg, Is.EqualTo("Name can accept 150 characters or fewer. You entered 151 characters."));
            Assert.That((int)response.StatusCode, Is.EqualTo(400));
        }

        // Issuer field value is not entered
        [Test]
        [Description("EMV Configurations Update Blank Issuer field")]
        [Category("Smoke")]
        [TestCase("Automation_EMV_Configurations_Update_Blank_Issuer_field")]
        public void EMV_Configurations_Update_Blank_Issuer(string APItoken)
        {
            //string randomString = RandomString.GetString(Types.ALPHANUMERIC_MIXEDCASE, 15);
            string Jsonbody = "{\"name\": \"apitesting\",\"issuerId\": 0,\"cardProfileId\": 85,\"personalizationScriptId\": 60,\"moduleId\": 8,\"authenticationTypeId\": 1021,\"inTest\": true,\"pclId\": 108,\"isCreate\": true,\"id\": 2}";
            var response = APICommonMethods.ResponseFromPutRequest(AppNameHelper.ApiBaseUrl, "/configurations", Jsonbody);
            Console.WriteLine(response.Content);
            dynamic jsonResponse = JObject.Parse(response.Content);
            String ErrorMsg = jsonResponse.errors.IssuerId[0];
            //dynamic jsonResponse =JsonConvert.DeserializeObject(response.Content);
            Console.WriteLine(jsonResponse.errors.IssuerId[0]);
            Assert.That(ErrorMsg, Is.EqualTo("The IssuerId field is required."));
            Assert.That((int)response.StatusCode, Is.EqualTo(400));
        }


        //Card Profile field value is not entered
        [Test]
        [Description("EMV Configurations Update Blank Card Profile")]
        [Category("Smoke")]
        [TestCase("Automation_EMV_Configurations_Update_Blank_CardProfile")]
        public void EMV_Configurations_Update_Blank_CardProfile(string APItoken)
        {
            //string randomString = RandomString.GetString(Types.ALPHANUMERIC_MIXEDCASE, 15);
            string Jsonbody = "{\"name\": \"Config12\",\"issuerId\": 28,\"cardProfileId\": 0,\"personalizationScriptId\": 60,\"moduleId\": 8,\"authenticationTypeId\": 1021,\"inTest\": true,\"pclId\": 108,\"isCreate\": true,\"id\": 2}";
            var response = APICommonMethods.ResponseFromPutRequest(AppNameHelper.ApiBaseUrl, "/configurations", Jsonbody);
            Console.WriteLine(response.Content);
            dynamic jsonResponse = JObject.Parse(response.Content);
            String ErrorMsg = jsonResponse.errors.CardProfileId[0];
            //dynamic jsonResponse =JsonConvert.DeserializeObject(response.Content);
            Console.WriteLine(jsonResponse.errors.CardProfileId[0]);
            Assert.That(ErrorMsg, Is.EqualTo("The CardProfileId field is required."));
            Assert.That((int)response.StatusCode, Is.EqualTo(400));
        }

        //Personalization Script field value is not entered
        [Test]
        [Description("EMV Configurations Update Blank PersonalizationID")]
        [Category("Smoke")]
        [TestCase("Automation_EMV_Configurations_Update_Blank_PersonalizationID")]
        public void EMV_Configurations_Update_Blank_PersonalizationIDe(string APItoken)
        {
            //string randomString = RandomString.GetString(Types.ALPHANUMERIC_MIXEDCASE, 15);
            string Jsonbody = "{\"name\": \"Config12\",\"issuerId\": 28,\"cardProfileId\": 85,\"personalizationScriptId\": 0,\"moduleId\": 8,\"authenticationTypeId\": 1021,\"inTest\": true,\"pclId\": 108,\"isCreate\": true,\"id\": 2}";
            var response = APICommonMethods.ResponseFromPutRequest(AppNameHelper.ApiBaseUrl, "/configurations", Jsonbody);
            Console.WriteLine(response.Content);
            dynamic jsonResponse = JObject.Parse(response.Content);
            String ErrorMsg = jsonResponse.errors.PersonalizationScriptId[0];
            //dynamic jsonResponse =JsonConvert.DeserializeObject(response.Content);
            Console.WriteLine(jsonResponse.errors.PersonalizationScriptId[0]);
            Assert.That(ErrorMsg, Is.EqualTo("The PersonalizationScriptId field is required."));
            Assert.That((int)response.StatusCode, Is.EqualTo(400));
        }

        //Module field value is not entered

        [Test]
        [Description("EMV Configurations Update Blank Module field")]
        [Category("Smoke")]
        [TestCase("Automation_EMV_Configurations_Update_Blank_Module_field")]
        public void EMV_Configurations_Update_Blank_Module_field(string APItoken)
        {
            //string randomString = RandomString.GetString(Types.ALPHANUMERIC_MIXEDCASE, 15);
            string Jsonbody = "{\"name\": \"Config12\",\"issuerId\": 28,\"cardProfileId\": 85,\"personalizationScriptId\": 60,\"moduleId\": 0,\"authenticationTypeId\": 1021,\"inTest\": true,\"pclId\": 108,\"isCreate\": true,\"id\": 2}";
            var response = APICommonMethods.ResponseFromPutRequest(AppNameHelper.ApiBaseUrl, "/configurations", Jsonbody);
            Console.WriteLine(response.Content);
            dynamic jsonResponse = JObject.Parse(response.Content);
            String ErrorMsg = jsonResponse.errors.ModuleId[0];
            //dynamic jsonResponse =JsonConvert.DeserializeObject(response.Content);
            Console.WriteLine(jsonResponse.errors.ModuleId[0]);
            Assert.That(ErrorMsg, Is.EqualTo("The ModuleId field is required."));
            Assert.That((int)response.StatusCode, Is.EqualTo(400));
        }

        //AuthenticationTypeId field value is not entered

        [Test]
        [Description("EMV Configurations Update Blank AuthenticationTypeId")]
        [Category("Smoke")]
        [TestCase("Automation_EMV_Configurations_Update_Blank_AuthenticationTypeId")]
        public void EMV_Configurations_Update_Blank_AuthenticationTypeId(string APItoken)
        {
            //string randomString = RandomString.GetString(Types.ALPHANUMERIC_MIXEDCASE, 15);
            string Jsonbody = "{\"name\": \"Config12\",\"issuerId\": 28,\"cardProfileId\": 85,\"personalizationScriptId\": 60,\"moduleId\": 8,\"authenticationTypeId\": 0,\"inTest\": true,\"pclId\": 108,\"isCreate\": true,\"id\": 2}";
            var response = APICommonMethods.ResponseFromPutRequest(AppNameHelper.ApiBaseUrl, "/configurations", Jsonbody);
            Console.WriteLine(response.Content);
            dynamic jsonResponse = JObject.Parse(response.Content);
            String ErrorMsg = jsonResponse.errors.AuthenticationTypeId[0];
            //dynamic jsonResponse =JsonConvert.DeserializeObject(response.Content);
            Console.WriteLine(jsonResponse.errors.AuthenticationTypeId[0]);
            Assert.That(ErrorMsg, Is.EqualTo("The AuthenticationTypeId field is required."));
            Assert.That((int)response.StatusCode, Is.EqualTo(400));
        }


        [Test]
        [Description("EMV Configurations Get by AutheTypeId")]
        [Category("Smoke")]
        [TestCase("Automation_EMV_Configurations_Get_By_AutheTypeId")]
        public void EMV_Configurations_Get_By_AutheTypeId(string APItoken)
        {

            var response = APICommonMethods.ResponseFromGETrequest(AppNameHelper.ApiBaseUrl, "/configurations?AuthenticationTypeId=1021");
            //Console.WriteLine(response.Content);
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

        //EMVConfigurations ID Values from response and DB
        [Test]
        [Description("EMVConfigurations Get authenticationtypeid Match with DB")]
        [Category("Smoke")]
        [TestCase("Automation_EMVConfigurations_Get_authenticationtypeid_DB")]
        public void EMVConfiguration_Get_authenticationtypeid_DB(string APItoken)
        {
            var response = APICommonMethods.ResponseFromGETrequest(AppNameHelper.ApiBaseUrl,"/configurations?AuthenticationTypeId=1021");
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
            Console.WriteLine("Code for DB Coonection and retrieving values");
            string sql = "SELECT * FROM dbo.emvconfiguration where dbo.emvconfiguration.authenticationtypeid =1021 order by id";
            ArrayList ls = DBConnect_Methods.SelectMethod(sql);
            //Console.WriteLine(ls);
            foreach (var item in ls)
            {
                Console.WriteLine(item);
            }
            //Console.WriteLine("Count is "+ApiIDValue.Count);
            //Console.WriteLine("Count is " + ls.Count);
            if ((ApiIDValue.ToArray() as IStructuralEquatable).Equals(ls.ToArray(), EqualityComparer<int>.Default))
            {
                Assert.Pass();
            }
            else
            {
                Assert.Fail();
            }

        }

        [Test]
        [Description("EMV Configurations Get by PclId")]
        [Category("Smoke")]
        [TestCase("Automation_EMV_Configurations_Get_By_PclId")]
        public void EMV_Configurations_Get_By_PclId(string APItoken)
        {

            var response = APICommonMethods.ResponseFromGETrequest(AppNameHelper.ApiBaseUrl, "/configurations?PclId=108");
            Console.WriteLine(response.Content);
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

        //EMVConfigurations PclId Values from response and DB
        [Test]
        [Description("EMV Configurations Get by PclId with DB")]
        [Category("Smoke")]
        [TestCase("Automation_EMV_Configurations_Get_PclId_DB")]
        public void EMV_Configurations_PclID_DBConnect(string APItoken)
        {
            var response = APICommonMethods.ResponseFromGETrequest(AppNameHelper.ApiBaseUrl, "/configurations?PclId=108");
            //Console.WriteLine(response.Content);
            JArray jsonArray = JArray.Parse(response.Content);
            ArrayList ApiIDValue = new ArrayList();
            foreach (JObject var in jsonArray)
            {
                ApiIDValue.Add((int)var.GetValue("id"));
            }
            ApiIDValue.Sort();
            string sql = "SELECT * FROM dbo.emvconfiguration where pclid=108 order by id";
            ArrayList ls = DBConnect_Methods.SelectMethod(sql);
            if ((ApiIDValue.ToArray() as IStructuralEquatable).Equals(ls.ToArray(), EqualityComparer<int>.Default))
            {
                Assert.Pass();
            }
            else
            {
                Assert.Fail();
            }

        }

        [Test]
        [Description("EMV Configurations Get by Id")]
        [Category("Smoke")]
        [TestCase("Automation_EMV_Configurations_Get_By_Id")]
        public void EMV_Configurations_Get_By_Id(string APItoken)
        {

            var response = APICommonMethods.ResponseFromGETrequest(AppNameHelper.ApiBaseUrl, "/configurations/24/programs");
            //Console.WriteLine(response.Content);
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

        //EMVConfigurations Id Values from response and DB
        [Test]
        [Description("EMV Configurations Get by Id with DB")]
        [Category("Smoke")]
        [TestCase("Automation_EMV_Configurations_Get_Ids_DB")]
        public void EMV_Configurations_Get_Ids_With_DB(string APItoken)
        {
            var response = APICommonMethods.ResponseFromGETrequest(AppNameHelper.ApiBaseUrl, "/configurations/24/programs");
            //Console.WriteLine(response.Content);
            JArray jsonArray = JArray.Parse(response.Content);
            ArrayList ApiIDValue = new ArrayList();
            foreach (JObject var in jsonArray)
            {
                //Console.WriteLine((int)var.GetValue("id"));
                ApiIDValue.Add((int)var.GetValue("id"));
            }
            ApiIDValue.Sort();
            string sql = "SELECT rcpid AS Id,rcpname AS Name,rcpactive AS Status FROM dbo.retailercardprogram WHERE rcpemvconfigurationid =24 ORDER BY rcpid DESC";
            ArrayList ls = DBConnect_Methods.SelectMethod(sql);
            //Console.WriteLine("DBBBBBB Values");
            foreach (var item in ls)
            {
                //jsonArray[var]["id"];
                //var var = va;
                Console.WriteLine(item);
            }
            if (ApiIDValue[0].ToString().Equals(ls[0].ToString())) 
            { 
                Assert.Pass(); 
            }           
            else
            {
                Assert.Fail();
            }

        }


        [Test]
        [Description("EMV Issuer Create")]
        [Category("Smoke")]
        [TestCase("Automation_EMV_Issuer_Create")]
        public void EMV_Issuer_Create(string APItoken)
        {
            string randomString = RandomString.GetString(Types.ALPHANUMERIC_MIXEDCASE, 15);
            string Jsonbody = "{\"pclId\": 108,\"name\":\"" + randomString + "\" ,\"cpv_PVT\": \"pdf\",\"approvalPathId\": 2,\"description\": \"Issuer12 Desc\"}";
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


        //Name field value is not entered
        [Test]
        [Description("EMV Issuer Create_BlankName")]
        [Category("Smoke")]
        [TestCase("Automation_EMV_Issuer_Create_BlankName")]
        public void EMV_Issuer_Create_BlankName(string APItoken)
        {
            string randomString = RandomString.GetString(Types.ALPHANUMERIC_MIXEDCASE, 15);
            string Jsonbody = "{\"pclId\": 108,\"name\":\"\" ,\"cpv_PVT\": \"pdf\",\"approvalPathId\": 2,\"description\": \"Issuer12 Desc\"}";
            var response = APICommonMethods.ResponseFromPostRequest(AppNameHelper.ApiBaseUrl, "/issuers", Jsonbody);
            //Console.WriteLine(response.Content);
            dynamic jsonResponse = JObject.Parse(response.Content);
            String ErrorMsg = jsonResponse.errors.Name[0];
            //dynamic jsonResponse =JsonConvert.DeserializeObject(response.Content);
            Console.WriteLine(jsonResponse.errors.Name[0]);
            Assert.That(ErrorMsg, Is.EqualTo("The Name field is required."));
            Assert.That((int)response.StatusCode, Is.EqualTo(400));

        }

        //Name which already exists

        [Test]
        [Description("EMV Issuer Create_AlradyExistName")]
        [Category("Smoke")]
        [TestCase("Automation_EMV_Issuer_Create_AlradyExistName")]
        public void EMV_Issuer_Create_Alrady_Exist_Name(string APItoken)
        {
            string randomString = RandomString.GetString(Types.ALPHANUMERIC_MIXEDCASE, 15);
            string Jsonbody = "{\"pclId\": 108,\"name\":\"Issuer234\" ,\"cpv_PVT\": \"pdf\",\"approvalPathId\": 2,\"description\": \"Issuer12 Desc\"}";
            var response = APICommonMethods.ResponseFromPostRequest(AppNameHelper.ApiBaseUrl, "/issuers", Jsonbody);
            //Console.WriteLine(response.Content);
            dynamic jsonResponse = JObject.Parse(response.Content);
            Console.WriteLine(jsonResponse);
            String ErrorMsg = jsonResponse.error[0].message;
            //dynamic jsonResponse =JsonConvert.DeserializeObject(response.Content);
            Console.WriteLine(jsonResponse.error[0].message);
            Assert.That(ErrorMsg, Is.EqualTo("EMV Issuer Id 251 doesn't exists."));
            Assert.That((int)response.StatusCode, Is.EqualTo(404));

        }


        //Name field value is enter MoreThan50Char
        [Test]
        [Description("EMV Issuer Create_Name_MoreThan50Char")]
        [Category("Smoke")]
        [TestCase("Automation_EMV_Issuer_Create_Name_MoreThan50Char")]
        public void EMV_Issuer_Create_Name_MoreThan50Char(string APItoken)
        {
            //string randomString = RandomString.GetString(Types.ALPHANUMERIC_MIXEDCASE, 15);
            string Jsonbody = "{\"pclId\": 108,\"name\":\"apitestingapitestingapitestingapitestingapitestinga\",\"cpv_PVT\": \"pdf\",\"approvalPathId\": 2,\"description\": \"Issuer12 Desc\"}";
            var response = APICommonMethods.ResponseFromPostRequest(AppNameHelper.ApiBaseUrl, "/issuers", Jsonbody);
            //Console.WriteLine(response.Content);
            dynamic jsonResponse = JObject.Parse(response.Content);
            String ErrorMsg = jsonResponse.errors.Name[0];
            //dynamic jsonResponse =JsonConvert.DeserializeObject(response.Content);
            Console.WriteLine(jsonResponse.errors.Name[0]);
            Assert.That(ErrorMsg, Is.EqualTo("Name can accept 50 characters or fewer. You entered 51 characters."));
            Assert.That((int)response.StatusCode, Is.EqualTo(400));

        }

        //description field value is enter MoreThan50Char
        [Test]
        [Description("EMV Issuer Create_description_MoreThan50Char")]
        [Category("Smoke")]
        [TestCase("Automation_EMV_Issuer_Create_description_MoreThan50Char")]
        public void EMV_Issuer_Create_description_MoreThan50Char(string APItoken)
        {
            string randomString = RandomString.GetString(Types.ALPHANUMERIC_MIXEDCASE, 15);
            string Jsonbody = "{\"pclId\": 108,\"name\":\""+ randomString+ "\",\"cpv_PVT\": \"pdf\",\"approvalPathId\": 2,\"description\": \"apitestingapitestingapitestingapitestingapitestinga\"}";
            var response = APICommonMethods.ResponseFromPostRequest(AppNameHelper.ApiBaseUrl, "/issuers", Jsonbody);
            //Console.WriteLine(response.Content);
            dynamic jsonResponse = JObject.Parse(response.Content);
            String ErrorMsg = jsonResponse.errors.Description[0];
            //dynamic jsonResponse =JsonConvert.DeserializeObject(response.Content);
            Console.WriteLine(jsonResponse.errors.Description[0]);
            Assert.That(ErrorMsg, Is.EqualTo("Description can accept 50 characters or fewer. You entered 51 characters."));
            Assert.That((int)response.StatusCode, Is.EqualTo(400));

        }

        //cpv_PVT field value is enter MoreThan50Char
        [Test]
        [Description("EMV Issuer Create_cpv_PVT_MoreThan50Char")]
        [Category("Smoke")]
        [TestCase("Automation_EMV_Issuer_Create_cpv_PVT_MoreThan50Char")]
        public void EMV_Issuer_Create_cpv_PVT_MoreThan50Char(string APItoken)
        {
            string randomString = RandomString.GetString(Types.ALPHANUMERIC_MIXEDCASE, 15);
            string Jsonbody = "{\"pclId\": 108,\"name\":\"" + randomString + "\",\"cpv_PVT\": \"apitestingapitestingapitestingapitestingapitestinga\",\"approvalPathId\": 2,\"description\": \"Desc\"}";
            var response = APICommonMethods.ResponseFromPostRequest(AppNameHelper.ApiBaseUrl, "/issuers", Jsonbody);
            //Console.WriteLine(response.Content);
            dynamic jsonResponse = JObject.Parse(response.Content);
            String ErrorMsg = jsonResponse.errors.Cpv_PVT[0];
            //dynamic jsonResponse =JsonConvert.DeserializeObject(response.Content);
            Console.WriteLine(jsonResponse.errors.Cpv_PVT[0]);
            Assert.That(ErrorMsg, Is.EqualTo("Cpv_PVT can accept 50 characters or fewer. You entered 51 characters."));
            Assert.That((int)response.StatusCode, Is.EqualTo(400));

        }


        [Test]
        [Description("EMV Issuer Update")]
        [Category("Smoke")]
        [TestCase("Automation_Issuer_Update")]
        public void EMV_Issuer_Update(string APItoken)
        {
            string Jsonbody = "{\"pclId\": 108,\"name\": \"IssuerName116\",\"cpv_PVT\": \"pdf\",\"approvalPathId\": 2,\"description\":\"Issuer116 Desc\",\"id\": 11}";
            var response = APICommonMethods.ResponseFromPutRequest(AppNameHelper.ApiBaseUrl, "/issuers", Jsonbody);
            var toasterMessageID = Regex.Match(response.Content, @"\d+").Value;
            String OutputVal = response.Content.ToString().Replace("\"", "");
            String txt = "EMV Issuer " + toasterMessageID + " Updated Successfully.";

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

        //Name field value is not entered
        [Test]
        [Description("EMV Issuer Update_BlankName")]
        [Category("Smoke")]
        [TestCase("Automation_EMV_Issuer_Update_BlankName")]
        public void EMV_Issuer_Update_BlankName(string APItoken)
        {
            string randomString = RandomString.GetString(Types.ALPHANUMERIC_MIXEDCASE, 15);
            string Jsonbody = "{\"pclId\": 108,\"name\":\"\" ,\"cpv_PVT\": \"pdf\",\"approvalPathId\": 2,\"description\": \"Issuer12 Desc\",\"id\": 11}";
            var response = APICommonMethods.ResponseFromPutRequest(AppNameHelper.ApiBaseUrl, "/issuers", Jsonbody);  
            //Console.WriteLine(response.Content);
            dynamic jsonResponse = JObject.Parse(response.Content);
            String ErrorMsg = jsonResponse.errors.Name[0];
            //dynamic jsonResponse =JsonConvert.DeserializeObject(response.Content);
            Console.WriteLine(jsonResponse.errors.Name[0]);
            Assert.That(ErrorMsg, Is.EqualTo("The Name field is required."));
            Assert.That((int)response.StatusCode, Is.EqualTo(400));

        }

        //Name which already exists

        [Test]
        [Description("EMV Issuer Update_AlradyExistName")]
        [Category("Smoke")]
        [TestCase("Automation_EMV_Issuer_Update_AlradyExistName")]
        public void EMV_Issuer_Update_Alrady_Exist_Name(string APItoken)
        {
            string randomString = RandomString.GetString(Types.ALPHANUMERIC_MIXEDCASE, 15);
            string Jsonbody = "{\"pclId\": 108,\"name\":\"Issuer234\" ,\"cpv_PVT\": \"pdf\",\"approvalPathId\": 2,\"description\": \"Issuer12 Desc\",\"id\": 11}";
            var response = APICommonMethods.ResponseFromPutRequest(AppNameHelper.ApiBaseUrl, "/issuers", Jsonbody);
            //Console.WriteLine(response.Content);
            dynamic jsonResponse = JObject.Parse(response.Content);
            Console.WriteLine(jsonResponse);
            String ErrorMsg = jsonResponse.error[0].message;
            //dynamic jsonResponse =JsonConvert.DeserializeObject(response.Content);
            Console.WriteLine(jsonResponse.error[0].message);
            //Assert.That(ErrorMsg, Is.EqualTo("EMV Issuer Id 11 doesn't exists."));
            //Assert.That((int)response.StatusCode, Is.EqualTo(404));

        }

        //Name field value is enter MoreThan50Char
        [Test]
        [Description("EMV Issuer Update_Name_MoreThan50Char")]
        [Category("Smoke")]
        [TestCase("Automation_EMV_Issuer_Update_Name_MoreThan50Char")]
        public void EMV_Issuer_Update_Name_MoreThan50Char(string APItoken)
        {
            //string randomString = RandomString.GetString(Types.ALPHANUMERIC_MIXEDCASE, 15);
            string Jsonbody = "{\"pclId\": 108,\"name\":\"apitestingapitestingapitestingapitestingapitestinga\",\"cpv_PVT\": \"pdf\",\"approvalPathId\": 2,\"description\": \"Issuer12 Desc\",\"id\": 11}";
            var response = APICommonMethods.ResponseFromPutRequest(AppNameHelper.ApiBaseUrl, "/issuers", Jsonbody);
            //Console.WriteLine(response.Content);
            dynamic jsonResponse = JObject.Parse(response.Content);
            String ErrorMsg = jsonResponse.errors.Name[0];
            //dynamic jsonResponse =JsonConvert.DeserializeObject(response.Content);
            Console.WriteLine(jsonResponse.errors.Name[0]);
            Assert.That(ErrorMsg, Is.EqualTo("Name can accept 50 characters or fewer. You entered 51 characters."));
            Assert.That((int)response.StatusCode, Is.EqualTo(400));

        }

        //description field value is enter MoreThan50Char
        [Test]
        [Description("EMV Issuer Update_description_MoreThan50Char")]
        [Category("Smoke")]
        [TestCase("Automation_EMV_Issuer_Update_description_MoreThan50Char")]
        public void EMV_Issuer_Update_description_MoreThan50Char(string APItoken)
        {
            string randomString = RandomString.GetString(Types.ALPHANUMERIC_MIXEDCASE, 15);
            string Jsonbody = "{\"pclId\": 108,\"name\":\"" + randomString + "\",\"cpv_PVT\": \"pdf\",\"approvalPathId\": 2,\"description\": \"apitestingapitestingapitestingapitestingapitestinga\",\"id\": 11}";
            var response = APICommonMethods.ResponseFromPutRequest(AppNameHelper.ApiBaseUrl, "/issuers", Jsonbody);
            //Console.WriteLine(response.Content);
            dynamic jsonResponse = JObject.Parse(response.Content);
            String ErrorMsg = jsonResponse.errors.Description[0];
            //dynamic jsonResponse =JsonConvert.DeserializeObject(response.Content);
            Console.WriteLine(jsonResponse.errors.Description[0]);
            Assert.That(ErrorMsg, Is.EqualTo("Description can accept 50 characters or fewer. You entered 51 characters."));
            Assert.That((int)response.StatusCode, Is.EqualTo(400));

        }

        //cpv_PVT field value is enter MoreThan50Char
        [Test]
        [Description("EMV Issuer Update_cpv_PVT_MoreThan50Char")]
        [Category("Smoke")]
        [TestCase("Automation_EMV_Issuer_Update_cpv_PVT_MoreThan50Char")]
        public void EMV_Issuer_Update_cpv_PVT_MoreThan50Char(string APItoken)
        {
            string randomString = RandomString.GetString(Types.ALPHANUMERIC_MIXEDCASE, 15);
            string Jsonbody = "{\"pclId\": 108,\"name\":\"" + randomString + "\",\"cpv_PVT\": \"apitestingapitestingapitestingapitestingapitestinga\",\"approvalPathId\": 2,\"description\": \"Desc\",\"id\": 11}";
            var response = APICommonMethods.ResponseFromPutRequest(AppNameHelper.ApiBaseUrl, "/issuers", Jsonbody);
            //Console.WriteLine(response.Content);
            dynamic jsonResponse = JObject.Parse(response.Content);
            String ErrorMsg = jsonResponse.errors.Cpv_PVT[0];
            //dynamic jsonResponse =JsonConvert.DeserializeObject(response.Content);
            Console.WriteLine(jsonResponse.errors.Cpv_PVT[0]);
            Assert.That(ErrorMsg, Is.EqualTo("Cpv_PVT can accept 50 characters or fewer. You entered 51 characters."));
            Assert.That((int)response.StatusCode, Is.EqualTo(400));

        }



        [Test]
        [Description("EMV Issuer Get")]
        [Category("Smoke")]
        [TestCase("Automation_EMV_Issuer_Get")]
        public void EMV_Issuer_Get(string APItoken)
        {

            var response = APICommonMethods.ResponseFromGETrequest(AppNameHelper.ApiBaseUrl, "/issuers?PclId=108");
            //Console.WriteLine(response.Content);
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
        [Description("EMV Issuer Get Ids Match")]
        [Category("Smoke")]
        [TestCase("Automation_EMV_Issuer_Get_IDMatch_DB")]
        public void EMV_Issuer_Get_IDMatch_DB(string APItoken)
        {
            var response = APICommonMethods.ResponseFromGETrequest(AppNameHelper.ApiBaseUrl, "/issuers?PclId=108");
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
            string sql = "SELECT * from dbo.emvissuer e where pclid=108 and e.isdeleted =false  order by id";
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


        [Test]
        [Description("EMV Module Create")]
        [Category("Smoke")]
        [TestCase("Automation_EMV_Module_Create")]
        public void EMV_Module_Create(string APItoken)
        {
            string randomString = RandomString.GetString(Types.ALPHANUMERIC_MIXEDCASE, 15);
            string Jsonbody = "{\"name\":\""+ randomString +"\",\"description\": \"Modules12 Desc\",\"travelerLabel\": \"Test Mod84\",\"cmiProgram\": \"Mod84\",\"groupId\": 12}";
            var response = APICommonMethods.ResponseFromPostRequest(AppNameHelper.ApiBaseUrl, "/modules", Jsonbody);
            Console.WriteLine(response.Content);
            var toasterMessageID = Regex.Match(response.Content, @"\d+").Value;
            String OutputVal = response.Content.ToString().Replace("\"", "");
            String txt = "EMV Module " + toasterMessageID + " Added Successfully.";

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


        //Name field value is not entered
        [Test]
        [Description("EMV Module Create Blank Name")]
        [Category("Smoke")]
        [TestCase("Automation_EMV_Module_Create_Blank_NAme")]
        public void EMV_Module_Create_Blank_Name(string APItoken)
        {
            //string randomString = RandomString.GetString(Types.ALPHANUMERIC_MIXEDCASE, 15);
            string Jsonbody = "{\"name\":\"\",\"description\": \"Modules12 Desc\",\"travelerLabel\": \"Test Mod84\",\"cmiProgram\": \"Mod84\",\"groupId\": 12}";
            var response = APICommonMethods.ResponseFromPostRequest(AppNameHelper.ApiBaseUrl, "/modules", Jsonbody);
            dynamic jsonResponse = JObject.Parse(response.Content);
            String ErrorMsg = jsonResponse.errors.Name[0];
            //dynamic jsonResponse =JsonConvert.DeserializeObject(response.Content);
            Console.WriteLine(jsonResponse.errors.Name[0]);
            Assert.That(ErrorMsg, Is.EqualTo("The Name field is required."));
            Assert.That((int)response.StatusCode, Is.EqualTo(400));
        }

        //// Name field must not exceed 50 characters.
        [Test]
        [Description("EMV Module Create Name More50Char")]
        [Category("Smoke")]
        [TestCase("Automation_EMV_Module_Create_Name_More50Char")]
        public void EMV_Module_Create_Name_More50Char(string APItoken)
        {
            //string randomString = RandomString.GetString(Types.ALPHANUMERIC_MIXEDCASE, 15);
            string Jsonbody = "{\"name\":\"apitestingapitestingapitestingapitestingapitestinga\",\"description\": \"Modules12 Desc\",\"travelerLabel\": \"Test Mod84\",\"cmiProgram\": \"Mod84\",\"groupId\": 12}";
            var response = APICommonMethods.ResponseFromPostRequest(AppNameHelper.ApiBaseUrl, "/modules", Jsonbody);
            dynamic jsonResponse = JObject.Parse(response.Content);
            String ErrorMsg = jsonResponse.errors.Name[0];
            //dynamic jsonResponse =JsonConvert.DeserializeObject(response.Content);
            Console.WriteLine(jsonResponse.errors.Name[0]);
            Assert.That(ErrorMsg, Is.EqualTo("Name can accept 50 characters or fewer. You entered 51 characters."));
            Assert.That((int)response.StatusCode, Is.EqualTo(400));
        }


        //description field value is not entered
        [Test]
        [Description("EMV Module Create blank description")]
        [Category("Smoke")]
        [TestCase("Automation_EMV_Module_Create_blank_description")]
        public void EMV_Module_Create_blank_description(string APItoken)
        {
            //string randomString = RandomString.GetString(Types.ALPHANUMERIC_MIXEDCASE, 15);
            string Jsonbody = "{\"name\":\"apitesting\",\"description\": \"\",\"travelerLabel\": \"Test Mod84\",\"cmiProgram\": \"Mod84\",\"groupId\": 12}";
            var response = APICommonMethods.ResponseFromPostRequest(AppNameHelper.ApiBaseUrl, "/modules", Jsonbody);
            dynamic jsonResponse = JObject.Parse(response.Content);
            String ErrorMsg = jsonResponse.errors.Description[0];
            //dynamic jsonResponse =JsonConvert.DeserializeObject(response.Content);
            Console.WriteLine(jsonResponse.errors.Description[0]);
            Assert.That(ErrorMsg, Is.EqualTo("The Description field is required."));
            Assert.That((int)response.StatusCode, Is.EqualTo(400));
        }

        // description field must not exceed 100 characters.
        [Test]
        [Description("EMV Module Create description More100Char")]
        [Category("Smoke")]
        [TestCase("Automation_EMV_Module_Create_description_More100Char")]
        public void EMV_Module_Create_description_More100Char(string APItoken)
        {
            //string randomString = RandomString.GetString(Types.ALPHANUMERIC_MIXEDCASE, 15);
            string Jsonbody = "{\"name\":\"apitesting\",\"description\": \"apitestingapitestingapitestingapitestingapitestingaapitestingapitestingapitestingapitestingapitesting\",\"travelerLabel\": \"Test Mod84\",\"cmiProgram\": \"Mod84\",\"groupId\": 12}";
            var response = APICommonMethods.ResponseFromPostRequest(AppNameHelper.ApiBaseUrl, "/modules", Jsonbody);
            dynamic jsonResponse = JObject.Parse(response.Content);
            String ErrorMsg = jsonResponse.errors.Description[0];
            //dynamic jsonResponse =JsonConvert.DeserializeObject(response.Content);
            Console.WriteLine(jsonResponse.errors.Description[0]);
            Assert.That(ErrorMsg, Is.EqualTo("Description can accept 100 characters or fewer. You entered 101 characters."));
            Assert.That((int)response.StatusCode, Is.EqualTo(400));
        }

        // travelerLabel field must not exceed 100 characters.
        [Test]
        [Description("EMV Module Create travelerLabel More100Char")]
        [Category("Smoke")]
        [TestCase("Automation_EMV_Module_Create_travelerLabel_More100Char")]
        public void EMV_Module_Create_travelerLabel_More100Char(string APItoken)
        {
            //string randomString = RandomString.GetString(Types.ALPHANUMERIC_MIXEDCASE, 15);
            string Jsonbody = "{\"name\":\"apitesting\",\"description\": \"API\",\"travelerLabel\": \"apitestingapitestingapitestingapitestingapitestingaapitestingapitestingapitestingapitestingapitesting\",\"cmiProgram\": \"Mod84\",\"groupId\": 12}";
            var response = APICommonMethods.ResponseFromPostRequest(AppNameHelper.ApiBaseUrl, "/modules", Jsonbody);
            dynamic jsonResponse = JObject.Parse(response.Content);
            String ErrorMsg = jsonResponse.errors.TravelerLabel[0];
            //dynamic jsonResponse =JsonConvert.DeserializeObject(response.Content);
            Console.WriteLine(jsonResponse.errors.TravelerLabel[0]);
            Assert.That(ErrorMsg, Is.EqualTo("TravelerLabel can accept 100 characters or fewer. You entered 101 characters."));
            Assert.That((int)response.StatusCode, Is.EqualTo(400));
        }

        // CMI Program field must not exceed 100 characters.
        [Test]
        [Description("EMV Module Create CMIProgram More100Char")]
        [Category("Smoke")]
        [TestCase("Automation_EMV_Module_Create_CMIProgram_More100Char")]
        public void EMV_Module_Create_CMIProgram_More100Char(string APItoken)
        {
            //string randomString = RandomString.GetString(Types.ALPHANUMERIC_MIXEDCASE, 15);
            string Jsonbody = "{\"name\":\"apitesting\",\"description\": \"API\",\"travelerLabel\": \"Test Mod84\",\"cmiProgram\": \"apitestingapitestingapitestingapitestingapitestingaapitestingapitestingapitestingapitestingapitesting\",\"groupId\": 12}";
            var response = APICommonMethods.ResponseFromPostRequest(AppNameHelper.ApiBaseUrl, "/modules", Jsonbody);
            dynamic jsonResponse = JObject.Parse(response.Content);
            String ErrorMsg = jsonResponse.errors.CMIProgram[0];
            //dynamic jsonResponse =JsonConvert.DeserializeObject(response.Content);
            Console.WriteLine(jsonResponse.errors.CMIProgram[0]);
            Assert.That(ErrorMsg, Is.EqualTo("CMIProgram can accept 100 characters or fewer. You entered 101 characters."));
            Assert.That((int)response.StatusCode, Is.EqualTo(400));
        }

        // groupId field not entered.
        [Test]
        [Description("EMV Module Create blank groupId")]
        [Category("Smoke")]
        [TestCase("Automation_EMV_Module_Create_blank_groupId")]
        public void EMV_Module_Create_blank_groupId(string APItoken)
        {
            //string randomString = RandomString.GetString(Types.ALPHANUMERIC_MIXEDCASE, 15);
            string Jsonbody = "{\"name\":\"apitesting\",\"description\": \"API\",\"travelerLabel\": \"Test Mod84\",\"cmiProgram\": \"apitesting\",\"groupId\": 0}";
            var response = APICommonMethods.ResponseFromPostRequest(AppNameHelper.ApiBaseUrl, "/modules", Jsonbody);
            dynamic jsonResponse = JObject.Parse(response.Content);
            String ErrorMsg = jsonResponse.errors.GroupId[0];
            //dynamic jsonResponse =JsonConvert.DeserializeObject(response.Content);
            Console.WriteLine(jsonResponse.errors.GroupId[0]);
            Assert.That(ErrorMsg, Is.EqualTo("The GroupId field is required."));
            Assert.That((int)response.StatusCode, Is.EqualTo(400));
        }

        // groupId field entered as -0.
        [Test]
        [Description("EMV Module Create blank groupId")]
        [Category("Smoke")]
        [TestCase("Automation_EMV_Module_Create_blank_groupId")]
        public void EMV_Module_Create_Less0_groupId(string APItoken)
        {
            //string randomString = RandomString.GetString(Types.ALPHANUMERIC_MIXEDCASE, 15);
            string Jsonbody = "{\"name\":\"apitesting\",\"description\": \"API\",\"travelerLabel\": \"Test Mod84\",\"cmiProgram\": \"apitesting\",\"groupId\": -1}";
            var response = APICommonMethods.ResponseFromPostRequest(AppNameHelper.ApiBaseUrl, "/modules", Jsonbody);
            dynamic jsonResponse = JObject.Parse(response.Content);
            String ErrorMsg = jsonResponse.errors.GroupId[0];
            //dynamic jsonResponse =JsonConvert.DeserializeObject(response.Content);
            Console.WriteLine(jsonResponse.errors.GroupId[0]);
            Assert.That(ErrorMsg, Is.EqualTo("Only accpeted values are numbers from 1 - 999."));
            Assert.That((int)response.StatusCode, Is.EqualTo(400));
        }


        // groupId field entered as 1000.
        [Test]
        [Description("EMV Module Create blank groupId")]
        [Category("Smoke")]
        [TestCase("Automation_EMV_Module_Create_blank_groupId")]
        public void EMV_Module_Create_Greater999_groupId(string APItoken)
        {
            //string randomString = RandomString.GetString(Types.ALPHANUMERIC_MIXEDCASE, 15);
            string Jsonbody = "{\"name\":\"apitesting\",\"description\": \"API\",\"travelerLabel\": \"Test Mod84\",\"cmiProgram\": \"apitesting\",\"groupId\": 1000}";
            var response = APICommonMethods.ResponseFromPostRequest(AppNameHelper.ApiBaseUrl, "/modules", Jsonbody);
            dynamic jsonResponse = JObject.Parse(response.Content);
            String ErrorMsg = jsonResponse.errors.GroupId[0];
            //dynamic jsonResponse =JsonConvert.DeserializeObject(response.Content);
            Console.WriteLine(jsonResponse.errors.GroupId[0]);
            Assert.That(ErrorMsg, Is.EqualTo("Only accpeted values are numbers from 1 - 999."));
            Assert.That((int)response.StatusCode, Is.EqualTo(400));
        }











        [Test]
        [Description("EMV Module Update")]
        [Category("Smoke")]
        [TestCase("Automation_Module_Update")]
        public void EMV_Module_Update(string APItoken)
        {
            string Jsonbody = "{\"name\": \"Modules84\",\"description\": \"Modules84 Desc\",\"travelerLabel\": \"Test Mod84\", \"cmiProgram\": \"Mod84\",\"groupId\": 12,\"id\": 84}";
            var response = APICommonMethods.ResponseFromPutRequest(AppNameHelper.ApiBaseUrl, "/modules", Jsonbody);
            var toasterMessageID = Regex.Match(response.Content, @"\d+").Value;
            String OutputVal = response.Content.ToString().Replace("\"", "");
            String txt = "EMV Module " + toasterMessageID + " Updated Successfully.";

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



        //Name field value is not entered
        [Test]
        [Description("EMV Module Update Blank Name")]
        [Category("Smoke")]
        [TestCase("Automation_EMV_Module_Update_Blank_NAme")]
        public void EMV_Module_Update_Blank_Name(string APItoken)
        {
            //string randomString = RandomString.GetString(Types.ALPHANUMERIC_MIXEDCASE, 15);
            string Jsonbody = "{\"name\":\"\",\"description\": \"Modules12 Desc\",\"travelerLabel\": \"Test Mod84\",\"cmiProgram\": \"Mod84\",\"groupId\": 12, \"id\": 84}";
            var response = APICommonMethods.ResponseFromPutRequest(AppNameHelper.ApiBaseUrl, "/modules", Jsonbody);
            dynamic jsonResponse = JObject.Parse(response.Content);
            String ErrorMsg = jsonResponse.errors.Name[0];
            //dynamic jsonResponse =JsonConvert.DeserializeObject(response.Content);
            Console.WriteLine(jsonResponse.errors.Name[0]);
            Assert.That(ErrorMsg, Is.EqualTo("The Name field is required."));
            Assert.That((int)response.StatusCode, Is.EqualTo(400));
        }

        //// Name field must not exceed 50 characters.
        [Test]
        [Description("EMV Module Update Name More50Char")]
        [Category("Smoke")]
        [TestCase("Automation_EMV_Module_Update_Name_More50Char")]
        public void EMV_Module_Update_Name_More50Char(string APItoken)
        {
            //string randomString = RandomString.GetString(Types.ALPHANUMERIC_MIXEDCASE, 15);
            string Jsonbody = "{\"name\":\"apitestingapitestingapitestingapitestingapitestinga\",\"description\": \"Modules12 Desc\",\"travelerLabel\": \"Test Mod84\",\"cmiProgram\": \"Mod84\",\"groupId\": 12, \"id\": 84}";
            var response = APICommonMethods.ResponseFromPutRequest(AppNameHelper.ApiBaseUrl, "/modules", Jsonbody);
            dynamic jsonResponse = JObject.Parse(response.Content);
            String ErrorMsg = jsonResponse.errors.Name[0];
            //dynamic jsonResponse =JsonConvert.DeserializeObject(response.Content);
            Console.WriteLine(jsonResponse.errors.Name[0]);
            Assert.That(ErrorMsg, Is.EqualTo("Name can accept 50 characters or fewer. You entered 51 characters."));
            Assert.That((int)response.StatusCode, Is.EqualTo(400));
        }


        //description field value is not entered
        [Test]
        [Description("EMV Module Update blank description")]
        [Category("Smoke")]
        [TestCase("Automation_EMV_Module_Update_blank_description")]
        public void EMV_Module_Update_blank_description(string APItoken)
        {
            //string randomString = RandomString.GetString(Types.ALPHANUMERIC_MIXEDCASE, 15);
            string Jsonbody = "{\"name\":\"apitesting\",\"description\": \"\",\"travelerLabel\": \"Test Mod84\",\"cmiProgram\": \"Mod84\",\"groupId\": 12, \"id\": 84}";
            var response = APICommonMethods.ResponseFromPutRequest(AppNameHelper.ApiBaseUrl, "/modules", Jsonbody);
            dynamic jsonResponse = JObject.Parse(response.Content);
            String ErrorMsg = jsonResponse.errors.Description[0];
            //dynamic jsonResponse =JsonConvert.DeserializeObject(response.Content);
            Console.WriteLine(jsonResponse.errors.Description[0]);
            Assert.That(ErrorMsg, Is.EqualTo("The Description field is required."));
            Assert.That((int)response.StatusCode, Is.EqualTo(400));
        }

        // description field must not exceed 100 characters.
        [Test]
        [Description("EMV Module Update description More100Char")]
        [Category("Smoke")]
        [TestCase("Automation_EMV_Module_Update_description_More100Char")]
        public void EMV_Module_Update_description_More100Char(string APItoken)
        {
            //string randomString = RandomString.GetString(Types.ALPHANUMERIC_MIXEDCASE, 15);
            string Jsonbody = "{\"name\":\"apitesting\",\"description\": \"apitestingapitestingapitestingapitestingapitestingaapitestingapitestingapitestingapitestingapitesting\",\"travelerLabel\": \"Test Mod84\",\"cmiProgram\": \"Mod84\",\"groupId\": 12,\"id\": 84}";
            var response = APICommonMethods.ResponseFromPutRequest(AppNameHelper.ApiBaseUrl, "/modules", Jsonbody);
            dynamic jsonResponse = JObject.Parse(response.Content);
            String ErrorMsg = jsonResponse.errors.Description[0];
            //dynamic jsonResponse =JsonConvert.DeserializeObject(response.Content);
            Console.WriteLine(jsonResponse.errors.Description[0]);
            Assert.That(ErrorMsg, Is.EqualTo("Description can accept 100 characters or fewer. You entered 101 characters."));
            Assert.That((int)response.StatusCode, Is.EqualTo(400));
        }

        // travelerLabel field must not exceed 100 characters.
        [Test]
        [Description("EMV Module Update travelerLabel More100Char")]
        [Category("Smoke")]
        [TestCase("Automation_EMV_Module_Update_travelerLabel_More100Char")]
        public void EMV_Module_Update_travelerLabel_More100Char(string APItoken)
        {
            //string randomString = RandomString.GetString(Types.ALPHANUMERIC_MIXEDCASE, 15);
            string Jsonbody = "{\"name\":\"apitesting\",\"description\": \"API\",\"travelerLabel\": \"apitestingapitestingapitestingapitestingapitestingaapitestingapitestingapitestingapitestingapitesting\",\"cmiProgram\": \"Mod84\",\"groupId\": 12,\"id\": 84}";
            var response = APICommonMethods.ResponseFromPutRequest(AppNameHelper.ApiBaseUrl, "/modules", Jsonbody);
            dynamic jsonResponse = JObject.Parse(response.Content);
            String ErrorMsg = jsonResponse.errors.TravelerLabel[0];
            //dynamic jsonResponse =JsonConvert.DeserializeObject(response.Content);
            Console.WriteLine(jsonResponse.errors.TravelerLabel[0]);
            Assert.That(ErrorMsg, Is.EqualTo("TravelerLabel can accept 100 characters or fewer. You entered 101 characters."));
            Assert.That((int)response.StatusCode, Is.EqualTo(400));
        }

        // CMI Program field must not exceed 100 characters.
        [Test]
        [Description("EMV Module Update CMIProgram More100Char")]
        [Category("Smoke")]
        [TestCase("Automation_EMV_Module_Update_CMIProgram_More100Char")]
        public void EMV_Module_Update_CMIProgram_More100Char(string APItoken)
        {
            //string randomString = RandomString.GetString(Types.ALPHANUMERIC_MIXEDCASE, 15);
            string Jsonbody = "{\"name\":\"apitesting\",\"description\": \"API\",\"travelerLabel\": \"Test Mod84\",\"cmiProgram\": \"apitestingapitestingapitestingapitestingapitestingaapitestingapitestingapitestingapitestingapitesting\",\"groupId\": 12,\"id\": 84,\"id\": 84}";
            var response = APICommonMethods.ResponseFromPutRequest(AppNameHelper.ApiBaseUrl, "/modules", Jsonbody);
            dynamic jsonResponse = JObject.Parse(response.Content);
            String ErrorMsg = jsonResponse.errors.CMIProgram[0];
            //dynamic jsonResponse =JsonConvert.DeserializeObject(response.Content);
            Console.WriteLine(jsonResponse.errors.CMIProgram[0]);
            Assert.That(ErrorMsg, Is.EqualTo("CMIProgram can accept 100 characters or fewer. You entered 101 characters."));
            Assert.That((int)response.StatusCode, Is.EqualTo(400));
        }

        // groupId field not entered.
        [Test]
        [Description("EMV Module Update blank groupId")]
        [Category("Smoke")]
        [TestCase("Automation_EMV_Module_Update_blank_groupId")]
        public void EMV_Module_Update_blank_groupId(string APItoken)
        {
            //string randomString = RandomString.GetString(Types.ALPHANUMERIC_MIXEDCASE, 15);
            string Jsonbody = "{\"name\":\"apitesting\",\"description\": \"API\",\"travelerLabel\": \"Test Mod84\",\"cmiProgram\": \"apitesting\",\"groupId\": 0,\"id\": 84}";
            var response = APICommonMethods.ResponseFromPutRequest(AppNameHelper.ApiBaseUrl, "/modules", Jsonbody);
            dynamic jsonResponse = JObject.Parse(response.Content);
            String ErrorMsg = jsonResponse.errors.GroupId[0];
            //dynamic jsonResponse =JsonConvert.DeserializeObject(response.Content);
            Console.WriteLine(jsonResponse.errors.GroupId[0]);
            Assert.That(ErrorMsg, Is.EqualTo("The GroupId field is required."));
            Assert.That((int)response.StatusCode, Is.EqualTo(400));
        }

        // groupId field entered as less than 0.
        [Test]
        [Description("EMV Module Update LessThan0 groupId")]
        [Category("Smoke")]
        [TestCase("Automation_EMV_Module_Update_LessThan0_groupId")]
        public void EMV_Module_Update_Less0_groupId(string APItoken)
        {
            //string randomString = RandomString.GetString(Types.ALPHANUMERIC_MIXEDCASE, 15);
            string Jsonbody = "{\"name\":\"apitesting\",\"description\": \"API\",\"travelerLabel\": \"Test Mod84\",\"cmiProgram\": \"apitesting\",\"groupId\": -1,\"id\": 84}";
            var response = APICommonMethods.ResponseFromPutRequest(AppNameHelper.ApiBaseUrl, "/modules", Jsonbody);
            dynamic jsonResponse = JObject.Parse(response.Content);
            String ErrorMsg = jsonResponse.errors.GroupId[0];
            //dynamic jsonResponse =JsonConvert.DeserializeObject(response.Content);
            Console.WriteLine(jsonResponse.errors.GroupId[0]);
            Assert.That(ErrorMsg, Is.EqualTo("Only accpeted values are numbers from 1 - 999."));
            Assert.That((int)response.StatusCode, Is.EqualTo(400));
        }


        // groupId field entered as 1000.
        [Test]
        [Description("EMV Module Update blank groupId")]
        [Category("Smoke")]
        [TestCase("Automation_EMV_Module_Update_blank_groupId")]
        public void EMV_Module_Update_Greater999_groupId(string APItoken)
        {
            //string randomString = RandomString.GetString(Types.ALPHANUMERIC_MIXEDCASE, 15);
            string Jsonbody = "{\"name\":\"apitesting\",\"description\": \"API\",\"travelerLabel\": \"Test Mod84\",\"cmiProgram\": \"apitesting\",\"groupId\": 1000,\"id\": 84}";
            var response = APICommonMethods.ResponseFromPutRequest(AppNameHelper.ApiBaseUrl, "/modules", Jsonbody);
            dynamic jsonResponse = JObject.Parse(response.Content);
            String ErrorMsg = jsonResponse.errors.GroupId[0];
            //dynamic jsonResponse =JsonConvert.DeserializeObject(response.Content);
            Console.WriteLine(jsonResponse.errors.GroupId[0]);
            Assert.That(ErrorMsg, Is.EqualTo("Only accpeted values are numbers from 1 - 999."));
            Assert.That((int)response.StatusCode, Is.EqualTo(400));
        }

        // Id field entered as 0.
        [Test]
        [Description("EMV Module Update blank Id")]
        [Category("Smoke")]
        [TestCase("Automation_EMV_Module_Update_blank_Id")]
        public void EMV_Module_Update_Blank_Id(string APItoken)
        {
            //string randomString = RandomString.GetString(Types.ALPHANUMERIC_MIXEDCASE, 15);
            string Jsonbody = "{\"name\":\"apitesting\",\"description\": \"API\",\"travelerLabel\": \"Test Mod84\",\"cmiProgram\": \"apitesting\",\"groupId\": 1000,\"id\": 0}";
            var response = APICommonMethods.ResponseFromPutRequest(AppNameHelper.ApiBaseUrl, "/modules", Jsonbody);
            dynamic jsonResponse = JObject.Parse(response.Content);
            String ErrorMsg = jsonResponse.errors.Id[0];
            //dynamic jsonResponse =JsonConvert.DeserializeObject(response.Content);
            Console.WriteLine(jsonResponse.errors.Id[0]);
            Assert.That(ErrorMsg, Is.EqualTo("The Id field is required."));
            Assert.That((int)response.StatusCode, Is.EqualTo(400));
        }

        // Id field entered Invalid.
        [Test]
        [Description("EMV Module Update Invalid Id")]
        [Category("Smoke")]
        [TestCase("Automation_EMV_Module_Update_Invalid_Id")]
        public void EMV_Module_Update_Invalid_Id(string APItoken)
        {
            //string randomString = RandomString.GetString(Types.ALPHANUMERIC_MIXEDCASE, 15);
            string Jsonbody = "{\"name\":\"apitesting\",\"description\": \"API\",\"travelerLabel\": \"Test Mod84\",\"cmiProgram\": \"apitesting\",\"groupId\": 12,\"id\": 90870}";
            var response = APICommonMethods.ResponseFromPutRequest(AppNameHelper.ApiBaseUrl, "/modules", Jsonbody);
            //dynamic jsonResponse = JObject.Parse(response.Content);
            //String ErrorMsg = jsonResponse.errors.GroupId[0];
            //dynamic jsonResponse =JsonConvert.DeserializeObject(response.Content);
            Console.WriteLine(response.Content);
            String ErrorString= @"""EMV Module Id 90870 doesn't exists.""";
            Assert.That(response.Content, Is.EqualTo(ErrorString));
            Assert.That((int)response.StatusCode, Is.EqualTo(404));
        }

        [Test]
        [Description("EMV Module Get")]
        [Category("Smoke")]
        [TestCase("Automation_EMV_Module_Get")]
        public void EMV_Module_Get(string APItoken)
        {

            var response = APICommonMethods.ResponseFromGETrequest(AppNameHelper.ApiBaseUrl, "/modules");
            //Console.WriteLine(response.Content);
            //Assert.That((int)response.StatusCode, Is.EqualTo(200));
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

        //EMVModule ID Values from response and DB

        [Test]
        [Description("EMV Module Ids Match")]
        [Category("Smoke")]
        [TestCase("Automation_EMVModule_Get_IDMatch_DB")]
        public void EMV_Module_Get_IDMatch_DB(string APItoken)
        {
            var response = APICommonMethods.ResponseFromGETrequest(AppNameHelper.ApiBaseUrl, "/modules");
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
            string sql = "select * from dbo.emvmodule e order by id";
            ArrayList ls = DBConnect_Methods.SelectMethod(sql);
            Console.WriteLine("DB VAluessssssssssssssssssssss");
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

        [Test]
        [Description("EMV Script Create")]
        [Category("Smoke")]
        [TestCase("Automation_EMV_Script_Create")]
        public void EMV_Script_Create(string APItoken)
        {
            string randomString = RandomString.GetString(Types.ALPHANUMERIC_MIXEDCASE, 15);
            string Jsonbody = "{\"name\": \""+ randomString+"\",\"description\": \"Script12 Desc\",\"profileName\": \"Scr12\"}";
            var response = APICommonMethods.ResponseFromPostRequest(AppNameHelper.ApiBaseUrl, "/scripts", Jsonbody);
            Console.WriteLine(response.Content);
            if (response.IsSuccessful)
            {
                Assert.That((int)response.StatusCode, Is.EqualTo(201));
                Assert.That(response.Content, Does.Not.Contain("One or more validation errors occurred."));
            }
            else
            {
                Console.WriteLine((int)response.StatusCode);
                Assert.Fail("Test Fail");
            }
        }

        // Name field value is blank
        [Test]
        [Description("EMV Script Create Blank Name")]
        [Category("Smoke")]
        [TestCase("Automation_EMV_Script_Create_BlankName")]
        public void EMV_Script_Create_Blank_Name(string APItoken)
        {
            //string randomString = RandomString.GetString(Types.ALPHANUMERIC_MIXEDCASE, 15);
            string Jsonbody = "{\"name\": \"\",\"description\": \"Script12 Desc\",\"profileName\": \"Scr12\"}";
            var response = APICommonMethods.ResponseFromPostRequest(AppNameHelper.ApiBaseUrl, "/scripts", Jsonbody);
            dynamic jsonResponse = JObject.Parse(response.Content);
            String ErrorMsg = jsonResponse.errors.Name[0];
            //dynamic jsonResponse =JsonConvert.DeserializeObject(response.Content);
            Console.WriteLine(jsonResponse.errors.Name[0]);
            Assert.That(ErrorMsg, Is.EqualTo("The Name field is required."));
            Assert.That((int)response.StatusCode, Is.EqualTo(400));
        }

        //Name field value is enter MoreThan50Char
        [Test]
        [Description("EMV Script Create Name MoreThan50Char")]
        [Category("Smoke")]
        [TestCase("Automation_EMV_Script_Create_Name_MoreThan50Char")]
        public void EMV_Script_Create_Name_MoreThan50Char(string APItoken)
        {
            //string randomString = RandomString.GetString(Types.ALPHANUMERIC_MIXEDCASE, 15);
            string Jsonbody = "{\"name\": \"apitestingapitestingapitestingapitestingapitestinga\",\"description\": \"Script12 Desc\",\"profileName\": \"Scr12\"}";
            var response = APICommonMethods.ResponseFromPostRequest(AppNameHelper.ApiBaseUrl, "/scripts", Jsonbody);
            dynamic jsonResponse = JObject.Parse(response.Content);
            String ErrorMsg = jsonResponse.errors.Name[0];
            //dynamic jsonResponse =JsonConvert.DeserializeObject(response.Content);
            Console.WriteLine(jsonResponse.errors.Name[0]);
            Assert.That(ErrorMsg, Is.EqualTo("Name can accept 50 characters or fewer. You entered 51 characters."));
            Assert.That((int)response.StatusCode, Is.EqualTo(400));
        }

        // description field value is blank
        [Test]
        [Description("EMV Script Create Blank description")]
        [Category("Smoke")]
        [TestCase("Automation_EMV_Script_Create_Blankdescription")]
        public void EMV_Script_Create_Blank_description(string APItoken)
        {
            string randomString = RandomString.GetString(Types.ALPHANUMERIC_MIXEDCASE, 15);
            string Jsonbody = "{\"name\": \""+ randomString+"\",\"description\": \"\",\"profileName\": \"Scr12\"}";
            var response = APICommonMethods.ResponseFromPostRequest(AppNameHelper.ApiBaseUrl, "/scripts", Jsonbody);
            dynamic jsonResponse = JObject.Parse(response.Content);
            String ErrorMsg = jsonResponse.errors.Description[0];
            //dynamic jsonResponse =JsonConvert.DeserializeObject(response.Content);
            Console.WriteLine(jsonResponse.errors.Description[0]);
            Assert.That(ErrorMsg, Is.EqualTo("The Description field is required."));
            Assert.That((int)response.StatusCode, Is.EqualTo(400));
        }

        //description field value is enter MoreThan50Char
        [Test]
        [Description("EMV Script Create description MoreThan50Char")]
        [Category("Smoke")]
        [TestCase("Automation_EMV_Script_Create_description_MoreThan50Char")]
        public void EMV_Script_Create_description_MoreThan50Char(string APItoken)
        {
            string randomString = RandomString.GetString(Types.ALPHANUMERIC_MIXEDCASE, 15);
            string Jsonbody = "{\"name\": \""+ randomString +"\",\"description\": \"apitestingapitestingapitestingapitestingapitestinga\",\"profileName\": \"Scr12\"}";
            var response = APICommonMethods.ResponseFromPostRequest(AppNameHelper.ApiBaseUrl, "/scripts", Jsonbody);
            Console.WriteLine(response.Content);
            dynamic jsonResponse = JObject.Parse(response.Content);
            String ErrorMsg = jsonResponse.errors.Description[0];
            //dynamic jsonResponse =JsonConvert.DeserializeObject(response.Content);
            Console.WriteLine(jsonResponse.errors.Description[0]);
            Assert.That(ErrorMsg, Is.EqualTo("Description can accept 50 characters or fewer. You entered 51 characters."));
            Assert.That((int)response.StatusCode, Is.EqualTo(400));
        }


        [Test]
        [Description("EMV Script Update")]
        [Category("Smoke")]
        [TestCase("Automation_Script_Update")]
        public void EMV_Script_Update(string APItoken)
        {
            string Jsonbody = "{\"name\": \"Script89\",\"description\": \"Script12 Desc\",\"profileName\": \"Scr12\",\"id\":89}";
            var response = APICommonMethods.ResponseFromPutRequest(AppNameHelper.ApiBaseUrl, "/scripts", Jsonbody);
            Console.WriteLine(response.Content);
            Assert.That((int)response.StatusCode, Is.EqualTo(200));
        }

        // Name field value is blank
        [Test]
        [Description("EMV Script Update Blank Name")]
        [Category("Smoke")]
        [TestCase("Automation_EMV_Script_Update_BlankName")]
        public void EMV_Script_Update_Blank_Name(string APItoken)
        {
            //string randomString = RandomString.GetString(Types.ALPHANUMERIC_MIXEDCASE, 15);
            string Jsonbody = "{\"name\": \"\",\"description\": \"Script12 Desc\",\"profileName\": \"Scr12\",\"id\":89}";
            var response = APICommonMethods.ResponseFromPutRequest(AppNameHelper.ApiBaseUrl, "/scripts", Jsonbody);
            dynamic jsonResponse = JObject.Parse(response.Content);
            String ErrorMsg = jsonResponse.errors.Name[0];
            //dynamic jsonResponse =JsonConvert.DeserializeObject(response.Content);
            Console.WriteLine(jsonResponse.errors.Name[0]);
            Assert.That(ErrorMsg, Is.EqualTo("The Name field is required."));
            Assert.That((int)response.StatusCode, Is.EqualTo(400));
        }

        //Name field value is enter MoreThan50Char
        [Test]
        [Description("EMV Script Update Name MoreThan50Char")]
        [Category("Smoke")]
        [TestCase("Automation_EMV_Script_Update_Name_MoreThan50Char")]
        public void EMV_Script_Update_Name_MoreThan50Char(string APItoken)
        {
            //string randomString = RandomString.GetString(Types.ALPHANUMERIC_MIXEDCASE, 15);
            string Jsonbody = "{\"name\": \"apitestingapitestingapitestingapitestingapitestinga\",\"description\": \"Script12 Desc\",\"profileName\": \"Scr12\",\"id\":89}";
            var response = APICommonMethods.ResponseFromPutRequest(AppNameHelper.ApiBaseUrl, "/scripts", Jsonbody);
            dynamic jsonResponse = JObject.Parse(response.Content);
            String ErrorMsg = jsonResponse.errors.Name[0];
            //dynamic jsonResponse =JsonConvert.DeserializeObject(response.Content);
            Console.WriteLine(jsonResponse.errors.Name[0]);
            Assert.That(ErrorMsg, Is.EqualTo("Name can accept 50 characters or fewer. You entered 51 characters."));
            Assert.That((int)response.StatusCode, Is.EqualTo(400));
        }

        // description field value is blank
        [Test]
        [Description("EMV Script Update Blank description")]
        [Category("Smoke")]
        [TestCase("Automation_EMV_Script_Update_Blankdescription")]
        public void EMV_Script_Update_Blank_description(string APItoken)
        {
            string randomString = RandomString.GetString(Types.ALPHANUMERIC_MIXEDCASE, 15);
            string Jsonbody = "{\"name\": \"" + randomString + "\",\"description\": \"\",\"profileName\": \"Scr12\",\"id\":89}";
            var response = APICommonMethods.ResponseFromPutRequest(AppNameHelper.ApiBaseUrl, "/scripts", Jsonbody);
            dynamic jsonResponse = JObject.Parse(response.Content);
            String ErrorMsg = jsonResponse.errors.Description[0];
            //dynamic jsonResponse =JsonConvert.DeserializeObject(response.Content);
            Console.WriteLine(jsonResponse.errors.Description[0]);
            Assert.That(ErrorMsg, Is.EqualTo("The Description field is required."));
            Assert.That((int)response.StatusCode, Is.EqualTo(400));
        }

        //description field value is enter MoreThan50Char
        [Test]
        [Description("EMV Script Update description MoreThan50Char")]
        [Category("Smoke")]
        [TestCase("Automation_EMV_Script_Update_description_MoreThan50Char")]
        public void EMV_Script_Update_description_MoreThan50Char(string APItoken)
        {
            string randomString = RandomString.GetString(Types.ALPHANUMERIC_MIXEDCASE, 15);
            string Jsonbody = "{\"name\": \"" + randomString + "\",\"description\": \"apitestingapitestingapitestingapitestingapitestinga\",\"profileName\": \"Scr12\",\"id\":89}";
            var response = APICommonMethods.ResponseFromPutRequest(AppNameHelper.ApiBaseUrl, "/scripts", Jsonbody);
            Console.WriteLine(response.Content);
            dynamic jsonResponse = JObject.Parse(response.Content);
            String ErrorMsg = jsonResponse.errors.Description[0];
            //dynamic jsonResponse =JsonConvert.DeserializeObject(response.Content);
            Console.WriteLine(jsonResponse.errors.Description[0]);
            Assert.That(ErrorMsg, Is.EqualTo("Description can accept 50 characters or fewer. You entered 51 characters."));
            Assert.That((int)response.StatusCode, Is.EqualTo(400));
        }



        [Test]
        [Description("EMV Script Get Method")]
        [Category("Smoke")]
        [TestCase("Automation_EMV_Script_Get")]
        public void EMV_Script_Get(string APItoken)
        {

            var response = APICommonMethods.ResponseFromGETrequest(AppNameHelper.ApiBaseUrl, "/scripts");
            //Console.WriteLine(response.Content);
            Assert.That((int)response.StatusCode, Is.EqualTo(200));

        }

        [Test]
        [Description("EMV Issuer Get Ids Match")]
        [Category("Smoke")]
        [TestCase("Automation_EMV_Issuer_Get_IDMatch_DB")]
        public void EMV_Script_Get_IDMatch_DB(string APItoken)
        {
            var response = APICommonMethods.ResponseFromGETrequest(AppNameHelper.ApiBaseUrl, "/scripts");
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
            string sql = "SELECT * from dbo.emvpersonalizationscript e where e.\"IsDeleted\" =false  order by id";
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


    }


}