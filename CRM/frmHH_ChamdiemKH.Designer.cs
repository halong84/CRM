namespace CRM
{
    partial class frmHH_ChamdiemKH
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
            this.btnChamdiem = new DevComponents.DotNetBar.ButtonX();
            this.dtpThang = new System.Windows.Forms.DateTimePicker();
            this.SuspendLayout();
            // 
            // labelX1
            // 
            this.labelX1.Location = new System.Drawing.Point(43, 34);
            this.labelX1.Name = "labelX1";
            this.labelX1.Size = new System.Drawing.Size(49, 23);
            this.labelX1.TabIndex = 0;
            this.labelX1.Text = "Tháng";
            // 
            // btnChamdiem
            // 
            this.btnChamdiem.AccessibleRole = System.Windows.Forms.AccessibleRole.PushButton;
            this.btnChamdiem.Location = new System.Drawing.Point(252, 34);
            this.btnChamdiem.Name = "btnChamdiem";
            this.btnChamdiem.Size = new System.Drawing.Size(87, 26);
            this.btnChamdiem.TabIndex = 2;
            this.btnChamdiem.Text = "Chấm điểm";
            this.btnChamdiem.Click += new System.EventHandler(this.btnChamdiem_Click);
            // 
            // dtpThang
            // 
            this.dtpThang.CustomFormat = "MM/yyyy";
            this.dtpThang.Format = System.Windows.Forms.DateTimePickerFormat.Custom;
            this.dtpThang.Location = new System.Drawing.Point(98, 33);
            this.dtpThang.Name = "dtpThang";
            this.dtpThang.ShowUpDown = true;
            this.dtpThang.Size = new System.Drawing.Size(95, 26);
            this.dtpThang.TabIndex = 192;
            this.dtpThang.Value = new System.DateTime(2018, 1, 1, 0, 0, 0, 0);
            this.dtpThang.ValueChanged += new System.EventHandler(this.dtpThang_ValueChanged);
            // 
            // frmHH_ChamdiemKH
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(9F, 18F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(381, 93);
            this.Controls.Add(this.dtpThang);
            this.Controls.Add(this.btnChamdiem);
            this.Controls.Add(this.labelX1);
            this.Font = new System.Drawing.Font("Arial", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.Margin = new System.Windows.Forms.Padding(4);
            this.Name = "frmHH_ChamdiemKH";
            this.ShowIcon = false;
            this.Text = "Chấm điểm khách hàng";
            this.Load += new System.EventHandler(this.frmHH_ChamdiemKH_Load);
            this.ResumeLayout(false);

        }

        #endregion

        private DevComponents.DotNetBar.LabelX labelX1;
        private DevComponents.DotNetBar.ButtonX btnChamdiem;
        private System.Windows.Forms.DateTimePicker dtpThang;

    }
}