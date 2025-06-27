using DocumentFormat.OpenXml.Bibliography;
using DocumentFormat.OpenXml.Office.CustomUI;
using PDMS.Facade;
using PDMS.SyncFusion;
using Syncfusion.Windows.Forms.Spreadsheet;
using Syncfusion.Windows.Forms.Tools;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace PDMS
{
    public partial class FormExcelTemplate : Form
    {
        public  SpreadsheetHelper spreadsheet { get; set; }
        public string SelectedTemplate { get; set; } = null;
        public List<string> TemplateList { get; set; }
        public FormFacade facade { get; set; }

        public FormExcelTemplate()
        {
            InitializeComponent();
            facade = new FormFacade(this);
            try
            {
                facade.CopyExcelTemplates2Local();

                TemplateList = facade.GetTemplateList();
            }
            catch
            {
                MessageBox.Show("连接服务器\\192.168.31.223失败。");
                this.Close();
            }

            InitialForm();
        }

        private void InitialForm()
        {
            spreadsheet = null;
            spreadsheet = new SpreadsheetHelper(spreadsheet1);
            ShowStatus("",Color.White);
        }


        private void 关闭ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void 载入模板ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            FormSelection selectForm = new FormSelection(TemplateList);
            if (selectForm.ShowDialog() == DialogResult.OK)
            {
                SelectedTemplate = selectForm.RetrunFileName;
                LoadTemplate();
            }
            else
            {
                return;
            }
        }

        private void FormExcelTemplate_Load(object sender, EventArgs e)
        {
        }

        private void LoadTemplate()
        {
            spreadsheet.Open(SelectedTemplate);
            InitialForm();
        }

        private void 上传ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            ShowStatus("...to do", Color.Red);
        }

        private void FormExcelTemplate_FormClosing(object sender, FormClosingEventArgs e)
        {
            spreadsheet.Dispose();
        }

        private void 数据校验ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            List<SpreadsheetModel> list = spreadsheet.ReadSheetToList();

            if (spreadsheet.ValidateSpreadsheetModels(list, out string error))
            {
                ShowStatus("数据校验成功。" + error, Color.Green);
            }
            else
            {
                ShowStatus("数据校验失败：" + error, Color.Red);
            }
        }

        private void ShowStatus(string msg,Color color)
        {
            toolStripStatusLabel1.Text = msg;
            toolStripStatusLabel1.ForeColor = color;
        }

        private void 历史记录ToolStripMenuItem_Click(object sender, EventArgs e)
        {

        }
    }
}
