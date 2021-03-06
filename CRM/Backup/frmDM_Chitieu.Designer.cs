namespace CRM
{
    partial class frmDM_Chitieu
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
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle1 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle2 = new System.Windows.Forms.DataGridViewCellStyle();
            this.labelX1 = new DevComponents.DotNetBar.LabelX();
            this.txtTenCT = new DevComponents.DotNetBar.Controls.TextBoxX();
            this.labelX7 = new DevComponents.DotNetBar.LabelX();
            this.btnModify = new DevComponents.DotNetBar.ButtonX();
            this.btnDel = new DevComponents.DotNetBar.ButtonX();
            this.cbbTenNhomCT = new DevComponents.DotNetBar.Controls.ComboBoxEx();
            this.btnAdd = new DevComponents.DotNetBar.ButtonX();
            this.labelX5 = new DevComponents.DotNetBar.LabelX();
            this.dgvDanhsach = new DevComponents.DotNetBar.Controls.DataGridViewX();
            this.txtGiatri = new DevComponents.DotNetBar.Controls.TextBoxX();
            this.labelX4 = new DevComponents.DotNetBar.LabelX();
            this.cbbDonvi = new DevComponents.DotNetBar.Controls.ComboBoxEx();
            this.labelX2 = new DevComponents.DotNetBar.LabelX();
            this.cbbMaCT = new DevComponents.DotNetBar.Controls.ComboBoxEx();
            this.labelX3 = new DevComponents.DotNetBar.LabelX();
            this.txtMaNhomCT = new DevComponents.DotNetBar.Controls.TextBoxX();
            this.labelX6 = new DevComponents.DotNetBar.LabelX();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.rdbDN = new System.Windows.Forms.RadioButton();
            this.rdbCN = new System.Windows.Forms.RadioButton();
            this.btnExit = new DevComponents.DotNetBar.ButtonX();
            ((System.ComponentModel.ISupportInitialize)(this.dgvDanhsach)).BeginInit();
            this.groupBox1.SuspendLayout();
            this.SuspendLayout();
            // 
            // labelX1
            // 
            this.labelX1.Location = new System.Drawing.Point(114, 155);
            this.labelX1.Name = "labelX1";
            this.labelX1.Size = new System.Drawing.Size(57, 23);
            this.labelX1.TabIndex = 145;
            this.labelX1.Text = "Tên CT";
            this.labelX1.TextAlignment = System.Drawing.StringAlignment.Far;
            // 
            // txtTenCT
            // 
            // 
            // 
            // 
            this.txtTenCT.Border.Class = "TextBoxBorder";
            this.txtTenCT.Location = new System.Drawing.Point(190, 152);
            this.txtTenCT.Name = "txtTenCT";
            this.txtTenCT.Size = new System.Drawing.Size(477, 26);
            this.txtTenCT.TabIndex = 7;
            // 
            // labelX7
            // 
            this.labelX7.Location = new System.Drawing.Point(99, 53);
            this.labelX7.Name = "labelX7";
            this.labelX7.Size = new System.Drawing.Size(72, 23);
            this.labelX7.TabIndex = 144;
            this.labelX7.Text = "Nhóm CT";
            this.labelX7.TextAlignment = System.Drawing.StringAlignment.Far;
            // 
            // btnModify
            // 
            this.btnModify.AccessibleRole = System.Windows.Forms.AccessibleRole.PushButton;
            this.btnModify.Location = new System.Drawing.Point(511, 189);
            this.btnModify.Name = "btnModify";
            this.btnModify.Size = new System.Drawing.Size(75, 23);
            this.btnModify.TabIndex = 9;
            this.btnModify.Text = "Thay đổi";
            this.btnModify.Click += new System.EventHandler(this.btnModify_Click);
            // 
            // btnDel
            // 
            this.btnDel.AccessibleRole = System.Windows.Forms.AccessibleRole.PushButton;
            this.btnDel.Location = new System.Drawing.Point(511, 481);
            this.btnDel.Name = "btnDel";
            this.btnDel.Size = new System.Drawing.Size(75, 23);
            this.btnDel.TabIndex = 10;
            this.btnDel.Text = "Xóa";
            this.btnDel.Click += new System.EventHandler(this.btnDel_Click);
            // 
            // cbbTenNhomCT
            // 
            this.cbbTenNhomCT.DisplayMember = "Text";
            this.cbbTenNhomCT.DrawMode = System.Windows.Forms.DrawMode.OwnerDrawFixed;
            this.cbbTenNhomCT.FormattingEnabled = true;
            this.cbbTenNhomCT.ItemHeight = 19;
            this.cbbTenNhomCT.Location = new System.Drawing.Point(266, 50);
            this.cbbTenNhomCT.Name = "cbbTenNhomCT";
            this.cbbTenNhomCT.Size = new System.Drawing.Size(401, 25);
            this.cbbTenNhomCT.TabIndex = 1;
            this.cbbTenNhomCT.SelectionChangeCommitted += new System.EventHandler(this.cbbTenNhomCT_SelectionChangeCommitted);
            this.cbbTenNhomCT.SelectedIndexChanged += new System.EventHandler(this.cbbTenNhomCT_SelectedIndexChanged);
            this.cbbTenNhomCT.TextChanged += new System.EventHandler(this.cbbTenNhomCT_TextChanged);
            // 
            // btnAdd
            // 
            this.btnAdd.AccessibleRole = System.Windows.Forms.AccessibleRole.PushButton;
            this.btnAdd.Location = new System.Drawing.Point(592, 189);
            this.btnAdd.Name = "btnAdd";
            this.btnAdd.Size = new System.Drawing.Size(75, 23);
            this.btnAdd.TabIndex = 8;
            this.btnAdd.Text = "Thêm";
            this.btnAdd.Click += new System.EventHandler(this.btnAdd_Click);
            // 
            // labelX5
            // 
            this.labelX5.Location = new System.Drawing.Point(484, 84);
            this.labelX5.Name = "labelX5";
            this.labelX5.Size = new System.Drawing.Size(47, 23);
            this.labelX5.TabIndex = 143;
            this.labelX5.Text = "Giá trị";
            this.labelX5.TextAlignment = System.Drawing.StringAlignment.Far;
            // 
            // dgvDanhsach
            // 
            this.dgvDanhsach.BackgroundColor = System.Drawing.SystemColors.Control;
            this.dgvDanhsach.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            dataGridViewCellStyle1.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle1.BackColor = System.Drawing.SystemColors.Window;
            dataGridViewCellStyle1.Font = new System.Drawing.Font("Arial", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            dataGridViewCellStyle1.ForeColor = System.Drawing.SystemColors.ControlText;
            dataGridViewCellStyle1.SelectionBackColor = System.Drawing.SystemColors.Highlight;
            dataGridViewCellStyle1.SelectionForeColor = System.Drawing.SystemColors.ControlText;
            dataGridViewCellStyle1.WrapMode = System.Windows.Forms.DataGridViewTriState.False;
            this.dgvDanhsach.DefaultCellStyle = dataGridViewCellStyle1;
            this.dgvDanhsach.GridColor = System.Drawing.Color.FromArgb(((int)(((byte)(208)))), ((int)(((byte)(215)))), ((int)(((byte)(229)))));
            this.dgvDanhsach.Location = new System.Drawing.Point(12, 224);
            this.dgvDanhsach.Name = "dgvDanhsach";
            dataGridViewCellStyle2.Font = new System.Drawing.Font("Arial", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.dgvDanhsach.RowsDefaultCellStyle = dataGridViewCellStyle2;
            this.dgvDanhsach.Size = new System.Drawing.Size(718, 242);
            this.dgvDanhsach.TabIndex = 11;
            this.dgvDanhsach.CellClick += new System.Windows.Forms.DataGridViewCellEventHandler(this.dgvDanhsach_CellClick);
            // 
            // txtGiatri
            // 
            // 
            // 
            // 
            this.txtGiatri.Border.Class = "TextBoxBorder";
            this.txtGiatri.Location = new System.Drawing.Point(537, 81);
            this.txtGiatri.Name = "txtGiatri";
            this.txtGiatri.Size = new System.Drawing.Size(130, 26);
            this.txtGiatri.TabIndex = 5;
            this.txtGiatri.TextChanged += new System.EventHandler(this.txtGiatri_TextChanged);
            this.txtGiatri.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.txtGiatri_KeyPress);
            // 
            // labelX4
            // 
            this.labelX4.Location = new System.Drawing.Point(207, 13);
            this.labelX4.Margin = new System.Windows.Forms.Padding(4);
            this.labelX4.Name = "labelX4";
            this.labelX4.Size = new System.Drawing.Size(286, 20);
            this.labelX4.TabIndex = 141;
            this.labelX4.Text = "Bảng: Mã chỉ tiêu phân loại khách hàng";
            this.labelX4.TextAlignment = System.Drawing.StringAlignment.Center;
            // 
            // cbbDonvi
            // 
            this.cbbDonvi.DisplayMember = "Text";
            this.cbbDonvi.DrawMode = System.Windows.Forms.DrawMode.OwnerDrawFixed;
            this.cbbDonvi.FormattingEnabled = true;
            this.cbbDonvi.ItemHeight = 19;
            this.cbbDonvi.Location = new System.Drawing.Point(537, 113);
            this.cbbDonvi.Name = "cbbDonvi";
            this.cbbDonvi.Size = new System.Drawing.Size(130, 25);
            this.cbbDonvi.TabIndex = 6;
            // 
            // labelX2
            // 
            this.labelX2.ForeColor = System.Drawing.Color.Red;
            this.labelX2.Location = new System.Drawing.Point(99, 84);
            this.labelX2.Name = "labelX2";
            this.labelX2.Size = new System.Drawing.Size(72, 23);
            this.labelX2.TabIndex = 148;
            this.labelX2.Text = "<font color=\"#1F497D\">Mã CT (</font><font color=\"#ED1C24\">*</font><font color=\"#1" +
                "F497D\">)</font>";
            this.labelX2.TextAlignment = System.Drawing.StringAlignment.Far;
            // 
            // cbbMaCT
            // 
            this.cbbMaCT.DisplayMember = "Text";
            this.cbbMaCT.DrawMode = System.Windows.Forms.DrawMode.OwnerDrawFixed;
            this.cbbMaCT.FormattingEnabled = true;
            this.cbbMaCT.ItemHeight = 19;
            this.cbbMaCT.Location = new System.Drawing.Point(190, 82);
            this.cbbMaCT.Name = "cbbMaCT";
            this.cbbMaCT.Size = new System.Drawing.Size(130, 25);
            this.cbbMaCT.TabIndex = 2;
            this.cbbMaCT.SelectedIndexChanged += new System.EventHandler(this.cbbMaCT_SelectedIndexChanged);
            this.cbbMaCT.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.cbbMaCT_KeyPress);
            this.cbbMaCT.TextChanged += new System.EventHandler(this.cbbMaCT_TextChanged);
            // 
            // labelX3
            // 
            this.labelX3.Location = new System.Drawing.Point(473, 115);
            this.labelX3.Name = "labelX3";
            this.labelX3.Size = new System.Drawing.Size(58, 23);
            this.labelX3.TabIndex = 149;
            this.labelX3.Text = "Đơn vị";
            this.labelX3.TextAlignment = System.Drawing.StringAlignment.Far;
            // 
            // txtMaNhomCT
            // 
            this.txtMaNhomCT.BackColor = System.Drawing.SystemColors.Control;
            // 
            // 
            // 
            this.txtMaNhomCT.Border.Class = "TextBoxBorder";
            this.txtMaNhomCT.Enabled = false;
            this.txtMaNhomCT.Location = new System.Drawing.Point(190, 50);
            this.txtMaNhomCT.Name = "txtMaNhomCT";
            this.txtMaNhomCT.Size = new System.Drawing.Size(75, 26);
            this.txtMaNhomCT.TabIndex = 150;
            // 
            // labelX6
            // 
            this.labelX6.Location = new System.Drawing.Point(111, 116);
            this.labelX6.Name = "labelX6";
            this.labelX6.Size = new System.Drawing.Size(60, 23);
            this.labelX6.TabIndex = 151;
            this.labelX6.Text = "Loại KH";
            this.labelX6.TextAlignment = System.Drawing.StringAlignment.Far;
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.rdbDN);
            this.groupBox1.Controls.Add(this.rdbCN);
            this.groupBox1.Location = new System.Drawing.Point(190, 104);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(227, 39);
            this.groupBox1.TabIndex = 170;
            this.groupBox1.TabStop = false;
            // 
            // rdbDN
            // 
            this.rdbDN.AutoSize = true;
            this.rdbDN.ForeColor = System.Drawing.Color.Navy;
            this.rdbDN.Location = new System.Drawing.Point(100, 13);
            this.rdbDN.Name = "rdbDN";
            this.rdbDN.Size = new System.Drawing.Size(123, 22);
            this.rdbDN.TabIndex = 4;
            this.rdbDN.TabStop = true;
            this.rdbDN.Text = "Doanh nghiệp";
            this.rdbDN.UseVisualStyleBackColor = true;
            // 
            // rdbCN
            // 
            this.rdbCN.AutoSize = true;
            this.rdbCN.Checked = true;
            this.rdbCN.ForeColor = System.Drawing.Color.Navy;
            this.rdbCN.Location = new System.Drawing.Point(6, 13);
            this.rdbCN.Name = "rdbCN";
            this.rdbCN.Size = new System.Drawing.Size(84, 22);
            this.rdbCN.TabIndex = 3;
            this.rdbCN.TabStop = true;
            this.rdbCN.Text = "Cá nhân";
            this.rdbCN.UseVisualStyleBackColor = true;
            this.rdbCN.CheckedChanged += new System.EventHandler(this.rdbCN_CheckedChanged);
            // 
            // btnExit
            // 
            this.btnExit.AccessibleRole = System.Windows.Forms.AccessibleRole.PushButton;
            this.btnExit.Location = new System.Drawing.Point(592, 481);
            this.btnExit.Name = "btnExit";
            this.btnExit.Size = new System.Drawing.Size(75, 23);
            this.btnExit.TabIndex = 171;
            this.btnExit.Text = "Đóng";
            this.btnExit.Click += new System.EventHandler(this.btnExit_Click);
            // 
            // frmDM_Chitieu
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(9F, 18F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(742, 516);
            this.Controls.Add(this.btnExit);
            this.Controls.Add(this.labelX6);
            this.Controls.Add(this.txtMaNhomCT);
            this.Controls.Add(this.labelX3);
            this.Controls.Add(this.labelX2);
            this.Controls.Add(this.cbbMaCT);
            this.Controls.Add(this.cbbDonvi);
            this.Controls.Add(this.labelX1);
            this.Controls.Add(this.txtTenCT);
            this.Controls.Add(this.labelX7);
            this.Controls.Add(this.btnModify);
            this.Controls.Add(this.btnDel);
            this.Controls.Add(this.cbbTenNhomCT);
            this.Controls.Add(this.btnAdd);
            this.Controls.Add(this.labelX5);
            this.Controls.Add(this.dgvDanhsach);
            this.Controls.Add(this.txtGiatri);
            this.Controls.Add(this.labelX4);
            this.Controls.Add(this.groupBox1);
            this.Font = new System.Drawing.Font("Arial", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.Margin = new System.Windows.Forms.Padding(4);
            this.Name = "frmDM_Chitieu";
            this.Text = "Danh muc - Chi tieu";
            this.Load += new System.EventHandler(this.frmDM_Chitieu_Load);
            ((System.ComponentModel.ISupportInitialize)(this.dgvDanhsach)).EndInit();
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private DevComponents.DotNetBar.LabelX labelX1;
        private DevComponents.DotNetBar.Controls.TextBoxX txtTenCT;
        private DevComponents.DotNetBar.LabelX labelX7;
        private DevComponents.DotNetBar.ButtonX btnModify;
        private DevComponents.DotNetBar.ButtonX btnDel;
        private DevComponents.DotNetBar.Controls.ComboBoxEx cbbTenNhomCT;
        private DevComponents.DotNetBar.ButtonX btnAdd;
        private DevComponents.DotNetBar.LabelX labelX5;
        private DevComponents.DotNetBar.Controls.DataGridViewX dgvDanhsach;
        private DevComponents.DotNetBar.Controls.TextBoxX txtGiatri;
        private DevComponents.DotNetBar.LabelX labelX4;
        private DevComponents.DotNetBar.Controls.ComboBoxEx cbbDonvi;
        private DevComponents.DotNetBar.LabelX labelX2;
        private DevComponents.DotNetBar.Controls.ComboBoxEx cbbMaCT;
        private DevComponents.DotNetBar.LabelX labelX3;
        private DevComponents.DotNetBar.Controls.TextBoxX txtMaNhomCT;
        private DevComponents.DotNetBar.LabelX labelX6;
        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.RadioButton rdbDN;
        private System.Windows.Forms.RadioButton rdbCN;
        private DevComponents.DotNetBar.ButtonX btnExit;
    }
}