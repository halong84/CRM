namespace CRM
{
    partial class frmUser
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
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle2 = new System.Windows.Forms.DataGridViewCellStyle();
            this.labelX1 = new DevComponents.DotNetBar.LabelX();
            this.labelX2 = new DevComponents.DotNetBar.LabelX();
            this.labelX3 = new DevComponents.DotNetBar.LabelX();
            this.txtUser = new DevComponents.DotNetBar.Controls.TextBoxX();
            this.txtPass = new DevComponents.DotNetBar.Controls.TextBoxX();
            this.txtHoten = new DevComponents.DotNetBar.Controls.TextBoxX();
            this.btnModify = new DevComponents.DotNetBar.ButtonX();
            this.btnAdd = new DevComponents.DotNetBar.ButtonX();
            this.dgvDanhsach = new DevComponents.DotNetBar.Controls.DataGridViewX();
            this.labelX6 = new DevComponents.DotNetBar.LabelX();
            this.txtGhichu = new DevComponents.DotNetBar.Controls.TextBoxX();
            this.btnDel = new DevComponents.DotNetBar.ButtonX();
            this.labelX4 = new DevComponents.DotNetBar.LabelX();
            this.btnClose = new DevComponents.DotNetBar.ButtonX();
            this.labelX5 = new DevComponents.DotNetBar.LabelX();
            this.cbbMaCN = new DevComponents.DotNetBar.Controls.ComboBoxEx();
            this.btnBackAll = new DevComponents.DotNetBar.ButtonX();
            this.btnBack = new DevComponents.DotNetBar.ButtonX();
            this.btnToAll = new DevComponents.DotNetBar.ButtonX();
            this.labelX7 = new DevComponents.DotNetBar.LabelX();
            this.btnTo = new DevComponents.DotNetBar.ButtonX();
            this.lstTo = new System.Windows.Forms.ListBox();
            this.lstFrom = new System.Windows.Forms.ListBox();
            this.labelX19 = new DevComponents.DotNetBar.LabelX();
            this.tctSearch = new DevComponents.DotNetBar.TabControl();
            this.tabControlPanel2 = new DevComponents.DotNetBar.TabControlPanel();
            this.panel2 = new System.Windows.Forms.Panel();
            this.btnSTenDN = new DevComponents.DotNetBar.ButtonX();
            this.txtSTenDN = new System.Windows.Forms.TextBox();
            this.titTenDN = new DevComponents.DotNetBar.TabItem(this.components);
            this.tabControlPanel1 = new DevComponents.DotNetBar.TabControlPanel();
            this.panel1 = new System.Windows.Forms.Panel();
            this.btnSTenND = new DevComponents.DotNetBar.ButtonX();
            this.txtSTenND = new System.Windows.Forms.TextBox();
            this.panelEx1 = new DevComponents.DotNetBar.PanelEx();
            this.titTenND = new DevComponents.DotNetBar.TabItem(this.components);
            this.labelX8 = new DevComponents.DotNetBar.LabelX();
            this.cbbPhong = new DevComponents.DotNetBar.Controls.ComboBoxEx();
            this.cbbChucvu = new DevComponents.DotNetBar.Controls.ComboBoxEx();
            this.labelX9 = new DevComponents.DotNetBar.LabelX();
            ((System.ComponentModel.ISupportInitialize)(this.dgvDanhsach)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.tctSearch)).BeginInit();
            this.tctSearch.SuspendLayout();
            this.tabControlPanel2.SuspendLayout();
            this.panel2.SuspendLayout();
            this.tabControlPanel1.SuspendLayout();
            this.panel1.SuspendLayout();
            this.SuspendLayout();
            // 
            // labelX1
            // 
            this.labelX1.ForeColor = System.Drawing.SystemColors.ControlText;
            this.labelX1.Location = new System.Drawing.Point(9, 103);
            this.labelX1.Margin = new System.Windows.Forms.Padding(4);
            this.labelX1.Name = "labelX1";
            this.labelX1.Size = new System.Drawing.Size(133, 24);
            this.labelX1.TabIndex = 0;
            this.labelX1.Text = "Tên đăng nhập (<font color=\"#ED1C24\">*</font>)";
            // 
            // labelX2
            // 
            this.labelX2.ForeColor = System.Drawing.SystemColors.ControlText;
            this.labelX2.Location = new System.Drawing.Point(51, 135);
            this.labelX2.Name = "labelX2";
            this.labelX2.Size = new System.Drawing.Size(91, 23);
            this.labelX2.TabIndex = 1;
            this.labelX2.Text = "Mật khẩu (<font color=\"#ED1C24\">*</font>)";
            // 
            // labelX3
            // 
            this.labelX3.Location = new System.Drawing.Point(94, 166);
            this.labelX3.Name = "labelX3";
            this.labelX3.Size = new System.Drawing.Size(48, 23);
            this.labelX3.TabIndex = 2;
            this.labelX3.Text = "Họ tên";
            // 
            // txtUser
            // 
            // 
            // 
            // 
            this.txtUser.Border.Class = "TextBoxBorder";
            this.txtUser.Location = new System.Drawing.Point(148, 101);
            this.txtUser.Name = "txtUser";
            this.txtUser.Size = new System.Drawing.Size(251, 26);
            this.txtUser.TabIndex = 4;
            this.txtUser.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.txtUser_KeyPress);
            // 
            // txtPass
            // 
            // 
            // 
            // 
            this.txtPass.Border.Class = "TextBoxBorder";
            this.txtPass.Font = new System.Drawing.Font("Arial", 20F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtPass.Location = new System.Drawing.Point(148, 132);
            this.txtPass.Name = "txtPass";
            this.txtPass.PasswordChar = '*';
            this.txtPass.Size = new System.Drawing.Size(251, 26);
            this.txtPass.TabIndex = 5;
            // 
            // txtHoten
            // 
            // 
            // 
            // 
            this.txtHoten.Border.Class = "TextBoxBorder";
            this.txtHoten.Location = new System.Drawing.Point(148, 163);
            this.txtHoten.Name = "txtHoten";
            this.txtHoten.Size = new System.Drawing.Size(251, 26);
            this.txtHoten.TabIndex = 8;
            // 
            // btnModify
            // 
            this.btnModify.AccessibleRole = System.Windows.Forms.AccessibleRole.PushButton;
            this.btnModify.Location = new System.Drawing.Point(619, 229);
            this.btnModify.Name = "btnModify";
            this.btnModify.Size = new System.Drawing.Size(75, 23);
            this.btnModify.TabIndex = 10;
            this.btnModify.Text = "Thay đổi";
            this.btnModify.Click += new System.EventHandler(this.btnModify_Click);
            // 
            // btnAdd
            // 
            this.btnAdd.AccessibleRole = System.Windows.Forms.AccessibleRole.PushButton;
            this.btnAdd.Location = new System.Drawing.Point(700, 229);
            this.btnAdd.Name = "btnAdd";
            this.btnAdd.Size = new System.Drawing.Size(75, 23);
            this.btnAdd.TabIndex = 11;
            this.btnAdd.Text = "Thêm";
            this.btnAdd.Click += new System.EventHandler(this.btnAdd_Click);
            // 
            // dgvDanhsach
            // 
            this.dgvDanhsach.BackgroundColor = System.Drawing.SystemColors.Control;
            this.dgvDanhsach.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            dataGridViewCellStyle2.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle2.BackColor = System.Drawing.SystemColors.Window;
            dataGridViewCellStyle2.Font = new System.Drawing.Font("Arial", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            dataGridViewCellStyle2.ForeColor = System.Drawing.SystemColors.ControlText;
            dataGridViewCellStyle2.SelectionBackColor = System.Drawing.SystemColors.Highlight;
            dataGridViewCellStyle2.SelectionForeColor = System.Drawing.SystemColors.ControlText;
            dataGridViewCellStyle2.WrapMode = System.Windows.Forms.DataGridViewTriState.False;
            this.dgvDanhsach.DefaultCellStyle = dataGridViewCellStyle2;
            this.dgvDanhsach.GridColor = System.Drawing.Color.FromArgb(((int)(((byte)(208)))), ((int)(((byte)(215)))), ((int)(((byte)(229)))));
            this.dgvDanhsach.Location = new System.Drawing.Point(9, 350);
            this.dgvDanhsach.Name = "dgvDanhsach";
            this.dgvDanhsach.Size = new System.Drawing.Size(801, 180);
            this.dgvDanhsach.TabIndex = 12;
            this.dgvDanhsach.CellClick += new System.Windows.Forms.DataGridViewCellEventHandler(this.dgvDanhsach_CellClick);
            // 
            // labelX6
            // 
            this.labelX6.ForeColor = System.Drawing.SystemColors.ControlText;
            this.labelX6.Location = new System.Drawing.Point(411, 166);
            this.labelX6.Name = "labelX6";
            this.labelX6.Size = new System.Drawing.Size(56, 23);
            this.labelX6.TabIndex = 13;
            this.labelX6.Text = "Ghi chú";
            // 
            // txtGhichu
            // 
            // 
            // 
            // 
            this.txtGhichu.Border.Class = "TextBoxBorder";
            this.txtGhichu.Location = new System.Drawing.Point(473, 163);
            this.txtGhichu.Multiline = true;
            this.txtGhichu.Name = "txtGhichu";
            this.txtGhichu.Size = new System.Drawing.Size(337, 55);
            this.txtGhichu.TabIndex = 14;
            // 
            // btnDel
            // 
            this.btnDel.AccessibleRole = System.Windows.Forms.AccessibleRole.PushButton;
            this.btnDel.Location = new System.Drawing.Point(619, 536);
            this.btnDel.Name = "btnDel";
            this.btnDel.Size = new System.Drawing.Size(75, 23);
            this.btnDel.TabIndex = 15;
            this.btnDel.Text = "Xóa";
            this.btnDel.Click += new System.EventHandler(this.btnDel_Click);
            // 
            // labelX4
            // 
            this.labelX4.Font = new System.Drawing.Font("Arial", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.labelX4.Location = new System.Drawing.Point(303, 12);
            this.labelX4.Name = "labelX4";
            this.labelX4.Size = new System.Drawing.Size(213, 23);
            this.labelX4.TabIndex = 16;
            this.labelX4.Text = "QUẢN LÝ NGƯỜI SỬ DỤNG";
            // 
            // btnClose
            // 
            this.btnClose.AccessibleRole = System.Windows.Forms.AccessibleRole.PushButton;
            this.btnClose.Location = new System.Drawing.Point(700, 536);
            this.btnClose.Name = "btnClose";
            this.btnClose.Size = new System.Drawing.Size(75, 23);
            this.btnClose.TabIndex = 17;
            this.btnClose.Text = "Đóng";
            this.btnClose.Click += new System.EventHandler(this.btnClose_Click);
            // 
            // labelX5
            // 
            this.labelX5.Location = new System.Drawing.Point(67, 41);
            this.labelX5.Name = "labelX5";
            this.labelX5.Size = new System.Drawing.Size(75, 25);
            this.labelX5.TabIndex = 18;
            this.labelX5.Text = "Chi nhánh";
            // 
            // cbbMaCN
            // 
            this.cbbMaCN.DisplayMember = "Text";
            this.cbbMaCN.DrawMode = System.Windows.Forms.DrawMode.OwnerDrawFixed;
            this.cbbMaCN.FormattingEnabled = true;
            this.cbbMaCN.ItemHeight = 19;
            this.cbbMaCN.Location = new System.Drawing.Point(148, 41);
            this.cbbMaCN.Name = "cbbMaCN";
            this.cbbMaCN.Size = new System.Drawing.Size(251, 25);
            this.cbbMaCN.TabIndex = 20;
            this.cbbMaCN.SelectionChangeCommitted += new System.EventHandler(this.cbbMaCN_SelectionChangeCommitted);
            this.cbbMaCN.SelectedIndexChanged += new System.EventHandler(this.cbbMaCN_SelectedIndexChanged);
            // 
            // btnBackAll
            // 
            this.btnBackAll.AccessibleRole = System.Windows.Forms.AccessibleRole.PushButton;
            this.btnBackAll.Location = new System.Drawing.Point(628, 128);
            this.btnBackAll.Name = "btnBackAll";
            this.btnBackAll.Size = new System.Drawing.Size(27, 23);
            this.btnBackAll.TabIndex = 35;
            this.btnBackAll.Text = "<<";
            this.btnBackAll.Click += new System.EventHandler(this.btnBackAll_Click);
            // 
            // btnBack
            // 
            this.btnBack.AccessibleRole = System.Windows.Forms.AccessibleRole.PushButton;
            this.btnBack.Location = new System.Drawing.Point(628, 99);
            this.btnBack.Name = "btnBack";
            this.btnBack.Size = new System.Drawing.Size(27, 23);
            this.btnBack.TabIndex = 36;
            this.btnBack.Text = "<";
            this.btnBack.Click += new System.EventHandler(this.btnBack_Click);
            // 
            // btnToAll
            // 
            this.btnToAll.AccessibleRole = System.Windows.Forms.AccessibleRole.PushButton;
            this.btnToAll.Location = new System.Drawing.Point(628, 70);
            this.btnToAll.Name = "btnToAll";
            this.btnToAll.Size = new System.Drawing.Size(27, 23);
            this.btnToAll.TabIndex = 34;
            this.btnToAll.Text = ">>";
            this.btnToAll.Click += new System.EventHandler(this.btnToAll_Click);
            // 
            // labelX7
            // 
            this.labelX7.Location = new System.Drawing.Point(411, 41);
            this.labelX7.Name = "labelX7";
            this.labelX7.Size = new System.Drawing.Size(59, 64);
            this.labelX7.TabIndex = 33;
            this.labelX7.Text = "Nhóm\r\nngười\r\ndùng";
            // 
            // btnTo
            // 
            this.btnTo.AccessibleRole = System.Windows.Forms.AccessibleRole.PushButton;
            this.btnTo.Location = new System.Drawing.Point(628, 41);
            this.btnTo.Name = "btnTo";
            this.btnTo.Size = new System.Drawing.Size(27, 23);
            this.btnTo.TabIndex = 32;
            this.btnTo.Text = ">";
            this.btnTo.Click += new System.EventHandler(this.btnTo_Click);
            // 
            // lstTo
            // 
            this.lstTo.FormattingEnabled = true;
            this.lstTo.ItemHeight = 18;
            this.lstTo.Location = new System.Drawing.Point(661, 41);
            this.lstTo.Name = "lstTo";
            this.lstTo.SelectionMode = System.Windows.Forms.SelectionMode.MultiSimple;
            this.lstTo.Size = new System.Drawing.Size(149, 112);
            this.lstTo.TabIndex = 31;
            // 
            // lstFrom
            // 
            this.lstFrom.FormattingEnabled = true;
            this.lstFrom.ItemHeight = 18;
            this.lstFrom.Location = new System.Drawing.Point(473, 41);
            this.lstFrom.Name = "lstFrom";
            this.lstFrom.SelectionMode = System.Windows.Forms.SelectionMode.MultiSimple;
            this.lstFrom.Size = new System.Drawing.Size(149, 112);
            this.lstFrom.TabIndex = 30;
            // 
            // labelX19
            // 
            this.labelX19.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.labelX19.BackColor = System.Drawing.SystemColors.ActiveCaption;
            this.labelX19.Font = new System.Drawing.Font("Arial", 15.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.labelX19.ForeColor = System.Drawing.SystemColors.ActiveCaptionText;
            this.labelX19.Location = new System.Drawing.Point(9, 258);
            this.labelX19.Name = "labelX19";
            this.labelX19.SingleLineColor = System.Drawing.SystemColors.ActiveCaption;
            this.labelX19.Size = new System.Drawing.Size(801, 30);
            this.labelX19.TabIndex = 214;
            this.labelX19.Text = "DANH SÁCH NGƯỜI DÙNG";
            this.labelX19.TextAlignment = System.Drawing.StringAlignment.Center;
            // 
            // tctSearch
            // 
            this.tctSearch.CanReorderTabs = true;
            this.tctSearch.Controls.Add(this.tabControlPanel2);
            this.tctSearch.Controls.Add(this.tabControlPanel1);
            this.tctSearch.Location = new System.Drawing.Point(9, 288);
            this.tctSearch.Name = "tctSearch";
            this.tctSearch.SelectedTabFont = new System.Drawing.Font("Arial", 12F, System.Drawing.FontStyle.Bold);
            this.tctSearch.SelectedTabIndex = 0;
            this.tctSearch.Size = new System.Drawing.Size(801, 60);
            this.tctSearch.TabIndex = 215;
            this.tctSearch.TabLayoutType = DevComponents.DotNetBar.eTabLayoutType.FixedWithNavigationBox;
            this.tctSearch.Tabs.Add(this.titTenDN);
            this.tctSearch.Tabs.Add(this.titTenND);
            this.tctSearch.Text = "tabControl2";
            // 
            // tabControlPanel2
            // 
            this.tabControlPanel2.Controls.Add(this.panel2);
            this.tabControlPanel2.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tabControlPanel2.Location = new System.Drawing.Point(0, 29);
            this.tabControlPanel2.Name = "tabControlPanel2";
            this.tabControlPanel2.Padding = new System.Windows.Forms.Padding(1);
            this.tabControlPanel2.Size = new System.Drawing.Size(801, 31);
            this.tabControlPanel2.Style.BackColor1.Color = System.Drawing.Color.FromArgb(((int)(((byte)(142)))), ((int)(((byte)(179)))), ((int)(((byte)(231)))));
            this.tabControlPanel2.Style.BackColor2.Color = System.Drawing.Color.FromArgb(((int)(((byte)(223)))), ((int)(((byte)(237)))), ((int)(((byte)(254)))));
            this.tabControlPanel2.Style.Border = DevComponents.DotNetBar.eBorderType.SingleLine;
            this.tabControlPanel2.Style.BorderColor.Color = System.Drawing.Color.FromArgb(((int)(((byte)(59)))), ((int)(((byte)(97)))), ((int)(((byte)(156)))));
            this.tabControlPanel2.Style.BorderSide = ((DevComponents.DotNetBar.eBorderSide)(((DevComponents.DotNetBar.eBorderSide.Left | DevComponents.DotNetBar.eBorderSide.Right)
                        | DevComponents.DotNetBar.eBorderSide.Bottom)));
            this.tabControlPanel2.Style.GradientAngle = 90;
            this.tabControlPanel2.TabIndex = 2;
            this.tabControlPanel2.TabItem = this.titTenDN;
            // 
            // panel2
            // 
            this.panel2.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.panel2.Controls.Add(this.btnSTenDN);
            this.panel2.Controls.Add(this.txtSTenDN);
            this.panel2.Location = new System.Drawing.Point(0, 0);
            this.panel2.Name = "panel2";
            this.panel2.Size = new System.Drawing.Size(801, 31);
            this.panel2.TabIndex = 209;
            // 
            // btnSTenDN
            // 
            this.btnSTenDN.AccessibleRole = System.Windows.Forms.AccessibleRole.PushButton;
            this.btnSTenDN.Location = new System.Drawing.Point(229, 4);
            this.btnSTenDN.Name = "btnSTenDN";
            this.btnSTenDN.Size = new System.Drawing.Size(75, 22);
            this.btnSTenDN.TabIndex = 1;
            this.btnSTenDN.Text = "Tìm kiếm";
            this.btnSTenDN.Click += new System.EventHandler(this.btnSTenDN_Click);
            // 
            // txtSTenDN
            // 
            this.txtSTenDN.Location = new System.Drawing.Point(3, 2);
            this.txtSTenDN.Name = "txtSTenDN";
            this.txtSTenDN.Size = new System.Drawing.Size(211, 26);
            this.txtSTenDN.TabIndex = 0;
            // 
            // titTenDN
            // 
            this.titTenDN.AttachedControl = this.tabControlPanel2;
            this.titTenDN.Name = "titTenDN";
            this.titTenDN.Text = "Tên đăng nhập";
            // 
            // tabControlPanel1
            // 
            this.tabControlPanel1.Controls.Add(this.panel1);
            this.tabControlPanel1.Controls.Add(this.panelEx1);
            this.tabControlPanel1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tabControlPanel1.Location = new System.Drawing.Point(0, 29);
            this.tabControlPanel1.Name = "tabControlPanel1";
            this.tabControlPanel1.Padding = new System.Windows.Forms.Padding(1);
            this.tabControlPanel1.Size = new System.Drawing.Size(801, 31);
            this.tabControlPanel1.Style.BackColor1.Color = System.Drawing.Color.FromArgb(((int)(((byte)(142)))), ((int)(((byte)(179)))), ((int)(((byte)(231)))));
            this.tabControlPanel1.Style.BackColor2.Color = System.Drawing.Color.FromArgb(((int)(((byte)(223)))), ((int)(((byte)(237)))), ((int)(((byte)(254)))));
            this.tabControlPanel1.Style.Border = DevComponents.DotNetBar.eBorderType.SingleLine;
            this.tabControlPanel1.Style.BorderColor.Color = System.Drawing.Color.FromArgb(((int)(((byte)(59)))), ((int)(((byte)(97)))), ((int)(((byte)(156)))));
            this.tabControlPanel1.Style.BorderSide = ((DevComponents.DotNetBar.eBorderSide)(((DevComponents.DotNetBar.eBorderSide.Left | DevComponents.DotNetBar.eBorderSide.Right)
                        | DevComponents.DotNetBar.eBorderSide.Bottom)));
            this.tabControlPanel1.Style.GradientAngle = 90;
            this.tabControlPanel1.TabIndex = 1;
            this.tabControlPanel1.TabItem = this.titTenND;
            // 
            // panel1
            // 
            this.panel1.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.panel1.Controls.Add(this.btnSTenND);
            this.panel1.Controls.Add(this.txtSTenND);
            this.panel1.Location = new System.Drawing.Point(0, 0);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(801, 31);
            this.panel1.TabIndex = 1;
            // 
            // btnSTenND
            // 
            this.btnSTenND.AccessibleRole = System.Windows.Forms.AccessibleRole.PushButton;
            this.btnSTenND.Location = new System.Drawing.Point(229, 4);
            this.btnSTenND.Name = "btnSTenND";
            this.btnSTenND.Size = new System.Drawing.Size(75, 22);
            this.btnSTenND.TabIndex = 1;
            this.btnSTenND.Text = "Tìm kiếm";
            this.btnSTenND.Click += new System.EventHandler(this.btnSTenND_Click);
            // 
            // txtSTenND
            // 
            this.txtSTenND.Location = new System.Drawing.Point(3, 2);
            this.txtSTenND.Name = "txtSTenND";
            this.txtSTenND.Size = new System.Drawing.Size(211, 26);
            this.txtSTenND.TabIndex = 0;
            // 
            // panelEx1
            // 
            this.panelEx1.CanvasColor = System.Drawing.SystemColors.Control;
            this.panelEx1.Location = new System.Drawing.Point(42, 89);
            this.panelEx1.Name = "panelEx1";
            this.panelEx1.Size = new System.Drawing.Size(201, 27);
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
            // titTenND
            // 
            this.titTenND.AttachedControl = this.tabControlPanel1;
            this.titTenND.Name = "titTenND";
            this.titTenND.Text = "Tên người dùng";
            // 
            // labelX8
            // 
            this.labelX8.Font = new System.Drawing.Font("Arial", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.labelX8.Location = new System.Drawing.Point(64, 71);
            this.labelX8.Margin = new System.Windows.Forms.Padding(4);
            this.labelX8.Name = "labelX8";
            this.labelX8.Size = new System.Drawing.Size(78, 25);
            this.labelX8.TabIndex = 216;
            this.labelX8.Text = "Phòng ban";
            // 
            // cbbPhong
            // 
            this.cbbPhong.DisplayMember = "Text";
            this.cbbPhong.DrawMode = System.Windows.Forms.DrawMode.OwnerDrawFixed;
            this.cbbPhong.FormattingEnabled = true;
            this.cbbPhong.ItemHeight = 19;
            this.cbbPhong.Location = new System.Drawing.Point(148, 71);
            this.cbbPhong.Name = "cbbPhong";
            this.cbbPhong.Size = new System.Drawing.Size(251, 25);
            this.cbbPhong.TabIndex = 217;
            this.cbbPhong.SelectionChangeCommitted += new System.EventHandler(this.cbbPhong_SelectionChangeCommitted);
            this.cbbPhong.SelectedIndexChanged += new System.EventHandler(this.cbbPhong_SelectedIndexChanged);
            // 
            // cbbChucvu
            // 
            this.cbbChucvu.DisplayMember = "Text";
            this.cbbChucvu.DrawMode = System.Windows.Forms.DrawMode.OwnerDrawFixed;
            this.cbbChucvu.FormattingEnabled = true;
            this.cbbChucvu.ItemHeight = 19;
            this.cbbChucvu.Location = new System.Drawing.Point(148, 193);
            this.cbbChucvu.Name = "cbbChucvu";
            this.cbbChucvu.Size = new System.Drawing.Size(251, 25);
            this.cbbChucvu.TabIndex = 219;
            // 
            // labelX9
            // 
            this.labelX9.Location = new System.Drawing.Point(77, 197);
            this.labelX9.Name = "labelX9";
            this.labelX9.Size = new System.Drawing.Size(65, 21);
            this.labelX9.TabIndex = 218;
            this.labelX9.Text = "Chức vụ";
            this.labelX9.TextAlignment = System.Drawing.StringAlignment.Far;
            // 
            // frmUser
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(9F, 18F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(819, 571);
            this.Controls.Add(this.cbbChucvu);
            this.Controls.Add(this.labelX9);
            this.Controls.Add(this.labelX8);
            this.Controls.Add(this.cbbPhong);
            this.Controls.Add(this.tctSearch);
            this.Controls.Add(this.labelX19);
            this.Controls.Add(this.btnBackAll);
            this.Controls.Add(this.btnBack);
            this.Controls.Add(this.btnToAll);
            this.Controls.Add(this.labelX7);
            this.Controls.Add(this.btnTo);
            this.Controls.Add(this.lstTo);
            this.Controls.Add(this.lstFrom);
            this.Controls.Add(this.cbbMaCN);
            this.Controls.Add(this.labelX5);
            this.Controls.Add(this.btnClose);
            this.Controls.Add(this.labelX4);
            this.Controls.Add(this.btnDel);
            this.Controls.Add(this.txtGhichu);
            this.Controls.Add(this.labelX6);
            this.Controls.Add(this.dgvDanhsach);
            this.Controls.Add(this.btnAdd);
            this.Controls.Add(this.btnModify);
            this.Controls.Add(this.txtHoten);
            this.Controls.Add(this.txtPass);
            this.Controls.Add(this.txtUser);
            this.Controls.Add(this.labelX3);
            this.Controls.Add(this.labelX2);
            this.Controls.Add(this.labelX1);
            this.Font = new System.Drawing.Font("Arial", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.Margin = new System.Windows.Forms.Padding(4);
            this.Name = "frmUser";
            this.Text = "Thong tin nguoi su dung";
            this.Load += new System.EventHandler(this.frmUser_Load);
            ((System.ComponentModel.ISupportInitialize)(this.dgvDanhsach)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.tctSearch)).EndInit();
            this.tctSearch.ResumeLayout(false);
            this.tabControlPanel2.ResumeLayout(false);
            this.panel2.ResumeLayout(false);
            this.panel2.PerformLayout();
            this.tabControlPanel1.ResumeLayout(false);
            this.panel1.ResumeLayout(false);
            this.panel1.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private DevComponents.DotNetBar.LabelX labelX1;
        private DevComponents.DotNetBar.LabelX labelX2;
        private DevComponents.DotNetBar.LabelX labelX3;
        private DevComponents.DotNetBar.Controls.TextBoxX txtUser;
        private DevComponents.DotNetBar.Controls.TextBoxX txtPass;
        private DevComponents.DotNetBar.Controls.TextBoxX txtHoten;
        private DevComponents.DotNetBar.ButtonX btnModify;
        private DevComponents.DotNetBar.ButtonX btnAdd;
        private DevComponents.DotNetBar.Controls.DataGridViewX dgvDanhsach;
        private DevComponents.DotNetBar.LabelX labelX6;
        private DevComponents.DotNetBar.Controls.TextBoxX txtGhichu;
        private DevComponents.DotNetBar.ButtonX btnDel;
        private DevComponents.DotNetBar.LabelX labelX4;
        private DevComponents.DotNetBar.ButtonX btnClose;
        private DevComponents.DotNetBar.LabelX labelX5;
        private DevComponents.DotNetBar.Controls.ComboBoxEx cbbMaCN;
        private DevComponents.DotNetBar.ButtonX btnBackAll;
        private DevComponents.DotNetBar.ButtonX btnBack;
        private DevComponents.DotNetBar.ButtonX btnToAll;
        private DevComponents.DotNetBar.LabelX labelX7;
        private DevComponents.DotNetBar.ButtonX btnTo;
        private System.Windows.Forms.ListBox lstTo;
        private System.Windows.Forms.ListBox lstFrom;
        private DevComponents.DotNetBar.LabelX labelX19;
        private DevComponents.DotNetBar.TabControl tctSearch;
        private DevComponents.DotNetBar.TabControlPanel tabControlPanel1;
        private System.Windows.Forms.Panel panel1;
        private DevComponents.DotNetBar.ButtonX btnSTenND;
        private System.Windows.Forms.TextBox txtSTenND;
        private DevComponents.DotNetBar.PanelEx panelEx1;
        private DevComponents.DotNetBar.TabItem titTenND;
        private DevComponents.DotNetBar.TabControlPanel tabControlPanel2;
        private System.Windows.Forms.Panel panel2;
        private DevComponents.DotNetBar.ButtonX btnSTenDN;
        private System.Windows.Forms.TextBox txtSTenDN;
        private DevComponents.DotNetBar.TabItem titTenDN;
        private DevComponents.DotNetBar.LabelX labelX8;
        private DevComponents.DotNetBar.Controls.ComboBoxEx cbbPhong;
        private DevComponents.DotNetBar.Controls.ComboBoxEx cbbChucvu;
        private DevComponents.DotNetBar.LabelX labelX9;
    }
}