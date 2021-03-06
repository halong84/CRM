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
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle2 = new System.Windows.Forms.DataGridViewCellStyle();
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
            this.labelX2 = new DevComponents.DotNetBar.LabelX();
            this.labelX7 = new DevComponents.DotNetBar.LabelX();
            this.btnClose = new DevComponents.DotNetBar.ButtonX();
            this.cbbMaCN = new System.Windows.Forms.ComboBox();
            this.cbbPhong = new System.Windows.Forms.ComboBox();
            ((System.ComponentModel.ISupportInitialize)(this.dgvDanhsach)).BeginInit();
            this.SuspendLayout();
            // 
            // labelX3
            // 
            this.labelX3.Location = new System.Drawing.Point(35, 109);
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
            this.txtMaNV.Size = new System.Drawing.Size(189, 26);
            this.txtMaNV.TabIndex = 6;
            // 
            // labelX4
            // 
            this.labelX4.Location = new System.Drawing.Point(56, 77);
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
            this.txtTenNV.Size = new System.Drawing.Size(260, 26);
            this.txtTenNV.TabIndex = 8;
            // 
            // labelX5
            // 
            this.labelX5.Location = new System.Drawing.Point(48, 142);
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
            this.cbbChucvu.Size = new System.Drawing.Size(189, 25);
            this.cbbChucvu.TabIndex = 10;
            // 
            // dgvDanhsach
            // 
            this.dgvDanhsach.BackgroundColor = System.Drawing.SystemColors.Control;
            this.dgvDanhsach.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            dataGridViewCellStyle2.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle2.BackColor = System.Drawing.SystemColors.Window;
            dataGridViewCellStyle2.Font = new System.Drawing.Font("Arial", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            dataGridViewCellStyle2.ForeColor = System.Drawing.SystemColors.ControlText;
            dataGridViewCellStyle2.SelectionBackColor = System.Drawing.SystemColors.Highlight;
            dataGridViewCellStyle2.SelectionForeColor = System.Drawing.SystemColors.ControlText;
            dataGridViewCellStyle2.WrapMode = System.Windows.Forms.DataGridViewTriState.False;
            this.dgvDanhsach.DefaultCellStyle = dataGridViewCellStyle2;
            this.dgvDanhsach.GridColor = System.Drawing.Color.FromArgb(((int)(((byte)(208)))), ((int)(((byte)(215)))), ((int)(((byte)(229)))));
            this.dgvDanhsach.Location = new System.Drawing.Point(12, 219);
            this.dgvDanhsach.Name = "dgvDanhsach";
            this.dgvDanhsach.Size = new System.Drawing.Size(392, 227);
            this.dgvDanhsach.TabIndex = 13;
            this.dgvDanhsach.CellClick += new System.Windows.Forms.DataGridViewCellEventHandler(this.dgvDanhsach_CellClick);
            // 
            // btnAdd
            // 
            this.btnAdd.AccessibleRole = System.Windows.Forms.AccessibleRole.PushButton;
            this.btnAdd.Location = new System.Drawing.Point(309, 190);
            this.btnAdd.Name = "btnAdd";
            this.btnAdd.Size = new System.Drawing.Size(75, 23);
            this.btnAdd.TabIndex = 14;
            this.btnAdd.Text = "Thêm";
            this.btnAdd.Click += new System.EventHandler(this.btnAdd_Click);
            // 
            // btnModify
            // 
            this.btnModify.AccessibleRole = System.Windows.Forms.AccessibleRole.PushButton;
            this.btnModify.Location = new System.Drawing.Point(228, 190);
            this.btnModify.Name = "btnModify";
            this.btnModify.Size = new System.Drawing.Size(75, 23);
            this.btnModify.TabIndex = 15;
            this.btnModify.Text = "Thay đổi";
            this.btnModify.Click += new System.EventHandler(this.btnModify_Click);
            // 
            // btnDel
            // 
            this.btnDel.AccessibleRole = System.Windows.Forms.AccessibleRole.PushButton;
            this.btnDel.Location = new System.Drawing.Point(228, 461);
            this.btnDel.Name = "btnDel";
            this.btnDel.Size = new System.Drawing.Size(75, 23);
            this.btnDel.TabIndex = 16;
            this.btnDel.Text = "Xóa";
            this.btnDel.Click += new System.EventHandler(this.btnDel_Click);
            // 
            // labelX2
            // 
            this.labelX2.Font = new System.Drawing.Font("Arial", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.labelX2.Location = new System.Drawing.Point(35, 44);
            this.labelX2.Margin = new System.Windows.Forms.Padding(4);
            this.labelX2.Name = "labelX2";
            this.labelX2.Size = new System.Drawing.Size(78, 25);
            this.labelX2.TabIndex = 3;
            this.labelX2.Text = "Phòng ban";
            // 
            // labelX7
            // 
            this.labelX7.ForeColor = System.Drawing.SystemColors.ControlText;
            this.labelX7.Location = new System.Drawing.Point(35, 14);
            this.labelX7.Name = "labelX7";
            this.labelX7.Size = new System.Drawing.Size(78, 23);
            this.labelX7.TabIndex = 162;
            this.labelX7.Text = "Chi nhánh";
            this.labelX7.TextAlignment = System.Drawing.StringAlignment.Far;
            // 
            // btnClose
            // 
            this.btnClose.AccessibleRole = System.Windows.Forms.AccessibleRole.PushButton;
            this.btnClose.Location = new System.Drawing.Point(309, 461);
            this.btnClose.Name = "btnClose";
            this.btnClose.Size = new System.Drawing.Size(75, 23);
            this.btnClose.TabIndex = 163;
            this.btnClose.Text = "Đóng";
            this.btnClose.Click += new System.EventHandler(this.btnClose_Click);
            // 
            // cbbMaCN
            // 
            this.cbbMaCN.FormattingEnabled = true;
            this.cbbMaCN.Location = new System.Drawing.Point(124, 11);
            this.cbbMaCN.Name = "cbbMaCN";
            this.cbbMaCN.Size = new System.Drawing.Size(121, 26);
            this.cbbMaCN.TabIndex = 164;
            this.cbbMaCN.SelectedIndexChanged += new System.EventHandler(this.cbbMaCN_SelectedIndexChanged_1);
            // 
            // cbbPhong
            // 
            this.cbbPhong.FormattingEnabled = true;
            this.cbbPhong.Location = new System.Drawing.Point(124, 43);
            this.cbbPhong.Name = "cbbPhong";
            this.cbbPhong.Size = new System.Drawing.Size(260, 26);
            this.cbbPhong.TabIndex = 165;
            // 
            // frmNhanvien
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(9F, 18F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(416, 496);
            this.Controls.Add(this.cbbPhong);
            this.Controls.Add(this.cbbMaCN);
            this.Controls.Add(this.btnClose);
            this.Controls.Add(this.labelX7);
            this.Controls.Add(this.labelX2);
            this.Controls.Add(this.btnDel);
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
            this.Text = "Nhân viên";
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
        private DevComponents.DotNetBar.LabelX labelX2;
        private DevComponents.DotNetBar.LabelX labelX7;
        private DevComponents.DotNetBar.ButtonX btnClose;
        private System.Windows.Forms.ComboBox cbbMaCN;
        private System.Windows.Forms.ComboBox cbbPhong;
    }
}