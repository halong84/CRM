namespace CRM
{
    partial class frmTK_CLSPDV
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
            System.Windows.Forms.DataVisualization.Charting.ChartArea chartArea1 = new System.Windows.Forms.DataVisualization.Charting.ChartArea();
            System.Windows.Forms.DataVisualization.Charting.Legend legend1 = new System.Windows.Forms.DataVisualization.Charting.Legend();
            System.Windows.Forms.DataVisualization.Charting.Series series1 = new System.Windows.Forms.DataVisualization.Charting.Series();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle1 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle2 = new System.Windows.Forms.DataGridViewCellStyle();
            this.labelX5 = new DevComponents.DotNetBar.LabelX();
            this.btnSearch = new DevComponents.DotNetBar.ButtonX();
            this.labelX4 = new DevComponents.DotNetBar.LabelX();
            this.labelX3 = new DevComponents.DotNetBar.LabelX();
            this.Chart1 = new System.Windows.Forms.DataVisualization.Charting.Chart();
            this.dataGridViewX2 = new DevComponents.DotNetBar.Controls.DataGridViewX();
            this.dtpFrom = new System.Windows.Forms.DateTimePicker();
            this.dtpTo = new System.Windows.Forms.DateTimePicker();
            this.groupBox10 = new System.Windows.Forms.GroupBox();
            this.optDN = new System.Windows.Forms.RadioButton();
            this.optCN = new System.Windows.Forms.RadioButton();
            this.labelX12 = new DevComponents.DotNetBar.LabelX();
            this.cbCN = new DevComponents.DotNetBar.Controls.ComboBoxEx();
            this.comboItem15 = new DevComponents.Editors.ComboItem();
            this.comboItem16 = new DevComponents.Editors.ComboItem();
            this.comboItem17 = new DevComponents.Editors.ComboItem();
            this.comboItem18 = new DevComponents.Editors.ComboItem();
            this.comboItem19 = new DevComponents.Editors.ComboItem();
            this.comboItem20 = new DevComponents.Editors.ComboItem();
            this.comboItem21 = new DevComponents.Editors.ComboItem();
            this.comboItem22 = new DevComponents.Editors.ComboItem();
            this.comboItem23 = new DevComponents.Editors.ComboItem();
            this.comboItem24 = new DevComponents.Editors.ComboItem();
            this.comboItem39 = new DevComponents.Editors.ComboItem();
            this.comboItem40 = new DevComponents.Editors.ComboItem();
            this.comboItem41 = new DevComponents.Editors.ComboItem();
            this.comboItem42 = new DevComponents.Editors.ComboItem();
            this.comboItem75 = new DevComponents.Editors.ComboItem();
            this.comboItem76 = new DevComponents.Editors.ComboItem();
            this.buttonX1 = new DevComponents.DotNetBar.ButtonX();
            this.saveFileDialog1 = new System.Windows.Forms.SaveFileDialog();
            ((System.ComponentModel.ISupportInitialize)(this.Chart1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.dataGridViewX2)).BeginInit();
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
            this.labelX5.Location = new System.Drawing.Point(-5, -2);
            this.labelX5.Name = "labelX5";
            this.labelX5.SingleLineColor = System.Drawing.SystemColors.ActiveCaption;
            this.labelX5.Size = new System.Drawing.Size(1008, 39);
            this.labelX5.TabIndex = 222;
            this.labelX5.Text = "MỨC ĐỘ HÀI LÒNG SẢN PHẨM DỊCH VỤ";
            this.labelX5.TextAlignment = System.Drawing.StringAlignment.Center;
            // 
            // btnSearch
            // 
            this.btnSearch.AccessibleRole = System.Windows.Forms.AccessibleRole.PushButton;
            this.btnSearch.Font = new System.Drawing.Font("Arial", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnSearch.Location = new System.Drawing.Point(771, 56);
            this.btnSearch.Name = "btnSearch";
            this.btnSearch.Size = new System.Drawing.Size(93, 23);
            this.btnSearch.TabIndex = 242;
            this.btnSearch.Text = "Tìm kiếm";
            this.btnSearch.Click += new System.EventHandler(this.btnSearch_Click);
            // 
            // labelX4
            // 
            this.labelX4.Font = new System.Drawing.Font("Arial", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.labelX4.ForeColor = System.Drawing.SystemColors.ControlText;
            this.labelX4.Location = new System.Drawing.Point(615, 58);
            this.labelX4.Name = "labelX4";
            this.labelX4.Size = new System.Drawing.Size(18, 20);
            this.labelX4.TabIndex = 241;
            this.labelX4.Text = "->";
            // 
            // labelX3
            // 
            this.labelX3.Font = new System.Drawing.Font("Arial", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.labelX3.ForeColor = System.Drawing.SystemColors.ControlText;
            this.labelX3.Location = new System.Drawing.Point(465, 58);
            this.labelX3.Name = "labelX3";
            this.labelX3.Size = new System.Drawing.Size(48, 20);
            this.labelX3.TabIndex = 238;
            this.labelX3.Text = "Ngày:";
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
            this.Chart1.Location = new System.Drawing.Point(25, 426);
            this.Chart1.Name = "Chart1";
            series1.ChartArea = "ChartArea1";
            series1.IsVisibleInLegend = false;
            series1.Legend = "Legend1";
            series1.Name = "Series1";
            this.Chart1.Series.Add(series1);
            this.Chart1.Size = new System.Drawing.Size(952, 288);
            this.Chart1.TabIndex = 237;
            this.Chart1.Text = "chart1";
            this.Chart1.Visible = false;
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
            this.dataGridViewX2.Location = new System.Drawing.Point(25, 94);
            this.dataGridViewX2.Name = "dataGridViewX2";
            this.dataGridViewX2.RowHeadersVisible = false;
            this.dataGridViewX2.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            this.dataGridViewX2.Size = new System.Drawing.Size(952, 284);
            this.dataGridViewX2.TabIndex = 236;
            this.dataGridViewX2.CellDoubleClick += new System.Windows.Forms.DataGridViewCellEventHandler(this.dataGridViewX2_CellDoubleClick);
            // 
            // dtpFrom
            // 
            this.dtpFrom.CustomFormat = "dd/MM/yyyy";
            this.dtpFrom.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.dtpFrom.Format = System.Windows.Forms.DateTimePickerFormat.Custom;
            this.dtpFrom.Location = new System.Drawing.Point(519, 55);
            this.dtpFrom.Name = "dtpFrom";
            this.dtpFrom.ShowUpDown = true;
            this.dtpFrom.Size = new System.Drawing.Size(90, 23);
            this.dtpFrom.TabIndex = 260;
            // 
            // dtpTo
            // 
            this.dtpTo.CustomFormat = "dd/MM/yyyy";
            this.dtpTo.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.dtpTo.Format = System.Windows.Forms.DateTimePickerFormat.Custom;
            this.dtpTo.Location = new System.Drawing.Point(639, 55);
            this.dtpTo.Name = "dtpTo";
            this.dtpTo.ShowUpDown = true;
            this.dtpTo.Size = new System.Drawing.Size(90, 23);
            this.dtpTo.TabIndex = 260;
            // 
            // groupBox10
            // 
            this.groupBox10.Controls.Add(this.optDN);
            this.groupBox10.Controls.Add(this.optCN);
            this.groupBox10.Location = new System.Drawing.Point(280, 44);
            this.groupBox10.Name = "groupBox10";
            this.groupBox10.Size = new System.Drawing.Size(164, 44);
            this.groupBox10.TabIndex = 387;
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
            this.labelX12.Location = new System.Drawing.Point(69, 57);
            this.labelX12.Name = "labelX12";
            this.labelX12.Size = new System.Drawing.Size(88, 22);
            this.labelX12.TabIndex = 386;
            this.labelX12.Text = "Chi nhánh :";
            // 
            // cbCN
            // 
            this.cbCN.DisplayMember = "Text";
            this.cbCN.DrawMode = System.Windows.Forms.DrawMode.OwnerDrawFixed;
            this.cbCN.DropDownHeight = 200;
            this.cbCN.FormattingEnabled = true;
            this.cbCN.IntegralHeight = false;
            this.cbCN.Items.AddRange(new object[] {
            this.comboItem15,
            this.comboItem16,
            this.comboItem17,
            this.comboItem18,
            this.comboItem19,
            this.comboItem20,
            this.comboItem21,
            this.comboItem22,
            this.comboItem23,
            this.comboItem24,
            this.comboItem39,
            this.comboItem40,
            this.comboItem41,
            this.comboItem42,
            this.comboItem75,
            this.comboItem76});
            this.cbCN.Location = new System.Drawing.Point(163, 57);
            this.cbCN.Name = "cbCN";
            this.cbCN.Size = new System.Drawing.Size(99, 21);
            this.cbCN.TabIndex = 385;
            // 
            // comboItem15
            // 
            this.comboItem15.Text = "Tất cả";
            // 
            // comboItem16
            // 
            this.comboItem16.Text = "4800";
            // 
            // comboItem17
            // 
            this.comboItem17.Text = "4801";
            // 
            // comboItem18
            // 
            this.comboItem18.Text = "4802";
            // 
            // comboItem19
            // 
            this.comboItem19.Text = "4803";
            // 
            // comboItem20
            // 
            this.comboItem20.Text = "4804";
            // 
            // comboItem21
            // 
            this.comboItem21.Text = "4805";
            // 
            // comboItem22
            // 
            this.comboItem22.Text = "4806";
            // 
            // comboItem23
            // 
            this.comboItem23.Text = "4807";
            // 
            // comboItem24
            // 
            this.comboItem24.Text = "4808";
            // 
            // comboItem39
            // 
            this.comboItem39.Text = "4809";
            // 
            // comboItem40
            // 
            this.comboItem40.Text = "4810";
            // 
            // comboItem41
            // 
            this.comboItem41.Text = "4811";
            // 
            // comboItem42
            // 
            this.comboItem42.Text = "4813";
            // 
            // comboItem75
            // 
            this.comboItem75.Text = "4814";
            // 
            // comboItem76
            // 
            this.comboItem76.Text = "4815";
            // 
            // buttonX1
            // 
            this.buttonX1.AccessibleRole = System.Windows.Forms.AccessibleRole.PushButton;
            this.buttonX1.Font = new System.Drawing.Font("Arial", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.buttonX1.Location = new System.Drawing.Point(771, 384);
            this.buttonX1.Name = "buttonX1";
            this.buttonX1.Size = new System.Drawing.Size(93, 23);
            this.buttonX1.TabIndex = 388;
            this.buttonX1.Text = "Xuất Excel";
            this.buttonX1.Click += new System.EventHandler(this.buttonX1_Click);
            // 
            // frmTK_CLSPDV
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1002, 746);
            this.Controls.Add(this.buttonX1);
            this.Controls.Add(this.groupBox10);
            this.Controls.Add(this.labelX12);
            this.Controls.Add(this.cbCN);
            this.Controls.Add(this.dtpTo);
            this.Controls.Add(this.dtpFrom);
            this.Controls.Add(this.btnSearch);
            this.Controls.Add(this.labelX4);
            this.Controls.Add(this.labelX3);
            this.Controls.Add(this.Chart1);
            this.Controls.Add(this.dataGridViewX2);
            this.Controls.Add(this.labelX5);
            this.Name = "frmTK_CLSPDV";
            this.Text = "Thong ke chat luong SPDV";
            this.Load += new System.EventHandler(this.TK_CLSPDV_Load);
            ((System.ComponentModel.ISupportInitialize)(this.Chart1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.dataGridViewX2)).EndInit();
            this.groupBox10.ResumeLayout(false);
            this.groupBox10.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private DevComponents.DotNetBar.LabelX labelX5;
        private DevComponents.DotNetBar.ButtonX btnSearch;
        private DevComponents.DotNetBar.LabelX labelX4;
        private DevComponents.DotNetBar.LabelX labelX3;
        private System.Windows.Forms.DataVisualization.Charting.Chart Chart1;
        private DevComponents.DotNetBar.Controls.DataGridViewX dataGridViewX2;
        private System.Windows.Forms.DateTimePicker dtpFrom;
        private System.Windows.Forms.DateTimePicker dtpTo;
        private System.Windows.Forms.GroupBox groupBox10;
        private System.Windows.Forms.RadioButton optDN;
        private System.Windows.Forms.RadioButton optCN;
        private DevComponents.DotNetBar.LabelX labelX12;
        private DevComponents.DotNetBar.Controls.ComboBoxEx cbCN;
        private DevComponents.Editors.ComboItem comboItem15;
        private DevComponents.Editors.ComboItem comboItem16;
        private DevComponents.Editors.ComboItem comboItem17;
        private DevComponents.Editors.ComboItem comboItem18;
        private DevComponents.Editors.ComboItem comboItem19;
        private DevComponents.Editors.ComboItem comboItem20;
        private DevComponents.Editors.ComboItem comboItem21;
        private DevComponents.Editors.ComboItem comboItem22;
        private DevComponents.Editors.ComboItem comboItem23;
        private DevComponents.Editors.ComboItem comboItem24;
        private DevComponents.Editors.ComboItem comboItem39;
        private DevComponents.Editors.ComboItem comboItem40;
        private DevComponents.Editors.ComboItem comboItem41;
        private DevComponents.Editors.ComboItem comboItem42;
        private DevComponents.Editors.ComboItem comboItem75;
        private DevComponents.Editors.ComboItem comboItem76;
        private DevComponents.DotNetBar.ButtonX buttonX1;
        private System.Windows.Forms.SaveFileDialog saveFileDialog1;

    }
}