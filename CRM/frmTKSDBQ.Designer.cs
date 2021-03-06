namespace CRM
{
    partial class frmTKSDBQ
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
            this.label1 = new System.Windows.Forms.Label();
            this.dtpTo = new System.Windows.Forms.DateTimePicker();
            this.cbbChinhanh = new DevComponents.DotNetBar.Controls.ComboBoxEx();
            this.labelX2 = new DevComponents.DotNetBar.LabelX();
            this.btnCancel = new DevComponents.DotNetBar.ButtonX();
            this.btnIn = new DevComponents.DotNetBar.ButtonX();
            this.dtpFrom = new System.Windows.Forms.DateTimePicker();
            this.groupBox10 = new System.Windows.Forms.GroupBox();
            this.rdbDN = new System.Windows.Forms.RadioButton();
            this.rdbCN = new System.Windows.Forms.RadioButton();
            this.labelX3 = new DevComponents.DotNetBar.LabelX();
            this.txtFrom = new DevComponents.DotNetBar.Controls.TextBoxX();
            this.txtTo = new DevComponents.DotNetBar.Controls.TextBoxX();
            this.labelX4 = new DevComponents.DotNetBar.LabelX();
            this.label2 = new System.Windows.Forms.Label();
            this.txtMaKH = new DevComponents.DotNetBar.Controls.TextBoxX();
            this.labelX5 = new DevComponents.DotNetBar.LabelX();
            this.chkMaKH = new System.Windows.Forms.CheckBox();
            this.groupBox10.SuspendLayout();
            this.SuspendLayout();
            // 
            // labelX1
            // 
            this.labelX1.Font = new System.Drawing.Font("Arial", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.labelX1.ForeColor = System.Drawing.SystemColors.ControlText;
            this.labelX1.Location = new System.Drawing.Point(28, 17);
            this.labelX1.Name = "labelX1";
            this.labelX1.Size = new System.Drawing.Size(87, 20);
            this.labelX1.TabIndex = 376;
            this.labelX1.Text = "Chi nhánh";
            this.labelX1.TextAlignment = System.Drawing.StringAlignment.Far;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(245, 92);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(13, 18);
            this.label1.TabIndex = 371;
            this.label1.Text = "-";
            // 
            // dtpTo
            // 
            this.dtpTo.CustomFormat = "MM/yyyy";
            this.dtpTo.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.dtpTo.Format = System.Windows.Forms.DateTimePickerFormat.Custom;
            this.dtpTo.Location = new System.Drawing.Point(283, 87);
            this.dtpTo.Name = "dtpTo";
            this.dtpTo.ShowUpDown = true;
            this.dtpTo.Size = new System.Drawing.Size(90, 26);
            this.dtpTo.TabIndex = 370;
            // 
            // cbbChinhanh
            // 
            this.cbbChinhanh.DisplayMember = "Text";
            this.cbbChinhanh.DrawMode = System.Windows.Forms.DrawMode.OwnerDrawFixed;
            this.cbbChinhanh.DropDownHeight = 100;
            this.cbbChinhanh.DropDownWidth = 99;
            this.cbbChinhanh.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.cbbChinhanh.FormattingEnabled = true;
            this.cbbChinhanh.IntegralHeight = false;
            this.cbbChinhanh.ItemHeight = 19;
            this.cbbChinhanh.Location = new System.Drawing.Point(130, 12);
            this.cbbChinhanh.Name = "cbbChinhanh";
            this.cbbChinhanh.Size = new System.Drawing.Size(397, 25);
            this.cbbChinhanh.TabIndex = 375;
            // 
            // labelX2
            // 
            this.labelX2.Font = new System.Drawing.Font("Arial", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.labelX2.ForeColor = System.Drawing.SystemColors.ControlText;
            this.labelX2.Location = new System.Drawing.Point(62, 93);
            this.labelX2.Name = "labelX2";
            this.labelX2.Size = new System.Drawing.Size(53, 20);
            this.labelX2.TabIndex = 374;
            this.labelX2.Text = "Tháng";
            this.labelX2.TextAlignment = System.Drawing.StringAlignment.Far;
            // 
            // btnCancel
            // 
            this.btnCancel.AccessibleRole = System.Windows.Forms.AccessibleRole.PushButton;
            this.btnCancel.Location = new System.Drawing.Point(314, 192);
            this.btnCancel.Name = "btnCancel";
            this.btnCancel.Size = new System.Drawing.Size(97, 31);
            this.btnCancel.TabIndex = 373;
            this.btnCancel.Text = "Đóng";
            this.btnCancel.Click += new System.EventHandler(this.btnCancel_Click);
            // 
            // btnIn
            // 
            this.btnIn.AccessibleRole = System.Windows.Forms.AccessibleRole.PushButton;
            this.btnIn.Location = new System.Drawing.Point(211, 192);
            this.btnIn.Name = "btnIn";
            this.btnIn.Size = new System.Drawing.Size(97, 31);
            this.btnIn.TabIndex = 372;
            this.btnIn.Text = "In";
            this.btnIn.Click += new System.EventHandler(this.btnIn_Click);
            // 
            // dtpFrom
            // 
            this.dtpFrom.CustomFormat = "MM/yyyy";
            this.dtpFrom.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.dtpFrom.Format = System.Windows.Forms.DateTimePickerFormat.Custom;
            this.dtpFrom.Location = new System.Drawing.Point(130, 87);
            this.dtpFrom.Name = "dtpFrom";
            this.dtpFrom.ShowUpDown = true;
            this.dtpFrom.Size = new System.Drawing.Size(90, 26);
            this.dtpFrom.TabIndex = 369;
            // 
            // groupBox10
            // 
            this.groupBox10.Controls.Add(this.rdbDN);
            this.groupBox10.Controls.Add(this.rdbCN);
            this.groupBox10.Location = new System.Drawing.Point(130, 43);
            this.groupBox10.Name = "groupBox10";
            this.groupBox10.Size = new System.Drawing.Size(243, 38);
            this.groupBox10.TabIndex = 377;
            this.groupBox10.TabStop = false;
            // 
            // rdbDN
            // 
            this.rdbDN.AutoSize = true;
            this.rdbDN.ForeColor = System.Drawing.SystemColors.ActiveCaption;
            this.rdbDN.Location = new System.Drawing.Point(102, 13);
            this.rdbDN.Name = "rdbDN";
            this.rdbDN.Size = new System.Drawing.Size(123, 22);
            this.rdbDN.TabIndex = 14;
            this.rdbDN.Text = "Doanh nghiệp";
            this.rdbDN.UseVisualStyleBackColor = true;
            // 
            // rdbCN
            // 
            this.rdbCN.AutoSize = true;
            this.rdbCN.Checked = true;
            this.rdbCN.ForeColor = System.Drawing.SystemColors.ActiveCaption;
            this.rdbCN.Location = new System.Drawing.Point(6, 13);
            this.rdbCN.Name = "rdbCN";
            this.rdbCN.Size = new System.Drawing.Size(84, 22);
            this.rdbCN.TabIndex = 13;
            this.rdbCN.TabStop = true;
            this.rdbCN.Text = "Cá nhân";
            this.rdbCN.UseVisualStyleBackColor = true;
            // 
            // labelX3
            // 
            this.labelX3.Font = new System.Drawing.Font("Arial", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.labelX3.ForeColor = System.Drawing.SystemColors.ControlText;
            this.labelX3.Location = new System.Drawing.Point(28, 58);
            this.labelX3.Name = "labelX3";
            this.labelX3.Size = new System.Drawing.Size(87, 20);
            this.labelX3.TabIndex = 376;
            this.labelX3.Text = "Loại KH";
            this.labelX3.TextAlignment = System.Drawing.StringAlignment.Far;
            // 
            // txtFrom
            // 
            // 
            // 
            // 
            this.txtFrom.Border.Class = "TextBoxBorder";
            this.txtFrom.Font = new System.Drawing.Font("Arial", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtFrom.Location = new System.Drawing.Point(130, 119);
            this.txtFrom.Name = "txtFrom";
            this.txtFrom.Size = new System.Drawing.Size(169, 26);
            this.txtFrom.TabIndex = 378;
            this.txtFrom.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            this.txtFrom.TextChanged += new System.EventHandler(this.txtFrom_TextChanged);
            this.txtFrom.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.txtFrom_KeyPress);
            // 
            // txtTo
            // 
            // 
            // 
            // 
            this.txtTo.Border.Class = "TextBoxBorder";
            this.txtTo.Font = new System.Drawing.Font("Arial", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtTo.Location = new System.Drawing.Point(358, 119);
            this.txtTo.Name = "txtTo";
            this.txtTo.Size = new System.Drawing.Size(169, 26);
            this.txtTo.TabIndex = 379;
            this.txtTo.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            this.txtTo.TextChanged += new System.EventHandler(this.txtTo_TextChanged);
            this.txtTo.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.txtTo_KeyPress);
            // 
            // labelX4
            // 
            this.labelX4.Font = new System.Drawing.Font("Arial", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.labelX4.ForeColor = System.Drawing.SystemColors.ControlText;
            this.labelX4.Location = new System.Drawing.Point(28, 125);
            this.labelX4.Name = "labelX4";
            this.labelX4.Size = new System.Drawing.Size(87, 20);
            this.labelX4.TabIndex = 376;
            this.labelX4.Text = "Số dư BQ";
            this.labelX4.TextAlignment = System.Drawing.StringAlignment.Far;
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(323, 125);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(13, 18);
            this.label2.TabIndex = 371;
            this.label2.Text = "-";
            // 
            // txtMaKH
            // 
            // 
            // 
            // 
            this.txtMaKH.Border.Class = "TextBoxBorder";
            this.txtMaKH.Enabled = false;
            this.txtMaKH.Font = new System.Drawing.Font("Arial", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtMaKH.Location = new System.Drawing.Point(151, 151);
            this.txtMaKH.Name = "txtMaKH";
            this.txtMaKH.Size = new System.Drawing.Size(376, 26);
            this.txtMaKH.TabIndex = 380;
            this.txtMaKH.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            // 
            // labelX5
            // 
            this.labelX5.Font = new System.Drawing.Font("Arial", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.labelX5.ForeColor = System.Drawing.SystemColors.ControlText;
            this.labelX5.Location = new System.Drawing.Point(53, 157);
            this.labelX5.Name = "labelX5";
            this.labelX5.Size = new System.Drawing.Size(62, 20);
            this.labelX5.TabIndex = 381;
            this.labelX5.Text = "Mã KH";
            this.labelX5.TextAlignment = System.Drawing.StringAlignment.Far;
            // 
            // chkMaKH
            // 
            this.chkMaKH.AutoSize = true;
            this.chkMaKH.Location = new System.Drawing.Point(130, 161);
            this.chkMaKH.Name = "chkMaKH";
            this.chkMaKH.Size = new System.Drawing.Size(15, 14);
            this.chkMaKH.TabIndex = 382;
            this.chkMaKH.UseVisualStyleBackColor = true;
            this.chkMaKH.CheckedChanged += new System.EventHandler(this.chkMaKH_CheckedChanged);
            // 
            // frmTKSDBQ
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(9F, 18F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(552, 235);
            this.Controls.Add(this.chkMaKH);
            this.Controls.Add(this.labelX5);
            this.Controls.Add(this.txtMaKH);
            this.Controls.Add(this.txtTo);
            this.Controls.Add(this.txtFrom);
            this.Controls.Add(this.labelX4);
            this.Controls.Add(this.labelX3);
            this.Controls.Add(this.labelX1);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.dtpTo);
            this.Controls.Add(this.cbbChinhanh);
            this.Controls.Add(this.labelX2);
            this.Controls.Add(this.btnCancel);
            this.Controls.Add(this.btnIn);
            this.Controls.Add(this.dtpFrom);
            this.Controls.Add(this.groupBox10);
            this.Font = new System.Drawing.Font("Arial", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.Margin = new System.Windows.Forms.Padding(4);
            this.Name = "frmTKSDBQ";
            this.ShowIcon = false;
            this.Text = "Thống kê số dư bình quân khách hàng";
            this.Load += new System.EventHandler(this.frmTKSDBQ_Load);
            this.groupBox10.ResumeLayout(false);
            this.groupBox10.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private DevComponents.DotNetBar.LabelX labelX1;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.DateTimePicker dtpTo;
        private DevComponents.DotNetBar.Controls.ComboBoxEx cbbChinhanh;
        private DevComponents.DotNetBar.LabelX labelX2;
        private DevComponents.DotNetBar.ButtonX btnCancel;
        private DevComponents.DotNetBar.ButtonX btnIn;
        private System.Windows.Forms.DateTimePicker dtpFrom;
        private System.Windows.Forms.GroupBox groupBox10;
        private System.Windows.Forms.RadioButton rdbDN;
        private System.Windows.Forms.RadioButton rdbCN;
        private DevComponents.DotNetBar.LabelX labelX3;
        private DevComponents.DotNetBar.Controls.TextBoxX txtFrom;
        private DevComponents.DotNetBar.Controls.TextBoxX txtTo;
        private DevComponents.DotNetBar.LabelX labelX4;
        private System.Windows.Forms.Label label2;
        private DevComponents.DotNetBar.Controls.TextBoxX txtMaKH;
        private DevComponents.DotNetBar.LabelX labelX5;
        private System.Windows.Forms.CheckBox chkMaKH;
    }
}