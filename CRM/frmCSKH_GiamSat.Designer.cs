namespace CRM
{
    partial class frmCSKH_GiamSat
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
            this.dtpFrom = new System.Windows.Forms.DateTimePicker();
            this.labelX3 = new DevComponents.DotNetBar.LabelX();
            this.labelX12 = new DevComponents.DotNetBar.LabelX();
            this.cbCN = new DevComponents.DotNetBar.Controls.ComboBoxEx();
            this.labelX5 = new DevComponents.DotNetBar.LabelX();
            this.buttonX1 = new DevComponents.DotNetBar.ButtonX();
            this.btnSelectall = new DevComponents.DotNetBar.ButtonX();
            this.btnDeselectall = new DevComponents.DotNetBar.ButtonX();
            this.btnConfirm = new DevComponents.DotNetBar.ButtonX();
            this.dgvDanhsach = new DevComponents.DotNetBar.Controls.DataGridViewX();
            this.btnCancel = new DevComponents.DotNetBar.ButtonX();
            ((System.ComponentModel.ISupportInitialize)(this.dgvDanhsach)).BeginInit();
            this.SuspendLayout();
            // 
            // dtpFrom
            // 
            this.dtpFrom.CustomFormat = "MM/yyyy";
            this.dtpFrom.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.dtpFrom.Format = System.Windows.Forms.DateTimePickerFormat.Custom;
            this.dtpFrom.Location = new System.Drawing.Point(108, 40);
            this.dtpFrom.Name = "dtpFrom";
            this.dtpFrom.ShowUpDown = true;
            this.dtpFrom.Size = new System.Drawing.Size(99, 26);
            this.dtpFrom.TabIndex = 265;
            // 
            // labelX3
            // 
            this.labelX3.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.labelX3.ForeColor = System.Drawing.SystemColors.ControlText;
            this.labelX3.Location = new System.Drawing.Point(14, 40);
            this.labelX3.Name = "labelX3";
            this.labelX3.Size = new System.Drawing.Size(88, 23);
            this.labelX3.TabIndex = 264;
            this.labelX3.Text = "Tháng:";
            // 
            // labelX12
            // 
            this.labelX12.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.labelX12.ForeColor = System.Drawing.SystemColors.ControlText;
            this.labelX12.Location = new System.Drawing.Point(14, 12);
            this.labelX12.Name = "labelX12";
            this.labelX12.Size = new System.Drawing.Size(88, 22);
            this.labelX12.TabIndex = 267;
            this.labelX12.Text = "Chi nhánh:";
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
            this.cbCN.Location = new System.Drawing.Point(108, 12);
            this.cbCN.Name = "cbCN";
            this.cbCN.Size = new System.Drawing.Size(396, 25);
            this.cbCN.TabIndex = 266;
            // 
            // labelX5
            // 
            this.labelX5.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.labelX5.BackColor = System.Drawing.SystemColors.ActiveCaption;
            this.labelX5.Font = new System.Drawing.Font("Arial", 15.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.labelX5.ForeColor = System.Drawing.SystemColors.ActiveCaptionText;
            this.labelX5.Location = new System.Drawing.Point(-1, 74);
            this.labelX5.Name = "labelX5";
            this.labelX5.SingleLineColor = System.Drawing.SystemColors.ActiveCaption;
            this.labelX5.Size = new System.Drawing.Size(647, 39);
            this.labelX5.TabIndex = 270;
            this.labelX5.Text = "            DANH SÁCH CHI NHÁNH";
            this.labelX5.TextAlignment = System.Drawing.StringAlignment.Center;
            // 
            // buttonX1
            // 
            this.buttonX1.AccessibleRole = System.Windows.Forms.AccessibleRole.PushButton;
            this.buttonX1.Font = new System.Drawing.Font("Arial", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.buttonX1.Location = new System.Drawing.Point(539, 28);
            this.buttonX1.Name = "buttonX1";
            this.buttonX1.Size = new System.Drawing.Size(96, 35);
            this.buttonX1.TabIndex = 271;
            this.buttonX1.Text = "Tìm kiếm";
            this.buttonX1.Click += new System.EventHandler(this.buttonX1_Click);
            // 
            // btnSelectall
            // 
            this.btnSelectall.AccessibleRole = System.Windows.Forms.AccessibleRole.PushButton;
            this.btnSelectall.Font = new System.Drawing.Font("Arial", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnSelectall.Location = new System.Drawing.Point(539, 504);
            this.btnSelectall.Name = "btnSelectall";
            this.btnSelectall.Size = new System.Drawing.Size(103, 29);
            this.btnSelectall.TabIndex = 378;
            this.btnSelectall.Text = "Chọn hết";
            this.btnSelectall.Click += new System.EventHandler(this.btnSelectall_Click);
            // 
            // btnDeselectall
            // 
            this.btnDeselectall.AccessibleRole = System.Windows.Forms.AccessibleRole.PushButton;
            this.btnDeselectall.Font = new System.Drawing.Font("Arial", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnDeselectall.Location = new System.Drawing.Point(430, 504);
            this.btnDeselectall.Name = "btnDeselectall";
            this.btnDeselectall.Size = new System.Drawing.Size(103, 29);
            this.btnDeselectall.TabIndex = 377;
            this.btnDeselectall.Text = "Bỏ chọn hết";
            this.btnDeselectall.Click += new System.EventHandler(this.btnDeselectall_Click);
            // 
            // btnConfirm
            // 
            this.btnConfirm.AccessibleRole = System.Windows.Forms.AccessibleRole.PushButton;
            this.btnConfirm.Font = new System.Drawing.Font("Arial", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnConfirm.Location = new System.Drawing.Point(-1, 504);
            this.btnConfirm.Name = "btnConfirm";
            this.btnConfirm.Size = new System.Drawing.Size(286, 29);
            this.btnConfirm.TabIndex = 376;
            this.btnConfirm.Text = "Xác nhận phê duyệt/hủy phê duyệt";
            this.btnConfirm.Click += new System.EventHandler(this.btnConfirm_Click);
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
            this.dgvDanhsach.Location = new System.Drawing.Point(-1, 108);
            this.dgvDanhsach.Name = "dgvDanhsach";
            this.dgvDanhsach.RowHeadersVisible = false;
            this.dgvDanhsach.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            this.dgvDanhsach.Size = new System.Drawing.Size(647, 390);
            this.dgvDanhsach.TabIndex = 379;
            // 
            // btnCancel
            // 
            this.btnCancel.AccessibleRole = System.Windows.Forms.AccessibleRole.PushButton;
            this.btnCancel.Font = new System.Drawing.Font("Arial", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnCancel.Location = new System.Drawing.Point(291, 504);
            this.btnCancel.Name = "btnCancel";
            this.btnCancel.Size = new System.Drawing.Size(124, 29);
            this.btnCancel.TabIndex = 380;
            this.btnCancel.Text = "Hủy phê duyệt";
            // 
            // frmCSKH_GiamSat
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(644, 536);
            this.Controls.Add(this.btnCancel);
            this.Controls.Add(this.dgvDanhsach);
            this.Controls.Add(this.btnSelectall);
            this.Controls.Add(this.btnDeselectall);
            this.Controls.Add(this.btnConfirm);
            this.Controls.Add(this.buttonX1);
            this.Controls.Add(this.labelX5);
            this.Controls.Add(this.labelX12);
            this.Controls.Add(this.cbCN);
            this.Controls.Add(this.dtpFrom);
            this.Controls.Add(this.labelX3);
            this.Name = "frmCSKH_GiamSat";
            this.ShowIcon = false;
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Phê duyệt tích lũy điểm tiền gửi toàn tỉnh";
            this.Load += new System.EventHandler(this.frmCSKH_GiamSat_Load);
            ((System.ComponentModel.ISupportInitialize)(this.dgvDanhsach)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.DateTimePicker dtpFrom;
        private DevComponents.DotNetBar.LabelX labelX3;
        private DevComponents.DotNetBar.LabelX labelX12;
        private DevComponents.DotNetBar.Controls.ComboBoxEx cbCN;
        private DevComponents.DotNetBar.LabelX labelX5;
        private DevComponents.DotNetBar.ButtonX buttonX1;
        private DevComponents.DotNetBar.ButtonX btnSelectall;
        private DevComponents.DotNetBar.ButtonX btnDeselectall;
        private DevComponents.DotNetBar.ButtonX btnConfirm;
        private DevComponents.DotNetBar.Controls.DataGridViewX dgvDanhsach;
        private DevComponents.DotNetBar.ButtonX btnCancel;
    }
}