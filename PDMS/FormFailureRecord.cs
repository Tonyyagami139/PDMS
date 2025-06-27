using Common;
using Dal;
using DocumentFormat.OpenXml.Presentation;
using PDMS.Facade;
using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Diagnostics;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Net.Mail;
using System.Reflection;
using System.Security.Principal;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;
using static System.Windows.Forms.VisualStyles.VisualStyleElement;

namespace PDMS
{
    public partial class FormFailureRecord : Form
    {
        private FailureRecordDal dal { get; set; } = new FailureRecordDal(Global.DbSettingSqlserver);
        private FailureRecordDal daltym { get; set; } = new FailureRecordDal(Global.DbSettingTym);
        public List<FailureRecord> FailureRecordList { get; set; }
        //Current Infos
        public string CurrentStatus { get; set; }
        public string CurrentPictureFileName { get; set; }
        public FailureRecord SelectedFailureRecord { get; set; }

        public List<FailureRecord> FilteredRecords { get; set; }

        //Lists for combobox
        public List<string> FailureModeList { get; set; }
        public List<string> FilteredFailureModes { get; set; }
        public List<string> ProcessesList { get; set; }

        public Dictionary<string,string> ProcessesMap { get; set; }
        public List<FailureRecord_ProductInfo> ProductInfoList { get; set; }


        public FormFacade facade { get; set; }
        public FormFailureRecord()
        {
            InitializeComponent();
        }
        private void FormFailureRecord_Load(object sender, EventArgs e)
        {
            FormInitial();
            ResponseDataGridViewHeaderSearch();
        }

        private void DebounceTimer_Tick(object sender, EventArgs e)
        {
            // 只有停顿超过 timerDelay 才来到这里，认定“输入完成”
            OnSnTextInputCompleted(tb_FRserialNumber.Text);

        }
       
        /// <summary>
        /// serial number 输入完成后的逻辑
        /// </summary>
        /// <param name="text"></param>
        private void OnSnTextInputCompleted(object text)
        {
            Application.DoEvents();
            //..todo: 处理输入完成后的逻辑
            string input = text.ToString().Trim();

            if (string.IsNullOrEmpty(input))
            {
                // 清空相关字段
                tb_productFamily.Text = "";
                tb_producType.Text = "";
                tb_productName.Text = "";
                return;
            }
            UpdateProcesses(input);

            UpdateProductInfo(input);

            bt_FRuploadPicture.Focus();

        }

        private FailureRecord_ProductInfo UpdateProductInfo(string sn,bool updateUI=true)
        {
            #region update productInfo
            var treePath = daltym.GetFullTreePathBySn(sn);
            ProductInfoList = facade.ReadProductInfo(treePath);
            // 筛选ProductInfoList，找出ProductType以输入内容开头的项
            var matched = ProductInfoList
                .Where(p => !string.IsNullOrEmpty(p.ProductType) && sn.StartsWith(p.ProductType, StringComparison.OrdinalIgnoreCase))
                .ToList();
            if (updateUI)
            {
                if (matched.Count == 1)
                {
                    // 只匹配到一个，自动填充
                    tb_productFamily.Text = matched[0].ProductFamily ?? "";
                    tb_producType.Text = matched[0].ProductType ?? "";
                    tb_productName.Text = matched[0].ProductName ?? "";
                }
                else if (matched.Count < 1)
                {
                    // 没有匹配，清空填充
                    tb_productFamily.Text = "";
                    tb_producType.Text = "";
                    tb_productName.Text = "";
                }
                else
                {
                    // 匹配到多个，自动填充最后一个
                    tb_productFamily.Text = matched.Last().ProductFamily ?? "";
                    tb_producType.Text = matched.Last().ProductType ?? "";
                    tb_productName.Text = matched.Last().ProductName ?? "";
                }
            }
            return matched.LastOrDefault();
            #endregion
        }
        private void UpdateProcesses(string sn)
        {
            #region update processes
            // 1. 取出原始数据（方便后面算最大长度）
            var rawList = daltym.GetProcessesBySn(sn).ToList();
            if (rawList.Count < 1)
            {
                facade.FailureRecordShowLog(tb_FRlog, $"错误:序列号{sn}没有查到任何流程");
                return;
            }
            // 2. 计算 Key 的最大长度
            int maxKeyLen = rawList.Max(p => p.Key.Length);

            // 3. 生成等长的展示字符串
            var displayList = rawList
                .Select(p =>
                    p.Key.PadRight(maxKeyLen)
                    + " | "
                    + p.Name
                )
                .ToList();
            // 4. 将 Key 和 Name 映射到字典中
            ProcessesMap = rawList.ToDictionary(p => p.Key, p => p.Name);

            if (displayList != null)
            {
                ProcessesList = displayList;
                cb_FRworkStep.DataSource = null; ;
                string oldText = cb_FRworkStep.Text;
                int oldSelStart = cb_FRworkStep.SelectionStart;
                cb_FRworkStep.DataSource = null;//ProcessesList;
                cb_FRworkStep.Items.Clear();
                cb_FRworkStep.Items.AddRange(ProcessesList.ToArray());
                cb_FRworkStep.SelectedIndex = -1;
                cb_FRworkStep.Text = oldText;
                cb_FRworkStep.SelectionStart = oldSelStart;
            }
            #endregion
        }
        private void ResponseDataGridViewHeaderSearch()
        {
            System.Windows.Forms.TextBox filterBox = new System.Windows.Forms.TextBox();
            filterBox.Visible = false;
            filterBox.Width = 120;
            // 失去焦点自动隐藏
            filterBox.Leave += (s, e) => filterBox.Visible = false;
            // 按回车时应用筛选
            filterBox.TextChanged += (s, e) =>
            {
                if (!string.IsNullOrEmpty(filterBox.Text.Trim()))
                {
                    ApplyFilter(dataGridView_failureRecord, filterBox.Tag as int?, filterBox.Text);
                    //filterBox.Visible = false;
                }
            };
            // 将TextBox控件添加到DataGridView控件层级中
            dataGridView_failureRecord.Controls.Add(filterBox);
            // 3. 列头点击事件
            dataGridView_failureRecord.CellMouseClick += (s, e) =>
            {
                // 判断是否点击的是列头单元格
                if (e.RowIndex == -1 && e.ColumnIndex >= 0)
                {
                    // 获取当前列头单元格的屏幕区域
                    var headerRect = dataGridView_failureRecord.GetCellDisplayRectangle(e.ColumnIndex, -1, true);
                    // 计算放大镜图标实际的显示区域（和上面CellPainting一致）
                    var iconRect = new Rectangle(headerRect.Right - 20, headerRect.Top + 5, 16, 16);
                    // 当前鼠标点击位置（DataGridView控件坐标系下）
                    var mousePos = dataGridView_failureRecord.PointToClient(Cursor.Position);
                    // 判断是否点击了图标区域
                    if (iconRect.Contains(mousePos))
                    {
                        // 记录当前要筛选的列索引
                        filterBox.Tag = e.ColumnIndex;
                        //filterBox.Location = new Point(headerRect.Right - filterBox.Width, headerRect.Bottom);
                        filterBox.Location = new Point(headerRect.Left, headerRect.Bottom);
                        filterBox.Text = "";
                        filterBox.Visible = true;
                        filterBox.Focus();
                    }
                }
            };

        }
        private void ApplyFilter(DataGridView dgv, int? colIdx, string filterText)
        {
            // 获取要筛选的列名
            var colName = dgv.Columns[colIdx.Value].DataPropertyName;

            // 如果未输入内容则清除筛选
            if (string.IsNullOrWhiteSpace(filterText))
            {

            }
            else
            {
                //var currentList=FilteredRecords==null?FailureRecordList:FilteredRecords.ToList();
                FilteredRecords = FailureRecordList.Where(fr =>
                    fr.GetType().GetProperty(colName).GetValue(fr, null).ToString().IndexOf(filterText, StringComparison.OrdinalIgnoreCase) >= 0
                ).ToList();
                dataGridView_failureRecord.DataSource = null;
                dataGridView_failureRecord.DataSource = FilteredRecords;
            }
        }
        private void FormInitial()
        {
            var version = Global.GetAppVersion();
            this.Text = $"FailureRecord - {version.ToString()}";
            //增加列筛选图标
            dataGridView_failureRecord.CellPainting += (s, e) =>
            {
                if (e.RowIndex == -1 && e.ColumnIndex >= 0) // 只处理Header
                {
                    e.PaintBackground(e.ClipBounds, true);
                    e.PaintContent(e.ClipBounds);

                    // 画小图标（你也可以用e.Graphics.DrawImage画图片）
                    var iconRect = new Rectangle(e.CellBounds.Right - 20, e.CellBounds.Top + 5, 16, 16);

                    TextRenderer.DrawText(e.Graphics, "🔍", e.CellStyle.Font, iconRect, Color.Gray);

                    e.Handled = true;
                }
            };


            facade = new FormFacade(this);
            try
            {
                var f= facade.CopyConfigs2Local();
                //ProductInfoList = facade.ReadProductInfo();
                FailureModeList = facade.GetFailureModes(f);
                //ProcessesList = facade.GetProcesses();
            }
            catch
            {
                MessageBox.Show("连接服务器\\192.168.31.223失败。");
                this.Close();
            }
            FailureRecordList = new List<FailureRecord>();
            dataGridView_failureRecord.AutoGenerateColumns = false;
            radioButton_FRpass.CheckedChanged += RadioButton_CheckedChanged;
            radioButton_FRpass.ForeColor = Color.Green;
            radioButton_FRfail.CheckedChanged += RadioButton_CheckedChanged;
            radioButton_FRfail.ForeColor= Color.Red;
            radioButton_FRhold.CheckedChanged += RadioButton_CheckedChanged;
            radioButton_FRhold.ForeColor = Color.Orange;
            radioButton_FRrework.CheckedChanged += RadioButton_CheckedChanged;
            radioButton_FRrework.ForeColor = Color.Blue;
            radioButton_FRquarantine.CheckedChanged += RadioButton_CheckedChanged;
            radioButton_FRquarantine.ForeColor = Color.Purple;
            CurrentStatus = string.Empty;
            //status access level should be [1,3]
            if (Global.UserLevel < 0 || Global.UserLevel > 3)
            {
                radioButton_FRpass.Enabled = false;
                radioButton_FRfail.Enabled = false;
                radioButton_FRquarantine.Enabled = false;
                radioButton_FRrework.Enabled = false;
                bt_FRdelete.Enabled = false;
            }

            Image image = Image.FromFile(Global.LogoPicture);
            pictureBox_FailureRecord.Image = image;

            ClearField();

            FailureRecordList = dal.GetFailureRecords();
            dataGridView_failureRecord.DataSource = FailureRecordList;
        }
        private void ClearField()
        {
            tb_FRserialNumber.Text = "";
            tb_productFamily.Text = "";
            tb_producType.Text = "";
            tb_productName.Text = "";
            tb_FRcomment.Text = "";

            radioButton_FRpass.Checked = false;
            radioButton_FRfail.Checked = false;
            radioButton_FRhold.Checked = false;
            radioButton_FRrework.Checked = false;
            radioButton_FRquarantine.Checked = false;

            cb_FRfailureMode.DataSource = null;
            cb_FRworkStep.DataSource = null;
            cb_FRfailureMode.Items.Clear();
            //cb_FRworkStep.Items.Clear();
            cb_FRfailureMode.DataSource = FailureModeList;
            cb_FRworkStep.DataSource = null;// ProcessesList;
            cb_FRfailureMode.Text = "";
            cb_FRworkStep.Text = "";

            SelectedFailureRecord = null;
            Image image = Image.FromFile(Global.LogoPicture);
            pictureBox_FailureRecord.Image = image;
            bt_FRuploadPicture.BackColor = Color.White;

            radioButton_FRhold.Checked = true;

        }
        private void RadioButton_CheckedChanged(object sender, EventArgs e)
        {
            System.Windows.Forms.RadioButton rb = sender as System.Windows.Forms.RadioButton;
            if (rb != null && rb.Checked)
            {
                CurrentStatus = rb.Text;
            }
        }
        private void bt_FRuploadPicture_Click(object sender, EventArgs e)
        {
            OpenFileDialog ofd = new OpenFileDialog();
            ofd.ShowDialog();
            string fn = ofd.FileName.ToLower();
            if (string.IsNullOrEmpty(fn))
            {
                facade.FailureRecordShowLog(tb_FRlog, "错误:照片没有被加载");
                return;
            }
            if (!fn.EndsWith(".jpg") && !fn.EndsWith(".png"))
            {
                facade.FailureRecordShowLog(tb_FRlog, "错误:照片格式需为jpg或者png");
                return;
            }
            byte[] FileData = File.ReadAllBytes(fn);
            if (FileData.LongLength == 0)
            {
                facade.FailureRecordShowLog(tb_FRlog, "错误:空文件");
                return;
            }
            else if (FileData.LongLength > 20971520 / 2)
            {
                facade.FailureRecordShowLog(tb_FRlog, "错误:照片需小于10M");
                return;
            }
            pictureBox_FailureRecord.Image = Image.FromFile(fn);
            string PicSavePath = Path.Combine(Global.FailurePictureRootPath, Guid.NewGuid().ToString() + Path.GetExtension(fn));
            File.Copy(fn, PicSavePath);
            CurrentPictureFileName = PicSavePath;
            bt_FRuploadPicture.BackColor = Color.Green;
        }
        private void 关闭ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            this.Close();
        }
        private void lb_finalResult_Click(object sender, EventArgs e)
        {
            radioButton_FRpass.Checked = false;
            radioButton_FRfail.Checked = false;
            radioButton_FRhold.Checked = false;
            radioButton_FRrework.Checked = false;
            radioButton_FRquarantine.Checked = false;
            CurrentStatus = "";
        }
        private void bt_FRdelete_Click(object sender, EventArgs e)
        {
            if (SelectedFailureRecord == null) facade.FailureRecordShowLog(tb_FRlog, "请选择要删除的记录。");
            DialogResult result = MessageBox.Show("确定要删除这条记录吗？", "确认删除", MessageBoxButtons.YesNo, MessageBoxIcon.Warning);

            // 检查用户的选择
            if (result == DialogResult.Yes)
            {
                SelectedFailureRecord.DeleteUserName = Global.UserName;
                dal.DeleteFailureRecord(SelectedFailureRecord);
                bt_FRfresh.PerformClick();
                facade.FailureRecordShowLog(tb_FRlog, "删除成功。");
            }
            else
            {
                // 用户选择了“否”，取消删除操作
                facade.FailureRecordShowLog(tb_FRlog, "删除操作已取消。");
            }
        }
        private void dataGridView_failureRecord_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            // 检查双击的行索引是否有效
            if (e.RowIndex < 0) return;
            // 获取双击的行
            DataGridViewRow row = dataGridView_failureRecord.Rows[e.RowIndex];
            // 假设 ID 列的名称为 "Id"
            int id = Convert.ToInt32(row.Cells["Id"].Value);

            FailureRecordDal dal = new FailureRecordDal(Global.DbSettingSqlserver);
            var fr = dal.GetFailureRecord(id);
            FailureRecordInfo2Form(fr);
            string sn = tb_FRserialNumber.Text;
            OnSnTextInputCompleted(sn);
        }
        private void dataGridView_failureRecord_CellFormatting(object sender, DataGridViewCellFormattingEventArgs e)
        {
            if (dataGridView_failureRecord.Columns[e.ColumnIndex].Name == "Status")
            {
                string status = e.Value.ToString();
                switch (status)
                {
                    case "Pass":
                        e.CellStyle.BackColor = Color.White;
                        e.CellStyle.ForeColor = Color.Green;
                        break;
                    case "Fail":
                        e.CellStyle.BackColor = Color.White;
                        e.CellStyle.ForeColor = Color.Red;
                        break;
                    case "Hold":
                        e.CellStyle.BackColor = Color.White;
                        e.CellStyle.ForeColor = Color.Orange;
                        break;
                    case "Rework":
                        e.CellStyle.BackColor = Color.White;
                        e.CellStyle.ForeColor = Color.Blue;
                        break;
                    case "Quarantine":
                        e.CellStyle.BackColor = Color.White;
                        e.CellStyle.ForeColor = Color.Purple;
                        break;
                    default:
                        e.CellStyle.BackColor = Color.White;
                        e.CellStyle.ForeColor = Color.White;
                        break;
                }
            }
        }
        private void bt_FRfresh_Click(object sender, EventArgs e)
        {
            ClearField();
            FailureRecordList = dal.GetFailureRecords();
            dataGridView_failureRecord.DataSource = FailureRecordList;
        }
        private void bt_FRadd_Click(object sender, EventArgs e)
        {
            var failureRecord = FormInfo2FailureRecord();
            failureRecord.Status = "Hold"; // 默认状态为Hold
            string sn = failureRecord.SerialNumber;
            if (string.IsNullOrEmpty(failureRecord.SerialNumber))
            {
                facade.FailureRecordShowLog(tb_FRlog, $"错误:序列号{sn}不能为空");
                return;
            }
            
            if (dal.IsSerialNumberExist(failureRecord.SerialNumber))
            {
                facade.FailureRecordShowLog(tb_FRlog, $"错误:序列号{sn}已存在，无法新增。");
                return;
            }
            if (!daltym.IsSnValid(failureRecord.SerialNumber))
            {
                facade.FailureRecordShowLog(tb_FRlog, $"错误:太行数据库中没有该序列号:{failureRecord.SerialNumber}");
                return;
            }
            if (string.IsNullOrEmpty(failureRecord.ProductName))
            {
                facade.FailureRecordShowLog(tb_FRlog, "错误:产品名称不能为空");
                return;
            }
            if (string.IsNullOrEmpty(failureRecord.WorkStepProcessName))
            {
                facade.FailureRecordShowLog(tb_FRlog, "错误:工序不能为空");
                return;
            }
            if (!string.IsNullOrEmpty(failureRecord.PictureFileName))
            {
                if (!File.Exists(failureRecord.PictureFileName))
                {
                    facade.FailureRecordShowLog(tb_FRlog, "错误:照片不存在");
                    return;
                }
            }
            else
            {
                failureRecord.PictureFileName = Global.DefaultPicturePath;
            }
            if (string.IsNullOrEmpty(failureRecord.FailureMode))
            {
                facade.FailureRecordShowLog(tb_FRlog, "错误:失效模式不能为空");
                return;
            }
            if (!facade.IsFailureModeValidate(failureRecord.FailureMode))
            {
                facade.FailureRecordShowLog(tb_FRlog, "错误:失效模式未定义");
                return;
            }
            if (!IsValidFailureRecord(failureRecord))
            {
                return;
            }
            if (dal.AddFailureRecord(failureRecord))
            {
                facade.FailureRecordShowLog(tb_FRlog, "增加成功。");
                bt_FRfresh.PerformClick();
            }
            else
            {
                facade.FailureRecordShowLog(tb_FRlog, "增加失败。");
            }


        }
        private void FailureRecordInfo2Form(FailureRecord fr)
        {        
            SelectedFailureRecord = fr;

            cb_FRfailureMode.TextChanged -= cb_FRfailureMode_TextChanged;
            tb_FRserialNumber.Text = SelectedFailureRecord.SerialNumber;

            cb_FRfailureMode.Text = SelectedFailureRecord.FailureMode;
            cb_FRfailureMode.TextChanged += cb_FRfailureMode_TextChanged;
            cb_FRworkStep.Text = SelectedFailureRecord.WorkStepProcessName;


            tb_producType.Text = SelectedFailureRecord.ProductType;
            tb_productFamily.Text = SelectedFailureRecord.ProductFamily;
            tb_productName.Text = SelectedFailureRecord.ProductName;
            tb_FRcomment.Text = SelectedFailureRecord.Comment;
            try
            {
                pictureBox_FailureRecord.Image = Image.FromFile(SelectedFailureRecord.PictureFileName);
            }
            catch { pictureBox_FailureRecord.Image = Image.FromFile(Global.DefaultPicturePath); }

            CurrentPictureFileName = SelectedFailureRecord.PictureFileName;
            CurrentStatus = SelectedFailureRecord.Status;

            switch (CurrentStatus)
            {
                case "Pass":
                    radioButton_FRpass.Checked = true;
                    break;
                case "Fail":
                    radioButton_FRfail.Checked = true;
                    break;
                case "Hold":
                    radioButton_FRhold.Checked = true;
                    break;
                case "Rework":
                    radioButton_FRrework.Checked = true;
                    break;
                case "Quarantine":
                    radioButton_FRquarantine.Checked = true;
                    break;
                default:
                    radioButton_FRpass.Checked = false;
                    radioButton_FRfail.Checked = false;
                    radioButton_FRhold.Checked = false;
                    radioButton_FRrework.Checked = false;
                    radioButton_FRquarantine.Checked = false;
                    break;
            }
        }
        private FailureRecord FormInfo2FailureRecord()
        {
            FailureRecord fr = new FailureRecord();
            fr.PictureFileName = CurrentPictureFileName;
            fr.Status = radioButton_FRpass.Checked ? "Pass"
                        : radioButton_FRfail.Checked ? "Fail"
                        : radioButton_FRhold.Checked ? "Hold"
                        : radioButton_FRrework.Checked ? "Rework"
                        : radioButton_FRquarantine.Checked ? "Quarantine"
                        : "";
            fr.SerialNumber = tb_FRserialNumber.Text;
            fr.ProductName = tb_productName.Text;
            fr.ProductFamily = tb_productFamily.Text;
            fr.ProductType = tb_producType.Text;
            fr.WorkStepProcessName = cb_FRworkStep.Text;
            fr.FailureMode = cb_FRfailureMode.Text;
            fr.Comment = tb_FRcomment.Text;
            fr.CreateUserName = Global.UserName;
            fr.CreateTime = DateTime.Now;
            fr.ModifyUserName = Global.UserName;
            fr.ModifyTime = DateTime.Now;
            fr.DeleteUserName = Global.UserName;
            fr.DeleteTime = DateTime.Now;
            return fr;
        }
        private void tb_FRserialNumber_KeyPress(object sender, KeyPressEventArgs e)
        {
            e.KeyChar = char.ToUpper(e.KeyChar);
        }
        private void tb_productName_KeyPress(object sender, KeyPressEventArgs e)
        {
            e.KeyChar = char.ToUpper(e.KeyChar);
        }

        private bool IsValidFailureRecord(FailureRecord failureRecord)
        {
            var productInfo = UpdateProductInfo(failureRecord.SerialNumber, false);
            UpdateProcesses(failureRecord.SerialNumber);
            if (failureRecord.ProductType != productInfo.ProductType)
            {
                facade.FailureRecordShowLog(tb_FRlog, "错误:产品类型错误。");
                return false;
            }
            if (failureRecord.ProductFamily != productInfo.ProductFamily)
            {
                facade.FailureRecordShowLog(tb_FRlog, "错误:产品系列错误。");
                return false;
            }
            if (failureRecord.ProductName != productInfo.ProductName)
            {
                facade.FailureRecordShowLog(tb_FRlog, "错误:产品名称错误。");
                return false;
            }
            if (!this.ProcessesList.Contains(failureRecord.WorkStepProcessName))
            {
                facade.FailureRecordShowLog(tb_FRlog, "错误:流程步骤录入错误。");
                return false;
            }
            return true;
        }

        private void cb_FRworkStep_TextChanged(object sender, EventArgs e)
        {
        }

        

        private void bt_FRsearch_Click(object sender, EventArgs e)
        {
            string sn = tb_FRserialNumber.Text;
            string productFamily = tb_productFamily.Text.Trim();
            string productType = tb_producType.Text.Trim();
            string productName = tb_productName.Text.Trim();
            string workStep = cb_FRworkStep.Text.Trim();
            FailureRecordDal dal = new FailureRecordDal(Global.DbSettingSqlserver);
            var recordList = dal.GetFailureRecordsByFuzzy(sn);

            // 筛选
            var filtered = recordList.Where(fr =>
                // ProductFamily 筛选（为空不限制，不为空就做模糊包含，忽略大小写）
                (string.IsNullOrEmpty(productFamily) ||
                 (fr.ProductFamily ?? "").IndexOf(productFamily, StringComparison.OrdinalIgnoreCase) >= 0)
                &&
                // ProductName 筛选
                (string.IsNullOrEmpty(productName) ||
                 (fr.ProductName ?? "").IndexOf(productName, StringComparison.OrdinalIgnoreCase) >= 0)
                &&
                // WorkStepProcessName 筛选
                (string.IsNullOrEmpty(workStep) ||
                 (fr.WorkStepProcessName ?? "").IndexOf(workStep, StringComparison.OrdinalIgnoreCase) >= 0)
                 &&
                // ProductType 筛选
                (string.IsNullOrEmpty(productType) ||
                 (fr.ProductType ?? "").IndexOf(productType, StringComparison.OrdinalIgnoreCase) >= 0)
            ).ToList();

            FailureRecordList = filtered;
            dataGridView_failureRecord.DataSource = FailureRecordList;
        }

        private void bt_FRedit_Click(object sender, EventArgs e)
        {
            string sn = tb_FRserialNumber.Text;

            if (string.IsNullOrEmpty(sn))
            {
                facade.FailureRecordShowLog(tb_FRlog, $"错误:序列号{sn}不能为空");
                return;
            }
            if (!dal.IsSerialNumberExist(sn))
            {
                facade.FailureRecordShowLog(tb_FRlog, $"错误:序列号{sn}不存在，需要新增。");
                return;
            }
            if (string.IsNullOrEmpty(tb_productName.Text.Trim()))
            {
                facade.FailureRecordShowLog(tb_FRlog, "错误:产品名称不能为空");
                return;
            }
            if (string.IsNullOrEmpty(tb_producType.Text.Trim()))
            {
                facade.FailureRecordShowLog(tb_FRlog, "错误:产品类型不能为空");
                return;
            }
            if (string.IsNullOrEmpty(tb_productFamily.Text.Trim()))
            {
                facade.FailureRecordShowLog(tb_FRlog, "错误:产品系列不能为空");
                return;
            }
            if (string.IsNullOrEmpty(cb_FRworkStep.Text.Trim()))
            {
                facade.FailureRecordShowLog(tb_FRlog, "错误:工序不能为空");
                return;
            }
            if (string.IsNullOrEmpty(cb_FRfailureMode.Text.Trim()))
            {
                facade.FailureRecordShowLog(tb_FRlog, "错误:失效模式不能为空");
                return;
            }
            if (!facade.IsFailureModeValidate(cb_FRfailureMode.Text.Trim()))
            {
                facade.FailureRecordShowLog(tb_FRlog, "错误:失效模式未定义");
                return;
            }

            FailureRecord tempFailureRecord = dal.GetFailureRecordBySn(sn);
            SelectedFailureRecord = new FailureRecord();
            SelectedFailureRecord.Id = tempFailureRecord.Id;
            SelectedFailureRecord.SerialNumber = tb_FRserialNumber.Text.Trim();
            SelectedFailureRecord.CreateTime = tempFailureRecord.CreateTime;
            SelectedFailureRecord.CreateUserName = tempFailureRecord.CreateUserName;
            SelectedFailureRecord.ProductFamily = tb_productFamily.Text.Trim();
            SelectedFailureRecord.ProductType = tb_producType.Text.Trim();
            SelectedFailureRecord.ProductName = tb_productName.Text.Trim();
            SelectedFailureRecord.WorkStepProcessName = cb_FRworkStep.Text.Trim();
            SelectedFailureRecord.Comment = tb_FRcomment.Text.Replace("\t", " ").Replace("\r", " ").Replace("\n", " ");
            SelectedFailureRecord.FailureMode = cb_FRfailureMode.Text;
            SelectedFailureRecord.ModifyUserName = Global.UserName;
            SelectedFailureRecord.ModifyTime = DateTime.Now;
            SelectedFailureRecord.Status = radioButton_FRpass.Checked ? "Pass" : radioButton_FRfail.Checked ? "Fail" : radioButton_FRhold.Checked ? "Hold" : radioButton_FRrework.Checked ? "Rework" : radioButton_FRquarantine.Checked ? "Quarantine" : "";
            SelectedFailureRecord.PictureFileName = CurrentPictureFileName ?? Global.DefaultPicturePath;

            if (tempFailureRecord.WorkStepProcessName == SelectedFailureRecord.WorkStepProcessName)//相同流程的已判定产品，不能修改回 "Hold" 状态
            {
                if (SelectedFailureRecord.Status == "Hold"&&tempFailureRecord.Status!="Hold")
                {
                    facade.FailureRecordShowLog(tb_FRlog, $"错误:序列号{SelectedFailureRecord.SerialNumber}的流程步骤 {SelectedFailureRecord.WorkStepProcessName} 已判定为非Hold状态，不能修改回Hold，请联系产品工程师。");
                    return;
                }
            }

            if (!IsValidFailureRecord(SelectedFailureRecord))
            {
                return;
            }

            if (dal.UpdateFailureRecord(SelectedFailureRecord))
            {
                facade.FailureRecordShowLog(tb_FRlog, "更新成功。");
                var product = SelectedFailureRecord.ProductType;
                bt_FRfresh.PerformClick();

                PerformSearchProductAfterEdit(product);

            }
            else
            {
                facade.FailureRecordShowLog(tb_FRlog, "更新失败。");
            }
        }

        private void PerformSearchProductAfterEdit(string product)
        {
            tb_producType.Text = product;
            bt_FRsearch.PerformClick();
        }



        private void dataGridView_failureRecord_ColumnHeaderMouseClick(object sender, DataGridViewCellMouseEventArgs e)
        {
            // 获取点击的列
            DataGridViewColumn column = dataGridView_failureRecord.Columns[e.ColumnIndex];
            var columnName = column.Name;
            // 获取当前排序方向
            if (facade.direction == ListSortDirection.Ascending)
            {
                facade.direction = ListSortDirection.Descending;
            }
            else
            {
                facade.direction = ListSortDirection.Ascending;
            }

            // 根据列名和排序方向对 FailureRecordList 进行排序
            if (facade.direction == ListSortDirection.Ascending)
            {
                FailureRecordList = FailureRecordList.OrderBy(record => GetPropertyValue(record, columnName)).ToList();
            }
            else
            {
                FailureRecordList = FailureRecordList.OrderByDescending(record => GetPropertyValue(record, columnName)).ToList();
            }

            // 更新 DataGridView 的数据源
            dataGridView_failureRecord.DataSource = null;
            dataGridView_failureRecord.DataSource = FailureRecordList;
        }

        private object GetPropertyValue(object obj, string propertyName)
        {
            return obj.GetType().GetProperty(propertyName).GetValue(obj, null);
        }

        private void tb_FRserialNumber_TextChanged(object sender, EventArgs e)
        {
            //// 每次输入都重置定时器
            //_debounceTimer.Stop();
            //_debounceTimer.Start();
            
        }

        private bool isFiltering = false;
        private void cb_FRfailureMode_TextChanged(object sender, EventArgs e)
        {
            try
            {
                // 防止程序赋值触发时递归
                if (isFiltering) return;

                string input = cb_FRfailureMode.Text;
                if (string.IsNullOrEmpty(input)) return;

                int selStart = cb_FRfailureMode.SelectionStart;

                // 1. 筛选
                var filtered = FailureModeList
                    .Where(x => x.IndexOf(input, StringComparison.OrdinalIgnoreCase) >= 0)
                    .ToList();

                // 如果只有一个匹配项且输入等于它，不做任何事，防止后续逻辑干扰
                if (filtered.Count == 1 && filtered[0].Equals(input, StringComparison.OrdinalIgnoreCase))
                {
                    return;
                }

                // 2. 刷新数据源
                isFiltering = true; // 开始程序筛选
                Thread.Sleep(50);
                cb_FRfailureMode.TextChanged -= cb_FRfailureMode_TextChanged;

                cb_FRfailureMode.DataSource = null;
                cb_FRfailureMode.Items.Clear();
                cb_FRfailureMode.DataSource = filtered;

                // 恢复输入
                cb_FRfailureMode.Text = input;
                cb_FRfailureMode.SelectionStart = selStart;

                cb_FRfailureMode.DroppedDown = true; // 自动弹出下拉
                cb_FRfailureMode.TextChanged += cb_FRfailureMode_TextChanged;
                isFiltering = false; // 结束
            }
            catch
            {
                isFiltering = false; // 结束
            }

        }

        private void dataGridView_failureRecord_CurrentCellChanged(object sender, EventArgs e)
        {
            //if (dataGridView_failureRecord.CurrentRow == null) return;
            
            //var selectedRow = dataGridView_failureRecord.CurrentRow;
            //var idstr = selectedRow.Cells["Id"].Value?.ToString();
            //if (string.IsNullOrEmpty(idstr)) return;
            //var id = Convert.ToInt32(idstr);

            //FailureRecordDal dal = new FailureRecordDal(Global.DbSettingSqlserver);
            //var fr = dal.GetFailureRecord(id);
            //FailureRecordInfo2Form(fr);
        }

        private void 下载ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (FailureRecordList.Count < 1)
            {
                facade.FailureRecordShowLog(tb_FRlog, "没有可下载的数据。");
                return;
            }

            SaveFileDialog sfd = new SaveFileDialog();
            sfd.Filter = "Data Files|*.xlsx";

            if (sfd.ShowDialog() == DialogResult.OK)
            {
                try
                {
                    string FilePath = sfd.FileName;
                    var report = facade.GetSyncFusionExcelReportMemory(FailureRecordList);
                    File.WriteAllBytes(FilePath, report);
                    facade.FailureRecordShowLog(tb_FRlog, "下载完成。");
                }
                catch (Exception ex)
                {
                    facade.FailureRecordShowLog(tb_FRlog, "文件下载失败:" + ex.Message);
                }
            }
        }

        private void tb_FRserialNumber_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                e.SuppressKeyPress = true;  // 屏蔽“嘀”声
                OnSnTextInputCompleted(tb_FRserialNumber.Text);
            }
        }

        private void menuStrip1_ItemClicked(object sender, ToolStripItemClickedEventArgs e)
        {

        }
    }
}
