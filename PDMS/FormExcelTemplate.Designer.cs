namespace PDMS
{
    partial class FormExcelTemplate
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

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

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.menuStrip1 = new System.Windows.Forms.MenuStrip();
            this.设置ToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.关闭ToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.载入模板ToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.数据校验ToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.上传ToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.statusStrip1 = new System.Windows.Forms.StatusStrip();
            this.toolStripStatusLabel1 = new System.Windows.Forms.ToolStripStatusLabel();
            this.spreadsheet1 = new Syncfusion.Windows.Forms.Spreadsheet.Spreadsheet();
            this.tabControl1 = new System.Windows.Forms.TabControl();
            this.tp_template = new System.Windows.Forms.TabPage();
            this.tp_history = new System.Windows.Forms.TabPage();
            this.dataGridView_history = new System.Windows.Forms.DataGridView();
            this.Id = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.SerialNumber = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Approval = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.HostName = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.UploadUser = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.UploadDate = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.ResultFileName = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.menuStrip1.SuspendLayout();
            this.statusStrip1.SuspendLayout();
            this.tabControl1.SuspendLayout();
            this.tp_template.SuspendLayout();
            this.tp_history.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dataGridView_history)).BeginInit();
            this.SuspendLayout();
            // 
            // menuStrip1
            // 
            this.menuStrip1.Font = new System.Drawing.Font("Microsoft YaHei UI", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.menuStrip1.ImageScalingSize = new System.Drawing.Size(20, 20);
            this.menuStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.设置ToolStripMenuItem,
            this.载入模板ToolStripMenuItem,
            this.数据校验ToolStripMenuItem,
            this.上传ToolStripMenuItem});
            this.menuStrip1.Location = new System.Drawing.Point(0, 0);
            this.menuStrip1.Name = "menuStrip1";
            this.menuStrip1.Size = new System.Drawing.Size(1080, 35);
            this.menuStrip1.TabIndex = 0;
            this.menuStrip1.Text = "menuStrip1";
            // 
            // 设置ToolStripMenuItem
            // 
            this.设置ToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.关闭ToolStripMenuItem});
            this.设置ToolStripMenuItem.Name = "设置ToolStripMenuItem";
            this.设置ToolStripMenuItem.Size = new System.Drawing.Size(66, 31);
            this.设置ToolStripMenuItem.Text = "设置";
            // 
            // 关闭ToolStripMenuItem
            // 
            this.关闭ToolStripMenuItem.Name = "关闭ToolStripMenuItem";
            this.关闭ToolStripMenuItem.Size = new System.Drawing.Size(224, 32);
            this.关闭ToolStripMenuItem.Text = "关闭";
            this.关闭ToolStripMenuItem.Click += new System.EventHandler(this.关闭ToolStripMenuItem_Click);
            // 
            // 载入模板ToolStripMenuItem
            // 
            this.载入模板ToolStripMenuItem.Name = "载入模板ToolStripMenuItem";
            this.载入模板ToolStripMenuItem.Size = new System.Drawing.Size(106, 31);
            this.载入模板ToolStripMenuItem.Text = "载入模板";
            this.载入模板ToolStripMenuItem.Click += new System.EventHandler(this.载入模板ToolStripMenuItem_Click);
            // 
            // 数据校验ToolStripMenuItem
            // 
            this.数据校验ToolStripMenuItem.Name = "数据校验ToolStripMenuItem";
            this.数据校验ToolStripMenuItem.Size = new System.Drawing.Size(106, 31);
            this.数据校验ToolStripMenuItem.Text = "数据校验";
            this.数据校验ToolStripMenuItem.Click += new System.EventHandler(this.数据校验ToolStripMenuItem_Click);
            // 
            // 上传ToolStripMenuItem
            // 
            this.上传ToolStripMenuItem.Name = "上传ToolStripMenuItem";
            this.上传ToolStripMenuItem.Size = new System.Drawing.Size(106, 31);
            this.上传ToolStripMenuItem.Text = "上传数据";
            this.上传ToolStripMenuItem.Click += new System.EventHandler(this.上传ToolStripMenuItem_Click);
            // 
            // statusStrip1
            // 
            this.statusStrip1.ImageScalingSize = new System.Drawing.Size(20, 20);
            this.statusStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.toolStripStatusLabel1});
            this.statusStrip1.Location = new System.Drawing.Point(0, 882);
            this.statusStrip1.Name = "statusStrip1";
            this.statusStrip1.Size = new System.Drawing.Size(1080, 22);
            this.statusStrip1.TabIndex = 1;
            this.statusStrip1.Text = "statusStrip1";
            // 
            // toolStripStatusLabel1
            // 
            this.toolStripStatusLabel1.Name = "toolStripStatusLabel1";
            this.toolStripStatusLabel1.Size = new System.Drawing.Size(0, 16);
            // 
            // spreadsheet1
            // 
            this.spreadsheet1.AllowCellContextMenu = true;
            this.spreadsheet1.AllowExtendRowColumnCount = true;
            this.spreadsheet1.AllowFiltering = false;
            this.spreadsheet1.AllowFormulaRangeSelection = true;
            this.spreadsheet1.AllowTabItemContextMenu = true;
            this.spreadsheet1.AllowZooming = true;
            this.spreadsheet1.BaseThemeName = "";
            this.spreadsheet1.DefaultColumnCount = 101;
            this.spreadsheet1.DefaultRowCount = 101;
            this.spreadsheet1.DisplayAlerts = true;
            this.spreadsheet1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.spreadsheet1.FileName = "Book1";
            this.spreadsheet1.FormulaBarVisibility = false;
            this.spreadsheet1.IsCustomTabItemContextMenuEnabled = false;
            this.spreadsheet1.Location = new System.Drawing.Point(3, 3);
            this.spreadsheet1.Name = "spreadsheet1";
            this.spreadsheet1.SelectedTabIndex = 0;
            this.spreadsheet1.ShowBusyIndicator = true;
            this.spreadsheet1.Size = new System.Drawing.Size(1033, 833);
            this.spreadsheet1.TabIndex = 2;
            this.spreadsheet1.Text = "spreadsheet1";
            this.spreadsheet1.ThemeName = "Default";
            // 
            // tabControl1
            // 
            this.tabControl1.Alignment = System.Windows.Forms.TabAlignment.Left;
            this.tabControl1.Controls.Add(this.tp_template);
            this.tabControl1.Controls.Add(this.tp_history);
            this.tabControl1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tabControl1.Font = new System.Drawing.Font("微软雅黑", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.tabControl1.Location = new System.Drawing.Point(0, 35);
            this.tabControl1.Multiline = true;
            this.tabControl1.Name = "tabControl1";
            this.tabControl1.SelectedIndex = 0;
            this.tabControl1.Size = new System.Drawing.Size(1080, 847);
            this.tabControl1.TabIndex = 3;
            // 
            // tp_template
            // 
            this.tp_template.Controls.Add(this.spreadsheet1);
            this.tp_template.Location = new System.Drawing.Point(37, 4);
            this.tp_template.Name = "tp_template";
            this.tp_template.Padding = new System.Windows.Forms.Padding(3);
            this.tp_template.Size = new System.Drawing.Size(1039, 839);
            this.tp_template.TabIndex = 0;
            this.tp_template.Text = "数据模板";
            this.tp_template.UseVisualStyleBackColor = true;
            // 
            // tp_history
            // 
            this.tp_history.Controls.Add(this.dataGridView_history);
            this.tp_history.Location = new System.Drawing.Point(37, 4);
            this.tp_history.Name = "tp_history";
            this.tp_history.Padding = new System.Windows.Forms.Padding(3);
            this.tp_history.Size = new System.Drawing.Size(1039, 839);
            this.tp_history.TabIndex = 1;
            this.tp_history.Text = "历史记录";
            this.tp_history.UseVisualStyleBackColor = true;
            // 
            // dataGridView_history
            // 
            this.dataGridView_history.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.AllCells;
            this.dataGridView_history.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dataGridView_history.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.Id,
            this.SerialNumber,
            this.Approval,
            this.HostName,
            this.UploadUser,
            this.UploadDate,
            this.ResultFileName});
            this.dataGridView_history.Dock = System.Windows.Forms.DockStyle.Fill;
            this.dataGridView_history.GridColor = System.Drawing.Color.White;
            this.dataGridView_history.Location = new System.Drawing.Point(3, 3);
            this.dataGridView_history.Name = "dataGridView_history";
            this.dataGridView_history.RowHeadersVisible = false;
            this.dataGridView_history.RowHeadersWidth = 51;
            this.dataGridView_history.RowTemplate.Height = 27;
            this.dataGridView_history.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            this.dataGridView_history.Size = new System.Drawing.Size(1033, 833);
            this.dataGridView_history.TabIndex = 0;
            // 
            // Id
            // 
            this.Id.DataPropertyName = "Id";
            this.Id.HeaderText = "Id";
            this.Id.MinimumWidth = 6;
            this.Id.Name = "Id";
            this.Id.Width = 60;
            // 
            // SerialNumber
            // 
            this.SerialNumber.DataPropertyName = "SerialNumber";
            this.SerialNumber.HeaderText = "SerialNumber";
            this.SerialNumber.MinimumWidth = 6;
            this.SerialNumber.Name = "SerialNumber";
            this.SerialNumber.Width = 172;
            // 
            // Approval
            // 
            this.Approval.DataPropertyName = "Approval";
            this.Approval.HeaderText = "Approval";
            this.Approval.MinimumWidth = 6;
            this.Approval.Name = "Approval";
            this.Approval.Width = 129;
            // 
            // HostName
            // 
            this.HostName.DataPropertyName = "HostName";
            this.HostName.HeaderText = "HostName";
            this.HostName.MinimumWidth = 6;
            this.HostName.Name = "HostName";
            this.HostName.Width = 142;
            // 
            // UploadUser
            // 
            this.UploadUser.DataPropertyName = "UploadUser";
            this.UploadUser.HeaderText = "UploadUser";
            this.UploadUser.MinimumWidth = 6;
            this.UploadUser.Name = "UploadUser";
            this.UploadUser.Width = 154;
            // 
            // UploadDate
            // 
            this.UploadDate.DataPropertyName = "UploadDate";
            this.UploadDate.HeaderText = "UploadDate";
            this.UploadDate.MinimumWidth = 6;
            this.UploadDate.Name = "UploadDate";
            this.UploadDate.Width = 155;
            // 
            // ResultFileName
            // 
            this.ResultFileName.HeaderText = "ResultFileName";
            this.ResultFileName.MinimumWidth = 6;
            this.ResultFileName.Name = "ResultFileName";
            this.ResultFileName.Width = 187;
            // 
            // FormExcelTemplate
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 15F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1080, 904);
            this.Controls.Add(this.tabControl1);
            this.Controls.Add(this.statusStrip1);
            this.Controls.Add(this.menuStrip1);
            this.MainMenuStrip = this.menuStrip1;
            this.Name = "FormExcelTemplate";
            this.Text = "FormExcelTemplate";
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.FormExcelTemplate_FormClosing);
            this.Load += new System.EventHandler(this.FormExcelTemplate_Load);
            this.menuStrip1.ResumeLayout(false);
            this.menuStrip1.PerformLayout();
            this.statusStrip1.ResumeLayout(false);
            this.statusStrip1.PerformLayout();
            this.tabControl1.ResumeLayout(false);
            this.tp_template.ResumeLayout(false);
            this.tp_history.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.dataGridView_history)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.MenuStrip menuStrip1;
        private System.Windows.Forms.StatusStrip statusStrip1;
        private System.Windows.Forms.ToolStripMenuItem 设置ToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem 载入模板ToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem 关闭ToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem 上传ToolStripMenuItem;
        private Syncfusion.Windows.Forms.Spreadsheet.Spreadsheet spreadsheet1;
        private System.Windows.Forms.ToolStripMenuItem 数据校验ToolStripMenuItem;
        private System.Windows.Forms.ToolStripStatusLabel toolStripStatusLabel1;
        private System.Windows.Forms.TabControl tabControl1;
        private System.Windows.Forms.TabPage tp_template;
        private System.Windows.Forms.TabPage tp_history;
        private System.Windows.Forms.DataGridView dataGridView_history;
        private System.Windows.Forms.DataGridViewTextBoxColumn Id;
        private System.Windows.Forms.DataGridViewTextBoxColumn SerialNumber;
        private System.Windows.Forms.DataGridViewTextBoxColumn Approval;
        private System.Windows.Forms.DataGridViewTextBoxColumn HostName;
        private System.Windows.Forms.DataGridViewTextBoxColumn UploadUser;
        private System.Windows.Forms.DataGridViewTextBoxColumn UploadDate;
        private System.Windows.Forms.DataGridViewTextBoxColumn ResultFileName;
    }
}