namespace CRM
{
    partial class frmMK_Email
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
            this.panel43 = new System.Windows.Forms.Panel();
            this.txtSMaKH = new System.Windows.Forms.TextBox();
            this.btnSMaKH = new DevComponents.DotNetBar.ButtonX();
            this.cbbSNhomKH = new DevComponents.DotNetBar.Controls.ComboBoxEx();
            this.tabControlPanel37 = new DevComponents.DotNetBar.TabControlPanel();
            this.tabItem33 = new DevComponents.DotNetBar.TabItem(this.components);
            this.tabControlPanel35 = new DevComponents.DotNetBar.TabControlPanel();
            this.panel41 = new System.Windows.Forms.Panel();
            this.txtSTenKH = new System.Windows.Forms.TextBox();
            this.btnSTenKH = new DevComponents.DotNetBar.ButtonX();
            this.tabItem31 = new DevComponents.DotNetBar.TabItem(this.components);
            this.btnDeselectall = new DevComponents.DotNetBar.ButtonX();
            this.btnSave = new DevComponents.DotNetBar.ButtonX();
            this.rdbTiemnang = new System.Windows.Forms.RadioButton();
            this.labelX96 = new DevComponents.DotNetBar.LabelX();
            this.groupBox10 = new System.Windows.Forms.GroupBox();
            this.rdbHienhuu = new System.Windows.Forms.RadioButton();
            this.dgvDanhsach = new DevComponents.DotNetBar.Controls.DataGridViewX();
            this.labelX2 = new DevComponents.DotNetBar.LabelX();
            this.tabControl8 = new DevComponents.DotNetBar.TabControl();
            this.tabControlPanel39 = new DevComponents.DotNetBar.TabControlPanel();
            this.panel47 = new System.Windows.Forms.Panel();
            this.btnSNhomKH = new DevComponents.DotNetBar.ButtonX();
            this.tabItem35 = new DevComponents.DotNetBar.TabItem(this.components);
            this.tabControlPanel40 = new DevComponents.DotNetBar.TabControlPanel();
            this.panel46 = new System.Windows.Forms.Panel();
            this.txtSEmail = new System.Windows.Forms.TextBox();
            this.btnSEmail = new DevComponents.DotNetBar.ButtonX();
            this.tabItem36 = new DevComponents.DotNetBar.TabItem(this.components);
            this.tabControlPanel38 = new DevComponents.DotNetBar.TabControlPanel();
            this.panel44 = new System.Windows.Forms.Panel();
            this.txtSCMND = new System.Windows.Forms.TextBox();
            this.btnSCMND = new DevComponents.DotNetBar.ButtonX();
            this.tabItem34 = new DevComponents.DotNetBar.TabItem(this.components);
            this.txtNoidung = new DevComponents.DotNetBar.Controls.TextBoxX();
            this.btnSelectall = new DevComponents.DotNetBar.ButtonX();
            this.panel1 = new System.Windows.Forms.Panel();
            this.txtChude = new System.Windows.Forms.TextBox();
            this.labelX1 = new DevComponents.DotNetBar.LabelX();
            this.panel43.SuspendLayout();
            this.tabControlPanel37.SuspendLayout();
            this.tabControlPanel35.SuspendLayout();
            this.panel41.SuspendLayout();
            this.groupBox10.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dgvDanhsach)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.tabControl8)).BeginInit();
            this.tabControl8.SuspendLayout();
            this.tabControlPanel39.SuspendLayout();
            this.panel47.SuspendLayout();
            this.tabControlPanel40.SuspendLayout();
            this.panel46.SuspendLayout();
            this.tabControlPanel38.SuspendLayout();
            this.panel44.SuspendLayout();
            this.panel1.SuspendLayout();
            this.SuspendLayout();
            // 
            // panel43
            // 
            this.panel43.Controls.Add(this.txtSMaKH);
            this.panel43.Controls.Add(this.btnSMaKH);
            this.panel43.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panel43.Location = new System.Drawing.Point(1, 1);
            this.panel43.Name = "panel43";
            this.panel43.Size = new System.Drawing.Size(798, 31);
            this.panel43.TabIndex = 1;
            // 
            // txtSMaKH
            // 
            this.txtSMaKH.Font = new System.Drawing.Font("Arial", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtSMaKH.Location = new System.Drawing.Point(5, 3);
            this.txtSMaKH.Name = "txtSMaKH";
            this.txtSMaKH.Size = new System.Drawing.Size(202, 22);
            this.txtSMaKH.TabIndex = 159;
            // 
            // btnSMaKH
            // 
            this.btnSMaKH.AccessibleRole = System.Windows.Forms.AccessibleRole.PushButton;
            this.btnSMaKH.Font = new System.Drawing.Font("Arial", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnSMaKH.Location = new System.Drawing.Point(213, 3);
            this.btnSMaKH.Name = "btnSMaKH";
            this.btnSMaKH.Size = new System.Drawing.Size(81, 22);
            this.btnSMaKH.TabIndex = 160;
            this.btnSMaKH.Text = "Tìm Kiếm";
            this.btnSMaKH.Click += new System.EventHandler(this.btnSMaKH_Click);
            // 
            // cbbSNhomKH
            // 
            this.cbbSNhomKH.DisplayMember = "Text";
            this.cbbSNhomKH.DrawMode = System.Windows.Forms.DrawMode.OwnerDrawFixed;
            this.cbbSNhomKH.FormattingEnabled = true;
            this.cbbSNhomKH.ItemHeight = 19;
            this.cbbSNhomKH.Location = new System.Drawing.Point(5, 2);
            this.cbbSNhomKH.Name = "cbbSNhomKH";
            this.cbbSNhomKH.Size = new System.Drawing.Size(249, 25);
            this.cbbSNhomKH.TabIndex = 180;
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
            // tabControlPanel35
            // 
            this.tabControlPanel35.Controls.Add(this.panel41);
            this.tabControlPanel35.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tabControlPanel35.Location = new System.Drawing.Point(0, 29);
            this.tabControlPanel35.Name = "tabControlPanel35";
            this.tabControlPanel35.Padding = new System.Windows.Forms.Padding(1);
            this.tabControlPanel35.Size = new System.Drawing.Size(800, 33);
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
            this.panel41.Size = new System.Drawing.Size(798, 31);
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
            this.btnSTenKH.Text = "Tìm Kiếm";
            this.btnSTenKH.Click += new System.EventHandler(this.btnSTenKH_Click);
            // 
            // tabItem31
            // 
            this.tabItem31.AttachedControl = this.tabControlPanel35;
            this.tabItem31.Name = "tabItem31";
            this.tabItem31.Text = "Tên KH";
            // 
            // btnDeselectall
            // 
            this.btnDeselectall.AccessibleRole = System.Windows.Forms.AccessibleRole.PushButton;
            this.btnDeselectall.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.btnDeselectall.Font = new System.Drawing.Font("Arial", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnDeselectall.Location = new System.Drawing.Point(622, 457);
            this.btnDeselectall.Name = "btnDeselectall";
            this.btnDeselectall.Size = new System.Drawing.Size(89, 27);
            this.btnDeselectall.TabIndex = 332;
            this.btnDeselectall.Text = "Bỏ chọn hết";
            this.btnDeselectall.Click += new System.EventHandler(this.btnDeselectall_Click);
            // 
            // btnSave
            // 
            this.btnSave.AccessibleRole = System.Windows.Forms.AccessibleRole.PushButton;
            this.btnSave.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.btnSave.Font = new System.Drawing.Font("Arial", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnSave.Location = new System.Drawing.Point(12, 457);
            this.btnSave.Name = "btnSave";
            this.btnSave.Size = new System.Drawing.Size(89, 27);
            this.btnSave.TabIndex = 331;
            this.btnSave.Text = "Lưu";
            this.btnSave.Click += new System.EventHandler(this.btnSave_Click);
            // 
            // rdbTiemnang
            // 
            this.rdbTiemnang.AutoSize = true;
            this.rdbTiemnang.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.rdbTiemnang.ForeColor = System.Drawing.SystemColors.ActiveCaption;
            this.rdbTiemnang.Location = new System.Drawing.Point(6, 45);
            this.rdbTiemnang.Name = "rdbTiemnang";
            this.rdbTiemnang.Size = new System.Drawing.Size(157, 20);
            this.rdbTiemnang.TabIndex = 14;
            this.rdbTiemnang.Text = "Khách hàng tiềm năng";
            this.rdbTiemnang.UseVisualStyleBackColor = true;
            // 
            // labelX96
            // 
            this.labelX96.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.labelX96.BackColor = System.Drawing.SystemColors.ActiveCaption;
            this.labelX96.Font = new System.Drawing.Font("Arial", 15.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.labelX96.ForeColor = System.Drawing.SystemColors.ActiveCaptionText;
            this.labelX96.Location = new System.Drawing.Point(6, 6);
            this.labelX96.Name = "labelX96";
            this.labelX96.SingleLineColor = System.Drawing.SystemColors.ActiveCaption;
            this.labelX96.Size = new System.Drawing.Size(800, 37);
            this.labelX96.TabIndex = 329;
            this.labelX96.Text = "GỬI EMAIL CHO KHÁCH HÀNG";
            this.labelX96.TextAlignment = System.Drawing.StringAlignment.Center;
            // 
            // groupBox10
            // 
            this.groupBox10.Controls.Add(this.rdbTiemnang);
            this.groupBox10.Controls.Add(this.rdbHienhuu);
            this.groupBox10.Location = new System.Drawing.Point(536, 34);
            this.groupBox10.Name = "groupBox10";
            this.groupBox10.Size = new System.Drawing.Size(168, 73);
            this.groupBox10.TabIndex = 322;
            this.groupBox10.TabStop = false;
            // 
            // rdbHienhuu
            // 
            this.rdbHienhuu.AutoSize = true;
            this.rdbHienhuu.Checked = true;
            this.rdbHienhuu.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.rdbHienhuu.ForeColor = System.Drawing.SystemColors.ActiveCaption;
            this.rdbHienhuu.Location = new System.Drawing.Point(6, 19);
            this.rdbHienhuu.Name = "rdbHienhuu";
            this.rdbHienhuu.Size = new System.Drawing.Size(148, 20);
            this.rdbHienhuu.TabIndex = 13;
            this.rdbHienhuu.TabStop = true;
            this.rdbHienhuu.Text = "Khách hàng hiện hữu";
            this.rdbHienhuu.UseVisualStyleBackColor = true;
            this.rdbHienhuu.CheckedChanged += new System.EventHandler(this.rdbHienhuu_CheckedChanged);
            // 
            // dgvDanhsach
            // 
            this.dgvDanhsach.AllowUserToAddRows = false;
            this.dgvDanhsach.AllowUserToDeleteRows = false;
            this.dgvDanhsach.BackgroundColor = System.Drawing.SystemColors.Control;
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
            this.dgvDanhsach.Location = new System.Drawing.Point(6, 224);
            this.dgvDanhsach.Name = "dgvDanhsach";
            this.dgvDanhsach.Size = new System.Drawing.Size(799, 227);
            this.dgvDanhsach.TabIndex = 334;
            // 
            // labelX2
            // 
            this.labelX2.Font = new System.Drawing.Font("Arial", 14.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.labelX2.Location = new System.Drawing.Point(16, 29);
            this.labelX2.Name = "labelX2";
            this.labelX2.Size = new System.Drawing.Size(78, 24);
            this.labelX2.TabIndex = 320;
            this.labelX2.Text = "Nội dung";
            // 
            // tabControl8
            // 
            this.tabControl8.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.tabControl8.BackColor = System.Drawing.Color.Transparent;
            this.tabControl8.CanReorderTabs = true;
            this.tabControl8.Controls.Add(this.tabControlPanel35);
            this.tabControl8.Controls.Add(this.tabControlPanel37);
            this.tabControl8.Controls.Add(this.tabControlPanel39);
            this.tabControl8.Controls.Add(this.tabControlPanel40);
            this.tabControl8.Controls.Add(this.tabControlPanel38);
            this.tabControl8.Location = new System.Drawing.Point(6, 159);
            this.tabControl8.Name = "tabControl8";
            this.tabControl8.SelectedTabFont = new System.Drawing.Font("Arial", 8.25F, System.Drawing.FontStyle.Bold);
            this.tabControl8.SelectedTabIndex = 0;
            this.tabControl8.Size = new System.Drawing.Size(800, 62);
            this.tabControl8.TabIndex = 330;
            this.tabControl8.TabLayoutType = DevComponents.DotNetBar.eTabLayoutType.FixedWithNavigationBox;
            this.tabControl8.Tabs.Add(this.tabItem33);
            this.tabControl8.Tabs.Add(this.tabItem31);
            this.tabControl8.Tabs.Add(this.tabItem34);
            this.tabControl8.Tabs.Add(this.tabItem36);
            this.tabControl8.Tabs.Add(this.tabItem35);
            this.tabControl8.Text = "tabControl8";
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
            // panel47
            // 
            this.panel47.Controls.Add(this.cbbSNhomKH);
            this.panel47.Controls.Add(this.btnSNhomKH);
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
            this.btnSNhomKH.Text = "Tìm Kiếm";
            this.btnSNhomKH.Click += new System.EventHandler(this.btnSNhomKH_Click);
            // 
            // tabItem35
            // 
            this.tabItem35.AttachedControl = this.tabControlPanel39;
            this.tabItem35.Name = "tabItem35";
            this.tabItem35.Text = "Nhóm KH";
            // 
            // tabControlPanel40
            // 
            this.tabControlPanel40.Controls.Add(this.panel46);
            this.tabControlPanel40.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tabControlPanel40.Location = new System.Drawing.Point(0, 29);
            this.tabControlPanel40.Name = "tabControlPanel40";
            this.tabControlPanel40.Padding = new System.Windows.Forms.Padding(1);
            this.tabControlPanel40.Size = new System.Drawing.Size(800, 33);
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
            this.panel46.Controls.Add(this.txtSEmail);
            this.panel46.Controls.Add(this.btnSEmail);
            this.panel46.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panel46.Location = new System.Drawing.Point(1, 1);
            this.panel46.Name = "panel46";
            this.panel46.Size = new System.Drawing.Size(798, 31);
            this.panel46.TabIndex = 1;
            // 
            // txtSEmail
            // 
            this.txtSEmail.Font = new System.Drawing.Font("Arial", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtSEmail.Location = new System.Drawing.Point(3, 4);
            this.txtSEmail.Name = "txtSEmail";
            this.txtSEmail.Size = new System.Drawing.Size(202, 22);
            this.txtSEmail.TabIndex = 161;
            // 
            // btnSEmail
            // 
            this.btnSEmail.AccessibleRole = System.Windows.Forms.AccessibleRole.PushButton;
            this.btnSEmail.Font = new System.Drawing.Font("Arial", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnSEmail.Location = new System.Drawing.Point(211, 4);
            this.btnSEmail.Name = "btnSEmail";
            this.btnSEmail.Size = new System.Drawing.Size(81, 22);
            this.btnSEmail.TabIndex = 162;
            this.btnSEmail.Text = "Tìm Kiếm";
            this.btnSEmail.Click += new System.EventHandler(this.btnSEmail_Click);
            // 
            // tabItem36
            // 
            this.tabItem36.AttachedControl = this.tabControlPanel40;
            this.tabItem36.Name = "tabItem36";
            this.tabItem36.Text = "Email";
            // 
            // tabControlPanel38
            // 
            this.tabControlPanel38.Controls.Add(this.panel44);
            this.tabControlPanel38.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tabControlPanel38.Location = new System.Drawing.Point(0, 29);
            this.tabControlPanel38.Name = "tabControlPanel38";
            this.tabControlPanel38.Padding = new System.Windows.Forms.Padding(1);
            this.tabControlPanel38.Size = new System.Drawing.Size(800, 33);
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
            this.panel44.Size = new System.Drawing.Size(798, 31);
            this.panel44.TabIndex = 0;
            // 
            // txtSCMND
            // 
            this.txtSCMND.Font = new System.Drawing.Font("Arial", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtSCMND.Location = new System.Drawing.Point(3, 3);
            this.txtSCMND.Name = "txtSCMND";
            this.txtSCMND.Size = new System.Drawing.Size(202, 22);
            this.txtSCMND.TabIndex = 161;
            // 
            // btnSCMND
            // 
            this.btnSCMND.AccessibleRole = System.Windows.Forms.AccessibleRole.PushButton;
            this.btnSCMND.Font = new System.Drawing.Font("Arial", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnSCMND.Location = new System.Drawing.Point(211, 3);
            this.btnSCMND.Name = "btnSCMND";
            this.btnSCMND.Size = new System.Drawing.Size(81, 22);
            this.btnSCMND.TabIndex = 162;
            this.btnSCMND.Text = "Tìm Kiếm";
            this.btnSCMND.Click += new System.EventHandler(this.btnSCMND_Click);
            // 
            // tabItem34
            // 
            this.tabItem34.AttachedControl = this.tabControlPanel38;
            this.tabItem34.Name = "tabItem34";
            this.tabItem34.Text = "CMND";
            // 
            // txtNoidung
            // 
            // 
            // 
            // 
            this.txtNoidung.Border.Class = "TextBoxBorder";
            this.txtNoidung.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtNoidung.Location = new System.Drawing.Point(104, 33);
            this.txtNoidung.MaxLength = 0;
            this.txtNoidung.Multiline = true;
            this.txtNoidung.Name = "txtNoidung";
            this.txtNoidung.ScrollBars = System.Windows.Forms.ScrollBars.Both;
            this.txtNoidung.Size = new System.Drawing.Size(401, 74);
            this.txtNoidung.TabIndex = 319;
            // 
            // btnSelectall
            // 
            this.btnSelectall.AccessibleRole = System.Windows.Forms.AccessibleRole.PushButton;
            this.btnSelectall.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.btnSelectall.Font = new System.Drawing.Font("Arial", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnSelectall.Location = new System.Drawing.Point(716, 457);
            this.btnSelectall.Name = "btnSelectall";
            this.btnSelectall.Size = new System.Drawing.Size(83, 27);
            this.btnSelectall.TabIndex = 333;
            this.btnSelectall.Text = "Chọn hết";
            this.btnSelectall.Click += new System.EventHandler(this.btnSelectall_Click);
            // 
            // panel1
            // 
            this.panel1.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.panel1.Controls.Add(this.txtChude);
            this.panel1.Controls.Add(this.txtNoidung);
            this.panel1.Controls.Add(this.labelX1);
            this.panel1.Controls.Add(this.labelX2);
            this.panel1.Controls.Add(this.groupBox10);
            this.panel1.Location = new System.Drawing.Point(6, 45);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(800, 112);
            this.panel1.TabIndex = 335;
            // 
            // txtChude
            // 
            this.txtChude.Location = new System.Drawing.Point(104, 3);
            this.txtChude.Name = "txtChude";
            this.txtChude.Size = new System.Drawing.Size(401, 26);
            this.txtChude.TabIndex = 323;
            // 
            // labelX1
            // 
            this.labelX1.Font = new System.Drawing.Font("Arial", 14.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.labelX1.Location = new System.Drawing.Point(31, 3);
            this.labelX1.Name = "labelX1";
            this.labelX1.Size = new System.Drawing.Size(63, 24);
            this.labelX1.TabIndex = 320;
            this.labelX1.Text = "Chủ đề";
            // 
            // frmMK_Email
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(9F, 18F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(812, 490);
            this.Controls.Add(this.btnDeselectall);
            this.Controls.Add(this.btnSave);
            this.Controls.Add(this.labelX96);
            this.Controls.Add(this.dgvDanhsach);
            this.Controls.Add(this.tabControl8);
            this.Controls.Add(this.btnSelectall);
            this.Controls.Add(this.panel1);
            this.Font = new System.Drawing.Font("Arial", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.Margin = new System.Windows.Forms.Padding(4);
            this.Name = "frmMK_Email";
            this.Text = "Gui email";
            this.Load += new System.EventHandler(this.frmMK_Email_Load);
            this.panel43.ResumeLayout(false);
            this.panel43.PerformLayout();
            this.tabControlPanel37.ResumeLayout(false);
            this.tabControlPanel35.ResumeLayout(false);
            this.panel41.ResumeLayout(false);
            this.panel41.PerformLayout();
            this.groupBox10.ResumeLayout(false);
            this.groupBox10.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dgvDanhsach)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.tabControl8)).EndInit();
            this.tabControl8.ResumeLayout(false);
            this.tabControlPanel39.ResumeLayout(false);
            this.panel47.ResumeLayout(false);
            this.tabControlPanel40.ResumeLayout(false);
            this.panel46.ResumeLayout(false);
            this.panel46.PerformLayout();
            this.tabControlPanel38.ResumeLayout(false);
            this.panel44.ResumeLayout(false);
            this.panel44.PerformLayout();
            this.panel1.ResumeLayout(false);
            this.panel1.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Panel panel43;
        private System.Windows.Forms.TextBox txtSMaKH;
        private DevComponents.DotNetBar.ButtonX btnSMaKH;
        private DevComponents.DotNetBar.Controls.ComboBoxEx cbbSNhomKH;
        private DevComponents.DotNetBar.TabControlPanel tabControlPanel37;
        private DevComponents.DotNetBar.TabItem tabItem33;
        private DevComponents.DotNetBar.TabControlPanel tabControlPanel35;
        private System.Windows.Forms.Panel panel41;
        private System.Windows.Forms.TextBox txtSTenKH;
        private DevComponents.DotNetBar.ButtonX btnSTenKH;
        private DevComponents.DotNetBar.TabItem tabItem31;
        private DevComponents.DotNetBar.ButtonX btnDeselectall;
        private DevComponents.DotNetBar.ButtonX btnSave;
        private System.Windows.Forms.RadioButton rdbTiemnang;
        private DevComponents.DotNetBar.LabelX labelX96;
        private System.Windows.Forms.GroupBox groupBox10;
        private System.Windows.Forms.RadioButton rdbHienhuu;
        private DevComponents.DotNetBar.Controls.DataGridViewX dgvDanhsach;
        private DevComponents.DotNetBar.LabelX labelX2;
        private DevComponents.DotNetBar.TabControl tabControl8;
        private DevComponents.DotNetBar.TabControlPanel tabControlPanel39;
        private System.Windows.Forms.Panel panel47;
        private DevComponents.DotNetBar.ButtonX btnSNhomKH;
        private DevComponents.DotNetBar.TabItem tabItem35;
        private DevComponents.DotNetBar.TabControlPanel tabControlPanel40;
        private System.Windows.Forms.Panel panel46;
        private System.Windows.Forms.TextBox txtSEmail;
        private DevComponents.DotNetBar.ButtonX btnSEmail;
        private DevComponents.DotNetBar.TabItem tabItem36;
        private DevComponents.DotNetBar.TabControlPanel tabControlPanel38;
        private System.Windows.Forms.Panel panel44;
        private System.Windows.Forms.TextBox txtSCMND;
        private DevComponents.DotNetBar.ButtonX btnSCMND;
        private DevComponents.DotNetBar.TabItem tabItem34;
        private DevComponents.DotNetBar.Controls.TextBoxX txtNoidung;
        private DevComponents.DotNetBar.ButtonX btnSelectall;
        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.TextBox txtChude;
        private DevComponents.DotNetBar.LabelX labelX1;


    }
}