namespace CRM
{
    partial class frmCSKH_TKQuaTang
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
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle3 = new System.Windows.Forms.DataGridViewCellStyle();
            this.labelX7 = new DevComponents.DotNetBar.LabelX();
            this.dtpTo = new System.Windows.Forms.DateTimePicker();
            this.dtpFrom = new System.Windows.Forms.DateTimePicker();
            this.labelX11 = new DevComponents.DotNetBar.LabelX();
            this.buttonX1 = new DevComponents.DotNetBar.ButtonX();
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
            this.dataGridViewX1 = new DevComponents.DotNetBar.Controls.DataGridViewX();
            this.btnIn = new DevComponents.DotNetBar.ButtonX();
            this.buttonX159 = new DevComponents.DotNetBar.ButtonX();
            this.saveFileDialog1 = new System.Windows.Forms.SaveFileDialog();
            this.txtMakh = new System.Windows.Forms.TextBox();
            this.labelX1 = new DevComponents.DotNetBar.LabelX();
            this.buttonX3 = new DevComponents.DotNetBar.ButtonX();
            ((System.ComponentModel.ISupportInitialize)(this.dataGridViewX1)).BeginInit();
            this.SuspendLayout();
            // 
            // labelX7
            // 
            this.labelX7.Font = new System.Drawing.Font("Arial", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.labelX7.Location = new System.Drawing.Point(576, 9);
            this.labelX7.Name = "labelX7";
            this.labelX7.Size = new System.Drawing.Size(10, 17);
            this.labelX7.TabIndex = 230;
            this.labelX7.Text = "-";
            this.labelX7.TextAlignment = System.Drawing.StringAlignment.Center;
            // 
            // dtpTo
            // 
            this.dtpTo.CustomFormat = "dd/MM/yyyy";
            this.dtpTo.Format = System.Windows.Forms.DateTimePickerFormat.Custom;
            this.dtpTo.Location = new System.Drawing.Point(587, 6);
            this.dtpTo.Name = "dtpTo";
            this.dtpTo.ShowUpDown = true;
            this.dtpTo.Size = new System.Drawing.Size(88, 20);
            this.dtpTo.TabIndex = 229;
            // 
            // dtpFrom
            // 
            this.dtpFrom.CustomFormat = "dd/MM/yyyy";
            this.dtpFrom.Format = System.Windows.Forms.DateTimePickerFormat.Custom;
            this.dtpFrom.Location = new System.Drawing.Point(479, 6);
            this.dtpFrom.Name = "dtpFrom";
            this.dtpFrom.ShowUpDown = true;
            this.dtpFrom.Size = new System.Drawing.Size(91, 20);
            this.dtpFrom.TabIndex = 228;
            // 
            // labelX11
            // 
            this.labelX11.Font = new System.Drawing.Font("Arial", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.labelX11.ForeColor = System.Drawing.SystemColors.ControlText;
            this.labelX11.Location = new System.Drawing.Point(426, 4);
            this.labelX11.Name = "labelX11";
            this.labelX11.Size = new System.Drawing.Size(49, 22);
            this.labelX11.TabIndex = 227;
            this.labelX11.Text = "Ngày :";
            this.labelX11.Click += new System.EventHandler(this.labelX11_Click);
            // 
            // buttonX1
            // 
            this.buttonX1.AccessibleRole = System.Windows.Forms.AccessibleRole.PushButton;
            this.buttonX1.Font = new System.Drawing.Font("Arial", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.buttonX1.Location = new System.Drawing.Point(681, 3);
            this.buttonX1.Name = "buttonX1";
            this.buttonX1.Size = new System.Drawing.Size(87, 25);
            this.buttonX1.TabIndex = 226;
            this.buttonX1.Text = "Tìm kiếm";
            this.buttonX1.Click += new System.EventHandler(this.buttonX1_Click);
            // 
            // labelX12
            // 
            this.labelX12.Font = new System.Drawing.Font("Arial", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.labelX12.ForeColor = System.Drawing.SystemColors.ControlText;
            this.labelX12.Location = new System.Drawing.Point(3, 3);
            this.labelX12.Name = "labelX12";
            this.labelX12.Size = new System.Drawing.Size(96, 22);
            this.labelX12.TabIndex = 225;
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
            this.cbCN.Location = new System.Drawing.Point(101, 4);
            this.cbCN.Name = "cbCN";
            this.cbCN.Size = new System.Drawing.Size(99, 21);
            this.cbCN.TabIndex = 224;
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
            // dataGridViewX1
            // 
            this.dataGridViewX1.AllowUserToAddRows = false;
            this.dataGridViewX1.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom)
                        | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.dataGridViewX1.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.Fill;
            this.dataGridViewX1.BackgroundColor = System.Drawing.SystemColors.Control;
            this.dataGridViewX1.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            dataGridViewCellStyle1.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleCenter;
            dataGridViewCellStyle1.BackColor = System.Drawing.SystemColors.Control;
            dataGridViewCellStyle1.Font = new System.Drawing.Font("Arial", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            dataGridViewCellStyle1.ForeColor = System.Drawing.SystemColors.WindowText;
            dataGridViewCellStyle1.SelectionBackColor = System.Drawing.SystemColors.Highlight;
            dataGridViewCellStyle1.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
            dataGridViewCellStyle1.WrapMode = System.Windows.Forms.DataGridViewTriState.True;
            this.dataGridViewX1.ColumnHeadersDefaultCellStyle = dataGridViewCellStyle1;
            this.dataGridViewX1.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            dataGridViewCellStyle2.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle2.BackColor = System.Drawing.SystemColors.Window;
            dataGridViewCellStyle2.Font = new System.Drawing.Font("Arial", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            dataGridViewCellStyle2.ForeColor = System.Drawing.SystemColors.ControlText;
            dataGridViewCellStyle2.SelectionBackColor = System.Drawing.SystemColors.Highlight;
            dataGridViewCellStyle2.SelectionForeColor = System.Drawing.SystemColors.ControlText;
            dataGridViewCellStyle2.WrapMode = System.Windows.Forms.DataGridViewTriState.False;
            this.dataGridViewX1.DefaultCellStyle = dataGridViewCellStyle2;
            this.dataGridViewX1.GridColor = System.Drawing.Color.FromArgb(((int)(((byte)(208)))), ((int)(((byte)(215)))), ((int)(((byte)(229)))));
            this.dataGridViewX1.Location = new System.Drawing.Point(3, 31);
            this.dataGridViewX1.Name = "dataGridViewX1";
            dataGridViewCellStyle3.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle3.BackColor = System.Drawing.SystemColors.Control;
            dataGridViewCellStyle3.Font = new System.Drawing.Font("Arial", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            dataGridViewCellStyle3.ForeColor = System.Drawing.SystemColors.WindowText;
            dataGridViewCellStyle3.SelectionBackColor = System.Drawing.SystemColors.Highlight;
            dataGridViewCellStyle3.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
            dataGridViewCellStyle3.WrapMode = System.Windows.Forms.DataGridViewTriState.True;
            this.dataGridViewX1.RowHeadersDefaultCellStyle = dataGridViewCellStyle3;
            this.dataGridViewX1.RowHeadersVisible = false;
            this.dataGridViewX1.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            this.dataGridViewX1.Size = new System.Drawing.Size(769, 417);
            this.dataGridViewX1.TabIndex = 231;
            this.dataGridViewX1.CellContentClick += new System.Windows.Forms.DataGridViewCellEventHandler(this.dataGridViewX1_CellContentClick);
            // 
            // btnIn
            // 
            this.btnIn.AccessibleRole = System.Windows.Forms.AccessibleRole.PushButton;
            this.btnIn.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.btnIn.Font = new System.Drawing.Font("Arial", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnIn.Location = new System.Drawing.Point(581, 462);
            this.btnIn.Name = "btnIn";
            this.btnIn.Size = new System.Drawing.Size(91, 28);
            this.btnIn.TabIndex = 254;
            this.btnIn.Text = "In";
            this.btnIn.Click += new System.EventHandler(this.buttonX2_Click);
            // 
            // buttonX159
            // 
            this.buttonX159.AccessibleRole = System.Windows.Forms.AccessibleRole.PushButton;
            this.buttonX159.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.buttonX159.Font = new System.Drawing.Font("Arial", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.buttonX159.Location = new System.Drawing.Point(678, 462);
            this.buttonX159.Name = "buttonX159";
            this.buttonX159.Size = new System.Drawing.Size(91, 28);
            this.buttonX159.TabIndex = 253;
            this.buttonX159.Text = "Xuất ra Excel";
            this.buttonX159.Click += new System.EventHandler(this.buttonX159_Click);
            // 
            // txtMakh
            // 
            this.txtMakh.Font = new System.Drawing.Font("Arial", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtMakh.Location = new System.Drawing.Point(305, 5);
            this.txtMakh.Name = "txtMakh";
            this.txtMakh.Size = new System.Drawing.Size(115, 22);
            this.txtMakh.TabIndex = 255;
            this.txtMakh.TextChanged += new System.EventHandler(this.txtMakh_TextChanged);
            // 
            // labelX1
            // 
            this.labelX1.Font = new System.Drawing.Font("Arial", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.labelX1.ForeColor = System.Drawing.SystemColors.ControlText;
            this.labelX1.Location = new System.Drawing.Point(235, 6);
            this.labelX1.Name = "labelX1";
            this.labelX1.Size = new System.Drawing.Size(64, 22);
            this.labelX1.TabIndex = 256;
            this.labelX1.Text = "Mã KH :";
            // 
            // buttonX3
            // 
            this.buttonX3.AccessibleRole = System.Windows.Forms.AccessibleRole.PushButton;
            this.buttonX3.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.buttonX3.Font = new System.Drawing.Font("Arial", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.buttonX3.Location = new System.Drawing.Point(479, 462);
            this.buttonX3.Name = "buttonX3";
            this.buttonX3.Size = new System.Drawing.Size(91, 28);
            this.buttonX3.TabIndex = 257;
            this.buttonX3.Text = "Reset";
            this.buttonX3.Click += new System.EventHandler(this.buttonX3_Click);
            // 
            // frmCSKH_TKQuaTang
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(775, 502);
            this.Controls.Add(this.buttonX3);
            this.Controls.Add(this.labelX1);
            this.Controls.Add(this.txtMakh);
            this.Controls.Add(this.btnIn);
            this.Controls.Add(this.buttonX159);
            this.Controls.Add(this.dataGridViewX1);
            this.Controls.Add(this.labelX7);
            this.Controls.Add(this.dtpTo);
            this.Controls.Add(this.dtpFrom);
            this.Controls.Add(this.labelX11);
            this.Controls.Add(this.buttonX1);
            this.Controls.Add(this.labelX12);
            this.Controls.Add(this.cbCN);
            this.Name = "frmCSKH_TKQuaTang";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "frmCSKH_TKQuaTang";
            this.Load += new System.EventHandler(this.frmCSKH_TKQuaTang_Load);
            ((System.ComponentModel.ISupportInitialize)(this.dataGridViewX1)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private DevComponents.DotNetBar.LabelX labelX7;
        private System.Windows.Forms.DateTimePicker dtpTo;
        private System.Windows.Forms.DateTimePicker dtpFrom;
        private DevComponents.DotNetBar.LabelX labelX11;
        private DevComponents.DotNetBar.ButtonX buttonX1;
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
        private DevComponents.DotNetBar.Controls.DataGridViewX dataGridViewX1;
        private DevComponents.DotNetBar.ButtonX btnIn;
        private DevComponents.DotNetBar.ButtonX buttonX159;
        private System.Windows.Forms.SaveFileDialog saveFileDialog1;
        private System.Windows.Forms.TextBox txtMakh;
        private DevComponents.DotNetBar.LabelX labelX1;
        private DevComponents.DotNetBar.ButtonX buttonX3;

    }
}