namespace CRM
{
    partial class frmKHChuyen
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
            this.dtpNgaychuyen = new System.Windows.Forms.DateTimePicker();
            this.btnChuyen = new DevComponents.DotNetBar.ButtonX();
            this.txtMaKH = new System.Windows.Forms.TextBox();
            this.cmnd = new DevComponents.DotNetBar.LabelX();
            this.labelX1 = new DevComponents.DotNetBar.LabelX();
            this.SuspendLayout();
            // 
            // dtpNgaychuyen
            // 
            this.dtpNgaychuyen.CustomFormat = "dd/MM/yyyy";
            this.dtpNgaychuyen.Format = System.Windows.Forms.DateTimePickerFormat.Custom;
            this.dtpNgaychuyen.Location = new System.Drawing.Point(107, 29);
            this.dtpNgaychuyen.Name = "dtpNgaychuyen";
            this.dtpNgaychuyen.ShowUpDown = true;
            this.dtpNgaychuyen.Size = new System.Drawing.Size(95, 20);
            this.dtpNgaychuyen.TabIndex = 2;
            this.dtpNgaychuyen.Value = new System.DateTime(2012, 4, 25, 0, 0, 0, 0);
            // 
            // btnChuyen
            // 
            this.btnChuyen.AccessibleRole = System.Windows.Forms.AccessibleRole.PushButton;
            this.btnChuyen.Location = new System.Drawing.Point(260, 3);
            this.btnChuyen.Name = "btnChuyen";
            this.btnChuyen.Size = new System.Drawing.Size(87, 26);
            this.btnChuyen.TabIndex = 194;
            this.btnChuyen.Text = "Chuyển";
            this.btnChuyen.Click += new System.EventHandler(this.btnChuyen_Click);
            // 
            // txtMaKH
            // 
            this.txtMaKH.Font = new System.Drawing.Font("Arial", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtMaKH.Location = new System.Drawing.Point(107, 3);
            this.txtMaKH.Name = "txtMaKH";
            this.txtMaKH.Size = new System.Drawing.Size(147, 23);
            this.txtMaKH.TabIndex = 1;
            // 
            // cmnd
            // 
            this.cmnd.Font = new System.Drawing.Font("Arial", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.cmnd.ForeColor = System.Drawing.SystemColors.ControlText;
            this.cmnd.Location = new System.Drawing.Point(17, 3);
            this.cmnd.Name = "cmnd";
            this.cmnd.Size = new System.Drawing.Size(70, 20);
            this.cmnd.TabIndex = 239;
            this.cmnd.Text = "Mã KH ";
            // 
            // labelX1
            // 
            this.labelX1.Font = new System.Drawing.Font("Arial", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.labelX1.ForeColor = System.Drawing.SystemColors.ControlText;
            this.labelX1.Location = new System.Drawing.Point(17, 29);
            this.labelX1.Name = "labelX1";
            this.labelX1.Size = new System.Drawing.Size(88, 20);
            this.labelX1.TabIndex = 241;
            this.labelX1.Text = "Ngày chuyển";
            // 
            // frmKHChuyen
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(439, 67);
            this.Controls.Add(this.labelX1);
            this.Controls.Add(this.txtMaKH);
            this.Controls.Add(this.cmnd);
            this.Controls.Add(this.dtpNgaychuyen);
            this.Controls.Add(this.btnChuyen);
            this.Name = "frmKHChuyen";
            this.ShowIcon = false;
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Chuyển đổi khách hàng";
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.DateTimePicker dtpNgaychuyen;
        private DevComponents.DotNetBar.ButtonX btnChuyen;
        private System.Windows.Forms.TextBox txtMaKH;
        private DevComponents.DotNetBar.LabelX cmnd;
        private DevComponents.DotNetBar.LabelX labelX1;
    }
}