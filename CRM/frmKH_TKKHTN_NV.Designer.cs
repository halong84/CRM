namespace CRM
{
    partial class frmKH_TKKHTN_NV
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
            this.cbbCB = new DevComponents.DotNetBar.Controls.ComboBoxEx();
            this.cbLoaiKH = new DevComponents.DotNetBar.Controls.ComboBoxEx();
            this.SuspendLayout();
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
            this.cbbCN.Location = new System.Drawing.Point(173, 12);
            this.cbbCN.Name = "cbbCN";
            this.cbbCN.Size = new System.Drawing.Size(441, 25);
            this.cbbCN.TabIndex = 357;
            this.cbbCN.SelectionChangeCommitted += new System.EventHandler(this.cbbCN_SelectionChangeCommitted);
            // 
            // labelX3
            // 
            this.labelX3.Font = new System.Drawing.Font("Arial", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.labelX3.ForeColor = System.Drawing.SystemColors.ControlText;
            this.labelX3.Location = new System.Drawing.Point(40, 17);
            this.labelX3.Name = "labelX3";
            this.labelX3.Size = new System.Drawing.Size(96, 20);
            this.labelX3.TabIndex = 356;
            this.labelX3.Text = "Chi nhánh";
            // 
            // labelX7
            // 
            this.labelX7.Font = new System.Drawing.Font("Arial", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.labelX7.ForeColor = System.Drawing.SystemColors.ControlText;
            this.labelX7.Location = new System.Drawing.Point(110, 110);
            this.labelX7.Name = "labelX7";
            this.labelX7.Size = new System.Drawing.Size(41, 20);
            this.labelX7.TabIndex = 354;
            this.labelX7.Text = "Xã:";
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
            this.cbXa.Location = new System.Drawing.Point(173, 105);
            this.cbXa.Name = "cbXa";
            this.cbXa.Size = new System.Drawing.Size(441, 25);
            this.cbXa.TabIndex = 352;
            // 
            // btnCancel
            // 
            this.btnCancel.AccessibleRole = System.Windows.Forms.AccessibleRole.PushButton;
            this.btnCancel.Location = new System.Drawing.Point(296, 207);
            this.btnCancel.Name = "btnCancel";
            this.btnCancel.Size = new System.Drawing.Size(97, 31);
            this.btnCancel.TabIndex = 355;
            this.btnCancel.Text = "Đóng";
            this.btnCancel.Click += new System.EventHandler(this.btnCancel_Click);
            // 
            // labelX1
            // 
            this.labelX1.Font = new System.Drawing.Font("Arial", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.labelX1.ForeColor = System.Drawing.SystemColors.ControlText;
            this.labelX1.Location = new System.Drawing.Point(40, 43);
            this.labelX1.Name = "labelX1";
            this.labelX1.Size = new System.Drawing.Size(64, 20);
            this.labelX1.TabIndex = 347;
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
            this.cbTinh.Location = new System.Drawing.Point(173, 43);
            this.cbTinh.Name = "cbTinh";
            this.cbTinh.Size = new System.Drawing.Size(441, 25);
            this.cbTinh.TabIndex = 346;
            this.cbTinh.SelectedIndexChanged += new System.EventHandler(this.cbTinh_SelectedIndexChanged);
            this.cbTinh.SelectionChangeCommitted += new System.EventHandler(this.cbTinh_SelectionChangeCommitted);
            // 
            // labelX5
            // 
            this.labelX5.Font = new System.Drawing.Font("Arial", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.labelX5.ForeColor = System.Drawing.SystemColors.ControlText;
            this.labelX5.Location = new System.Drawing.Point(40, 172);
            this.labelX5.Name = "labelX5";
            this.labelX5.Size = new System.Drawing.Size(125, 20);
            this.labelX5.TabIndex = 345;
            this.labelX5.Text = "Loại khách hàng:";
            this.labelX5.Visible = false;
            // 
            // labelX6
            // 
            this.labelX6.Font = new System.Drawing.Font("Arial", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.labelX6.ForeColor = System.Drawing.SystemColors.ControlText;
            this.labelX6.Location = new System.Drawing.Point(110, 74);
            this.labelX6.Name = "labelX6";
            this.labelX6.Size = new System.Drawing.Size(60, 20);
            this.labelX6.TabIndex = 351;
            this.labelX6.Text = "Huyện:";
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
            this.cbHuyen.Location = new System.Drawing.Point(173, 74);
            this.cbHuyen.Name = "cbHuyen";
            this.cbHuyen.Size = new System.Drawing.Size(441, 25);
            this.cbHuyen.TabIndex = 350;
            this.cbHuyen.SelectedIndexChanged += new System.EventHandler(this.cbHuyen_SelectedIndexChanged);
            this.cbHuyen.SelectionChangeCommitted += new System.EventHandler(this.cbHuyen_SelectionChangeCommitted);
            // 
            // btnIn
            // 
            this.btnIn.AccessibleRole = System.Windows.Forms.AccessibleRole.PushButton;
            this.btnIn.Location = new System.Drawing.Point(193, 207);
            this.btnIn.Name = "btnIn";
            this.btnIn.Size = new System.Drawing.Size(97, 31);
            this.btnIn.TabIndex = 353;
            this.btnIn.Text = "In";
            this.btnIn.Click += new System.EventHandler(this.btnIn_Click);
            // 
            // labelX4
            // 
            this.labelX4.Font = new System.Drawing.Font("Arial", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.labelX4.ForeColor = System.Drawing.SystemColors.ControlText;
            this.labelX4.Location = new System.Drawing.Point(110, 43);
            this.labelX4.Name = "labelX4";
            this.labelX4.Size = new System.Drawing.Size(41, 20);
            this.labelX4.TabIndex = 348;
            this.labelX4.Text = "Tỉnh:";
            // 
            // labelX2
            // 
            this.labelX2.Font = new System.Drawing.Font("Arial", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.labelX2.ForeColor = System.Drawing.SystemColors.ControlText;
            this.labelX2.Location = new System.Drawing.Point(44, 141);
            this.labelX2.Name = "labelX2";
            this.labelX2.Size = new System.Drawing.Size(126, 20);
            this.labelX2.TabIndex = 349;
            this.labelX2.Text = "Cán bộ phụ trách";
            // 
            // cbbCB
            // 
            this.cbbCB.DisplayMember = "Text";
            this.cbbCB.DrawMode = System.Windows.Forms.DrawMode.OwnerDrawFixed;
            this.cbbCB.DropDownHeight = 100;
            this.cbbCB.DropDownWidth = 99;
            this.cbbCB.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.cbbCB.FormattingEnabled = true;
            this.cbbCB.IntegralHeight = false;
            this.cbbCB.ItemHeight = 19;
            this.cbbCB.Location = new System.Drawing.Point(173, 136);
            this.cbbCB.Name = "cbbCB";
            this.cbbCB.Size = new System.Drawing.Size(441, 25);
            this.cbbCB.TabIndex = 343;
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
            this.cbLoaiKH.Location = new System.Drawing.Point(173, 167);
            this.cbLoaiKH.Name = "cbLoaiKH";
            this.cbLoaiKH.Size = new System.Drawing.Size(441, 25);
            this.cbLoaiKH.TabIndex = 344;
            this.cbLoaiKH.Visible = false;
            // 
            // frmKH_TKKHTN_NV
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(626, 250);
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
            this.Controls.Add(this.cbbCB);
            this.Controls.Add(this.cbLoaiKH);
            this.Name = "frmKH_TKKHTN_NV";
            this.ShowIcon = false;
            this.Text = "Thống kê khách hàng tiềm năng đang được theo dõi";
            this.Load += new System.EventHandler(this.frmKH_TKKHTN_NV_Load);
            this.ResumeLayout(false);

        }

        #endregion

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
        private DevComponents.DotNetBar.Controls.ComboBoxEx cbbCB;
        private DevComponents.DotNetBar.Controls.ComboBoxEx cbLoaiKH;

    }
}