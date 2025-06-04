using ClosedXML.Excel;
using Common;
using Dal;
using DocumentFormat.OpenXml.Spreadsheet;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace PDMS.Facade
{
    public partial class FormFacade
    {
        public ListSortDirection direction { get; set; } = ListSortDirection.Ascending;
        public string LocalPDMSFolder { get; set; } = "C:\\ProgramData\\zTx\\PDMS";
        public string configurationFileName { get; set; }
        public void FailureRecordShowLog(TextBox tb_log, string CnStr)
        {
            tb_log.AppendText(CnStr);
            tb_log.AppendText("\r");
            tb_log.AppendText("\n");
        }

        public void CopyConfigs2Local()
        {
            string filename = "\\\\192.168.31.223\\TestingDataRoot\\PDMS\\FailureRecord\\FailureRecord_Configs.xlsx";
            string guid = Guid.NewGuid().ToString();
            configurationFileName = $"{LocalPDMSFolder}\\{guid}_FailureRecord_Configs.xlsx";
            if (Directory.Exists(LocalPDMSFolder))
            {
                var files = Directory.GetFiles(LocalPDMSFolder);
                try
                {
                    foreach (var f in files)
                    {
                        if (f.EndsWith("_FailureRecord_Configs.xlsx"))
                        {
                            File.Delete(f);
                        }
                    }
                }
                catch
                { 
                  //ignore error
                }
            }
            if (!Directory.Exists(LocalPDMSFolder))
            {
                Directory.CreateDirectory(LocalPDMSFolder);
            }
            if (File.Exists(configurationFileName))
            {
                File.Delete(configurationFileName);
            }
            File.Copy(filename, configurationFileName, true);
        }

        public List<FailureRecord_ProductInfo> ReadProductInfo()
        {
            FastExcelWorkbook workbook = new FastExcelWorkbook(configurationFileName);
            FastExcelWorksheet worksheet = new FastExcelWorksheet(workbook, "ProductInfo");
            List<FailureRecord_ProductInfo> list = new List<FailureRecord_ProductInfo>();
            if (worksheet == null)
            {
                MessageBox.Show("配置文件中未找到ProductInfo工作表，请检查配置文件。");
                throw new FastExcelException("配置文件中未找到ProductInfo工作表，请检查配置文件。");
            }

            var row = 2;//skip header
            while (true)
            {

                var cellValue = worksheet.GetCellValue(row, 1);
                if (string.IsNullOrEmpty(cellValue))
                {
                    break; // 如果第一列的值为空，结束读取
                }
                var productInfo = new FailureRecord_ProductInfo
                {
                    ProductFamily = worksheet.GetCellValue(row, 1) ?? string.Empty,
                    ProductName = worksheet.GetCellValue(row, 2) ?? string.Empty,
                    ProductType = worksheet.GetCellValue(row, 3) ?? string.Empty
                };
                list.Add(productInfo);
                row++;
            }
            return list;

        }

        public List<string> GetFailureModes()
        {
            FastExcelWorkbook workbook = new FastExcelWorkbook(configurationFileName);
            FastExcelWorksheet worksheet = new FastExcelWorksheet(workbook, "FailureModes");
            List<string> list = new List<string>();
            var row = 2;//skip header
            while (true)
            {

                var cellValue = worksheet.GetCellValue(row, 1);
                if (string.IsNullOrEmpty(cellValue))
                {
                    break; // 如果第一列的值为空，结束读取
                }

                list.Add(cellValue);
                row++;
            }
            return list;
        }
        public List<string> GetProcesses()
        {

            FastExcelWorkbook workbook = new FastExcelWorkbook(configurationFileName);
            FastExcelWorksheet worksheet = new FastExcelWorksheet(workbook, "Worksteps");
            List<string> list = new List<string>();
            var row = 2;//skip header
            while (true)
            {

                var cellValue = worksheet.GetCellValue(row, 1);
                if (string.IsNullOrEmpty(cellValue))
                {
                    break; // 如果第一列的值为空，结束读取
                }

                list.Add(cellValue);
                row++;

            }
            return list;
        }

        public bool IsFailureModeValidate(string failureMode)
        {
            var failures = GetFailureModes();
            return failures.Contains(failureMode);
        }

        public StringBuilder GetFailureRecordsStringBuilder(List<FailureRecord> failures)
        {
            StringBuilder sb = new StringBuilder();
            sb.AppendLine("SerialNumber,ProductFamily,ProductName,WorkStepProcessName,FailureMode,Status,CreateUserName,CreateTime,ModifyUserName,ModifyTime,Comment");
            foreach (FailureRecord failureRecord in failures)
            {
                sb.Append(failureRecord.SerialNumber);
                sb.Append(",");
                sb.Append(failureRecord.ProductFamily);
                sb.Append(",");
                sb.Append(failureRecord.ProductName);
                sb.Append(",");
                sb.Append(failureRecord.WorkStepProcessName);
                sb.Append(",");
                sb.Append(failureRecord.FailureMode);
                sb.Append(",");
                sb.Append(failureRecord.Status);
                sb.Append(",");
                sb.Append(failureRecord.CreateUserName);
                sb.Append(",");
                sb.Append(failureRecord.CreateTime);
                sb.Append(",");
                sb.Append(failureRecord.ModifyUserName);
                sb.Append(",");
                sb.Append(failureRecord.ModifyTime);
                sb.Append(",");
                sb.Append(failureRecord.Comment);
                sb.AppendLine();
            }
            return sb;
        }


        public byte[] GetExcelReportMemory(List<FailureRecord> records)
        {
            using (XLWorkbook workbook = new XLWorkbook())
            {
                var ws = workbook.AddWorksheet("FailureRecords");

                // 添加标题
                ws.Cell(1, 1).Value = "Id";
                ws.Cell(1, 2).Value = "SerialNumber";
                ws.Cell(1, 3).Value = "ProductFamily";
                ws.Cell(1, 4).Value = "ProductName";
                ws.Cell(1, 5).Value = "ProductType";
                ws.Cell(1, 6).Value = "WorkStepProcessName";
                ws.Cell(1, 7).Value = "FailureMode";
                ws.Cell(1, 8).Value = "Status";
                ws.Cell(1, 9).Value = "CreateUserName";
                ws.Cell(1, 10).Value = "CreateTime";
                ws.Cell(1, 11).Value = "ModifyUserName";
                ws.Cell(1, 12).Value = "ModifyTime";
                ws.Cell(1, 13).Value = "Comment";

                // 添加数据
                for (int i = 0; i < records.Count; i++)
                {
                    var r = records[i];
                    int row = i + 2; // 数据从第二行开始
                    ws.Cell(row, 1).Value = r.Id;
                    ws.Cell(row, 2).Value = r.SerialNumber;
                    ws.Cell(row, 3).Value = r.ProductFamily;
                    ws.Cell(row, 4).Value = r.ProductName;
                    ws.Cell(row, 5).Value = r.ProductType;
                    ws.Cell(row, 6).Value = r.WorkStepProcessName;
                    ws.Cell(row, 7).Value = r.FailureMode;
                    // Status设置值和字体颜色
                    var statusCell = ws.Cell(row, 8);
                    statusCell.Value = r.Status;
                    if (string.Equals(r.Status, "", StringComparison.OrdinalIgnoreCase))
                    {
                        //default 
                    }
                    else if (string.Equals(r.Status, "PASS", StringComparison.OrdinalIgnoreCase))
                    {
                        statusCell.Style.Font.FontColor = XLColor.Green;
                    }
                    else if (string.Equals(r.Status, "FAIL", StringComparison.OrdinalIgnoreCase))
                    {
                        statusCell.Style.Font.FontColor = XLColor.Red;
                    }
                    else
                    {
                        statusCell.Style.Font.FontColor = XLColor.Orange; // 黄色字体
                    }
                    ws.Cell(row, 9).Value = r.CreateUserName;
                    ws.Cell(row, 10).Value = r.CreateTime;
                    ws.Cell(row, 11).Value = r.ModifyUserName;
                    ws.Cell(row, 12).Value = r.ModifyTime;
                    ws.Cell(row, 13).Value = r.Comment;
                }

                // 可选：自动调整列宽
                ws.Columns().AdjustToContents();

                // 保存 Excel 文件到内存流
                using (var memoryStream = new MemoryStream())
                {
                    workbook.SaveAs(memoryStream);
                    return memoryStream.ToArray();
                }
            }
        }
    }

}
