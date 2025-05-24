using ClosedXML.Excel;
using Common;
using Dal;
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
        public void FailureRecordShowLog(TextBox tb_log, string CnStr)
        {
            tb_log.AppendText(CnStr);
            tb_log.AppendText("\r");
            tb_log.AppendText("\n");
        }

        public List<string> GetFailureModes()
        {
            return File.ReadAllLines(Global.FailureModesFileName).ToList();
        }
        public List<string> GetProcesses()
        {
            return File.ReadLines(Global.ProcessesFileName).ToList();
        }

        public List<string> GetProductFamilies()
        {
            var dal = new FailureRecordDal(Global.DbSettingTym);
            return dal.GetProductFamilies();

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
                ws.Cell(1, 5).Value = "WorkStepProcessName";
                ws.Cell(1, 6).Value = "FailureMode";
                ws.Cell(1, 7).Value = "Status";
                ws.Cell(1, 8).Value = "CreateUserName";
                ws.Cell(1, 9).Value = "CreateTime";
                ws.Cell(1, 10).Value = "ModifyUserName";
                ws.Cell(1, 11).Value = "ModifyTime";
                ws.Cell(1, 12).Value = "Comment";

                // 添加数据
                for (int i = 0; i < records.Count; i++)
                {
                    var r = records[i];
                    int row = i + 2; // 数据从第二行开始
                    ws.Cell(row, 1).Value = r.Id;
                    ws.Cell(row, 2).Value = r.SerialNumber;
                    ws.Cell(row, 3).Value = r.ProductFamily;
                    ws.Cell(row, 4).Value = r.ProductName;
                    ws.Cell(row, 5).Value = r.WorkStepProcessName;
                    ws.Cell(row, 6).Value = r.FailureMode;
                    // Status设置值和字体颜色
                    var statusCell = ws.Cell(row, 7);
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
                    ws.Cell(row, 8).Value = r.CreateUserName;
                    ws.Cell(row, 9).Value = r.CreateTime;
                    ws.Cell(row, 10).Value = r.ModifyUserName;
                    ws.Cell(row, 11).Value = r.ModifyTime;
                    ws.Cell(row, 12).Value = r.Comment;
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
