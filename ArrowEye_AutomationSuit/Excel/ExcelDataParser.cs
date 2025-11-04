using ArrowEye_Automation_Framework.Common;
using NPOI.SS.Formula.Functions;
using NPOI.SS.UserModel;
using NPOI.XSSF.UserModel;
using NUnit.Framework;
using OpenQA.Selenium.BiDi.Modules.Script;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ArrowEye_Automation_Framework.Excel
{
    public class ExcelDataParser
    {
        public static IEnumerable<TestCaseData> TestData(string sheetName, int[] rowNumbers)
        {
            string currentAssemblyPath = System.Reflection.Assembly.GetCallingAssembly().CodeBase;
            UriBuilder uri = new UriBuilder(currentAssemblyPath);
            string basePath = Uri.UnescapeDataString(uri.Path);
            string finalPath = "C:/Users/PJain/Source/Repos/ArrowEye_TestAutomationSuit/ArrowEye_AutomationSuit/Excel/TestData.xlsx";

            List<TestCaseData> testCaseDataList = ReadExcelData(finalPath, sheetName, rowNumbers);

            if (testCaseDataList != null)
                foreach (TestCaseData testCaseData in testCaseDataList)
                    yield return testCaseData;
        }

        public static List<TestCaseData> ReadExcelData(string path, string sheetName, int[] rowNumbers)
        {
            List<TestCaseData> testCaseData = new List<TestCaseData>();
            XSSFWorkbook xssfWorkBook;
            using (FileStream file = new FileStream(path, FileMode.Open, FileAccess.Read))
            {
                xssfWorkBook = new XSSFWorkbook(file);
            }

            ISheet sheet = xssfWorkBook.GetSheet(sheetName);
            List<string> headers = sheet.GetRow(0).Cells.Select(s => s.StringCellValue).ToList();
            for (int row = 1; row <= sheet.LastRowNum; row++)
            {
                if (rowNumbers.Contains(row))
                {
                    if (sheet.GetRow(row) != null) //null is when the row only contains empty cells 
                    {
                        TestCaseData testCase = new TestCaseData();

                        int cellCount = sheet.GetRow(row).Cells.Count;
                        for (int cell = 0; cell < cellCount; cell++)
                        {
                            string columnName = headers[cell];
                            testCase.GetColumnRowMapping().Add(columnName, sheet.GetRow(row).GetCell(cell).StringCellValue);
                        }
                        testCaseData.Add(testCase);
                    }
                }
            }
            return testCaseData;
        }
    }
}
