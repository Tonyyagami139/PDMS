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
            this.label3 = new System.Windows.Forms.Label();
            this.tb_description = new System.Windows.Forms.TextBox();
            this.label2 = new System.Windows.Forms.Label();
            this.tb_title = new System.Windows.Forms.TextBox();
            this.label1 = new System.Windows.Forms.Label();
            this.tb_search = new System.Windows.Forms.TextBox();
            this.label4 = new System.Windows.Forms.Label();
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
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.bt_delete);
            this.groupBox1.Controls.Add(this.bt_update);
            this.groupBox1.Controls.Add(this.bt_add);
            this.groupBox1.Controls.Add(this.bt_search);
            this.groupBox1.Controls.Add(this.tb_search);
            this.groupBox1.Controls.Add(this.label4);
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
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.TextBox tb_description;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.TextBox tb_title;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.TextBox tb_search;
        private System.Windows.Forms.Label label4;
    }
}
