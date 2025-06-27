namespace PDMS
{
    partial class FormSelection
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
            this.tb_selectFilter = new System.Windows.Forms.TextBox();
            this.bt_cancel = new System.Windows.Forms.Button();
            this.flowLayoutPanel1 = new System.Windows.Forms.FlowLayoutPanel();
            this.SuspendLayout();
            // 
            // tb_selectFilter
            // 
            this.tb_selectFilter.Dock = System.Windows.Forms.DockStyle.Top;
            this.tb_selectFilter.Font = new System.Drawing.Font("微软雅黑", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.tb_selectFilter.Location = new System.Drawing.Point(0, 0);
            this.tb_selectFilter.Name = "tb_selectFilter";
            this.tb_selectFilter.Size = new System.Drawing.Size(740, 34);
            this.tb_selectFilter.TabIndex = 0;
            this.tb_selectFilter.TextChanged += new System.EventHandler(this.tb_selectFilter_TextChanged);
            // 
            // bt_cancel
            // 
            this.bt_cancel.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.bt_cancel.Font = new System.Drawing.Font("微软雅黑", 15F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.bt_cancel.Location = new System.Drawing.Point(0, 868);
            this.bt_cancel.Name = "bt_cancel";
            this.bt_cancel.Size = new System.Drawing.Size(740, 67);
            this.bt_cancel.TabIndex = 1;
            this.bt_cancel.Text = "取消";
            this.bt_cancel.UseVisualStyleBackColor = true;
            this.bt_cancel.Click += new System.EventHandler(this.bt_cancel_Click);
            // 
            // flowLayoutPanel1
            // 
            this.flowLayoutPanel1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.flowLayoutPanel1.Font = new System.Drawing.Font("微软雅黑", 10.8F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.flowLayoutPanel1.Location = new System.Drawing.Point(0, 34);
            this.flowLayoutPanel1.Name = "flowLayoutPanel1";
            this.flowLayoutPanel1.Size = new System.Drawing.Size(740, 834);
            this.flowLayoutPanel1.TabIndex = 2;
            // 
            // FormSelection
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 15F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(740, 935);
            this.Controls.Add(this.flowLayoutPanel1);
            this.Controls.Add(this.bt_cancel);
            this.Controls.Add(this.tb_selectFilter);
            this.Name = "FormSelection";
            this.Text = "请选择";
            this.Load += new System.EventHandler(this.FormSelection_Load);
            this.SizeChanged += new System.EventHandler(this.SelectForm_SizeChanged);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.TextBox tb_selectFilter;
        private System.Windows.Forms.Button bt_cancel;
        private System.Windows.Forms.FlowLayoutPanel flowLayoutPanel1;
    }
}