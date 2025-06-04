using ClosedXML.Excel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Common
{
    public class FastExcelWorkbook
    {
        private XLWorkbook workbook { get; set; }
        private IXLWorksheet worksheet { get; set; }

        public static void CloneExcelFile(string sourceFilePath, string targetFilePath)
        {
            try
            {
                // Load the source workbook
                using (var sourceWorkbook = new XLWorkbook(sourceFilePath))
                {
                    // Create a new workbook for the target file
                    using (var targetWorkbook = new XLWorkbook())
                    {
                        // Loop through each worksheet in the source workbook
                        foreach (var worksheet in sourceWorkbook.Worksheets)
                        {
                            // Add the worksheet to the target workbook
                            worksheet.CopyTo(targetWorkbook, worksheet.Name);
                        }

                        // Save the target workbook to the specified file path
                        targetWorkbook.SaveAs(targetFilePath);
                    }
                }
            }
            catch (Exception ex)
            {
                throw new FastExcelException($"An Error occurred while cloning the Excel:{ex.Message}");
            }
        }

        public FastExcelWorkbook(string filePath, string sheetName)
        {
            workbook = new XLWorkbook(filePath);
            if (workbook.Worksheets.Contains(sheetName))
            {
                worksheet = workbook.Worksheet(sheetName);
            }
            else
            {
                throw new FastExcelException("Error:excel sheet :" + sheetName + " is not found.");
            }
        }
        public FastExcelWorkbook(string filePath)
        {
            workbook = new XLWorkbook(filePath);
        }


        public IXLWorksheet GetCurrentWorksheet()
        {
            return worksheet;
        }

        public IXLWorksheet GetAnotherWorkSheet(string sheetName)
        {
            if (workbook.Worksheets.Contains(sheetName))
            {
                return workbook.Worksheet(sheetName);
            }
            else
            {
                throw new FastExcelException("worksheet" + sheetName + " is not exist.");
            }
        }

        public void Save(string filePath)
        {
            workbook.SaveAs(filePath);
        }

        public string GetWorksheetCellValue(int row, int column)
        {
            return worksheet.Cell(row, column).GetValue<string>();
        }

        public void SetWorksheetCellValue(int row, int column, string value)
        {
            worksheet.Cell(row, column).Value = value;
        }
        public List<IXLWorksheet> GetWorksheetList()
        {
            return workbook.Worksheets.ToList();
        }
        private object[,] GetSheetDataArray()
        {
            var range = worksheet.RangeUsed();
            var rowCount = range.RowCount();
            var colCount = range.ColumnCount();
            var dataArray = new object[rowCount, colCount];
            for (var row = 0; row < rowCount; row++)
            {
                for (var col = 0; col < colCount; col++)
                {
                    dataArray[row, col] = range.Cell(row + 1, col + 1).Value;
                }
            }
            return dataArray;
        }
        ~FastExcelWorkbook()
        {
            if ((workbook)!=null)
            {
                workbook.Dispose();
            }
        }


    }
}
