namespace CRM
{
    partial class frmHH_TKVIPCT
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
            this.labelX1 = new DevComponents.DotNetBar.LabelX();
            this.label1 = new System.Windows.Forms.Label();
            this.dtpTo = new System.Windows.Forms.DateTimePicker();
            this.labelX2 = new DevComponents.DotNetBar.LabelX();
            this.btnIn = new DevComponents.DotNetBar.ButtonX();
            this.dtpFrom = new System.Windows.Forms.DateTimePicker();
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
            this.SuspendLayout();
            // 
            // labelX1
            // 
            this.labelX1.Font = new System.Drawing.Font("Arial", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.labelX1.ForeColor = System.Drawing.SystemColors.ControlText;
            this.labelX1.Location = new System.Drawing.Point(29, 27);
            this.labelX1.Name = "labelX1";
            this.labelX1.Size = new System.Drawing.Size(87, 20);
            this.labelX1.TabIndex = 383;
            this.labelX1.Text = "Chi nhánh";
            this.labelX1.TextAlignment = System.Drawing.StringAlignment.Far;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(227, 72);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(10, 13);
            this.label1.TabIndex = 379;
            this.label1.Text = "-";
            // 
            // dtpTo
            // 
            this.dtpTo.CustomFormat = "MM/yyyy";
            this.dtpTo.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.dtpTo.Format = System.Windows.Forms.DateTimePickerFormat.Custom;
            this.dtpTo.Location = new System.Drawing.Point(243, 64);
            this.dtpTo.Name = "dtpTo";
            this.dtpTo.ShowUpDown = true;
            this.dtpTo.Size = new System.Drawing.Size(90, 26);
            this.dtpTo.TabIndex = 378;
            // 
            // labelX2
            // 
            this.labelX2.Font = new System.Drawing.Font("Arial", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.labelX2.ForeColor = System.Drawing.SystemColors.ControlText;
            this.labelX2.Location = new System.Drawing.Point(63, 70);
            this.labelX2.Name = "labelX2";
            this.labelX2.Size = new System.Drawing.Size(53, 20);
            this.labelX2.TabIndex = 381;
            this.labelX2.Text = "Tháng";
            this.labelX2.TextAlignment = System.Drawing.StringAlignment.Far;
            // 
            // btnIn
            // 
            this.btnIn.AccessibleRole = System.Windows.Forms.AccessibleRole.PushButton;
            this.btnIn.Location = new System.Drawing.Point(176, 108);
            this.btnIn.Name = "btnIn";
            this.btnIn.Size = new System.Drawing.Size(97, 31);
            this.btnIn.TabIndex = 380;
            this.btnIn.Text = "In";
            this.btnIn.Click += new System.EventHandler(this.btnIn_Click);
            // 
            // dtpFrom
            // 
            this.dtpFrom.CustomFormat = "MM/yyyy";
            this.dtpFrom.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.dtpFrom.Format = System.Windows.Forms.DateTimePickerFormat.Custom;
            this.dtpFrom.Location = new System.Drawing.Point(131, 64);
            this.dtpFrom.Name = "dtpFrom";
            this.dtpFrom.ShowUpDown = true;
            this.dtpFrom.Size = new System.Drawing.Size(90, 26);
            this.dtpFrom.TabIndex = 377;
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
            this.cbCN.Location = new System.Drawing.Point(131, 22);
            this.cbCN.Name = "cbCN";
            this.cbCN.Size = new System.Drawing.Size(202, 25);
            this.cbCN.TabIndex = 384;
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
            // frmHH_TKVIPCT
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(401, 157);
            this.Controls.Add(this.cbCN);
            this.Controls.Add(this.labelX1);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.dtpTo);
            this.Controls.Add(this.labelX2);
            this.Controls.Add(this.btnIn);
            this.Controls.Add(this.dtpFrom);
            this.Name = "frmHH_TKVIPCT";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "frmHH_TKVIPCT";
            this.Load += new System.EventHandler(this.frmHH_TKVIPCT_Load);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private DevComponents.DotNetBar.LabelX labelX1;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.DateTimePicker dtpTo;
        private DevComponents.DotNetBar.LabelX labelX2;
        private DevComponents.DotNetBar.ButtonX btnIn;
        private System.Windows.Forms.DateTimePicker dtpFrom;
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
    }
}