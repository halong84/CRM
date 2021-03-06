namespace CRM
{
    partial class frmHT_DMImport
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
            this.labelX7 = new DevComponents.DotNetBar.LabelX();
            this.btnModify = new DevComponents.DotNetBar.ButtonX();
            this.btnDel = new DevComponents.DotNetBar.ButtonX();
            this.cbbMaDM = new DevComponents.DotNetBar.Controls.ComboBoxEx();
            this.btnAdd = new DevComponents.DotNetBar.ButtonX();
            this.labelX5 = new DevComponents.DotNetBar.LabelX();
            this.dgvDanhsach = new DevComponents.DotNetBar.Controls.DataGridViewX();
            this.txtTenDM = new DevComponents.DotNetBar.Controls.TextBoxX();
            this.labelX4 = new DevComponents.DotNetBar.LabelX();
            this.labelX1 = new DevComponents.DotNetBar.LabelX();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.rdbDL = new System.Windows.Forms.RadioButton();
            this.rdbHT = new System.Windows.Forms.RadioButton();
            this.btnExit = new DevComponents.DotNetBar.ButtonX();
            ((System.ComponentModel.ISupportInitialize)(this.dgvDanhsach)).BeginInit();
            this.groupBox1.SuspendLayout();
            this.SuspendLayout();
            // 
            // labelX7
            // 
            this.labelX7.ForeColor = System.Drawing.SystemColors.ControlText;
            this.labelX7.Location = new System.Drawing.Point(40, 41);
            this.labelX7.Name = "labelX7";
            this.labelX7.Size = new System.Drawing.Size(123, 23);
            this.labelX7.TabIndex = 144;
            this.labelX7.Text = "Mã danh mục (<font color=\"#ED1C24\">*</font>)";
            this.labelX7.TextAlignment = System.Drawing.StringAlignment.Far;
            // 
            // btnModify
            // 
            this.btnModify.AccessibleRole = System.Windows.Forms.AccessibleRole.PushButton;
            this.btnModify.Location = new System.Drawing.Point(329, 168);
            this.btnModify.Name = "btnModify";
            this.btnModify.Size = new System.Drawing.Size(75, 23);
            this.btnModify.TabIndex = 4;
            this.btnModify.Text = "Thay đổi";
            this.btnModify.Click += new System.EventHandler(this.btnModify_Click);
            // 
            // btnDel
            // 
            this.btnDel.AccessibleRole = System.Windows.Forms.AccessibleRole.PushButton;
            this.btnDel.Location = new System.Drawing.Point(329, 461);
            this.btnDel.Name = "btnDel";
            this.btnDel.Size = new System.Drawing.Size(75, 23);
            this.btnDel.TabIndex = 5;
            this.btnDel.Text = "Xóa";
            this.btnDel.Click += new System.EventHandler(this.btnDel_Click);
            // 
            // cbbMaDM
            // 
            this.cbbMaDM.DisplayMember = "Text";
            this.cbbMaDM.DrawMode = System.Windows.Forms.DrawMode.OwnerDrawFixed;
            this.cbbMaDM.FormattingEnabled = true;
            this.cbbMaDM.ItemHeight = 19;
            this.cbbMaDM.Location = new System.Drawing.Point(179, 41);
            this.cbbMaDM.Name = "cbbMaDM";
            this.cbbMaDM.Size = new System.Drawing.Size(165, 25);
            this.cbbMaDM.TabIndex = 1;
            this.cbbMaDM.SelectedIndexChanged += new System.EventHandler(this.cbbMaDM_SelectedIndexChanged);
            this.cbbMaDM.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.cbbMaDM_KeyPress);
            this.cbbMaDM.TextChanged += new System.EventHandler(this.cbbMaDM_TextChanged);
            // 
            // btnAdd
            // 
            this.btnAdd.AccessibleRole = System.Windows.Forms.AccessibleRole.PushButton;
            this.btnAdd.Location = new System.Drawing.Point(410, 168);
            this.btnAdd.Name = "btnAdd";
            this.btnAdd.Size = new System.Drawing.Size(75, 23);
            this.btnAdd.TabIndex = 3;
            this.btnAdd.Text = "Thêm";
            this.btnAdd.Click += new System.EventHandler(this.btnAdd_Click);
            // 
            // labelX5
            // 
            this.labelX5.Location = new System.Drawing.Point(57, 75);
            this.labelX5.Name = "labelX5";
            this.labelX5.Size = new System.Drawing.Size(106, 23);
            this.labelX5.TabIndex = 143;
            this.labelX5.Text = "Tên danh mục";
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
            this.dgvDanhsach.Location = new System.Drawing.Point(12, 208);
            this.dgvDanhsach.Name = "dgvDanhsach";
            dataGridViewCellStyle2.Font = new System.Drawing.Font("Arial", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.dgvDanhsach.RowsDefaultCellStyle = dataGridViewCellStyle2;
            this.dgvDanhsach.Size = new System.Drawing.Size(498, 237);
            this.dgvDanhsach.TabIndex = 6;
            this.dgvDanhsach.CellClick += new System.Windows.Forms.DataGridViewCellEventHandler(this.dgvDanhsach_CellClick);
            // 
            // txtTenDM
            // 
            // 
            // 
            // 
            this.txtTenDM.Border.Class = "TextBoxBorder";
            this.txtTenDM.Location = new System.Drawing.Point(179, 72);
            this.txtTenDM.Name = "txtTenDM";
            this.txtTenDM.Size = new System.Drawing.Size(306, 26);
            this.txtTenDM.TabIndex = 2;
            // 
            // labelX4
            // 
            this.labelX4.Location = new System.Drawing.Point(164, 2);
            this.labelX4.Margin = new System.Windows.Forms.Padding(4);
            this.labelX4.Name = "labelX4";
            this.labelX4.Size = new System.Drawing.Size(204, 32);
            this.labelX4.TabIndex = 141;
            this.labelX4.Text = "Bảng: Mã danh mục import";
            this.labelX4.TextAlignment = System.Drawing.StringAlignment.Center;
            // 
            // labelX1
            // 
            this.labelX1.Location = new System.Drawing.Point(57, 110);
            this.labelX1.Name = "labelX1";
            this.labelX1.Size = new System.Drawing.Size(106, 23);
            this.labelX1.TabIndex = 145;
            this.labelX1.Text = "Loại danh mục";
            this.labelX1.TextAlignment = System.Drawing.StringAlignment.Far;
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.rdbDL);
            this.groupBox1.Controls.Add(this.rdbHT);
            this.groupBox1.Location = new System.Drawing.Point(179, 98);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(189, 39);
            this.groupBox1.TabIndex = 171;
            this.groupBox1.TabStop = false;
            // 
            // rdbDL
            // 
            this.rdbDL.AutoSize = true;
            this.rdbDL.Location = new System.Drawing.Point(106, 13);
            this.rdbDL.Name = "rdbDL";
            this.rdbDL.Size = new System.Drawing.Size(77, 22);
            this.rdbDL.TabIndex = 4;
            this.rdbDL.TabStop = true;
            this.rdbDL.Text = "Dữ liệu";
            this.rdbDL.UseVisualStyleBackColor = true;
            // 
            // rdbHT
            // 
            this.rdbHT.AutoSize = true;
            this.rdbHT.Checked = true;
            this.rdbHT.Location = new System.Drawing.Point(6, 13);
            this.rdbHT.Name = "rdbHT";
            this.rdbHT.Size = new System.Drawing.Size(88, 22);
            this.rdbHT.TabIndex = 3;
            this.rdbHT.TabStop = true;
            this.rdbHT.Text = "Hệ thống";
            this.rdbHT.UseVisualStyleBackColor = true;
            // 
            // btnExit
            // 
            this.btnExit.AccessibleRole = System.Windows.Forms.AccessibleRole.PushButton;
            this.btnExit.Location = new System.Drawing.Point(410, 461);
            this.btnExit.Name = "btnExit";
            this.btnExit.Size = new System.Drawing.Size(75, 23);
            this.btnExit.TabIndex = 172;
            this.btnExit.Text = "Đóng";
            this.btnExit.Click += new System.EventHandler(this.btnExit_Click);
            // 
            // frmHT_DMImport
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(9F, 18F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(522, 496);
            this.Controls.Add(this.btnExit);
            this.Controls.Add(this.groupBox1);
            this.Controls.Add(this.labelX1);
            this.Controls.Add(this.labelX7);
            this.Controls.Add(this.btnModify);
            this.Controls.Add(this.btnDel);
            this.Controls.Add(this.cbbMaDM);
            this.Controls.Add(this.btnAdd);
            this.Controls.Add(this.labelX5);
            this.Controls.Add(this.dgvDanhsach);
            this.Controls.Add(this.txtTenDM);
            this.Controls.Add(this.labelX4);
            this.Font = new System.Drawing.Font("Arial", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.Margin = new System.Windows.Forms.Padding(4);
            this.Name = "frmHT_DMImport";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Danh muc - Import";
            this.Load += new System.EventHandler(this.frmDM_Import_Load);
            ((System.ComponentModel.ISupportInitialize)(this.dgvDanhsach)).EndInit();
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private DevComponents.DotNetBar.LabelX labelX7;
        private DevComponents.DotNetBar.ButtonX btnModify;
        private DevComponents.DotNetBar.ButtonX btnDel;
        private DevComponents.DotNetBar.Controls.ComboBoxEx cbbMaDM;
        private DevComponents.DotNetBar.ButtonX btnAdd;
        private DevComponents.DotNetBar.LabelX labelX5;
        private DevComponents.DotNetBar.Controls.DataGridViewX dgvDanhsach;
        private DevComponents.DotNetBar.Controls.TextBoxX txtTenDM;
        private DevComponents.DotNetBar.LabelX labelX4;
        private DevComponents.DotNetBar.LabelX labelX1;
        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.RadioButton rdbDL;
        private System.Windows.Forms.RadioButton rdbHT;
        private DevComponents.DotNetBar.ButtonX btnExit;

    }
}