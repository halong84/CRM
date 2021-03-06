namespace CRM
{
    partial class frmMenu
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
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle3 = new System.Windows.Forms.DataGridViewCellStyle();
            this.cbbMamenu = new DevComponents.DotNetBar.Controls.ComboBoxEx();
            this.btnDel = new DevComponents.DotNetBar.ButtonX();
            this.btnClose = new DevComponents.DotNetBar.ButtonX();
            this.dgvDanhsach = new DevComponents.DotNetBar.Controls.DataGridViewX();
            this.btnModify = new DevComponents.DotNetBar.ButtonX();
            this.btnAdd = new DevComponents.DotNetBar.ButtonX();
            this.txtTenmenu = new DevComponents.DotNetBar.Controls.TextBoxX();
            this.labelX2 = new DevComponents.DotNetBar.LabelX();
            this.labelX1 = new DevComponents.DotNetBar.LabelX();
            this.btnBackAll = new DevComponents.DotNetBar.ButtonX();
            this.btnBack = new DevComponents.DotNetBar.ButtonX();
            this.btnToAll = new DevComponents.DotNetBar.ButtonX();
            this.labelX7 = new DevComponents.DotNetBar.LabelX();
            this.btnTo = new DevComponents.DotNetBar.ButtonX();
            this.lstTo = new System.Windows.Forms.ListBox();
            this.lstFrom = new System.Windows.Forms.ListBox();
            this.labelX3 = new DevComponents.DotNetBar.LabelX();
            this.txtTenform = new DevComponents.DotNetBar.Controls.TextBoxX();
            this.labelX4 = new DevComponents.DotNetBar.LabelX();
            this.cbbParentID = new DevComponents.DotNetBar.Controls.ComboBoxEx();
            this.labelX5 = new DevComponents.DotNetBar.LabelX();
            this.txtDeep = new DevComponents.DotNetBar.Controls.TextBoxX();
            this.labelX6 = new DevComponents.DotNetBar.LabelX();
            this.txtPos = new DevComponents.DotNetBar.Controls.TextBoxX();
            this.labelX8 = new DevComponents.DotNetBar.LabelX();
            ((System.ComponentModel.ISupportInitialize)(this.dgvDanhsach)).BeginInit();
            this.SuspendLayout();
            // 
            // cbbMamenu
            // 
            this.cbbMamenu.DisplayMember = "Text";
            this.cbbMamenu.DrawMode = System.Windows.Forms.DrawMode.OwnerDrawFixed;
            this.cbbMamenu.FormattingEnabled = true;
            this.cbbMamenu.ItemHeight = 19;
            this.cbbMamenu.Location = new System.Drawing.Point(159, 53);
            this.cbbMamenu.Name = "cbbMamenu";
            this.cbbMamenu.Size = new System.Drawing.Size(149, 25);
            this.cbbMamenu.TabIndex = 12;
            this.cbbMamenu.SelectionChangeCommitted += new System.EventHandler(this.cbbMamenu_SelectionChangeCommitted);
            this.cbbMamenu.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.cbbMamenu_KeyPress);
            // 
            // btnDel
            // 
            this.btnDel.AccessibleRole = System.Windows.Forms.AccessibleRole.PushButton;
            this.btnDel.Location = new System.Drawing.Point(636, 536);
            this.btnDel.Name = "btnDel";
            this.btnDel.Size = new System.Drawing.Size(75, 23);
            this.btnDel.TabIndex = 20;
            this.btnDel.Text = "Xóa";
            this.btnDel.Click += new System.EventHandler(this.btnDel_Click);
            // 
            // btnClose
            // 
            this.btnClose.AccessibleRole = System.Windows.Forms.AccessibleRole.PushButton;
            this.btnClose.Location = new System.Drawing.Point(717, 536);
            this.btnClose.Name = "btnClose";
            this.btnClose.Size = new System.Drawing.Size(75, 23);
            this.btnClose.TabIndex = 19;
            this.btnClose.Text = "Đóng";
            this.btnClose.Click += new System.EventHandler(this.btnClose_Click);
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
            this.dgvDanhsach.Location = new System.Drawing.Point(12, 253);
            this.dgvDanhsach.Name = "dgvDanhsach";
            this.dgvDanhsach.Size = new System.Drawing.Size(795, 277);
            this.dgvDanhsach.TabIndex = 18;
            this.dgvDanhsach.CellClick += new System.Windows.Forms.DataGridViewCellEventHandler(this.dgvDanhsach_CellClick);
            // 
            // btnModify
            // 
            this.btnModify.AccessibleRole = System.Windows.Forms.AccessibleRole.PushButton;
            this.btnModify.Location = new System.Drawing.Point(636, 224);
            this.btnModify.Name = "btnModify";
            this.btnModify.Size = new System.Drawing.Size(75, 23);
            this.btnModify.TabIndex = 17;
            this.btnModify.Text = "Thay đổi";
            this.btnModify.Click += new System.EventHandler(this.btnModify_Click);
            // 
            // btnAdd
            // 
            this.btnAdd.AccessibleRole = System.Windows.Forms.AccessibleRole.PushButton;
            this.btnAdd.Location = new System.Drawing.Point(717, 224);
            this.btnAdd.Name = "btnAdd";
            this.btnAdd.Size = new System.Drawing.Size(75, 23);
            this.btnAdd.TabIndex = 16;
            this.btnAdd.Text = "Thêm";
            this.btnAdd.Click += new System.EventHandler(this.btnAdd_Click);
            // 
            // txtTenmenu
            // 
            // 
            // 
            // 
            this.txtTenmenu.Border.Class = "TextBoxBorder";
            this.txtTenmenu.Location = new System.Drawing.Point(448, 52);
            this.txtTenmenu.Name = "txtTenmenu";
            this.txtTenmenu.Size = new System.Drawing.Size(337, 26);
            this.txtTenmenu.TabIndex = 14;
            // 
            // labelX2
            // 
            this.labelX2.Location = new System.Drawing.Point(367, 55);
            this.labelX2.Name = "labelX2";
            this.labelX2.Size = new System.Drawing.Size(75, 23);
            this.labelX2.TabIndex = 13;
            this.labelX2.Text = "Menu_title";
            // 
            // labelX1
            // 
            this.labelX1.Location = new System.Drawing.Point(60, 55);
            this.labelX1.Name = "labelX1";
            this.labelX1.Size = new System.Drawing.Size(93, 23);
            this.labelX1.TabIndex = 11;
            this.labelX1.Text = "Menu_ID (<font color=\"#ED1C24\">*</font>)";
            // 
            // btnBackAll
            // 
            this.btnBackAll.AccessibleRole = System.Windows.Forms.AccessibleRole.PushButton;
            this.btnBackAll.Location = new System.Drawing.Point(603, 180);
            this.btnBackAll.Name = "btnBackAll";
            this.btnBackAll.Size = new System.Drawing.Size(27, 23);
            this.btnBackAll.TabIndex = 42;
            this.btnBackAll.Text = "<<";
            this.btnBackAll.Click += new System.EventHandler(this.btnBackAll_Click);
            // 
            // btnBack
            // 
            this.btnBack.AccessibleRole = System.Windows.Forms.AccessibleRole.PushButton;
            this.btnBack.Location = new System.Drawing.Point(603, 151);
            this.btnBack.Name = "btnBack";
            this.btnBack.Size = new System.Drawing.Size(27, 23);
            this.btnBack.TabIndex = 43;
            this.btnBack.Text = "<";
            this.btnBack.Click += new System.EventHandler(this.btnBack_Click);
            // 
            // btnToAll
            // 
            this.btnToAll.AccessibleRole = System.Windows.Forms.AccessibleRole.PushButton;
            this.btnToAll.Location = new System.Drawing.Point(603, 122);
            this.btnToAll.Name = "btnToAll";
            this.btnToAll.Size = new System.Drawing.Size(27, 23);
            this.btnToAll.TabIndex = 41;
            this.btnToAll.Text = ">>";
            this.btnToAll.Click += new System.EventHandler(this.btnToAll_Click);
            // 
            // labelX7
            // 
            this.labelX7.Location = new System.Drawing.Point(367, 83);
            this.labelX7.Name = "labelX7";
            this.labelX7.Size = new System.Drawing.Size(75, 26);
            this.labelX7.TabIndex = 40;
            this.labelX7.Text = "Group_list";
            // 
            // btnTo
            // 
            this.btnTo.AccessibleRole = System.Windows.Forms.AccessibleRole.PushButton;
            this.btnTo.Location = new System.Drawing.Point(603, 93);
            this.btnTo.Name = "btnTo";
            this.btnTo.Size = new System.Drawing.Size(27, 23);
            this.btnTo.TabIndex = 39;
            this.btnTo.Text = ">";
            this.btnTo.Click += new System.EventHandler(this.btnTo_Click);
            // 
            // lstTo
            // 
            this.lstTo.FormattingEnabled = true;
            this.lstTo.ItemHeight = 18;
            this.lstTo.Location = new System.Drawing.Point(636, 93);
            this.lstTo.Name = "lstTo";
            this.lstTo.SelectionMode = System.Windows.Forms.SelectionMode.MultiSimple;
            this.lstTo.Size = new System.Drawing.Size(149, 112);
            this.lstTo.TabIndex = 38;
            // 
            // lstFrom
            // 
            this.lstFrom.FormattingEnabled = true;
            this.lstFrom.ItemHeight = 18;
            this.lstFrom.Location = new System.Drawing.Point(448, 93);
            this.lstFrom.Name = "lstFrom";
            this.lstFrom.SelectionMode = System.Windows.Forms.SelectionMode.MultiSimple;
            this.lstFrom.Size = new System.Drawing.Size(149, 112);
            this.lstFrom.TabIndex = 37;
            // 
            // labelX3
            // 
            this.labelX3.Location = new System.Drawing.Point(60, 182);
            this.labelX3.Name = "labelX3";
            this.labelX3.Size = new System.Drawing.Size(93, 23);
            this.labelX3.TabIndex = 13;
            this.labelX3.Text = "Form_name";
            // 
            // txtTenform
            // 
            // 
            // 
            // 
            this.txtTenform.Border.Class = "TextBoxBorder";
            this.txtTenform.Location = new System.Drawing.Point(159, 179);
            this.txtTenform.Name = "txtTenform";
            this.txtTenform.Size = new System.Drawing.Size(149, 26);
            this.txtTenform.TabIndex = 14;
            // 
            // labelX4
            // 
            this.labelX4.Location = new System.Drawing.Point(74, 86);
            this.labelX4.Name = "labelX4";
            this.labelX4.Size = new System.Drawing.Size(79, 23);
            this.labelX4.TabIndex = 11;
            this.labelX4.Text = "Parent_ID";
            // 
            // cbbParentID
            // 
            this.cbbParentID.DisplayMember = "Text";
            this.cbbParentID.DrawMode = System.Windows.Forms.DrawMode.OwnerDrawFixed;
            this.cbbParentID.FormattingEnabled = true;
            this.cbbParentID.ItemHeight = 19;
            this.cbbParentID.Location = new System.Drawing.Point(159, 84);
            this.cbbParentID.Name = "cbbParentID";
            this.cbbParentID.Size = new System.Drawing.Size(149, 25);
            this.cbbParentID.TabIndex = 12;
            this.cbbParentID.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.cbbParentID_KeyPress);
            // 
            // labelX5
            // 
            this.labelX5.Location = new System.Drawing.Point(108, 118);
            this.labelX5.Name = "labelX5";
            this.labelX5.Size = new System.Drawing.Size(45, 23);
            this.labelX5.TabIndex = 13;
            this.labelX5.Text = "Deep";
            // 
            // txtDeep
            // 
            // 
            // 
            // 
            this.txtDeep.Border.Class = "TextBoxBorder";
            this.txtDeep.Location = new System.Drawing.Point(159, 115);
            this.txtDeep.Name = "txtDeep";
            this.txtDeep.Size = new System.Drawing.Size(149, 26);
            this.txtDeep.TabIndex = 14;
            this.txtDeep.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.txtDeep_KeyPress);
            // 
            // labelX6
            // 
            this.labelX6.Location = new System.Drawing.Point(118, 150);
            this.labelX6.Name = "labelX6";
            this.labelX6.Size = new System.Drawing.Size(35, 23);
            this.labelX6.TabIndex = 13;
            this.labelX6.Text = "Pos";
            // 
            // txtPos
            // 
            // 
            // 
            // 
            this.txtPos.Border.Class = "TextBoxBorder";
            this.txtPos.Location = new System.Drawing.Point(159, 147);
            this.txtPos.Name = "txtPos";
            this.txtPos.Size = new System.Drawing.Size(149, 26);
            this.txtPos.TabIndex = 14;
            this.txtPos.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.txtPos_KeyPress);
            // 
            // labelX8
            // 
            this.labelX8.Font = new System.Drawing.Font("Arial", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.labelX8.Location = new System.Drawing.Point(345, 11);
            this.labelX8.Name = "labelX8";
            this.labelX8.Size = new System.Drawing.Size(129, 23);
            this.labelX8.TabIndex = 44;
            this.labelX8.Text = "QUẢN LÝ MENU";
            this.labelX8.TextAlignment = System.Drawing.StringAlignment.Center;
            // 
            // frmMenu
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(9F, 18F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(819, 571);
            this.Controls.Add(this.labelX8);
            this.Controls.Add(this.btnBackAll);
            this.Controls.Add(this.btnBack);
            this.Controls.Add(this.btnToAll);
            this.Controls.Add(this.labelX7);
            this.Controls.Add(this.btnTo);
            this.Controls.Add(this.lstTo);
            this.Controls.Add(this.lstFrom);
            this.Controls.Add(this.cbbParentID);
            this.Controls.Add(this.cbbMamenu);
            this.Controls.Add(this.btnDel);
            this.Controls.Add(this.btnClose);
            this.Controls.Add(this.dgvDanhsach);
            this.Controls.Add(this.btnModify);
            this.Controls.Add(this.btnAdd);
            this.Controls.Add(this.txtPos);
            this.Controls.Add(this.labelX6);
            this.Controls.Add(this.txtDeep);
            this.Controls.Add(this.labelX5);
            this.Controls.Add(this.txtTenform);
            this.Controls.Add(this.labelX3);
            this.Controls.Add(this.txtTenmenu);
            this.Controls.Add(this.labelX4);
            this.Controls.Add(this.labelX2);
            this.Controls.Add(this.labelX1);
            this.Font = new System.Drawing.Font("Arial", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.Margin = new System.Windows.Forms.Padding(4);
            this.Name = "frmMenu";
            this.Text = "Phan quyen menu";
            this.Load += new System.EventHandler(this.frmMenu_Load);
            ((System.ComponentModel.ISupportInitialize)(this.dgvDanhsach)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private DevComponents.DotNetBar.Controls.ComboBoxEx cbbMamenu;
        private DevComponents.DotNetBar.ButtonX btnDel;
        private DevComponents.DotNetBar.ButtonX btnClose;
        private DevComponents.DotNetBar.Controls.DataGridViewX dgvDanhsach;
        private DevComponents.DotNetBar.ButtonX btnModify;
        private DevComponents.DotNetBar.ButtonX btnAdd;
        private DevComponents.DotNetBar.Controls.TextBoxX txtTenmenu;
        private DevComponents.DotNetBar.LabelX labelX2;
        private DevComponents.DotNetBar.LabelX labelX1;
        private DevComponents.DotNetBar.ButtonX btnBackAll;
        private DevComponents.DotNetBar.ButtonX btnBack;
        private DevComponents.DotNetBar.ButtonX btnToAll;
        private DevComponents.DotNetBar.LabelX labelX7;
        private DevComponents.DotNetBar.ButtonX btnTo;
        private System.Windows.Forms.ListBox lstTo;
        private System.Windows.Forms.ListBox lstFrom;
        private DevComponents.DotNetBar.LabelX labelX3;
        private DevComponents.DotNetBar.Controls.TextBoxX txtTenform;
        private DevComponents.DotNetBar.LabelX labelX4;
        private DevComponents.DotNetBar.Controls.ComboBoxEx cbbParentID;
        private DevComponents.DotNetBar.Controls.TextBoxX txtDeep;
        private DevComponents.DotNetBar.LabelX labelX5;
        private DevComponents.DotNetBar.LabelX labelX6;
        private DevComponents.DotNetBar.Controls.TextBoxX txtPos;
        private DevComponents.DotNetBar.LabelX labelX8;
    }
}