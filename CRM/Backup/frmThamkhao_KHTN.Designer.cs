namespace CRM
{
    partial class frmThamkhao_KHTN
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
            this.components = new System.ComponentModel.Container();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle3 = new System.Windows.Forms.DataGridViewCellStyle();
            this.btnSCMND = new DevComponents.DotNetBar.ButtonX();
            this.pnlSCN = new System.Windows.Forms.Panel();
            this.dgvDanhsach = new DevComponents.DotNetBar.Controls.DataGridViewX();
            this.tctSearch = new DevComponents.DotNetBar.TabControl();
            this.tabControlPanel1 = new DevComponents.DotNetBar.TabControlPanel();
            this.panel1 = new System.Windows.Forms.Panel();
            this.btnSTen = new DevComponents.DotNetBar.ButtonX();
            this.txtSTen = new System.Windows.Forms.TextBox();
            this.panelEx1 = new DevComponents.DotNetBar.PanelEx();
            this.titTenKH = new DevComponents.DotNetBar.TabItem(this.components);
            this.tabControlPanel2 = new DevComponents.DotNetBar.TabControlPanel();
            this.panel2 = new System.Windows.Forms.Panel();
            this.cbbSLoaiKH = new DevComponents.DotNetBar.Controls.ComboBoxEx();
            this.comboItem1 = new DevComponents.Editors.ComboItem();
            this.comboItem2 = new DevComponents.Editors.ComboItem();
            this.btnLoaiKH = new DevComponents.DotNetBar.ButtonX();
            this.tabItem1 = new DevComponents.DotNetBar.TabItem(this.components);
            this.tabControlPanel5 = new DevComponents.DotNetBar.TabControlPanel();
            this.panel4 = new System.Windows.Forms.Panel();
            this.txtSMaKH = new System.Windows.Forms.TextBox();
            this.titCMND = new DevComponents.DotNetBar.TabItem(this.components);
            this.labelX19 = new DevComponents.DotNetBar.LabelX();
            this.btnExit = new DevComponents.DotNetBar.ButtonX();
            this.btnOK = new DevComponents.DotNetBar.ButtonX();
            this.pnlSCN.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dgvDanhsach)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.tctSearch)).BeginInit();
            this.tctSearch.SuspendLayout();
            this.tabControlPanel1.SuspendLayout();
            this.panel1.SuspendLayout();
            this.tabControlPanel2.SuspendLayout();
            this.panel2.SuspendLayout();
            this.tabControlPanel5.SuspendLayout();
            this.panel4.SuspendLayout();
            this.SuspendLayout();
            // 
            // btnSCMND
            // 
            this.btnSCMND.AccessibleRole = System.Windows.Forms.AccessibleRole.PushButton;
            this.btnSCMND.Location = new System.Drawing.Point(229, 4);
            this.btnSCMND.Name = "btnSCMND";
            this.btnSCMND.Size = new System.Drawing.Size(75, 20);
            this.btnSCMND.TabIndex = 1;
            this.btnSCMND.Text = "Tìm kiếm";
            this.btnSCMND.Click += new System.EventHandler(this.btnSMaKH_Click);
            // 
            // pnlSCN
            // 
            this.pnlSCN.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.pnlSCN.Controls.Add(this.dgvDanhsach);
            this.pnlSCN.Controls.Add(this.tctSearch);
            this.pnlSCN.Controls.Add(this.labelX19);
            this.pnlSCN.Location = new System.Drawing.Point(11, 8);
            this.pnlSCN.Name = "pnlSCN";
            this.pnlSCN.Size = new System.Drawing.Size(768, 286);
            this.pnlSCN.TabIndex = 285;
            // 
            // dgvDanhsach
            // 
            this.dgvDanhsach.BackgroundColor = System.Drawing.SystemColors.Control;
            this.dgvDanhsach.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            dataGridViewCellStyle3.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle3.BackColor = System.Drawing.SystemColors.Window;
            dataGridViewCellStyle3.Font = new System.Drawing.Font("Arial", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            dataGridViewCellStyle3.ForeColor = System.Drawing.SystemColors.ControlText;
            dataGridViewCellStyle3.SelectionBackColor = System.Drawing.SystemColors.Highlight;
            dataGridViewCellStyle3.SelectionForeColor = System.Drawing.SystemColors.ControlText;
            dataGridViewCellStyle3.WrapMode = System.Windows.Forms.DataGridViewTriState.False;
            this.dgvDanhsach.DefaultCellStyle = dataGridViewCellStyle3;
            this.dgvDanhsach.GridColor = System.Drawing.Color.FromArgb(((int)(((byte)(208)))), ((int)(((byte)(215)))), ((int)(((byte)(229)))));
            this.dgvDanhsach.Location = new System.Drawing.Point(-1, 86);
            this.dgvDanhsach.Name = "dgvDanhsach";
            this.dgvDanhsach.Size = new System.Drawing.Size(768, 199);
            this.dgvDanhsach.TabIndex = 218;
            // 
            // tctSearch
            // 
            this.tctSearch.CanReorderTabs = true;
            this.tctSearch.Controls.Add(this.tabControlPanel1);
            this.tctSearch.Controls.Add(this.tabControlPanel2);
            this.tctSearch.Controls.Add(this.tabControlPanel5);
            this.tctSearch.Location = new System.Drawing.Point(-1, 26);
            this.tctSearch.Name = "tctSearch";
            this.tctSearch.SelectedTabFont = new System.Drawing.Font("Arial", 12F, System.Drawing.FontStyle.Bold);
            this.tctSearch.SelectedTabIndex = 0;
            this.tctSearch.Size = new System.Drawing.Size(768, 57);
            this.tctSearch.TabIndex = 214;
            this.tctSearch.TabLayoutType = DevComponents.DotNetBar.eTabLayoutType.FixedWithNavigationBox;
            this.tctSearch.Tabs.Add(this.titTenKH);
            this.tctSearch.Tabs.Add(this.titCMND);
            this.tctSearch.Tabs.Add(this.tabItem1);
            this.tctSearch.Text = "tabControl2";
            // 
            // tabControlPanel1
            // 
            this.tabControlPanel1.Controls.Add(this.panel1);
            this.tabControlPanel1.Controls.Add(this.panelEx1);
            this.tabControlPanel1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tabControlPanel1.Location = new System.Drawing.Point(0, 29);
            this.tabControlPanel1.Name = "tabControlPanel1";
            this.tabControlPanel1.Padding = new System.Windows.Forms.Padding(1);
            this.tabControlPanel1.Size = new System.Drawing.Size(768, 28);
            this.tabControlPanel1.Style.BackColor1.Color = System.Drawing.Color.FromArgb(((int)(((byte)(142)))), ((int)(((byte)(179)))), ((int)(((byte)(231)))));
            this.tabControlPanel1.Style.BackColor2.Color = System.Drawing.Color.FromArgb(((int)(((byte)(223)))), ((int)(((byte)(237)))), ((int)(((byte)(254)))));
            this.tabControlPanel1.Style.Border = DevComponents.DotNetBar.eBorderType.SingleLine;
            this.tabControlPanel1.Style.BorderColor.Color = System.Drawing.Color.FromArgb(((int)(((byte)(59)))), ((int)(((byte)(97)))), ((int)(((byte)(156)))));
            this.tabControlPanel1.Style.BorderSide = ((DevComponents.DotNetBar.eBorderSide)(((DevComponents.DotNetBar.eBorderSide.Left | DevComponents.DotNetBar.eBorderSide.Right)
                        | DevComponents.DotNetBar.eBorderSide.Bottom)));
            this.tabControlPanel1.Style.GradientAngle = 90;
            this.tabControlPanel1.TabIndex = 1;
            this.tabControlPanel1.TabItem = this.titTenKH;
            // 
            // panel1
            // 
            this.panel1.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.panel1.Controls.Add(this.btnSTen);
            this.panel1.Controls.Add(this.txtSTen);
            this.panel1.Location = new System.Drawing.Point(0, 0);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(768, 28);
            this.panel1.TabIndex = 1;
            // 
            // btnSTen
            // 
            this.btnSTen.AccessibleRole = System.Windows.Forms.AccessibleRole.PushButton;
            this.btnSTen.Location = new System.Drawing.Point(229, 4);
            this.btnSTen.Name = "btnSTen";
            this.btnSTen.Size = new System.Drawing.Size(75, 20);
            this.btnSTen.TabIndex = 1;
            this.btnSTen.Text = "Tìm kiếm";
            this.btnSTen.Click += new System.EventHandler(this.btnSTen_Click);
            // 
            // txtSTen
            // 
            this.txtSTen.Location = new System.Drawing.Point(3, 1);
            this.txtSTen.Name = "txtSTen";
            this.txtSTen.Size = new System.Drawing.Size(211, 26);
            this.txtSTen.TabIndex = 0;
            // 
            // panelEx1
            // 
            this.panelEx1.CanvasColor = System.Drawing.SystemColors.Control;
            this.panelEx1.Location = new System.Drawing.Point(42, 80);
            this.panelEx1.Name = "panelEx1";
            this.panelEx1.Size = new System.Drawing.Size(201, 24);
            this.panelEx1.Style.Alignment = System.Drawing.StringAlignment.Center;
            this.panelEx1.Style.BackColor1.ColorSchemePart = DevComponents.DotNetBar.eColorSchemePart.PanelBackground;
            this.panelEx1.Style.BackColor2.ColorSchemePart = DevComponents.DotNetBar.eColorSchemePart.PanelBackground2;
            this.panelEx1.Style.Border = DevComponents.DotNetBar.eBorderType.SingleLine;
            this.panelEx1.Style.BorderColor.ColorSchemePart = DevComponents.DotNetBar.eColorSchemePart.PanelBorder;
            this.panelEx1.Style.ForeColor.ColorSchemePart = DevComponents.DotNetBar.eColorSchemePart.PanelText;
            this.panelEx1.Style.GradientAngle = 90;
            this.panelEx1.TabIndex = 0;
            this.panelEx1.Text = "panelEx1";
            // 
            // titTenKH
            // 
            this.titTenKH.AttachedControl = this.tabControlPanel1;
            this.titTenKH.Name = "titTenKH";
            this.titTenKH.Text = "Tên KH";
            // 
            // tabControlPanel2
            // 
            this.tabControlPanel2.Controls.Add(this.panel2);
            this.tabControlPanel2.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tabControlPanel2.Location = new System.Drawing.Point(0, 29);
            this.tabControlPanel2.Name = "tabControlPanel2";
            this.tabControlPanel2.Padding = new System.Windows.Forms.Padding(1);
            this.tabControlPanel2.Size = new System.Drawing.Size(768, 28);
            this.tabControlPanel2.Style.BackColor1.Color = System.Drawing.Color.FromArgb(((int)(((byte)(142)))), ((int)(((byte)(179)))), ((int)(((byte)(231)))));
            this.tabControlPanel2.Style.BackColor2.Color = System.Drawing.Color.FromArgb(((int)(((byte)(223)))), ((int)(((byte)(237)))), ((int)(((byte)(254)))));
            this.tabControlPanel2.Style.Border = DevComponents.DotNetBar.eBorderType.SingleLine;
            this.tabControlPanel2.Style.BorderColor.Color = System.Drawing.Color.FromArgb(((int)(((byte)(59)))), ((int)(((byte)(97)))), ((int)(((byte)(156)))));
            this.tabControlPanel2.Style.BorderSide = ((DevComponents.DotNetBar.eBorderSide)(((DevComponents.DotNetBar.eBorderSide.Left | DevComponents.DotNetBar.eBorderSide.Right)
                        | DevComponents.DotNetBar.eBorderSide.Bottom)));
            this.tabControlPanel2.Style.GradientAngle = 90;
            this.tabControlPanel2.TabIndex = 5;
            this.tabControlPanel2.TabItem = this.tabItem1;
            // 
            // panel2
            // 
            this.panel2.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.panel2.Controls.Add(this.cbbSLoaiKH);
            this.panel2.Controls.Add(this.btnLoaiKH);
            this.panel2.Location = new System.Drawing.Point(0, -2);
            this.panel2.Name = "panel2";
            this.panel2.Size = new System.Drawing.Size(768, 28);
            this.panel2.TabIndex = 288;
            // 
            // cbbSLoaiKH
            // 
            this.cbbSLoaiKH.DisplayMember = "Text";
            this.cbbSLoaiKH.DrawMode = System.Windows.Forms.DrawMode.OwnerDrawFixed;
            this.cbbSLoaiKH.FormattingEnabled = true;
            this.cbbSLoaiKH.ItemHeight = 19;
            this.cbbSLoaiKH.Items.AddRange(new object[] {
            this.comboItem1,
            this.comboItem2});
            this.cbbSLoaiKH.Location = new System.Drawing.Point(4, 2);
            this.cbbSLoaiKH.Name = "cbbSLoaiKH";
            this.cbbSLoaiKH.Size = new System.Drawing.Size(176, 25);
            this.cbbSLoaiKH.TabIndex = 218;
            // 
            // comboItem1
            // 
            this.comboItem1.FontSize = 12F;
            this.comboItem1.Text = "Cá nhân";
            // 
            // comboItem2
            // 
            this.comboItem2.FontSize = 12F;
            this.comboItem2.Text = "Doanh nghiệp";
            // 
            // btnLoaiKH
            // 
            this.btnLoaiKH.AccessibleRole = System.Windows.Forms.AccessibleRole.PushButton;
            this.btnLoaiKH.Location = new System.Drawing.Point(186, 6);
            this.btnLoaiKH.Name = "btnLoaiKH";
            this.btnLoaiKH.Size = new System.Drawing.Size(75, 20);
            this.btnLoaiKH.TabIndex = 1;
            this.btnLoaiKH.Text = "Tìm kiếm";
            this.btnLoaiKH.Click += new System.EventHandler(this.btnLoaiKH_Click);
            // 
            // tabItem1
            // 
            this.tabItem1.AttachedControl = this.tabControlPanel2;
            this.tabItem1.Name = "tabItem1";
            this.tabItem1.Text = "Loại KH";
            this.tabItem1.Visible = false;
            // 
            // tabControlPanel5
            // 
            this.tabControlPanel5.Controls.Add(this.panel4);
            this.tabControlPanel5.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tabControlPanel5.Location = new System.Drawing.Point(0, 29);
            this.tabControlPanel5.Name = "tabControlPanel5";
            this.tabControlPanel5.Padding = new System.Windows.Forms.Padding(1);
            this.tabControlPanel5.Size = new System.Drawing.Size(768, 28);
            this.tabControlPanel5.Style.BackColor1.Color = System.Drawing.Color.FromArgb(((int)(((byte)(142)))), ((int)(((byte)(179)))), ((int)(((byte)(231)))));
            this.tabControlPanel5.Style.BackColor2.Color = System.Drawing.Color.FromArgb(((int)(((byte)(223)))), ((int)(((byte)(237)))), ((int)(((byte)(254)))));
            this.tabControlPanel5.Style.Border = DevComponents.DotNetBar.eBorderType.SingleLine;
            this.tabControlPanel5.Style.BorderColor.Color = System.Drawing.Color.FromArgb(((int)(((byte)(59)))), ((int)(((byte)(97)))), ((int)(((byte)(156)))));
            this.tabControlPanel5.Style.BorderSide = ((DevComponents.DotNetBar.eBorderSide)(((DevComponents.DotNetBar.eBorderSide.Left | DevComponents.DotNetBar.eBorderSide.Right)
                        | DevComponents.DotNetBar.eBorderSide.Bottom)));
            this.tabControlPanel5.Style.GradientAngle = 90;
            this.tabControlPanel5.TabIndex = 4;
            this.tabControlPanel5.TabItem = this.titCMND;
            // 
            // panel4
            // 
            this.panel4.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.panel4.Controls.Add(this.btnSCMND);
            this.panel4.Controls.Add(this.txtSMaKH);
            this.panel4.Location = new System.Drawing.Point(0, 0);
            this.panel4.Name = "panel4";
            this.panel4.Size = new System.Drawing.Size(768, 28);
            this.panel4.TabIndex = 210;
            // 
            // txtSMaKH
            // 
            this.txtSMaKH.Location = new System.Drawing.Point(3, 1);
            this.txtSMaKH.Name = "txtSMaKH";
            this.txtSMaKH.Size = new System.Drawing.Size(211, 26);
            this.txtSMaKH.TabIndex = 0;
            // 
            // titCMND
            // 
            this.titCMND.AttachedControl = this.tabControlPanel5;
            this.titCMND.Name = "titCMND";
            this.titCMND.Text = "Mã KH";
            // 
            // labelX19
            // 
            this.labelX19.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.labelX19.BackColor = System.Drawing.SystemColors.ActiveCaption;
            this.labelX19.Font = new System.Drawing.Font("Arial", 15.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.labelX19.ForeColor = System.Drawing.SystemColors.ActiveCaptionText;
            this.labelX19.Location = new System.Drawing.Point(-1, -1);
            this.labelX19.Name = "labelX19";
            this.labelX19.SingleLineColor = System.Drawing.SystemColors.ActiveCaption;
            this.labelX19.Size = new System.Drawing.Size(768, 27);
            this.labelX19.TabIndex = 213;
            this.labelX19.Text = "DANH SÁCH KHÁCH HÀNG TIỀM NĂNG";
            this.labelX19.TextAlignment = System.Drawing.StringAlignment.Center;
            // 
            // btnExit
            // 
            this.btnExit.AccessibleRole = System.Windows.Forms.AccessibleRole.PushButton;
            this.btnExit.Location = new System.Drawing.Point(672, 311);
            this.btnExit.Name = "btnExit";
            this.btnExit.Size = new System.Drawing.Size(75, 20);
            this.btnExit.TabIndex = 287;
            this.btnExit.Text = "Đóng";
            this.btnExit.Click += new System.EventHandler(this.btnExit_Click);
            // 
            // btnOK
            // 
            this.btnOK.AccessibleRole = System.Windows.Forms.AccessibleRole.PushButton;
            this.btnOK.Location = new System.Drawing.Point(591, 311);
            this.btnOK.Name = "btnOK";
            this.btnOK.Size = new System.Drawing.Size(75, 20);
            this.btnOK.TabIndex = 286;
            this.btnOK.Text = "OK";
            this.btnOK.Click += new System.EventHandler(this.btnOK_Click);
            // 
            // frmThamkhao_KHTN
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(9F, 18F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(791, 338);
            this.Controls.Add(this.pnlSCN);
            this.Controls.Add(this.btnExit);
            this.Controls.Add(this.btnOK);
            this.Font = new System.Drawing.Font("Arial", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.Margin = new System.Windows.Forms.Padding(4);
            this.Name = "frmThamkhao_KHTN";
            this.Text = "Tim kiem KH";
            this.Load += new System.EventHandler(this.frmThamkhao_KHTN_Load_1);
            this.pnlSCN.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.dgvDanhsach)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.tctSearch)).EndInit();
            this.tctSearch.ResumeLayout(false);
            this.tabControlPanel1.ResumeLayout(false);
            this.panel1.ResumeLayout(false);
            this.panel1.PerformLayout();
            this.tabControlPanel2.ResumeLayout(false);
            this.panel2.ResumeLayout(false);
            this.tabControlPanel5.ResumeLayout(false);
            this.panel4.ResumeLayout(false);
            this.panel4.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private DevComponents.DotNetBar.ButtonX btnSCMND;
        private System.Windows.Forms.Panel pnlSCN;
        private DevComponents.DotNetBar.TabControl tctSearch;
        private DevComponents.DotNetBar.TabControlPanel tabControlPanel5;
        private System.Windows.Forms.Panel panel4;
        private System.Windows.Forms.TextBox txtSMaKH;
        private DevComponents.DotNetBar.TabItem titCMND;
        private DevComponents.DotNetBar.TabControlPanel tabControlPanel1;
        private System.Windows.Forms.Panel panel1;
        private DevComponents.DotNetBar.ButtonX btnSTen;
        private System.Windows.Forms.TextBox txtSTen;
        private DevComponents.DotNetBar.PanelEx panelEx1;
        private DevComponents.DotNetBar.TabItem titTenKH;
        private DevComponents.DotNetBar.LabelX labelX19;
        private DevComponents.DotNetBar.ButtonX btnExit;
        private DevComponents.DotNetBar.ButtonX btnOK;
        private DevComponents.DotNetBar.TabControlPanel tabControlPanel2;
        private System.Windows.Forms.Panel panel2;
        private DevComponents.DotNetBar.Controls.ComboBoxEx cbbSLoaiKH;
        private DevComponents.Editors.ComboItem comboItem1;
        private DevComponents.DotNetBar.ButtonX btnLoaiKH;
        private DevComponents.DotNetBar.TabItem tabItem1;
        private DevComponents.Editors.ComboItem comboItem2;
        private DevComponents.DotNetBar.Controls.DataGridViewX dgvDanhsach;


    }
}