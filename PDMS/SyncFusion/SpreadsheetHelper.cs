using Syncfusion.Windows.Forms.Spreadsheet;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace PDMS.SyncFusion
{
    public class SpreadsheetHelper
    {
        public Spreadsheet spreadsheet { get; set; }
        public SpreadsheetHelper(Spreadsheet in_spreadsheet) 
        { 
            this.spreadsheet = in_spreadsheet;
        }
        // 你需要在用 spreadsheet1 前，确保已打开文件并加载完成
        public List<SpreadsheetModel> ReadSheetToList()
        {
            SendKeysEnter();
            var result = new List<SpreadsheetModel>();
            var ws = spreadsheet.ActiveSheet;
            
            int rowCount = ws.UsedRange.LastRow;
            int colCount = ws.UsedRange.LastColumn;

            // 修复索引问题，确保正确访问单元格值  
            string workStep = ws.Range[1, 1].Value?.ToString() ?? "";

            // 读取第一行第2列及以后为 StringData 的 key  
            List<string> keys = new List<string>();
            for (int col = 2; col <= colCount; col++)
            {
                string key = ws.Range[1, col].Value ?? "";
                keys.Add(key);
            }

            // 从第二行开始，每行生成一个 SpreadsheetModel  
            for (int row = 2; row <= rowCount; row++)
            {
                // 判断整行是否全空（从第1列起）  
                bool rowIsEmpty = true;
                for (int col = 1; col <= colCount; col++)
                {
                    var cell = ws.Range[row, col].Value;
                    if (!string.IsNullOrWhiteSpace(cell))
                    {
                        rowIsEmpty = false;
                        break;
                    }
                }
                if (rowIsEmpty) continue;

                var model = new SpreadsheetModel();
                model.WorkStep = workStep;
                model.Serial = ws.Range[row, 1].Value ?? "";
                model.StringData = new Dictionary<string, List<string>>();

                // 遍历每个数据列  
                for (int col = 2; col <= colCount; col++)
                {
                    string key = keys[col - 2]; // keys索引比col小2  
                    string value = ws.Range[row, col].Value ?? "";

                    if (model.StringData.ContainsKey(key))
                        model.StringData[key].Add(value);
                    else
                        model.StringData[key] = new List<string> { value };
                }

                result.Add(model);
            }

            return result;
        }

        private void SendKeysEnter()
        {
            SendKeys.Send("{ENTER}"); //退出编辑模式
            Application.DoEvents();
        }
        public bool ValidateSpreadsheetModels(List<SpreadsheetModel> list, out string error)
        {
            var errors = new List<string>();
            if (list.Count < 1)
            {
                errors.Add($"无数据。");
            }

            // 1. 校验 Serial 是否有重复
            var duplicateSerials = list
                .GroupBy(m => m.Serial)
                .Where(g => g.Count() > 1)
                .Select(g => g.Key)
                .ToList();

            if (duplicateSerials.Any())
            {
                errors.Add($"Serial 重复: {string.Join(", ", duplicateSerials)}");
            }

            // 2. 校验 StringData 中是否有 count > 1 的值
            foreach (var model in list)
            {
                //这里添加对workstep的校验，不能为空
                if (string.IsNullOrWhiteSpace(model.WorkStep))
                {
                    errors.Add($"Serial: {model.Serial} 的 WorkStep 不能为空");
                }
                foreach (var kv in model.StringData ?? new Dictionary<string, List<string>>())
                {
                    if (kv.Value != null && kv.Value.Count > 1)
                    {
                        errors.Add($"Serial: {model.Serial} 的 Item {kv.Key} 有 {kv.Value.Count} 个值");
                    }
                }
            }

            // 组装错误信息
            if (errors.Count > 0)
            {
                //error = string.Join(Environment.NewLine, errors);
                error = errors.First();
                return false;
            }
            else
            {
                error = "";
                return true;
            }
        }

        public void Open(string path)
        {
            spreadsheet.Open(path);
        }

        public void Save()
        {
            string path = "C:\\Users\\tianx\\Desktop\\temp\\新建文件夹\\123.xlsx";
            spreadsheet.SaveAs(path);
        }

        public void Dispose()
        {
            spreadsheet.Dispose();
        }
    }
}
