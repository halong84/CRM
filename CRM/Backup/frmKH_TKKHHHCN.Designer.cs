namespace CRM
{
    partial class frmKH_TKKHHHCN
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
            this.labelX5 = new DevComponents.DotNetBar.LabelX();
            this.cbLoaiKH = new DevComponents.DotNetBar.Controls.ComboBoxEx();
            this.comboItem1 = new DevComponents.Editors.ComboItem();
            this.comboItem2 = new DevComponents.Editors.ComboItem();
            this.txtDenngay = new System.Windows.Forms.TextBox();
            this.txtTungay = new System.Windows.Forms.TextBox();
            this.labelX3 = new DevComponents.DotNetBar.LabelX();
            this.btnCancel = new DevComponents.DotNetBar.ButtonX();
            this.btnIn = new DevComponents.DotNetBar.ButtonX();
            this.labelX2 = new DevComponents.DotNetBar.LabelX();
            this.labelX1 = new DevComponents.DotNetBar.LabelX();
            this.cbChiNhanh = new DevComponents.DotNetBar.Controls.ComboBoxEx();
            this.comboItem3 = new DevComponents.Editors.ComboItem();
            this.comboItem4 = new DevComponents.Editors.ComboItem();
            this.comboItem5 = new DevComponents.Editors.ComboItem();
            this.comboItem6 = new DevComponents.Editors.ComboItem();
            this.comboItem7 = new DevComponents.Editors.ComboItem();
            this.comboItem8 = new DevComponents.Editors.ComboItem();
            this.comboItem9 = new DevComponents.Editors.ComboItem();
            this.comboItem10 = new DevComponents.Editors.ComboItem();
            this.comboItem11 = new DevComponents.Editors.ComboItem();
            this.comboItem12 = new DevComponents.Editors.ComboItem();
            this.comboItem13 = new DevComponents.Editors.ComboItem();
            this.comboItem14 = new DevComponents.Editors.ComboItem();
            this.comboItem15 = new DevComponents.Editors.ComboItem();
            this.comboItem16 = new DevComponents.Editors.ComboItem();
            this.comboItem17 = new DevComponents.Editors.ComboItem();
            this.comboItem18 = new DevComponents.Editors.ComboItem();
            this.SuspendLayout();
            // 
            // labelX5
            // 
            this.labelX5.Font = new System.Drawing.Font("Arial", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.labelX5.ForeColor = System.Drawing.SystemColors.ControlText;
            this.labelX5.Location = new System.Drawing.Point(12, 48);
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
            this.cbLoaiKH.Items.AddRange(new object[] {
            this.comboItem1,
            this.comboItem2});
            this.cbLoaiKH.Location = new System.Drawing.Point(145, 43);
            this.cbLoaiKH.Name = "cbLoaiKH";
            this.cbLoaiKH.Size = new System.Drawing.Size(220, 25);
            this.cbLoaiKH.TabIndex = 288;
            // 
            // comboItem1
            // 
            this.comboItem1.FontSize = 10F;
            this.comboItem1.Text = "Nam";
            // 
            // comboItem2
            // 
            this.comboItem2.FontSize = 10F;
            this.comboItem2.Text = "Nữ";
            // 
            // txtDenngay
            // 
            this.txtDenngay.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtDenngay.Location = new System.Drawing.Point(269, 80);
            this.txtDenngay.Name = "txtDenngay";
            this.txtDenngay.Size = new System.Drawing.Size(98, 26);
            this.txtDenngay.TabIndex = 304;
            // 
            // txtTungay
            // 
            this.txtTungay.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtTungay.Location = new System.Drawing.Point(145, 80);
            this.txtTungay.Name = "txtTungay";
            this.txtTungay.Size = new System.Drawing.Size(98, 26);
            this.txtTungay.TabIndex = 303;
            // 
            // labelX3
            // 
            this.labelX3.Font = new System.Drawing.Font("Arial", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.labelX3.ForeColor = System.Drawing.SystemColors.ControlText;
            this.labelX3.Location = new System.Drawing.Point(249, 83);
            this.labelX3.Name = "labelX3";
            this.labelX3.Size = new System.Drawing.Size(24, 20);
            this.labelX3.TabIndex = 302;
            this.labelX3.Text = "=>";
            // 
            // btnCancel
            // 
            this.btnCancel.AccessibleRole = System.Windows.Forms.AccessibleRole.PushButton;
            this.btnCancel.Location = new System.Drawing.Point(249, 112);
            this.btnCancel.Name = "btnCancel";
            this.btnCancel.Size = new System.Drawing.Size(97, 31);
            this.btnCancel.TabIndex = 301;
            this.btnCancel.Text = "Thoát";
            this.btnCancel.Click += new System.EventHandler(this.btnCancel_Click);
            // 
            // btnIn
            // 
            this.btnIn.AccessibleRole = System.Windows.Forms.AccessibleRole.PushButton;
            this.btnIn.Location = new System.Drawing.Point(146, 112);
            this.btnIn.Name = "btnIn";
            this.btnIn.Size = new System.Drawing.Size(97, 31);
            this.btnIn.TabIndex = 300;
            this.btnIn.Text = "In";
            this.btnIn.Click += new System.EventHandler(this.btnIn_Click);
            // 
            // labelX2
            // 
            this.labelX2.Font = new System.Drawing.Font("Arial", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.labelX2.ForeColor = System.Drawing.SystemColors.ControlText;
            this.labelX2.Location = new System.Drawing.Point(12, 83);
            this.labelX2.Name = "labelX2";
            this.labelX2.Size = new System.Drawing.Size(102, 20);
            this.labelX2.TabIndex = 299;
            this.labelX2.Text = "Sinh nhật:";
            // 
            // labelX1
            // 
            this.labelX1.Font = new System.Drawing.Font("Arial", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.labelX1.ForeColor = System.Drawing.SystemColors.ControlText;
            this.labelX1.Location = new System.Drawing.Point(12, 12);
            this.labelX1.Name = "labelX1";
            this.labelX1.Size = new System.Drawing.Size(125, 20);
            this.labelX1.TabIndex = 306;
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
            this.cbChiNhanh.Items.AddRange(new object[] {
            this.comboItem3,
            this.comboItem4,
            this.comboItem5,
            this.comboItem6,
            this.comboItem7,
            this.comboItem8,
            this.comboItem9,
            this.comboItem10,
            this.comboItem11,
            this.comboItem12,
            this.comboItem13,
            this.comboItem14,
            this.comboItem15,
            this.comboItem16,
            this.comboItem17,
            this.comboItem18});
            this.cbChiNhanh.Location = new System.Drawing.Point(145, 7);
            this.cbChiNhanh.Name = "cbChiNhanh";
            this.cbChiNhanh.Size = new System.Drawing.Size(220, 25);
            this.cbChiNhanh.TabIndex = 305;
            // 
            // comboItem3
            // 
            this.comboItem3.FontSize = 10F;
            this.comboItem3.Text = "4800";
            // 
            // comboItem4
            // 
            this.comboItem4.FontSize = 10F;
            this.comboItem4.Text = "4801";
            // 
            // comboItem5
            // 
            this.comboItem5.Text = "4802";
            // 
            // comboItem6
            // 
            this.comboItem6.Text = "4803";
            // 
            // comboItem7
            // 
            this.comboItem7.Text = "4804";
            // 
            // comboItem8
            // 
            this.comboItem8.Text = "4805";
            // 
            // comboItem9
            // 
            this.comboItem9.Text = "4806";
            // 
            // comboItem10
            // 
            this.comboItem10.Text = "4807";
            // 
            // comboItem11
            // 
            this.comboItem11.Text = "4808";
            // 
            // comboItem12
            // 
            this.comboItem12.Text = "4809";
            // 
            // comboItem13
            // 
            this.comboItem13.Text = "4810";
            // 
            // comboItem14
            // 
            this.comboItem14.Text = "4811";
            // 
            // comboItem15
            // 
            this.comboItem15.Text = "4813";
            // 
            // comboItem16
            // 
            this.comboItem16.Text = "4814";
            // 
            // comboItem17
            // 
            this.comboItem17.Text = "4815";
            // 
            // comboItem18
            // 
            this.comboItem18.Text = "Toàn tỉnh";
            // 
            // frmKH_TKKHHHCN
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(385, 148);
            this.Controls.Add(this.labelX1);
            this.Controls.Add(this.cbChiNhanh);
            this.Controls.Add(this.txtDenngay);
            this.Controls.Add(this.txtTungay);
            this.Controls.Add(this.labelX3);
            this.Controls.Add(this.btnCancel);
            this.Controls.Add(this.btnIn);
            this.Controls.Add(this.labelX2);
            this.Controls.Add(this.labelX5);
            this.Controls.Add(this.cbLoaiKH);
            this.Name = "frmKH_TKKHHHCN";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Thong ke KH hien huu theo CN";
            this.Load += new System.EventHandler(this.frmKH_TKKHHHCN_Load);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private DevComponents.DotNetBar.LabelX labelX5;
        private DevComponents.DotNetBar.Controls.ComboBoxEx cbLoaiKH;
        private DevComponents.Editors.ComboItem comboItem1;
        private DevComponents.Editors.ComboItem comboItem2;
        private System.Windows.Forms.TextBox txtDenngay;
        private System.Windows.Forms.TextBox txtTungay;
        private DevComponents.DotNetBar.LabelX labelX3;
        private DevComponents.DotNetBar.ButtonX btnCancel;
        private DevComponents.DotNetBar.ButtonX btnIn;
        private DevComponents.DotNetBar.LabelX labelX2;
        private DevComponents.DotNetBar.LabelX labelX1;
        private DevComponents.DotNetBar.Controls.ComboBoxEx cbChiNhanh;
        private DevComponents.Editors.ComboItem comboItem3;
        private DevComponents.Editors.ComboItem comboItem4;
        private DevComponents.Editors.ComboItem comboItem5;
        private DevComponents.Editors.ComboItem comboItem6;
        private DevComponents.Editors.ComboItem comboItem7;
        private DevComponents.Editors.ComboItem comboItem8;
        private DevComponents.Editors.ComboItem comboItem9;
        private DevComponents.Editors.ComboItem comboItem10;
        private DevComponents.Editors.ComboItem comboItem11;
        private DevComponents.Editors.ComboItem comboItem12;
        private DevComponents.Editors.ComboItem comboItem13;
        private DevComponents.Editors.ComboItem comboItem14;
        private DevComponents.Editors.ComboItem comboItem15;
        private DevComponents.Editors.ComboItem comboItem16;
        private DevComponents.Editors.ComboItem comboItem17;
        private DevComponents.Editors.ComboItem comboItem18;
    }
}