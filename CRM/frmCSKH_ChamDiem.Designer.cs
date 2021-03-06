namespace CRM
{
    partial class frmCSKH_ChamDiem
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
            this.dtpThang = new System.Windows.Forms.DateTimePicker();
            this.btnChamdiem = new DevComponents.DotNetBar.ButtonX();
            this.labelX1 = new DevComponents.DotNetBar.LabelX();
            this.cbCCY = new DevComponents.DotNetBar.Controls.ComboBoxEx();
            this.comboItem1 = new DevComponents.Editors.ComboItem();
            this.comboItem2 = new DevComponents.Editors.ComboItem();
            this.labelX2 = new DevComponents.DotNetBar.LabelX();
            this.SuspendLayout();
            // 
            // dtpThang
            // 
            this.dtpThang.CustomFormat = "MM/yyyy";
            this.dtpThang.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.dtpThang.Format = System.Windows.Forms.DateTimePickerFormat.Custom;
            this.dtpThang.Location = new System.Drawing.Point(136, 11);
            this.dtpThang.Name = "dtpThang";
            this.dtpThang.ShowUpDown = true;
            this.dtpThang.Size = new System.Drawing.Size(95, 26);
            this.dtpThang.TabIndex = 195;
            this.dtpThang.Value = new System.DateTime(2013, 1, 1, 0, 0, 0, 0);
            // 
            // btnChamdiem
            // 
            this.btnChamdiem.AccessibleRole = System.Windows.Forms.AccessibleRole.PushButton;
            this.btnChamdiem.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnChamdiem.Location = new System.Drawing.Point(71, 85);
            this.btnChamdiem.Name = "btnChamdiem";
            this.btnChamdiem.Size = new System.Drawing.Size(199, 26);
            this.btnChamdiem.TabIndex = 194;
            this.btnChamdiem.Text = "Tích lũy điểm tiền gửi";
            this.btnChamdiem.Click += new System.EventHandler(this.btnChamdiem_Click);
            // 
            // labelX1
            // 
            this.labelX1.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.labelX1.Location = new System.Drawing.Point(81, 12);
            this.labelX1.Name = "labelX1";
            this.labelX1.Size = new System.Drawing.Size(49, 23);
            this.labelX1.TabIndex = 193;
            this.labelX1.Text = "Tháng";
            // 
            // cbCCY
            // 
            this.cbCCY.DisplayMember = "Text";
            this.cbCCY.DrawMode = System.Windows.Forms.DrawMode.OwnerDrawFixed;
            this.cbCCY.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.cbCCY.FormattingEnabled = true;
            this.cbCCY.ItemHeight = 19;
            this.cbCCY.Items.AddRange(new object[] {
            this.comboItem1,
            this.comboItem2});
            this.cbCCY.Location = new System.Drawing.Point(136, 43);
            this.cbCCY.Name = "cbCCY";
            this.cbCCY.Size = new System.Drawing.Size(95, 25);
            this.cbCCY.TabIndex = 196;
            this.cbCCY.Text = "VND";
            // 
            // comboItem1
            // 
            this.comboItem1.Text = "VND";
            // 
            // comboItem2
            // 
            this.comboItem2.Text = "USD";
            // 
            // labelX2
            // 
            this.labelX2.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.labelX2.Location = new System.Drawing.Point(33, 45);
            this.labelX2.Name = "labelX2";
            this.labelX2.Size = new System.Drawing.Size(97, 23);
            this.labelX2.TabIndex = 197;
            this.labelX2.Text = "Loại ngoại tệ:";
            // 
            // frmCSKH_ChamDiem
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(312, 127);
            this.Controls.Add(this.labelX2);
            this.Controls.Add(this.cbCCY);
            this.Controls.Add(this.dtpThang);
            this.Controls.Add(this.btnChamdiem);
            this.Controls.Add(this.labelX1);
            this.Name = "frmCSKH_ChamDiem";
            this.ShowIcon = false;
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Tích lũy điểm tiền gửi";
            this.Load += new System.EventHandler(this.frmCSKH_ChamDiem_Load);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.DateTimePicker dtpThang;
        private DevComponents.DotNetBar.ButtonX btnChamdiem;
        private DevComponents.DotNetBar.LabelX labelX1;
        private DevComponents.DotNetBar.Controls.ComboBoxEx cbCCY;
        private DevComponents.Editors.ComboItem comboItem1;
        private DevComponents.Editors.ComboItem comboItem2;
        private DevComponents.DotNetBar.LabelX labelX2;
    }
}