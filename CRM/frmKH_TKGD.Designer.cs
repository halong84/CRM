namespace CRM
{
    partial class frmKH_TKGD
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
            this.labelX3 = new DevComponents.DotNetBar.LabelX();
            this.btnCancel = new DevComponents.DotNetBar.ButtonX();
            this.labelX5 = new DevComponents.DotNetBar.LabelX();
            this.btnIn = new DevComponents.DotNetBar.ButtonX();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.rdbKH = new System.Windows.Forms.RadioButton();
            this.rdbCB = new System.Windows.Forms.RadioButton();
            this.grbLoai = new System.Windows.Forms.GroupBox();
            this.rdbHH = new System.Windows.Forms.RadioButton();
            this.rdbTN = new System.Windows.Forms.RadioButton();
            this.btnKH = new DevComponents.DotNetBar.ButtonX();
            this.txtMaKH = new DevComponents.DotNetBar.Controls.TextBoxX();
            this.cbbCN = new DevComponents.DotNetBar.Controls.ComboBoxEx();
            this.cbbCB = new DevComponents.DotNetBar.Controls.ComboBoxEx();
            this.labelX1 = new DevComponents.DotNetBar.LabelX();
            this.groupBox1.SuspendLayout();
            this.grbLoai.SuspendLayout();
            this.SuspendLayout();
            // 
            // labelX3
            // 
            this.labelX3.Font = new System.Drawing.Font("Arial", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.labelX3.ForeColor = System.Drawing.SystemColors.ControlText;
            this.labelX3.Location = new System.Drawing.Point(41, 17);
            this.labelX3.Name = "labelX3";
            this.labelX3.Size = new System.Drawing.Size(78, 20);
            this.labelX3.TabIndex = 371;
            this.labelX3.Text = "Chi nhánh";
            this.labelX3.TextAlignment = System.Drawing.StringAlignment.Far;
            // 
            // btnCancel
            // 
            this.btnCancel.AccessibleRole = System.Windows.Forms.AccessibleRole.PushButton;
            this.btnCancel.Location = new System.Drawing.Point(422, 173);
            this.btnCancel.Name = "btnCancel";
            this.btnCancel.Size = new System.Drawing.Size(66, 31);
            this.btnCancel.TabIndex = 370;
            this.btnCancel.Text = "Đóng";
            this.btnCancel.Click += new System.EventHandler(this.btnCancel_Click);
            // 
            // labelX5
            // 
            this.labelX5.Font = new System.Drawing.Font("Arial", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.labelX5.ForeColor = System.Drawing.SystemColors.ControlText;
            this.labelX5.Location = new System.Drawing.Point(31, 51);
            this.labelX5.Name = "labelX5";
            this.labelX5.Size = new System.Drawing.Size(88, 20);
            this.labelX5.TabIndex = 360;
            this.labelX5.Text = "Khách hàng";
            // 
            // btnIn
            // 
            this.btnIn.AccessibleRole = System.Windows.Forms.AccessibleRole.PushButton;
            this.btnIn.Location = new System.Drawing.Point(350, 173);
            this.btnIn.Name = "btnIn";
            this.btnIn.Size = new System.Drawing.Size(66, 31);
            this.btnIn.TabIndex = 368;
            this.btnIn.Text = "In";
            this.btnIn.Click += new System.EventHandler(this.btnIn_Click);
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.rdbKH);
            this.groupBox1.Controls.Add(this.rdbCB);
            this.groupBox1.Location = new System.Drawing.Point(125, 85);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(135, 69);
            this.groupBox1.TabIndex = 373;
            this.groupBox1.TabStop = false;
            // 
            // rdbKH
            // 
            this.rdbKH.AutoSize = true;
            this.rdbKH.Checked = true;
            this.rdbKH.ForeColor = System.Drawing.Color.Navy;
            this.rdbKH.Location = new System.Drawing.Point(6, 12);
            this.rdbKH.Name = "rdbKH";
            this.rdbKH.Size = new System.Drawing.Size(108, 22);
            this.rdbKH.TabIndex = 3;
            this.rdbKH.TabStop = true;
            this.rdbKH.Text = "Khách hàng";
            this.rdbKH.UseVisualStyleBackColor = true;
            this.rdbKH.CheckedChanged += new System.EventHandler(this.rdbKH_CheckedChanged);
            // 
            // rdbCB
            // 
            this.rdbCB.AutoSize = true;
            this.rdbCB.ForeColor = System.Drawing.Color.Navy;
            this.rdbCB.Location = new System.Drawing.Point(6, 41);
            this.rdbCB.Name = "rdbCB";
            this.rdbCB.Size = new System.Drawing.Size(77, 22);
            this.rdbCB.TabIndex = 2;
            this.rdbCB.Text = "Cán bộ";
            this.rdbCB.UseVisualStyleBackColor = true;
            // 
            // grbLoai
            // 
            this.grbLoai.Controls.Add(this.rdbHH);
            this.grbLoai.Controls.Add(this.rdbTN);
            this.grbLoai.Location = new System.Drawing.Point(125, 38);
            this.grbLoai.Name = "grbLoai";
            this.grbLoai.Size = new System.Drawing.Size(250, 41);
            this.grbLoai.TabIndex = 373;
            this.grbLoai.TabStop = false;
            // 
            // rdbHH
            // 
            this.rdbHH.AutoSize = true;
            this.rdbHH.Checked = true;
            this.rdbHH.ForeColor = System.Drawing.Color.Navy;
            this.rdbHH.Location = new System.Drawing.Point(6, 13);
            this.rdbHH.Name = "rdbHH";
            this.rdbHH.Size = new System.Drawing.Size(89, 22);
            this.rdbHH.TabIndex = 3;
            this.rdbHH.TabStop = true;
            this.rdbHH.Text = "Hiện hữu";
            this.rdbHH.UseVisualStyleBackColor = true;
            this.rdbHH.CheckedChanged += new System.EventHandler(this.rdbHH_CheckedChanged);
            // 
            // rdbTN
            // 
            this.rdbTN.AutoSize = true;
            this.rdbTN.ForeColor = System.Drawing.Color.Navy;
            this.rdbTN.Location = new System.Drawing.Point(145, 13);
            this.rdbTN.Name = "rdbTN";
            this.rdbTN.Size = new System.Drawing.Size(99, 22);
            this.rdbTN.TabIndex = 2;
            this.rdbTN.Text = "Tiềm năng";
            this.rdbTN.UseVisualStyleBackColor = true;
            // 
            // btnKH
            // 
            this.btnKH.AccessibleRole = System.Windows.Forms.AccessibleRole.PushButton;
            this.btnKH.Font = new System.Drawing.Font("Arial", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnKH.Location = new System.Drawing.Point(494, 98);
            this.btnKH.Name = "btnKH";
            this.btnKH.Size = new System.Drawing.Size(45, 23);
            this.btnKH.TabIndex = 374;
            this.btnKH.Text = "...";
            this.btnKH.Click += new System.EventHandler(this.btnKH_Click);
            // 
            // txtMaKH
            // 
            // 
            // 
            // 
            this.txtMaKH.Border.Class = "TextBoxBorder";
            this.txtMaKH.Location = new System.Drawing.Point(270, 95);
            this.txtMaKH.Name = "txtMaKH";
            this.txtMaKH.Size = new System.Drawing.Size(218, 26);
            this.txtMaKH.TabIndex = 375;
            // 
            // cbbCN
            // 
            this.cbbCN.DisplayMember = "Text";
            this.cbbCN.DrawMode = System.Windows.Forms.DrawMode.OwnerDrawFixed;
            this.cbbCN.FormattingEnabled = true;
            this.cbbCN.ItemHeight = 19;
            this.cbbCN.Location = new System.Drawing.Point(125, 12);
            this.cbbCN.Name = "cbbCN";
            this.cbbCN.Size = new System.Drawing.Size(363, 25);
            this.cbbCN.TabIndex = 376;
            // 
            // cbbCB
            // 
            this.cbbCB.DisplayMember = "Text";
            this.cbbCB.DrawMode = System.Windows.Forms.DrawMode.OwnerDrawFixed;
            this.cbbCB.FormattingEnabled = true;
            this.cbbCB.ItemHeight = 19;
            this.cbbCB.Location = new System.Drawing.Point(270, 129);
            this.cbbCB.Name = "cbbCB";
            this.cbbCB.Size = new System.Drawing.Size(218, 25);
            this.cbbCB.TabIndex = 376;
            // 
            // labelX1
            // 
            this.labelX1.Font = new System.Drawing.Font("Arial", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.labelX1.ForeColor = System.Drawing.SystemColors.ControlText;
            this.labelX1.Location = new System.Drawing.Point(59, 95);
            this.labelX1.Name = "labelX1";
            this.labelX1.Size = new System.Drawing.Size(60, 20);
            this.labelX1.TabIndex = 360;
            this.labelX1.Text = "Tiêu chí";
            // 
            // frmKH_TKGD
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(9F, 18F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(561, 216);
            this.Controls.Add(this.cbbCB);
            this.Controls.Add(this.cbbCN);
            this.Controls.Add(this.grbLoai);
            this.Controls.Add(this.groupBox1);
            this.Controls.Add(this.txtMaKH);
            this.Controls.Add(this.btnKH);
            this.Controls.Add(this.labelX3);
            this.Controls.Add(this.btnCancel);
            this.Controls.Add(this.labelX1);
            this.Controls.Add(this.labelX5);
            this.Controls.Add(this.btnIn);
            this.Font = new System.Drawing.Font("Arial", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.Margin = new System.Windows.Forms.Padding(4);
            this.Name = "frmKH_TKGD";
            this.ShowIcon = false;
            this.Text = "Thống kê giao dịch";
            this.Load += new System.EventHandler(this.frmKH_TKGD_Load);
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            this.grbLoai.ResumeLayout(false);
            this.grbLoai.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private DevComponents.DotNetBar.LabelX labelX3;
        private DevComponents.DotNetBar.ButtonX btnCancel;
        private DevComponents.DotNetBar.LabelX labelX5;
        private DevComponents.DotNetBar.ButtonX btnIn;
        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.RadioButton rdbKH;
        private System.Windows.Forms.RadioButton rdbCB;
        private System.Windows.Forms.GroupBox grbLoai;
        private System.Windows.Forms.RadioButton rdbHH;
        private System.Windows.Forms.RadioButton rdbTN;
        private DevComponents.DotNetBar.ButtonX btnKH;
        private DevComponents.DotNetBar.Controls.TextBoxX txtMaKH;
        private DevComponents.DotNetBar.Controls.ComboBoxEx cbbCN;
        private DevComponents.DotNetBar.Controls.ComboBoxEx cbbCB;
        private DevComponents.DotNetBar.LabelX labelX1;

    }
}