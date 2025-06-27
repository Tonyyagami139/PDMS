using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace PDMS
{
    public partial class FormSelection : Form
    {

        public string RetrunFileName { get; set; }

        public List<string> FileList { get; set; }
        public FormSelection(List<string> ExcelFileList)
        {
            InitializeComponent();
            flowLayoutPanel1.Controls.Clear();
            flowLayoutPanel1.AutoScroll = true;
            FileList = ExcelFileList;
        }

        private void FormSelection_Load(object sender, EventArgs e)
        {
            LoadButtons(string.Empty);
        }

        private void LoadButtons(string filter)
        {
            foreach (var item in FileList)
            {
                if (!item.EndsWith("xlsx"))
                {
                    continue;
                }
                Button b = new Button();
                b.Text = Path.GetFileNameWithoutExtension(item);
                if (!string.IsNullOrEmpty(filter) && !b.Text.ToLower().Contains(filter.ToLower()))
                {
                    continue;
                }
                b.Tag = item;
                b.Font = new Font("Arial", 12, FontStyle.Bold);
                b.Width = flowLayoutPanel1.Width;
                b.Height = 50;
                b.Click += B_Click; ;
                flowLayoutPanel1.Controls.Add(b);
            }
        }
        private void B_Click(object sender, EventArgs e)
        {
            Button clickedButton = sender as Button;

            this.RetrunFileName = clickedButton.Tag.ToString();
            this.DialogResult = DialogResult.OK;
            this.Close();
        }
        private void SelectForm_SizeChanged(object sender, EventArgs e)
        {
            foreach (Button button in flowLayoutPanel1.Controls)
            {
                button.Width = flowLayoutPanel1.Width;
            }
        }
        private void bt_cancel_Click(object sender, EventArgs e)
        {
            this.DialogResult = DialogResult.Cancel;
            this.Close();
        }
        private void tb_selectFilter_TextChanged(object sender, EventArgs e)
        {
            string filter = tb_selectFilter.Text;
            flowLayoutPanel1.Controls.Clear();
            LoadButtons(filter);
        }
    }
}
