namespace CRM
{
    partial class frmHH_BATD
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
            this.labelX2 = new DevComponents.DotNetBar.LabelX();
            this.txtCbtd = new DevComponents.DotNetBar.Controls.TextBoxX();
            this.labelX11 = new DevComponents.DotNetBar.LabelX();
            this.cbLHBH = new DevComponents.DotNetBar.Controls.ComboBoxEx();
            this.labelX9 = new DevComponents.DotNetBar.LabelX();
            this.dtpTo = new System.Windows.Forms.DateTimePicker();
            this.dtpFrom = new System.Windows.Forms.DateTimePicker();
            this.labelX1 = new DevComponents.DotNetBar.LabelX();
            this.buttonX1 = new DevComponents.DotNetBar.ButtonX();
            this.cbCN = new DevComponents.DotNetBar.Controls.ComboBoxEx();
            this.labelX12 = new DevComponents.DotNetBar.LabelX();
            this.SuspendLayout();
            // 
            // labelX2
            // 
            this.labelX2.Font = new System.Drawing.Font("Arial", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.labelX2.ForeColor = System.Drawing.SystemColors.ControlText;
            this.labelX2.Location = new System.Drawing.Point(1, 51);
            this.labelX2.Name = "labelX2";
            this.labelX2.Size = new System.Drawing.Size(145, 20);
            this.labelX2.TabIndex = 412;
            this.labelX2.Text = "Ngày:";
            // 
            // txtCbtd
            // 
            // 
            // 
            // 
            this.txtCbtd.Border.Class = "TextBoxBorder";
            this.txtCbtd.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtCbtd.Location = new System.Drawing.Point(158, 132);
            this.txtCbtd.Name = "txtCbtd";
            this.txtCbtd.Size = new System.Drawing.Size(134, 26);
            this.txtCbtd.TabIndex = 5;
            // 
            // labelX11
            // 
            this.labelX11.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.labelX11.Location = new System.Drawing.Point(1, 134);
            this.labelX11.Name = "labelX11";
            this.labelX11.Size = new System.Drawing.Size(145, 23);
            this.labelX11.TabIndex = 415;
            this.labelX11.Text = "Cán bộ tín dụng";
            // 
            // cbLHBH
            // 
            this.cbLHBH.DisplayMember = "Text";
            this.cbLHBH.DrawMode = System.Windows.Forms.DrawMode.OwnerDrawFixed;
            this.cbLHBH.Font = new System.Drawing.Font("Arial", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.cbLHBH.FormattingEnabled = true;
            this.cbLHBH.ItemHeight = 19;
            this.cbLHBH.Location = new System.Drawing.Point(158, 91);
            this.cbLHBH.Name = "cbLHBH";
            this.cbLHBH.Size = new System.Drawing.Size(134, 25);
            this.cbLHBH.TabIndex = 4;
            // 
            // labelX9
            // 
            this.labelX9.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.labelX9.Location = new System.Drawing.Point(1, 92);
            this.labelX9.Name = "labelX9";
            this.labelX9.Size = new System.Drawing.Size(145, 23);
            this.labelX9.TabIndex = 414;
            this.labelX9.Text = "Loại hình bảo hiểm";
            // 
            // dtpTo
            // 
            this.dtpTo.CustomFormat = "dd/MM/yyyy";
            this.dtpTo.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.dtpTo.Format = System.Windows.Forms.DateTimePickerFormat.Custom;
            this.dtpTo.Location = new System.Drawing.Point(278, 50);
            this.dtpTo.Name = "dtpTo";
            this.dtpTo.ShowUpDown = true;
            this.dtpTo.Size = new System.Drawing.Size(90, 23);
            this.dtpTo.TabIndex = 3;
            // 
            // dtpFrom
            // 
            this.dtpFrom.CustomFormat = "dd/MM/yyyy";
            this.dtpFrom.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.dtpFrom.Format = System.Windows.Forms.DateTimePickerFormat.Custom;
            this.dtpFrom.Location = new System.Drawing.Point(158, 50);
            this.dtpFrom.Name = "dtpFrom";
            this.dtpFrom.ShowUpDown = true;
            this.dtpFrom.Size = new System.Drawing.Size(90, 23);
            this.dtpFrom.TabIndex = 2;
            // 
            // labelX1
            // 
            this.labelX1.Font = new System.Drawing.Font("Arial", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.labelX1.ForeColor = System.Drawing.SystemColors.ControlText;
            this.labelX1.Location = new System.Drawing.Point(254, 53);
            this.labelX1.Name = "labelX1";
            this.labelX1.Size = new System.Drawing.Size(18, 20);
            this.labelX1.TabIndex = 413;
            this.labelX1.Text = "->";
            // 
            // buttonX1
            // 
            this.buttonX1.AccessibleRole = System.Windows.Forms.AccessibleRole.PushButton;
            this.buttonX1.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.buttonX1.Location = new System.Drawing.Point(237, 178);
            this.buttonX1.Name = "buttonX1";
            this.buttonX1.Size = new System.Drawing.Size(87, 25);
            this.buttonX1.TabIndex = 411;
            this.buttonX1.Text = "Thống kê";
            this.buttonX1.Click += new System.EventHandler(this.buttonX1_Click);
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
            this.cbCN.Location = new System.Drawing.Point(158, 12);
            this.cbCN.Name = "cbCN";
            this.cbCN.Size = new System.Drawing.Size(364, 25);
            this.cbCN.TabIndex = 1;
            // 
            // labelX12
            // 
            this.labelX12.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.labelX12.ForeColor = System.Drawing.SystemColors.ControlText;
            this.labelX12.Location = new System.Drawing.Point(1, 13);
            this.labelX12.Name = "labelX12";
            this.labelX12.Size = new System.Drawing.Size(145, 22);
            this.labelX12.TabIndex = 410;
            this.labelX12.Text = "Chi nhánh :";
            // 
            // frmHH_BATD
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(534, 215);
            this.Controls.Add(this.labelX2);
            this.Controls.Add(this.txtCbtd);
            this.Controls.Add(this.labelX11);
            this.Controls.Add(this.cbLHBH);
            this.Controls.Add(this.labelX9);
            this.Controls.Add(this.dtpTo);
            this.Controls.Add(this.dtpFrom);
            this.Controls.Add(this.labelX1);
            this.Controls.Add(this.buttonX1);
            this.Controls.Add(this.cbCN);
            this.Controls.Add(this.labelX12);
            this.Name = "frmHH_BATD";
            this.ShowIcon = false;
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Bảo an tín dụng";
            this.Load += new System.EventHandler(this.frmHH_BATD_Load);
            this.ResumeLayout(false);

        }

        #endregion

        private DevComponents.DotNetBar.LabelX labelX2;
        private DevComponents.DotNetBar.Controls.TextBoxX txtCbtd;
        private DevComponents.DotNetBar.LabelX labelX11;
        private DevComponents.DotNetBar.Controls.ComboBoxEx cbLHBH;
        private DevComponents.DotNetBar.LabelX labelX9;
        private System.Windows.Forms.DateTimePicker dtpTo;
        private System.Windows.Forms.DateTimePicker dtpFrom;
        private DevComponents.DotNetBar.LabelX labelX1;
        private DevComponents.DotNetBar.ButtonX buttonX1;
        private DevComponents.DotNetBar.Controls.ComboBoxEx cbCN;
        private DevComponents.DotNetBar.LabelX labelX12;
    }
}