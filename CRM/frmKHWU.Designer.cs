namespace CRM
{
    partial class frmKHWU
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
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle1 = new System.Windows.Forms.DataGridViewCellStyle();
            this.dtpTo = new System.Windows.Forms.DateTimePicker();
            this.btnSearch = new DevComponents.DotNetBar.ButtonX();
            this.cbbMaCN = new DevComponents.DotNetBar.Controls.ComboBoxEx();
            this.dtpFrom = new System.Windows.Forms.DateTimePicker();
            this.panel4 = new System.Windows.Forms.Panel();
            this.groupBox2 = new System.Windows.Forms.GroupBox();
            this.label1 = new System.Windows.Forms.Label();
            this.groupBox3 = new System.Windows.Forms.GroupBox();
            this.btnClose = new DevComponents.DotNetBar.ButtonX();
            this.btnSEmail = new DevComponents.DotNetBar.ButtonX();
            this.txtSEmail = new System.Windows.Forms.TextBox();
            this.btnSMaKH = new DevComponents.DotNetBar.ButtonX();
            this.txtSMaKH = new System.Windows.Forms.TextBox();
            this.panel43 = new System.Windows.Forms.Panel();
            this.tabControlPanel37 = new DevComponents.DotNetBar.TabControlPanel();
            this.tabItem33 = new DevComponents.DotNetBar.TabItem(this.components);
            this.panel47 = new System.Windows.Forms.Panel();
            this.btnSNhomKH = new DevComponents.DotNetBar.ButtonX();
            this.cbbSNhomKH = new DevComponents.DotNetBar.Controls.ComboBoxEx();
            this.tabControlPanel39 = new DevComponents.DotNetBar.TabControlPanel();
            this.tabItem35 = new DevComponents.DotNetBar.TabItem(this.components);
            this.tabControl8 = new DevComponents.DotNetBar.TabControl();
            this.tabControlPanel38 = new DevComponents.DotNetBar.TabControlPanel();
            this.panel44 = new System.Windows.Forms.Panel();
            this.txtSCMND = new System.Windows.Forms.TextBox();
            this.btnSCMND = new DevComponents.DotNetBar.ButtonX();
            this.tabItem34 = new DevComponents.DotNetBar.TabItem(this.components);
            this.tabControlPanel40 = new DevComponents.DotNetBar.TabControlPanel();
            this.panel46 = new System.Windows.Forms.Panel();
            this.txtSDiachi = new System.Windows.Forms.TextBox();
            this.btnSDiachi = new DevComponents.DotNetBar.ButtonX();
            this.tabItem36 = new DevComponents.DotNetBar.TabItem(this.components);
            this.tabControlPanel35 = new DevComponents.DotNetBar.TabControlPanel();
            this.panel41 = new System.Windows.Forms.Panel();
            this.txtSTenKH = new System.Windows.Forms.TextBox();
            this.btnSTenKH = new DevComponents.DotNetBar.ButtonX();
            this.tabItem31 = new DevComponents.DotNetBar.TabItem(this.components);
            this.btnPrint = new DevComponents.DotNetBar.ButtonX();
            this.dgvDanhsach = new DevComponents.DotNetBar.Controls.DataGridViewX();
            this.btnExcel = new DevComponents.DotNetBar.ButtonX();
            this.saveFileDialog1 = new System.Windows.Forms.SaveFileDialog();
            this.labelX5 = new DevComponents.DotNetBar.LabelX();
            this.panel4.SuspendLayout();
            this.groupBox2.SuspendLayout();
            this.groupBox3.SuspendLayout();
            this.tabControlPanel37.SuspendLayout();
            this.tabControlPanel39.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.tabControl8)).BeginInit();
            this.tabControl8.SuspendLayout();
            this.tabControlPanel38.SuspendLayout();
            this.panel44.SuspendLayout();
            this.tabControlPanel40.SuspendLayout();
            this.panel46.SuspendLayout();
            this.tabControlPanel35.SuspendLayout();
            this.panel41.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dgvDanhsach)).BeginInit();
            this.SuspendLayout();
            // 
            // dtpTo
            // 
            this.dtpTo.CustomFormat = "MM/yyyy";
            this.dtpTo.Format = System.Windows.Forms.DateTimePickerFormat.Custom;
            this.dtpTo.Location = new System.Drawing.Point(122, 24);
            this.dtpTo.Name = "dtpTo";
            this.dtpTo.ShowUpDown = true;
            this.dtpTo.Size = new System.Drawing.Size(95, 26);
            this.dtpTo.TabIndex = 234;
            this.dtpTo.Value = new System.DateTime(2013, 1, 1, 0, 0, 0, 0);
            // 
            // btnSearch
            // 
            this.btnSearch.AccessibleRole = System.Windows.Forms.AccessibleRole.PushButton;
            this.btnSearch.Font = new System.Drawing.Font("Arial", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnSearch.Location = new System.Drawing.Point(621, 36);
            this.btnSearch.Name = "btnSearch";
            this.btnSearch.Size = new System.Drawing.Size(93, 23);
            this.btnSearch.TabIndex = 232;
            this.btnSearch.Text = "Tìm kiếm";
            this.btnSearch.Click += new System.EventHandler(this.btnSearch_Click);
            // 
            // cbbMaCN
            // 
            this.cbbMaCN.DisplayMember = "Text";
            this.cbbMaCN.DrawMode = System.Windows.Forms.DrawMode.OwnerDrawFixed;
            this.cbbMaCN.FormattingEnabled = true;
            this.cbbMaCN.ItemHeight = 19;
            this.cbbMaCN.Location = new System.Drawing.Point(2, 25);
            this.cbbMaCN.Name = "cbbMaCN";
            this.cbbMaCN.Size = new System.Drawing.Size(251, 25);
            this.cbbMaCN.TabIndex = 258;
            // 
            // dtpFrom
            // 
            this.dtpFrom.CustomFormat = "MM/yyyy";
            this.dtpFrom.Format = System.Windows.Forms.DateTimePickerFormat.Custom;
            this.dtpFrom.Location = new System.Drawing.Point(6, 24);
            this.dtpFrom.Name = "dtpFrom";
            this.dtpFrom.ShowUpDown = true;
            this.dtpFrom.Size = new System.Drawing.Size(95, 26);
            this.dtpFrom.TabIndex = 234;
            this.dtpFrom.Value = new System.DateTime(2013, 1, 1, 0, 0, 0, 0);
            // 
            // panel4
            // 
            this.panel4.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.panel4.Controls.Add(this.btnSearch);
            this.panel4.Controls.Add(this.groupBox2);
            this.panel4.Controls.Add(this.groupBox3);
            this.panel4.Location = new System.Drawing.Point(1, 8);
            this.panel4.Name = "panel4";
            this.panel4.Size = new System.Drawing.Size(726, 77);
            this.panel4.TabIndex = 267;
            // 
            // groupBox2
            // 
            this.groupBox2.Controls.Add(this.label1);
            this.groupBox2.Controls.Add(this.dtpFrom);
            this.groupBox2.Controls.Add(this.dtpTo);
            this.groupBox2.Font = new System.Drawing.Font("Arial", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.groupBox2.Location = new System.Drawing.Point(271, 3);
            this.groupBox2.Name = "groupBox2";
            this.groupBox2.Size = new System.Drawing.Size(225, 56);
            this.groupBox2.TabIndex = 259;
            this.groupBox2.TabStop = false;
            this.groupBox2.Text = "Kỳ";
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(105, 34);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(13, 18);
            this.label1.TabIndex = 235;
            this.label1.Text = "-";
            this.label1.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // groupBox3
            // 
            this.groupBox3.Controls.Add(this.cbbMaCN);
            this.groupBox3.Location = new System.Drawing.Point(6, 3);
            this.groupBox3.Name = "groupBox3";
            this.groupBox3.Size = new System.Drawing.Size(259, 56);
            this.groupBox3.TabIndex = 260;
            this.groupBox3.TabStop = false;
            this.groupBox3.Text = "Chi nhánh";
            // 
            // btnClose
            // 
            this.btnClose.AccessibleRole = System.Windows.Forms.AccessibleRole.PushButton;
            this.btnClose.Font = new System.Drawing.Font("Arial", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnClose.Location = new System.Drawing.Point(613, 456);
            this.btnClose.Name = "btnClose";
            this.btnClose.Size = new System.Drawing.Size(103, 29);
            this.btnClose.TabIndex = 268;
            this.btnClose.Text = "Đóng";
            this.btnClose.Click += new System.EventHandler(this.btnClose_Click);
            // 
            // btnSEmail
            // 
            this.btnSEmail.AccessibleRole = System.Windows.Forms.AccessibleRole.PushButton;
            this.btnSEmail.Font = new System.Drawing.Font("Arial", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnSEmail.Location = new System.Drawing.Point(211, 4);
            this.btnSEmail.Name = "btnSEmail";
            this.btnSEmail.Size = new System.Drawing.Size(81, 22);
            this.btnSEmail.TabIndex = 162;
            // 
            // txtSEmail
            // 
            this.txtSEmail.Font = new System.Drawing.Font("Arial", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtSEmail.Location = new System.Drawing.Point(3, 4);
            this.txtSEmail.Name = "txtSEmail";
            this.txtSEmail.Size = new System.Drawing.Size(202, 22);
            this.txtSEmail.TabIndex = 161;
            // 
            // btnSMaKH
            // 
            this.btnSMaKH.AccessibleRole = System.Windows.Forms.AccessibleRole.PushButton;
            this.btnSMaKH.Font = new System.Drawing.Font("Arial", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnSMaKH.Location = new System.Drawing.Point(213, 3);
            this.btnSMaKH.Name = "btnSMaKH";
            this.btnSMaKH.Size = new System.Drawing.Size(81, 22);
            this.btnSMaKH.TabIndex = 160;
            // 
            // txtSMaKH
            // 
            this.txtSMaKH.Font = new System.Drawing.Font("Arial", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtSMaKH.Location = new System.Drawing.Point(5, 3);
            this.txtSMaKH.Name = "txtSMaKH";
            this.txtSMaKH.Size = new System.Drawing.Size(202, 22);
            this.txtSMaKH.TabIndex = 159;
            // 
            // panel43
            // 
            this.panel43.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panel43.Location = new System.Drawing.Point(1, 1);
            this.panel43.Name = "panel43";
            this.panel43.Size = new System.Drawing.Size(798, 31);
            this.panel43.TabIndex = 1;
            // 
            // tabControlPanel37
            // 
            this.tabControlPanel37.Controls.Add(this.panel43);
            this.tabControlPanel37.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tabControlPanel37.Location = new System.Drawing.Point(0, 29);
            this.tabControlPanel37.Name = "tabControlPanel37";
            this.tabControlPanel37.Padding = new System.Windows.Forms.Padding(1);
            this.tabControlPanel37.Size = new System.Drawing.Size(800, 33);
            this.tabControlPanel37.Style.BackColor1.Color = System.Drawing.Color.FromArgb(((int)(((byte)(142)))), ((int)(((byte)(179)))), ((int)(((byte)(231)))));
            this.tabControlPanel37.Style.BackColor2.Color = System.Drawing.Color.FromArgb(((int)(((byte)(223)))), ((int)(((byte)(237)))), ((int)(((byte)(254)))));
            this.tabControlPanel37.Style.Border = DevComponents.DotNetBar.eBorderType.SingleLine;
            this.tabControlPanel37.Style.BorderColor.Color = System.Drawing.Color.FromArgb(((int)(((byte)(59)))), ((int)(((byte)(97)))), ((int)(((byte)(156)))));
            this.tabControlPanel37.Style.BorderSide = ((DevComponents.DotNetBar.eBorderSide)(((DevComponents.DotNetBar.eBorderSide.Left | DevComponents.DotNetBar.eBorderSide.Right) 
            | DevComponents.DotNetBar.eBorderSide.Bottom)));
            this.tabControlPanel37.Style.GradientAngle = 90;
            this.tabControlPanel37.TabIndex = 4;
            this.tabControlPanel37.TabItem = this.tabItem33;
            // 
            // tabItem33
            // 
            this.tabItem33.AttachedControl = this.tabControlPanel37;
            this.tabItem33.Name = "tabItem33";
            this.tabItem33.Text = "Mã KH";
            // 
            // panel47
            // 
            this.panel47.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panel47.Location = new System.Drawing.Point(1, 1);
            this.panel47.Name = "panel47";
            this.panel47.Size = new System.Drawing.Size(798, 31);
            this.panel47.TabIndex = 2;
            // 
            // btnSNhomKH
            // 
            this.btnSNhomKH.AccessibleRole = System.Windows.Forms.AccessibleRole.PushButton;
            this.btnSNhomKH.Font = new System.Drawing.Font("Arial", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnSNhomKH.Location = new System.Drawing.Point(278, 4);
            this.btnSNhomKH.Name = "btnSNhomKH";
            this.btnSNhomKH.Size = new System.Drawing.Size(81, 22);
            this.btnSNhomKH.TabIndex = 162;
            // 
            // cbbSNhomKH
            // 
            this.cbbSNhomKH.DisableInternalDrawing = true;
            this.cbbSNhomKH.DisplayMember = "Text";
            this.cbbSNhomKH.DrawMode = System.Windows.Forms.DrawMode.OwnerDrawFixed;
            this.cbbSNhomKH.FormattingEnabled = true;
            this.cbbSNhomKH.ItemHeight = 13;
            this.cbbSNhomKH.Location = new System.Drawing.Point(5, 2);
            this.cbbSNhomKH.Name = "cbbSNhomKH";
            this.cbbSNhomKH.Size = new System.Drawing.Size(249, 19);
            this.cbbSNhomKH.TabIndex = 180;
            // 
            // tabControlPanel39
            // 
            this.tabControlPanel39.Controls.Add(this.panel47);
            this.tabControlPanel39.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tabControlPanel39.Location = new System.Drawing.Point(0, 29);
            this.tabControlPanel39.Name = "tabControlPanel39";
            this.tabControlPanel39.Padding = new System.Windows.Forms.Padding(1);
            this.tabControlPanel39.Size = new System.Drawing.Size(800, 33);
            this.tabControlPanel39.Style.BackColor1.Color = System.Drawing.Color.FromArgb(((int)(((byte)(142)))), ((int)(((byte)(179)))), ((int)(((byte)(231)))));
            this.tabControlPanel39.Style.BackColor2.Color = System.Drawing.Color.FromArgb(((int)(((byte)(223)))), ((int)(((byte)(237)))), ((int)(((byte)(254)))));
            this.tabControlPanel39.Style.Border = DevComponents.DotNetBar.eBorderType.SingleLine;
            this.tabControlPanel39.Style.BorderColor.Color = System.Drawing.Color.FromArgb(((int)(((byte)(59)))), ((int)(((byte)(97)))), ((int)(((byte)(156)))));
            this.tabControlPanel39.Style.BorderSide = ((DevComponents.DotNetBar.eBorderSide)(((DevComponents.DotNetBar.eBorderSide.Left | DevComponents.DotNetBar.eBorderSide.Right) 
            | DevComponents.DotNetBar.eBorderSide.Bottom)));
            this.tabControlPanel39.Style.GradientAngle = 90;
            this.tabControlPanel39.TabIndex = 8;
            this.tabControlPanel39.TabItem = this.tabItem35;
            // 
            // tabItem35
            // 
            this.tabItem35.AttachedControl = this.tabControlPanel39;
            this.tabItem35.Name = "tabItem35";
            this.tabItem35.Text = "Nhóm KH";
            // 
            // tabControl8
            // 
            this.tabControl8.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.tabControl8.BackColor = System.Drawing.Color.Transparent;
            this.tabControl8.CanReorderTabs = true;
            this.tabControl8.Controls.Add(this.tabControlPanel38);
            this.tabControl8.Controls.Add(this.tabControlPanel40);
            this.tabControl8.Controls.Add(this.tabControlPanel35);
            this.tabControl8.Location = new System.Drawing.Point(1, 130);
            this.tabControl8.Name = "tabControl8";
            this.tabControl8.SelectedTabFont = new System.Drawing.Font("Arial", 8.25F, System.Drawing.FontStyle.Bold);
            this.tabControl8.SelectedTabIndex = 0;
            this.tabControl8.Size = new System.Drawing.Size(726, 62);
            this.tabControl8.TabIndex = 331;
            this.tabControl8.TabLayoutType = DevComponents.DotNetBar.eTabLayoutType.FixedWithNavigationBox;
            this.tabControl8.Tabs.Add(this.tabItem34);
            this.tabControl8.Tabs.Add(this.tabItem31);
            this.tabControl8.Tabs.Add(this.tabItem36);
            this.tabControl8.Text = "tabControl8";
            // 
            // tabControlPanel38
            // 
            this.tabControlPanel38.Controls.Add(this.panel44);
            this.tabControlPanel38.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tabControlPanel38.Location = new System.Drawing.Point(0, 29);
            this.tabControlPanel38.Name = "tabControlPanel38";
            this.tabControlPanel38.Padding = new System.Windows.Forms.Padding(1);
            this.tabControlPanel38.Size = new System.Drawing.Size(726, 33);
            this.tabControlPanel38.Style.BackColor1.Color = System.Drawing.Color.FromArgb(((int)(((byte)(142)))), ((int)(((byte)(179)))), ((int)(((byte)(231)))));
            this.tabControlPanel38.Style.BackColor2.Color = System.Drawing.Color.FromArgb(((int)(((byte)(223)))), ((int)(((byte)(237)))), ((int)(((byte)(254)))));
            this.tabControlPanel38.Style.Border = DevComponents.DotNetBar.eBorderType.SingleLine;
            this.tabControlPanel38.Style.BorderColor.Color = System.Drawing.Color.FromArgb(((int)(((byte)(59)))), ((int)(((byte)(97)))), ((int)(((byte)(156)))));
            this.tabControlPanel38.Style.BorderSide = ((DevComponents.DotNetBar.eBorderSide)(((DevComponents.DotNetBar.eBorderSide.Left | DevComponents.DotNetBar.eBorderSide.Right) 
            | DevComponents.DotNetBar.eBorderSide.Bottom)));
            this.tabControlPanel38.Style.GradientAngle = 90;
            this.tabControlPanel38.TabIndex = 5;
            this.tabControlPanel38.TabItem = this.tabItem34;
            // 
            // panel44
            // 
            this.panel44.Controls.Add(this.txtSCMND);
            this.panel44.Controls.Add(this.btnSCMND);
            this.panel44.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panel44.Location = new System.Drawing.Point(1, 1);
            this.panel44.Name = "panel44";
            this.panel44.Size = new System.Drawing.Size(724, 31);
            this.panel44.TabIndex = 0;
            // 
            // txtSCMND
            // 
            this.txtSCMND.Font = new System.Drawing.Font("Arial", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtSCMND.Location = new System.Drawing.Point(3, 4);
            this.txtSCMND.Name = "txtSCMND";
            this.txtSCMND.Size = new System.Drawing.Size(202, 22);
            this.txtSCMND.TabIndex = 161;
            // 
            // btnSCMND
            // 
            this.btnSCMND.AccessibleRole = System.Windows.Forms.AccessibleRole.PushButton;
            this.btnSCMND.Font = new System.Drawing.Font("Arial", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnSCMND.Location = new System.Drawing.Point(211, 4);
            this.btnSCMND.Name = "btnSCMND";
            this.btnSCMND.Size = new System.Drawing.Size(81, 22);
            this.btnSCMND.TabIndex = 162;
            this.btnSCMND.Text = "Tìm kiếm";
            this.btnSCMND.Click += new System.EventHandler(this.btnSCMND_Click);
            // 
            // tabItem34
            // 
            this.tabItem34.AttachedControl = this.tabControlPanel38;
            this.tabItem34.Name = "tabItem34";
            this.tabItem34.Text = "CMND";
            // 
            // tabControlPanel40
            // 
            this.tabControlPanel40.Controls.Add(this.panel46);
            this.tabControlPanel40.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tabControlPanel40.Location = new System.Drawing.Point(0, 29);
            this.tabControlPanel40.Name = "tabControlPanel40";
            this.tabControlPanel40.Padding = new System.Windows.Forms.Padding(1);
            this.tabControlPanel40.Size = new System.Drawing.Size(726, 33);
            this.tabControlPanel40.Style.BackColor1.Color = System.Drawing.Color.FromArgb(((int)(((byte)(142)))), ((int)(((byte)(179)))), ((int)(((byte)(231)))));
            this.tabControlPanel40.Style.BackColor2.Color = System.Drawing.Color.FromArgb(((int)(((byte)(223)))), ((int)(((byte)(237)))), ((int)(((byte)(254)))));
            this.tabControlPanel40.Style.Border = DevComponents.DotNetBar.eBorderType.SingleLine;
            this.tabControlPanel40.Style.BorderColor.Color = System.Drawing.Color.FromArgb(((int)(((byte)(59)))), ((int)(((byte)(97)))), ((int)(((byte)(156)))));
            this.tabControlPanel40.Style.BorderSide = ((DevComponents.DotNetBar.eBorderSide)(((DevComponents.DotNetBar.eBorderSide.Left | DevComponents.DotNetBar.eBorderSide.Right) 
            | DevComponents.DotNetBar.eBorderSide.Bottom)));
            this.tabControlPanel40.Style.GradientAngle = 90;
            this.tabControlPanel40.TabIndex = 7;
            this.tabControlPanel40.TabItem = this.tabItem36;
            // 
            // panel46
            // 
            this.panel46.Controls.Add(this.txtSDiachi);
            this.panel46.Controls.Add(this.btnSDiachi);
            this.panel46.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panel46.Location = new System.Drawing.Point(1, 1);
            this.panel46.Name = "panel46";
            this.panel46.Size = new System.Drawing.Size(724, 31);
            this.panel46.TabIndex = 1;
            // 
            // txtSDiachi
            // 
            this.txtSDiachi.Font = new System.Drawing.Font("Arial", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtSDiachi.Location = new System.Drawing.Point(3, 4);
            this.txtSDiachi.Name = "txtSDiachi";
            this.txtSDiachi.Size = new System.Drawing.Size(202, 22);
            this.txtSDiachi.TabIndex = 332;
            // 
            // btnSDiachi
            // 
            this.btnSDiachi.AccessibleRole = System.Windows.Forms.AccessibleRole.PushButton;
            this.btnSDiachi.Font = new System.Drawing.Font("Arial", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnSDiachi.Location = new System.Drawing.Point(211, 4);
            this.btnSDiachi.Name = "btnSDiachi";
            this.btnSDiachi.Size = new System.Drawing.Size(81, 22);
            this.btnSDiachi.TabIndex = 162;
            this.btnSDiachi.Text = "Tìm kiếm";
            this.btnSDiachi.Click += new System.EventHandler(this.btnSDiachi_Click);
            // 
            // tabItem36
            // 
            this.tabItem36.AttachedControl = this.tabControlPanel40;
            this.tabItem36.Name = "tabItem36";
            this.tabItem36.Text = "Địa chỉ";
            // 
            // tabControlPanel35
            // 
            this.tabControlPanel35.Controls.Add(this.panel41);
            this.tabControlPanel35.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tabControlPanel35.Location = new System.Drawing.Point(0, 29);
            this.tabControlPanel35.Name = "tabControlPanel35";
            this.tabControlPanel35.Padding = new System.Windows.Forms.Padding(1);
            this.tabControlPanel35.Size = new System.Drawing.Size(726, 33);
            this.tabControlPanel35.Style.BackColor1.Color = System.Drawing.Color.FromArgb(((int)(((byte)(142)))), ((int)(((byte)(179)))), ((int)(((byte)(231)))));
            this.tabControlPanel35.Style.BackColor2.Color = System.Drawing.Color.FromArgb(((int)(((byte)(223)))), ((int)(((byte)(237)))), ((int)(((byte)(254)))));
            this.tabControlPanel35.Style.Border = DevComponents.DotNetBar.eBorderType.SingleLine;
            this.tabControlPanel35.Style.BorderColor.Color = System.Drawing.Color.FromArgb(((int)(((byte)(59)))), ((int)(((byte)(97)))), ((int)(((byte)(156)))));
            this.tabControlPanel35.Style.BorderSide = ((DevComponents.DotNetBar.eBorderSide)(((DevComponents.DotNetBar.eBorderSide.Left | DevComponents.DotNetBar.eBorderSide.Right) 
            | DevComponents.DotNetBar.eBorderSide.Bottom)));
            this.tabControlPanel35.Style.GradientAngle = 90;
            this.tabControlPanel35.TabIndex = 1;
            this.tabControlPanel35.TabItem = this.tabItem31;
            // 
            // panel41
            // 
            this.panel41.Controls.Add(this.txtSTenKH);
            this.panel41.Controls.Add(this.btnSTenKH);
            this.panel41.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panel41.Location = new System.Drawing.Point(1, 1);
            this.panel41.Name = "panel41";
            this.panel41.Size = new System.Drawing.Size(724, 31);
            this.panel41.TabIndex = 0;
            // 
            // txtSTenKH
            // 
            this.txtSTenKH.Font = new System.Drawing.Font("Arial", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtSTenKH.Location = new System.Drawing.Point(3, 4);
            this.txtSTenKH.Name = "txtSTenKH";
            this.txtSTenKH.Size = new System.Drawing.Size(202, 22);
            this.txtSTenKH.TabIndex = 156;
            // 
            // btnSTenKH
            // 
            this.btnSTenKH.AccessibleRole = System.Windows.Forms.AccessibleRole.PushButton;
            this.btnSTenKH.Font = new System.Drawing.Font("Arial", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnSTenKH.Location = new System.Drawing.Point(211, 4);
            this.btnSTenKH.Name = "btnSTenKH";
            this.btnSTenKH.Size = new System.Drawing.Size(81, 22);
            this.btnSTenKH.TabIndex = 158;
            this.btnSTenKH.Text = "Tìm kiếm";
            this.btnSTenKH.Click += new System.EventHandler(this.btnSTenKH_Click);
            // 
            // tabItem31
            // 
            this.tabItem31.AttachedControl = this.tabControlPanel35;
            this.tabItem31.Name = "tabItem31";
            this.tabItem31.Text = "Tên KH";
            // 
            // btnPrint
            // 
            this.btnPrint.AccessibleRole = System.Windows.Forms.AccessibleRole.PushButton;
            this.btnPrint.Font = new System.Drawing.Font("Arial", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnPrint.Location = new System.Drawing.Point(12, 456);
            this.btnPrint.Name = "btnPrint";
            this.btnPrint.Size = new System.Drawing.Size(103, 29);
            this.btnPrint.TabIndex = 268;
            this.btnPrint.Text = "In";
            this.btnPrint.Click += new System.EventHandler(this.btnPrint_Click);
            // 
            // dgvDanhsach
            // 
            this.dgvDanhsach.BackgroundColor = System.Drawing.SystemColors.Control;
            this.dgvDanhsach.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            this.dgvDanhsach.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            dataGridViewCellStyle1.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle1.BackColor = System.Drawing.SystemColors.Window;
            dataGridViewCellStyle1.Font = new System.Drawing.Font("Arial", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            dataGridViewCellStyle1.ForeColor = System.Drawing.SystemColors.ControlText;
            dataGridViewCellStyle1.SelectionBackColor = System.Drawing.SystemColors.Highlight;
            dataGridViewCellStyle1.SelectionForeColor = System.Drawing.SystemColors.ControlText;
            dataGridViewCellStyle1.WrapMode = System.Windows.Forms.DataGridViewTriState.False;
            this.dgvDanhsach.DefaultCellStyle = dataGridViewCellStyle1;
            this.dgvDanhsach.GridColor = System.Drawing.Color.FromArgb(((int)(((byte)(208)))), ((int)(((byte)(215)))), ((int)(((byte)(229)))));
            this.dgvDanhsach.Location = new System.Drawing.Point(1, 192);
            this.dgvDanhsach.Name = "dgvDanhsach";
            this.dgvDanhsach.Size = new System.Drawing.Size(726, 258);
            this.dgvDanhsach.TabIndex = 332;
            this.dgvDanhsach.CellDoubleClick += new System.Windows.Forms.DataGridViewCellEventHandler(this.dgvDanhsach_CellDoubleClick);
            // 
            // btnExcel
            // 
            this.btnExcel.AccessibleRole = System.Windows.Forms.AccessibleRole.PushButton;
            this.btnExcel.Font = new System.Drawing.Font("Arial", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnExcel.Location = new System.Drawing.Point(121, 456);
            this.btnExcel.Name = "btnExcel";
            this.btnExcel.Size = new System.Drawing.Size(103, 29);
            this.btnExcel.TabIndex = 268;
            this.btnExcel.Text = "Xuất excel";
            this.btnExcel.Click += new System.EventHandler(this.btnExcel_Click);
            // 
            // labelX5
            // 
            this.labelX5.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.labelX5.BackColor = System.Drawing.SystemColors.ActiveCaption;
            this.labelX5.Font = new System.Drawing.Font("Arial", 15.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.labelX5.ForeColor = System.Drawing.SystemColors.ActiveCaptionText;
            this.labelX5.Location = new System.Drawing.Point(1, 91);
            this.labelX5.Name = "labelX5";
            this.labelX5.SingleLineColor = System.Drawing.SystemColors.ActiveCaption;
            this.labelX5.Size = new System.Drawing.Size(726, 39);
            this.labelX5.TabIndex = 265;
            this.labelX5.Text = "DANH SÁCH KHÁCH HÀNG";
            this.labelX5.TextAlignment = System.Drawing.StringAlignment.Center;
            // 
            // frmKHWU
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(9F, 18F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(728, 492);
            this.Controls.Add(this.dgvDanhsach);
            this.Controls.Add(this.tabControl8);
            this.Controls.Add(this.labelX5);
            this.Controls.Add(this.panel4);
            this.Controls.Add(this.btnExcel);
            this.Controls.Add(this.btnPrint);
            this.Controls.Add(this.btnClose);
            this.Font = new System.Drawing.Font("Arial", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.Margin = new System.Windows.Forms.Padding(4);
            this.Name = "frmKHWU";
            this.ShowIcon = false;
            this.Text = "Khách hàng nhận tiền WU";
            this.Load += new System.EventHandler(this.frmKHWU_Load);
            this.panel4.ResumeLayout(false);
            this.groupBox2.ResumeLayout(false);
            this.groupBox2.PerformLayout();
            this.groupBox3.ResumeLayout(false);
            this.tabControlPanel37.ResumeLayout(false);
            this.tabControlPanel39.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.tabControl8)).EndInit();
            this.tabControl8.ResumeLayout(false);
            this.tabControlPanel38.ResumeLayout(false);
            this.panel44.ResumeLayout(false);
            this.panel44.PerformLayout();
            this.tabControlPanel40.ResumeLayout(false);
            this.panel46.ResumeLayout(false);
            this.panel46.PerformLayout();
            this.tabControlPanel35.ResumeLayout(false);
            this.panel41.ResumeLayout(false);
            this.panel41.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dgvDanhsach)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.DateTimePicker dtpTo;
        private DevComponents.DotNetBar.ButtonX btnSearch;
        private DevComponents.DotNetBar.Controls.ComboBoxEx cbbMaCN;
        private System.Windows.Forms.DateTimePicker dtpFrom;
        private System.Windows.Forms.Panel panel4;
        private System.Windows.Forms.GroupBox groupBox2;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.GroupBox groupBox3;
        private DevComponents.DotNetBar.ButtonX btnClose;
        private DevComponents.DotNetBar.ButtonX btnSEmail;
        private System.Windows.Forms.TextBox txtSEmail;
        private DevComponents.DotNetBar.ButtonX btnSMaKH;
        private System.Windows.Forms.TextBox txtSMaKH;
        private System.Windows.Forms.Panel panel43;
        private DevComponents.DotNetBar.TabControlPanel tabControlPanel37;
        private DevComponents.DotNetBar.TabItem tabItem33;
        private System.Windows.Forms.Panel panel47;
        private DevComponents.DotNetBar.ButtonX btnSNhomKH;
        private DevComponents.DotNetBar.Controls.ComboBoxEx cbbSNhomKH;
        private DevComponents.DotNetBar.TabControlPanel tabControlPanel39;
        private DevComponents.DotNetBar.TabItem tabItem35;
        private DevComponents.DotNetBar.TabControl tabControl8;
        private DevComponents.DotNetBar.TabControlPanel tabControlPanel35;
        private System.Windows.Forms.Panel panel41;
        private System.Windows.Forms.TextBox txtSTenKH;
        private DevComponents.DotNetBar.ButtonX btnSTenKH;
        private DevComponents.DotNetBar.TabItem tabItem31;
        private DevComponents.DotNetBar.TabControlPanel tabControlPanel38;
        private System.Windows.Forms.Panel panel44;
        private System.Windows.Forms.TextBox txtSCMND;
        private DevComponents.DotNetBar.ButtonX btnSCMND;
        private DevComponents.DotNetBar.TabItem tabItem34;
        private DevComponents.DotNetBar.TabControlPanel tabControlPanel40;
        private System.Windows.Forms.Panel panel46;
        private DevComponents.DotNetBar.ButtonX btnSDiachi;
        private DevComponents.DotNetBar.TabItem tabItem36;
        private System.Windows.Forms.TextBox txtSDiachi;
        private DevComponents.DotNetBar.ButtonX btnPrint;
        private DevComponents.DotNetBar.Controls.DataGridViewX dgvDanhsach;
        private DevComponents.DotNetBar.ButtonX btnExcel;
        private System.Windows.Forms.SaveFileDialog saveFileDialog1;
        private DevComponents.DotNetBar.LabelX labelX5;
    }
}