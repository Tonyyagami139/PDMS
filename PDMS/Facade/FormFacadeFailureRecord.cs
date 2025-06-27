using Common;
using Dal;
using DocumentFormat.OpenXml.Spreadsheet;
using Syncfusion.XlsIO;
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
            string str = DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss") + " " + CnStr;
            // 先把插入点移动到开头
            tb_log.SelectionStart = 0;
            tb_log.SelectionLength = 0;

            // 插入新日志并换行
            tb_log.SelectedText = str + Environment.NewLine;
        }

        public string CopyConfigs2Local()
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
            return configurationFileName;
        }

        public List<FailureRecord_ProductInfo> ReadProductInfo(string path)
        {
            if (path == null) throw new ArgumentNullException(nameof(path));

            var segments = path.Split(new[] { '/' }, StringSplitOptions.None);

            string first = segments.Length > 0 ? segments[0] : string.Empty;
            string last = segments.Length > 0 ? segments[segments.Length - 1] : string.Empty;
            string middle = segments.Length > 2
                ? string.Join("/", segments.Skip(1).Take(segments.Length - 2))
                : string.Empty;

            return new List<FailureRecord_ProductInfo>
           {
               new FailureRecord_ProductInfo
               {
                   ProductFamily = first,
                   ProductName = middle,
                   ProductType = last
               }
           };
        }

        public List<string> GetFailureModes(string filePath)
        {

            List<string> firstColumn = new List<string>();
            using (ExcelEngine excelEngine = new ExcelEngine())
            {
                IApplication application = excelEngine.Excel;
                application.DefaultVersion = ExcelVersion.Xlsx;

                using (FileStream fs = new FileStream(filePath, FileMode.Open, FileAccess.Read))
                {
                    IWorkbook workbook = application.Workbooks.Open(fs);
                    IWorksheet sheet = workbook.Worksheets["FailureModes"]; // 读取第一个 sheet

                    int rowCount = sheet.Rows.Length; // 或用 sheet.UsedRange.LastRow

                    for (int i = 2; i <= sheet.UsedRange.LastRow; i++) // skip header
                    {
                        string value = sheet.Range[i, 1].Value; // A列就是第1列
                        firstColumn.Add(value);
                    }
                }
            }
            return firstColumn;
        }


        public bool IsFailureModeValidate(string failureMode)
        {
            var failures = GetFailureModes(configurationFileName);
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


        

        public byte[] GetSyncFusionExcelReportMemory(List<FailureRecord> records)
        {
            using (ExcelEngine excelEngine = new ExcelEngine())
            {
                IApplication application = excelEngine.Excel;
                application.DefaultVersion = ExcelVersion.Xlsx;

                // 2. 新建工作簿
                IWorkbook workbook = application.Workbooks.Create(1);
                IWorksheet sheet = workbook.Worksheets[0];
                sheet.Name = "FailureRecords";

                // 3. 写入数据
                sheet.Range[1, 1].Value = "Id";
                sheet.Range[1, 2].Value = "SerialNumber";
                sheet.Range[1, 3].Value = "ProductFamily";
                sheet.Range[1, 4].Value = "ProductName";
                sheet.Range[1, 5].Value = "ProductType";
                sheet.Range[1, 6].Value = "WorkStepProcessName";
                sheet.Range[1, 7].Value = "FailureMode";
                sheet.Range[1, 8].Value = "Status";
                sheet.Range[1, 9].Value = "CreateUserName";
                sheet.Range[1, 10].Value = "CreateTime";
                sheet.Range[1, 11].Value = "ModifyUserName";
                sheet.Range[1, 12].Value = "ModifyTime";
                sheet.Range[1, 13].Value = "Comment";

                // 添加数据
                for (int i = 0; i < records.Count; i++)
                {
                    var r = records[i];
                    int row = i + 2; // 数据从第二行开始
                    sheet.Range[row, 1].Value = r.Id.ToString();
                    sheet.Range[row, 2].Value = r.SerialNumber;
                    sheet.Range[row, 3].Value = r.ProductFamily;
                    sheet.Range[row, 4].Value = r.ProductName;
                    sheet.Range[row, 5].Value = r.ProductType;
                    sheet.Range[row, 6].Value = r.WorkStepProcessName;
                    sheet.Range[row, 7].Value = r.FailureMode;
                    // Status设置值和字体颜色
                    var statusCell = sheet.Range[row, 8];
                    statusCell.Value = r.Status;
                    if (string.Equals(r.Status, "", StringComparison.OrdinalIgnoreCase))
                    {
                        //default 
                    }
                    else if (string.Equals(r.Status, "PASS", StringComparison.OrdinalIgnoreCase))
                    {
                        statusCell.CellStyle.Font.Color = ExcelKnownColors.Green;
                    }
                    else if (string.Equals(r.Status, "FAIL", StringComparison.OrdinalIgnoreCase))
                    {
                        statusCell.CellStyle.Font.Color = ExcelKnownColors.Red;
                    }
                    else if (string.Equals(r.Status, "QUARANTINE", StringComparison.OrdinalIgnoreCase))
                    {
                        statusCell.CellStyle.Font.Color = ExcelKnownColors.Violet;
                    }
                    else if (string.Equals(r.Status, "REWORK", StringComparison.OrdinalIgnoreCase))
                    {
                        statusCell.CellStyle.Font.Color = ExcelKnownColors.Blue;
                    }
                    else
                    {
                        statusCell.CellStyle.Font.Color = ExcelKnownColors.Orange;
                    }
                    sheet.Range[row, 9].Value = r.CreateUserName;
                    sheet.Range[row, 10].Value = r.CreateTime.ToString("yyyy-MM-dd HH:mm:ss") ;
                    sheet.Range[row, 11].Value = r.ModifyUserName;
                    sheet.Range[row, 12].Value = r.ModifyTime.ToString("yyyy-MM-dd HH:mm:ss");
                    sheet.Range[row, 13].Value = r.Comment;
                }

                sheet.UsedRange.AutofitColumns();

                byte[] data;
                using (MemoryStream memoryStream = new MemoryStream())
                {
                    workbook.SaveAs(memoryStream);
                    data = memoryStream.ToArray();
                }
                return data;
            }

            
        }
    }

}
