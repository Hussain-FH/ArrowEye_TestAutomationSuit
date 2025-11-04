using System;
using System.Collections.Generic;
using System.Linq;

using System.Configuration;


namespace ArrowEye_Automation_Framework.Common
{
     public static class AppNameHelper
     {
        static public string appBaseURL
        {
            get
            {
                return ConfigurationManager.AppSettings["Environment"];
            }
        }

        static public string apiToken
        {
            get
            {
                return ConfigurationManager.AppSettings["ApiDevToken"];
            }
        }

        static public string ApiBaseUrl
        {
            get
            {
                return ConfigurationManager.AppSettings["apiDevUrl"];
            }
        }






    }
}
