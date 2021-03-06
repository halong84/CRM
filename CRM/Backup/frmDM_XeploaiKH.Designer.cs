namespace CRM
{
    partial class frmDM_XeploaiKH
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
            this.labelX7 = new DevComponents.DotNetBar.LabelX();
            this.btnModify = new DevComponents.DotNetBar.ButtonX();
            this.btnDel = new DevComponents.DotNetBar.ButtonX();
            this.cbbMaloai = new DevComponents.DotNetBar.Controls.ComboBoxEx();
            this.btnAdd = new DevComponents.DotNetBar.ButtonX();
            this.labelX5 = new DevComponents.DotNetBar.LabelX();
            this.dgvDanhsach = new DevComponents.DotNetBar.Controls.DataGridViewX();
            this.txtTieuchi = new DevComponents.DotNetBar.Controls.TextBoxX();
            this.labelX4 = new DevComponents.DotNetBar.LabelX();
            this.labelX6 = new DevComponents.DotNetBar.LabelX();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.rdbDN = new System.Windows.Forms.RadioButton();
            this.rdbCN = new System.Windows.Forms.RadioButton();
            this.cbbTheKH = new DevComponents.DotNetBar.Controls.ComboBoxEx();
            this.btnExit = new DevComponents.DotNetBar.ButtonX();
            ((System.ComponentModel.ISupportInitialize)(this.dgvDanhsach)).BeginInit();
            this.groupBox1.SuspendLayout();
            this.SuspendLayout();
            // 
            // labelX1
            // 
            this.labelX1.Location = new System.Drawing.Point(77, 103);
            this.labelX1.Name = "labelX1";
            this.labelX1.Size = new System.Drawing.Size(60, 23);
            this.labelX1.TabIndex = 145;
            this.labelX1.Text = "Thẻ KH";
            this.labelX1.TextAlignment = System.Drawing.StringAlignment.Far;
            // 
            // labelX7
            // 
            this.labelX7.ForeColor = System.Drawing.Color.Red;
            this.labelX7.Location = new System.Drawing.Point(91, 41);
            this.labelX7.Name = "labelX7";
            this.labelX7.Size = new System.Drawing.Size(46, 23);
            this.labelX7.TabIndex = 144;
            this.labelX7.Text = "<font color=\"#1F497D\">Mã (</font><font color=\"#ED1C24\">*</font><font color=\"#1F49" +
                "7D\">)</font>";
            this.labelX7.TextAlignment = System.Drawing.StringAlignment.Far;
            // 
            // btnModify
            // 
            this.btnModify.AccessibleRole = System.Windows.Forms.AccessibleRole.PushButton;
            this.btnModify.Location = new System.Drawing.Point(307, 187);
            this.btnModify.Name = "btnModify";
            this.btnModify.Size = new System.Drawing.Size(75, 23);
            this.btnModify.TabIndex = 7;
            this.btnModify.Text = "Thay đổi";
            this.btnModify.Click += new System.EventHandler(this.btnModify_Click);
            // 
            // btnDel
            // 
            this.btnDel.AccessibleRole = System.Windows.Forms.AccessibleRole.PushButton;
            this.btnDel.Location = new System.Drawing.Point(307, 461);
            this.btnDel.Name = "btnDel";
            this.btnDel.Size = new System.Drawing.Size(75, 23);
            this.btnDel.TabIndex = 8;
            this.btnDel.Text = "Xóa";
            this.btnDel.Click += new System.EventHandler(this.btnDel_Click);
            // 
            // cbbMaloai
            // 
            this.cbbMaloai.DisplayMember = "Text";
            this.cbbMaloai.DrawMode = System.Windows.Forms.DrawMode.OwnerDrawFixed;
            this.cbbMaloai.FormattingEnabled = true;
            this.cbbMaloai.ItemHeight = 19;
            this.cbbMaloai.Location = new System.Drawing.Point(143, 39);
            this.cbbMaloai.Name = "cbbMaloai";
            this.cbbMaloai.Size = new System.Drawing.Size(169, 25);
            this.cbbMaloai.TabIndex = 1;
            this.cbbMaloai.SelectedIndexChanged += new System.EventHandler(this.cbbMaloai_SelectedIndexChanged);
            this.cbbMaloai.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.cbbMaloai_KeyPress);
            this.cbbMaloai.TextChanged += new System.EventHandler(this.cbbMaloai_TextChanged);
            // 
            // btnAdd
            // 
            this.btnAdd.AccessibleRole = System.Windows.Forms.AccessibleRole.PushButton;
            this.btnAdd.Location = new System.Drawing.Point(388, 187);
            this.btnAdd.Name = "btnAdd";
            this.btnAdd.Size = new System.Drawing.Size(75, 23);
            this.btnAdd.TabIndex = 6;
            this.btnAdd.Text = "Thêm";
            this.btnAdd.Click += new System.EventHandler(this.btnAdd_Click);
            // 
            // labelX5
            // 
            this.labelX5.Location = new System.Drawing.Point(77, 70);
            this.labelX5.Name = "labelX5";
            this.labelX5.Size = new System.Drawing.Size(60, 26);
            this.labelX5.TabIndex = 143;
            this.labelX5.Text = "Tiêu chí";
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
            this.dgvDanhsach.Location = new System.Drawing.Point(12, 227);
            this.dgvDanhsach.Name = "dgvDanhsach";
            dataGridViewCellStyle2.Font = new System.Drawing.Font("Arial", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.dgvDanhsach.RowsDefaultCellStyle = dataGridViewCellStyle2;
            this.dgvDanhsach.Size = new System.Drawing.Size(498, 218);
            this.dgvDanhsach.TabIndex = 9;
            this.dgvDanhsach.CellClick += new System.Windows.Forms.DataGridViewCellEventHandler(this.dgvDanhsach_CellClick);
            // 
            // txtTieuchi
            // 
            // 
            // 
            // 
            this.txtTieuchi.Border.Class = "TextBoxBorder";
            this.txtTieuchi.Location = new System.Drawing.Point(143, 70);
            this.txtTieuchi.Name = "txtTieuchi";
            this.txtTieuchi.Size = new System.Drawing.Size(298, 26);
            this.txtTieuchi.TabIndex = 2;
            // 
            // labelX4
            // 
            this.labelX4.Location = new System.Drawing.Point(165, 3);
            this.labelX4.Margin = new System.Windows.Forms.Padding(4);
            this.labelX4.Name = "labelX4";
            this.labelX4.Size = new System.Drawing.Size(197, 29);
            this.labelX4.TabIndex = 141;
            this.labelX4.Text = "Bảng: Xếp loại khách hàng";
            this.labelX4.TextAlignment = System.Drawing.StringAlignment.Center;
            // 
            // labelX6
            // 
            this.labelX6.Location = new System.Drawing.Point(77, 138);
            this.labelX6.Name = "labelX6";
            this.labelX6.Size = new System.Drawing.Size(60, 23);
            this.labelX6.TabIndex = 171;
            this.labelX6.Text = "Loại KH";
            this.labelX6.TextAlignment = System.Drawing.StringAlignment.Far;
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.rdbDN);
            this.groupBox1.Controls.Add(this.rdbCN);
            this.groupBox1.Location = new System.Drawing.Point(143, 126);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(227, 39);
            this.groupBox1.TabIndex = 172;
            this.groupBox1.TabStop = false;
            // 
            // rdbDN
            // 
            this.rdbDN.AutoSize = true;
            this.rdbDN.Location = new System.Drawing.Point(100, 13);
            this.rdbDN.Name = "rdbDN";
            this.rdbDN.Size = new System.Drawing.Size(123, 22);
            this.rdbDN.TabIndex = 5;
            this.rdbDN.TabStop = true;
            this.rdbDN.Text = "Doanh nghiệp";
            this.rdbDN.UseVisualStyleBackColor = true;
            // 
            // rdbCN
            // 
            this.rdbCN.AutoSize = true;
            this.rdbCN.Checked = true;
            this.rdbCN.Location = new System.Drawing.Point(6, 13);
            this.rdbCN.Name = "rdbCN";
            this.rdbCN.Size = new System.Drawing.Size(84, 22);
            this.rdbCN.TabIndex = 4;
            this.rdbCN.TabStop = true;
            this.rdbCN.Text = "Cá nhân";
            this.rdbCN.UseVisualStyleBackColor = true;
            this.rdbCN.CheckedChanged += new System.EventHandler(this.rdbCN_CheckedChanged);
            // 
            // cbbTheKH
            // 
            this.cbbTheKH.DisplayMember = "Text";
            this.cbbTheKH.DrawMode = System.Windows.Forms.DrawMode.OwnerDrawFixed;
            this.cbbTheKH.FormattingEnabled = true;
            this.cbbTheKH.ItemHeight = 19;
            this.cbbTheKH.Location = new System.Drawing.Point(143, 101);
            this.cbbTheKH.Name = "cbbTheKH";
            this.cbbTheKH.Size = new System.Drawing.Size(169, 25);
            this.cbbTheKH.TabIndex = 3;
            // 
            // btnExit
            // 
            this.btnExit.AccessibleRole = System.Windows.Forms.AccessibleRole.PushButton;
            this.btnExit.Location = new System.Drawing.Point(388, 461);
            this.btnExit.Name = "btnExit";
            this.btnExit.Size = new System.Drawing.Size(75, 23);
            this.btnExit.TabIndex = 173;
            this.btnExit.Text = "Đóng";
            this.btnExit.Click += new System.EventHandler(this.btnExit_Click);
            // 
            // frmDM_XeploaiKH
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(9F, 18F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(522, 496);
            this.Controls.Add(this.btnExit);
            this.Controls.Add(this.cbbTheKH);
            this.Controls.Add(this.labelX6);
            this.Controls.Add(this.labelX1);
            this.Controls.Add(this.labelX7);
            this.Controls.Add(this.btnModify);
            this.Controls.Add(this.btnDel);
            this.Controls.Add(this.cbbMaloai);
            this.Controls.Add(this.btnAdd);
            this.Controls.Add(this.labelX5);
            this.Controls.Add(this.dgvDanhsach);
            this.Controls.Add(this.txtTieuchi);
            this.Controls.Add(this.labelX4);
            this.Controls.Add(this.groupBox1);
            this.Font = new System.Drawing.Font("Arial", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.Margin = new System.Windows.Forms.Padding(4);
            this.Name = "frmDM_XeploaiKH";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Danh muc - Xep loai KH";
            this.Load += new System.EventHandler(this.frmDM_XeploaiKH_Load);
            ((System.ComponentModel.ISupportInitialize)(this.dgvDanhsach)).EndInit();
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private DevComponents.DotNetBar.LabelX labelX1;
        private DevComponents.DotNetBar.LabelX labelX7;
        private DevComponents.DotNetBar.ButtonX btnModify;
        private DevComponents.DotNetBar.ButtonX btnDel;
        private DevComponents.DotNetBar.Controls.ComboBoxEx cbbMaloai;
        private DevComponents.DotNetBar.ButtonX btnAdd;
        private DevComponents.DotNetBar.LabelX labelX5;
        private DevComponents.DotNetBar.Controls.DataGridViewX dgvDanhsach;
        private DevComponents.DotNetBar.Controls.TextBoxX txtTieuchi;
        private DevComponents.DotNetBar.LabelX labelX4;
        private DevComponents.DotNetBar.LabelX labelX6;
        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.RadioButton rdbDN;
        private System.Windows.Forms.RadioButton rdbCN;
        private DevComponents.DotNetBar.Controls.ComboBoxEx cbbTheKH;
        private DevComponents.DotNetBar.ButtonX btnExit;
    }
}