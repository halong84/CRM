namespace CRM
{
    partial class frmCCDiem
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
            this.dgvDanhsach = new DevComponents.DotNetBar.Controls.DataGridViewX();
            this.btnModify = new DevComponents.DotNetBar.ButtonX();
            this.btnAdd = new DevComponents.DotNetBar.ButtonX();
            this.txtSoDiem = new DevComponents.DotNetBar.Controls.TextBoxX();
            this.labelX2 = new DevComponents.DotNetBar.LabelX();
            this.labelX1 = new DevComponents.DotNetBar.LabelX();
            this.cbLoaitien = new DevComponents.DotNetBar.Controls.ComboBoxEx();
            this.comboItem1 = new DevComponents.Editors.ComboItem();
            this.comboItem2 = new DevComponents.Editors.ComboItem();
            this.labelX5 = new DevComponents.DotNetBar.LabelX();
            this.txtSotien = new DevComponents.DotNetBar.Controls.TextBoxX();
            this.btnDel = new DevComponents.DotNetBar.ButtonX();
            this.dtpNgayHL = new System.Windows.Forms.DateTimePicker();
            this.labelX3 = new DevComponents.DotNetBar.LabelX();
            ((System.ComponentModel.ISupportInitialize)(this.dgvDanhsach)).BeginInit();
            this.SuspendLayout();
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
            this.dgvDanhsach.Location = new System.Drawing.Point(16, 197);
            this.dgvDanhsach.Name = "dgvDanhsach";
            this.dgvDanhsach.Size = new System.Drawing.Size(380, 226);
            this.dgvDanhsach.TabIndex = 51;
            this.dgvDanhsach.CellClick += new System.Windows.Forms.DataGridViewCellEventHandler(this.dgvDanhsach_CellClick);
            // 
            // btnModify
            // 
            this.btnModify.AccessibleRole = System.Windows.Forms.AccessibleRole.PushButton;
            this.btnModify.Enabled = false;
            this.btnModify.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnModify.Location = new System.Drawing.Point(159, 168);
            this.btnModify.Name = "btnModify";
            this.btnModify.Size = new System.Drawing.Size(75, 23);
            this.btnModify.TabIndex = 50;
            this.btnModify.Text = "Thay đổi";
            this.btnModify.Click += new System.EventHandler(this.btnModify_Click);
            // 
            // btnAdd
            // 
            this.btnAdd.AccessibleRole = System.Windows.Forms.AccessibleRole.PushButton;
            this.btnAdd.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnAdd.Location = new System.Drawing.Point(240, 168);
            this.btnAdd.Name = "btnAdd";
            this.btnAdd.Size = new System.Drawing.Size(75, 23);
            this.btnAdd.TabIndex = 49;
            this.btnAdd.Text = "Thêm";
            this.btnAdd.Click += new System.EventHandler(this.btnAdd_Click);
            // 
            // txtSoDiem
            // 
            // 
            // 
            // 
            this.txtSoDiem.Border.Class = "TextBoxBorder";
            this.txtSoDiem.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtSoDiem.Location = new System.Drawing.Point(178, 74);
            this.txtSoDiem.Name = "txtSoDiem";
            this.txtSoDiem.Size = new System.Drawing.Size(104, 26);
            this.txtSoDiem.TabIndex = 3;
            this.txtSoDiem.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.txtSoDiem_KeyPress);
            // 
            // labelX2
            // 
            this.labelX2.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.labelX2.Location = new System.Drawing.Point(67, 74);
            this.labelX2.Name = "labelX2";
            this.labelX2.Size = new System.Drawing.Size(70, 23);
            this.labelX2.TabIndex = 47;
            this.labelX2.Text = "Số điểm";
            // 
            // labelX1
            // 
            this.labelX1.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.labelX1.Location = new System.Drawing.Point(67, 45);
            this.labelX1.Name = "labelX1";
            this.labelX1.Size = new System.Drawing.Size(61, 23);
            this.labelX1.TabIndex = 45;
            this.labelX1.Text = "Số tiền";
            // 
            // cbLoaitien
            // 
            this.cbLoaitien.DisplayMember = "Text";
            this.cbLoaitien.DrawMode = System.Windows.Forms.DrawMode.OwnerDrawFixed;
            this.cbLoaitien.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.cbLoaitien.FormattingEnabled = true;
            this.cbLoaitien.ItemHeight = 19;
            this.cbLoaitien.Items.AddRange(new object[] {
            this.comboItem1,
            this.comboItem2});
            this.cbLoaitien.Location = new System.Drawing.Point(178, 12);
            this.cbLoaitien.Name = "cbLoaitien";
            this.cbLoaitien.Size = new System.Drawing.Size(104, 25);
            this.cbLoaitien.TabIndex = 1;
            // 
            // comboItem1
            // 
            this.comboItem1.Text = "VND";
            // 
            // comboItem2
            // 
            this.comboItem2.Text = "USD";
            // 
            // labelX5
            // 
            this.labelX5.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.labelX5.Location = new System.Drawing.Point(67, 12);
            this.labelX5.Name = "labelX5";
            this.labelX5.Size = new System.Drawing.Size(70, 25);
            this.labelX5.TabIndex = 43;
            this.labelX5.Text = "Loại tiền";
            // 
            // txtSotien
            // 
            // 
            // 
            // 
            this.txtSotien.Border.Class = "TextBoxBorder";
            this.txtSotien.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtSotien.Location = new System.Drawing.Point(178, 42);
            this.txtSotien.Name = "txtSotien";
            this.txtSotien.Size = new System.Drawing.Size(104, 26);
            this.txtSotien.TabIndex = 2;
            this.txtSotien.TextChanged += new System.EventHandler(this.txtSotien_TextChanged);
            this.txtSotien.KeyDown += new System.Windows.Forms.KeyEventHandler(this.txtSotien_KeyDown);
            this.txtSotien.Leave += new System.EventHandler(this.txtSotien_Leave);
            this.txtSotien.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.txtSotien_KeyPress);
            // 
            // btnDel
            // 
            this.btnDel.AccessibleRole = System.Windows.Forms.AccessibleRole.PushButton;
            this.btnDel.Enabled = false;
            this.btnDel.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnDel.Location = new System.Drawing.Point(321, 168);
            this.btnDel.Name = "btnDel";
            this.btnDel.Size = new System.Drawing.Size(75, 23);
            this.btnDel.TabIndex = 53;
            this.btnDel.Text = "Xóa";
            this.btnDel.Click += new System.EventHandler(this.btnDel_Click);
            // 
            // dtpNgayHL
            // 
            this.dtpNgayHL.CustomFormat = "dd/MM/yyyy";
            this.dtpNgayHL.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.dtpNgayHL.Format = System.Windows.Forms.DateTimePickerFormat.Custom;
            this.dtpNgayHL.Location = new System.Drawing.Point(178, 102);
            this.dtpNgayHL.Name = "dtpNgayHL";
            this.dtpNgayHL.ShowUpDown = true;
            this.dtpNgayHL.Size = new System.Drawing.Size(104, 26);
            this.dtpNgayHL.TabIndex = 4;
            // 
            // labelX3
            // 
            this.labelX3.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.labelX3.ForeColor = System.Drawing.SystemColors.ControlText;
            this.labelX3.Location = new System.Drawing.Point(67, 103);
            this.labelX3.Name = "labelX3";
            this.labelX3.Size = new System.Drawing.Size(105, 23);
            this.labelX3.TabIndex = 174;
            this.labelX3.Text = "Ngày hiệu lực";
            // 
            // frmCCDiem
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(422, 435);
            this.Controls.Add(this.dtpNgayHL);
            this.Controls.Add(this.labelX3);
            this.Controls.Add(this.btnDel);
            this.Controls.Add(this.txtSotien);
            this.Controls.Add(this.dgvDanhsach);
            this.Controls.Add(this.btnModify);
            this.Controls.Add(this.btnAdd);
            this.Controls.Add(this.txtSoDiem);
            this.Controls.Add(this.labelX2);
            this.Controls.Add(this.labelX1);
            this.Controls.Add(this.cbLoaitien);
            this.Controls.Add(this.labelX5);
            this.Name = "frmCCDiem";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "frmCCDiem";
            this.Load += new System.EventHandler(this.frmCCDiem_Load);
            ((System.ComponentModel.ISupportInitialize)(this.dgvDanhsach)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private DevComponents.DotNetBar.Controls.DataGridViewX dgvDanhsach;
        private DevComponents.DotNetBar.ButtonX btnModify;
        private DevComponents.DotNetBar.ButtonX btnAdd;
        private DevComponents.DotNetBar.Controls.TextBoxX txtSoDiem;
        private DevComponents.DotNetBar.LabelX labelX2;
        private DevComponents.DotNetBar.LabelX labelX1;
        private DevComponents.DotNetBar.Controls.ComboBoxEx cbLoaitien;
        private DevComponents.Editors.ComboItem comboItem1;
        private DevComponents.Editors.ComboItem comboItem2;
        private DevComponents.DotNetBar.LabelX labelX5;
        private DevComponents.DotNetBar.Controls.TextBoxX txtSotien;
        private DevComponents.DotNetBar.ButtonX btnDel;
        private System.Windows.Forms.DateTimePicker dtpNgayHL;
        private DevComponents.DotNetBar.LabelX labelX3;
    }
}