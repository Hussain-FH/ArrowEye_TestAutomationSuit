using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Npgsql;

namespace ArrowEye_Automation_Framework.API
{
      class DBConnect_Methods
    {
        static string connectionString = "Host=asxtwebdbc-dev.ckmkjhucjkjr.us-west-2.rds.amazonaws.com;Username=postgres;Password=PortalDev2024!;Database=4YSClientPortalServer;sslmode=Require;Trust Server Certificate=true";

        static public ArrayList SelectMethod(String Sql)
        {
            //string connectionString = "Host=asxtwebdbc-dev.ckmkjhucjkjr.us-west-2.rds.amazonaws.com;Username=postgres;Password=PortalDev2024!;Database=QA_4YSClientPortalServer;sslmode=Require;Trust Server Certificate=true";
            var conn = new NpgsqlConnection(connectionString);
            conn.Open();
            //string sql = "SELECT * FROM dbo.emvcardprofile limit 10";
            ArrayList IdList = new ArrayList();
            using (var cmd = new NpgsqlCommand(Sql, conn))
            {
                // Execute the command and read the data
                using (var reader = cmd.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        IdList.Add(reader.GetValue(0));
                        //Console.WriteLine(reader.GetValue(0));
                        //Console.WriteLine(IdList[0]); // Adjust based on your table structure
                        //IdList.Add(reader.GetValue(0));
                    }
                }
            }
            return IdList;
        }
    }
}
