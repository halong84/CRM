namespace Quan_he_khach_hang
{
    partial class frmThamso
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
            this.labelX2 = new DevComponents.DotNetBar.LabelX();
            this.txtTenCN = new DevComponents.DotNetBar.Controls.TextBoxX();
            this.txtMaCN = new DevComponents.DotNetBar.Controls.TextBoxX();
            this.btnSave = new DevComponents.DotNetBar.ButtonX();
            this.SuspendLayout();
            // 
            // labelX1
            // 
            this.labelX1.Location = new System.Drawing.Point(39, 23);
            this.labelX1.Margin = new System.Windows.Forms.Padding(4);
            this.labelX1.Name = "labelX1";
            this.labelX1.Size = new System.Drawing.Size(50, 24);
            this.labelX1.TabIndex = 0;
            this.labelX1.Text = "Mã CN";
            // 
            // labelX2
            // 
            this.labelX2.Location = new System.Drawing.Point(32, 56);
            this.labelX2.Name = "labelX2";
            this.labelX2.Size = new System.Drawing.Size(57, 23);
            this.labelX2.TabIndex = 1;
            this.labelX2.Text = "Tên CN";
            this.labelX2.TextAlignment = System.Drawing.StringAlignment.Far;
            // 
            // txtTenCN
            // 
            // 
            // 
            // 
            this.txtTenCN.Border.Class = "TextBoxBorder";
            this.txtTenCN.Location = new System.Drawing.Point(102, 53);
            this.txtTenCN.Name = "txtTenCN";
            this.txtTenCN.Size = new System.Drawing.Size(255, 26);
            this.txtTenCN.TabIndex = 3;
            // 
            // txtMaCN
            // 
            // 
            // 
            // 
            this.txtMaCN.Border.Class = "TextBoxBorder";
            this.txtMaCN.Location = new System.Drawing.Point(102, 21);
            this.txtMaCN.Name = "txtMaCN";
            this.txtMaCN.Size = new System.Drawing.Size(71, 26);
            this.txtMaCN.TabIndex = 4;
            // 
            // btnSave
            // 
            this.btnSave.AccessibleRole = System.Windows.Forms.AccessibleRole.PushButton;
            this.btnSave.Location = new System.Drawing.Point(282, 132);
            this.btnSave.Name = "btnSave";
            this.btnSave.Size = new System.Drawing.Size(75, 23);
            this.btnSave.TabIndex = 5;
            this.btnSave.Text = "Lưu";
            this.btnSave.Click += new System.EventHandler(this.btnSave_Click);
            // 
            // frmThamso
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(9F, 18F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(382, 174);
            this.Controls.Add(this.btnSave);
            this.Controls.Add(this.txtMaCN);
            this.Controls.Add(this.txtTenCN);
            this.Controls.Add(this.labelX2);
            this.Controls.Add(this.labelX1);
            this.Font = new System.Drawing.Font("Arial", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.Margin = new System.Windows.Forms.Padding(4);
            this.Name = "frmThamso";
            this.Text = "Tham số";
            this.Load += new System.EventHandler(this.frmThamso_Load);
            this.ResumeLayout(false);

        }

        #endregion

        private DevComponents.DotNetBar.LabelX labelX1;
        private DevComponents.DotNetBar.LabelX labelX2;
        private DevComponents.DotNetBar.Controls.TextBoxX txtTenCN;
        private DevComponents.DotNetBar.Controls.TextBoxX txtMaCN;
        private DevComponents.DotNetBar.ButtonX btnSave;
    }
}