namespace CRM
{
    partial class frmKH_NV
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
            this.btnClose = new DevComponents.DotNetBar.ButtonX();
            this.btnDel = new DevComponents.DotNetBar.ButtonX();
            this.btnModify = new DevComponents.DotNetBar.ButtonX();
            this.btnAdd = new DevComponents.DotNetBar.ButtonX();
            this.dgvDanhsach = new DevComponents.DotNetBar.Controls.DataGridViewX();
            this.labelX4 = new DevComponents.DotNetBar.LabelX();
            this.labelX3 = new DevComponents.DotNetBar.LabelX();
            this.dtpNgayBD = new System.Windows.Forms.DateTimePicker();
            this.labelX1 = new DevComponents.DotNetBar.LabelX();
            this.labelX2 = new DevComponents.DotNetBar.LabelX();
            this.dtpNgayKT = new System.Windows.Forms.DateTimePicker();
            this.labelX5 = new DevComponents.DotNetBar.LabelX();
            this.txtGhichu = new DevComponents.DotNetBar.Controls.TextBoxX();
            this.labelX6 = new DevComponents.DotNetBar.LabelX();
            this.cbbNV = new DevComponents.DotNetBar.Controls.ComboBoxEx();
            this.btnKH = new DevComponents.DotNetBar.ButtonX();
            this.txtKH = new DevComponents.DotNetBar.Controls.TextBoxX();
            this.cbbNhomKHTN = new DevComponents.DotNetBar.Controls.ComboBoxEx();
            this.btnDetail = new DevComponents.DotNetBar.ButtonX();
            ((System.ComponentModel.ISupportInitialize)(this.dgvDanhsach)).BeginInit();
            this.SuspendLayout();
            // 
            // btnClose
            // 
            this.btnClose.AccessibleRole = System.Windows.Forms.AccessibleRole.PushButton;
            this.btnClose.Location = new System.Drawing.Point(623, 469);
            this.btnClose.Name = "btnClose";
            this.btnClose.Size = new System.Drawing.Size(75, 23);
            this.btnClose.TabIndex = 9;
            this.btnClose.Text = "Đóng";
            this.btnClose.Click += new System.EventHandler(this.btnClose_Click);
            // 
            // btnDel
            // 
            this.btnDel.AccessibleRole = System.Windows.Forms.AccessibleRole.PushButton;
            this.btnDel.Location = new System.Drawing.Point(542, 469);
            this.btnDel.Name = "btnDel";
            this.btnDel.Size = new System.Drawing.Size(75, 23);
            this.btnDel.TabIndex = 10;
            this.btnDel.Text = "Xóa";
            this.btnDel.Click += new System.EventHandler(this.btnDel_Click);
            // 
            // btnModify
            // 
            this.btnModify.AccessibleRole = System.Windows.Forms.AccessibleRole.PushButton;
            this.btnModify.Location = new System.Drawing.Point(542, 155);
            this.btnModify.Name = "btnModify";
            this.btnModify.Size = new System.Drawing.Size(75, 23);
            this.btnModify.TabIndex = 8;
            this.btnModify.Text = "Thay đổi";
            this.btnModify.Click += new System.EventHandler(this.btnModify_Click);
            // 
            // btnAdd
            // 
            this.btnAdd.AccessibleRole = System.Windows.Forms.AccessibleRole.PushButton;
            this.btnAdd.Location = new System.Drawing.Point(623, 155);
            this.btnAdd.Name = "btnAdd";
            this.btnAdd.Size = new System.Drawing.Size(75, 23);
            this.btnAdd.TabIndex = 7;
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
            this.dgvDanhsach.Location = new System.Drawing.Point(12, 197);
            this.dgvDanhsach.Name = "dgvDanhsach";
            this.dgvDanhsach.Size = new System.Drawing.Size(688, 257);
            this.dgvDanhsach.TabIndex = 11;
            this.dgvDanhsach.CellClick += new System.Windows.Forms.DataGridViewCellEventHandler(this.dgvDanhsach_CellClick);
            // 
            // labelX4
            // 
            this.labelX4.Location = new System.Drawing.Point(20, 51);
            this.labelX4.Name = "labelX4";
            this.labelX4.Size = new System.Drawing.Size(115, 21);
            this.labelX4.TabIndex = 167;
            this.labelX4.Text = "Nhóm KHTN (<font color=\"#ED1C24\">*</font>)";
            this.labelX4.TextAlignment = System.Drawing.StringAlignment.Far;
            // 
            // labelX3
            // 
            this.labelX3.Location = new System.Drawing.Point(26, 80);
            this.labelX3.Name = "labelX3";
            this.labelX3.Size = new System.Drawing.Size(109, 23);
            this.labelX3.TabIndex = 165;
            this.labelX3.Text = "Cán bộ quản lý";
            this.labelX3.TextAlignment = System.Drawing.StringAlignment.Far;
            // 
            // dtpNgayBD
            // 
            this.dtpNgayBD.CustomFormat = "dd/MM/yyyy";
            this.dtpNgayBD.Format = System.Windows.Forms.DateTimePickerFormat.Custom;
            this.dtpNgayBD.Location = new System.Drawing.Point(589, 46);
            this.dtpNgayBD.Name = "dtpNgayBD";
            this.dtpNgayBD.ShowUpDown = true;
            this.dtpNgayBD.Size = new System.Drawing.Size(109, 26);
            this.dtpNgayBD.TabIndex = 4;
            // 
            // labelX1
            // 
            this.labelX1.Location = new System.Drawing.Point(443, 51);
            this.labelX1.Name = "labelX1";
            this.labelX1.Size = new System.Drawing.Size(131, 21);
            this.labelX1.TabIndex = 165;
            this.labelX1.Text = "Thời gian bắt đầu";
            this.labelX1.TextAlignment = System.Drawing.StringAlignment.Far;
            // 
            // labelX2
            // 
            this.labelX2.Location = new System.Drawing.Point(443, 80);
            this.labelX2.Name = "labelX2";
            this.labelX2.Size = new System.Drawing.Size(131, 23);
            this.labelX2.TabIndex = 165;
            this.labelX2.Text = "Thời gian kết thúc";
            this.labelX2.TextAlignment = System.Drawing.StringAlignment.Far;
            // 
            // dtpNgayKT
            // 
            this.dtpNgayKT.CustomFormat = "dd/MM/yyyy";
            this.dtpNgayKT.Format = System.Windows.Forms.DateTimePickerFormat.Custom;
            this.dtpNgayKT.Location = new System.Drawing.Point(589, 78);
            this.dtpNgayKT.Name = "dtpNgayKT";
            this.dtpNgayKT.ShowUpDown = true;
            this.dtpNgayKT.Size = new System.Drawing.Size(109, 26);
            this.dtpNgayKT.TabIndex = 5;
            this.dtpNgayKT.Value = new System.DateTime(9998, 12, 31, 0, 0, 0, 0);
            // 
            // labelX5
            // 
            this.labelX5.Location = new System.Drawing.Point(74, 108);
            this.labelX5.Name = "labelX5";
            this.labelX5.Size = new System.Drawing.Size(61, 23);
            this.labelX5.TabIndex = 165;
            this.labelX5.Text = "Ghi chú";
            this.labelX5.TextAlignment = System.Drawing.StringAlignment.Far;
            // 
            // txtGhichu
            // 
            // 
            // 
            // 
            this.txtGhichu.Border.Class = "TextBoxBorder";
            this.txtGhichu.Location = new System.Drawing.Point(141, 109);
            this.txtGhichu.Multiline = true;
            this.txtGhichu.Name = "txtGhichu";
            this.txtGhichu.ScrollBars = System.Windows.Forms.ScrollBars.Vertical;
            this.txtGhichu.Size = new System.Drawing.Size(282, 69);
            this.txtGhichu.TabIndex = 6;
            // 
            // labelX6
            // 
            this.labelX6.Font = new System.Drawing.Font("Arial", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.labelX6.Location = new System.Drawing.Point(171, 12);
            this.labelX6.Name = "labelX6";
            this.labelX6.Size = new System.Drawing.Size(370, 23);
            this.labelX6.TabIndex = 181;
            this.labelX6.Text = "PHÂN CÔNG CÁN BỘ THEO DÕI KH TIỀM NĂNG";
            // 
            // cbbNV
            // 
            this.cbbNV.DisplayMember = "Text";
            this.cbbNV.DrawMode = System.Windows.Forms.DrawMode.OwnerDrawFixed;
            this.cbbNV.FormattingEnabled = true;
            this.cbbNV.ItemHeight = 19;
            this.cbbNV.Location = new System.Drawing.Point(141, 78);
            this.cbbNV.Name = "cbbNV";
            this.cbbNV.Size = new System.Drawing.Size(282, 25);
            this.cbbNV.TabIndex = 3;
            // 
            // btnKH
            // 
            this.btnKH.AccessibleRole = System.Windows.Forms.AccessibleRole.PushButton;
            this.btnKH.Font = new System.Drawing.Font("Arial", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnKH.Location = new System.Drawing.Point(378, 469);
            this.btnKH.Name = "btnKH";
            this.btnKH.Size = new System.Drawing.Size(45, 23);
            this.btnKH.TabIndex = 2;
            this.btnKH.Text = "...";
            this.btnKH.Visible = false;
            this.btnKH.Click += new System.EventHandler(this.btnKH_Click);
            // 
            // txtKH
            // 
            // 
            // 
            // 
            this.txtKH.Border.Class = "TextBoxBorder";
            this.txtKH.Location = new System.Drawing.Point(141, 466);
            this.txtKH.Name = "txtKH";
            this.txtKH.Size = new System.Drawing.Size(231, 26);
            this.txtKH.TabIndex = 1;
            this.txtKH.Visible = false;
            // 
            // cbbNhomKHTN
            // 
            this.cbbNhomKHTN.DisplayMember = "Text";
            this.cbbNhomKHTN.DrawMode = System.Windows.Forms.DrawMode.OwnerDrawFixed;
            this.cbbNhomKHTN.FormattingEnabled = true;
            this.cbbNhomKHTN.ItemHeight = 19;
            this.cbbNhomKHTN.Location = new System.Drawing.Point(141, 47);
            this.cbbNhomKHTN.Name = "cbbNhomKHTN";
            this.cbbNhomKHTN.Size = new System.Drawing.Size(282, 25);
            this.cbbNhomKHTN.TabIndex = 3;
            this.cbbNhomKHTN.SelectionChangeCommitted += new System.EventHandler(this.cbbNhomKHTN_SelectionChangeCommitted);
            // 
            // btnDetail
            // 
            this.btnDetail.AccessibleRole = System.Windows.Forms.AccessibleRole.PushButton;
            this.btnDetail.Location = new System.Drawing.Point(12, 469);
            this.btnDetail.Name = "btnDetail";
            this.btnDetail.Size = new System.Drawing.Size(75, 23);
            this.btnDetail.TabIndex = 10;
            this.btnDetail.Text = "Chi tiết";
            this.btnDetail.Click += new System.EventHandler(this.btnDetail_Click);
            // 
            // frmKH_NV
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(9F, 18F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(712, 504);
            this.Controls.Add(this.txtKH);
            this.Controls.Add(this.btnKH);
            this.Controls.Add(this.cbbNhomKHTN);
            this.Controls.Add(this.cbbNV);
            this.Controls.Add(this.labelX6);
            this.Controls.Add(this.dtpNgayKT);
            this.Controls.Add(this.dtpNgayBD);
            this.Controls.Add(this.btnClose);
            this.Controls.Add(this.btnDetail);
            this.Controls.Add(this.btnDel);
            this.Controls.Add(this.btnModify);
            this.Controls.Add(this.btnAdd);
            this.Controls.Add(this.dgvDanhsach);
            this.Controls.Add(this.labelX4);
            this.Controls.Add(this.txtGhichu);
            this.Controls.Add(this.labelX5);
            this.Controls.Add(this.labelX2);
            this.Controls.Add(this.labelX1);
            this.Controls.Add(this.labelX3);
            this.Font = new System.Drawing.Font("Arial", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.Margin = new System.Windows.Forms.Padding(4);
            this.Name = "frmKH_NV";
            this.ShowIcon = false;
            this.Text = "Theo dõi khách hàng tiềm năng";
            this.Load += new System.EventHandler(this.frmKH_NV_Load);
            ((System.ComponentModel.ISupportInitialize)(this.dgvDanhsach)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private DevComponents.DotNetBar.ButtonX btnClose;
        private DevComponents.DotNetBar.ButtonX btnDel;
        private DevComponents.DotNetBar.ButtonX btnModify;
        private DevComponents.DotNetBar.ButtonX btnAdd;
        private DevComponents.DotNetBar.Controls.DataGridViewX dgvDanhsach;
        private DevComponents.DotNetBar.LabelX labelX4;
        private DevComponents.DotNetBar.LabelX labelX3;
        private System.Windows.Forms.DateTimePicker dtpNgayBD;
        private DevComponents.DotNetBar.LabelX labelX1;
        private DevComponents.DotNetBar.LabelX labelX2;
        private System.Windows.Forms.DateTimePicker dtpNgayKT;
        private DevComponents.DotNetBar.LabelX labelX5;
        private DevComponents.DotNetBar.Controls.TextBoxX txtGhichu;
        private DevComponents.DotNetBar.LabelX labelX6;
        private DevComponents.DotNetBar.Controls.ComboBoxEx cbbNV;
        private DevComponents.DotNetBar.ButtonX btnKH;
        private DevComponents.DotNetBar.Controls.TextBoxX txtKH;
        private DevComponents.DotNetBar.Controls.ComboBoxEx cbbNhomKHTN;
        private DevComponents.DotNetBar.ButtonX btnDetail;

    }
}