namespace CRM.GUI_DV
{
    partial class frmGiayUyQuyen
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
            this.label1 = new System.Windows.Forms.Label();
            this.cbLanhDao = new System.Windows.Forms.ComboBox();
            this.label2 = new System.Windows.Forms.Label();
            this.txtGiayUyQuyen = new System.Windows.Forms.TextBox();
            this.btnLuu = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(12, 9);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(56, 13);
            this.label1.TabIndex = 0;
            this.label1.Text = "Lãnh đạo:";
            // 
            // cbLanhDao
            // 
            this.cbLanhDao.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cbLanhDao.FormattingEnabled = true;
            this.cbLanhDao.Location = new System.Drawing.Point(95, 6);
            this.cbLanhDao.Name = "cbLanhDao";
            this.cbLanhDao.Size = new System.Drawing.Size(193, 21);
            this.cbLanhDao.TabIndex = 1;
            this.cbLanhDao.SelectedIndexChanged += new System.EventHandler(this.cbLanhDao_SelectedIndexChanged);
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(12, 40);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(77, 13);
            this.label2.TabIndex = 2;
            this.label2.Text = "Giấy ủy quyền:";
            // 
            // txtGiayUyQuyen
            // 
            this.txtGiayUyQuyen.Location = new System.Drawing.Point(95, 33);
            this.txtGiayUyQuyen.Multiline = true;
            this.txtGiayUyQuyen.Name = "txtGiayUyQuyen";
            this.txtGiayUyQuyen.Size = new System.Drawing.Size(392, 66);
            this.txtGiayUyQuyen.TabIndex = 3;
            // 
            // btnLuu
            // 
            this.btnLuu.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnLuu.Location = new System.Drawing.Point(213, 105);
            this.btnLuu.Name = "btnLuu";
            this.btnLuu.Size = new System.Drawing.Size(75, 23);
            this.btnLuu.TabIndex = 4;
            this.btnLuu.Text = "Lưu";
            this.btnLuu.UseVisualStyleBackColor = true;
            this.btnLuu.Click += new System.EventHandler(this.btnLuu_Click);
            // 
            // frmGiayUyQuyen
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(499, 136);
            this.Controls.Add(this.btnLuu);
            this.Controls.Add(this.txtGiayUyQuyen);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.cbLanhDao);
            this.Controls.Add(this.label1);
            this.Name = "frmGiayUyQuyen";
            this.Text = "Nhap Giay Uy Quyen";
            this.Load += new System.EventHandler(this.frmGiayUyQuyen_Load);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.ComboBox cbLanhDao;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.TextBox txtGiayUyQuyen;
        private System.Windows.Forms.Button btnLuu;
    }
}