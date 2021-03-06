namespace CRM
{
    partial class frmKH_TKKHTN
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
            this.labelX8 = new DevComponents.DotNetBar.LabelX();
            this.chkNgay = new System.Windows.Forms.CheckBox();
            this.dtpTo = new System.Windows.Forms.DateTimePicker();
            this.dtpFrom = new System.Windows.Forms.DateTimePicker();
            this.cbbCN = new DevComponents.DotNetBar.Controls.ComboBoxEx();
            this.labelX3 = new DevComponents.DotNetBar.LabelX();
            this.labelX7 = new DevComponents.DotNetBar.LabelX();
            this.cbXa = new DevComponents.DotNetBar.Controls.ComboBoxEx();
            this.btnCancel = new DevComponents.DotNetBar.ButtonX();
            this.labelX1 = new DevComponents.DotNetBar.LabelX();
            this.cbTinh = new DevComponents.DotNetBar.Controls.ComboBoxEx();
            this.labelX5 = new DevComponents.DotNetBar.LabelX();
            this.labelX6 = new DevComponents.DotNetBar.LabelX();
            this.cbHuyen = new DevComponents.DotNetBar.Controls.ComboBoxEx();
            this.btnIn = new DevComponents.DotNetBar.ButtonX();
            this.labelX4 = new DevComponents.DotNetBar.LabelX();
            this.labelX2 = new DevComponents.DotNetBar.LabelX();
            this.cbLoaiKH = new DevComponents.DotNetBar.Controls.ComboBoxEx();
            this.labelX10 = new DevComponents.DotNetBar.LabelX();
            this.labelX9 = new DevComponents.DotNetBar.LabelX();
            this.cbbNhomDT = new DevComponents.DotNetBar.Controls.ComboBoxEx();
            this.cbbDoituongKH = new DevComponents.DotNetBar.Controls.ComboBoxEx();
            this.txtDoituongKH = new DevComponents.DotNetBar.Controls.TextBoxX();
            this.txtNhomDT = new DevComponents.DotNetBar.Controls.TextBoxX();
            this.SuspendLayout();
            // 
            // labelX8
            // 
            this.labelX8.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.labelX8.Location = new System.Drawing.Point(276, 228);
            this.labelX8.Name = "labelX8";
            this.labelX8.Size = new System.Drawing.Size(10, 23);
            this.labelX8.TabIndex = 363;
            this.labelX8.Text = "-";
            // 
            // chkNgay
            // 
            this.chkNgay.AutoSize = true;
            this.chkNgay.Location = new System.Drawing.Point(173, 235);
            this.chkNgay.Name = "chkNgay";
            this.chkNgay.Size = new System.Drawing.Size(15, 14);
            this.chkNgay.TabIndex = 362;
            this.chkNgay.UseVisualStyleBackColor = true;
            this.chkNgay.Click += new System.EventHandler(this.chkNgay_CheckedChanged);
            // 
            // dtpTo
            // 
            this.dtpTo.CustomFormat = "dd/MM";
            this.dtpTo.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.dtpTo.Format = System.Windows.Forms.DateTimePickerFormat.Custom;
            this.dtpTo.Location = new System.Drawing.Point(296, 227);
            this.dtpTo.Name = "dtpTo";
            this.dtpTo.ShowUpDown = true;
            this.dtpTo.Size = new System.Drawing.Size(69, 26);
            this.dtpTo.TabIndex = 360;
            // 
            // dtpFrom
            // 
            this.dtpFrom.CustomFormat = "dd/MM";
            this.dtpFrom.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.dtpFrom.Format = System.Windows.Forms.DateTimePickerFormat.Custom;
            this.dtpFrom.Location = new System.Drawing.Point(194, 227);
            this.dtpFrom.Name = "dtpFrom";
            this.dtpFrom.ShowUpDown = true;
            this.dtpFrom.Size = new System.Drawing.Size(69, 26);
            this.dtpFrom.TabIndex = 361;
            // 
            // cbbCN
            // 
            this.cbbCN.DisplayMember = "Text";
            this.cbbCN.DrawMode = System.Windows.Forms.DrawMode.OwnerDrawFixed;
            this.cbbCN.DropDownHeight = 100;
            this.cbbCN.DropDownWidth = 99;
            this.cbbCN.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.cbbCN.FormattingEnabled = true;
            this.cbbCN.IntegralHeight = false;
            this.cbbCN.ItemHeight = 19;
            this.cbbCN.Location = new System.Drawing.Point(173, 10);
            this.cbbCN.Name = "cbbCN";
            this.cbbCN.Size = new System.Drawing.Size(401, 25);
            this.cbbCN.TabIndex = 359;
            // 
            // labelX3
            // 
            this.labelX3.Font = new System.Drawing.Font("Arial", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.labelX3.ForeColor = System.Drawing.SystemColors.ControlText;
            this.labelX3.Location = new System.Drawing.Point(81, 233);
            this.labelX3.Name = "labelX3";
            this.labelX3.Size = new System.Drawing.Size(84, 20);
            this.labelX3.TabIndex = 356;
            this.labelX3.Text = "Ngày sinh:";
            this.labelX3.TextAlignment = System.Drawing.StringAlignment.Far;
            // 
            // labelX7
            // 
            this.labelX7.Font = new System.Drawing.Font("Arial", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.labelX7.ForeColor = System.Drawing.SystemColors.ControlText;
            this.labelX7.Location = new System.Drawing.Point(124, 201);
            this.labelX7.Name = "labelX7";
            this.labelX7.Size = new System.Drawing.Size(41, 20);
            this.labelX7.TabIndex = 357;
            this.labelX7.Text = "Xã:";
            this.labelX7.TextAlignment = System.Drawing.StringAlignment.Far;
            // 
            // cbXa
            // 
            this.cbXa.DisplayMember = "Text";
            this.cbXa.DrawMode = System.Windows.Forms.DrawMode.OwnerDrawFixed;
            this.cbXa.DropDownHeight = 100;
            this.cbXa.DropDownWidth = 99;
            this.cbXa.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.cbXa.FormattingEnabled = true;
            this.cbXa.IntegralHeight = false;
            this.cbXa.ItemHeight = 19;
            this.cbXa.Location = new System.Drawing.Point(173, 196);
            this.cbXa.Name = "cbXa";
            this.cbXa.Size = new System.Drawing.Size(401, 25);
            this.cbXa.TabIndex = 354;
            // 
            // btnCancel
            // 
            this.btnCancel.AccessibleRole = System.Windows.Forms.AccessibleRole.PushButton;
            this.btnCancel.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnCancel.Location = new System.Drawing.Point(296, 271);
            this.btnCancel.Name = "btnCancel";
            this.btnCancel.Size = new System.Drawing.Size(97, 31);
            this.btnCancel.TabIndex = 358;
            this.btnCancel.Text = "Đóng";
            this.btnCancel.Click += new System.EventHandler(this.btnCancel_Click);
            // 
            // labelX1
            // 
            this.labelX1.Font = new System.Drawing.Font("Arial", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.labelX1.ForeColor = System.Drawing.SystemColors.ControlText;
            this.labelX1.Location = new System.Drawing.Point(40, 134);
            this.labelX1.Name = "labelX1";
            this.labelX1.Size = new System.Drawing.Size(64, 20);
            this.labelX1.TabIndex = 349;
            this.labelX1.Text = "Địa bàn:";
            // 
            // cbTinh
            // 
            this.cbTinh.DisplayMember = "Text";
            this.cbTinh.DrawMode = System.Windows.Forms.DrawMode.OwnerDrawFixed;
            this.cbTinh.DropDownHeight = 100;
            this.cbTinh.DropDownWidth = 99;
            this.cbTinh.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.cbTinh.FormattingEnabled = true;
            this.cbTinh.IntegralHeight = false;
            this.cbTinh.ItemHeight = 19;
            this.cbTinh.Location = new System.Drawing.Point(173, 134);
            this.cbTinh.Name = "cbTinh";
            this.cbTinh.Size = new System.Drawing.Size(401, 25);
            this.cbTinh.TabIndex = 348;
            this.cbTinh.SelectionChangeCommitted += new System.EventHandler(this.cbTinh_SelectionChangeCommitted);
            this.cbTinh.Click += new System.EventHandler(this.cbTinh_SelectedIndexChanged);
            // 
            // labelX5
            // 
            this.labelX5.Font = new System.Drawing.Font("Arial", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.labelX5.ForeColor = System.Drawing.SystemColors.ControlText;
            this.labelX5.Location = new System.Drawing.Point(32, 46);
            this.labelX5.Name = "labelX5";
            this.labelX5.Size = new System.Drawing.Size(133, 20);
            this.labelX5.TabIndex = 347;
            this.labelX5.Text = "Loại KH (IPCAS):";
            this.labelX5.TextAlignment = System.Drawing.StringAlignment.Far;
            // 
            // labelX6
            // 
            this.labelX6.Font = new System.Drawing.Font("Arial", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.labelX6.ForeColor = System.Drawing.SystemColors.ControlText;
            this.labelX6.Location = new System.Drawing.Point(105, 165);
            this.labelX6.Name = "labelX6";
            this.labelX6.Size = new System.Drawing.Size(60, 20);
            this.labelX6.TabIndex = 353;
            this.labelX6.Text = "Huyện:";
            this.labelX6.TextAlignment = System.Drawing.StringAlignment.Far;
            // 
            // cbHuyen
            // 
            this.cbHuyen.DisplayMember = "Text";
            this.cbHuyen.DrawMode = System.Windows.Forms.DrawMode.OwnerDrawFixed;
            this.cbHuyen.DropDownHeight = 100;
            this.cbHuyen.DropDownWidth = 99;
            this.cbHuyen.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.cbHuyen.FormattingEnabled = true;
            this.cbHuyen.IntegralHeight = false;
            this.cbHuyen.ItemHeight = 19;
            this.cbHuyen.Location = new System.Drawing.Point(173, 165);
            this.cbHuyen.Name = "cbHuyen";
            this.cbHuyen.Size = new System.Drawing.Size(401, 25);
            this.cbHuyen.TabIndex = 352;
            this.cbHuyen.SelectionChangeCommitted += new System.EventHandler(this.cbHuyen_SelectionChangeCommitted);
            this.cbHuyen.Click += new System.EventHandler(this.cbHuyen_SelectedIndexChanged);
            // 
            // btnIn
            // 
            this.btnIn.AccessibleRole = System.Windows.Forms.AccessibleRole.PushButton;
            this.btnIn.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnIn.Location = new System.Drawing.Point(193, 271);
            this.btnIn.Name = "btnIn";
            this.btnIn.Size = new System.Drawing.Size(97, 31);
            this.btnIn.TabIndex = 355;
            this.btnIn.Text = "In";
            this.btnIn.Click += new System.EventHandler(this.btnIn_Click);
            // 
            // labelX4
            // 
            this.labelX4.Font = new System.Drawing.Font("Arial", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.labelX4.ForeColor = System.Drawing.SystemColors.ControlText;
            this.labelX4.Location = new System.Drawing.Point(124, 134);
            this.labelX4.Name = "labelX4";
            this.labelX4.Size = new System.Drawing.Size(41, 20);
            this.labelX4.TabIndex = 350;
            this.labelX4.Text = "Tỉnh:";
            this.labelX4.TextAlignment = System.Drawing.StringAlignment.Far;
            // 
            // labelX2
            // 
            this.labelX2.Font = new System.Drawing.Font("Arial", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.labelX2.ForeColor = System.Drawing.SystemColors.ControlText;
            this.labelX2.Location = new System.Drawing.Point(69, 15);
            this.labelX2.Name = "labelX2";
            this.labelX2.Size = new System.Drawing.Size(96, 20);
            this.labelX2.TabIndex = 351;
            this.labelX2.Text = "Chi nhánh:";
            this.labelX2.TextAlignment = System.Drawing.StringAlignment.Far;
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
            this.cbLoaiKH.Location = new System.Drawing.Point(173, 41);
            this.cbLoaiKH.Name = "cbLoaiKH";
            this.cbLoaiKH.Size = new System.Drawing.Size(401, 25);
            this.cbLoaiKH.TabIndex = 346;
            this.cbLoaiKH.SelectionChangeCommitted += new System.EventHandler(this.cbLoaiKH_SelectionChangeCommitted);
            // 
            // labelX10
            // 
            this.labelX10.Font = new System.Drawing.Font("Arial", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.labelX10.ForeColor = System.Drawing.SystemColors.ControlText;
            this.labelX10.Location = new System.Drawing.Point(7, 77);
            this.labelX10.Name = "labelX10";
            this.labelX10.Size = new System.Drawing.Size(158, 20);
            this.labelX10.TabIndex = 368;
            this.labelX10.Text = "Nhóm đối tượng KH:";
            this.labelX10.TextAlignment = System.Drawing.StringAlignment.Far;
            // 
            // labelX9
            // 
            this.labelX9.Font = new System.Drawing.Font("Arial", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.labelX9.ForeColor = System.Drawing.SystemColors.ControlText;
            this.labelX9.Location = new System.Drawing.Point(54, 108);
            this.labelX9.Name = "labelX9";
            this.labelX9.Size = new System.Drawing.Size(111, 20);
            this.labelX9.TabIndex = 369;
            this.labelX9.Text = "Đối tượng KH:";
            this.labelX9.TextAlignment = System.Drawing.StringAlignment.Far;
            // 
            // cbbNhomDT
            // 
            this.cbbNhomDT.DisplayMember = "Text";
            this.cbbNhomDT.DrawMode = System.Windows.Forms.DrawMode.OwnerDrawFixed;
            this.cbbNhomDT.DropDownHeight = 100;
            this.cbbNhomDT.DropDownWidth = 99;
            this.cbbNhomDT.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.cbbNhomDT.FormattingEnabled = true;
            this.cbbNhomDT.IntegralHeight = false;
            this.cbbNhomDT.ItemHeight = 19;
            this.cbbNhomDT.Location = new System.Drawing.Point(173, 72);
            this.cbbNhomDT.Name = "cbbNhomDT";
            this.cbbNhomDT.Size = new System.Drawing.Size(401, 25);
            this.cbbNhomDT.TabIndex = 366;
            this.cbbNhomDT.SelectionChangeCommitted += new System.EventHandler(this.cbbNhomDT_SelectionChangeCommitted);
            // 
            // cbbDoituongKH
            // 
            this.cbbDoituongKH.DisplayMember = "Text";
            this.cbbDoituongKH.DrawMode = System.Windows.Forms.DrawMode.OwnerDrawFixed;
            this.cbbDoituongKH.DropDownHeight = 100;
            this.cbbDoituongKH.DropDownWidth = 99;
            this.cbbDoituongKH.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.cbbDoituongKH.FormattingEnabled = true;
            this.cbbDoituongKH.IntegralHeight = false;
            this.cbbDoituongKH.ItemHeight = 19;
            this.cbbDoituongKH.Location = new System.Drawing.Point(173, 103);
            this.cbbDoituongKH.Name = "cbbDoituongKH";
            this.cbbDoituongKH.Size = new System.Drawing.Size(401, 25);
            this.cbbDoituongKH.TabIndex = 367;
            this.cbbDoituongKH.SelectionChangeCommitted += new System.EventHandler(this.cbbDoituongKH_SelectionChangeCommitted);
            // 
            // txtDoituongKH
            // 
            this.txtDoituongKH.BackColor = System.Drawing.SystemColors.Control;
            // 
            // 
            // 
            this.txtDoituongKH.Border.Class = "TextBoxBorder";
            this.txtDoituongKH.Enabled = false;
            this.txtDoituongKH.Location = new System.Drawing.Point(93, 276);
            this.txtDoituongKH.Name = "txtDoituongKH";
            this.txtDoituongKH.Size = new System.Drawing.Size(75, 26);
            this.txtDoituongKH.TabIndex = 384;
            this.txtDoituongKH.Visible = false;
            // 
            // txtNhomDT
            // 
            this.txtNhomDT.BackColor = System.Drawing.SystemColors.Control;
            // 
            // 
            // 
            this.txtNhomDT.Border.Class = "TextBoxBorder";
            this.txtNhomDT.Enabled = false;
            this.txtNhomDT.Location = new System.Drawing.Point(12, 276);
            this.txtNhomDT.Name = "txtNhomDT";
            this.txtNhomDT.Size = new System.Drawing.Size(75, 26);
            this.txtNhomDT.TabIndex = 383;
            this.txtNhomDT.Visible = false;
            // 
            // frmKH_TKKHTN
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(586, 311);
            this.Controls.Add(this.txtDoituongKH);
            this.Controls.Add(this.txtNhomDT);
            this.Controls.Add(this.labelX10);
            this.Controls.Add(this.labelX9);
            this.Controls.Add(this.cbbNhomDT);
            this.Controls.Add(this.cbbDoituongKH);
            this.Controls.Add(this.labelX8);
            this.Controls.Add(this.chkNgay);
            this.Controls.Add(this.dtpTo);
            this.Controls.Add(this.dtpFrom);
            this.Controls.Add(this.cbbCN);
            this.Controls.Add(this.labelX3);
            this.Controls.Add(this.labelX7);
            this.Controls.Add(this.cbXa);
            this.Controls.Add(this.btnCancel);
            this.Controls.Add(this.labelX1);
            this.Controls.Add(this.cbTinh);
            this.Controls.Add(this.labelX5);
            this.Controls.Add(this.labelX6);
            this.Controls.Add(this.cbHuyen);
            this.Controls.Add(this.btnIn);
            this.Controls.Add(this.labelX4);
            this.Controls.Add(this.labelX2);
            this.Controls.Add(this.cbLoaiKH);
            this.Name = "frmKH_TKKHTN";
            this.ShowIcon = false;
            this.Text = "Thống kê khách hàng tiềm năng";
            this.Load += new System.EventHandler(this.frmKH_TKKHTN_Load);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private DevComponents.DotNetBar.LabelX labelX8;
        private System.Windows.Forms.CheckBox chkNgay;
        private System.Windows.Forms.DateTimePicker dtpTo;
        private System.Windows.Forms.DateTimePicker dtpFrom;
        private DevComponents.DotNetBar.Controls.ComboBoxEx cbbCN;
        private DevComponents.DotNetBar.LabelX labelX3;
        private DevComponents.DotNetBar.LabelX labelX7;
        private DevComponents.DotNetBar.Controls.ComboBoxEx cbXa;
        private DevComponents.DotNetBar.ButtonX btnCancel;
        private DevComponents.DotNetBar.LabelX labelX1;
        private DevComponents.DotNetBar.Controls.ComboBoxEx cbTinh;
        private DevComponents.DotNetBar.LabelX labelX5;
        private DevComponents.DotNetBar.LabelX labelX6;
        private DevComponents.DotNetBar.Controls.ComboBoxEx cbHuyen;
        private DevComponents.DotNetBar.ButtonX btnIn;
        private DevComponents.DotNetBar.LabelX labelX4;
        private DevComponents.DotNetBar.LabelX labelX2;
        private DevComponents.DotNetBar.Controls.ComboBoxEx cbLoaiKH;
        private DevComponents.DotNetBar.LabelX labelX10;
        private DevComponents.DotNetBar.LabelX labelX9;
        private DevComponents.DotNetBar.Controls.ComboBoxEx cbbNhomDT;
        private DevComponents.DotNetBar.Controls.ComboBoxEx cbbDoituongKH;
        private DevComponents.DotNetBar.Controls.TextBoxX txtDoituongKH;
        private DevComponents.DotNetBar.Controls.TextBoxX txtNhomDT;

    }
}