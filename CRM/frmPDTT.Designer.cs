namespace CRM
{
    partial class frmPDTT
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
            this.label1 = new System.Windows.Forms.Label();
            this.dtpTo = new System.Windows.Forms.DateTimePicker();
            this.dtpFrom = new System.Windows.Forms.DateTimePicker();
            this.labelX2 = new DevComponents.DotNetBar.LabelX();
            this.buttonX1 = new DevComponents.DotNetBar.ButtonX();
            this.labelX5 = new DevComponents.DotNetBar.LabelX();
            this.labelX12 = new DevComponents.DotNetBar.LabelX();
            this.cbCN = new DevComponents.DotNetBar.Controls.ComboBoxEx();
            this.btnConfirm = new DevComponents.DotNetBar.ButtonX();
            this.btnSelectall = new DevComponents.DotNetBar.ButtonX();
            this.btnDeselectall = new DevComponents.DotNetBar.ButtonX();
            this.dgvDanhsach = new DevComponents.DotNetBar.Controls.DataGridViewX();
            ((System.ComponentModel.ISupportInitialize)(this.dgvDanhsach)).BeginInit();
            this.SuspendLayout();
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(326, 49);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(10, 13);
            this.label1.TabIndex = 240;
            this.label1.Text = "-";
            // 
            // dtpTo
            // 
            this.dtpTo.CustomFormat = "MM/yyyy";
            this.dtpTo.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.dtpTo.Format = System.Windows.Forms.DateTimePickerFormat.Custom;
            this.dtpTo.Location = new System.Drawing.Point(342, 42);
            this.dtpTo.Name = "dtpTo";
            this.dtpTo.ShowUpDown = true;
            this.dtpTo.Size = new System.Drawing.Size(95, 26);
            this.dtpTo.TabIndex = 239;
            this.dtpTo.Value = new System.DateTime(2013, 1, 1, 0, 0, 0, 0);
            // 
            // dtpFrom
            // 
            this.dtpFrom.CustomFormat = "MM/yyyy";
            this.dtpFrom.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.dtpFrom.Format = System.Windows.Forms.DateTimePickerFormat.Custom;
            this.dtpFrom.Location = new System.Drawing.Point(225, 42);
            this.dtpFrom.Name = "dtpFrom";
            this.dtpFrom.ShowUpDown = true;
            this.dtpFrom.Size = new System.Drawing.Size(95, 26);
            this.dtpFrom.TabIndex = 238;
            this.dtpFrom.Value = new System.DateTime(2013, 1, 1, 0, 0, 0, 0);
            // 
            // labelX2
            // 
            this.labelX2.Font = new System.Drawing.Font("Arial", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.labelX2.ForeColor = System.Drawing.SystemColors.ControlText;
            this.labelX2.Location = new System.Drawing.Point(129, 45);
            this.labelX2.Name = "labelX2";
            this.labelX2.Size = new System.Drawing.Size(87, 20);
            this.labelX2.TabIndex = 236;
            this.labelX2.Text = "Kỳ xếp loại:";
            // 
            // buttonX1
            // 
            this.buttonX1.AccessibleRole = System.Windows.Forms.AccessibleRole.PushButton;
            this.buttonX1.Font = new System.Drawing.Font("Arial", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.buttonX1.Location = new System.Drawing.Point(522, 38);
            this.buttonX1.Name = "buttonX1";
            this.buttonX1.Size = new System.Drawing.Size(96, 35);
            this.buttonX1.TabIndex = 371;
            this.buttonX1.Text = "Tìm kiếm";
            this.buttonX1.Click += new System.EventHandler(this.buttonX1_Click);
            // 
            // labelX5
            // 
            this.labelX5.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.labelX5.BackColor = System.Drawing.SystemColors.ActiveCaption;
            this.labelX5.Font = new System.Drawing.Font("Arial", 15.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.labelX5.ForeColor = System.Drawing.SystemColors.ActiveCaptionText;
            this.labelX5.Location = new System.Drawing.Point(5, 74);
            this.labelX5.Name = "labelX5";
            this.labelX5.SingleLineColor = System.Drawing.SystemColors.ActiveCaption;
            this.labelX5.Size = new System.Drawing.Size(656, 39);
            this.labelX5.TabIndex = 370;
            this.labelX5.Text = "            DANH SÁCH CHI NHÁNH";
            this.labelX5.TextAlignment = System.Drawing.StringAlignment.Center;
            // 
            // labelX12
            // 
            this.labelX12.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.labelX12.ForeColor = System.Drawing.SystemColors.ControlText;
            this.labelX12.Location = new System.Drawing.Point(129, 12);
            this.labelX12.Name = "labelX12";
            this.labelX12.Size = new System.Drawing.Size(88, 22);
            this.labelX12.TabIndex = 369;
            this.labelX12.Text = "Chi nhánh :";
            // 
            // cbCN
            // 
            this.cbCN.DisplayMember = "Text";
            this.cbCN.DrawMode = System.Windows.Forms.DrawMode.OwnerDrawFixed;
            this.cbCN.DropDownHeight = 200;
            this.cbCN.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.cbCN.FormattingEnabled = true;
            this.cbCN.IntegralHeight = false;
            this.cbCN.ItemHeight = 19;
            this.cbCN.Location = new System.Drawing.Point(225, 9);
            this.cbCN.Name = "cbCN";
            this.cbCN.Size = new System.Drawing.Size(393, 25);
            this.cbCN.TabIndex = 368;
            // 
            // btnConfirm
            // 
            this.btnConfirm.AccessibleRole = System.Windows.Forms.AccessibleRole.PushButton;
            this.btnConfirm.Font = new System.Drawing.Font("Arial", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnConfirm.Location = new System.Drawing.Point(12, 496);
            this.btnConfirm.Name = "btnConfirm";
            this.btnConfirm.Size = new System.Drawing.Size(103, 29);
            this.btnConfirm.TabIndex = 373;
            this.btnConfirm.Text = "Phê duyệt";
            this.btnConfirm.Click += new System.EventHandler(this.btnConfirm_Click);
            // 
            // btnSelectall
            // 
            this.btnSelectall.AccessibleRole = System.Windows.Forms.AccessibleRole.PushButton;
            this.btnSelectall.Font = new System.Drawing.Font("Arial", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnSelectall.Location = new System.Drawing.Point(549, 496);
            this.btnSelectall.Name = "btnSelectall";
            this.btnSelectall.Size = new System.Drawing.Size(103, 29);
            this.btnSelectall.TabIndex = 375;
            this.btnSelectall.Text = "Chọn hết";
            this.btnSelectall.Click += new System.EventHandler(this.btnSelectall_Click);
            // 
            // btnDeselectall
            // 
            this.btnDeselectall.AccessibleRole = System.Windows.Forms.AccessibleRole.PushButton;
            this.btnDeselectall.Font = new System.Drawing.Font("Arial", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnDeselectall.Location = new System.Drawing.Point(440, 496);
            this.btnDeselectall.Name = "btnDeselectall";
            this.btnDeselectall.Size = new System.Drawing.Size(103, 29);
            this.btnDeselectall.TabIndex = 374;
            this.btnDeselectall.Text = "Bỏ chọn hết";
            this.btnDeselectall.Click += new System.EventHandler(this.btnDeselectall_Click);
            // 
            // dgvDanhsach
            // 
            this.dgvDanhsach.AllowUserToAddRows = false;
            this.dgvDanhsach.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.dgvDanhsach.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.Fill;
            this.dgvDanhsach.BackgroundColor = System.Drawing.SystemColors.Control;
            this.dgvDanhsach.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            dataGridViewCellStyle1.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleCenter;
            dataGridViewCellStyle1.BackColor = System.Drawing.SystemColors.Control;
            dataGridViewCellStyle1.Font = new System.Drawing.Font("Arial", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            dataGridViewCellStyle1.ForeColor = System.Drawing.SystemColors.WindowText;
            dataGridViewCellStyle1.SelectionBackColor = System.Drawing.SystemColors.Highlight;
            dataGridViewCellStyle1.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
            dataGridViewCellStyle1.WrapMode = System.Windows.Forms.DataGridViewTriState.True;
            this.dgvDanhsach.ColumnHeadersDefaultCellStyle = dataGridViewCellStyle1;
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
            this.dgvDanhsach.Location = new System.Drawing.Point(5, 110);
            this.dgvDanhsach.Name = "dgvDanhsach";
            this.dgvDanhsach.RowHeadersVisible = false;
            this.dgvDanhsach.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            this.dgvDanhsach.Size = new System.Drawing.Size(656, 372);
            this.dgvDanhsach.TabIndex = 376;
            // 
            // frmPDTT
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(664, 533);
            this.Controls.Add(this.dgvDanhsach);
            this.Controls.Add(this.btnSelectall);
            this.Controls.Add(this.btnDeselectall);
            this.Controls.Add(this.btnConfirm);
            this.Controls.Add(this.buttonX1);
            this.Controls.Add(this.labelX5);
            this.Controls.Add(this.labelX12);
            this.Controls.Add(this.cbCN);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.dtpTo);
            this.Controls.Add(this.dtpFrom);
            this.Controls.Add(this.labelX2);
            this.Name = "frmPDTT";
            this.ShowIcon = false;
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Phê duyệt xếp loại khách hàng toàn tỉnh";
            this.Load += new System.EventHandler(this.frmPDTT_Load);
            ((System.ComponentModel.ISupportInitialize)(this.dgvDanhsach)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.DateTimePicker dtpTo;
        private System.Windows.Forms.DateTimePicker dtpFrom;
        private DevComponents.DotNetBar.LabelX labelX2;
        private DevComponents.DotNetBar.ButtonX buttonX1;
        private DevComponents.DotNetBar.LabelX labelX5;
        private DevComponents.DotNetBar.LabelX labelX12;
        private DevComponents.DotNetBar.Controls.ComboBoxEx cbCN;
        private DevComponents.DotNetBar.ButtonX btnConfirm;
        private DevComponents.DotNetBar.ButtonX btnSelectall;
        private DevComponents.DotNetBar.ButtonX btnDeselectall;
        private DevComponents.DotNetBar.Controls.DataGridViewX dgvDanhsach;
    }
}