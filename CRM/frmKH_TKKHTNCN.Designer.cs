namespace CRM
{
    partial class frmKH_TKKHTNCN
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
            this.labelX1 = new DevComponents.DotNetBar.LabelX();
            this.cbChiNhanh = new DevComponents.DotNetBar.Controls.ComboBoxEx();
            this.txtDenngay = new System.Windows.Forms.TextBox();
            this.txtTungay = new System.Windows.Forms.TextBox();
            this.comboItem2 = new DevComponents.Editors.ComboItem();
            this.labelX3 = new DevComponents.DotNetBar.LabelX();
            this.comboItem1 = new DevComponents.Editors.ComboItem();
            this.btnCancel = new DevComponents.DotNetBar.ButtonX();
            this.btnIn = new DevComponents.DotNetBar.ButtonX();
            this.cbLoaiKH = new DevComponents.DotNetBar.Controls.ComboBoxEx();
            this.labelX2 = new DevComponents.DotNetBar.LabelX();
            this.SuspendLayout();
            // 
            // labelX5
            // 
            this.labelX5.Font = new System.Drawing.Font("Arial", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.labelX5.ForeColor = System.Drawing.SystemColors.ControlText;
            this.labelX5.Location = new System.Drawing.Point(15, 47);
            this.labelX5.Name = "labelX5";
            this.labelX5.Size = new System.Drawing.Size(125, 20);
            this.labelX5.TabIndex = 308;
            this.labelX5.Text = "Loại khách hàng:";
            // 
            // labelX1
            // 
            this.labelX1.Font = new System.Drawing.Font("Arial", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.labelX1.ForeColor = System.Drawing.SystemColors.ControlText;
            this.labelX1.Location = new System.Drawing.Point(15, 11);
            this.labelX1.Name = "labelX1";
            this.labelX1.Size = new System.Drawing.Size(125, 20);
            this.labelX1.TabIndex = 316;
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
            this.cbChiNhanh.Location = new System.Drawing.Point(148, 6);
            this.cbChiNhanh.Name = "cbChiNhanh";
            this.cbChiNhanh.Size = new System.Drawing.Size(467, 25);
            this.cbChiNhanh.TabIndex = 315;
            // 
            // txtDenngay
            // 
            this.txtDenngay.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtDenngay.Location = new System.Drawing.Point(272, 79);
            this.txtDenngay.Name = "txtDenngay";
            this.txtDenngay.Size = new System.Drawing.Size(98, 26);
            this.txtDenngay.TabIndex = 314;
            // 
            // txtTungay
            // 
            this.txtTungay.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtTungay.Location = new System.Drawing.Point(148, 79);
            this.txtTungay.Name = "txtTungay";
            this.txtTungay.Size = new System.Drawing.Size(98, 26);
            this.txtTungay.TabIndex = 313;
            // 
            // comboItem2
            // 
            this.comboItem2.FontSize = 10F;
            this.comboItem2.Text = "Nữ";
            // 
            // labelX3
            // 
            this.labelX3.Font = new System.Drawing.Font("Arial", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.labelX3.ForeColor = System.Drawing.SystemColors.ControlText;
            this.labelX3.Location = new System.Drawing.Point(252, 82);
            this.labelX3.Name = "labelX3";
            this.labelX3.Size = new System.Drawing.Size(24, 20);
            this.labelX3.TabIndex = 312;
            this.labelX3.Text = "=>";
            // 
            // comboItem1
            // 
            this.comboItem1.FontSize = 10F;
            this.comboItem1.Text = "Nam";
            // 
            // btnCancel
            // 
            this.btnCancel.AccessibleRole = System.Windows.Forms.AccessibleRole.PushButton;
            this.btnCancel.Location = new System.Drawing.Point(252, 111);
            this.btnCancel.Name = "btnCancel";
            this.btnCancel.Size = new System.Drawing.Size(97, 31);
            this.btnCancel.TabIndex = 311;
            this.btnCancel.Text = "Thoát";
            this.btnCancel.Click += new System.EventHandler(this.btnCancel_Click);
            // 
            // btnIn
            // 
            this.btnIn.AccessibleRole = System.Windows.Forms.AccessibleRole.PushButton;
            this.btnIn.Location = new System.Drawing.Point(149, 111);
            this.btnIn.Name = "btnIn";
            this.btnIn.Size = new System.Drawing.Size(97, 31);
            this.btnIn.TabIndex = 310;
            this.btnIn.Text = "In";
            this.btnIn.Click += new System.EventHandler(this.btnIn_Click);
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
            this.cbLoaiKH.Location = new System.Drawing.Point(148, 42);
            this.cbLoaiKH.Name = "cbLoaiKH";
            this.cbLoaiKH.Size = new System.Drawing.Size(220, 25);
            this.cbLoaiKH.TabIndex = 307;
            // 
            // labelX2
            // 
            this.labelX2.Font = new System.Drawing.Font("Arial", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.labelX2.ForeColor = System.Drawing.SystemColors.ControlText;
            this.labelX2.Location = new System.Drawing.Point(15, 82);
            this.labelX2.Name = "labelX2";
            this.labelX2.Size = new System.Drawing.Size(102, 20);
            this.labelX2.TabIndex = 309;
            this.labelX2.Text = "Sinh nhật:";
            // 
            // frmKH_TKKHTNCN
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(627, 148);
            this.Controls.Add(this.labelX5);
            this.Controls.Add(this.labelX1);
            this.Controls.Add(this.cbChiNhanh);
            this.Controls.Add(this.txtDenngay);
            this.Controls.Add(this.txtTungay);
            this.Controls.Add(this.labelX3);
            this.Controls.Add(this.btnCancel);
            this.Controls.Add(this.btnIn);
            this.Controls.Add(this.cbLoaiKH);
            this.Controls.Add(this.labelX2);
            this.Name = "frmKH_TKKHTNCN";
            this.ShowIcon = false;
            this.Text = "Thống kê khách hàng tiềm năng theo chi nhánh";
            this.Load += new System.EventHandler(this.frmKH_TKKHTNCN_Load);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private DevComponents.DotNetBar.LabelX labelX5;
        private DevComponents.DotNetBar.LabelX labelX1;
        private DevComponents.DotNetBar.Controls.ComboBoxEx cbChiNhanh;
        private System.Windows.Forms.TextBox txtDenngay;
        private System.Windows.Forms.TextBox txtTungay;
        private DevComponents.Editors.ComboItem comboItem2;
        private DevComponents.DotNetBar.LabelX labelX3;
        private DevComponents.Editors.ComboItem comboItem1;
        private DevComponents.DotNetBar.ButtonX btnCancel;
        private DevComponents.DotNetBar.ButtonX btnIn;
        private DevComponents.DotNetBar.Controls.ComboBoxEx cbLoaiKH;
        private DevComponents.DotNetBar.LabelX labelX2;
    }
}