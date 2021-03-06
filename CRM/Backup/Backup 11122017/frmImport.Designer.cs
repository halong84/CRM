namespace CRM
{
    partial class frmImport
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
            this.lblThang = new DevComponents.DotNetBar.LabelX();
            this.dtpThang = new System.Windows.Forms.DateTimePicker();
            this.openFileDialog1 = new System.Windows.Forms.OpenFileDialog();
            this.labelX2 = new DevComponents.DotNetBar.LabelX();
            this.cbbTen = new DevComponents.DotNetBar.Controls.ComboBoxEx();
            this.btnImport = new DevComponents.DotNetBar.ButtonX();
            this.lblTien = new DevComponents.DotNetBar.LabelX();
            this.cbbTien = new DevComponents.DotNetBar.Controls.ComboBoxEx();
            this.btnLichsu = new DevComponents.DotNetBar.ButtonX();
            this.SuspendLayout();
            // 
            // lblThang
            // 
            this.lblThang.Location = new System.Drawing.Point(24, 24);
            this.lblThang.Margin = new System.Windows.Forms.Padding(4);
            this.lblThang.Name = "lblThang";
            this.lblThang.Size = new System.Drawing.Size(52, 26);
            this.lblThang.TabIndex = 1;
            this.lblThang.Text = "Tháng";
            this.lblThang.TextAlignment = System.Drawing.StringAlignment.Far;
            // 
            // dtpThang
            // 
            this.dtpThang.CustomFormat = "MM/yyyy";
            this.dtpThang.Format = System.Windows.Forms.DateTimePickerFormat.Custom;
            this.dtpThang.Location = new System.Drawing.Point(83, 24);
            this.dtpThang.Name = "dtpThang";
            this.dtpThang.ShowUpDown = true;
            this.dtpThang.Size = new System.Drawing.Size(95, 26);
            this.dtpThang.TabIndex = 191;
            this.dtpThang.Value = new System.DateTime(2013, 1, 1, 0, 0, 0, 0);
            // 
            // openFileDialog1
            // 
            this.openFileDialog1.FileName = "openFileDialog1";
            // 
            // labelX2
            // 
            this.labelX2.Location = new System.Drawing.Point(42, 68);
            this.labelX2.Margin = new System.Windows.Forms.Padding(4);
            this.labelX2.Name = "labelX2";
            this.labelX2.Size = new System.Drawing.Size(34, 26);
            this.labelX2.TabIndex = 195;
            this.labelX2.Text = "Loại";
            this.labelX2.TextAlignment = System.Drawing.StringAlignment.Far;
            // 
            // cbbTen
            // 
            this.cbbTen.DisplayMember = "Text";
            this.cbbTen.DrawMode = System.Windows.Forms.DrawMode.OwnerDrawFixed;
            this.cbbTen.FormattingEnabled = true;
            this.cbbTen.ItemHeight = 19;
            this.cbbTen.Location = new System.Drawing.Point(83, 69);
            this.cbbTen.Name = "cbbTen";
            this.cbbTen.Size = new System.Drawing.Size(273, 25);
            this.cbbTen.TabIndex = 196;
            this.cbbTen.TextChanged += new System.EventHandler(this.cbbTen_TextChanged);
            // 
            // btnImport
            // 
            this.btnImport.AccessibleRole = System.Windows.Forms.AccessibleRole.PushButton;
            this.btnImport.Location = new System.Drawing.Point(200, 136);
            this.btnImport.Name = "btnImport";
            this.btnImport.Size = new System.Drawing.Size(75, 23);
            this.btnImport.TabIndex = 197;
            this.btnImport.Text = "Import";
            this.btnImport.Click += new System.EventHandler(this.btnImport_Click);
            // 
            // lblTien
            // 
            this.lblTien.Location = new System.Drawing.Point(231, 24);
            this.lblTien.Margin = new System.Windows.Forms.Padding(4);
            this.lblTien.Name = "lblTien";
            this.lblTien.Size = new System.Drawing.Size(53, 26);
            this.lblTien.TabIndex = 199;
            this.lblTien.Text = "Tiền tệ";
            this.lblTien.Visible = false;
            // 
            // cbbTien
            // 
            this.cbbTien.DisplayMember = "Text";
            this.cbbTien.DrawMode = System.Windows.Forms.DrawMode.OwnerDrawFixed;
            this.cbbTien.FormattingEnabled = true;
            this.cbbTien.ItemHeight = 19;
            this.cbbTien.Location = new System.Drawing.Point(291, 24);
            this.cbbTien.Name = "cbbTien";
            this.cbbTien.Size = new System.Drawing.Size(65, 25);
            this.cbbTien.TabIndex = 200;
            this.cbbTien.Visible = false;
            // 
            // btnLichsu
            // 
            this.btnLichsu.AccessibleRole = System.Windows.Forms.AccessibleRole.PushButton;
            this.btnLichsu.Location = new System.Drawing.Point(281, 136);
            this.btnLichsu.Name = "btnLichsu";
            this.btnLichsu.Size = new System.Drawing.Size(75, 23);
            this.btnLichsu.TabIndex = 201;
            this.btnLichsu.Text = "Lịch sử";
            this.btnLichsu.Click += new System.EventHandler(this.btnLichsu_Click);
            // 
            // frmImport
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(9F, 18F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(382, 171);
            this.Controls.Add(this.btnLichsu);
            this.Controls.Add(this.cbbTien);
            this.Controls.Add(this.lblTien);
            this.Controls.Add(this.btnImport);
            this.Controls.Add(this.cbbTen);
            this.Controls.Add(this.labelX2);
            this.Controls.Add(this.dtpThang);
            this.Controls.Add(this.lblThang);
            this.Font = new System.Drawing.Font("Arial", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.Margin = new System.Windows.Forms.Padding(4);
            this.Name = "frmImport";
            this.Text = "Import";
            this.Load += new System.EventHandler(this.frmImport_Load);
            this.FormClosed += new System.Windows.Forms.FormClosedEventHandler(this.frmImport_FormClosed);
            this.ResumeLayout(false);

        }

        #endregion

        private DevComponents.DotNetBar.LabelX lblThang;
        private System.Windows.Forms.DateTimePicker dtpThang;
        private System.Windows.Forms.OpenFileDialog openFileDialog1;
        private DevComponents.DotNetBar.LabelX labelX2;
        private DevComponents.DotNetBar.Controls.ComboBoxEx cbbTen;
        private DevComponents.DotNetBar.ButtonX btnImport;
        private DevComponents.DotNetBar.LabelX lblTien;
        private DevComponents.DotNetBar.Controls.ComboBoxEx cbbTien;
        private DevComponents.DotNetBar.ButtonX btnLichsu;
    }
}