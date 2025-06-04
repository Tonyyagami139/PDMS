using ClosedXML.Excel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Common
{
    public partial class FastExcelWorksheet
    {
        public string _sheetName { get; set; }
        private object[,] _cells { get; set; }
        private int _row { get; set; }
        private int _col { get; set; }


        private IXLWorksheet worksheet { get; set; }

        public XLWorkbook workbook { get; set; }

        public FastExcelWorksheet(FastExcelWorkbook wb)
        {
            _row = 1;
            _col = 1;
            worksheet = wb.GetWorksheetList().First();
            _sheetName = worksheet.Name;
        }

        public FastExcelWorksheet(FastExcelWorkbook wb, string sheetName)
        {
            _sheetName = sheetName;
            _row = 1;
            _col = 1;
            worksheet = wb.GetAnotherWorkSheet(sheetName);
            _cells = GetSheetDataArray();
        }

        public FastExcelWorksheet(string excelFileName, string sheetName)
        {
            _sheetName = sheetName;
            _row = 1;
            _col = 1;
            workbook = new XLWorkbook(excelFileName);
            worksheet = workbook.Worksheet(sheetName);
            _cells = GetSheetDataArray();
        }

        public bool ReadBool(int exactRow, int exactCol)
        {
            bool boolValue = false;
            var cell = worksheet.Cell(exactRow, exactCol);
            cell.TryGetValue(out boolValue);
            return boolValue;
        }
        /// <summary>
        /// Read string by next col
        /// </summary>
        /// <param name="parameter"></param>
        /// <param name="startRow"></param>
        /// <param name="startCol"></param>
        /// <returns></returns>
        /// <exception cref="FastExcelException"></exception>
        public string ReadString(string parameter, int startRow, int startCol)
        {
            for (int r = startRow; r <= worksheet.LastRowUsed().RowNumber(); r++)// row start from 1 , need to change to <=  from <
            {
                var cell = worksheet.Cell(r, startCol);
                string para = cell.Value.ToString().ToLower().Trim();
                if (string.IsNullOrWhiteSpace(para))
                {
                    break;
                }
                if (para == parameter.ToLower().Trim())
                {
                    var paraValueCell = worksheet.Cell(r, startCol + 1);
                    return paraValueCell.Value.ToString();
                }
            }
            throw new FastExcelException("Para not found.");
        }

        public double GetCellDoubleValue(int row, int col)
        {
            string str = GetCellValue(row, col);
            return Convert.ToDouble(str);
        }
        public T GetCellValue<T>(int row, int col)
        {
            var cell = worksheet.Cell(row, col);
            if (cell.IsEmpty())
            {
                throw new Exception("The cell is empty.");
                //return default(T);
            }
            return cell.GetValue<T>();
        }
        public int PointToSection(string SectionName, int startRow, int fixedCol)
        {
            if (worksheet == null) throw new FastExcelException($"The general worksheet does not exist.");
            for (int r = startRow; r < worksheet.LastRowUsed().RowNumber(); r++)
            {
                var cell = worksheet.Cell(r, fixedCol);
                if (string.Compare(cell.Value.ToString(), SectionName, true) == 0)
                {
                    return r;
                }
            }
            throw new FastExcelException($"the value {SectionName} was not found in the first column.");
        }
        public int PointToSectionBy1stCol(string SectionName)
        {
            if (worksheet == null) throw new FastExcelException($"The worksheet is not exist.");
            foreach (var row in worksheet.RowsUsed())
            {
                var cell = row.Cell(1);//Only check the first column
                if (string.Compare(cell.Value.ToString(), SectionName, true) == 0)
                {
                    return row.RowNumber();
                }
            }
            throw new FastExcelException($"the value {SectionName} was not found in the first column.");
        }

        public int CountSections_general(string SectionName)//for example workstep section's count
        {
            if (worksheet == null) throw new FastExcelException($"The general worksheet does not exist.");
            int generalRow = PointToSectionBy1stCol("general");
            int Count = 0;
            for (int r = generalRow + 1; r <= worksheet.LastRowUsed().RowNumber(); r++)
            {
                var cell = worksheet.Cell(r, 1);
                if (string.Compare(cell.Value.ToString(), SectionName, true) == 0)
                {
                    Count++;
                }
            }
            return Count;
        }

        public int CountRowBlock(string sectionName, int startRow, int fixedcol)
        {
            int count = 0;
            int row = PointToSection(sectionName, startRow, fixedcol);
            row++;
            for (int r = row; r <= worksheet.LastRowUsed().RowNumber(); r++)
            {
                var cell = worksheet.Cell(row, fixedcol);
                if (string.IsNullOrWhiteSpace(cell.Value.ToString()))
                {
                    break;
                }
                count++;
            }
            return count;
        }
        public int CountRowBlock(int startRow, int fixedcol)
        {
            int count = 0;
            int row = startRow;
            row++;
            for (int r = row; r <= worksheet.LastRowUsed().RowNumber(); r++)
            {
                var cell = worksheet.Cell(row, fixedcol);
                if (string.IsNullOrWhiteSpace(cell.Value.ToString()))
                {
                    break;
                }
                count++;
            }
            return count;
        }

        public object[,] GetSheetDataArray()
        {
            var range = worksheet.RangeUsed();
            var rowCount = range.RowCount();
            var colCount = range.ColumnCount();
            var dataArray = new object[rowCount, colCount];


            return dataArray;
        }

        // 获取指定单元格的值
        public string GetCellValue(int row, int column)
        {
            return worksheet.Cell(row, column).GetValue<string>();
        }

        // 设置指定单元格的值
        public void SetCellValue(int row, int column, string value)
        {
            worksheet.Cell(row, column).Value = value;
        }

        public void SetCellValue(int row, int column, double value)
        {
            worksheet.Cell(row, column).Value = value;
        }

        public Dictionary<string, List<string>> ReadSheetChannels()
        {
            Dictionary<string, List<string>> SheetData = new Dictionary<string, List<string>>(StringComparer.OrdinalIgnoreCase);

            var firstRowUsed = worksheet.FirstRowUsed();

            int col = 1;
            while (true)
            {
                string columnName = worksheet.Cell(3, col).GetString();
                if (string.IsNullOrWhiteSpace(columnName))
                {
                    break;
                }
                SheetData[columnName] = new List<string>();
                int rowNum = 4;
                while (true)
                {
                    string cellValue = worksheet.Cell(rowNum, col).GetString();
                    if (string.IsNullOrWhiteSpace(cellValue))
                    {
                        break;
                    }
                    SheetData[columnName].Add(cellValue);
                    rowNum++;
                }
                col++;
            }
            return SheetData;
        }

        public Dictionary<string, List<string>> ReadColumnContent(int row, int col)
        {
            Dictionary<string, List<string>> ColumnData = new Dictionary<string, List<string>>(StringComparer.OrdinalIgnoreCase);
            string header = GetCellValue(row, col);
            row++;
            List<string> content = new List<string>();
            while (true)
            {
                string colValue = GetCellValue(row, col);
                if (string.IsNullOrWhiteSpace(colValue))
                {
                    break;
                }
                content.Add(colValue);
                row++;
            }
            ColumnData.Add(header, content);
            return ColumnData;
        }
        public List<string> ReadSingleColumnContent(int row, int col)
        {
            List<string> ColumnData = new List<string>();

            while (true)
            {
                string colValue = GetCellValue(row, col);
                if (string.IsNullOrWhiteSpace(colValue))
                {
                    break;
                }
                ColumnData.Add(colValue);
                row++;
            }
            return ColumnData;
        }
        public Dictionary<string, List<double>> ReadDoubleColumnContent(int row, int col)
        {
            Dictionary<string, List<double>> ColumnData = new Dictionary<string, List<double>>(StringComparer.OrdinalIgnoreCase);
            string header = GetCellValue(row, col);
            row++;
            List<double> content = new List<double>();
            while (true)
            {
                string colString = GetCellValue(row, col);
                if (string.IsNullOrWhiteSpace(colString)) { break; }
                double colValue;
                if (!double.TryParse(colString, out colValue)) { break; }
                content.Add(colValue);
                row++;
            }
            ColumnData.Add(header, content);
            return ColumnData;
        }
        public List<string> ReadColumnContentSkipHeader(int row, int col)
        {
            //string header = GetCellValue(row, col);
            row++;//Skip header
            List<string> content = new List<string>();
            while (true)
            {
                string colValue = GetCellValue(row, col);
                if (string.IsNullOrWhiteSpace(colValue))
                {
                    break;
                }
                content.Add(colValue);
                row++;
            }
            return content;
        }
        public int GetColumnNumber(string sectionName, int fixedRow, int startCol)
        {
            var searchRow = worksheet.Row(fixedRow);
            foreach (var cell in searchRow.Cells(startCol, searchRow.LastCellUsed().Address.ColumnNumber))
            {
                if (cell.GetString().Equals(sectionName, StringComparison.OrdinalIgnoreCase))
                {
                    return cell.Address.ColumnNumber;
                }
            }
            return -1;
        }

        public FastExcelWorksheet ReFresh(string filePath)
        {
            string sheetName = this._sheetName;
            this.Save(filePath);
            Thread.Sleep(100);
            return new FastExcelWorksheet(filePath, sheetName);
        }
        public int GetNextUnusedCol(int startRow, int startCol)
        {
            int currentRow = startRow;
            int currentCol = startCol;
            while (!worksheet.Cell(currentRow, currentCol).IsEmpty())
            {
                currentCol++;
            }
            return currentCol;
        }


        // 保存工作簿
        public void Save(string filePath)
        {
            workbook.SaveAs(filePath);
        }
    }
}


