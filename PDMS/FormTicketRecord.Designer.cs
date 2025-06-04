using System.Windows.Forms;

namespace PDMS
{
    partial class FormTicketRecord
    {
        private System.ComponentModel.IContainer components = null;
        private TableLayoutPanel tableLayoutPanel1;
        private Panel panelLeft;
        private DataGridView dataGridViewTickets;
        private TextBox tbTitle;
        private TextBox tbDescription;
        private ComboBox cbStatus;
        private ComboBox cbPriority;
        private TextBox tbAssignee;
        private TextBox tbAttachments;
        private Button btnUpload;
        private Button btnAdd;
        private Button btnDelete;
        private Button btnEdit;
        private Button btnSearch;
        private Button btnRefresh;
        private Button btnExport;
        private RichTextBox richLog;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        private void InitializeComponent()
        {
            DataGridViewCellStyle dataGridViewCellStyle1 = new DataGridViewCellStyle();
            this.tableLayoutPanel1 = new TableLayoutPanel();
            this.panelLeft = new Panel();
            this.dataGridViewTickets = new DataGridView();
            this.tbTitle = new TextBox();
            this.tbDescription = new TextBox();
            this.cbStatus = new ComboBox();
            this.cbPriority = new ComboBox();
            this.tbAssignee = new TextBox();
            this.tbAttachments = new TextBox();
            this.btnUpload = new Button();
            this.btnAdd = new Button();
            this.btnDelete = new Button();
            this.btnEdit = new Button();
            this.btnSearch = new Button();
            this.btnRefresh = new Button();
            this.btnExport = new Button();
            this.richLog = new RichTextBox();
            this.tableLayoutPanel1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dataGridViewTickets)).BeginInit();
            this.SuspendLayout();
            // 
            // tableLayoutPanel1
            // 
            this.tableLayoutPanel1.ColumnCount = 2;
            this.tableLayoutPanel1.ColumnStyles.Add(new ColumnStyle(SizeType.Absolute, 300F));
            this.tableLayoutPanel1.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 100F));
            this.tableLayoutPanel1.Dock = DockStyle.Fill;
            this.tableLayoutPanel1.Controls.Add(this.panelLeft, 0, 0);
            this.tableLayoutPanel1.Controls.Add(this.dataGridViewTickets, 1, 0);
            this.tableLayoutPanel1.Controls.Add(this.richLog, 0, 1);
            this.tableLayoutPanel1.SetColumnSpan(this.dataGridViewTickets, 2);
            this.tableLayoutPanel1.RowCount = 2;
            this.tableLayoutPanel1.RowStyles.Add(new RowStyle(SizeType.Percent, 70F));
            this.tableLayoutPanel1.RowStyles.Add(new RowStyle(SizeType.Percent, 30F));
            this.tableLayoutPanel1.Dock = DockStyle.Fill;
            // 
            // panelLeft
            // 
            this.panelLeft.Dock = DockStyle.Fill;
            this.panelLeft.Padding = new Padding(5);
            this.panelLeft.AutoScroll = true;
            this.panelLeft.Controls.Add(new Label(){Text="Title",Top=10,Left=5});
            this.tbTitle.Top = 30; this.tbTitle.Left = 5; this.tbTitle.Width = 250;
            this.panelLeft.Controls.Add(this.tbTitle);
            this.panelLeft.Controls.Add(new Label(){Text="Description",Top=60,Left=5});
            this.tbDescription.Top = 80; this.tbDescription.Left = 5; this.tbDescription.Width = 250; this.tbDescription.Height=60; this.tbDescription.Multiline=true;
            this.panelLeft.Controls.Add(this.tbDescription);
            this.panelLeft.Controls.Add(new Label(){Text="Status",Top=150,Left=5});
            this.cbStatus.Top = 170; this.cbStatus.Left = 5; this.cbStatus.Width = 150;
            this.panelLeft.Controls.Add(this.cbStatus);
            this.panelLeft.Controls.Add(new Label(){Text="Priority",Top=200,Left=5});
            this.cbPriority.Top = 220; this.cbPriority.Left = 5; this.cbPriority.Width = 150;
            this.panelLeft.Controls.Add(this.cbPriority);
            this.panelLeft.Controls.Add(new Label(){Text="Assignee",Top=250,Left=5});
            this.tbAssignee.Top = 270; this.tbAssignee.Left = 5; this.tbAssignee.Width = 250;
            this.panelLeft.Controls.Add(this.tbAssignee);
            this.panelLeft.Controls.Add(new Label(){Text="Attachments",Top=300,Left=5});
            this.tbAttachments.Top = 320; this.tbAttachments.Left = 5; this.tbAttachments.Width = 200;
            this.panelLeft.Controls.Add(this.tbAttachments);
            this.btnUpload.Text = "Upload"; this.btnUpload.Top = 320; this.btnUpload.Left = 210; this.btnUpload.Width = 70;
            this.panelLeft.Controls.Add(this.btnUpload);
            this.btnAdd.Text = "Add"; this.btnAdd.Top = 360; this.btnAdd.Left = 5; this.panelLeft.Controls.Add(this.btnAdd);
            this.btnDelete.Text = "Delete"; this.btnDelete.Top = 360; this.btnDelete.Left = 70; this.panelLeft.Controls.Add(this.btnDelete);
            this.btnEdit.Text = "Edit"; this.btnEdit.Top = 360; this.btnEdit.Left = 140; this.panelLeft.Controls.Add(this.btnEdit);
            this.btnSearch.Text = "Search"; this.btnSearch.Top = 390; this.btnSearch.Left = 5; this.panelLeft.Controls.Add(this.btnSearch);
            this.btnRefresh.Text = "Refresh"; this.btnRefresh.Top = 390; this.btnRefresh.Left = 70; this.panelLeft.Controls.Add(this.btnRefresh);
            this.btnExport.Text = "Export"; this.btnExport.Top = 390; this.btnExport.Left = 140; this.panelLeft.Controls.Add(this.btnExport);
            // 
            // dataGridViewTickets
            // 
            this.dataGridViewTickets.Dock = DockStyle.Fill;
            this.dataGridViewTickets.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.AllCells;
            dataGridViewCellStyle1.BackColor = System.Drawing.Color.WhiteSmoke;
            this.dataGridViewTickets.AlternatingRowsDefaultCellStyle = dataGridViewCellStyle1;
            this.dataGridViewTickets.SelectionMode = DataGridViewSelectionMode.FullRowSelect;
            this.dataGridViewTickets.MultiSelect = false;
            // 
            // richLog
            // 
            this.richLog.Dock = DockStyle.Fill;
            this.richLog.ReadOnly = true;
            // 
            // FormTicketRecord
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 15F);
            this.AutoScaleMode = AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(800, 600);
            this.Controls.Add(this.tableLayoutPanel1);
            this.Text = "Ticket Record";
            this.tableLayoutPanel1.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.dataGridViewTickets)).EndInit();
            this.ResumeLayout(false);
        }

        #endregion
    }
}
