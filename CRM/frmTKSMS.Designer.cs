namespace CRM
{
    partial class frmTKSMS
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
            this.labelX1 = new DevComponents.DotNetBar.LabelX();
            this.btnIn = new DevComponents.DotNetBar.ButtonX();
            this.cbCN = new DevComponents.DotNetBar.Controls.ComboBoxEx();
            this.optDN = new System.Windows.Forms.RadioButton();
            this.groupBox10 = new System.Windows.Forms.GroupBox();
            this.optCN = new System.Windows.Forms.RadioButton();
            this.buttonX2 = new DevComponents.DotNetBar.ButtonX();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.optCDT = new System.Windows.Forms.RadioButton();
            this.optDT = new System.Windows.Forms.RadioButton();
            this.groupBox10.SuspendLayout();
            this.groupBox1.SuspendLayout();
            this.SuspendLayout();
            // 
            // labelX1
            // 
            this.labelX1.Font = new System.Drawing.Font("Arial", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.labelX1.ForeColor = System.Drawing.SystemColors.ControlText;
            this.labelX1.Location = new System.Drawing.Point(29, 14);
            this.labelX1.Name = "labelX1";
            this.labelX1.Size = new System.Drawing.Size(87, 20);
            this.labelX1.TabIndex = 364;
            this.labelX1.Text = "Chi nhánh:";
            // 
            // btnIn
            // 
            this.btnIn.AccessibleRole = System.Windows.Forms.AccessibleRole.PushButton;
            this.btnIn.Location = new System.Drawing.Point(287, 153);
            this.btnIn.Name = "btnIn";
            this.btnIn.Size = new System.Drawing.Size(97, 31);
            this.btnIn.TabIndex = 361;
            this.btnIn.Text = "In";
            this.btnIn.Click += new System.EventHandler(this.btnIn_Click);
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
            this.cbCN.Location = new System.Drawing.Point(131, 12);
            this.cbCN.Name = "cbCN";
            this.cbCN.Size = new System.Drawing.Size(366, 25);
            this.cbCN.TabIndex = 365;
            // 
            // optDN
            // 
            this.optDN.AutoSize = true;
            this.optDN.ForeColor = System.Drawing.SystemColors.ActiveCaption;
            this.optDN.Location = new System.Drawing.Point(96, 13);
            this.optDN.Name = "optDN";
            this.optDN.Size = new System.Drawing.Size(127, 24);
            this.optDN.TabIndex = 14;
            this.optDN.Text = "Doanh nghiệp";
            this.optDN.UseVisualStyleBackColor = true;
            this.optDN.CheckedChanged += new System.EventHandler(this.optDN_CheckedChanged);
            // 
            // groupBox10
            // 
            this.groupBox10.Controls.Add(this.optDN);
            this.groupBox10.Controls.Add(this.optCN);
            this.groupBox10.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.groupBox10.Location = new System.Drawing.Point(131, 43);
            this.groupBox10.Name = "groupBox10";
            this.groupBox10.Size = new System.Drawing.Size(229, 47);
            this.groupBox10.TabIndex = 379;
            this.groupBox10.TabStop = false;
            // 
            // optCN
            // 
            this.optCN.AutoSize = true;
            this.optCN.Checked = true;
            this.optCN.ForeColor = System.Drawing.SystemColors.ActiveCaption;
            this.optCN.Location = new System.Drawing.Point(6, 13);
            this.optCN.Name = "optCN";
            this.optCN.Size = new System.Drawing.Size(87, 24);
            this.optCN.TabIndex = 13;
            this.optCN.TabStop = true;
            this.optCN.Text = "Cá nhân";
            this.optCN.UseVisualStyleBackColor = true;
            this.optCN.CheckedChanged += new System.EventHandler(this.optCN_CheckedChanged);
            // 
            // buttonX2
            // 
            this.buttonX2.AccessibleRole = System.Windows.Forms.AccessibleRole.PushButton;
            this.buttonX2.Font = new System.Drawing.Font("Arial", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.buttonX2.Location = new System.Drawing.Point(178, 153);
            this.buttonX2.Name = "buttonX2";
            this.buttonX2.Size = new System.Drawing.Size(93, 31);
            this.buttonX2.TabIndex = 380;
            this.buttonX2.Text = "Tạo báo cáo";
            this.buttonX2.Click += new System.EventHandler(this.buttonX2_Click);
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.optCDT);
            this.groupBox1.Controls.Add(this.optDT);
            this.groupBox1.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.groupBox1.Location = new System.Drawing.Point(74, 96);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(436, 51);
            this.groupBox1.TabIndex = 381;
            this.groupBox1.TabStop = false;
            // 
            // optCDT
            // 
            this.optCDT.AutoSize = true;
            this.optCDT.ForeColor = System.Drawing.SystemColors.ActiveCaption;
            this.optCDT.Location = new System.Drawing.Point(213, 13);
            this.optCDT.Name = "optCDT";
            this.optCDT.Size = new System.Drawing.Size(218, 24);
            this.optCDT.TabIndex = 14;
            this.optCDT.Text = "Chưa đăng ký số điện thoại";
            this.optCDT.UseVisualStyleBackColor = true;
            this.optCDT.CheckedChanged += new System.EventHandler(this.optCDT_CheckedChanged);
            // 
            // optDT
            // 
            this.optDT.AutoSize = true;
            this.optDT.Checked = true;
            this.optDT.ForeColor = System.Drawing.SystemColors.ActiveCaption;
            this.optDT.Location = new System.Drawing.Point(6, 13);
            this.optDT.Name = "optDT";
            this.optDT.Size = new System.Drawing.Size(201, 24);
            this.optDT.TabIndex = 13;
            this.optDT.TabStop = true;
            this.optDT.Text = "Đã đăng ký số điện thoại";
            this.optDT.UseVisualStyleBackColor = true;
            this.optDT.CheckedChanged += new System.EventHandler(this.optDT_CheckedChanged);
            // 
            // frmTKSMS
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(542, 192);
            this.Controls.Add(this.groupBox1);
            this.Controls.Add(this.buttonX2);
            this.Controls.Add(this.groupBox10);
            this.Controls.Add(this.cbCN);
            this.Controls.Add(this.labelX1);
            this.Controls.Add(this.btnIn);
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "frmTKSMS";
            this.ShowIcon = false;
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Thống kê khách hàng có tài khoản thanh toán chưa sử dụng dịch vụ SMS";
            this.Load += new System.EventHandler(this.frmTKSMS_Load);
            this.groupBox10.ResumeLayout(false);
            this.groupBox10.PerformLayout();
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private DevComponents.DotNetBar.LabelX labelX1;
        private DevComponents.DotNetBar.ButtonX btnIn;
        private DevComponents.DotNetBar.Controls.ComboBoxEx cbCN;
        private System.Windows.Forms.RadioButton optDN;
        private System.Windows.Forms.GroupBox groupBox10;
        private System.Windows.Forms.RadioButton optCN;
        private DevComponents.DotNetBar.ButtonX buttonX2;
        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.RadioButton optCDT;
        private System.Windows.Forms.RadioButton optDT;
    }
}