namespace PDMS
{
    partial class FormTicketRecord
    {
        private System.ComponentModel.IContainer components = null;
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        private void InitializeComponent()
        {
            this.dataGridView_tickets = new System.Windows.Forms.DataGridView();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.bt_delete = new System.Windows.Forms.Button();
            this.bt_update = new System.Windows.Forms.Button();
            this.bt_add = new System.Windows.Forms.Button();
            this.bt_search = new System.Windows.Forms.Button();
            this.radioButton_pending = new System.Windows.Forms.RadioButton();
            this.radioButton_inProgress = new System.Windows.Forms.RadioButton();
            this.radioButton_finished = new System.Windows.Forms.RadioButton();
            this.radioButton_created = new System.Windows.Forms.RadioButton();
            this.label3 = new System.Windows.Forms.Label();
            this.tb_description = new System.Windows.Forms.TextBox();
            this.label2 = new System.Windows.Forms.Label();
            this.tb_title = new System.Windows.Forms.TextBox();
            this.label1 = new System.Windows.Forms.Label();
            this.tb_search = new System.Windows.Forms.TextBox();
            this.label4 = new System.Windows.Forms.Label();
            this.label5 = new System.Windows.Forms.Label();
            this.label6 = new System.Windows.Forms.Label();
            this.label7 = new System.Windows.Forms.Label();
            this.cb_priority = new System.Windows.Forms.ComboBox();
            this.cb_status = new System.Windows.Forms.ComboBox();
            this.tb_assignee = new System.Windows.Forms.TextBox();
            this.label8 = new System.Windows.Forms.Label();
            this.tb_attachments = new System.Windows.Forms.TextBox();
            this.bt_browse = new System.Windows.Forms.Button();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle1 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle2 = new System.Windows.Forms.DataGridViewCellStyle();
            ((System.ComponentModel.ISupportInitialize)(this.dataGridView_tickets)).BeginInit();
            this.groupBox1.SuspendLayout();
            this.SuspendLayout();
            // 
            // dataGridView_tickets
            // 
            this.dataGridView_tickets.AllowUserToAddRows = false;
            this.dataGridView_tickets.AllowUserToDeleteRows = false;
            this.dataGridView_tickets.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.Fill;
            this.dataGridView_tickets.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            dataGridViewCellStyle1.BackColor = System.Drawing.Color.WhiteSmoke;
            this.dataGridView_tickets.AlternatingRowsDefaultCellStyle = dataGridViewCellStyle1;
            dataGridViewCellStyle2.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle2.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(41)))), ((int)(((byte)(128)))), ((int)(((byte)(185)))));
            dataGridViewCellStyle2.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Bold);
            dataGridViewCellStyle2.ForeColor = System.Drawing.Color.White;
            dataGridViewCellStyle2.SelectionBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(41)))), ((int)(((byte)(128)))), ((int)(((byte)(185)))));
            dataGridViewCellStyle2.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
            dataGridViewCellStyle2.WrapMode = System.Windows.Forms.DataGridViewTriState.True;
            this.dataGridView_tickets.ColumnHeadersDefaultCellStyle = dataGridViewCellStyle2;
            this.dataGridView_tickets.EnableHeadersVisualStyles = false;
            this.dataGridView_tickets.Dock = System.Windows.Forms.DockStyle.Top;
            this.dataGridView_tickets.Location = new System.Drawing.Point(0, 0);
            this.dataGridView_tickets.MultiSelect = false;
            this.dataGridView_tickets.Name = "dataGridView_tickets";
            this.dataGridView_tickets.ReadOnly = true;
            this.dataGridView_tickets.RowHeadersVisible = false;
            this.dataGridView_tickets.RowTemplate.Height = 27;
            this.dataGridView_tickets.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            this.dataGridView_tickets.Size = new System.Drawing.Size(800, 240);
            this.dataGridView_tickets.TabIndex = 0;
            this.dataGridView_tickets.SelectionChanged += new System.EventHandler(this.dataGridView_tickets_SelectionChanged);
            this.dataGridView_tickets.CellFormatting += new System.Windows.Forms.DataGridViewCellFormattingEventHandler(this.dataGridView_tickets_CellFormatting);
            //
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.bt_delete);
            this.groupBox1.Controls.Add(this.bt_update);
            this.groupBox1.Controls.Add(this.bt_add);
            this.groupBox1.Controls.Add(this.bt_search);
            this.groupBox1.Controls.Add(this.tb_search);
            this.groupBox1.Controls.Add(this.label4);
            this.groupBox1.Controls.Add(this.cb_status);
            this.groupBox1.Controls.Add(this.cb_priority);
            this.groupBox1.Controls.Add(this.tb_assignee);
            this.groupBox1.Controls.Add(this.label7);
            this.groupBox1.Controls.Add(this.label6);
            this.groupBox1.Controls.Add(this.label5);
            this.groupBox1.Controls.Add(this.bt_browse);
            this.groupBox1.Controls.Add(this.tb_attachments);
            this.groupBox1.Controls.Add(this.label8);
            this.groupBox1.Controls.Add(this.radioButton_created);
            this.groupBox1.Controls.Add(this.radioButton_finished);
            this.groupBox1.Controls.Add(this.radioButton_inProgress);
            this.groupBox1.Controls.Add(this.radioButton_pending);
            this.groupBox1.Controls.Add(this.label3);
            this.groupBox1.Controls.Add(this.tb_description);
            this.groupBox1.Controls.Add(this.label2);
            this.groupBox1.Controls.Add(this.tb_title);
            this.groupBox1.Controls.Add(this.label1);
            this.groupBox1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.groupBox1.Location = new System.Drawing.Point(0, 240);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(800, 210);
            this.groupBox1.TabIndex = 1;
            this.groupBox1.TabStop = false;
            this.groupBox1.Font = new System.Drawing.Font("Segoe UI", 9F);
            this.groupBox1.Text = "Ticket Details";
            // 
            // bt_delete
            // 
            this.bt_delete.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(231)))), ((int)(((byte)(76)))), ((int)(((byte)(60)))));
            this.bt_delete.FlatAppearance.BorderSize = 0;
            this.bt_delete.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.bt_delete.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Bold);
            this.bt_delete.ForeColor = System.Drawing.Color.White;
            this.bt_delete.Location = new System.Drawing.Point(357, 158);
            this.bt_delete.Name = "bt_delete";
            this.bt_delete.Size = new System.Drawing.Size(75, 30);
            this.bt_delete.TabIndex = 8;
            this.bt_delete.Text = "Delete";
            this.bt_delete.UseVisualStyleBackColor = false;
            this.bt_delete.Click += new System.EventHandler(this.bt_delete_Click);
            // 
            // bt_update
            // 
            this.bt_update.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(52)))), ((int)(((byte)(152)))), ((int)(((byte)(219)))));
            this.bt_update.FlatAppearance.BorderSize = 0;
            this.bt_update.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.bt_update.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Bold);
            this.bt_update.ForeColor = System.Drawing.Color.White;
            this.bt_update.Location = new System.Drawing.Point(276, 158);
            this.bt_update.Name = "bt_update";
            this.bt_update.Size = new System.Drawing.Size(75, 30);
            this.bt_update.TabIndex = 7;
            this.bt_update.Text = "Update";
            this.bt_update.UseVisualStyleBackColor = false;
            this.bt_update.Click += new System.EventHandler(this.bt_update_Click);
            // 
            // bt_add
            // 
            this.bt_add.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(46)))), ((int)(((byte)(204)))), ((int)(((byte)(113)))));
            this.bt_add.FlatAppearance.BorderSize = 0;
            this.bt_add.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.bt_add.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Bold);
            this.bt_add.ForeColor = System.Drawing.Color.White;
            this.bt_add.Location = new System.Drawing.Point(195, 158);
            this.bt_add.Name = "bt_add";
            this.bt_add.Size = new System.Drawing.Size(75, 30);
            this.bt_add.TabIndex = 6;
            this.bt_add.Text = "Add";
            this.bt_add.UseVisualStyleBackColor = false;
            this.bt_add.Click += new System.EventHandler(this.bt_add_Click);
            // 
            // bt_search
            //
            this.bt_search.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(52)))), ((int)(((byte)(152)))), ((int)(((byte)(219)))));
            this.bt_search.FlatAppearance.BorderSize = 0;
            this.bt_search.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.bt_search.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Bold);
            this.bt_search.ForeColor = System.Drawing.Color.White;
            this.bt_search.Location = new System.Drawing.Point(600, 30);
            this.bt_search.Name = "bt_search";
            this.bt_search.Size = new System.Drawing.Size(75, 30);
            this.bt_search.TabIndex = 9;
            this.bt_search.Text = "Search";
            this.bt_search.UseVisualStyleBackColor = false;
            this.bt_search.Click += new System.EventHandler(this.bt_search_Click);
            //
            // tb_search
            //
            this.tb_search.Location = new System.Drawing.Point(371, 34);
            this.tb_search.Name = "tb_search";
            this.tb_search.Size = new System.Drawing.Size(223, 25);
            this.tb_search.TabIndex = 8;
            //
            // label4
            //
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(320, 37);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(49, 15);
            this.label4.TabIndex = 7;
            this.label4.Text = "Search";
            //
            // label5
            //
            this.label5.AutoSize = true;
            this.label5.Location = new System.Drawing.Point(29, 150);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(55, 15);
            this.label5.TabIndex = 10;
            this.label5.Text = "Priority";
            //
            // label6
            //
            this.label6.AutoSize = true;
            this.label6.Location = new System.Drawing.Point(320, 77);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(60, 15);
            this.label6.TabIndex = 11;
            this.label6.Text = "Assignee";
            //
            // label7
            //
            this.label7.AutoSize = true;
            this.label7.Location = new System.Drawing.Point(320, 116);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(45, 15);
            this.label7.TabIndex = 12;
            this.label7.Text = "Status";
            //
            // cb_priority
            //
            this.cb_priority.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cb_priority.FormattingEnabled = true;
            this.cb_priority.Items.AddRange(new object[] {
            "Low",
            "Medium",
            "High"});
            this.cb_priority.Location = new System.Drawing.Point(103, 147);
            this.cb_priority.Name = "cb_priority";
            this.cb_priority.Size = new System.Drawing.Size(223, 23);
            this.cb_priority.TabIndex = 11;
            //
            // cb_status
            //
            this.cb_status.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cb_status.FormattingEnabled = true;
            this.cb_status.Items.AddRange(new object[] {
            "New",
            "InProgress",
            "Finished",
            "Closed"});
            this.cb_status.Location = new System.Drawing.Point(371, 113);
            this.cb_status.Name = "cb_status";
            this.cb_status.Size = new System.Drawing.Size(223, 23);
            this.cb_status.TabIndex = 13;
            //
            // tb_assignee
            //
            this.tb_assignee.Location = new System.Drawing.Point(371, 74);
            this.tb_assignee.Name = "tb_assignee";
            this.tb_assignee.Size = new System.Drawing.Size(223, 25);
            this.tb_assignee.TabIndex = 12;
            //
            // label8
            //
            this.label8.AutoSize = true;
            this.label8.Location = new System.Drawing.Point(29, 181);
            this.label8.Name = "label8";
            this.label8.Size = new System.Drawing.Size(73, 15);
            this.label8.TabIndex = 14;
            this.label8.Text = "Attachments";
            //
            // tb_attachments
            //
            this.tb_attachments.Location = new System.Drawing.Point(103, 178);
            this.tb_attachments.Name = "tb_attachments";
            this.tb_attachments.ReadOnly = true;
            this.tb_attachments.Size = new System.Drawing.Size(391, 25);
            this.tb_attachments.TabIndex = 15;
            //
            // bt_browse
            //
            this.bt_browse.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(52)))), ((int)(((byte)(152)))), ((int)(((byte)(219)))));
            this.bt_browse.FlatAppearance.BorderSize = 0;
            this.bt_browse.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.bt_browse.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Bold);
            this.bt_browse.ForeColor = System.Drawing.Color.White;
            this.bt_browse.Location = new System.Drawing.Point(500, 177);
            this.bt_browse.Name = "bt_browse";
            this.bt_browse.Size = new System.Drawing.Size(75, 27);
            this.bt_browse.TabIndex = 16;
            this.bt_browse.Text = "Browse";
            this.bt_browse.UseVisualStyleBackColor = false;
            this.bt_browse.Click += new System.EventHandler(this.bt_browse_Click);
            //
            // radioButton_pending
            //
            this.radioButton_pending.AutoSize = true;
            this.radioButton_pending.Location = new System.Drawing.Point(103, 116);
            this.radioButton_pending.Name = "radioButton_pending";
            this.radioButton_pending.Size = new System.Drawing.Size(78, 19);
            this.radioButton_pending.TabIndex = 5;
            this.radioButton_pending.TabStop = true;
            this.radioButton_pending.Text = "Pending";
            this.radioButton_pending.UseVisualStyleBackColor = true;
            //
            // radioButton_inProgress
            //
            this.radioButton_inProgress.AutoSize = true;
            this.radioButton_inProgress.Location = new System.Drawing.Point(187, 116);
            this.radioButton_inProgress.Name = "radioButton_inProgress";
            this.radioButton_inProgress.Size = new System.Drawing.Size(97, 19);
            this.radioButton_inProgress.TabIndex = 6;
            this.radioButton_inProgress.TabStop = true;
            this.radioButton_inProgress.Text = "InProgress";
            this.radioButton_inProgress.UseVisualStyleBackColor = true;
            //
            // radioButton_finished
            //
            this.radioButton_finished.AutoSize = true;
            this.radioButton_finished.Location = new System.Drawing.Point(290, 116);
            this.radioButton_finished.Name = "radioButton_finished";
            this.radioButton_finished.Size = new System.Drawing.Size(78, 19);
            this.radioButton_finished.TabIndex = 7;
            this.radioButton_finished.TabStop = true;
            this.radioButton_finished.Text = "Finished";
            this.radioButton_finished.UseVisualStyleBackColor = true;
            //
            // radioButton_created
            //
            this.radioButton_created.AutoSize = true;
            this.radioButton_created.Location = new System.Drawing.Point(374, 116);
            this.radioButton_created.Name = "radioButton_created";
            this.radioButton_created.Size = new System.Drawing.Size(71, 19);
            this.radioButton_created.TabIndex = 8;
            this.radioButton_created.TabStop = true;
            this.radioButton_created.Text = "Created";
            this.radioButton_created.UseVisualStyleBackColor = true;
            //
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(29, 119);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(55, 15);
            this.label3.TabIndex = 4;
            this.label3.Text = "Status";
            // 
            // tb_description
            // 
            this.tb_description.Location = new System.Drawing.Point(103, 74);
            this.tb_description.Name = "tb_description";
            this.tb_description.Size = new System.Drawing.Size(223, 25);
            this.tb_description.TabIndex = 3;
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(29, 77);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(79, 15);
            this.label2.TabIndex = 2;
            this.label2.Text = "Description";
            // 
            // tb_title
            // 
            this.tb_title.Location = new System.Drawing.Point(103, 32);
            this.tb_title.Name = "tb_title";
            this.tb_title.Size = new System.Drawing.Size(223, 25);
            this.tb_title.TabIndex = 1;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(29, 35);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(39, 15);
            this.label1.TabIndex = 0;
            this.label1.Text = "Title";
            // 
            // FormTicketRecord
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 15F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Font = new System.Drawing.Font("Segoe UI", 9F);
            this.ClientSize = new System.Drawing.Size(800, 450);
            this.Controls.Add(this.groupBox1);
            this.Controls.Add(this.dataGridView_tickets);
            this.Name = "FormTicketRecord";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Ticket Record";
            this.Load += new System.EventHandler(this.FormTicketRecord_Load);
            ((System.ComponentModel.ISupportInitialize)(this.dataGridView_tickets)).EndInit();
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            this.ResumeLayout(false);

        }

        private System.Windows.Forms.DataGridView dataGridView_tickets;
        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.Button bt_delete;
        private System.Windows.Forms.Button bt_update;
        private System.Windows.Forms.Button bt_add;
        private System.Windows.Forms.Button bt_search;
        private System.Windows.Forms.RadioButton radioButton_pending;
        private System.Windows.Forms.RadioButton radioButton_inProgress;
        private System.Windows.Forms.RadioButton radioButton_finished;
        private System.Windows.Forms.RadioButton radioButton_created;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.TextBox tb_description;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.TextBox tb_title;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.TextBox tb_search;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.Label label7;
        private System.Windows.Forms.ComboBox cb_priority;
        private System.Windows.Forms.ComboBox cb_status;
        private System.Windows.Forms.TextBox tb_assignee;
        private System.Windows.Forms.Label label8;
        private System.Windows.Forms.TextBox tb_attachments;
        private System.Windows.Forms.Button bt_browse;
    }
}
