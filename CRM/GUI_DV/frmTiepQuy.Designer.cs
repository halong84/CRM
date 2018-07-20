namespace CRM.GUI_DV
{
    partial class frmTiepQuy
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(frmTiepQuy));
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.txtTenCanBo = new System.Windows.Forms.TextBox();
            this.cbATMID = new System.Windows.Forms.ComboBox();
            this.label2 = new System.Windows.Forms.Label();
            this.label1 = new System.Windows.Forms.Label();
            this.groupBox2 = new System.Windows.Forms.GroupBox();
            this.txtTong = new System.Windows.Forms.TextBox();
            this.label7 = new System.Windows.Forms.Label();
            this.txt500 = new System.Windows.Forms.TextBox();
            this.label6 = new System.Windows.Forms.Label();
            this.txt200 = new System.Windows.Forms.TextBox();
            this.label5 = new System.Windows.Forms.Label();
            this.txt100 = new System.Windows.Forms.TextBox();
            this.label4 = new System.Windows.Forms.Label();
            this.txt50 = new System.Windows.Forms.TextBox();
            this.label3 = new System.Windows.Forms.Label();
            this.btnLuu = new System.Windows.Forms.Button();
            this.saveFileTiepQuy = new System.Windows.Forms.SaveFileDialog();
            this.cbKSV = new System.Windows.Forms.ComboBox();
            this.label8 = new System.Windows.Forms.Label();
            this.groupBox1.SuspendLayout();
            this.groupBox2.SuspendLayout();
            this.SuspendLayout();
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.cbKSV);
            this.groupBox1.Controls.Add(this.label8);
            this.groupBox1.Controls.Add(this.txtTenCanBo);
            this.groupBox1.Controls.Add(this.cbATMID);
            this.groupBox1.Controls.Add(this.label2);
            this.groupBox1.Controls.Add(this.label1);
            this.groupBox1.Location = new System.Drawing.Point(13, 13);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(231, 118);
            this.groupBox1.TabIndex = 0;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "Thông tin chung";
            // 
            // txtTenCanBo
            // 
            this.txtTenCanBo.Enabled = false;
            this.txtTenCanBo.Location = new System.Drawing.Point(98, 19);
            this.txtTenCanBo.Name = "txtTenCanBo";
            this.txtTenCanBo.Size = new System.Drawing.Size(121, 20);
            this.txtTenCanBo.TabIndex = 0;
            this.txtTenCanBo.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            // 
            // cbATMID
            // 
            this.cbATMID.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cbATMID.FormattingEnabled = true;
            this.cbATMID.Location = new System.Drawing.Point(98, 74);
            this.cbATMID.Name = "cbATMID";
            this.cbATMID.Size = new System.Drawing.Size(121, 21);
            this.cbATMID.TabIndex = 1;
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(6, 77);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(50, 13);
            this.label2.TabIndex = 6;
            this.label2.Text = "ATM ID: ";
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(6, 22);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(86, 13);
            this.label1.TabIndex = 4;
            this.label1.Text = "Cán bộ đề nghị: ";
            // 
            // groupBox2
            // 
            this.groupBox2.Controls.Add(this.txtTong);
            this.groupBox2.Controls.Add(this.label7);
            this.groupBox2.Controls.Add(this.txt500);
            this.groupBox2.Controls.Add(this.label6);
            this.groupBox2.Controls.Add(this.txt200);
            this.groupBox2.Controls.Add(this.label5);
            this.groupBox2.Controls.Add(this.txt100);
            this.groupBox2.Controls.Add(this.label4);
            this.groupBox2.Controls.Add(this.txt50);
            this.groupBox2.Controls.Add(this.label3);
            this.groupBox2.Location = new System.Drawing.Point(13, 137);
            this.groupBox2.Name = "groupBox2";
            this.groupBox2.Size = new System.Drawing.Size(231, 160);
            this.groupBox2.TabIndex = 1;
            this.groupBox2.TabStop = false;
            this.groupBox2.Text = "Tiền tiếp quỹ (Số tờ)";
            // 
            // txtTong
            // 
            this.txtTong.Enabled = false;
            this.txtTong.Location = new System.Drawing.Point(98, 121);
            this.txtTong.Name = "txtTong";
            this.txtTong.Size = new System.Drawing.Size(121, 20);
            this.txtTong.TabIndex = 9;
            this.txtTong.Text = "0";
            this.txtTong.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            // 
            // label7
            // 
            this.label7.AutoSize = true;
            this.label7.Location = new System.Drawing.Point(7, 124);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(62, 13);
            this.label7.TabIndex = 8;
            this.label7.Text = "Tổng cộng:";
            // 
            // txt500
            // 
            this.txt500.Location = new System.Drawing.Point(98, 95);
            this.txt500.Name = "txt500";
            this.txt500.Size = new System.Drawing.Size(121, 20);
            this.txt500.TabIndex = 7;
            this.txt500.Text = "0";
            this.txt500.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            this.txt500.TextChanged += new System.EventHandler(this.txt500_TextChanged);
            this.txt500.Leave += new System.EventHandler(this.txt500_Leave);
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.Location = new System.Drawing.Point(7, 98);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(46, 13);
            this.label6.TabIndex = 6;
            this.label6.Text = "500,000";
            // 
            // txt200
            // 
            this.txt200.Location = new System.Drawing.Point(98, 69);
            this.txt200.Name = "txt200";
            this.txt200.Size = new System.Drawing.Size(121, 20);
            this.txt200.TabIndex = 5;
            this.txt200.Text = "0";
            this.txt200.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            this.txt200.TextChanged += new System.EventHandler(this.txt200_TextChanged);
            this.txt200.Leave += new System.EventHandler(this.txt200_Leave);
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Location = new System.Drawing.Point(7, 72);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(46, 13);
            this.label5.TabIndex = 4;
            this.label5.Text = "200,000";
            // 
            // txt100
            // 
            this.txt100.Location = new System.Drawing.Point(98, 43);
            this.txt100.Name = "txt100";
            this.txt100.Size = new System.Drawing.Size(121, 20);
            this.txt100.TabIndex = 3;
            this.txt100.Text = "0";
            this.txt100.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            this.txt100.TextChanged += new System.EventHandler(this.txt100_TextChanged);
            this.txt100.Leave += new System.EventHandler(this.txt100_Leave);
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(7, 46);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(46, 13);
            this.label4.TabIndex = 2;
            this.label4.Text = "100,000";
            // 
            // txt50
            // 
            this.txt50.Location = new System.Drawing.Point(98, 17);
            this.txt50.Name = "txt50";
            this.txt50.Size = new System.Drawing.Size(121, 20);
            this.txt50.TabIndex = 1;
            this.txt50.Text = "0";
            this.txt50.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            this.txt50.TextChanged += new System.EventHandler(this.txt50_TextChanged);
            this.txt50.Leave += new System.EventHandler(this.txt50_Leave);
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(7, 20);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(40, 13);
            this.label3.TabIndex = 0;
            this.label3.Text = "50,000";
            // 
            // btnLuu
            // 
            this.btnLuu.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnLuu.Location = new System.Drawing.Point(95, 305);
            this.btnLuu.Name = "btnLuu";
            this.btnLuu.Size = new System.Drawing.Size(75, 32);
            this.btnLuu.TabIndex = 2;
            this.btnLuu.Text = "Lưu";
            this.btnLuu.UseVisualStyleBackColor = true;
            this.btnLuu.Click += new System.EventHandler(this.btnLuu_Click);
            // 
            // cbKSV
            // 
            this.cbKSV.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cbKSV.FormattingEnabled = true;
            this.cbKSV.Location = new System.Drawing.Point(98, 45);
            this.cbKSV.Name = "cbKSV";
            this.cbKSV.Size = new System.Drawing.Size(121, 21);
            this.cbKSV.TabIndex = 7;
            // 
            // label8
            // 
            this.label8.AutoSize = true;
            this.label8.Location = new System.Drawing.Point(6, 48);
            this.label8.Name = "label8";
            this.label8.Size = new System.Drawing.Size(56, 13);
            this.label8.TabIndex = 8;
            this.label8.Text = "Kiểm soát:";
            // 
            // frmTiepQuy
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(265, 346);
            this.Controls.Add(this.btnLuu);
            this.Controls.Add(this.groupBox2);
            this.Controls.Add(this.groupBox1);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Name = "frmTiepQuy";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Giấy đề nghị tiếp quỹ";
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            this.groupBox2.ResumeLayout(false);
            this.groupBox2.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.ComboBox cbATMID;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.GroupBox groupBox2;
        private System.Windows.Forms.TextBox txt500;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.TextBox txt200;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.TextBox txt100;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.TextBox txt50;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.TextBox txtTong;
        private System.Windows.Forms.Label label7;
        private System.Windows.Forms.Button btnLuu;
        private System.Windows.Forms.TextBox txtTenCanBo;
        private System.Windows.Forms.SaveFileDialog saveFileTiepQuy;
        private System.Windows.Forms.ComboBox cbKSV;
        private System.Windows.Forms.Label label8;

    }
}