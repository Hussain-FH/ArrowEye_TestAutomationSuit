using System;
using System.Collections.Generic;

namespace ArrowEye_Automation_Framework.Excel
{
    public class TestCaseData
    {
        Dictionary<string, string> columnRowMapping = new Dictionary<string, string>();

        public Dictionary<string, string> GetColumnRowMapping() { return columnRowMapping; }

        public void SetColumnRowMapping(Dictionary<string, string> columnRowMapping) { this.columnRowMapping = columnRowMapping; }

        public TestCaseData()
        {
        }

        public String GetValue(string columnName)
        {
            return columnRowMapping[columnName];
        }
    }
}
