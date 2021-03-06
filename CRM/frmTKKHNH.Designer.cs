namespace CRM
{
    partial class frmTKKHNH
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
            this.cbCN = new DevComponents.DotNetBar.Controls.ComboBoxEx();
            this.labelX1 = new DevComponents.DotNetBar.LabelX();
            this.label1 = new System.Windows.Forms.Label();
            this.dtpTo = new System.Windows.Forms.DateTimePicker();
            this.labelX2 = new DevComponents.DotNetBar.LabelX();
            this.btnIn = new DevComponents.DotNetBar.ButtonX();
            this.dtpFrom = new System.Windows.Forms.DateTimePicker();
            this.groupBox2 = new System.Windows.Forms.GroupBox();
            this.optKhac = new System.Windows.Forms.RadioButton();
            this.optWU = new System.Windows.Forms.RadioButton();
            this.optTatca = new System.Windows.Forms.RadioButton();
            this.groupBox2.SuspendLayout();
            this.SuspendLayout();
            // 
            // cbCN
            // 
            this.cbCN.DisplayMember = "Text";
            this.cbCN.DrawMode = System.Windows.Forms.DrawMode.OwnerDrawFixed;
            this.cbCN.DropDownHeight = 200;
            this.cbCN.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.cbCN.FormattingEnabled = true;
            this.cbCN.IntegralHeight = false;
            this.cbCN.ItemHeight = 19;
            this.cbCN.Location = new System.Drawing.Point(105, 7);
            this.cbCN.Name = "cbCN";
            this.cbCN.Size = new System.Drawing.Size(380, 25);
            this.cbCN.TabIndex = 391;
            // 
            // labelX1
            // 
            this.labelX1.Font = new System.Drawing.Font("Arial", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.labelX1.ForeColor = System.Drawing.SystemColors.ControlText;
            this.labelX1.Location = new System.Drawing.Point(12, 12);
            this.labelX1.Name = "labelX1";
            this.labelX1.Size = new System.Drawing.Size(87, 20);
            this.labelX1.TabIndex = 390;
            this.labelX1.Text = "Chi nhánh";
            this.labelX1.TextAlignment = System.Drawing.StringAlignment.Far;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(204, 54);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(10, 13);
            this.label1.TabIndex = 387;
            this.label1.Text = "-";
            // 
            // dtpTo
            // 
            this.dtpTo.CustomFormat = "MM/yyyy";
            this.dtpTo.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.dtpTo.Format = System.Windows.Forms.DateTimePickerFormat.Custom;
            this.dtpTo.Location = new System.Drawing.Point(220, 47);
            this.dtpTo.Name = "dtpTo";
            this.dtpTo.ShowUpDown = true;
            this.dtpTo.Size = new System.Drawing.Size(90, 26);
            this.dtpTo.TabIndex = 386;
            // 
            // labelX2
            // 
            this.labelX2.Font = new System.Drawing.Font("Arial", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.labelX2.ForeColor = System.Drawing.SystemColors.ControlText;
            this.labelX2.Location = new System.Drawing.Point(46, 50);
            this.labelX2.Name = "labelX2";
            this.labelX2.Size = new System.Drawing.Size(53, 20);
            this.labelX2.TabIndex = 389;
            this.labelX2.Text = "Tháng";
            this.labelX2.TextAlignment = System.Drawing.StringAlignment.Far;
            // 
            // btnIn
            // 
            this.btnIn.AccessibleRole = System.Windows.Forms.AccessibleRole.PushButton;
            this.btnIn.Location = new System.Drawing.Point(198, 137);
            this.btnIn.Name = "btnIn";
            this.btnIn.Size = new System.Drawing.Size(97, 31);
            this.btnIn.TabIndex = 388;
            this.btnIn.Text = "In";
            this.btnIn.Click += new System.EventHandler(this.btnIn_Click);
            // 
            // dtpFrom
            // 
            this.dtpFrom.CustomFormat = "MM/yyyy";
            this.dtpFrom.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.dtpFrom.Format = System.Windows.Forms.DateTimePickerFormat.Custom;
            this.dtpFrom.Location = new System.Drawing.Point(105, 47);
            this.dtpFrom.Name = "dtpFrom";
            this.dtpFrom.ShowUpDown = true;
            this.dtpFrom.Size = new System.Drawing.Size(90, 26);
            this.dtpFrom.TabIndex = 385;
            // 
            // groupBox2
            // 
            this.groupBox2.Controls.Add(this.optKhac);
            this.groupBox2.Controls.Add(this.optWU);
            this.groupBox2.Controls.Add(this.optTatca);
            this.groupBox2.Location = new System.Drawing.Point(122, 79);
            this.groupBox2.Name = "groupBox2";
            this.groupBox2.Size = new System.Drawing.Size(252, 52);
            this.groupBox2.TabIndex = 392;
            this.groupBox2.TabStop = false;
            // 
            // optKhac
            // 
            this.optKhac.AutoSize = true;
            this.optKhac.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.optKhac.ForeColor = System.Drawing.SystemColors.ActiveCaption;
            this.optKhac.Location = new System.Drawing.Point(140, 19);
            this.optKhac.Name = "optKhac";
            this.optKhac.Size = new System.Drawing.Size(105, 20);
            this.optKhac.TabIndex = 16;
            this.optKhac.Text = "Kiều hối khác";
            this.optKhac.UseVisualStyleBackColor = true;
            // 
            // optWU
            // 
            this.optWU.AutoSize = true;
            this.optWU.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.optWU.ForeColor = System.Drawing.SystemColors.ActiveCaption;
            this.optWU.Location = new System.Drawing.Point(85, 18);
            this.optWU.Name = "optWU";
            this.optWU.Size = new System.Drawing.Size(49, 20);
            this.optWU.TabIndex = 14;
            this.optWU.Text = "WU";
            this.optWU.UseVisualStyleBackColor = true;
            // 
            // optTatca
            // 
            this.optTatca.AutoSize = true;
            this.optTatca.Checked = true;
            this.optTatca.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.optTatca.ForeColor = System.Drawing.SystemColors.ActiveCaption;
            this.optTatca.Location = new System.Drawing.Point(15, 19);
            this.optTatca.Name = "optTatca";
            this.optTatca.Size = new System.Drawing.Size(64, 20);
            this.optTatca.TabIndex = 13;
            this.optTatca.TabStop = true;
            this.optTatca.Text = "Tất cả";
            this.optTatca.UseVisualStyleBackColor = true;
            // 
            // frmTKKHNH
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(497, 176);
            this.Controls.Add(this.groupBox2);
            this.Controls.Add(this.cbCN);
            this.Controls.Add(this.labelX1);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.dtpTo);
            this.Controls.Add(this.labelX2);
            this.Controls.Add(this.btnIn);
            this.Controls.Add(this.dtpFrom);
            this.Name = "frmTKKHNH";
            this.ShowIcon = false;
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Thống kê tổng hợp khách hàng kiều hối";
            this.Load += new System.EventHandler(this.frmTKKHNH_Load);
            this.groupBox2.ResumeLayout(false);
            this.groupBox2.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private DevComponents.DotNetBar.Controls.ComboBoxEx cbCN;
        private DevComponents.DotNetBar.LabelX labelX1;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.DateTimePicker dtpTo;
        private DevComponents.DotNetBar.LabelX labelX2;
        private DevComponents.DotNetBar.ButtonX btnIn;
        private System.Windows.Forms.DateTimePicker dtpFrom;
        private System.Windows.Forms.GroupBox groupBox2;
        private System.Windows.Forms.RadioButton optKhac;
        private System.Windows.Forms.RadioButton optWU;
        private System.Windows.Forms.RadioButton optTatca;
    }
}