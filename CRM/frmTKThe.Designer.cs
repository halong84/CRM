namespace CRM
{
    partial class frmTKThe
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
            this.cbCN = new DevComponents.DotNetBar.Controls.ComboBoxEx();
            this.labelX1 = new DevComponents.DotNetBar.LabelX();
            this.btnIn = new DevComponents.DotNetBar.ButtonX();
            this.buttonX1 = new DevComponents.DotNetBar.ButtonX();
            this.SuspendLayout();
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
            this.cbCN.Location = new System.Drawing.Point(105, 18);
            this.cbCN.Name = "cbCN";
            this.cbCN.Size = new System.Drawing.Size(385, 25);
            this.cbCN.TabIndex = 368;
            // 
            // labelX1
            // 
            this.labelX1.Font = new System.Drawing.Font("Arial", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.labelX1.ForeColor = System.Drawing.SystemColors.ControlText;
            this.labelX1.Location = new System.Drawing.Point(12, 20);
            this.labelX1.Name = "labelX1";
            this.labelX1.Size = new System.Drawing.Size(87, 20);
            this.labelX1.TabIndex = 367;
            this.labelX1.Text = "Chi nhánh:";
            // 
            // btnIn
            // 
            this.btnIn.AccessibleRole = System.Windows.Forms.AccessibleRole.PushButton;
            this.btnIn.Location = new System.Drawing.Point(156, 59);
            this.btnIn.Name = "btnIn";
            this.btnIn.Size = new System.Drawing.Size(97, 31);
            this.btnIn.TabIndex = 366;
            this.btnIn.Text = "In";
            this.btnIn.Click += new System.EventHandler(this.btnIn_Click);
            // 
            // buttonX1
            // 
            this.buttonX1.AccessibleRole = System.Windows.Forms.AccessibleRole.PushButton;
            this.buttonX1.Location = new System.Drawing.Point(259, 59);
            this.buttonX1.Name = "buttonX1";
            this.buttonX1.Size = new System.Drawing.Size(97, 31);
            this.buttonX1.TabIndex = 369;
            this.buttonX1.Text = "Đóng";
            this.buttonX1.Click += new System.EventHandler(this.buttonX1_Click);
            // 
            // frmTKThe
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(502, 104);
            this.Controls.Add(this.buttonX1);
            this.Controls.Add(this.cbCN);
            this.Controls.Add(this.labelX1);
            this.Controls.Add(this.btnIn);
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "frmTKThe";
            this.ShowIcon = false;
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Thống kê khách hàng có TKTT chưa sử dụng thẻ";
            this.Load += new System.EventHandler(this.frmTKThe_Load);
            this.ResumeLayout(false);

        }

        #endregion

        private DevComponents.DotNetBar.Controls.ComboBoxEx cbCN;
        private DevComponents.DotNetBar.LabelX labelX1;
        private DevComponents.DotNetBar.ButtonX btnIn;
        private DevComponents.DotNetBar.ButtonX buttonX1;
    }
}