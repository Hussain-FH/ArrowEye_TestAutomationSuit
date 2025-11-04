using System;
using System.Collections.Generic;
using System.Diagnostics.Metrics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ArrowEye_Automation_Framework.Common;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using NUnit.Framework;
using NUnit.Framework.Legacy;
using RandomString4Net;
using System.Text.RegularExpressions;
using System.Collections;
using System.Text.Json.Nodes;


namespace ArrowEye_Automation_Framework.API
{
    internal class API_CSPSettings
    {
        [Test]
        [Description("CSPSettings PostMethod")]
        [Category("Smoke")]
        [TestCase("Automation_CSP_Settings_PostRequestMethod")]
        public void CSP_Settings_PostRequestMethod(string APItoken)
        {
            string randomString = RandomString.GetString(Types.ALPHANUMERIC_MIXEDCASE, 15);
            string Jsonbody = "{\"name\":\"" + randomString + "\",\n  \"pclId\": 108,\n  \"infoType\": 3}";
            var response = APICommonMethods.ResponseFromPostRequest(AppNameHelper.ApiBaseUrl, "/cspsettings", Jsonbody);
            Console.WriteLine(response.Content);
            if (response.IsSuccessful)
            {
                //var regex = new Regex(@"\d+");
                Assert.That((int)response.StatusCode, Is.EqualTo(201));
                Assert.That(response.Content, Does.Not.Contain("One or more validation errors occurred."));
                //Assert.That(response.Content, Is.EqualTo("EMV Profile"+ regex.Match("[0-9 0-9]") + "Added Successfully."));                
            }
            else
            {
                Console.WriteLine((int)response.StatusCode);
                Assert.Fail("Test Fail");
            }
        }

        //Name field value is not entered
        [Test]
        [Description("CSPSettings PostMethod_BlankName")]
        [Category("Smoke")]
        [TestCase("Automation_CSP_Settings_Post_BlankName")]
        public void CSP_Settings_Post_BlankName(string APItoken)
        {
            string randomString = RandomString.GetString(Types.ALPHANUMERIC_MIXEDCASE, 15);
            string Jsonbody = "{\"name\": \"\",\"pclId\": 108, \"infoType\": 3}";
            var response = APICommonMethods.ResponseFromPostRequest(AppNameHelper.ApiBaseUrl, "/cspsettings", Jsonbody);
            //Console.WriteLine(response.Content);
            dynamic jsonResponse = JObject.Parse(response.Content);
            String ErrorMsg = jsonResponse.errors.Name[0];
            //dynamic jsonResponse =JsonConvert.DeserializeObject(response.Content);
            Console.WriteLine(jsonResponse.errors.Name[0]);
            Assert.That(ErrorMsg, Is.EqualTo("The EMV Profile Name field is required."));
            Assert.That((int)response.StatusCode, Is.EqualTo(400));

        }

        //Name field value is enter MoreThan50Char
        [Test]
        [Description("CSPSettings PostMethod_Name_MoreThan50Char")]
        [Category("Smoke")]
        [TestCase("Automation_CSP_Settings_Post_Name_MoreThan50Char")]
        public void CSP_Settings_Post_Name_MoreThan50Char(string APItoken)
        {
            string randomString = RandomString.GetString(Types.ALPHANUMERIC_MIXEDCASE, 15);
            string Jsonbody = "{\"name\": \"apitestingapitestingapitestingapitestingapitestinga\",\"pclId\": 108,\"infoType\": 3}";
            var response = APICommonMethods.ResponseFromPostRequest(AppNameHelper.ApiBaseUrl, "/cspsettings", Jsonbody);
            //Console.WriteLine(response.Content);
            dynamic jsonResponse = JObject.Parse(response.Content);
            String ErrorMsg = jsonResponse.errors.Name[0];
            //dynamic jsonResponse =JsonConvert.DeserializeObject(response.Content);
            Console.WriteLine(jsonResponse.errors.Name[0]);
            Assert.That(ErrorMsg, Is.EqualTo("EMV Profile Name can accept 50 characters or fewer. You have entered 51 characters."));
            Assert.That((int)response.StatusCode, Is.EqualTo(400));

        }


        //Name field value is enter MoreThan100Char
        [Test]
        [Description("CSPSettings PostMethod_Name_MoreThan100Char")]
        [Category("Smoke")]
        [TestCase("Automation_CSP_Settings_Post_Name_MoreThan100Char")]
        public void CSP_Settings_Post_Name_MoreThan100Char(string APItoken)
        {
            string randomString = RandomString.GetString(Types.ALPHANUMERIC_MIXEDCASE, 15);
            string Jsonbody = "{\"name\": \"apitestingapitestingapitestingapitestingapitestingapitestingapitestingapitestingapitestingapitestingh\",\"pclId\": 108,\"infoType\": 3}";
            var response = APICommonMethods.ResponseFromPostRequest(AppNameHelper.ApiBaseUrl, "/cspsettings", Jsonbody);
            //Console.WriteLine(response.Content);
            dynamic jsonResponse = JObject.Parse(response.Content);
            String ErrorMsg = jsonResponse.errors.Name[0];
            //dynamic jsonResponse =JsonConvert.DeserializeObject(response.Content);
            Console.WriteLine(jsonResponse.errors.Name[0]);
            Assert.That(ErrorMsg, Is.EqualTo("EMV Profile Name can accept 50 characters or fewer. You have entered 101 characters."));
            Assert.That((int)response.StatusCode, Is.EqualTo(400));

        }

        [Test]
        [Description("CSPSettings PutMethod")]
        [Category("Smoke")]
        [TestCase("Automation_CSPSettings_PutRequestMethod")]
        public void CSPSettings_PutRequestMethod(string APItoken)
        {
            string Jsonbody = "{\"name\": \"CSP281\",\"pclId\": 108,\"infoType\": 3, \"id\": 281}";
            var response = APICommonMethods.ResponseFromPutRequest(AppNameHelper.ApiBaseUrl, "/cspsettings", Jsonbody);
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

        //Name field value is not entered
        [Test]
        [Description("CSPSettings PutMethod_BlankName")]
        [Category("Smoke")]
        [TestCase("Automation_CSPSettings_PutRequestMethod_BlankName")]
        public void CSPSettings_PutRequestMethod_BlankName(string APItoken)
        {
            string Jsonbody = "{\"name\": \"\",\"pclId\": 108,\"infoType\": 3, \"id\": 281}";
            var response = APICommonMethods.ResponseFromPutRequest(AppNameHelper.ApiBaseUrl, "/cspsettings", Jsonbody);
            //Console.WriteLine(response.Content);
            dynamic jsonResponse = JObject.Parse(response.Content);
            String ErrorMsg = jsonResponse.errors.Name[0];
            //dynamic jsonResponse =JsonConvert.DeserializeObject(response.Content);
            Console.WriteLine(jsonResponse.errors.Name[0]);
            Assert.That(ErrorMsg, Is.EqualTo("The EMV Profile Name field is required."));
            Assert.That((int)response.StatusCode, Is.EqualTo(400));

        }

        //Invalid Pclid
        [Test]
        [Description("CSPSettings PutMethod_Invalid_Pclid")]
        [Category("Smoke")]
        [TestCase("Automation_CSPSettings_PutRequestMethod_Invalid_Pclid")]
        public void CSPSettings_PutRequestMethod_Invalid_Pclid(string APItoken)
        {
            string Jsonbody = "{\"name\":\"CSP281\",\"pclId\": 0,\"infoType\": 3, \"id\": 281}";
            var response = APICommonMethods.ResponseFromPutRequest(AppNameHelper.ApiBaseUrl, "/cspsettings", Jsonbody);
            //Console.WriteLine(response.Content);
            dynamic jsonResponse = JObject.Parse(response.Content);
            Console.WriteLine(jsonResponse);
            String ErrorMsg = jsonResponse.errors.PclId[0];
            //dynamic jsonResponse =JsonConvert.DeserializeObject(response.Content);
            //Console.WriteLine(jsonResponse.errors.Name[0]);
            Assert.That(ErrorMsg, Is.EqualTo("The EMV Profile PCL Id field is required."));
            Assert.That((int)response.StatusCode, Is.EqualTo(400));

        }

        //Name field value is enter MoreThan50Char
        [Test]
        [Description("CSPSettings PutMethod_MoreThan50Char")]
        [Category("Smoke")]
        [TestCase("Automation_CSPSettings_PutRequestMethod_MoreThan50Char")]
        public void CSPSettings_PutRequestMethod_MoreThan50Char(string APItoken)
        {
            string Jsonbody = "{\"name\":\"apitestingapitestingapitestingapitestingapitestinga\",\"pclId\": 0,\"infoType\": 3, \"id\": 281}";
            var response = APICommonMethods.ResponseFromPutRequest(AppNameHelper.ApiBaseUrl, "/cspsettings", Jsonbody);
            //Console.WriteLine(response.Content);
            dynamic jsonResponse = JObject.Parse(response.Content);
            Console.WriteLine(jsonResponse);
            String ErrorMsg = jsonResponse.errors.Name[0];
            //dynamic jsonResponse =JsonConvert.DeserializeObject(response.Content);
            //Console.WriteLine(jsonResponse.errors.Name[0]);
            Assert.That(ErrorMsg, Is.EqualTo("EMV Profile Name can accept 50 characters or fewer. You have entered 51 characters."));
            Assert.That((int)response.StatusCode, Is.EqualTo(400));

        }
        //CSPSettings GetMethod
        [Test]
        [Description("CSPSettings GetMethod")]
        [Category("Smoke")]
        [TestCase("Automation_CSPSettings_GetRequestMethod")]
        public void CSPSettings_GetRequestMethod(string APItoken)
        {

            var response = APICommonMethods.ResponseFromGETrequest(AppNameHelper.ApiBaseUrl, "/cspsettings?infotype=1&&PclId=108");
            //Console.WriteLine((int)response.StatusCode);
            Assert.That((int)response.StatusCode, Is.EqualTo(200));
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

        //CSPSettings ID Values from response and DB

        [Test]
        [Description("CSPSettings Ids Match")]
        [Category("Smoke")]
        [TestCase("Automation_CSPSettings_Get_IDMatch_DB")]
        public void CSPSettings_Get_IDMatch_DB(string APItoken)
        {
            var response = APICommonMethods.ResponseFromGETrequest(AppNameHelper.ApiBaseUrl, "/cspsettings?infotype=1&&PclId=108");
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
            string sql = "SELECT * FROM dbo.clientprogramsetupoption where optiontypeid=1 and pclid=108 and isdeleted='false' order by id";
            ArrayList ls = DBConnect_Methods.SelectMethod(sql);
            Console.WriteLine("DBBBBBBBBBBBBBBBBBB vales");
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

        //CSP Settings Delete
        [Test]
        [Description("CSP Settings DeleteMethod")]
        [Category("Smoke")]
        [TestCase("Automation_CSPSettings_DeleteRequestMethod")]
        public void CSPSettings_DeleteMethod(string APItoken)
        {
            string Jsonbody = "{\"id\": 282,\"infoType\": 3}";
            var response = APICommonMethods.ResponseFromDeleteRequest(AppNameHelper.ApiBaseUrl, "/cspsettings", Jsonbody);
            Console.WriteLine(response.Content);
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

        //CSP Settings Delete Blank values
        [Test]
        [Description("CSP Settings Delete_Invalid_Values")]
        [Category("Smoke")]
        [TestCase("Automation_CSPSettings_Invalid_Values")]
        public void CSPSettings_Delete_Invalid_Values(string APItoken)
        {
            string Jsonbody = "{\"id\": 0,\"infoType\": 0}";
            var response = APICommonMethods.ResponseFromDeleteRequest(AppNameHelper.ApiBaseUrl, "/cspsettings", Jsonbody);
            Console.WriteLine(response.Content);         
            dynamic jsonResponse = JObject.Parse(response.Content);
            String ErrorMsg = jsonResponse.errors.Id[0];
            String Error2Msg = jsonResponse.errors.InfoType[0];
            Console.WriteLine(jsonResponse);
            //dynamic jsonResponse =JsonConvert.DeserializeObject(response.Content);
            Console.WriteLine(jsonResponse.errors.Id[0]);
            Assert.That(ErrorMsg, Is.EqualTo("'Id' must not be empty."));
            Assert.That(Error2Msg, Is.EqualTo("InfoType sent is invalid."));
            Assert.That((int)response.StatusCode, Is.EqualTo(400));
        }
        [Test]
        [Description("CardHolderAggrement PostMethod")]
        [Category("Smoke")]
        [TestCase("Automation_CardHolderAggrement_PostRequestMethod")]
        public void CardHolderAggrement_PostRequestMethod(string APItoken)
        {
            string randomString = RandomString.GetString(Types.ALPHANUMERIC_MIXEDCASE, 15);
            // string Jsonbody = "{\"name\":\"" + randomString + "\",\n  \"pclId\": 108,\n  \"infoType\": 3}";
            string Jsonbody = "{\"name\":\"" + randomString + "\",\"pclId\": 110,\"description\": \"testuser\"}";
            var response = APICommonMethods.ResponseFromPostRequest(AppNameHelper.ApiBaseUrl, "/cspsettings/cardholderagreement", Jsonbody);
            //Console.WriteLine(response.Content);
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

        //Name field value is not entered
        [Test]
        [Description("CardHolderAggrement PostMethod_BlankName")]
        [Category("Smoke")]
        [TestCase("Automation_CardHolderAggrement_PostRequestMethod_BlankName")]
        public void CardHolderAggrement_PostRequestMethod_BlankName(string APItoken)
        {
            //"{\"name\":\"CSP281\",\"pclId\": 0,\"infoType\": 3, \"id\": 281}";
            string randomString = RandomString.GetString(Types.ALPHANUMERIC_MIXEDCASE, 15);

            string Jsonbody = "{\"name\": \"\",\"pclId\": 108,\"description\": \"testuser\"}";
            var response = APICommonMethods.ResponseFromPostRequest(AppNameHelper.ApiBaseUrl, "/cspsettings/cardholderagreement", Jsonbody);
            Console.WriteLine(response.Content);
            dynamic jsonResponse = JObject.Parse(response.Content);
            String ErrorMsg = jsonResponse.errors.Name[0];
            Console.WriteLine(jsonResponse);
            //dynamic jsonResponse =JsonConvert.DeserializeObject(response.Content);
            Console.WriteLine(jsonResponse.errors.Name[0]);
            Assert.That(ErrorMsg, Is.EqualTo("'Name' must not be empty."));
            Assert.That((int)response.StatusCode, Is.EqualTo(400));
        }

        //Name field value is enter MoreThan50Char

        [Test]
        [Description("CardHolderAggrement PostMethod_Name_MoreThan50Char")]
        [Category("Smoke")]
        [TestCase("Automation_CardHolderAggrement_PostRequestMethod_Name_MoreThan50Char")]
        public void CardHolderAggrement_PostRequestMethod_Name_MoreThan50Char(string APItoken)
        {
            //"{\"name\":\"CSP281\",\"pclId\": 0,\"infoType\": 3, \"id\": 281}";
            string randomString = RandomString.GetString(Types.ALPHANUMERIC_MIXEDCASE, 15);

            string Jsonbody = "{\"name\": \"apitestingapitestingapitestingapitestingapitestinga\",\"pclId\": 108,\"description\": \"testuser\"}";
            var response = APICommonMethods.ResponseFromPostRequest(AppNameHelper.ApiBaseUrl, "/cspsettings/cardholderagreement", Jsonbody);
            Console.WriteLine(response.Content);
            dynamic jsonResponse = JObject.Parse(response.Content);
            String ErrorMsg = jsonResponse.errors.Name[0];
            Console.WriteLine(jsonResponse);
            //dynamic jsonResponse =JsonConvert.DeserializeObject(response.Content);
            Console.WriteLine(jsonResponse.errors.Name[0]);
            Assert.That(ErrorMsg, Is.EqualTo("The length of 'Name' must be 50 characters or fewer. You entered 51 characters."));
            Assert.That((int)response.StatusCode, Is.EqualTo(400));

        }

        //Name field value is enter MoreThan100Char

        [Test]
        [Description("CardHolderAggrement PostMethod_Name_MoreThan100Char")]
        [Category("Smoke")]
        [TestCase("Automation_CardHolderAggrement_PostRequestMethod_Name_MoreThan100Char")]
        public void CardHolderAggrement_PostRequestMethod_Name_MoreThan100Char(string APItoken)
        {
            //"{\"name\":\"CSP281\",\"pclId\": 0,\"infoType\": 3, \"id\": 281}";
            string randomString = RandomString.GetString(Types.ALPHANUMERIC_MIXEDCASE, 15);

            string Jsonbody = "{\"name\": \"apitestingapitestingapitestingapitestingapitestingapitestingapitestingapitestingapitestingapitestinga\",\"pclId\": 108,\"description\": \"testuser\"}";
            var response = APICommonMethods.ResponseFromPostRequest(AppNameHelper.ApiBaseUrl, "/cspsettings/cardholderagreement", Jsonbody);
            Console.WriteLine(response.Content);
            dynamic jsonResponse = JObject.Parse(response.Content);
            String ErrorMsg = jsonResponse.errors.Name[0];
            Console.WriteLine(jsonResponse);
            //dynamic jsonResponse =JsonConvert.DeserializeObject(response.Content);
            Console.WriteLine(jsonResponse.errors.Name[0]);
            Assert.That(ErrorMsg, Is.EqualTo("The length of 'Name' must be 50 characters or fewer. You entered 101 characters."));
            Assert.That((int)response.StatusCode, Is.EqualTo(400));

        }

        [Test]
        [Description("CardHolderAggrement PutMethod")]
        [Category("Smoke")]
        [TestCase("Automation_CardHolderAggrement_PutRequestMethod")]
        public void CardHolderAggrement_PutRequestMethod(string APItoken)
        {
            string Jsonbody = "{\"name\": \"Card106\",\"pclId\": 108,\"description\": \"Card106 Desc\",\"cardHolderAgreementId\": 106}";
            var response = APICommonMethods.ResponseFromPutRequest(AppNameHelper.ApiBaseUrl, "/cspsettings/cardholderagreement", Jsonbody);
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


        //Name field value is not entered
        [Test]
        [Description("CardHolderAggrement PutMethod_BlankName")]
        [Category("Smoke")]
        [TestCase("Automation_CardHolderAggrement_PutRequestMethod_BlankName")]
        public void CardHolderAggrement_PutRequestMethod_BlankName(string APItoken)
        {
            string Jsonbody = "{\"name\": \"\",\"pclId\": 108,\"description\": \"Card106 Desc\",\"cardHolderAgreementId\": 106}";
            var response = APICommonMethods.ResponseFromPutRequest(AppNameHelper.ApiBaseUrl, "/cspsettings/cardholderagreement", Jsonbody);
            //Console.WriteLine(response.Content);
            dynamic jsonResponse = JObject.Parse(response.Content);
            String ErrorMsg = jsonResponse.errors.Name[0];
            //dynamic jsonResponse =JsonConvert.DeserializeObject(response.Content);
            Console.WriteLine(jsonResponse.errors.Name[0]);
            Assert.That(ErrorMsg, Is.EqualTo("'Name' must not be empty."));
            Assert.That((int)response.StatusCode, Is.EqualTo(400));

        }



        //Name field value is enter MoreThan50Char
        [Test]
        [Description("CardHolderAggrement PutMethod_MoreThan50Char")]
        [Category("Smoke")]
        [TestCase("Automation_CardHolderAggrement_PutRequestMethod_MoreThan50Char")]
        public void CardHolderAggrement_PutRequestMethod_MoreThan50Charnnnn(string APItoken)
        {
            string Jsonbody = "{\"name\":\"apitestingapitestingapitestingapitestingapitestinga\",\"pclId\": 108,\"description\": \"Card106 Desc\",\"cardHolderAgreementId\": 106}";
            var response = APICommonMethods.ResponseFromPutRequest(AppNameHelper.ApiBaseUrl, "/cspsettings/cardholderagreement", Jsonbody);
            //Console.WriteLine(response.Content);
            dynamic jsonResponse = JObject.Parse(response.Content);
            Console.WriteLine(jsonResponse);
            String ErrorMsg = jsonResponse.errors.Name[0];
            //dynamic jsonResponse =JsonConvert.DeserializeObject(response.Content);
            Console.WriteLine(jsonResponse.errors.Name[0]);
            Assert.That(ErrorMsg, Is.EqualTo("The length of 'Name' must be 50 characters or fewer. You entered 51 characters."));
            Assert.That((int)response.StatusCode, Is.EqualTo(400));

        }

        //Invalid Pclid
        [Test]
        [Description("CardHolderAggrement PutMethod_Invalid_Pclid")]
        [Category("Smoke")]
        [TestCase("Automation_CardHolderAggrement_PutRequestMethod_Invalid_Pclid")]
        public void CardHolderAggrement_PutRequestMethod_Invalid_Pclid(string APItoken)
        {
            string Jsonbody = "{\"name\": \"\",\"pclId\": 0,\"description\": \"Card106 Desc\",\"cardHolderAgreementId\": 106}";
            var response = APICommonMethods.ResponseFromPutRequest(AppNameHelper.ApiBaseUrl, "/cspsettings/cardholderagreement", Jsonbody);
            //Console.WriteLine(response.Content);
            dynamic jsonResponse = JObject.Parse(response.Content);
            Console.WriteLine(jsonResponse);
            String ErrorMsg = jsonResponse.errors.PclId[0];
            //dynamic jsonResponse =JsonConvert.DeserializeObject(response.Content);
            //Console.WriteLine(jsonResponse.errors.Name[0]);
            Assert.That(ErrorMsg, Is.EqualTo("'Pcl Id' must not be empty."));
            Assert.That((int)response.StatusCode, Is.EqualTo(400));

        }

        //CardHolderAggrement GetMethod
        [Test]
        [Description("CardHolderAggrement GetMethod")]
        [Category("Smoke")]
        [TestCase("Automation_CardHolderAggrement_GetRequestMethod")]
        public void CardHolderAggrement_GetRequestMethod(string APItoken)
        {

            var response = APICommonMethods.ResponseFromGETrequest(AppNameHelper.ApiBaseUrl, "cspsettings/cardholderagreement?PclId=108");
            //Console.WriteLine((int)response.StatusCode);
            Assert.That((int)response.StatusCode, Is.EqualTo(200));
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


        //CardHolAggrement ID Values from response and DB

        [Test]
        [Description("CardHolAggrement Ids Match")]
        [Category("Smoke")]
        [TestCase("Automation_CardHolAggrement_Get_IDMatch_DB")]
        public void CardHolAggrement_Get_IDMatch_DB(string APItoken)
        {
            var response = APICommonMethods.ResponseFromGETrequest(AppNameHelper.ApiBaseUrl, "/cspsettings/cardholderagreement?PclId=108");
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
            string sql = "SELECT * FROM dbo.cardholderagreement where isdeleted='false'and pclid=108 order by cardholderagreementid";
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


        //DB Code
        [Test]
        [Description("EMV Authentication DBConnect")]
        [Category("Smoke")]
        [TestCase("Automation_EMV_Authentication_GetRequestMethod")]
        public void EMV_Authentication_DBConnect(string APItoken)
        {
            var response = APICommonMethods.ResponseFromGETrequest(AppNameHelper.ApiBaseUrl, "/configurations?PclId=108");
            //Console.WriteLine(response.Content);
            JArray jsonArray = JArray.Parse(response.Content);
            //JObject jsonArray = JObject.Parse(response.Content);
            //dynamic jsonResponse = JObject.Parse(response.Content);
            //Console.WriteLine(jsonArray[0]["id"]);
            ArrayList ApiIDValue = new ArrayList();
            foreach (JObject var in jsonArray)
            {
                //jsonArray[var]["id"];
                //var idValu=var.GetValue("id");
                ApiIDValue.Add((int)var.GetValue("id"));
                //Console.WriteLine(idValu);
            }
            ApiIDValue.Sort();
            //Console.WriteLine(ApiIDValue[3]);
            foreach (var it in ApiIDValue)
            {
                //jsonArray[var]["id"];
                //var var = va;
                Console.WriteLine(it);
            }

            Console.WriteLine("#####################");

            string sql = "SELECT * FROM dbo.emvconfiguration where pclid=108 order by id";
            ArrayList ls = DBConnect_Methods.SelectMethod(sql);
            //Console.WriteLine(ls);
            foreach (var item in ls)
            {
                //jsonArray[var]["id"];
                //var var = va;
                Console.WriteLine(item);
            }
            if ((ApiIDValue.ToArray() as IStructuralEquatable).Equals(ls.ToArray(),EqualityComparer<int>.Default))
            {
                Console.WriteLine("Passsssssssssssssssss");

                Assert.Pass();
            }
            else
            {
                Console.WriteLine("Failllllllllllllll");
                Assert.Fail();
            }

        }
    }
}
      

