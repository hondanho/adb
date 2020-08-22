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
            this.btnStart = new System.Windows.Forms.Button();
            this.label1 = new System.Windows.Forms.Label();
            this.gbGeneralSetting = new System.Windows.Forms.GroupBox();
            this.nudThreadNo = new System.Windows.Forms.NumericUpDown();
            this.label3 = new System.Windows.Forms.Label();
            this.btnSetupMEmu = new System.Windows.Forms.Button();
            this.nupNoMEmuDevices = new System.Windows.Forms.NumericUpDown();
            this.label4 = new System.Windows.Forms.Label();
            this.txtMEmuRootPath = new System.Windows.Forms.TextBox();
            this.label2 = new System.Windows.Forms.Label();
            this.label5 = new System.Windows.Forms.Label();
            this.txtSuccess = new System.Windows.Forms.TextBox();
            this.txtFail = new System.Windows.Forms.TextBox();
            this.label6 = new System.Windows.Forms.Label();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.label7 = new System.Windows.Forms.Label();
            this.btnChooseBaseMEmu = new System.Windows.Forms.Button();
            this.txtMEmuZipBase = new System.Windows.Forms.TextBox();
            this.btnStop = new System.Windows.Forms.Button();
            this.button1 = new System.Windows.Forms.Button();
            this.button2 = new System.Windows.Forms.Button();
            this.lblStatus = new System.Windows.Forms.Label();
            this.cbTurn2faOn = new System.Windows.Forms.CheckBox();
            this.label8 = new System.Windows.Forms.Label();
            this.gbGeneralSetting.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.nudThreadNo)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.nupNoMEmuDevices)).BeginInit();
            this.groupBox1.SuspendLayout();
            this.SuspendLayout();
            // 
            // btnStart
            // 
            this.btnStart.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnStart.ForeColor = System.Drawing.Color.RoyalBlue;
            this.btnStart.Location = new System.Drawing.Point(18, 264);
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
            // gbGeneralSetting
            // 
            this.gbGeneralSetting.Controls.Add(this.label8);
            this.gbGeneralSetting.Controls.Add(this.cbTurn2faOn);
            this.gbGeneralSetting.Controls.Add(this.button2);
            this.gbGeneralSetting.Controls.Add(this.button1);
            this.gbGeneralSetting.Controls.Add(this.nudThreadNo);
            this.gbGeneralSetting.Controls.Add(this.label3);
            this.gbGeneralSetting.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.gbGeneralSetting.Location = new System.Drawing.Point(18, 17);
            this.gbGeneralSetting.Margin = new System.Windows.Forms.Padding(4);
            this.gbGeneralSetting.Name = "gbGeneralSetting";
            this.gbGeneralSetting.Padding = new System.Windows.Forms.Padding(4);
            this.gbGeneralSetting.Size = new System.Drawing.Size(498, 240);
            this.gbGeneralSetting.TabIndex = 4;
            this.gbGeneralSetting.TabStop = false;
            this.gbGeneralSetting.Text = "Cài đặt chung";
            // 
            // nudThreadNo
            // 
            this.nudThreadNo.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.nudThreadNo.Location = new System.Drawing.Point(112, 33);
            this.nudThreadNo.Margin = new System.Windows.Forms.Padding(4);
            this.nudThreadNo.Maximum = new decimal(new int[] {
            4,
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
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label3.Location = new System.Drawing.Point(15, 35);
            this.label3.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(67, 18);
            this.label3.TabIndex = 2;
            this.label3.Text = "Số luồng";
            // 
            // btnSetupMEmu
            // 
            this.btnSetupMEmu.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnSetupMEmu.Location = new System.Drawing.Point(24, 186);
            this.btnSetupMEmu.Margin = new System.Windows.Forms.Padding(4);
            this.btnSetupMEmu.Name = "btnSetupMEmu";
            this.btnSetupMEmu.Size = new System.Drawing.Size(123, 32);
            this.btnSetupMEmu.TabIndex = 7;
            this.btnSetupMEmu.Text = "Cài đặt";
            this.btnSetupMEmu.UseVisualStyleBackColor = true;
            this.btnSetupMEmu.Click += new System.EventHandler(this.btnSetupMEmu_Click);
            // 
            // nupNoMEmuDevices
            // 
            this.nupNoMEmuDevices.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.nupNoMEmuDevices.Location = new System.Drawing.Point(168, 68);
            this.nupNoMEmuDevices.Margin = new System.Windows.Forms.Padding(4);
            this.nupNoMEmuDevices.Maximum = new decimal(new int[] {
            16,
            0,
            0,
            0});
            this.nupNoMEmuDevices.Minimum = new decimal(new int[] {
            1,
            0,
            0,
            0});
            this.nupNoMEmuDevices.Name = "nupNoMEmuDevices";
            this.nupNoMEmuDevices.Size = new System.Drawing.Size(60, 24);
            this.nupNoMEmuDevices.TabIndex = 6;
            this.nupNoMEmuDevices.Value = new decimal(new int[] {
            2,
            0,
            0,
            0});
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label4.Location = new System.Drawing.Point(19, 71);
            this.label4.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(134, 18);
            this.label4.TabIndex = 5;
            this.label4.Text = "No. MEmu devices";
            // 
            // txtMEmuRootPath
            // 
            this.txtMEmuRootPath.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtMEmuRootPath.Location = new System.Drawing.Point(168, 30);
            this.txtMEmuRootPath.Margin = new System.Windows.Forms.Padding(4);
            this.txtMEmuRootPath.Name = "txtMEmuRootPath";
            this.txtMEmuRootPath.Size = new System.Drawing.Size(320, 24);
            this.txtMEmuRootPath.TabIndex = 1;
            this.txtMEmuRootPath.Text = "D:\\Program Files\\Microvirt\\MEmu";
            this.txtMEmuRootPath.MouseDoubleClick += new System.Windows.Forms.MouseEventHandler(this.txtMEmuRootPath_MouseDoubleClick);
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label2.Location = new System.Drawing.Point(19, 35);
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
            this.label5.Location = new System.Drawing.Point(14, 326);
            this.label5.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(148, 18);
            this.label5.TabIndex = 5;
            this.label5.Text = "Successed Accounts";
            // 
            // txtSuccess
            // 
            this.txtSuccess.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtSuccess.Location = new System.Drawing.Point(18, 349);
            this.txtSuccess.Margin = new System.Windows.Forms.Padding(4);
            this.txtSuccess.Multiline = true;
            this.txtSuccess.Name = "txtSuccess";
            this.txtSuccess.ReadOnly = true;
            this.txtSuccess.ScrollBars = System.Windows.Forms.ScrollBars.Vertical;
            this.txtSuccess.Size = new System.Drawing.Size(496, 278);
            this.txtSuccess.TabIndex = 6;
            // 
            // txtFail
            // 
            this.txtFail.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtFail.Location = new System.Drawing.Point(539, 349);
            this.txtFail.Margin = new System.Windows.Forms.Padding(4);
            this.txtFail.Multiline = true;
            this.txtFail.Name = "txtFail";
            this.txtFail.ReadOnly = true;
            this.txtFail.ScrollBars = System.Windows.Forms.ScrollBars.Vertical;
            this.txtFail.Size = new System.Drawing.Size(499, 278);
            this.txtFail.TabIndex = 7;
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label6.Location = new System.Drawing.Point(534, 326);
            this.label6.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(113, 18);
            this.label6.TabIndex = 8;
            this.label6.Text = "Failed Accounts";
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.label7);
            this.groupBox1.Controls.Add(this.btnChooseBaseMEmu);
            this.groupBox1.Controls.Add(this.txtMEmuZipBase);
            this.groupBox1.Controls.Add(this.btnSetupMEmu);
            this.groupBox1.Controls.Add(this.nupNoMEmuDevices);
            this.groupBox1.Controls.Add(this.label2);
            this.groupBox1.Controls.Add(this.label4);
            this.groupBox1.Controls.Add(this.txtMEmuRootPath);
            this.groupBox1.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.groupBox1.Location = new System.Drawing.Point(539, 17);
            this.groupBox1.Margin = new System.Windows.Forms.Padding(4);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Padding = new System.Windows.Forms.Padding(4);
            this.groupBox1.Size = new System.Drawing.Size(500, 240);
            this.groupBox1.TabIndex = 8;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "Cài đặt MEmu";
            // 
            // label7
            // 
            this.label7.AutoSize = true;
            this.label7.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label7.Location = new System.Drawing.Point(19, 108);
            this.label7.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(111, 18);
            this.label7.TabIndex = 10;
            this.label7.Text = "MEmu zip base";
            // 
            // btnChooseBaseMEmu
            // 
            this.btnChooseBaseMEmu.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnChooseBaseMEmu.Location = new System.Drawing.Point(425, 100);
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
            this.txtMEmuZipBase.Location = new System.Drawing.Point(168, 104);
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
            this.btnStop.Location = new System.Drawing.Point(158, 264);
            this.btnStop.Margin = new System.Windows.Forms.Padding(4);
            this.btnStop.Name = "btnStop";
            this.btnStop.Size = new System.Drawing.Size(130, 46);
            this.btnStop.TabIndex = 9;
            this.btnStop.Text = "STOP";
            this.btnStop.UseVisualStyleBackColor = true;
            this.btnStop.Click += new System.EventHandler(this.btnStop_Click);
            // 
            // button1
            // 
            this.button1.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.button1.Location = new System.Drawing.Point(8, 136);
            this.button1.Name = "button1";
            this.button1.Size = new System.Drawing.Size(84, 26);
            this.button1.TabIndex = 10;
            this.button1.Text = "button1";
            this.button1.UseVisualStyleBackColor = true;
            this.button1.Click += new System.EventHandler(this.button1_Click);
            // 
            // button2
            // 
            this.button2.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.button2.Location = new System.Drawing.Point(112, 136);
            this.button2.Name = "button2";
            this.button2.Size = new System.Drawing.Size(84, 26);
            this.button2.TabIndex = 11;
            this.button2.Text = "button2";
            this.button2.UseVisualStyleBackColor = true;
            this.button2.Click += new System.EventHandler(this.button2_Click);
            // 
            // lblStatus
            // 
            this.lblStatus.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblStatus.Location = new System.Drawing.Point(296, 277);
            this.lblStatus.Name = "lblStatus";
            this.lblStatus.Size = new System.Drawing.Size(742, 26);
            this.lblStatus.TabIndex = 10;
            this.lblStatus.Text = "lblStatus";
            // 
            // cbTurn2faOn
            // 
            this.cbTurn2faOn.AutoSize = true;
            this.cbTurn2faOn.Checked = true;
            this.cbTurn2faOn.CheckState = System.Windows.Forms.CheckState.Checked;
            this.cbTurn2faOn.Location = new System.Drawing.Point(112, 64);
            this.cbTurn2faOn.Name = "cbTurn2faOn";
            this.cbTurn2faOn.Size = new System.Drawing.Size(18, 17);
            this.cbTurn2faOn.TabIndex = 12;
            this.cbTurn2faOn.UseVisualStyleBackColor = true;
            // 
            // label8
            // 
            this.label8.AutoSize = true;
            this.label8.Location = new System.Drawing.Point(15, 65);
            this.label8.Name = "label8";
            this.label8.Size = new System.Drawing.Size(60, 18);
            this.label8.TabIndex = 13;
            this.label8.Text = "Bật 2FA";
            // 
            // Main
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(9F, 18F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1053, 642);
            this.Controls.Add(this.lblStatus);
            this.Controls.Add(this.btnStop);
            this.Controls.Add(this.groupBox1);
            this.Controls.Add(this.label6);
            this.Controls.Add(this.txtFail);
            this.Controls.Add(this.txtSuccess);
            this.Controls.Add(this.label5);
            this.Controls.Add(this.gbGeneralSetting);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.btnStart);
            this.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.Margin = new System.Windows.Forms.Padding(4);
            this.MaximumSize = new System.Drawing.Size(1071, 689);
            this.MinimumSize = new System.Drawing.Size(1071, 689);
            this.Name = "Main";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Main";
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.Main_FormClosing);
            this.gbGeneralSetting.ResumeLayout(false);
            this.gbGeneralSetting.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.nudThreadNo)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.nupNoMEmuDevices)).EndInit();
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion
        private System.Windows.Forms.Button btnStart;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.GroupBox gbGeneralSetting;
        private System.Windows.Forms.TextBox txtMEmuRootPath;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.NumericUpDown nudThreadNo;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Button btnSetupMEmu;
        private System.Windows.Forms.NumericUpDown nupNoMEmuDevices;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.TextBox txtSuccess;
        private System.Windows.Forms.TextBox txtFail;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.Label label7;
        private System.Windows.Forms.Button btnChooseBaseMEmu;
        private System.Windows.Forms.TextBox txtMEmuZipBase;
        private System.Windows.Forms.Button btnStop;
        private System.Windows.Forms.Button button1;
        private System.Windows.Forms.Button button2;
        private System.Windows.Forms.Label lblStatus;
        private System.Windows.Forms.Label label8;
        private System.Windows.Forms.CheckBox cbTurn2faOn;
    }
}