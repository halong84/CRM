namespace CRM
{
    partial class frmNhanvien
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
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle3 = new System.Windows.Forms.DataGridViewCellStyle();
            this.labelX3 = new DevComponents.DotNetBar.LabelX();
            this.txtMaNV = new DevComponents.DotNetBar.Controls.TextBoxX();
            this.labelX4 = new DevComponents.DotNetBar.LabelX();
            this.txtTenNV = new DevComponents.DotNetBar.Controls.TextBoxX();
            this.labelX5 = new DevComponents.DotNetBar.LabelX();
            this.cbbChucvu = new DevComponents.DotNetBar.Controls.ComboBoxEx();
            this.dgvDanhsach = new DevComponents.DotNetBar.Controls.DataGridViewX();
            this.btnAdd = new DevComponents.DotNetBar.ButtonX();
            this.btnModify = new DevComponents.DotNetBar.ButtonX();
            this.btnDel = new DevComponents.DotNetBar.ButtonX();
            this.cbbPhong = new DevComponents.DotNetBar.Controls.ComboBoxEx();
            this.labelX2 = new DevComponents.DotNetBar.LabelX();
            this.cbbMaCN = new DevComponents.DotNetBar.Controls.ComboBoxEx();
            this.labelX7 = new DevComponents.DotNetBar.LabelX();
            this.btnClose = new DevComponents.DotNetBar.ButtonX();
            this.labelX1 = new DevComponents.DotNetBar.LabelX();
            this.txtGiayuyquyen = new DevComponents.DotNetBar.Controls.TextBoxX();
            this.labelX6 = new DevComponents.DotNetBar.LabelX();
            this.txtNgaysinh = new DevComponents.DotNetBar.Controls.TextBoxX();
            this.txtHoatdong = new DevComponents.DotNetBar.Controls.TextBoxX();
            ((System.ComponentModel.ISupportInitialize)(this.dgvDanhsach)).BeginInit();
            this.SuspendLayout();
            // 
            // labelX3
            // 
            this.labelX3.Location = new System.Drawing.Point(35, 108);
            this.labelX3.Name = "labelX3";
            this.labelX3.Size = new System.Drawing.Size(78, 23);
            this.labelX3.TabIndex = 5;
            this.labelX3.Text = "Mã NV (<font color=\"#ED1C24\">*</font>)";
            this.labelX3.TextAlignment = System.Drawing.StringAlignment.Far;
            // 
            // txtMaNV
            // 
            // 
            // 
            // 
            this.txtMaNV.Border.Class = "TextBoxBorder";
            this.txtMaNV.Location = new System.Drawing.Point(124, 106);
            this.txtMaNV.Name = "txtMaNV";
            this.txtMaNV.Size = new System.Drawing.Size(389, 26);
            this.txtMaNV.TabIndex = 6;
            // 
            // labelX4
            // 
            this.labelX4.Location = new System.Drawing.Point(56, 76);
            this.labelX4.Name = "labelX4";
            this.labelX4.Size = new System.Drawing.Size(57, 23);
            this.labelX4.TabIndex = 7;
            this.labelX4.Text = "Họ tên";
            this.labelX4.TextAlignment = System.Drawing.StringAlignment.Far;
            // 
            // txtTenNV
            // 
            // 
            // 
            // 
            this.txtTenNV.Border.Class = "TextBoxBorder";
            this.txtTenNV.Location = new System.Drawing.Point(124, 74);
            this.txtTenNV.Name = "txtTenNV";
            this.txtTenNV.Size = new System.Drawing.Size(389, 26);
            this.txtTenNV.TabIndex = 8;
            // 
            // labelX5
            // 
            this.labelX5.Location = new System.Drawing.Point(48, 140);
            this.labelX5.Name = "labelX5";
            this.labelX5.Size = new System.Drawing.Size(65, 21);
            this.labelX5.TabIndex = 9;
            this.labelX5.Text = "Chức vụ";
            this.labelX5.TextAlignment = System.Drawing.StringAlignment.Far;
            // 
            // cbbChucvu
            // 
            this.cbbChucvu.DisplayMember = "Text";
            this.cbbChucvu.DrawMode = System.Windows.Forms.DrawMode.OwnerDrawFixed;
            this.cbbChucvu.FormattingEnabled = true;
            this.cbbChucvu.ItemHeight = 19;
            this.cbbChucvu.Location = new System.Drawing.Point(124, 138);
            this.cbbChucvu.Name = "cbbChucvu";
            this.cbbChucvu.Size = new System.Drawing.Size(389, 25);
            this.cbbChucvu.TabIndex = 10;
            // 
            // dgvDanhsach
            // 
            this.dgvDanhsach.BackgroundColor = System.Drawing.SystemColors.Control;
            this.dgvDanhsach.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            dataGridViewCellStyle3.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle3.BackColor = System.Drawing.SystemColors.Window;
            dataGridViewCellStyle3.Font = new System.Drawing.Font("Arial", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            dataGridViewCellStyle3.ForeColor = System.Drawing.SystemColors.ControlText;
            dataGridViewCellStyle3.SelectionBackColor = System.Drawing.SystemColors.Highlight;
            dataGridViewCellStyle3.SelectionForeColor = System.Drawing.SystemColors.ControlText;
            dataGridViewCellStyle3.WrapMode = System.Windows.Forms.DataGridViewTriState.False;
            this.dgvDanhsach.DefaultCellStyle = dataGridViewCellStyle3;
            this.dgvDanhsach.GridColor = System.Drawing.Color.FromArgb(((int)(((byte)(208)))), ((int)(((byte)(215)))), ((int)(((byte)(229)))));
            this.dgvDanhsach.Location = new System.Drawing.Point(12, 298);
            this.dgvDanhsach.Name = "dgvDanhsach";
            this.dgvDanhsach.Size = new System.Drawing.Size(501, 201);
            this.dgvDanhsach.TabIndex = 13;
            this.dgvDanhsach.CellClick += new System.Windows.Forms.DataGridViewCellEventHandler(this.dgvDanhsach_CellClick);
            // 
            // btnAdd
            // 
            this.btnAdd.AccessibleRole = System.Windows.Forms.AccessibleRole.PushButton;
            this.btnAdd.Location = new System.Drawing.Point(267, 269);
            this.btnAdd.Name = "btnAdd";
            this.btnAdd.Size = new System.Drawing.Size(75, 23);
            this.btnAdd.TabIndex = 14;
            this.btnAdd.Text = "Thêm";
            this.btnAdd.Click += new System.EventHandler(this.btnAdd_Click);
            // 
            // btnModify
            // 
            this.btnModify.AccessibleRole = System.Windows.Forms.AccessibleRole.PushButton;
            this.btnModify.Location = new System.Drawing.Point(186, 269);
            this.btnModify.Name = "btnModify";
            this.btnModify.Size = new System.Drawing.Size(75, 23);
            this.btnModify.TabIndex = 15;
            this.btnModify.Text = "Thay đổi";
            this.btnModify.Click += new System.EventHandler(this.btnModify_Click);
            // 
            // btnDel
            // 
            this.btnDel.AccessibleRole = System.Windows.Forms.AccessibleRole.PushButton;
            this.btnDel.Location = new System.Drawing.Point(186, 514);
            this.btnDel.Name = "btnDel";
            this.btnDel.Size = new System.Drawing.Size(75, 23);
            this.btnDel.TabIndex = 16;
            this.btnDel.Text = "Xóa";
            this.btnDel.Click += new System.EventHandler(this.btnDel_Click);
            // 
            // cbbPhong
            // 
            this.cbbPhong.DisplayMember = "Text";
            this.cbbPhong.DrawMode = System.Windows.Forms.DrawMode.OwnerDrawFixed;
            this.cbbPhong.FormattingEnabled = true;
            this.cbbPhong.ItemHeight = 19;
            this.cbbPhong.Location = new System.Drawing.Point(124, 43);
            this.cbbPhong.Name = "cbbPhong";
            this.cbbPhong.Size = new System.Drawing.Size(389, 25);
            this.cbbPhong.TabIndex = 11;
            this.cbbPhong.SelectionChangeCommitted += new System.EventHandler(this.cbbPhong_SelectionChangeCommitted);
            // 
            // labelX2
            // 
            this.labelX2.Font = new System.Drawing.Font("Arial", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.labelX2.Location = new System.Drawing.Point(35, 43);
            this.labelX2.Margin = new System.Windows.Forms.Padding(4);
            this.labelX2.Name = "labelX2";
            this.labelX2.Size = new System.Drawing.Size(78, 25);
            this.labelX2.TabIndex = 3;
            this.labelX2.Text = "Phòng ban";
            // 
            // cbbMaCN
            // 
            this.cbbMaCN.DisplayMember = "Text";
            this.cbbMaCN.DrawMode = System.Windows.Forms.DrawMode.OwnerDrawFixed;
            this.cbbMaCN.FormattingEnabled = true;
            this.cbbMaCN.ItemHeight = 19;
            this.cbbMaCN.Location = new System.Drawing.Point(124, 12);
            this.cbbMaCN.Name = "cbbMaCN";
            this.cbbMaCN.Size = new System.Drawing.Size(389, 25);
            this.cbbMaCN.TabIndex = 161;
            this.cbbMaCN.SelectionChangeCommitted += new System.EventHandler(this.cbbMaCN_SelectionChangeCommitted);
            // 
            // labelX7
            // 
            this.labelX7.ForeColor = System.Drawing.SystemColors.ControlText;
            this.labelX7.Location = new System.Drawing.Point(35, 13);
            this.labelX7.Name = "labelX7";
            this.labelX7.Size = new System.Drawing.Size(78, 23);
            this.labelX7.TabIndex = 162;
            this.labelX7.Text = "Chi nhánh";
            this.labelX7.TextAlignment = System.Drawing.StringAlignment.Far;
            // 
            // btnClose
            // 
            this.btnClose.AccessibleRole = System.Windows.Forms.AccessibleRole.PushButton;
            this.btnClose.Location = new System.Drawing.Point(267, 514);
            this.btnClose.Name = "btnClose";
            this.btnClose.Size = new System.Drawing.Size(75, 23);
            this.btnClose.TabIndex = 163;
            this.btnClose.Text = "Đóng";
            this.btnClose.Click += new System.EventHandler(this.btnClose_Click);
            // 
            // labelX1
            // 
            this.labelX1.Location = new System.Drawing.Point(-1, 187);
            this.labelX1.Name = "labelX1";
            this.labelX1.Size = new System.Drawing.Size(114, 21);
            this.labelX1.TabIndex = 164;
            this.labelX1.Text = "Giấy ủy quyền";
            this.labelX1.TextAlignment = System.Drawing.StringAlignment.Far;
            // 
            // txtGiayuyquyen
            // 
            // 
            // 
            // 
            this.txtGiayuyquyen.Border.Class = "TextBoxBorder";
            this.txtGiayuyquyen.Location = new System.Drawing.Point(124, 169);
            this.txtGiayuyquyen.Multiline = true;
            this.txtGiayuyquyen.Name = "txtGiayuyquyen";
            this.txtGiayuyquyen.Size = new System.Drawing.Size(389, 56);
            this.txtGiayuyquyen.TabIndex = 165;
            // 
            // labelX6
            // 
            this.labelX6.Location = new System.Drawing.Point(-1, 234);
            this.labelX6.Name = "labelX6";
            this.labelX6.Size = new System.Drawing.Size(114, 21);
            this.labelX6.TabIndex = 166;
            this.labelX6.Text = "Ngày sinh";
            this.labelX6.TextAlignment = System.Drawing.StringAlignment.Far;
            // 
            // txtNgaysinh
            // 
            // 
            // 
            // 
            this.txtNgaysinh.Border.Class = "TextBoxBorder";
            this.txtNgaysinh.Location = new System.Drawing.Point(124, 231);
            this.txtNgaysinh.Name = "txtNgaysinh";
            this.txtNgaysinh.ReadOnly = true;
            this.txtNgaysinh.Size = new System.Drawing.Size(164, 26);
            this.txtNgaysinh.TabIndex = 167;
            // 
            // txtHoatdong
            // 
            // 
            // 
            // 
            this.txtHoatdong.Border.Class = "TextBoxBorder";
            this.txtHoatdong.Location = new System.Drawing.Point(294, 231);
            this.txtHoatdong.Name = "txtHoatdong";
            this.txtHoatdong.ReadOnly = true;
            this.txtHoatdong.Size = new System.Drawing.Size(219, 26);
            this.txtHoatdong.TabIndex = 168;
            // 
            // frmNhanvien
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(9F, 18F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(525, 544);
            this.Controls.Add(this.txtHoatdong);
            this.Controls.Add(this.txtNgaysinh);
            this.Controls.Add(this.labelX6);
            this.Controls.Add(this.txtGiayuyquyen);
            this.Controls.Add(this.labelX1);
            this.Controls.Add(this.btnClose);
            this.Controls.Add(this.labelX7);
            this.Controls.Add(this.cbbMaCN);
            this.Controls.Add(this.labelX2);
            this.Controls.Add(this.btnDel);
            this.Controls.Add(this.cbbPhong);
            this.Controls.Add(this.btnModify);
            this.Controls.Add(this.btnAdd);
            this.Controls.Add(this.dgvDanhsach);
            this.Controls.Add(this.cbbChucvu);
            this.Controls.Add(this.labelX5);
            this.Controls.Add(this.txtTenNV);
            this.Controls.Add(this.labelX4);
            this.Controls.Add(this.txtMaNV);
            this.Controls.Add(this.labelX3);
            this.Font = new System.Drawing.Font("Arial", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.Margin = new System.Windows.Forms.Padding(4);
            this.Name = "frmNhanvien";
            this.ShowIcon = false;
            this.Text = "Danh sách nhân viên Agribank";
            this.Load += new System.EventHandler(this.frmNhanvien_Load);
            ((System.ComponentModel.ISupportInitialize)(this.dgvDanhsach)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private DevComponents.DotNetBar.LabelX labelX3;
        private DevComponents.DotNetBar.Controls.TextBoxX txtMaNV;
        private DevComponents.DotNetBar.LabelX labelX4;
        private DevComponents.DotNetBar.Controls.TextBoxX txtTenNV;
        private DevComponents.DotNetBar.LabelX labelX5;
        private DevComponents.DotNetBar.Controls.ComboBoxEx cbbChucvu;
        private DevComponents.DotNetBar.Controls.DataGridViewX dgvDanhsach;
        private DevComponents.DotNetBar.ButtonX btnAdd;
        private DevComponents.DotNetBar.ButtonX btnModify;
        private DevComponents.DotNetBar.ButtonX btnDel;
        private DevComponents.DotNetBar.Controls.ComboBoxEx cbbPhong;
        private DevComponents.DotNetBar.LabelX labelX2;
        private DevComponents.DotNetBar.Controls.ComboBoxEx cbbMaCN;
        private DevComponents.DotNetBar.LabelX labelX7;
        private DevComponents.DotNetBar.ButtonX btnClose;
        private DevComponents.DotNetBar.LabelX labelX1;
        private DevComponents.DotNetBar.Controls.TextBoxX txtGiayuyquyen;
        private DevComponents.DotNetBar.LabelX labelX6;
        private DevComponents.DotNetBar.Controls.TextBoxX txtNgaysinh;
        private DevComponents.DotNetBar.Controls.TextBoxX txtHoatdong;
    }
}