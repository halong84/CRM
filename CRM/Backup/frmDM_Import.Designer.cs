namespace CRM
{
    partial class frmDM_Import
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
            this.btnImport = new DevComponents.DotNetBar.ButtonX();
            this.cbbTen = new DevComponents.DotNetBar.Controls.ComboBoxEx();
            this.labelX2 = new DevComponents.DotNetBar.LabelX();
            this.openFileDialog1 = new System.Windows.Forms.OpenFileDialog();
            this.SuspendLayout();
            // 
            // btnImport
            // 
            this.btnImport.AccessibleRole = System.Windows.Forms.AccessibleRole.PushButton;
            this.btnImport.Location = new System.Drawing.Point(95, 79);
            this.btnImport.Name = "btnImport";
            this.btnImport.Size = new System.Drawing.Size(75, 23);
            this.btnImport.TabIndex = 206;
            this.btnImport.Text = "Import";
            this.btnImport.Click += new System.EventHandler(this.btnImport_Click);
            // 
            // cbbTen
            // 
            this.cbbTen.DisplayMember = "Text";
            this.cbbTen.DrawMode = System.Windows.Forms.DrawMode.OwnerDrawFixed;
            this.cbbTen.FormattingEnabled = true;
            this.cbbTen.ItemHeight = 19;
            this.cbbTen.Location = new System.Drawing.Point(61, 27);
            this.cbbTen.Name = "cbbTen";
            this.cbbTen.Size = new System.Drawing.Size(178, 25);
            this.cbbTen.TabIndex = 205;
            // 
            // labelX2
            // 
            this.labelX2.Location = new System.Drawing.Point(20, 26);
            this.labelX2.Margin = new System.Windows.Forms.Padding(4);
            this.labelX2.Name = "labelX2";
            this.labelX2.Size = new System.Drawing.Size(34, 26);
            this.labelX2.TabIndex = 204;
            this.labelX2.Text = "Loại";
            this.labelX2.TextAlignment = System.Drawing.StringAlignment.Far;
            // 
            // openFileDialog1
            // 
            this.openFileDialog1.FileName = "openFileDialog1";
            // 
            // frmDM_Import
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(9F, 18F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(265, 114);
            this.Controls.Add(this.btnImport);
            this.Controls.Add(this.cbbTen);
            this.Controls.Add(this.labelX2);
            this.Font = new System.Drawing.Font("Arial", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.Margin = new System.Windows.Forms.Padding(4);
            this.Name = "frmDM_Import";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Danh muc - Import";
            this.Load += new System.EventHandler(this.frmDM_Import_Load);
            this.ResumeLayout(false);

        }

        #endregion

        private DevComponents.DotNetBar.ButtonX btnImport;
        private DevComponents.DotNetBar.Controls.ComboBoxEx cbbTen;
        private DevComponents.DotNetBar.LabelX labelX2;
        private System.Windows.Forms.OpenFileDialog openFileDialog1;


    }
}