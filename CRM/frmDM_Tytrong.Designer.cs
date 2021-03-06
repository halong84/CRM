namespace CRM
{
    partial class frmDM_Tytrong
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
            this.dtpNgayKT = new System.Windows.Forms.DateTimePicker();
            this.rdbCN = new System.Windows.Forms.RadioButton();
            this.rdbDN = new System.Windows.Forms.RadioButton();
            this.dtpNgayBD = new System.Windows.Forms.DateTimePicker();
            this.labelX6 = new DevComponents.DotNetBar.LabelX();
            this.labelX5 = new DevComponents.DotNetBar.LabelX();
            this.txtTytrong = new DevComponents.DotNetBar.Controls.TextBoxX();
            this.labelX3 = new DevComponents.DotNetBar.LabelX();
            this.labelX2 = new DevComponents.DotNetBar.LabelX();
            this.cbbTenNhomCT = new DevComponents.DotNetBar.Controls.ComboBoxEx();
            this.labelX1 = new DevComponents.DotNetBar.LabelX();
            this.labelX7 = new DevComponents.DotNetBar.LabelX();
            this.btnModify = new DevComponents.DotNetBar.ButtonX();
            this.btnDel = new DevComponents.DotNetBar.ButtonX();
            this.cbbMaCN = new DevComponents.DotNetBar.Controls.ComboBoxEx();
            this.btnAdd = new DevComponents.DotNetBar.ButtonX();
            this.dgvDanhsach = new DevComponents.DotNetBar.Controls.DataGridViewX();
            this.txtMaNhomCT = new DevComponents.DotNetBar.Controls.TextBoxX();
            this.labelX4 = new DevComponents.DotNetBar.LabelX();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.txtTieuchi = new DevComponents.DotNetBar.Controls.TextBoxX();
            this.labelX8 = new DevComponents.DotNetBar.LabelX();
            this.btnExit = new DevComponents.DotNetBar.ButtonX();
            this.llbCopy = new System.Windows.Forms.LinkLabel();
            ((System.ComponentModel.ISupportInitialize)(this.dgvDanhsach)).BeginInit();
            this.groupBox1.SuspendLayout();
            this.SuspendLayout();
            // 
            // dtpNgayKT
            // 
            this.dtpNgayKT.CustomFormat = "dd/MM/yyyy";
            this.dtpNgayKT.Format = System.Windows.Forms.DateTimePickerFormat.Custom;
            this.dtpNgayKT.Location = new System.Drawing.Point(494, 186);
            this.dtpNgayKT.Name = "dtpNgayKT";
            this.dtpNgayKT.ShowUpDown = true;
            this.dtpNgayKT.Size = new System.Drawing.Size(110, 26);
            this.dtpNgayKT.TabIndex = 7;
            this.dtpNgayKT.Value = new System.DateTime(9998, 12, 31, 0, 0, 0, 0);
            // 
            // rdbCN
            // 
            this.rdbCN.AutoSize = true;
            this.rdbCN.Checked = true;
            this.rdbCN.Location = new System.Drawing.Point(6, 13);
            this.rdbCN.Name = "rdbCN";
            this.rdbCN.Size = new System.Drawing.Size(84, 22);
            this.rdbCN.TabIndex = 2;
            this.rdbCN.TabStop = true;
            this.rdbCN.Text = "Cá nhân";
            this.rdbCN.UseVisualStyleBackColor = true;
            this.rdbCN.CheckedChanged += new System.EventHandler(this.rdbCN_CheckedChanged);
            // 
            // rdbDN
            // 
            this.rdbDN.AutoSize = true;
            this.rdbDN.Location = new System.Drawing.Point(105, 13);
            this.rdbDN.Name = "rdbDN";
            this.rdbDN.Size = new System.Drawing.Size(123, 22);
            this.rdbDN.TabIndex = 3;
            this.rdbDN.TabStop = true;
            this.rdbDN.Text = "Doanh nghiệp";
            this.rdbDN.UseVisualStyleBackColor = true;
            // 
            // dtpNgayBD
            // 
            this.dtpNgayBD.CustomFormat = "dd/MM/yyyy";
            this.dtpNgayBD.Format = System.Windows.Forms.DateTimePickerFormat.Custom;
            this.dtpNgayBD.Location = new System.Drawing.Point(336, 186);
            this.dtpNgayBD.Name = "dtpNgayBD";
            this.dtpNgayBD.ShowUpDown = true;
            this.dtpNgayBD.Size = new System.Drawing.Size(110, 26);
            this.dtpNgayBD.TabIndex = 6;
            // 
            // labelX6
            // 
            this.labelX6.Location = new System.Drawing.Point(493, 157);
            this.labelX6.Name = "labelX6";
            this.labelX6.Size = new System.Drawing.Size(131, 23);
            this.labelX6.TabIndex = 191;
            this.labelX6.Text = "Ngày hết hiệu lực";
            // 
            // labelX5
            // 
            this.labelX5.ForeColor = System.Drawing.Color.Red;
            this.labelX5.Location = new System.Drawing.Point(336, 157);
            this.labelX5.Name = "labelX5";
            this.labelX5.Size = new System.Drawing.Size(124, 23);
            this.labelX5.TabIndex = 190;
            this.labelX5.Text = "<font color=\"#1F497D\">Ngày hiệu lực (</font><font color=\"#ED1C24\">*</font><font c" +
    "olor=\"#1F497D\">)</font>";
            // 
            // txtTytrong
            // 
            // 
            // 
            // 
            this.txtTytrong.Border.Class = "TextBoxBorder";
            this.txtTytrong.Location = new System.Drawing.Point(175, 186);
            this.txtTytrong.Name = "txtTytrong";
            this.txtTytrong.Size = new System.Drawing.Size(124, 26);
            this.txtTytrong.TabIndex = 5;
            this.txtTytrong.TextChanged += new System.EventHandler(this.txtTytrong_TextChanged);
            this.txtTytrong.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.txtTytrong_KeyPress);
            // 
            // labelX3
            // 
            this.labelX3.Location = new System.Drawing.Point(175, 157);
            this.labelX3.Name = "labelX3";
            this.labelX3.Size = new System.Drawing.Size(92, 23);
            this.labelX3.TabIndex = 188;
            this.labelX3.Text = "Tỷ trọng (%)";
            // 
            // labelX2
            // 
            this.labelX2.ForeColor = System.Drawing.Color.Red;
            this.labelX2.Location = new System.Drawing.Point(39, 125);
            this.labelX2.Name = "labelX2";
            this.labelX2.Size = new System.Drawing.Size(125, 23);
            this.labelX2.TabIndex = 186;
            this.labelX2.Text = "<font color=\"#1F497D\">Nhóm chỉ tiêu (</font><font color=\"#ED1C24\">*</font><font c" +
    "olor=\"#1F497D\">)</font>";
            this.labelX2.TextAlignment = System.Drawing.StringAlignment.Far;
            // 
            // cbbTenNhomCT
            // 
            this.cbbTenNhomCT.DisplayMember = "Text";
            this.cbbTenNhomCT.DrawMode = System.Windows.Forms.DrawMode.OwnerDrawFixed;
            this.cbbTenNhomCT.FormattingEnabled = true;
            this.cbbTenNhomCT.ItemHeight = 19;
            this.cbbTenNhomCT.Location = new System.Drawing.Point(300, 125);
            this.cbbTenNhomCT.Name = "cbbTenNhomCT";
            this.cbbTenNhomCT.Size = new System.Drawing.Size(352, 25);
            this.cbbTenNhomCT.TabIndex = 4;
            this.cbbTenNhomCT.SelectedIndexChanged += new System.EventHandler(this.cbbTenNhomCT_SelectedIndexChanged);
            this.cbbTenNhomCT.SelectionChangeCommitted += new System.EventHandler(this.cbbTenNhomCT_SelectionChangeCommitted);
            this.cbbTenNhomCT.TextChanged += new System.EventHandler(this.cbbTenNhomCT_TextChanged);
            // 
            // labelX1
            // 
            this.labelX1.ForeColor = System.Drawing.Color.Red;
            this.labelX1.Location = new System.Drawing.Point(81, 93);
            this.labelX1.Name = "labelX1";
            this.labelX1.Size = new System.Drawing.Size(88, 23);
            this.labelX1.TabIndex = 184;
            this.labelX1.Text = "<font color=\"#1F497D\">Loại KH (</font><font color=\"#ED1C24\">*</font><font color=\"" +
    "#1F497D\">)</font>";
            this.labelX1.TextAlignment = System.Drawing.StringAlignment.Far;
            // 
            // labelX7
            // 
            this.labelX7.ForeColor = System.Drawing.Color.Red;
            this.labelX7.Location = new System.Drawing.Point(63, 59);
            this.labelX7.Name = "labelX7";
            this.labelX7.Size = new System.Drawing.Size(101, 23);
            this.labelX7.TabIndex = 183;
            this.labelX7.Text = "<font color=\"#1F497D\">Chi nhánh (</font><font color=\"#ED1C24\">*</font><font color" +
    "=\"#1F497D\">)</font>";
            this.labelX7.TextAlignment = System.Drawing.StringAlignment.Far;
            // 
            // btnModify
            // 
            this.btnModify.AccessibleRole = System.Windows.Forms.AccessibleRole.PushButton;
            this.btnModify.Location = new System.Drawing.Point(549, 231);
            this.btnModify.Name = "btnModify";
            this.btnModify.Size = new System.Drawing.Size(75, 23);
            this.btnModify.TabIndex = 10;
            this.btnModify.Text = "Thay đổi";
            this.btnModify.Click += new System.EventHandler(this.btnModify_Click);
            // 
            // btnDel
            // 
            this.btnDel.AccessibleRole = System.Windows.Forms.AccessibleRole.PushButton;
            this.btnDel.Location = new System.Drawing.Point(549, 481);
            this.btnDel.Name = "btnDel";
            this.btnDel.Size = new System.Drawing.Size(75, 23);
            this.btnDel.TabIndex = 11;
            this.btnDel.Text = "Xóa";
            this.btnDel.Click += new System.EventHandler(this.btnDel_Click);
            // 
            // cbbMaCN
            // 
            this.cbbMaCN.DisplayMember = "Text";
            this.cbbMaCN.DrawMode = System.Windows.Forms.DrawMode.OwnerDrawFixed;
            this.cbbMaCN.FormattingEnabled = true;
            this.cbbMaCN.ItemHeight = 19;
            this.cbbMaCN.Location = new System.Drawing.Point(175, 56);
            this.cbbMaCN.Name = "cbbMaCN";
            this.cbbMaCN.Size = new System.Drawing.Size(477, 25);
            this.cbbMaCN.TabIndex = 1;
            this.cbbMaCN.SelectedIndexChanged += new System.EventHandler(this.cbbMaCN_SelectedIndexChanged);
            this.cbbMaCN.SelectionChangeCommitted += new System.EventHandler(this.cbbMaCN_SelectionChangeCommitted);
            // 
            // btnAdd
            // 
            this.btnAdd.AccessibleRole = System.Windows.Forms.AccessibleRole.PushButton;
            this.btnAdd.Location = new System.Drawing.Point(630, 231);
            this.btnAdd.Name = "btnAdd";
            this.btnAdd.Size = new System.Drawing.Size(75, 23);
            this.btnAdd.TabIndex = 9;
            this.btnAdd.Text = "Thêm";
            this.btnAdd.Click += new System.EventHandler(this.btnAdd_Click);
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
            this.dgvDanhsach.Location = new System.Drawing.Point(12, 260);
            this.dgvDanhsach.Name = "dgvDanhsach";
            dataGridViewCellStyle2.Font = new System.Drawing.Font("Arial", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.dgvDanhsach.RowsDefaultCellStyle = dataGridViewCellStyle2;
            this.dgvDanhsach.Size = new System.Drawing.Size(718, 215);
            this.dgvDanhsach.TabIndex = 12;
            this.dgvDanhsach.CellClick += new System.Windows.Forms.DataGridViewCellEventHandler(this.dgvDanhsach_CellClick);
            // 
            // txtMaNhomCT
            // 
            this.txtMaNhomCT.BackColor = System.Drawing.SystemColors.Control;
            // 
            // 
            // 
            this.txtMaNhomCT.Border.Class = "TextBoxBorder";
            this.txtMaNhomCT.Location = new System.Drawing.Point(175, 125);
            this.txtMaNhomCT.Name = "txtMaNhomCT";
            this.txtMaNhomCT.Size = new System.Drawing.Size(124, 26);
            this.txtMaNhomCT.TabIndex = 177;
            // 
            // labelX4
            // 
            this.labelX4.Location = new System.Drawing.Point(255, 7);
            this.labelX4.Margin = new System.Windows.Forms.Padding(4);
            this.labelX4.Name = "labelX4";
            this.labelX4.Size = new System.Drawing.Size(243, 32);
            this.labelX4.TabIndex = 181;
            this.labelX4.Text = "Bảng: Tỷ trọng điểm các chỉ tiêu";
            this.labelX4.TextAlignment = System.Drawing.StringAlignment.Center;
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.rdbDN);
            this.groupBox1.Controls.Add(this.rdbCN);
            this.groupBox1.Location = new System.Drawing.Point(175, 80);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(232, 39);
            this.groupBox1.TabIndex = 187;
            this.groupBox1.TabStop = false;
            // 
            // txtTieuchi
            // 
            // 
            // 
            // 
            this.txtTieuchi.Border.Class = "TextBoxBorder";
            this.txtTieuchi.Location = new System.Drawing.Point(175, 228);
            this.txtTieuchi.Name = "txtTieuchi";
            this.txtTieuchi.Size = new System.Drawing.Size(323, 26);
            this.txtTieuchi.TabIndex = 8;
            // 
            // labelX8
            // 
            this.labelX8.Location = new System.Drawing.Point(102, 228);
            this.labelX8.Name = "labelX8";
            this.labelX8.Size = new System.Drawing.Size(62, 23);
            this.labelX8.TabIndex = 195;
            this.labelX8.Text = "Tiêu chí";
            this.labelX8.TextAlignment = System.Drawing.StringAlignment.Far;
            // 
            // btnExit
            // 
            this.btnExit.AccessibleRole = System.Windows.Forms.AccessibleRole.PushButton;
            this.btnExit.Location = new System.Drawing.Point(630, 481);
            this.btnExit.Name = "btnExit";
            this.btnExit.Size = new System.Drawing.Size(75, 23);
            this.btnExit.TabIndex = 196;
            this.btnExit.Text = "Đóng";
            this.btnExit.Click += new System.EventHandler(this.btnExit_Click);
            // 
            // llbCopy
            // 
            this.llbCopy.AutoSize = true;
            this.llbCopy.Location = new System.Drawing.Point(24, 486);
            this.llbCopy.Name = "llbCopy";
            this.llbCopy.Size = new System.Drawing.Size(145, 18);
            this.llbCopy.TabIndex = 197;
            this.llbCopy.TabStop = true;
            this.llbCopy.Text = "Sao chép từ Hội sở";
            this.llbCopy.LinkClicked += new System.Windows.Forms.LinkLabelLinkClickedEventHandler(this.llbCopy_LinkClicked);
            // 
            // frmDM_Tytrong
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(9F, 18F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(742, 516);
            this.Controls.Add(this.llbCopy);
            this.Controls.Add(this.btnExit);
            this.Controls.Add(this.labelX8);
            this.Controls.Add(this.txtTieuchi);
            this.Controls.Add(this.dtpNgayKT);
            this.Controls.Add(this.dtpNgayBD);
            this.Controls.Add(this.labelX6);
            this.Controls.Add(this.labelX5);
            this.Controls.Add(this.txtTytrong);
            this.Controls.Add(this.labelX3);
            this.Controls.Add(this.labelX2);
            this.Controls.Add(this.cbbTenNhomCT);
            this.Controls.Add(this.labelX1);
            this.Controls.Add(this.labelX7);
            this.Controls.Add(this.btnModify);
            this.Controls.Add(this.btnDel);
            this.Controls.Add(this.cbbMaCN);
            this.Controls.Add(this.btnAdd);
            this.Controls.Add(this.dgvDanhsach);
            this.Controls.Add(this.txtMaNhomCT);
            this.Controls.Add(this.labelX4);
            this.Controls.Add(this.groupBox1);
            this.Font = new System.Drawing.Font("Arial", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.Margin = new System.Windows.Forms.Padding(4);
            this.Name = "frmDM_Tytrong";
            this.ShowIcon = false;
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Tỷ trọng điểm các chỉ tiêu";
            this.Load += new System.EventHandler(this.frmDM_Tytrong_Load);
            ((System.ComponentModel.ISupportInitialize)(this.dgvDanhsach)).EndInit();
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.DateTimePicker dtpNgayKT;
        private System.Windows.Forms.RadioButton rdbCN;
        private System.Windows.Forms.RadioButton rdbDN;
        private System.Windows.Forms.DateTimePicker dtpNgayBD;
        private DevComponents.DotNetBar.LabelX labelX6;
        private DevComponents.DotNetBar.LabelX labelX5;
        private DevComponents.DotNetBar.Controls.TextBoxX txtTytrong;
        private DevComponents.DotNetBar.LabelX labelX3;
        private DevComponents.DotNetBar.LabelX labelX2;
        private DevComponents.DotNetBar.Controls.ComboBoxEx cbbTenNhomCT;
        private DevComponents.DotNetBar.LabelX labelX1;
        private DevComponents.DotNetBar.LabelX labelX7;
        private DevComponents.DotNetBar.ButtonX btnModify;
        private DevComponents.DotNetBar.ButtonX btnDel;
        private DevComponents.DotNetBar.Controls.ComboBoxEx cbbMaCN;
        private DevComponents.DotNetBar.ButtonX btnAdd;
        private DevComponents.DotNetBar.Controls.DataGridViewX dgvDanhsach;
        private DevComponents.DotNetBar.Controls.TextBoxX txtMaNhomCT;
        private DevComponents.DotNetBar.LabelX labelX4;
        private System.Windows.Forms.GroupBox groupBox1;
        private DevComponents.DotNetBar.Controls.TextBoxX txtTieuchi;
        private DevComponents.DotNetBar.LabelX labelX8;
        private DevComponents.DotNetBar.ButtonX btnExit;
        private System.Windows.Forms.LinkLabel llbCopy;


    }
}