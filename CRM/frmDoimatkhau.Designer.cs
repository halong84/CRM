namespace CRM
{
    partial class frmDoimatkhau
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
            this.labelX3 = new DevComponents.DotNetBar.LabelX();
            this.labelX4 = new DevComponents.DotNetBar.LabelX();
            this.txtPass_old = new DevComponents.DotNetBar.Controls.TextBoxX();
            this.txtConfirm = new DevComponents.DotNetBar.Controls.TextBoxX();
            this.btnSave = new DevComponents.DotNetBar.ButtonX();
            this.btnExit = new DevComponents.DotNetBar.ButtonX();
            this.txtPass_new = new DevComponents.DotNetBar.Controls.TextBoxX();
            this.SuspendLayout();
            // 
            // labelX1
            // 
            this.labelX1.Font = new System.Drawing.Font("Arial", 15F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.labelX1.Location = new System.Drawing.Point(127, 12);
            this.labelX1.Name = "labelX1";
            this.labelX1.Size = new System.Drawing.Size(149, 23);
            this.labelX1.TabIndex = 0;
            this.labelX1.Text = "ĐỔI MẬT KHẨU";
            // 
            // labelX2
            // 
            this.labelX2.Location = new System.Drawing.Point(85, 57);
            this.labelX2.Name = "labelX2";
            this.labelX2.Size = new System.Drawing.Size(89, 23);
            this.labelX2.TabIndex = 1;
            this.labelX2.Text = "Mật khẩu cũ";
            // 
            // labelX3
            // 
            this.labelX3.Location = new System.Drawing.Point(72, 89);
            this.labelX3.Name = "labelX3";
            this.labelX3.Size = new System.Drawing.Size(102, 23);
            this.labelX3.TabIndex = 2;
            this.labelX3.Text = "Mật khẩu mới";
            // 
            // labelX4
            // 
            this.labelX4.Location = new System.Drawing.Point(42, 121);
            this.labelX4.Name = "labelX4";
            this.labelX4.Size = new System.Drawing.Size(132, 23);
            this.labelX4.TabIndex = 3;
            this.labelX4.Text = "Xác nhận MK mới";
            // 
            // txtPass_old
            // 
            // 
            // 
            // 
            this.txtPass_old.Border.Class = "TextBoxBorder";
            this.txtPass_old.Font = new System.Drawing.Font("Arial", 20F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtPass_old.Location = new System.Drawing.Point(193, 54);
            this.txtPass_old.Name = "txtPass_old";
            this.txtPass_old.PasswordChar = '*';
            this.txtPass_old.Size = new System.Drawing.Size(169, 26);
            this.txtPass_old.TabIndex = 1;
            // 
            // txtConfirm
            // 
            // 
            // 
            // 
            this.txtConfirm.Border.Class = "TextBoxBorder";
            this.txtConfirm.Font = new System.Drawing.Font("Arial", 20F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtConfirm.Location = new System.Drawing.Point(193, 118);
            this.txtConfirm.Name = "txtConfirm";
            this.txtConfirm.PasswordChar = '*';
            this.txtConfirm.Size = new System.Drawing.Size(169, 26);
            this.txtConfirm.TabIndex = 3;
            // 
            // btnSave
            // 
            this.btnSave.AccessibleRole = System.Windows.Forms.AccessibleRole.PushButton;
            this.btnSave.Location = new System.Drawing.Point(206, 164);
            this.btnSave.Name = "btnSave";
            this.btnSave.Size = new System.Drawing.Size(75, 23);
            this.btnSave.TabIndex = 4;
            this.btnSave.Text = "Lưu";
            this.btnSave.Click += new System.EventHandler(this.btnSave_Click);
            // 
            // btnExit
            // 
            this.btnExit.AccessibleRole = System.Windows.Forms.AccessibleRole.PushButton;
            this.btnExit.Location = new System.Drawing.Point(287, 164);
            this.btnExit.Name = "btnExit";
            this.btnExit.Size = new System.Drawing.Size(75, 23);
            this.btnExit.TabIndex = 5;
            this.btnExit.Text = "Đóng";
            this.btnExit.Click += new System.EventHandler(this.btnExit_Click);
            // 
            // txtPass_new
            // 
            // 
            // 
            // 
            this.txtPass_new.Border.Class = "TextBoxBorder";
            this.txtPass_new.Font = new System.Drawing.Font("Arial", 20F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtPass_new.Location = new System.Drawing.Point(193, 86);
            this.txtPass_new.Name = "txtPass_new";
            this.txtPass_new.PasswordChar = '*';
            this.txtPass_new.Size = new System.Drawing.Size(169, 26);
            this.txtPass_new.TabIndex = 2;
            // 
            // frmDoimatkhau
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(9F, 18F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(402, 195);
            this.Controls.Add(this.txtPass_new);
            this.Controls.Add(this.btnExit);
            this.Controls.Add(this.btnSave);
            this.Controls.Add(this.txtConfirm);
            this.Controls.Add(this.txtPass_old);
            this.Controls.Add(this.labelX4);
            this.Controls.Add(this.labelX3);
            this.Controls.Add(this.labelX2);
            this.Controls.Add(this.labelX1);
            this.Font = new System.Drawing.Font("Arial", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.Margin = new System.Windows.Forms.Padding(4);
            this.Name = "frmDoimatkhau";
            this.ShowIcon = false;
            this.Text = "Đổi mật khẩu";
            this.Load += new System.EventHandler(this.frmDoimatkhau_Load);
            this.ResumeLayout(false);

        }

        #endregion

        private DevComponents.DotNetBar.LabelX labelX1;
        private DevComponents.DotNetBar.LabelX labelX2;
        private DevComponents.DotNetBar.LabelX labelX3;
        private DevComponents.DotNetBar.LabelX labelX4;
        private DevComponents.DotNetBar.Controls.TextBoxX txtPass_old;
        private DevComponents.DotNetBar.Controls.TextBoxX txtConfirm;
        private DevComponents.DotNetBar.ButtonX btnSave;
        private DevComponents.DotNetBar.ButtonX btnExit;
        private DevComponents.DotNetBar.Controls.TextBoxX txtPass_new;
    }
}