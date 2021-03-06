namespace CRM
{
    partial class frmTKTGSPDV
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
            System.Windows.Forms.DataVisualization.Charting.ChartArea chartArea1 = new System.Windows.Forms.DataVisualization.Charting.ChartArea();
            System.Windows.Forms.DataVisualization.Charting.Legend legend1 = new System.Windows.Forms.DataVisualization.Charting.Legend();
            System.Windows.Forms.DataVisualization.Charting.Series series1 = new System.Windows.Forms.DataVisualization.Charting.Series();
            this.labelX5 = new DevComponents.DotNetBar.LabelX();
            this.dtpTo = new System.Windows.Forms.DateTimePicker();
            this.dtpFrom = new System.Windows.Forms.DateTimePicker();
            this.labelX1 = new DevComponents.DotNetBar.LabelX();
            this.labelX2 = new DevComponents.DotNetBar.LabelX();
            this.btnSearch = new DevComponents.DotNetBar.ButtonX();
            this.dataGridViewX2 = new DevComponents.DotNetBar.Controls.DataGridViewX();
            this.Chart1 = new System.Windows.Forms.DataVisualization.Charting.Chart();
            this.buttonX1 = new DevComponents.DotNetBar.ButtonX();
            this.groupBox10 = new System.Windows.Forms.GroupBox();
            this.optDN = new System.Windows.Forms.RadioButton();
            this.optCN = new System.Windows.Forms.RadioButton();
            this.labelX12 = new DevComponents.DotNetBar.LabelX();
            this.cbCN = new DevComponents.DotNetBar.Controls.ComboBoxEx();
            this.saveFileDialog1 = new System.Windows.Forms.SaveFileDialog();
            ((System.ComponentModel.ISupportInitialize)(this.dataGridViewX2)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.Chart1)).BeginInit();
            this.groupBox10.SuspendLayout();
            this.SuspendLayout();
            // 
            // labelX5
            // 
            this.labelX5.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.labelX5.BackColor = System.Drawing.SystemColors.ActiveCaption;
            this.labelX5.Font = new System.Drawing.Font("Arial", 15.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.labelX5.ForeColor = System.Drawing.SystemColors.ActiveCaptionText;
            this.labelX5.Location = new System.Drawing.Point(-2, 0);
            this.labelX5.Name = "labelX5";
            this.labelX5.SingleLineColor = System.Drawing.SystemColors.ActiveCaption;
            this.labelX5.Size = new System.Drawing.Size(861, 39);
            this.labelX5.TabIndex = 221;
            this.labelX5.Text = "CHƯƠNG TRÌNH KHUYẾN MÃI/CHÍNH SÁCH KHÁCH HÀNG CỦA NGÂN HÀNG";
            this.labelX5.TextAlignment = System.Drawing.StringAlignment.Center;
            // 
            // dtpTo
            // 
            this.dtpTo.CustomFormat = "dd/MM/yyyy";
            this.dtpTo.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.dtpTo.Format = System.Windows.Forms.DateTimePickerFormat.Custom;
            this.dtpTo.Location = new System.Drawing.Point(603, 55);
            this.dtpTo.Name = "dtpTo";
            this.dtpTo.ShowUpDown = true;
            this.dtpTo.Size = new System.Drawing.Size(90, 23);
            this.dtpTo.TabIndex = 268;
            // 
            // dtpFrom
            // 
            this.dtpFrom.CustomFormat = "dd/MM/yyyy";
            this.dtpFrom.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.dtpFrom.Format = System.Windows.Forms.DateTimePickerFormat.Custom;
            this.dtpFrom.Location = new System.Drawing.Point(483, 55);
            this.dtpFrom.Name = "dtpFrom";
            this.dtpFrom.ShowUpDown = true;
            this.dtpFrom.Size = new System.Drawing.Size(90, 23);
            this.dtpFrom.TabIndex = 269;
            // 
            // labelX1
            // 
            this.labelX1.Font = new System.Drawing.Font("Arial", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.labelX1.ForeColor = System.Drawing.SystemColors.ControlText;
            this.labelX1.Location = new System.Drawing.Point(579, 58);
            this.labelX1.Name = "labelX1";
            this.labelX1.Size = new System.Drawing.Size(18, 20);
            this.labelX1.TabIndex = 267;
            this.labelX1.Text = "->";
            // 
            // labelX2
            // 
            this.labelX2.Font = new System.Drawing.Font("Arial", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.labelX2.ForeColor = System.Drawing.SystemColors.ControlText;
            this.labelX2.Location = new System.Drawing.Point(429, 58);
            this.labelX2.Name = "labelX2";
            this.labelX2.Size = new System.Drawing.Size(48, 20);
            this.labelX2.TabIndex = 266;
            this.labelX2.Text = "Ngày:";
            // 
            // btnSearch
            // 
            this.btnSearch.AccessibleRole = System.Windows.Forms.AccessibleRole.PushButton;
            this.btnSearch.Font = new System.Drawing.Font("Arial", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnSearch.Location = new System.Drawing.Point(714, 55);
            this.btnSearch.Name = "btnSearch";
            this.btnSearch.Size = new System.Drawing.Size(93, 23);
            this.btnSearch.TabIndex = 265;
            this.btnSearch.Text = "Tìm kiếm";
            this.btnSearch.Click += new System.EventHandler(this.btnSearch_Click);
            // 
            // dataGridViewX2
            // 
            this.dataGridViewX2.AllowUserToAddRows = false;
            this.dataGridViewX2.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.dataGridViewX2.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.Fill;
            this.dataGridViewX2.BackgroundColor = System.Drawing.SystemColors.Control;
            this.dataGridViewX2.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            dataGridViewCellStyle1.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleCenter;
            dataGridViewCellStyle1.BackColor = System.Drawing.SystemColors.Control;
            dataGridViewCellStyle1.Font = new System.Drawing.Font("Times New Roman", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            dataGridViewCellStyle1.ForeColor = System.Drawing.SystemColors.WindowText;
            dataGridViewCellStyle1.SelectionBackColor = System.Drawing.SystemColors.Highlight;
            dataGridViewCellStyle1.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
            dataGridViewCellStyle1.WrapMode = System.Windows.Forms.DataGridViewTriState.True;
            this.dataGridViewX2.ColumnHeadersDefaultCellStyle = dataGridViewCellStyle1;
            this.dataGridViewX2.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            dataGridViewCellStyle2.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle2.BackColor = System.Drawing.SystemColors.Window;
            dataGridViewCellStyle2.Font = new System.Drawing.Font("Times New Roman", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            dataGridViewCellStyle2.ForeColor = System.Drawing.SystemColors.ControlText;
            dataGridViewCellStyle2.SelectionBackColor = System.Drawing.SystemColors.Highlight;
            dataGridViewCellStyle2.SelectionForeColor = System.Drawing.SystemColors.ControlText;
            dataGridViewCellStyle2.WrapMode = System.Windows.Forms.DataGridViewTriState.False;
            this.dataGridViewX2.DefaultCellStyle = dataGridViewCellStyle2;
            this.dataGridViewX2.GridColor = System.Drawing.Color.FromArgb(((int)(((byte)(208)))), ((int)(((byte)(215)))), ((int)(((byte)(229)))));
            this.dataGridViewX2.Location = new System.Drawing.Point(12, 92);
            this.dataGridViewX2.Name = "dataGridViewX2";
            this.dataGridViewX2.RowHeadersVisible = false;
            this.dataGridViewX2.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            this.dataGridViewX2.Size = new System.Drawing.Size(835, 178);
            this.dataGridViewX2.TabIndex = 271;
            this.dataGridViewX2.CellContentClick += new System.Windows.Forms.DataGridViewCellEventHandler(this.dataGridViewX2_CellContentClick);
            // 
            // Chart1
            // 
            chartArea1.Name = "ChartArea1";
            this.Chart1.ChartAreas.Add(chartArea1);
            legend1.Font = new System.Drawing.Font("Times New Roman", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            legend1.IsTextAutoFit = false;
            legend1.Name = "Legend1";
            legend1.TitleFont = new System.Drawing.Font("Times New Roman", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.Chart1.Legends.Add(legend1);
            this.Chart1.Location = new System.Drawing.Point(-2, 314);
            this.Chart1.Name = "Chart1";
            series1.ChartArea = "ChartArea1";
            series1.Font = new System.Drawing.Font("Times New Roman", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            series1.Legend = "Legend1";
            series1.Name = "Series1";
            this.Chart1.Series.Add(series1);
            this.Chart1.Size = new System.Drawing.Size(834, 147);
            this.Chart1.TabIndex = 270;
            this.Chart1.Text = "chart1";
            this.Chart1.Visible = false;
            // 
            // buttonX1
            // 
            this.buttonX1.AccessibleRole = System.Windows.Forms.AccessibleRole.PushButton;
            this.buttonX1.Font = new System.Drawing.Font("Arial", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.buttonX1.Location = new System.Drawing.Point(638, 276);
            this.buttonX1.Name = "buttonX1";
            this.buttonX1.Size = new System.Drawing.Size(72, 23);
            this.buttonX1.TabIndex = 272;
            this.buttonX1.Text = "Xuất Excel";
            this.buttonX1.Click += new System.EventHandler(this.buttonX1_Click);
            // 
            // groupBox10
            // 
            this.groupBox10.Controls.Add(this.optDN);
            this.groupBox10.Controls.Add(this.optCN);
            this.groupBox10.Location = new System.Drawing.Point(244, 41);
            this.groupBox10.Name = "groupBox10";
            this.groupBox10.Size = new System.Drawing.Size(164, 44);
            this.groupBox10.TabIndex = 393;
            this.groupBox10.TabStop = false;
            // 
            // optDN
            // 
            this.optDN.AutoSize = true;
            this.optDN.ForeColor = System.Drawing.SystemColors.ActiveCaption;
            this.optDN.Location = new System.Drawing.Point(86, 17);
            this.optDN.Name = "optDN";
            this.optDN.Size = new System.Drawing.Size(65, 17);
            this.optDN.TabIndex = 14;
            this.optDN.Text = "Tổ chức";
            this.optDN.UseVisualStyleBackColor = true;
            // 
            // optCN
            // 
            this.optCN.AutoSize = true;
            this.optCN.Checked = true;
            this.optCN.ForeColor = System.Drawing.SystemColors.ActiveCaption;
            this.optCN.Location = new System.Drawing.Point(15, 17);
            this.optCN.Name = "optCN";
            this.optCN.Size = new System.Drawing.Size(65, 17);
            this.optCN.TabIndex = 13;
            this.optCN.TabStop = true;
            this.optCN.Text = "Cá nhân";
            this.optCN.UseVisualStyleBackColor = true;
            // 
            // labelX12
            // 
            this.labelX12.Font = new System.Drawing.Font("Arial", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.labelX12.ForeColor = System.Drawing.SystemColors.ControlText;
            this.labelX12.Location = new System.Drawing.Point(33, 54);
            this.labelX12.Name = "labelX12";
            this.labelX12.Size = new System.Drawing.Size(88, 22);
            this.labelX12.TabIndex = 392;
            this.labelX12.Text = "Chi nhánh :";
            // 
            // cbCN
            // 
            this.cbCN.DisplayMember = "Text";
            this.cbCN.DrawMode = System.Windows.Forms.DrawMode.OwnerDrawFixed;
            this.cbCN.DropDownHeight = 200;
            this.cbCN.FormattingEnabled = true;
            this.cbCN.IntegralHeight = false;
            this.cbCN.Location = new System.Drawing.Point(127, 54);
            this.cbCN.Name = "cbCN";
            this.cbCN.Size = new System.Drawing.Size(99, 21);
            this.cbCN.TabIndex = 391;
            // 
            // frmTKTGSPDV
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(850, 569);
            this.Controls.Add(this.groupBox10);
            this.Controls.Add(this.labelX12);
            this.Controls.Add(this.cbCN);
            this.Controls.Add(this.buttonX1);
            this.Controls.Add(this.dataGridViewX2);
            this.Controls.Add(this.Chart1);
            this.Controls.Add(this.dtpTo);
            this.Controls.Add(this.dtpFrom);
            this.Controls.Add(this.labelX1);
            this.Controls.Add(this.labelX2);
            this.Controls.Add(this.btnSearch);
            this.Controls.Add(this.labelX5);
            this.Name = "frmTKTGSPDV";
            this.ShowIcon = false;
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Chương trình khuyến mãi";
            this.Load += new System.EventHandler(this.frmTKTGSPDV_Load);
            ((System.ComponentModel.ISupportInitialize)(this.dataGridViewX2)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.Chart1)).EndInit();
            this.groupBox10.ResumeLayout(false);
            this.groupBox10.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private DevComponents.DotNetBar.LabelX labelX5;
        private System.Windows.Forms.DateTimePicker dtpTo;
        private System.Windows.Forms.DateTimePicker dtpFrom;
        private DevComponents.DotNetBar.LabelX labelX1;
        private DevComponents.DotNetBar.LabelX labelX2;
        private DevComponents.DotNetBar.ButtonX btnSearch;
        private DevComponents.DotNetBar.Controls.DataGridViewX dataGridViewX2;
        private System.Windows.Forms.DataVisualization.Charting.Chart Chart1;
        private DevComponents.DotNetBar.ButtonX buttonX1;
        private System.Windows.Forms.GroupBox groupBox10;
        private System.Windows.Forms.RadioButton optDN;
        private System.Windows.Forms.RadioButton optCN;
        private DevComponents.DotNetBar.LabelX labelX12;
        private DevComponents.DotNetBar.Controls.ComboBoxEx cbCN;
        private System.Windows.Forms.SaveFileDialog saveFileDialog1;
    }
}