namespace CRM
{
    partial class frmHH_TKXLKH
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
            this.groupBox10 = new System.Windows.Forms.GroupBox();
            this.optDN = new System.Windows.Forms.RadioButton();
            this.optCN = new System.Windows.Forms.RadioButton();
            this.label1 = new System.Windows.Forms.Label();
            this.dtpTo = new System.Windows.Forms.DateTimePicker();
            this.dtpFrom = new System.Windows.Forms.DateTimePicker();
            this.groupBox2 = new System.Windows.Forms.GroupBox();
            this.optTatca = new System.Windows.Forms.RadioButton();
            this.optDTDL = new System.Windows.Forms.RadioButton();
            this.optDinhtinh = new System.Windows.Forms.RadioButton();
            this.optDinhluong = new System.Windows.Forms.RadioButton();
            this.groupBox3 = new System.Windows.Forms.GroupBox();
            this.optChuapheduyet = new System.Windows.Forms.RadioButton();
            this.optPheduyet = new System.Windows.Forms.RadioButton();
            this.labelX5 = new DevComponents.DotNetBar.LabelX();
            this.cbLoaiKH = new DevComponents.DotNetBar.Controls.ComboBoxEx();
            this.btnCancel = new DevComponents.DotNetBar.ButtonX();
            this.btnIn = new DevComponents.DotNetBar.ButtonX();
            this.labelX2 = new DevComponents.DotNetBar.LabelX();
            this.lblTenloai = new DevComponents.DotNetBar.LabelX();
            this.labelX1 = new DevComponents.DotNetBar.LabelX();
            this.cbChiNhanh = new DevComponents.DotNetBar.Controls.ComboBoxEx();
            this.groupBox10.SuspendLayout();
            this.groupBox2.SuspendLayout();
            this.groupBox3.SuspendLayout();
            this.SuspendLayout();
            // 
            // groupBox10
            // 
            this.groupBox10.Controls.Add(this.optDN);
            this.groupBox10.Controls.Add(this.optCN);
            this.groupBox10.Location = new System.Drawing.Point(12, 81);
            this.groupBox10.Name = "groupBox10";
            this.groupBox10.Size = new System.Drawing.Size(215, 44);
            this.groupBox10.TabIndex = 213;
            this.groupBox10.TabStop = false;
            // 
            // optDN
            // 
            this.optDN.AutoSize = true;
            this.optDN.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.optDN.ForeColor = System.Drawing.SystemColors.ActiveCaption;
            this.optDN.Location = new System.Drawing.Point(96, 17);
            this.optDN.Name = "optDN";
            this.optDN.Size = new System.Drawing.Size(110, 20);
            this.optDN.TabIndex = 14;
            this.optDN.Text = "Doanh nghiệp";
            this.optDN.UseVisualStyleBackColor = true;
            this.optDN.CheckedChanged += new System.EventHandler(this.optDN_CheckedChanged);
            // 
            // optCN
            // 
            this.optCN.AutoSize = true;
            this.optCN.Checked = true;
            this.optCN.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.optCN.ForeColor = System.Drawing.SystemColors.ActiveCaption;
            this.optCN.Location = new System.Drawing.Point(15, 17);
            this.optCN.Name = "optCN";
            this.optCN.Size = new System.Drawing.Size(75, 20);
            this.optCN.TabIndex = 13;
            this.optCN.TabStop = true;
            this.optCN.Text = "Cá nhân";
            this.optCN.UseVisualStyleBackColor = true;
            this.optCN.CheckedChanged += new System.EventHandler(this.optCN_CheckedChanged);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(206, 52);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(10, 13);
            this.label1.TabIndex = 239;
            this.label1.Text = "-";
            // 
            // dtpTo
            // 
            this.dtpTo.CustomFormat = "MM/yyyy";
            this.dtpTo.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.dtpTo.Format = System.Windows.Forms.DateTimePickerFormat.Custom;
            this.dtpTo.Location = new System.Drawing.Point(235, 48);
            this.dtpTo.Name = "dtpTo";
            this.dtpTo.ShowUpDown = true;
            this.dtpTo.Size = new System.Drawing.Size(95, 22);
            this.dtpTo.TabIndex = 238;
            this.dtpTo.Value = new System.DateTime(2013, 1, 1, 0, 0, 0, 0);
            // 
            // dtpFrom
            // 
            this.dtpFrom.CustomFormat = "MM/yyyy";
            this.dtpFrom.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.dtpFrom.Format = System.Windows.Forms.DateTimePickerFormat.Custom;
            this.dtpFrom.Location = new System.Drawing.Point(105, 49);
            this.dtpFrom.Name = "dtpFrom";
            this.dtpFrom.ShowUpDown = true;
            this.dtpFrom.Size = new System.Drawing.Size(95, 22);
            this.dtpFrom.TabIndex = 237;
            this.dtpFrom.Value = new System.DateTime(2013, 1, 1, 0, 0, 0, 0);
            // 
            // groupBox2
            // 
            this.groupBox2.Controls.Add(this.optTatca);
            this.groupBox2.Controls.Add(this.optDTDL);
            this.groupBox2.Controls.Add(this.optDinhtinh);
            this.groupBox2.Controls.Add(this.optDinhluong);
            this.groupBox2.Location = new System.Drawing.Point(235, 81);
            this.groupBox2.Name = "groupBox2";
            this.groupBox2.Size = new System.Drawing.Size(411, 44);
            this.groupBox2.TabIndex = 241;
            this.groupBox2.TabStop = false;
            // 
            // optTatca
            // 
            this.optTatca.AutoSize = true;
            this.optTatca.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.optTatca.ForeColor = System.Drawing.SystemColors.ActiveCaption;
            this.optTatca.Location = new System.Drawing.Point(341, 17);
            this.optTatca.Name = "optTatca";
            this.optTatca.Size = new System.Drawing.Size(64, 20);
            this.optTatca.TabIndex = 16;
            this.optTatca.Text = "Tất cả";
            this.optTatca.UseVisualStyleBackColor = true;
            // 
            // optDTDL
            // 
            this.optDTDL.AutoSize = true;
            this.optDTDL.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.optDTDL.ForeColor = System.Drawing.SystemColors.ActiveCaption;
            this.optDTDL.Location = new System.Drawing.Point(181, 17);
            this.optDTDL.Name = "optDTDL";
            this.optDTDL.Size = new System.Drawing.Size(157, 20);
            this.optDTDL.TabIndex = 15;
            this.optDTDL.Text = "Định tính và định lượng";
            this.optDTDL.UseVisualStyleBackColor = true;
            // 
            // optDinhtinh
            // 
            this.optDinhtinh.AutoSize = true;
            this.optDinhtinh.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.optDinhtinh.ForeColor = System.Drawing.SystemColors.ActiveCaption;
            this.optDinhtinh.Location = new System.Drawing.Point(100, 17);
            this.optDinhtinh.Name = "optDinhtinh";
            this.optDinhtinh.Size = new System.Drawing.Size(75, 20);
            this.optDinhtinh.TabIndex = 14;
            this.optDinhtinh.Text = "Định tính";
            this.optDinhtinh.UseVisualStyleBackColor = true;
            // 
            // optDinhluong
            // 
            this.optDinhluong.AutoSize = true;
            this.optDinhluong.Checked = true;
            this.optDinhluong.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.optDinhluong.ForeColor = System.Drawing.SystemColors.ActiveCaption;
            this.optDinhluong.Location = new System.Drawing.Point(6, 17);
            this.optDinhluong.Name = "optDinhluong";
            this.optDinhluong.Size = new System.Drawing.Size(88, 20);
            this.optDinhluong.TabIndex = 13;
            this.optDinhluong.TabStop = true;
            this.optDinhluong.Text = "Định lượng";
            this.optDinhluong.UseVisualStyleBackColor = true;
            // 
            // groupBox3
            // 
            this.groupBox3.Controls.Add(this.optChuapheduyet);
            this.groupBox3.Controls.Add(this.optPheduyet);
            this.groupBox3.Location = new System.Drawing.Point(336, 31);
            this.groupBox3.Name = "groupBox3";
            this.groupBox3.Size = new System.Drawing.Size(243, 44);
            this.groupBox3.TabIndex = 242;
            this.groupBox3.TabStop = false;
            this.groupBox3.Enter += new System.EventHandler(this.groupBox3_Enter);
            // 
            // optChuapheduyet
            // 
            this.optChuapheduyet.AutoSize = true;
            this.optChuapheduyet.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.optChuapheduyet.ForeColor = System.Drawing.SystemColors.ActiveCaption;
            this.optChuapheduyet.Location = new System.Drawing.Point(107, 17);
            this.optChuapheduyet.Name = "optChuapheduyet";
            this.optChuapheduyet.Size = new System.Drawing.Size(119, 20);
            this.optChuapheduyet.TabIndex = 14;
            this.optChuapheduyet.Text = "Chưa phê duyệt";
            this.optChuapheduyet.UseVisualStyleBackColor = true;
            // 
            // optPheduyet
            // 
            this.optPheduyet.AutoSize = true;
            this.optPheduyet.Checked = true;
            this.optPheduyet.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.optPheduyet.ForeColor = System.Drawing.SystemColors.ActiveCaption;
            this.optPheduyet.Location = new System.Drawing.Point(15, 17);
            this.optPheduyet.Name = "optPheduyet";
            this.optPheduyet.Size = new System.Drawing.Size(86, 20);
            this.optPheduyet.TabIndex = 13;
            this.optPheduyet.TabStop = true;
            this.optPheduyet.Text = "Phê duyệt";
            this.optPheduyet.UseVisualStyleBackColor = true;
            // 
            // labelX5
            // 
            this.labelX5.Font = new System.Drawing.Font("Arial", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.labelX5.ForeColor = System.Drawing.SystemColors.ControlText;
            this.labelX5.Location = new System.Drawing.Point(12, 140);
            this.labelX5.Name = "labelX5";
            this.labelX5.Size = new System.Drawing.Size(125, 20);
            this.labelX5.TabIndex = 289;
            this.labelX5.Text = "Loại khách hàng:";
            // 
            // cbLoaiKH
            // 
            this.cbLoaiKH.DisplayMember = "Text";
            this.cbLoaiKH.DrawMode = System.Windows.Forms.DrawMode.OwnerDrawFixed;
            this.cbLoaiKH.DropDownHeight = 100;
            this.cbLoaiKH.DropDownWidth = 99;
            this.cbLoaiKH.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.cbLoaiKH.FormattingEnabled = true;
            this.cbLoaiKH.IntegralHeight = false;
            this.cbLoaiKH.ItemHeight = 19;
            this.cbLoaiKH.Location = new System.Drawing.Point(145, 135);
            this.cbLoaiKH.Name = "cbLoaiKH";
            this.cbLoaiKH.Size = new System.Drawing.Size(185, 25);
            this.cbLoaiKH.TabIndex = 288;
            this.cbLoaiKH.SelectedIndexChanged += new System.EventHandler(this.cbLoaiKH_SelectedIndexChanged);
            // 
            // btnCancel
            // 
            this.btnCancel.AccessibleRole = System.Windows.Forms.AccessibleRole.PushButton;
            this.btnCancel.Location = new System.Drawing.Point(443, 186);
            this.btnCancel.Name = "btnCancel";
            this.btnCancel.Size = new System.Drawing.Size(97, 31);
            this.btnCancel.TabIndex = 297;
            this.btnCancel.Text = "Đóng";
            this.btnCancel.Click += new System.EventHandler(this.btnCancel_Click);
            // 
            // btnIn
            // 
            this.btnIn.AccessibleRole = System.Windows.Forms.AccessibleRole.PushButton;
            this.btnIn.Location = new System.Drawing.Point(340, 186);
            this.btnIn.Name = "btnIn";
            this.btnIn.Size = new System.Drawing.Size(97, 31);
            this.btnIn.TabIndex = 296;
            this.btnIn.Text = "In";
            this.btnIn.Click += new System.EventHandler(this.btnIn_Click);
            // 
            // labelX2
            // 
            this.labelX2.Font = new System.Drawing.Font("Arial", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.labelX2.ForeColor = System.Drawing.SystemColors.ControlText;
            this.labelX2.Location = new System.Drawing.Point(12, 49);
            this.labelX2.Name = "labelX2";
            this.labelX2.Size = new System.Drawing.Size(87, 20);
            this.labelX2.TabIndex = 298;
            this.labelX2.Text = "Kỳ xếp loại:";
            // 
            // lblTenloai
            // 
            this.lblTenloai.Font = new System.Drawing.Font("Arial", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblTenloai.ForeColor = System.Drawing.SystemColors.ControlText;
            this.lblTenloai.Location = new System.Drawing.Point(371, 140);
            this.lblTenloai.Name = "lblTenloai";
            this.lblTenloai.Size = new System.Drawing.Size(216, 20);
            this.lblTenloai.TabIndex = 299;
            this.lblTenloai.Visible = false;
            // 
            // labelX1
            // 
            this.labelX1.Font = new System.Drawing.Font("Arial", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.labelX1.ForeColor = System.Drawing.SystemColors.ControlText;
            this.labelX1.Location = new System.Drawing.Point(12, 12);
            this.labelX1.Name = "labelX1";
            this.labelX1.Size = new System.Drawing.Size(87, 20);
            this.labelX1.TabIndex = 308;
            this.labelX1.Text = "Chi nhánh:";
            // 
            // cbChiNhanh
            // 
            this.cbChiNhanh.DisplayMember = "Text";
            this.cbChiNhanh.DrawMode = System.Windows.Forms.DrawMode.OwnerDrawFixed;
            this.cbChiNhanh.DropDownHeight = 100;
            this.cbChiNhanh.DropDownWidth = 99;
            this.cbChiNhanh.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.cbChiNhanh.FormattingEnabled = true;
            this.cbChiNhanh.IntegralHeight = false;
            this.cbChiNhanh.ItemHeight = 19;
            this.cbChiNhanh.Location = new System.Drawing.Point(105, 7);
            this.cbChiNhanh.Name = "cbChiNhanh";
            this.cbChiNhanh.Size = new System.Drawing.Size(225, 25);
            this.cbChiNhanh.TabIndex = 307;
            // 
            // frmHH_TKXLKH
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(672, 229);
            this.Controls.Add(this.labelX1);
            this.Controls.Add(this.cbChiNhanh);
            this.Controls.Add(this.lblTenloai);
            this.Controls.Add(this.labelX2);
            this.Controls.Add(this.btnCancel);
            this.Controls.Add(this.btnIn);
            this.Controls.Add(this.labelX5);
            this.Controls.Add(this.cbLoaiKH);
            this.Controls.Add(this.groupBox3);
            this.Controls.Add(this.groupBox2);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.dtpTo);
            this.Controls.Add(this.dtpFrom);
            this.Controls.Add(this.groupBox10);
            this.Name = "frmHH_TKXLKH";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Xep loai KH - thong ke chi tiet";
            this.Load += new System.EventHandler(this.frmHH_TKXLKH_Load);
            this.groupBox10.ResumeLayout(false);
            this.groupBox10.PerformLayout();
            this.groupBox2.ResumeLayout(false);
            this.groupBox2.PerformLayout();
            this.groupBox3.ResumeLayout(false);
            this.groupBox3.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.GroupBox groupBox10;
        private System.Windows.Forms.RadioButton optDN;
        private System.Windows.Forms.RadioButton optCN;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.DateTimePicker dtpTo;
        private System.Windows.Forms.DateTimePicker dtpFrom;
        private System.Windows.Forms.GroupBox groupBox2;
        private System.Windows.Forms.RadioButton optDinhtinh;
        private System.Windows.Forms.RadioButton optDinhluong;
        private System.Windows.Forms.GroupBox groupBox3;
        private System.Windows.Forms.RadioButton optChuapheduyet;
        private System.Windows.Forms.RadioButton optPheduyet;
        private DevComponents.DotNetBar.LabelX labelX5;
        private DevComponents.DotNetBar.Controls.ComboBoxEx cbLoaiKH;
        private DevComponents.DotNetBar.ButtonX btnCancel;
        private DevComponents.DotNetBar.ButtonX btnIn;
        private DevComponents.DotNetBar.LabelX labelX2;
        private DevComponents.DotNetBar.LabelX lblTenloai;
        private System.Windows.Forms.RadioButton optDTDL;
        private DevComponents.DotNetBar.LabelX labelX1;
        private DevComponents.DotNetBar.Controls.ComboBoxEx cbChiNhanh;
        private System.Windows.Forms.RadioButton optTatca;
    }
}