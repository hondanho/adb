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

            if (this._fileAccountFailer != null)
            {
                this._fileAccountFailer.Dispose();
            }
            if (this._fileAccountSuccess != null)
            {
                this._fileAccountSuccess.Dispose();
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
            this.dgvListDevices = new System.Windows.Forms.DataGridView();
            this.label15 = new System.Windows.Forms.Label();
            this.txtOutputDirectory = new System.Windows.Forms.TextBox();
            this.rbUseMEmu = new System.Windows.Forms.RadioButton();
            this.rbUseLDPLayer = new System.Windows.Forms.RadioButton();
            this.btnRemoveLastName = new System.Windows.Forms.Button();
            this.btnAddLastName = new System.Windows.Forms.Button();
            this.txtLastName = new System.Windows.Forms.TextBox();
            this.lbLastName = new System.Windows.Forms.ListBox();
            this.btnRemoveFirstName = new System.Windows.Forms.Button();
            this.btnAddFirstName = new System.Windows.Forms.Button();
            this.txtFirstName = new System.Windows.Forms.TextBox();
            this.lbFirstName = new System.Windows.Forms.ListBox();
            this.cbMinimizeChrome = new System.Windows.Forms.CheckBox();
            this.label10 = new System.Windows.Forms.Label();
            this.cbHideChrome = new System.Windows.Forms.CheckBox();
            this.label9 = new System.Windows.Forms.Label();
            this.tpLDPlayerSetting = new System.Windows.Forms.TabPage();
            this.label11 = new System.Windows.Forms.Label();
            this.btnSetupLDPlayer = new System.Windows.Forms.Button();
            this.nudNoLDPlayerDevices = new System.Windows.Forms.NumericUpDown();
            this.label13 = new System.Windows.Forms.Label();
            this.btnChooseBaseLDPlayer = new System.Windows.Forms.Button();
            this.label12 = new System.Windows.Forms.Label();
            this.txtLDPlayerBase = new System.Windows.Forms.TextBox();
            this.txtLDPlayerRootPath = new System.Windows.Forms.TextBox();
            this.tpMEmuSetting = new System.Windows.Forms.TabPage();
            this.tbLogs = new System.Windows.Forms.TabPage();
            this.label14 = new System.Windows.Forms.Label();
            this.txtRegisterLogs = new System.Windows.Forms.TextBox();
            this.gbSetting = new System.Windows.Forms.GroupBox();
            this.btnReloadDevices = new System.Windows.Forms.Button();
            this.btnChooseAll = new System.Windows.Forms.Button();
            this.indexDataGridViewTextBoxColumn = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.chooseDataGridViewCheckBoxColumn = new System.Windows.Forms.DataGridViewCheckBoxColumn();
            this.idDataGridViewTextBoxColumn = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.nameDataGridViewTextBoxColumn = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.emulatorInfoBindingSource = new System.Windows.Forms.BindingSource(this.components);
            ((System.ComponentModel.ISupportInitialize)(this.nudThreadNo)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.nudNoMEmuDevices)).BeginInit();
            this.tabSetting.SuspendLayout();
            this.tbRegFbSetting.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dgvListDevices)).BeginInit();
            this.tpLDPlayerSetting.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.nudNoLDPlayerDevices)).BeginInit();
            this.tpMEmuSetting.SuspendLayout();
            this.tbLogs.SuspendLayout();
            this.gbSetting.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.emulatorInfoBindingSource)).BeginInit();
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
            this.label1.Size = new System.Drawing.Size(0, 18);
            this.label1.TabIndex = 3;
            // 
            // btnResetSetting
            // 
            this.btnResetSetting.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.btnResetSetting.Location = new System.Drawing.Point(774, 275);
            this.btnResetSetting.Name = "btnResetSetting";
            this.btnResetSetting.Size = new System.Drawing.Size(129, 28);
            this.btnResetSetting.TabIndex = 15;
            this.btnResetSetting.Text = "Cài đặt mặc đinh";
            this.btnResetSetting.UseVisualStyleBackColor = true;
            this.btnResetSetting.Click += new System.EventHandler(this.btnResetSetting_Click);
            // 
            // btnSaveSetting
            // 
            this.btnSaveSetting.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.btnSaveSetting.Enabled = false;
            this.btnSaveSetting.Location = new System.Drawing.Point(677, 275);
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
            this.label8.Location = new System.Drawing.Point(7, 44);
            this.label8.Name = "label8";
            this.label8.Size = new System.Drawing.Size(60, 18);
            this.label8.TabIndex = 13;
            this.label8.Text = "Bật 2FA";
            // 
            // cbTurn2faOn
            // 
            this.cbTurn2faOn.AutoSize = true;
            this.cbTurn2faOn.Checked = true;
            this.cbTurn2faOn.CheckState = System.Windows.Forms.CheckState.Checked;
            this.cbTurn2faOn.Location = new System.Drawing.Point(132, 43);
            this.cbTurn2faOn.Name = "cbTurn2faOn";
            this.cbTurn2faOn.Size = new System.Drawing.Size(18, 17);
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
            this.nudThreadNo.Location = new System.Drawing.Point(132, 13);
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
            this.nudThreadNo.Size = new System.Drawing.Size(60, 24);
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
            this.label3.Size = new System.Drawing.Size(67, 18);
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
            this.nudNoMEmuDevices.Location = new System.Drawing.Point(158, 46);
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
            this.nudNoMEmuDevices.Size = new System.Drawing.Size(60, 24);
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
            this.label4.Size = new System.Drawing.Size(134, 18);
            this.label4.TabIndex = 5;
            this.label4.Text = "No. MEmu devices";
            // 
            // txtMEmuRootPath
            // 
            this.txtMEmuRootPath.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtMEmuRootPath.Location = new System.Drawing.Point(158, 8);
            this.txtMEmuRootPath.Margin = new System.Windows.Forms.Padding(4);
            this.txtMEmuRootPath.Name = "txtMEmuRootPath";
            this.txtMEmuRootPath.Size = new System.Drawing.Size(320, 24);
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
            this.label2.Size = new System.Drawing.Size(83, 18);
            this.label2.TabIndex = 0;
            this.label2.Text = "MEmu root";
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label5.Location = new System.Drawing.Point(7, 393);
            this.label5.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(148, 18);
            this.label5.TabIndex = 5;
            this.label5.Text = "Successed Accounts";
            // 
            // txtSuccess
            // 
            this.txtSuccess.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left)));
            this.txtSuccess.BackColor = System.Drawing.SystemColors.ControlLightLight;
            this.txtSuccess.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtSuccess.Location = new System.Drawing.Point(10, 416);
            this.txtSuccess.Margin = new System.Windows.Forms.Padding(4);
            this.txtSuccess.Multiline = true;
            this.txtSuccess.Name = "txtSuccess";
            this.txtSuccess.ReadOnly = true;
            this.txtSuccess.ScrollBars = System.Windows.Forms.ScrollBars.Both;
            this.txtSuccess.Size = new System.Drawing.Size(450, 263);
            this.txtSuccess.TabIndex = 6;
            this.txtSuccess.WordWrap = false;
            // 
            // txtFail
            // 
            this.txtFail.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.txtFail.BackColor = System.Drawing.SystemColors.ControlLightLight;
            this.txtFail.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtFail.Location = new System.Drawing.Point(468, 416);
            this.txtFail.Margin = new System.Windows.Forms.Padding(4);
            this.txtFail.Multiline = true;
            this.txtFail.Name = "txtFail";
            this.txtFail.ReadOnly = true;
            this.txtFail.ScrollBars = System.Windows.Forms.ScrollBars.Both;
            this.txtFail.Size = new System.Drawing.Size(450, 263);
            this.txtFail.TabIndex = 7;
            this.txtFail.WordWrap = false;
            // 
            // label6
            // 
            this.label6.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.label6.AutoSize = true;
            this.label6.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label6.Location = new System.Drawing.Point(465, 393);
            this.label6.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(113, 18);
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
            this.label7.Size = new System.Drawing.Size(111, 18);
            this.label7.TabIndex = 10;
            this.label7.Text = "MEmu zip base";
            // 
            // btnChooseBaseMEmu
            // 
            this.btnChooseBaseMEmu.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnChooseBaseMEmu.Location = new System.Drawing.Point(413, 81);
            this.btnChooseBaseMEmu.Margin = new System.Windows.Forms.Padding(4);
            this.btnChooseBaseMEmu.Name = "btnChooseBaseMEmu";
            this.btnChooseBaseMEmu.Size = new System.Drawing.Size(63, 24);
            this.btnChooseBaseMEmu.TabIndex = 9;
            this.btnChooseBaseMEmu.Text = "Chọn";
            this.btnChooseBaseMEmu.UseVisualStyleBackColor = true;
            this.btnChooseBaseMEmu.Click += new System.EventHandler(this.btnChooseBaseMEmu_Click);
            // 
            // txtMEmuZipBase
            // 
            this.txtMEmuZipBase.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtMEmuZipBase.Location = new System.Drawing.Point(158, 82);
            this.txtMEmuZipBase.Margin = new System.Windows.Forms.Padding(4);
            this.txtMEmuZipBase.Name = "txtMEmuZipBase";
            this.txtMEmuZipBase.ReadOnly = true;
            this.txtMEmuZipBase.Size = new System.Drawing.Size(248, 24);
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
            this.lblStatus.Size = new System.Drawing.Size(423, 26);
            this.lblStatus.TabIndex = 10;
            this.lblStatus.Text = "lblStatus";
            // 
            // tabSetting
            // 
            this.tabSetting.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.tabSetting.Controls.Add(this.tbRegFbSetting);
            this.tabSetting.Controls.Add(this.tpLDPlayerSetting);
            this.tabSetting.Controls.Add(this.tpMEmuSetting);
            this.tabSetting.Controls.Add(this.tbLogs);
            this.tabSetting.Location = new System.Drawing.Point(7, 23);
            this.tabSetting.Name = "tabSetting";
            this.tabSetting.SelectedIndex = 0;
            this.tabSetting.Size = new System.Drawing.Size(896, 245);
            this.tabSetting.TabIndex = 11;
            // 
            // tbRegFbSetting
            // 
            this.tbRegFbSetting.Controls.Add(this.btnChooseAll);
            this.tbRegFbSetting.Controls.Add(this.btnReloadDevices);
            this.tbRegFbSetting.Controls.Add(this.dgvListDevices);
            this.tbRegFbSetting.Controls.Add(this.label15);
            this.tbRegFbSetting.Controls.Add(this.txtOutputDirectory);
            this.tbRegFbSetting.Controls.Add(this.rbUseMEmu);
            this.tbRegFbSetting.Controls.Add(this.rbUseLDPLayer);
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
            this.tbRegFbSetting.Location = new System.Drawing.Point(4, 27);
            this.tbRegFbSetting.Name = "tbRegFbSetting";
            this.tbRegFbSetting.Padding = new System.Windows.Forms.Padding(3);
            this.tbRegFbSetting.Size = new System.Drawing.Size(888, 214);
            this.tbRegFbSetting.TabIndex = 0;
            this.tbRegFbSetting.Text = "Cài đặt Reg Facebook";
            this.tbRegFbSetting.UseVisualStyleBackColor = true;
            // 
            // dgvListDevices
            // 
            this.dgvListDevices.AllowUserToDeleteRows = false;
            this.dgvListDevices.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.dgvListDevices.AutoGenerateColumns = false;
            this.dgvListDevices.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dgvListDevices.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.indexDataGridViewTextBoxColumn,
            this.chooseDataGridViewCheckBoxColumn,
            this.idDataGridViewTextBoxColumn,
            this.nameDataGridViewTextBoxColumn});
            this.dgvListDevices.DataSource = this.emulatorInfoBindingSource;
            this.dgvListDevices.Location = new System.Drawing.Point(280, 27);
            this.dgvListDevices.Name = "dgvListDevices";
            this.dgvListDevices.RowHeadersVisible = false;
            this.dgvListDevices.RowHeadersWidth = 51;
            this.dgvListDevices.RowTemplate.Height = 24;
            this.dgvListDevices.ShowEditingIcon = false;
            this.dgvListDevices.Size = new System.Drawing.Size(240, 184);
            this.dgvListDevices.TabIndex = 30;
            // 
            // label15
            // 
            this.label15.AutoSize = true;
            this.label15.Location = new System.Drawing.Point(5, 150);
            this.label15.Name = "label15";
            this.label15.Size = new System.Drawing.Size(52, 18);
            this.label15.TabIndex = 29;
            this.label15.Text = "Output";
            // 
            // txtOutputDirectory
            // 
            this.txtOutputDirectory.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtOutputDirectory.Location = new System.Drawing.Point(64, 147);
            this.txtOutputDirectory.Margin = new System.Windows.Forms.Padding(4);
            this.txtOutputDirectory.Name = "txtOutputDirectory";
            this.txtOutputDirectory.ReadOnly = true;
            this.txtOutputDirectory.Size = new System.Drawing.Size(195, 24);
            this.txtOutputDirectory.TabIndex = 28;
            this.txtOutputDirectory.Text = "D:\\rcfb";
            this.txtOutputDirectory.TextChanged += new System.EventHandler(this.SettingValueChanged);
            this.txtOutputDirectory.MouseDoubleClick += new System.Windows.Forms.MouseEventHandler(this.txtOutputDirectory_MouseDoubleClick);
            // 
            // rbUseMEmu
            // 
            this.rbUseMEmu.AutoSize = true;
            this.rbUseMEmu.Location = new System.Drawing.Point(129, 120);
            this.rbUseMEmu.Name = "rbUseMEmu";
            this.rbUseMEmu.Size = new System.Drawing.Size(101, 22);
            this.rbUseMEmu.TabIndex = 27;
            this.rbUseMEmu.Text = "MEmuPlay";
            this.rbUseMEmu.UseVisualStyleBackColor = true;
            this.rbUseMEmu.CheckedChanged += new System.EventHandler(this.SettingValueChanged);
            // 
            // rbUseLDPLayer
            // 
            this.rbUseLDPLayer.AutoSize = true;
            this.rbUseLDPLayer.Checked = true;
            this.rbUseLDPLayer.Location = new System.Drawing.Point(7, 120);
            this.rbUseLDPLayer.Name = "rbUseLDPLayer";
            this.rbUseLDPLayer.Size = new System.Drawing.Size(89, 22);
            this.rbUseLDPLayer.TabIndex = 26;
            this.rbUseLDPLayer.TabStop = true;
            this.rbUseLDPLayer.Text = "LDPlayer";
            this.rbUseLDPLayer.UseVisualStyleBackColor = true;
            this.rbUseLDPLayer.CheckedChanged += new System.EventHandler(this.SettingValueChanged);
            // 
            // btnRemoveLastName
            // 
            this.btnRemoveLastName.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.btnRemoveLastName.Location = new System.Drawing.Point(851, 2);
            this.btnRemoveLastName.Name = "btnRemoveLastName";
            this.btnRemoveLastName.Size = new System.Drawing.Size(32, 23);
            this.btnRemoveLastName.TabIndex = 25;
            this.btnRemoveLastName.Text = "-";
            this.btnRemoveLastName.UseVisualStyleBackColor = true;
            this.btnRemoveLastName.Click += new System.EventHandler(this.btnRemoveLastName_Click);
            // 
            // btnAddLastName
            // 
            this.btnAddLastName.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.btnAddLastName.Location = new System.Drawing.Point(819, 2);
            this.btnAddLastName.Name = "btnAddLastName";
            this.btnAddLastName.Size = new System.Drawing.Size(32, 23);
            this.btnAddLastName.TabIndex = 24;
            this.btnAddLastName.Text = "+";
            this.btnAddLastName.UseVisualStyleBackColor = true;
            this.btnAddLastName.Click += new System.EventHandler(this.btnAddLastName_Click);
            // 
            // txtLastName
            // 
            this.txtLastName.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.txtLastName.Location = new System.Drawing.Point(707, 3);
            this.txtLastName.Name = "txtLastName";
            this.txtLastName.Size = new System.Drawing.Size(109, 24);
            this.txtLastName.TabIndex = 23;
            // 
            // lbLastName
            // 
            this.lbLastName.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.lbLastName.FormattingEnabled = true;
            this.lbLastName.ItemHeight = 18;
            this.lbLastName.Location = new System.Drawing.Point(707, 27);
            this.lbLastName.Name = "lbLastName";
            this.lbLastName.Size = new System.Drawing.Size(175, 184);
            this.lbLastName.TabIndex = 22;
            // 
            // btnRemoveFirstName
            // 
            this.btnRemoveFirstName.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.btnRemoveFirstName.Location = new System.Drawing.Point(670, 2);
            this.btnRemoveFirstName.Name = "btnRemoveFirstName";
            this.btnRemoveFirstName.Size = new System.Drawing.Size(32, 23);
            this.btnRemoveFirstName.TabIndex = 21;
            this.btnRemoveFirstName.Text = "-";
            this.btnRemoveFirstName.UseVisualStyleBackColor = true;
            this.btnRemoveFirstName.Click += new System.EventHandler(this.btnRemoveFirstName_Click);
            // 
            // btnAddFirstName
            // 
            this.btnAddFirstName.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.btnAddFirstName.Location = new System.Drawing.Point(638, 2);
            this.btnAddFirstName.Name = "btnAddFirstName";
            this.btnAddFirstName.Size = new System.Drawing.Size(32, 23);
            this.btnAddFirstName.TabIndex = 20;
            this.btnAddFirstName.Text = "+";
            this.btnAddFirstName.UseVisualStyleBackColor = true;
            this.btnAddFirstName.Click += new System.EventHandler(this.btnAddFirstName_Click);
            // 
            // txtFirstName
            // 
            this.txtFirstName.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.txtFirstName.Location = new System.Drawing.Point(526, 3);
            this.txtFirstName.Name = "txtFirstName";
            this.txtFirstName.Size = new System.Drawing.Size(109, 24);
            this.txtFirstName.TabIndex = 19;
            // 
            // lbFirstName
            // 
            this.lbFirstName.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.lbFirstName.FormattingEnabled = true;
            this.lbFirstName.ItemHeight = 18;
            this.lbFirstName.Location = new System.Drawing.Point(526, 27);
            this.lbFirstName.Name = "lbFirstName";
            this.lbFirstName.Size = new System.Drawing.Size(175, 184);
            this.lbFirstName.TabIndex = 18;
            // 
            // cbMinimizeChrome
            // 
            this.cbMinimizeChrome.AutoSize = true;
            this.cbMinimizeChrome.Location = new System.Drawing.Point(132, 98);
            this.cbMinimizeChrome.Name = "cbMinimizeChrome";
            this.cbMinimizeChrome.Size = new System.Drawing.Size(18, 17);
            this.cbMinimizeChrome.TabIndex = 17;
            this.cbMinimizeChrome.UseVisualStyleBackColor = true;
            this.cbMinimizeChrome.CheckedChanged += new System.EventHandler(this.SettingValueChanged);
            // 
            // label10
            // 
            this.label10.AutoSize = true;
            this.label10.Location = new System.Drawing.Point(7, 97);
            this.label10.Name = "label10";
            this.label10.Size = new System.Drawing.Size(125, 18);
            this.label10.TabIndex = 16;
            this.label10.Text = "Minimize Chrome";
            // 
            // cbHideChrome
            // 
            this.cbHideChrome.AutoSize = true;
            this.cbHideChrome.Location = new System.Drawing.Point(132, 72);
            this.cbHideChrome.Name = "cbHideChrome";
            this.cbHideChrome.Size = new System.Drawing.Size(18, 17);
            this.cbHideChrome.TabIndex = 15;
            this.cbHideChrome.UseVisualStyleBackColor = true;
            this.cbHideChrome.CheckedChanged += new System.EventHandler(this.SettingValueChanged);
            // 
            // label9
            // 
            this.label9.AutoSize = true;
            this.label9.Location = new System.Drawing.Point(7, 71);
            this.label9.Name = "label9";
            this.label9.Size = new System.Drawing.Size(83, 18);
            this.label9.TabIndex = 14;
            this.label9.Text = "Ẩn Chrome";
            // 
            // tpLDPlayerSetting
            // 
            this.tpLDPlayerSetting.Controls.Add(this.label11);
            this.tpLDPlayerSetting.Controls.Add(this.btnSetupLDPlayer);
            this.tpLDPlayerSetting.Controls.Add(this.nudNoLDPlayerDevices);
            this.tpLDPlayerSetting.Controls.Add(this.label13);
            this.tpLDPlayerSetting.Controls.Add(this.btnChooseBaseLDPlayer);
            this.tpLDPlayerSetting.Controls.Add(this.label12);
            this.tpLDPlayerSetting.Controls.Add(this.txtLDPlayerBase);
            this.tpLDPlayerSetting.Controls.Add(this.txtLDPlayerRootPath);
            this.tpLDPlayerSetting.Location = new System.Drawing.Point(4, 27);
            this.tpLDPlayerSetting.Name = "tpLDPlayerSetting";
            this.tpLDPlayerSetting.Padding = new System.Windows.Forms.Padding(3);
            this.tpLDPlayerSetting.Size = new System.Drawing.Size(888, 214);
            this.tpLDPlayerSetting.TabIndex = 1;
            this.tpLDPlayerSetting.Text = "Cài đặt LDPlayer";
            this.tpLDPlayerSetting.UseVisualStyleBackColor = true;
            // 
            // label11
            // 
            this.label11.AutoSize = true;
            this.label11.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label11.Location = new System.Drawing.Point(7, 86);
            this.label11.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label11.Name = "label11";
            this.label11.Size = new System.Drawing.Size(104, 18);
            this.label11.TabIndex = 18;
            this.label11.Text = "LDPlayer base";
            // 
            // btnSetupLDPlayer
            // 
            this.btnSetupLDPlayer.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnSetupLDPlayer.Location = new System.Drawing.Point(7, 175);
            this.btnSetupLDPlayer.Margin = new System.Windows.Forms.Padding(4);
            this.btnSetupLDPlayer.Name = "btnSetupLDPlayer";
            this.btnSetupLDPlayer.Size = new System.Drawing.Size(123, 32);
            this.btnSetupLDPlayer.TabIndex = 15;
            this.btnSetupLDPlayer.Text = "Cài đặt";
            this.btnSetupLDPlayer.UseVisualStyleBackColor = true;
            this.btnSetupLDPlayer.Click += new System.EventHandler(this.btnSetupLDPlayer_Click);
            // 
            // nudNoLDPlayerDevices
            // 
            this.nudNoLDPlayerDevices.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.nudNoLDPlayerDevices.Location = new System.Drawing.Point(158, 46);
            this.nudNoLDPlayerDevices.Margin = new System.Windows.Forms.Padding(4);
            this.nudNoLDPlayerDevices.Maximum = new decimal(new int[] {
            8,
            0,
            0,
            0});
            this.nudNoLDPlayerDevices.Minimum = new decimal(new int[] {
            1,
            0,
            0,
            0});
            this.nudNoLDPlayerDevices.Name = "nudNoLDPlayerDevices";
            this.nudNoLDPlayerDevices.Size = new System.Drawing.Size(60, 24);
            this.nudNoLDPlayerDevices.TabIndex = 14;
            this.nudNoLDPlayerDevices.Value = new decimal(new int[] {
            2,
            0,
            0,
            0});
            this.nudNoLDPlayerDevices.ValueChanged += new System.EventHandler(this.SettingValueChanged);
            // 
            // label13
            // 
            this.label13.AutoSize = true;
            this.label13.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label13.Location = new System.Drawing.Point(7, 13);
            this.label13.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label13.Name = "label13";
            this.label13.Size = new System.Drawing.Size(99, 18);
            this.label13.TabIndex = 11;
            this.label13.Text = "LDPlayer root";
            // 
            // btnChooseBaseLDPlayer
            // 
            this.btnChooseBaseLDPlayer.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnChooseBaseLDPlayer.Location = new System.Drawing.Point(413, 81);
            this.btnChooseBaseLDPlayer.Margin = new System.Windows.Forms.Padding(4);
            this.btnChooseBaseLDPlayer.Name = "btnChooseBaseLDPlayer";
            this.btnChooseBaseLDPlayer.Size = new System.Drawing.Size(63, 24);
            this.btnChooseBaseLDPlayer.TabIndex = 17;
            this.btnChooseBaseLDPlayer.Text = "Chọn";
            this.btnChooseBaseLDPlayer.UseVisualStyleBackColor = true;
            this.btnChooseBaseLDPlayer.Click += new System.EventHandler(this.btnChooseBaseLDPlayer_Click);
            // 
            // label12
            // 
            this.label12.AutoSize = true;
            this.label12.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label12.Location = new System.Drawing.Point(7, 49);
            this.label12.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label12.Name = "label12";
            this.label12.Size = new System.Drawing.Size(150, 18);
            this.label12.TabIndex = 13;
            this.label12.Text = "No. LDPlayer devices";
            // 
            // txtLDPlayerBase
            // 
            this.txtLDPlayerBase.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtLDPlayerBase.Location = new System.Drawing.Point(158, 82);
            this.txtLDPlayerBase.Margin = new System.Windows.Forms.Padding(4);
            this.txtLDPlayerBase.Name = "txtLDPlayerBase";
            this.txtLDPlayerBase.ReadOnly = true;
            this.txtLDPlayerBase.Size = new System.Drawing.Size(248, 24);
            this.txtLDPlayerBase.TabIndex = 16;
            // 
            // txtLDPlayerRootPath
            // 
            this.txtLDPlayerRootPath.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtLDPlayerRootPath.Location = new System.Drawing.Point(158, 8);
            this.txtLDPlayerRootPath.Margin = new System.Windows.Forms.Padding(4);
            this.txtLDPlayerRootPath.Name = "txtLDPlayerRootPath";
            this.txtLDPlayerRootPath.Size = new System.Drawing.Size(320, 24);
            this.txtLDPlayerRootPath.TabIndex = 12;
            this.txtLDPlayerRootPath.Text = "E:\\ChangZhi\\LDPlayer";
            this.txtLDPlayerRootPath.TextChanged += new System.EventHandler(this.SettingValueChanged);
            this.txtLDPlayerRootPath.DoubleClick += new System.EventHandler(this.txtLDPlayerRootPath_DoubleClick);
            // 
            // tpMEmuSetting
            // 
            this.tpMEmuSetting.Controls.Add(this.label7);
            this.tpMEmuSetting.Controls.Add(this.btnSetupMEmu);
            this.tpMEmuSetting.Controls.Add(this.nudNoMEmuDevices);
            this.tpMEmuSetting.Controls.Add(this.label2);
            this.tpMEmuSetting.Controls.Add(this.label4);
            this.tpMEmuSetting.Controls.Add(this.btnChooseBaseMEmu);
            this.tpMEmuSetting.Controls.Add(this.txtMEmuZipBase);
            this.tpMEmuSetting.Controls.Add(this.txtMEmuRootPath);
            this.tpMEmuSetting.Location = new System.Drawing.Point(4, 27);
            this.tpMEmuSetting.Name = "tpMEmuSetting";
            this.tpMEmuSetting.Padding = new System.Windows.Forms.Padding(3);
            this.tpMEmuSetting.Size = new System.Drawing.Size(888, 214);
            this.tpMEmuSetting.TabIndex = 2;
            this.tpMEmuSetting.Text = "Cài đặt MEmu";
            this.tpMEmuSetting.UseVisualStyleBackColor = true;
            // 
            // tbLogs
            // 
            this.tbLogs.Controls.Add(this.label14);
            this.tbLogs.Controls.Add(this.txtRegisterLogs);
            this.tbLogs.Location = new System.Drawing.Point(4, 27);
            this.tbLogs.Name = "tbLogs";
            this.tbLogs.Padding = new System.Windows.Forms.Padding(3);
            this.tbLogs.Size = new System.Drawing.Size(888, 214);
            this.tbLogs.TabIndex = 3;
            this.tbLogs.Text = "Logs";
            this.tbLogs.UseVisualStyleBackColor = true;
            // 
            // label14
            // 
            this.label14.AutoSize = true;
            this.label14.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label14.Location = new System.Drawing.Point(4, 5);
            this.label14.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label14.Name = "label14";
            this.label14.Size = new System.Drawing.Size(95, 18);
            this.label14.TabIndex = 17;
            this.label14.Text = "Register logs";
            // 
            // txtRegisterLogs
            // 
            this.txtRegisterLogs.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.txtRegisterLogs.BackColor = System.Drawing.SystemColors.ControlLightLight;
            this.txtRegisterLogs.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtRegisterLogs.Location = new System.Drawing.Point(4, 27);
            this.txtRegisterLogs.Margin = new System.Windows.Forms.Padding(4);
            this.txtRegisterLogs.Multiline = true;
            this.txtRegisterLogs.Name = "txtRegisterLogs";
            this.txtRegisterLogs.ReadOnly = true;
            this.txtRegisterLogs.ScrollBars = System.Windows.Forms.ScrollBars.Both;
            this.txtRegisterLogs.Size = new System.Drawing.Size(876, 180);
            this.txtRegisterLogs.TabIndex = 17;
            this.txtRegisterLogs.WordWrap = false;
            // 
            // gbSetting
            // 
            this.gbSetting.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.gbSetting.Controls.Add(this.tabSetting);
            this.gbSetting.Controls.Add(this.btnResetSetting);
            this.gbSetting.Controls.Add(this.btnSaveSetting);
            this.gbSetting.Controls.Add(this.button1);
            this.gbSetting.Controls.Add(this.button2);
            this.gbSetting.Location = new System.Drawing.Point(10, 7);
            this.gbSetting.Name = "gbSetting";
            this.gbSetting.Size = new System.Drawing.Size(909, 317);
            this.gbSetting.TabIndex = 16;
            this.gbSetting.TabStop = false;
            this.gbSetting.Text = "Cặt đặt";
            // 
            // btnReloadDevices
            // 
            this.btnReloadDevices.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.btnReloadDevices.Location = new System.Drawing.Point(280, 2);
            this.btnReloadDevices.Name = "btnReloadDevices";
            this.btnReloadDevices.Size = new System.Drawing.Size(117, 23);
            this.btnReloadDevices.TabIndex = 31;
            this.btnReloadDevices.Text = "Reload devices";
            this.btnReloadDevices.UseVisualStyleBackColor = true;
            this.btnReloadDevices.Click += new System.EventHandler(this.btnReloadDevices_Click);
            // 
            // btnChooseAll
            // 
            this.btnChooseAll.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.btnChooseAll.Location = new System.Drawing.Point(403, 2);
            this.btnChooseAll.Name = "btnChooseAll";
            this.btnChooseAll.Size = new System.Drawing.Size(117, 23);
            this.btnChooseAll.TabIndex = 32;
            this.btnChooseAll.Text = "Dùng tất cả";
            this.btnChooseAll.UseVisualStyleBackColor = true;
            this.btnChooseAll.Click += new System.EventHandler(this.btnChooseAll_Click);
            // 
            // indexDataGridViewTextBoxColumn
            // 
            this.indexDataGridViewTextBoxColumn.DataPropertyName = "Index";
            dataGridViewCellStyle1.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleCenter;
            this.indexDataGridViewTextBoxColumn.DefaultCellStyle = dataGridViewCellStyle1;
            this.indexDataGridViewTextBoxColumn.HeaderText = "STT";
            this.indexDataGridViewTextBoxColumn.MinimumWidth = 6;
            this.indexDataGridViewTextBoxColumn.Name = "indexDataGridViewTextBoxColumn";
            this.indexDataGridViewTextBoxColumn.Width = 50;
            // 
            // chooseDataGridViewCheckBoxColumn
            // 
            this.chooseDataGridViewCheckBoxColumn.DataPropertyName = "Choose";
            this.chooseDataGridViewCheckBoxColumn.HeaderText = "Chọn";
            this.chooseDataGridViewCheckBoxColumn.MinimumWidth = 6;
            this.chooseDataGridViewCheckBoxColumn.Name = "chooseDataGridViewCheckBoxColumn";
            this.chooseDataGridViewCheckBoxColumn.Width = 50;
            // 
            // idDataGridViewTextBoxColumn
            // 
            this.idDataGridViewTextBoxColumn.DataPropertyName = "Id";
            this.idDataGridViewTextBoxColumn.HeaderText = "id";
            this.idDataGridViewTextBoxColumn.MinimumWidth = 6;
            this.idDataGridViewTextBoxColumn.Name = "idDataGridViewTextBoxColumn";
            this.idDataGridViewTextBoxColumn.Width = 50;
            // 
            // nameDataGridViewTextBoxColumn
            // 
            this.nameDataGridViewTextBoxColumn.DataPropertyName = "Name";
            this.nameDataGridViewTextBoxColumn.HeaderText = "Tên máy ảo";
            this.nameDataGridViewTextBoxColumn.MinimumWidth = 6;
            this.nameDataGridViewTextBoxColumn.Name = "nameDataGridViewTextBoxColumn";
            this.nameDataGridViewTextBoxColumn.Width = 125;
            // 
            // emulatorInfoBindingSource
            // 
            this.emulatorInfoBindingSource.DataSource = typeof(AutoTool.Models.EmulatorInfo);
            // 
            // Main
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(9F, 18F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(925, 691);
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
            ((System.ComponentModel.ISupportInitialize)(this.dgvListDevices)).EndInit();
            this.tpLDPlayerSetting.ResumeLayout(false);
            this.tpLDPlayerSetting.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.nudNoLDPlayerDevices)).EndInit();
            this.tpMEmuSetting.ResumeLayout(false);
            this.tpMEmuSetting.PerformLayout();
            this.tbLogs.ResumeLayout(false);
            this.tbLogs.PerformLayout();
            this.gbSetting.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.emulatorInfoBindingSource)).EndInit();
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
        private System.Windows.Forms.TabPage tpLDPlayerSetting;
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
        private System.Windows.Forms.RadioButton rbUseMEmu;
        private System.Windows.Forms.RadioButton rbUseLDPLayer;
        private System.Windows.Forms.TabPage tpMEmuSetting;
        private System.Windows.Forms.Label label11;
        private System.Windows.Forms.Button btnSetupLDPlayer;
        private System.Windows.Forms.Button btnChooseBaseLDPlayer;
        private System.Windows.Forms.TextBox txtLDPlayerRootPath;
        private System.Windows.Forms.TextBox txtLDPlayerBase;
        private System.Windows.Forms.Label label12;
        private System.Windows.Forms.Label label13;
        private System.Windows.Forms.NumericUpDown nudNoLDPlayerDevices;
        private System.Windows.Forms.TabPage tbLogs;
        private System.Windows.Forms.TextBox txtRegisterLogs;
        private System.Windows.Forms.Label label14;
        private System.Windows.Forms.TextBox txtOutputDirectory;
        private System.Windows.Forms.Label label15;
        private System.Windows.Forms.DataGridView dgvListDevices;
        private System.Windows.Forms.BindingSource emulatorInfoBindingSource;
        private System.Windows.Forms.Button btnReloadDevices;
        private System.Windows.Forms.DataGridViewTextBoxColumn indexDataGridViewTextBoxColumn;
        private System.Windows.Forms.DataGridViewCheckBoxColumn chooseDataGridViewCheckBoxColumn;
        private System.Windows.Forms.DataGridViewTextBoxColumn idDataGridViewTextBoxColumn;
        private System.Windows.Forms.DataGridViewTextBoxColumn nameDataGridViewTextBoxColumn;
        private System.Windows.Forms.Button btnChooseAll;
    }
}