using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using ArrowEye_Automation_Framework.Common;
using Newtonsoft.Json.Linq;
using NUnit.Framework;
using RandomString4Net;

namespace ArrowEye_Automation_Framework.API
{
    internal class API_Product
    {

        [Test]
        [Description("Products BOC Create")]
        [Category("Smoke")]
        [TestCase("Automation_ProductsBOC_Create")]
        public void Products_BOC_Create(string APItoken)
        {
            string randomString = RandomString.GetString(Types.ALPHANUMERIC_MIXEDCASE, 15);
            string Jsonbody = "{\"baseTemplateId\": 36,\"name\": \"Name Test BOC Demo\",\"description\": \"Desc Test BOC Demo\",\"pclId\": 109}";
            var response = APICommonMethods.ResponseFromPostRequest(AppNameHelper.ApiBaseUrl, "/bocs", Jsonbody);
            var toasterMessageID = Regex.Match(response.Content, @"\d+").Value;
            String OutputVal = response.Content.ToString().Replace("\"", "");
            String txt = "Back Card Template " + toasterMessageID + " Added Successfully.";
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

        // Name field value is not entered
        [Test]
        [Description("Products BOC Create")]
        [Category("Smoke")]
        [TestCase("Automation_ProductsBOC_Create")]
        public void Products_BOC_Create_Name_Blank(string APItoken)
        {
            string randomString = RandomString.GetString(Types.ALPHANUMERIC_MIXEDCASE, 15);
            string Jsonbody = "{\"baseTemplateId\": 36,\"name\": \"\",\"description\": \"Desc Test BOC Demo\",\"pclId\": 109}";
            var response = APICommonMethods.ResponseFromPostRequest(AppNameHelper.ApiBaseUrl, "/bocs", Jsonbody);
            dynamic jsonResponse = JObject.Parse(response.Content);
            String ErrorMsg = jsonResponse.errors.Name[0];
            Console.WriteLine(jsonResponse.errors.Name[0]);
            Assert.That(ErrorMsg, Is.EqualTo("'Name' must not be empty."));
            Assert.That((int)response.StatusCode, Is.EqualTo(400));
        }

        //character are more than 100 in Name field
        [Test]
        [Description("Products BOC Create")]
        [Category("Smoke")]
        [TestCase("Automation_ProductsBOC_Create")]
        public void Products_BOC_Create_Name_Charmore100(string APItoken)
        {
            string randomString = RandomString.GetString(Types.ALPHANUMERIC_MIXEDCASE, 15);
            string Jsonbody = "{\"baseTemplateId\": 36,\"name\": \"apitestingapitestingapitestingapitestingapitestingaapitestingapitestingapitestingapitestingapitesting\",\"description\": \"Desc Test BOC Demo\",\"pclId\": 109}";
            var response = APICommonMethods.ResponseFromPostRequest(AppNameHelper.ApiBaseUrl, "/bocs", Jsonbody);
            dynamic jsonResponse = JObject.Parse(response.Content);
            String ErrorMsg = jsonResponse.errors.Name[0];
            Console.WriteLine(jsonResponse.errors.Name[0]);
            Assert.That(ErrorMsg, Is.EqualTo("The length of 'Name' must be 100 characters or fewer. You entered 101 characters."));
            Assert.That((int)response.StatusCode, Is.EqualTo(400));
        }

        // Description is not entered
        [Test]
        [Description("Products BOC Create")]
        [Category("Smoke")]
        [TestCase("Automation_ProductsBOC_Create")]
        public void Products_BOC_Create_Description_Blank(string APItoken)
        {
            string randomString = RandomString.GetString(Types.ALPHANUMERIC_MIXEDCASE, 15);
            string Jsonbody = "{\"baseTemplateId\": 36,\"name\": \"Name Test BOC Demo\",\"description\": \"\",\"pclId\": 109}";
            var response = APICommonMethods.ResponseFromPostRequest(AppNameHelper.ApiBaseUrl, "/bocs", Jsonbody);
            dynamic jsonResponse = JObject.Parse(response.Content);
            String ErrorMsg = jsonResponse.errors.Description[0];
            Console.WriteLine(jsonResponse.errors.Description[0]);
            Assert.That(ErrorMsg, Is.EqualTo("'Description' must not be empty."));
            Assert.That((int)response.StatusCode, Is.EqualTo(400));
        }

        //character  more than 200 in Description field
        [Test]
        [Description("Products BOC Create")]
        [Category("Smoke")]
        [TestCase("Automation_ProductsBOC_Create")]
        public void Products_BOC_Create_Description_Charmore100(string APItoken)
        {
            string randomString = RandomString.GetString(Types.ALPHANUMERIC_MIXEDCASE, 15);
            string Jsonbody = "{\"baseTemplateId\": 36,\"name\": \"Name Test BOC Demo\",\"description\": \"apitestingapitestingapitestingapitestingapitestingaapitestingapitestingapitestingapitestingapitestingapitestingapitestingapitestingapitestingapitestingapitestingapitestingapitestingapitestingapitesting\",\"pclId\": 109}";
            var response = APICommonMethods.ResponseFromPostRequest(AppNameHelper.ApiBaseUrl, "/bocs", Jsonbody);
            dynamic jsonResponse = JObject.Parse(response.Content);
            String ErrorMsg = jsonResponse.errors.Description[0];
            Console.WriteLine(jsonResponse.errors.Description[0]);
            Assert.That(ErrorMsg, Is.EqualTo("The length of 'Description' must be 200 characters or fewer. You entered 201 characters."));
            Assert.That((int)response.StatusCode, Is.EqualTo(400));
        }

        // enters a Invalid Base Template [Test]
        [Description("Products BOC Create")]
        [Category("Smoke")]
        [TestCase("Automation_ProductsBOC_Create")]
        public void Products_BOC_Create_Invalid_baseTemplateId(string APItoken)
        {
            string randomString = RandomString.GetString(Types.ALPHANUMERIC_MIXEDCASE, 15);
            string Jsonbody = "{\"baseTemplateId\": 999999999,\"name\": \"Name Test BOC Demo\",\"description\": \"apitesting\",\"pclId\": 109}";
            var response = APICommonMethods.ResponseFromPostRequest(AppNameHelper.ApiBaseUrl, "/bocs", Jsonbody);
            //dynamic jsonResponse = JObject.Parse(response.Content);
            //String ErrorMsg = jsonResponse.errors.Description[0];
            //Console.WriteLine(jsonResponse.errors.Description[0]);
            //Assert.That(ErrorMsg, Is.EqualTo("The length of 'Description' must be 200 characters or fewer. You entered 201 characters."));
            //Assert.That((int)response.StatusCode, Is.EqualTo(400));
        }
    }
}
