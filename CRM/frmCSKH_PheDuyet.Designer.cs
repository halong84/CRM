namespace CRM
{
    partial class frmCSKH_PheDuyet
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
            this.btnSelectall = new DevComponents.DotNetBar.ButtonX();
            this.btnCancel = new DevComponents.DotNetBar.ButtonX();
            this.btnConfirm = new DevComponents.DotNetBar.ButtonX();
            this.btnDeselectall = new DevComponents.DotNetBar.ButtonX();
            this.labelX5 = new DevComponents.DotNetBar.LabelX();
            this.dtpFrom = new System.Windows.Forms.DateTimePicker();
            this.btnSearch = new DevComponents.DotNetBar.ButtonX();
            this.labelX3 = new DevComponents.DotNetBar.LabelX();
            this.dgvDanhsach = new DevComponents.DotNetBar.Controls.DataGridViewX();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.optTatca = new System.Windows.Forms.RadioButton();
            this.optPD = new System.Windows.Forms.RadioButton();
            this.optCPD = new System.Windows.Forms.RadioButton();
            ((System.ComponentModel.ISupportInitialize)(this.dgvDanhsach)).BeginInit();
            this.groupBox1.SuspendLayout();
            this.SuspendLayout();
            // 
            // btnSelectall
            // 
            this.btnSelectall.AccessibleRole = System.Windows.Forms.AccessibleRole.PushButton;
            this.btnSelectall.Font = new System.Drawing.Font("Arial", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnSelectall.Location = new System.Drawing.Point(668, 508);
            this.btnSelectall.Name = "btnSelectall";
            this.btnSelectall.Size = new System.Drawing.Size(103, 29);
            this.btnSelectall.TabIndex = 267;
            this.btnSelectall.Text = "Chọn hết";
            this.btnSelectall.Click += new System.EventHandler(this.btnSelectall_Click);
            // 
            // btnCancel
            // 
            this.btnCancel.AccessibleRole = System.Windows.Forms.AccessibleRole.PushButton;
            this.btnCancel.Font = new System.Drawing.Font("Arial", 11F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnCancel.Location = new System.Drawing.Point(105, 508);
            this.btnCancel.Name = "btnCancel";
            this.btnCancel.Size = new System.Drawing.Size(103, 29);
            this.btnCancel.TabIndex = 268;
            this.btnCancel.Text = "Hủy phê duyệt";
            this.btnCancel.Visible = false;
            this.btnCancel.Click += new System.EventHandler(this.btnCancel_Click);
            // 
            // btnConfirm
            // 
            this.btnConfirm.AccessibleRole = System.Windows.Forms.AccessibleRole.PushButton;
            this.btnConfirm.Font = new System.Drawing.Font("Arial", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnConfirm.Location = new System.Drawing.Point(-4, 508);
            this.btnConfirm.Name = "btnConfirm";
            this.btnConfirm.Size = new System.Drawing.Size(103, 29);
            this.btnConfirm.TabIndex = 269;
            this.btnConfirm.Text = "Phê duyệt";
            this.btnConfirm.Click += new System.EventHandler(this.btnConfirm_Click);
            // 
            // btnDeselectall
            // 
            this.btnDeselectall.AccessibleRole = System.Windows.Forms.AccessibleRole.PushButton;
            this.btnDeselectall.Font = new System.Drawing.Font("Arial", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnDeselectall.Location = new System.Drawing.Point(559, 508);
            this.btnDeselectall.Name = "btnDeselectall";
            this.btnDeselectall.Size = new System.Drawing.Size(103, 29);
            this.btnDeselectall.TabIndex = 266;
            this.btnDeselectall.Text = "Bỏ chọn hết";
            this.btnDeselectall.Click += new System.EventHandler(this.btnDeselectall_Click);
            // 
            // labelX5
            // 
            this.labelX5.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.labelX5.BackColor = System.Drawing.SystemColors.ActiveCaption;
            this.labelX5.Font = new System.Drawing.Font("Arial", 15.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.labelX5.ForeColor = System.Drawing.SystemColors.ActiveCaptionText;
            this.labelX5.Location = new System.Drawing.Point(-4, 50);
            this.labelX5.Name = "labelX5";
            this.labelX5.SingleLineColor = System.Drawing.SystemColors.ActiveCaption;
            this.labelX5.Size = new System.Drawing.Size(790, 39);
            this.labelX5.TabIndex = 264;
            this.labelX5.Text = "                DANH SÁCH KHÁCH HÀNG";
            this.labelX5.TextAlignment = System.Drawing.StringAlignment.Center;
            // 
            // dtpFrom
            // 
            this.dtpFrom.CustomFormat = "MM/yyyy";
            this.dtpFrom.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.dtpFrom.Format = System.Windows.Forms.DateTimePickerFormat.Custom;
            this.dtpFrom.Location = new System.Drawing.Point(134, 18);
            this.dtpFrom.Name = "dtpFrom";
            this.dtpFrom.ShowUpDown = true;
            this.dtpFrom.Size = new System.Drawing.Size(95, 26);
            this.dtpFrom.TabIndex = 263;
            this.dtpFrom.Value = new System.DateTime(2018, 1, 1, 0, 0, 0, 0);
            // 
            // btnSearch
            // 
            this.btnSearch.AccessibleRole = System.Windows.Forms.AccessibleRole.PushButton;
            this.btnSearch.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnSearch.Location = new System.Drawing.Point(601, 19);
            this.btnSearch.Name = "btnSearch";
            this.btnSearch.Size = new System.Drawing.Size(93, 23);
            this.btnSearch.TabIndex = 262;
            this.btnSearch.Text = "Tìm kiếm";
            this.btnSearch.Click += new System.EventHandler(this.btnSearch_Click);
            // 
            // labelX3
            // 
            this.labelX3.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.labelX3.ForeColor = System.Drawing.SystemColors.ControlText;
            this.labelX3.Location = new System.Drawing.Point(74, 21);
            this.labelX3.Name = "labelX3";
            this.labelX3.Size = new System.Drawing.Size(54, 23);
            this.labelX3.TabIndex = 261;
            this.labelX3.Text = "Tháng:";
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
            this.dgvDanhsach.Location = new System.Drawing.Point(-4, 84);
            this.dgvDanhsach.Name = "dgvDanhsach";
            this.dgvDanhsach.RowHeadersVisible = false;
            this.dgvDanhsach.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            this.dgvDanhsach.Size = new System.Drawing.Size(790, 418);
            this.dgvDanhsach.TabIndex = 270;
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.optTatca);
            this.groupBox1.Controls.Add(this.optPD);
            this.groupBox1.Controls.Add(this.optCPD);
            this.groupBox1.Location = new System.Drawing.Point(241, 7);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(354, 40);
            this.groupBox1.TabIndex = 271;
            this.groupBox1.TabStop = false;
            // 
            // optTatca
            // 
            this.optTatca.AutoSize = true;
            this.optTatca.Checked = true;
            this.optTatca.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.optTatca.ForeColor = System.Drawing.SystemColors.ActiveCaption;
            this.optTatca.Location = new System.Drawing.Point(272, 11);
            this.optTatca.Name = "optTatca";
            this.optTatca.Size = new System.Drawing.Size(71, 24);
            this.optTatca.TabIndex = 15;
            this.optTatca.TabStop = true;
            this.optTatca.Text = "Tất cả";
            this.optTatca.UseVisualStyleBackColor = true;
            // 
            // optPD
            // 
            this.optPD.AutoSize = true;
            this.optPD.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.optPD.ForeColor = System.Drawing.SystemColors.ActiveCaption;
            this.optPD.Location = new System.Drawing.Point(151, 12);
            this.optPD.Name = "optPD";
            this.optPD.Size = new System.Drawing.Size(122, 24);
            this.optPD.TabIndex = 14;
            this.optPD.Text = "Đã phê duyệt";
            this.optPD.UseVisualStyleBackColor = true;
            // 
            // optCPD
            // 
            this.optCPD.AutoSize = true;
            this.optCPD.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.optCPD.ForeColor = System.Drawing.SystemColors.ActiveCaption;
            this.optCPD.Location = new System.Drawing.Point(6, 13);
            this.optCPD.Name = "optCPD";
            this.optCPD.Size = new System.Drawing.Size(139, 24);
            this.optCPD.TabIndex = 13;
            this.optCPD.Text = "Chưa phê duyệt";
            this.optCPD.UseVisualStyleBackColor = true;
            // 
            // frmCSKH_PheDuyet
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(782, 554);
            this.Controls.Add(this.groupBox1);
            this.Controls.Add(this.dgvDanhsach);
            this.Controls.Add(this.btnSelectall);
            this.Controls.Add(this.btnCancel);
            this.Controls.Add(this.btnConfirm);
            this.Controls.Add(this.btnDeselectall);
            this.Controls.Add(this.labelX5);
            this.Controls.Add(this.dtpFrom);
            this.Controls.Add(this.btnSearch);
            this.Controls.Add(this.labelX3);
            this.Name = "frmCSKH_PheDuyet";
            this.ShowIcon = false;
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Phê duyệt tích lũy điểm tiền gửi";
            this.Load += new System.EventHandler(this.frmCSKH_PheDuyet_Load);
            ((System.ComponentModel.ISupportInitialize)(this.dgvDanhsach)).EndInit();
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private DevComponents.DotNetBar.ButtonX btnSelectall;
        private DevComponents.DotNetBar.ButtonX btnCancel;
        private DevComponents.DotNetBar.ButtonX btnConfirm;
        private DevComponents.DotNetBar.ButtonX btnDeselectall;
        private DevComponents.DotNetBar.LabelX labelX5;
        private System.Windows.Forms.DateTimePicker dtpFrom;
        private DevComponents.DotNetBar.ButtonX btnSearch;
        private DevComponents.DotNetBar.LabelX labelX3;
        private DevComponents.DotNetBar.Controls.DataGridViewX dgvDanhsach;
        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.RadioButton optPD;
        private System.Windows.Forms.RadioButton optCPD;
        private System.Windows.Forms.RadioButton optTatca;

    }
}