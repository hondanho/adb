namespace AutoTool
{
    partial class Main
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(Main));
            this.btnStart = new System.Windows.Forms.Button();
            this.label1 = new System.Windows.Forms.Label();
            this.btnResetSetting = new System.Windows.Forms.Button();
            this.btnSaveSetting = new System.Windows.Forms.Button();
            this.label8 = new System.Windows.Forms.Label();
            this.cbTurn2faOn = new System.Windows.Forms.CheckBox();
            this.button2 = new System.Windows.Forms.Button();
            this.button1 = new System.Windows.Forms.Button();
            this.nudThreadNo = new System.Windows.Forms.NumericUpDown();
            this.label3 = new System.Windows.Forms.Label();
            this.btnSetupMEmu = new System.Windows.Forms.Button();
            this.nudNoMEmuDevices = new System.Windows.Forms.NumericUpDown();
            this.label4 = new System.Windows.Forms.Label();
            this.txtMEmuRootPath = new System.Windows.Forms.TextBox();
            this.label2 = new System.Windows.Forms.Label();
            this.label5 = new System.Windows.Forms.Label();
            this.txtSuccess = new System.Windows.Forms.TextBox();
            this.txtFail = new System.Windows.Forms.TextBox();
            this.label6 = new System.Windows.Forms.Label();
            this.label7 = new System.Windows.Forms.Label();
            this.btnChooseBaseMEmu = new System.Windows.Forms.Button();
            this.txtMEmuZipBase = new System.Windows.Forms.TextBox();
            this.btnStop = new System.Windows.Forms.Button();
            this.lblStatus = new System.Windows.Forms.Label();
            this.tabSetting = new System.Windows.Forms.TabControl();
            this.tbRegFbSetting = new System.Windows.Forms.TabPage();
            this.tpMEmuSetupSetting = new System.Windows.Forms.TabPage();
            this.gbSetting = new System.Windows.Forms.GroupBox();
            this.label9 = new System.Windows.Forms.Label();
            this.cbHideChrome = new System.Windows.Forms.CheckBox();
            this.cbMinimizeChrome = new System.Windows.Forms.CheckBox();
            this.label10 = new System.Windows.Forms.Label();
            this.lbFirstName = new System.Windows.Forms.ListBox();
            this.txtFirstName = new System.Windows.Forms.TextBox();
            this.btnAddFirstName = new System.Windows.Forms.Button();
            this.btnRemoveFirstName = new System.Windows.Forms.Button();
            this.btnRemoveLastName = new System.Windows.Forms.Button();
            this.btnAddLastName = new System.Windows.Forms.Button();
            this.txtLastName = new System.Windows.Forms.TextBox();
            this.lbLastName = new System.Windows.Forms.ListBox();
            ((System.ComponentModel.ISupportInitialize)(this.nudThreadNo)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.nudNoMEmuDevices)).BeginInit();
            this.tabSetting.SuspendLayout();
            this.tbRegFbSetting.SuspendLayout();
            this.tpMEmuSetupSetting.SuspendLayout();
            this.gbSetting.SuspendLayout();
            this.SuspendLayout();
            // 
            // btnStart
            // 
            this.btnStart.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnStart.ForeColor = System.Drawing.Color.RoyalBlue;
            this.btnStart.Location = new System.Drawing.Point(10, 333);
            this.btnStart.Margin = new System.Windows.Forms.Padding(4);
            this.btnStart.Name = "btnStart";
            this.btnStart.Size = new System.Drawing.Size(130, 46);
            this.btnStart.TabIndex = 1;
            this.btnStart.Text = "START";
            this.btnStart.UseVisualStyleBackColor = true;
            this.btnStart.Click += new System.EventHandler(this.btnStart_Click);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label1.Location = new System.Drawing.Point(183, 25);
            this.label1.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(0, 15);
            this.label1.TabIndex = 3;
            // 
            // btnResetSetting
            // 
            this.btnResetSetting.Location = new System.Drawing.Point(881, 274);
            this.btnResetSetting.Name = "btnResetSetting";
            this.btnResetSetting.Size = new System.Drawing.Size(129, 28);
            this.btnResetSetting.TabIndex = 15;
            this.btnResetSetting.Text = "Cài đặt mặc đinh";
            this.btnResetSetting.UseVisualStyleBackColor = true;
            this.btnResetSetting.Click += new System.EventHandler(this.btnResetSetting_Click);
            // 
            // btnSaveSetting
            // 
            this.btnSaveSetting.Enabled = false;
            this.btnSaveSetting.Location = new System.Drawing.Point(784, 274);
            this.btnSaveSetting.Name = "btnSaveSetting";
            this.btnSaveSetting.Size = new System.Drawing.Size(91, 28);
            this.btnSaveSetting.TabIndex = 14;
            this.btnSaveSetting.Text = "Lưu cài đặt";
            this.btnSaveSetting.UseVisualStyleBackColor = true;
            this.btnSaveSetting.Click += new System.EventHandler(this.btnSaveSetting_Click);
            // 
            // label8
            // 
            this.label8.AutoSize = true;
            this.label8.Location = new System.Drawing.Point(7, 45);
            this.label8.Name = "label8";
            this.label8.Size = new System.Drawing.Size(49, 15);
            this.label8.TabIndex = 13;
            this.label8.Text = "Bật 2FA";
            // 
            // cbTurn2faOn
            // 
            this.cbTurn2faOn.AutoSize = true;
            this.cbTurn2faOn.Checked = true;
            this.cbTurn2faOn.CheckState = System.Windows.Forms.CheckState.Checked;
            this.cbTurn2faOn.Location = new System.Drawing.Point(117, 44);
            this.cbTurn2faOn.Name = "cbTurn2faOn";
            this.cbTurn2faOn.Size = new System.Drawing.Size(15, 14);
            this.cbTurn2faOn.TabIndex = 12;
            this.cbTurn2faOn.UseVisualStyleBackColor = true;
            this.cbTurn2faOn.CheckedChanged += new System.EventHandler(this.SettingValueChanged);
            // 
            // button2
            // 
            this.button2.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.button2.Location = new System.Drawing.Point(114, 276);
            this.button2.Name = "button2";
            this.button2.Size = new System.Drawing.Size(84, 26);
            this.button2.TabIndex = 11;
            this.button2.Text = "button2";
            this.button2.UseVisualStyleBackColor = true;
            this.button2.Click += new System.EventHandler(this.button2_Click);
            // 
            // button1
            // 
            this.button1.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.button1.Location = new System.Drawing.Point(10, 276);
            this.button1.Name = "button1";
            this.button1.Size = new System.Drawing.Size(84, 26);
            this.button1.TabIndex = 10;
            this.button1.Text = "button1";
            this.button1.UseVisualStyleBackColor = true;
            this.button1.Click += new System.EventHandler(this.button1_Click);
            // 
            // nudThreadNo
            // 
            this.nudThreadNo.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.nudThreadNo.Location = new System.Drawing.Point(117, 13);
            this.nudThreadNo.Margin = new System.Windows.Forms.Padding(4);
            this.nudThreadNo.Maximum = new decimal(new int[] {
            8,
            0,
            0,
            0});
            this.nudThreadNo.Minimum = new decimal(new int[] {
            1,
            0,
            0,
            0});
            this.nudThreadNo.Name = "nudThreadNo";
            this.nudThreadNo.Size = new System.Drawing.Size(60, 21);
            this.nudThreadNo.TabIndex = 3;
            this.nudThreadNo.Value = new decimal(new int[] {
            2,
            0,
            0,
            0});
            this.nudThreadNo.ValueChanged += new System.EventHandler(this.SettingValueChanged);
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label3.Location = new System.Drawing.Point(7, 15);
            this.label3.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(56, 15);
            this.label3.TabIndex = 2;
            this.label3.Text = "Số luồng";
            // 
            // btnSetupMEmu
            // 
            this.btnSetupMEmu.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnSetupMEmu.Location = new System.Drawing.Point(7, 175);
            this.btnSetupMEmu.Margin = new System.Windows.Forms.Padding(4);
            this.btnSetupMEmu.Name = "btnSetupMEmu";
            this.btnSetupMEmu.Size = new System.Drawing.Size(123, 32);
            this.btnSetupMEmu.TabIndex = 7;
            this.btnSetupMEmu.Text = "Cài đặt";
            this.btnSetupMEmu.UseVisualStyleBackColor = true;
            this.btnSetupMEmu.Click += new System.EventHandler(this.btnSetupMEmu_Click);
            // 
            // nudNoMEmuDevices
            // 
            this.nudNoMEmuDevices.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.nudNoMEmuDevices.Location = new System.Drawing.Point(156, 46);
            this.nudNoMEmuDevices.Margin = new System.Windows.Forms.Padding(4);
            this.nudNoMEmuDevices.Maximum = new decimal(new int[] {
            8,
            0,
            0,
            0});
            this.nudNoMEmuDevices.Minimum = new decimal(new int[] {
            1,
            0,
            0,
            0});
            this.nudNoMEmuDevices.Name = "nudNoMEmuDevices";
            this.nudNoMEmuDevices.Size = new System.Drawing.Size(60, 21);
            this.nudNoMEmuDevices.TabIndex = 6;
            this.nudNoMEmuDevices.Value = new decimal(new int[] {
            2,
            0,
            0,
            0});
            this.nudNoMEmuDevices.ValueChanged += new System.EventHandler(this.SettingValueChanged);
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label4.Location = new System.Drawing.Point(7, 49);
            this.label4.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(110, 15);
            this.label4.TabIndex = 5;
            this.label4.Text = "No. MEmu devices";
            // 
            // txtMEmuRootPath
            // 
            this.txtMEmuRootPath.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtMEmuRootPath.Location = new System.Drawing.Point(156, 8);
            this.txtMEmuRootPath.Margin = new System.Windows.Forms.Padding(4);
            this.txtMEmuRootPath.Name = "txtMEmuRootPath";
            this.txtMEmuRootPath.Size = new System.Drawing.Size(320, 21);
            this.txtMEmuRootPath.TabIndex = 1;
            this.txtMEmuRootPath.Text = "D:\\Program Files\\Microvirt\\MEmu";
            this.txtMEmuRootPath.TextChanged += new System.EventHandler(this.SettingValueChanged);
            this.txtMEmuRootPath.MouseDoubleClick += new System.Windows.Forms.MouseEventHandler(this.txtMEmuRootPath_MouseDoubleClick);
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label2.Location = new System.Drawing.Point(7, 13);
            this.label2.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(68, 15);
            this.label2.TabIndex = 0;
            this.label2.Text = "MEmu root";
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label5.Location = new System.Drawing.Point(6, 393);
            this.label5.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(119, 15);
            this.label5.TabIndex = 5;
            this.label5.Text = "Successed Accounts";
            // 
            // txtSuccess
            // 
            this.txtSuccess.BackColor = System.Drawing.SystemColors.ControlLightLight;
            this.txtSuccess.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtSuccess.Location = new System.Drawing.Point(10, 416);
            this.txtSuccess.Margin = new System.Windows.Forms.Padding(4);
            this.txtSuccess.Multiline = true;
            this.txtSuccess.Name = "txtSuccess";
            this.txtSuccess.ReadOnly = true;
            this.txtSuccess.ScrollBars = System.Windows.Forms.ScrollBars.Both;
            this.txtSuccess.Size = new System.Drawing.Size(496, 278);
            this.txtSuccess.TabIndex = 6;
            this.txtSuccess.WordWrap = false;
            // 
            // txtFail
            // 
            this.txtFail.BackColor = System.Drawing.SystemColors.ControlLightLight;
            this.txtFail.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtFail.Location = new System.Drawing.Point(531, 416);
            this.txtFail.Margin = new System.Windows.Forms.Padding(4);
            this.txtFail.Multiline = true;
            this.txtFail.Name = "txtFail";
            this.txtFail.ReadOnly = true;
            this.txtFail.ScrollBars = System.Windows.Forms.ScrollBars.Both;
            this.txtFail.Size = new System.Drawing.Size(499, 278);
            this.txtFail.TabIndex = 7;
            this.txtFail.WordWrap = false;
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label6.Location = new System.Drawing.Point(526, 393);
            this.label6.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(93, 15);
            this.label6.TabIndex = 8;
            this.label6.Text = "Failed Accounts";
            // 
            // label7
            // 
            this.label7.AutoSize = true;
            this.label7.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label7.Location = new System.Drawing.Point(7, 86);
            this.label7.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(93, 15);
            this.label7.TabIndex = 10;
            this.label7.Text = "MEmu zip base";
            // 
            // btnChooseBaseMEmu
            // 
            this.btnChooseBaseMEmu.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnChooseBaseMEmu.Location = new System.Drawing.Point(413, 78);
            this.btnChooseBaseMEmu.Margin = new System.Windows.Forms.Padding(4);
            this.btnChooseBaseMEmu.Name = "btnChooseBaseMEmu";
            this.btnChooseBaseMEmu.Size = new System.Drawing.Size(63, 32);
            this.btnChooseBaseMEmu.TabIndex = 9;
            this.btnChooseBaseMEmu.Text = "Chọn";
            this.btnChooseBaseMEmu.UseVisualStyleBackColor = true;
            this.btnChooseBaseMEmu.Click += new System.EventHandler(this.btnChooseBaseMEmu_Click);
            // 
            // txtMEmuZipBase
            // 
            this.txtMEmuZipBase.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtMEmuZipBase.Location = new System.Drawing.Point(156, 82);
            this.txtMEmuZipBase.Margin = new System.Windows.Forms.Padding(4);
            this.txtMEmuZipBase.Name = "txtMEmuZipBase";
            this.txtMEmuZipBase.ReadOnly = true;
            this.txtMEmuZipBase.Size = new System.Drawing.Size(248, 21);
            this.txtMEmuZipBase.TabIndex = 8;
            // 
            // btnStop
            // 
            this.btnStop.Enabled = false;
            this.btnStop.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnStop.ForeColor = System.Drawing.Color.Red;
            this.btnStop.Location = new System.Drawing.Point(150, 333);
            this.btnStop.Margin = new System.Windows.Forms.Padding(4);
            this.btnStop.Name = "btnStop";
            this.btnStop.Size = new System.Drawing.Size(130, 46);
            this.btnStop.TabIndex = 9;
            this.btnStop.Text = "STOP";
            this.btnStop.UseVisualStyleBackColor = true;
            this.btnStop.Click += new System.EventHandler(this.btnStop_Click);
            // 
            // lblStatus
            // 
            this.lblStatus.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblStatus.Location = new System.Drawing.Point(288, 346);
            this.lblStatus.Name = "lblStatus";
            this.lblStatus.Size = new System.Drawing.Size(742, 26);
            this.lblStatus.TabIndex = 10;
            this.lblStatus.Text = "lblStatus";
            // 
            // tabSetting
            // 
            this.tabSetting.Controls.Add(this.tbRegFbSetting);
            this.tabSetting.Controls.Add(this.tpMEmuSetupSetting);
            this.tabSetting.Location = new System.Drawing.Point(6, 23);
            this.tabSetting.Name = "tabSetting";
            this.tabSetting.SelectedIndex = 0;
            this.tabSetting.Size = new System.Drawing.Size(1008, 245);
            this.tabSetting.TabIndex = 11;
            // 
            // tbRegFbSetting
            // 
            this.tbRegFbSetting.Controls.Add(this.btnRemoveLastName);
            this.tbRegFbSetting.Controls.Add(this.btnAddLastName);
            this.tbRegFbSetting.Controls.Add(this.txtLastName);
            this.tbRegFbSetting.Controls.Add(this.lbLastName);
            this.tbRegFbSetting.Controls.Add(this.btnRemoveFirstName);
            this.tbRegFbSetting.Controls.Add(this.btnAddFirstName);
            this.tbRegFbSetting.Controls.Add(this.txtFirstName);
            this.tbRegFbSetting.Controls.Add(this.lbFirstName);
            this.tbRegFbSetting.Controls.Add(this.cbMinimizeChrome);
            this.tbRegFbSetting.Controls.Add(this.label10);
            this.tbRegFbSetting.Controls.Add(this.cbHideChrome);
            this.tbRegFbSetting.Controls.Add(this.label9);
            this.tbRegFbSetting.Controls.Add(this.label3);
            this.tbRegFbSetting.Controls.Add(this.nudThreadNo);
            this.tbRegFbSetting.Controls.Add(this.label8);
            this.tbRegFbSetting.Controls.Add(this.cbTurn2faOn);
            this.tbRegFbSetting.Location = new System.Drawing.Point(4, 24);
            this.tbRegFbSetting.Name = "tbRegFbSetting";
            this.tbRegFbSetting.Padding = new System.Windows.Forms.Padding(3);
            this.tbRegFbSetting.Size = new System.Drawing.Size(1000, 217);
            this.tbRegFbSetting.TabIndex = 0;
            this.tbRegFbSetting.Text = "Cài đặt Reg Facebook";
            this.tbRegFbSetting.UseVisualStyleBackColor = true;
            // 
            // tpMEmuSetupSetting
            // 
            this.tpMEmuSetupSetting.Controls.Add(this.label7);
            this.tpMEmuSetupSetting.Controls.Add(this.btnSetupMEmu);
            this.tpMEmuSetupSetting.Controls.Add(this.btnChooseBaseMEmu);
            this.tpMEmuSetupSetting.Controls.Add(this.txtMEmuRootPath);
            this.tpMEmuSetupSetting.Controls.Add(this.txtMEmuZipBase);
            this.tpMEmuSetupSetting.Controls.Add(this.label4);
            this.tpMEmuSetupSetting.Controls.Add(this.label2);
            this.tpMEmuSetupSetting.Controls.Add(this.nudNoMEmuDevices);
            this.tpMEmuSetupSetting.Location = new System.Drawing.Point(4, 24);
            this.tpMEmuSetupSetting.Name = "tpMEmuSetupSetting";
            this.tpMEmuSetupSetting.Padding = new System.Windows.Forms.Padding(3);
            this.tpMEmuSetupSetting.Size = new System.Drawing.Size(1000, 217);
            this.tpMEmuSetupSetting.TabIndex = 1;
            this.tpMEmuSetupSetting.Text = "Cài đặt MEmu";
            this.tpMEmuSetupSetting.UseVisualStyleBackColor = true;
            // 
            // gbSetting
            // 
            this.gbSetting.Controls.Add(this.tabSetting);
            this.gbSetting.Controls.Add(this.btnResetSetting);
            this.gbSetting.Controls.Add(this.btnSaveSetting);
            this.gbSetting.Controls.Add(this.button1);
            this.gbSetting.Controls.Add(this.button2);
            this.gbSetting.Location = new System.Drawing.Point(10, 7);
            this.gbSetting.Name = "gbSetting";
            this.gbSetting.Size = new System.Drawing.Size(1020, 317);
            this.gbSetting.TabIndex = 16;
            this.gbSetting.TabStop = false;
            this.gbSetting.Text = "Cặt đặt";
            // 
            // label9
            // 
            this.label9.AutoSize = true;
            this.label9.Location = new System.Drawing.Point(7, 73);
            this.label9.Name = "label9";
            this.label9.Size = new System.Drawing.Size(68, 15);
            this.label9.TabIndex = 14;
            this.label9.Text = "Ẩn Chrome";
            // 
            // cbHideChrome
            // 
            this.cbHideChrome.AutoSize = true;
            this.cbHideChrome.Location = new System.Drawing.Point(117, 74);
            this.cbHideChrome.Name = "cbHideChrome";
            this.cbHideChrome.Size = new System.Drawing.Size(15, 14);
            this.cbHideChrome.TabIndex = 15;
            this.cbHideChrome.UseVisualStyleBackColor = true;
            this.cbHideChrome.CheckedChanged += new System.EventHandler(this.SettingValueChanged);
            // 
            // cbMinimizeChrome
            // 
            this.cbMinimizeChrome.AutoSize = true;
            this.cbMinimizeChrome.Location = new System.Drawing.Point(117, 101);
            this.cbMinimizeChrome.Name = "cbMinimizeChrome";
            this.cbMinimizeChrome.Size = new System.Drawing.Size(15, 14);
            this.cbMinimizeChrome.TabIndex = 17;
            this.cbMinimizeChrome.UseVisualStyleBackColor = true;
            this.cbMinimizeChrome.CheckedChanged += new System.EventHandler(this.SettingValueChanged);
            // 
            // label10
            // 
            this.label10.AutoSize = true;
            this.label10.Location = new System.Drawing.Point(7, 100);
            this.label10.Name = "label10";
            this.label10.Size = new System.Drawing.Size(105, 15);
            this.label10.TabIndex = 16;
            this.label10.Text = "Minimize Chrome";
            // 
            // lbFirstName
            // 
            this.lbFirstName.FormattingEnabled = true;
            this.lbFirstName.ItemHeight = 15;
            this.lbFirstName.Location = new System.Drawing.Point(424, 29);
            this.lbFirstName.Name = "lbFirstName";
            this.lbFirstName.Size = new System.Drawing.Size(175, 184);
            this.lbFirstName.TabIndex = 18;
            // 
            // txtFirstName
            // 
            this.txtFirstName.Location = new System.Drawing.Point(424, 5);
            this.txtFirstName.Name = "txtFirstName";
            this.txtFirstName.Size = new System.Drawing.Size(109, 21);
            this.txtFirstName.TabIndex = 19;
            // 
            // btnAddFirstName
            // 
            this.btnAddFirstName.Location = new System.Drawing.Point(536, 4);
            this.btnAddFirstName.Name = "btnAddFirstName";
            this.btnAddFirstName.Size = new System.Drawing.Size(32, 23);
            this.btnAddFirstName.TabIndex = 20;
            this.btnAddFirstName.Text = "+";
            this.btnAddFirstName.UseVisualStyleBackColor = true;
            this.btnAddFirstName.Click += new System.EventHandler(this.btnAddFirstName_Click);
            // 
            // btnRemoveFirstName
            // 
            this.btnRemoveFirstName.Location = new System.Drawing.Point(568, 4);
            this.btnRemoveFirstName.Name = "btnRemoveFirstName";
            this.btnRemoveFirstName.Size = new System.Drawing.Size(32, 23);
            this.btnRemoveFirstName.TabIndex = 21;
            this.btnRemoveFirstName.Text = "-";
            this.btnRemoveFirstName.UseVisualStyleBackColor = true;
            this.btnRemoveFirstName.Click += new System.EventHandler(this.btnRemoveFirstName_Click);
            // 
            // btnRemoveLastName
            // 
            this.btnRemoveLastName.Location = new System.Drawing.Point(749, 4);
            this.btnRemoveLastName.Name = "btnRemoveLastName";
            this.btnRemoveLastName.Size = new System.Drawing.Size(32, 23);
            this.btnRemoveLastName.TabIndex = 25;
            this.btnRemoveLastName.Text = "-";
            this.btnRemoveLastName.UseVisualStyleBackColor = true;
            this.btnRemoveLastName.Click += new System.EventHandler(this.btnRemoveLastName_Click);
            // 
            // btnAddLastName
            // 
            this.btnAddLastName.Location = new System.Drawing.Point(717, 4);
            this.btnAddLastName.Name = "btnAddLastName";
            this.btnAddLastName.Size = new System.Drawing.Size(32, 23);
            this.btnAddLastName.TabIndex = 24;
            this.btnAddLastName.Text = "+";
            this.btnAddLastName.UseVisualStyleBackColor = true;
            this.btnAddLastName.Click += new System.EventHandler(this.btnAddLastName_Click);
            // 
            // txtLastName
            // 
            this.txtLastName.Location = new System.Drawing.Point(605, 5);
            this.txtLastName.Name = "txtLastName";
            this.txtLastName.Size = new System.Drawing.Size(109, 21);
            this.txtLastName.TabIndex = 23;
            // 
            // lbLastName
            // 
            this.lbLastName.FormattingEnabled = true;
            this.lbLastName.ItemHeight = 15;
            this.lbLastName.Location = new System.Drawing.Point(605, 29);
            this.lbLastName.Name = "lbLastName";
            this.lbLastName.Size = new System.Drawing.Size(175, 184);
            this.lbLastName.TabIndex = 22;
            // 
            // Main
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 15F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1041, 720);
            this.Controls.Add(this.gbSetting);
            this.Controls.Add(this.lblStatus);
            this.Controls.Add(this.btnStop);
            this.Controls.Add(this.label6);
            this.Controls.Add(this.txtFail);
            this.Controls.Add(this.txtSuccess);
            this.Controls.Add(this.label5);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.btnStart);
            this.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Margin = new System.Windows.Forms.Padding(4);
            this.Name = "Main";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Register Facebook Account";
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.Main_FormClosing);
            ((System.ComponentModel.ISupportInitialize)(this.nudThreadNo)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.nudNoMEmuDevices)).EndInit();
            this.tabSetting.ResumeLayout(false);
            this.tbRegFbSetting.ResumeLayout(false);
            this.tbRegFbSetting.PerformLayout();
            this.tpMEmuSetupSetting.ResumeLayout(false);
            this.tpMEmuSetupSetting.PerformLayout();
            this.gbSetting.ResumeLayout(false);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion
        private System.Windows.Forms.Button btnStart;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.TextBox txtMEmuRootPath;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.NumericUpDown nudThreadNo;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Button btnSetupMEmu;
        private System.Windows.Forms.NumericUpDown nudNoMEmuDevices;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.TextBox txtSuccess;
        private System.Windows.Forms.TextBox txtFail;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.Label label7;
        private System.Windows.Forms.Button btnChooseBaseMEmu;
        private System.Windows.Forms.TextBox txtMEmuZipBase;
        private System.Windows.Forms.Button btnStop;
        private System.Windows.Forms.Button button1;
        private System.Windows.Forms.Button button2;
        private System.Windows.Forms.Label lblStatus;
        private System.Windows.Forms.Label label8;
        private System.Windows.Forms.CheckBox cbTurn2faOn;
        private System.Windows.Forms.Button btnSaveSetting;
        private System.Windows.Forms.Button btnResetSetting;
        private System.Windows.Forms.TabControl tabSetting;
        private System.Windows.Forms.TabPage tbRegFbSetting;
        private System.Windows.Forms.TabPage tpMEmuSetupSetting;
        private System.Windows.Forms.GroupBox gbSetting;
        private System.Windows.Forms.CheckBox cbHideChrome;
        private System.Windows.Forms.Label label9;
        private System.Windows.Forms.CheckBox cbMinimizeChrome;
        private System.Windows.Forms.Label label10;
        private System.Windows.Forms.Button btnRemoveFirstName;
        private System.Windows.Forms.Button btnAddFirstName;
        private System.Windows.Forms.TextBox txtFirstName;
        private System.Windows.Forms.ListBox lbFirstName;
        private System.Windows.Forms.Button btnRemoveLastName;
        private System.Windows.Forms.Button btnAddLastName;
        private System.Windows.Forms.TextBox txtLastName;
        private System.Windows.Forms.ListBox lbLastName;
    }
}