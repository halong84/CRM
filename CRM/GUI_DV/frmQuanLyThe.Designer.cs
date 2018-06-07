namespace CRM.GUI_DV
{
    partial class frmQuanLyThe
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(frmQuanLyThe));
            this.dgvThongTinThe = new System.Windows.Forms.DataGridView();
            this.tcrlQuanLyThe = new System.Windows.Forms.TabControl();
            this.tabTheoNgay = new System.Windows.Forms.TabPage();
            this.dtpDenNgay = new System.Windows.Forms.DateTimePicker();
            this.dtpTuNgay = new System.Windows.Forms.DateTimePicker();
            this.btnTimKiem_TheoNgay = new System.Windows.Forms.Button();
            this.label2 = new System.Windows.Forms.Label();
            this.label1 = new System.Windows.Forms.Label();
            this.cbTieuChi_TheoNgay = new System.Windows.Forms.ComboBox();
            this.tabPage2 = new System.Windows.Forms.TabPage();
            this.btnTimKiem_TheoThongTin = new System.Windows.Forms.Button();
            this.txtThongTin = new System.Windows.Forms.TextBox();
            this.cbTieuChi_ThongTin = new System.Windows.Forms.ComboBox();
            ((System.ComponentModel.ISupportInitialize)(this.dgvThongTinThe)).BeginInit();
            this.tcrlQuanLyThe.SuspendLayout();
            this.tabTheoNgay.SuspendLayout();
            this.tabPage2.SuspendLayout();
            this.SuspendLayout();
            // 
            // dgvThongTinThe
            // 
            this.dgvThongTinThe.AllowUserToAddRows = false;
            this.dgvThongTinThe.AllowUserToDeleteRows = false;
            this.dgvThongTinThe.AllowUserToResizeRows = false;
            this.dgvThongTinThe.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dgvThongTinThe.Location = new System.Drawing.Point(12, 78);
            this.dgvThongTinThe.MultiSelect = false;
            this.dgvThongTinThe.Name = "dgvThongTinThe";
            this.dgvThongTinThe.ReadOnly = true;
            this.dgvThongTinThe.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            this.dgvThongTinThe.Size = new System.Drawing.Size(799, 439);
            this.dgvThongTinThe.TabIndex = 2;
            this.dgvThongTinThe.CellDoubleClick += new System.Windows.Forms.DataGridViewCellEventHandler(this.dgvThongTinThe_CellDoubleClick);
            this.dgvThongTinThe.CellValidated += new System.Windows.Forms.DataGridViewCellEventHandler(this.dgvThongTinThe_CellValidated);
            this.dgvThongTinThe.CellValueChanged += new System.Windows.Forms.DataGridViewCellEventHandler(this.dgvThongTinThe_CellValueChanged);
            this.dgvThongTinThe.MouseDown += new System.Windows.Forms.MouseEventHandler(this.dgvThongTinThe_MouseDown);
            // 
            // tcrlQuanLyThe
            // 
            this.tcrlQuanLyThe.Controls.Add(this.tabTheoNgay);
            this.tcrlQuanLyThe.Controls.Add(this.tabPage2);
            this.tcrlQuanLyThe.Location = new System.Drawing.Point(12, 12);
            this.tcrlQuanLyThe.Name = "tcrlQuanLyThe";
            this.tcrlQuanLyThe.SelectedIndex = 0;
            this.tcrlQuanLyThe.Size = new System.Drawing.Size(799, 60);
            this.tcrlQuanLyThe.TabIndex = 3;
            // 
            // tabTheoNgay
            // 
            this.tabTheoNgay.Controls.Add(this.dtpDenNgay);
            this.tabTheoNgay.Controls.Add(this.dtpTuNgay);
            this.tabTheoNgay.Controls.Add(this.btnTimKiem_TheoNgay);
            this.tabTheoNgay.Controls.Add(this.label2);
            this.tabTheoNgay.Controls.Add(this.label1);
            this.tabTheoNgay.Controls.Add(this.cbTieuChi_TheoNgay);
            this.tabTheoNgay.Location = new System.Drawing.Point(4, 22);
            this.tabTheoNgay.Name = "tabTheoNgay";
            this.tabTheoNgay.Padding = new System.Windows.Forms.Padding(3);
            this.tabTheoNgay.Size = new System.Drawing.Size(791, 34);
            this.tabTheoNgay.TabIndex = 0;
            this.tabTheoNgay.Text = "Theo ngày";
            this.tabTheoNgay.UseVisualStyleBackColor = true;
            // 
            // dtpDenNgay
            // 
            this.dtpDenNgay.CustomFormat = "dd/MM/yyyy";
            this.dtpDenNgay.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.dtpDenNgay.Format = System.Windows.Forms.DateTimePickerFormat.Custom;
            this.dtpDenNgay.Location = new System.Drawing.Point(502, 6);
            this.dtpDenNgay.Name = "dtpDenNgay";
            this.dtpDenNgay.Size = new System.Drawing.Size(98, 20);
            this.dtpDenNgay.TabIndex = 9;
            // 
            // dtpTuNgay
            // 
            this.dtpTuNgay.CustomFormat = "dd/MM/yyyy";
            this.dtpTuNgay.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.dtpTuNgay.Format = System.Windows.Forms.DateTimePickerFormat.Custom;
            this.dtpTuNgay.Location = new System.Drawing.Point(324, 6);
            this.dtpTuNgay.Name = "dtpTuNgay";
            this.dtpTuNgay.Size = new System.Drawing.Size(98, 20);
            this.dtpTuNgay.TabIndex = 7;
            // 
            // btnTimKiem_TheoNgay
            // 
            this.btnTimKiem_TheoNgay.Location = new System.Drawing.Point(653, 4);
            this.btnTimKiem_TheoNgay.Name = "btnTimKiem_TheoNgay";
            this.btnTimKiem_TheoNgay.Size = new System.Drawing.Size(115, 24);
            this.btnTimKiem_TheoNgay.TabIndex = 10;
            this.btnTimKiem_TheoNgay.Text = "Tìm kiếm";
            this.btnTimKiem_TheoNgay.UseVisualStyleBackColor = true;
            this.btnTimKiem_TheoNgay.Click += new System.EventHandler(this.btnTimKiem_TheoNgay_Click);
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label2.Location = new System.Drawing.Point(440, 10);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(56, 13);
            this.label2.TabIndex = 8;
            this.label2.Text = "Đến ngày:";
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label1.Location = new System.Drawing.Point(269, 10);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(49, 13);
            this.label1.TabIndex = 6;
            this.label1.Text = "Từ ngày:";
            // 
            // cbTieuChi_TheoNgay
            // 
            this.cbTieuChi_TheoNgay.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cbTieuChi_TheoNgay.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.cbTieuChi_TheoNgay.FormattingEnabled = true;
            this.cbTieuChi_TheoNgay.Items.AddRange(new object[] {
            "Thẻ chưa nhận",
            "Thẻ đã nhận - chưa giao",
            "Thẻ đã giao",
            "Tất cả"});
            this.cbTieuChi_TheoNgay.Location = new System.Drawing.Point(23, 6);
            this.cbTieuChi_TheoNgay.Name = "cbTieuChi_TheoNgay";
            this.cbTieuChi_TheoNgay.Size = new System.Drawing.Size(202, 21);
            this.cbTieuChi_TheoNgay.TabIndex = 5;
            // 
            // tabPage2
            // 
            this.tabPage2.Controls.Add(this.btnTimKiem_TheoThongTin);
            this.tabPage2.Controls.Add(this.txtThongTin);
            this.tabPage2.Controls.Add(this.cbTieuChi_ThongTin);
            this.tabPage2.Location = new System.Drawing.Point(4, 22);
            this.tabPage2.Name = "tabPage2";
            this.tabPage2.Padding = new System.Windows.Forms.Padding(3);
            this.tabPage2.Size = new System.Drawing.Size(791, 34);
            this.tabPage2.TabIndex = 1;
            this.tabPage2.Text = "Theo người dùng";
            this.tabPage2.UseVisualStyleBackColor = true;
            // 
            // btnTimKiem_TheoThongTin
            // 
            this.btnTimKiem_TheoThongTin.Location = new System.Drawing.Point(653, 4);
            this.btnTimKiem_TheoThongTin.Name = "btnTimKiem_TheoThongTin";
            this.btnTimKiem_TheoThongTin.Size = new System.Drawing.Size(115, 24);
            this.btnTimKiem_TheoThongTin.TabIndex = 11;
            this.btnTimKiem_TheoThongTin.Text = "Tìm kiếm";
            this.btnTimKiem_TheoThongTin.UseVisualStyleBackColor = true;
            this.btnTimKiem_TheoThongTin.Click += new System.EventHandler(this.btnTimKiem_TheoThongTin_Click);
            // 
            // txtThongTin
            // 
            this.txtThongTin.Location = new System.Drawing.Point(334, 7);
            this.txtThongTin.Name = "txtThongTin";
            this.txtThongTin.Size = new System.Drawing.Size(199, 20);
            this.txtThongTin.TabIndex = 7;
            this.txtThongTin.TextChanged += new System.EventHandler(this.textBox1_TextChanged);
            this.txtThongTin.KeyDown += new System.Windows.Forms.KeyEventHandler(this.txtThongTin_KeyDown);
            // 
            // cbTieuChi_ThongTin
            // 
            this.cbTieuChi_ThongTin.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cbTieuChi_ThongTin.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.cbTieuChi_ThongTin.FormattingEnabled = true;
            this.cbTieuChi_ThongTin.Items.AddRange(new object[] {
            "CMND",
            "Mã khách hàng"});
            this.cbTieuChi_ThongTin.Location = new System.Drawing.Point(23, 6);
            this.cbTieuChi_ThongTin.Name = "cbTieuChi_ThongTin";
            this.cbTieuChi_ThongTin.Size = new System.Drawing.Size(202, 21);
            this.cbTieuChi_ThongTin.TabIndex = 6;
            // 
            // frmQuanLyThe
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(823, 529);
            this.Controls.Add(this.tcrlQuanLyThe);
            this.Controls.Add(this.dgvThongTinThe);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.MaximizeBox = false;
            this.Name = "frmQuanLyThe";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Quản Lý Thẻ";
            ((System.ComponentModel.ISupportInitialize)(this.dgvThongTinThe)).EndInit();
            this.tcrlQuanLyThe.ResumeLayout(false);
            this.tabTheoNgay.ResumeLayout(false);
            this.tabTheoNgay.PerformLayout();
            this.tabPage2.ResumeLayout(false);
            this.tabPage2.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.DataGridView dgvThongTinThe;
        private System.Windows.Forms.TabControl tcrlQuanLyThe;
        private System.Windows.Forms.TabPage tabTheoNgay;
        private System.Windows.Forms.DateTimePicker dtpDenNgay;
        private System.Windows.Forms.DateTimePicker dtpTuNgay;
        private System.Windows.Forms.Button btnTimKiem_TheoNgay;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.ComboBox cbTieuChi_TheoNgay;
        private System.Windows.Forms.TabPage tabPage2;
        private System.Windows.Forms.Button btnTimKiem_TheoThongTin;
        private System.Windows.Forms.TextBox txtThongTin;
        private System.Windows.Forms.ComboBox cbTieuChi_ThongTin;
    }
}