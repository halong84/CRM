namespace CRM
{
    partial class frmTT_KetquathamdoTC
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
            this.cbHinhthuc = new DevComponents.DotNetBar.Controls.ComboBoxEx();
            this.comboItem11 = new DevComponents.Editors.ComboItem();
            this.comboItem12 = new DevComponents.Editors.ComboItem();
            this.comboItem5 = new DevComponents.Editors.ComboItem();
            this.comboItem6 = new DevComponents.Editors.ComboItem();
            this.comboItem7 = new DevComponents.Editors.ComboItem();
            this.labelX18 = new DevComponents.DotNetBar.LabelX();
            this.dtpNgaytt = new System.Windows.Forms.DateTimePicker();
            this.labelX42 = new DevComponents.DotNetBar.LabelX();
            this.panel19 = new System.Windows.Forms.Panel();
            this.labelX10 = new DevComponents.DotNetBar.LabelX();
            this.txtDiachi = new System.Windows.Forms.TextBox();
            this.labelX9 = new DevComponents.DotNetBar.LabelX();
            this.txtHoten = new System.Windows.Forms.TextBox();
            this.labelX8 = new DevComponents.DotNetBar.LabelX();
            this.txtEmail = new System.Windows.Forms.TextBox();
            this.labelX7 = new DevComponents.DotNetBar.LabelX();
            this.txtDienthoai = new System.Windows.Forms.TextBox();
            this.labelX26 = new DevComponents.DotNetBar.LabelX();
            this.dataGridViewX1 = new DevComponents.DotNetBar.Controls.DataGridViewX();
            this.cbHanmuc = new System.Windows.Forms.ComboBox();
            this.labelX37 = new DevComponents.DotNetBar.LabelX();
            this.btnAdd = new DevComponents.DotNetBar.ButtonX();
            this.btnLuu = new DevComponents.DotNetBar.ButtonX();
            this.panel19.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dataGridViewX1)).BeginInit();
            this.SuspendLayout();
            // 
            // cbHinhthuc
            // 
            this.cbHinhthuc.DisplayMember = "Text";
            this.cbHinhthuc.DrawMode = System.Windows.Forms.DrawMode.OwnerDrawFixed;
            this.cbHinhthuc.DropDownHeight = 100;
            this.cbHinhthuc.DropDownWidth = 99;
            this.cbHinhthuc.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.cbHinhthuc.FormattingEnabled = true;
            this.cbHinhthuc.IntegralHeight = false;
            this.cbHinhthuc.ItemHeight = 19;
            this.cbHinhthuc.Items.AddRange(new object[] {
            this.comboItem11,
            this.comboItem12,
            this.comboItem5,
            this.comboItem6,
            this.comboItem7});
            this.cbHinhthuc.Location = new System.Drawing.Point(475, 8);
            this.cbHinhthuc.Name = "cbHinhthuc";
            this.cbHinhthuc.Size = new System.Drawing.Size(165, 25);
            this.cbHinhthuc.TabIndex = 262;
            // 
            // comboItem11
            // 
            this.comboItem11.FontSize = 10F;
            this.comboItem11.Text = "Trực tiếp";
            // 
            // comboItem12
            // 
            this.comboItem12.FontSize = 10F;
            this.comboItem12.Text = "Điện thoại";
            // 
            // comboItem5
            // 
            this.comboItem5.FontSize = 10F;
            this.comboItem5.Text = "Thư,fax";
            // 
            // comboItem6
            // 
            this.comboItem6.FontSize = 10F;
            this.comboItem6.Text = "Internet";
            // 
            // comboItem7
            // 
            this.comboItem7.Text = "Phiếu thu thập";
            // 
            // labelX18
            // 
            this.labelX18.Font = new System.Drawing.Font("Arial", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.labelX18.ForeColor = System.Drawing.SystemColors.ControlText;
            this.labelX18.Location = new System.Drawing.Point(317, 12);
            this.labelX18.Name = "labelX18";
            this.labelX18.Size = new System.Drawing.Size(162, 19);
            this.labelX18.TabIndex = 263;
            this.labelX18.Text = "Hình thức tương tác:";
            // 
            // dtpNgaytt
            // 
            this.dtpNgaytt.Format = System.Windows.Forms.DateTimePickerFormat.Custom;
            this.dtpNgaytt.Location = new System.Drawing.Point(165, 13);
            this.dtpNgaytt.Name = "dtpNgaytt";
            this.dtpNgaytt.Size = new System.Drawing.Size(119, 20);
            this.dtpNgaytt.TabIndex = 260;
            // 
            // labelX42
            // 
            this.labelX42.Font = new System.Drawing.Font("Arial", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.labelX42.ForeColor = System.Drawing.SystemColors.ControlText;
            this.labelX42.Location = new System.Drawing.Point(12, 12);
            this.labelX42.Name = "labelX42";
            this.labelX42.Size = new System.Drawing.Size(147, 20);
            this.labelX42.TabIndex = 261;
            this.labelX42.Text = "Thời gian tương tác:";
            // 
            // panel19
            // 
            this.panel19.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.panel19.Controls.Add(this.labelX10);
            this.panel19.Controls.Add(this.txtDiachi);
            this.panel19.Controls.Add(this.labelX9);
            this.panel19.Controls.Add(this.txtHoten);
            this.panel19.Controls.Add(this.labelX8);
            this.panel19.Controls.Add(this.txtEmail);
            this.panel19.Controls.Add(this.labelX7);
            this.panel19.Controls.Add(this.txtDienthoai);
            this.panel19.Controls.Add(this.labelX26);
            this.panel19.Location = new System.Drawing.Point(12, 38);
            this.panel19.Name = "panel19";
            this.panel19.Size = new System.Drawing.Size(931, 92);
            this.panel19.TabIndex = 264;
            // 
            // labelX10
            // 
            this.labelX10.Font = new System.Drawing.Font("Arial", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.labelX10.ForeColor = System.Drawing.SystemColors.ControlText;
            this.labelX10.Location = new System.Drawing.Point(472, 24);
            this.labelX10.Name = "labelX10";
            this.labelX10.Size = new System.Drawing.Size(55, 21);
            this.labelX10.TabIndex = 286;
            this.labelX10.Text = "Địa chỉ:";
            // 
            // txtDiachi
            // 
            this.txtDiachi.Font = new System.Drawing.Font("Arial", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtDiachi.Location = new System.Drawing.Point(533, 24);
            this.txtDiachi.Name = "txtDiachi";
            this.txtDiachi.Size = new System.Drawing.Size(347, 22);
            this.txtDiachi.TabIndex = 285;
            // 
            // labelX9
            // 
            this.labelX9.Font = new System.Drawing.Font("Arial", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.labelX9.ForeColor = System.Drawing.SystemColors.ControlText;
            this.labelX9.Location = new System.Drawing.Point(3, 24);
            this.labelX9.Name = "labelX9";
            this.labelX9.Size = new System.Drawing.Size(122, 21);
            this.labelX9.TabIndex = 284;
            this.labelX9.Text = "Tên tổ chức,DN:";
            // 
            // txtHoten
            // 
            this.txtHoten.Font = new System.Drawing.Font("Arial", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtHoten.Location = new System.Drawing.Point(132, 25);
            this.txtHoten.Name = "txtHoten";
            this.txtHoten.Size = new System.Drawing.Size(334, 22);
            this.txtHoten.TabIndex = 283;
            // 
            // labelX8
            // 
            this.labelX8.Font = new System.Drawing.Font("Arial", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.labelX8.ForeColor = System.Drawing.SystemColors.ControlText;
            this.labelX8.Location = new System.Drawing.Point(472, 55);
            this.labelX8.Name = "labelX8";
            this.labelX8.Size = new System.Drawing.Size(55, 21);
            this.labelX8.TabIndex = 282;
            this.labelX8.Text = "Email:";
            // 
            // txtEmail
            // 
            this.txtEmail.Font = new System.Drawing.Font("Arial", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtEmail.Location = new System.Drawing.Point(533, 55);
            this.txtEmail.Name = "txtEmail";
            this.txtEmail.Size = new System.Drawing.Size(209, 22);
            this.txtEmail.TabIndex = 281;
            // 
            // labelX7
            // 
            this.labelX7.Font = new System.Drawing.Font("Arial", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.labelX7.ForeColor = System.Drawing.SystemColors.ControlText;
            this.labelX7.Location = new System.Drawing.Point(3, 54);
            this.labelX7.Name = "labelX7";
            this.labelX7.Size = new System.Drawing.Size(86, 20);
            this.labelX7.TabIndex = 280;
            this.labelX7.Text = "Điện thoại:";
            // 
            // txtDienthoai
            // 
            this.txtDienthoai.Font = new System.Drawing.Font("Arial", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtDienthoai.Location = new System.Drawing.Point(132, 52);
            this.txtDienthoai.Name = "txtDienthoai";
            this.txtDienthoai.Size = new System.Drawing.Size(144, 22);
            this.txtDienthoai.TabIndex = 279;
            // 
            // labelX26
            // 
            this.labelX26.Font = new System.Drawing.Font("Arial", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.labelX26.ForeColor = System.Drawing.Color.Red;
            this.labelX26.Location = new System.Drawing.Point(3, -1);
            this.labelX26.Name = "labelX26";
            this.labelX26.Size = new System.Drawing.Size(168, 23);
            this.labelX26.TabIndex = 94;
            this.labelX26.Text = "Thông tin khách hàng:";
            // 
            // dataGridViewX1
            // 
            this.dataGridViewX1.AllowUserToAddRows = false;
            this.dataGridViewX1.AllowUserToDeleteRows = false;
            this.dataGridViewX1.BackgroundColor = System.Drawing.SystemColors.Control;
            this.dataGridViewX1.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            dataGridViewCellStyle1.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle1.BackColor = System.Drawing.SystemColors.Window;
            dataGridViewCellStyle1.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            dataGridViewCellStyle1.ForeColor = System.Drawing.SystemColors.ControlText;
            dataGridViewCellStyle1.SelectionBackColor = System.Drawing.SystemColors.Highlight;
            dataGridViewCellStyle1.SelectionForeColor = System.Drawing.SystemColors.ControlText;
            dataGridViewCellStyle1.WrapMode = System.Windows.Forms.DataGridViewTriState.False;
            this.dataGridViewX1.DefaultCellStyle = dataGridViewCellStyle1;
            this.dataGridViewX1.GridColor = System.Drawing.Color.FromArgb(((int)(((byte)(208)))), ((int)(((byte)(215)))), ((int)(((byte)(229)))));
            this.dataGridViewX1.Location = new System.Drawing.Point(12, 165);
            this.dataGridViewX1.Name = "dataGridViewX1";
            this.dataGridViewX1.Size = new System.Drawing.Size(926, 241);
            this.dataGridViewX1.TabIndex = 267;
            this.dataGridViewX1.CellContentClick += new System.Windows.Forms.DataGridViewCellEventHandler(this.dataGridViewX1_CellContentClick);
            // 
            // cbHanmuc
            // 
            this.cbHanmuc.FormattingEnabled = true;
            this.cbHanmuc.Location = new System.Drawing.Point(162, 136);
            this.cbHanmuc.Name = "cbHanmuc";
            this.cbHanmuc.Size = new System.Drawing.Size(339, 21);
            this.cbHanmuc.TabIndex = 266;
            this.cbHanmuc.TextChanged += new System.EventHandler(this.cbHanmuc_TextChanged);
            // 
            // labelX37
            // 
            this.labelX37.Font = new System.Drawing.Font("Arial", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.labelX37.ForeColor = System.Drawing.SystemColors.ControlText;
            this.labelX37.Location = new System.Drawing.Point(12, 136);
            this.labelX37.Name = "labelX37";
            this.labelX37.Size = new System.Drawing.Size(144, 23);
            this.labelX37.TabIndex = 265;
            this.labelX37.Text = "Hạn mục khảo sát:";
            // 
            // btnAdd
            // 
            this.btnAdd.AccessibleRole = System.Windows.Forms.AccessibleRole.PushButton;
            this.btnAdd.Font = new System.Drawing.Font("Arial", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnAdd.Location = new System.Drawing.Point(12, 412);
            this.btnAdd.Name = "btnAdd";
            this.btnAdd.Size = new System.Drawing.Size(117, 29);
            this.btnAdd.TabIndex = 269;
            this.btnAdd.Text = "Thêm";
            this.btnAdd.Click += new System.EventHandler(this.btnAdd_Click);
            // 
            // btnLuu
            // 
            this.btnLuu.AccessibleRole = System.Windows.Forms.AccessibleRole.PushButton;
            this.btnLuu.Font = new System.Drawing.Font("Arial", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnLuu.Location = new System.Drawing.Point(140, 412);
            this.btnLuu.Name = "btnLuu";
            this.btnLuu.Size = new System.Drawing.Size(117, 29);
            this.btnLuu.TabIndex = 268;
            this.btnLuu.Text = "Lưu";
            this.btnLuu.Click += new System.EventHandler(this.btnLuu_Click);
            // 
            // frmTT_KetquathamdoTC
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(961, 450);
            this.Controls.Add(this.btnAdd);
            this.Controls.Add(this.btnLuu);
            this.Controls.Add(this.dataGridViewX1);
            this.Controls.Add(this.cbHanmuc);
            this.Controls.Add(this.labelX37);
            this.Controls.Add(this.panel19);
            this.Controls.Add(this.cbHinhthuc);
            this.Controls.Add(this.labelX18);
            this.Controls.Add(this.dtpNgaytt);
            this.Controls.Add(this.labelX42);
            this.Name = "frmTT_KetquathamdoTC";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "frmTT_KetquathamdoTC";
            this.Load += new System.EventHandler(this.frmTT_KetquathamdoTC_Load);
            this.panel19.ResumeLayout(false);
            this.panel19.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dataGridViewX1)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private DevComponents.DotNetBar.Controls.ComboBoxEx cbHinhthuc;
        private DevComponents.Editors.ComboItem comboItem11;
        private DevComponents.Editors.ComboItem comboItem12;
        private DevComponents.Editors.ComboItem comboItem5;
        private DevComponents.Editors.ComboItem comboItem6;
        private DevComponents.Editors.ComboItem comboItem7;
        private DevComponents.DotNetBar.LabelX labelX18;
        private System.Windows.Forms.DateTimePicker dtpNgaytt;
        private DevComponents.DotNetBar.LabelX labelX42;
        private System.Windows.Forms.Panel panel19;
        private DevComponents.DotNetBar.LabelX labelX10;
        private System.Windows.Forms.TextBox txtDiachi;
        private DevComponents.DotNetBar.LabelX labelX9;
        private System.Windows.Forms.TextBox txtHoten;
        private DevComponents.DotNetBar.LabelX labelX8;
        private System.Windows.Forms.TextBox txtEmail;
        private DevComponents.DotNetBar.LabelX labelX7;
        private System.Windows.Forms.TextBox txtDienthoai;
        private DevComponents.DotNetBar.LabelX labelX26;
        private DevComponents.DotNetBar.Controls.DataGridViewX dataGridViewX1;
        private System.Windows.Forms.ComboBox cbHanmuc;
        private DevComponents.DotNetBar.LabelX labelX37;
        private DevComponents.DotNetBar.ButtonX btnAdd;
        private DevComponents.DotNetBar.ButtonX btnLuu;
    }
}