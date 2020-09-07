using System;
using System.Collections.Generic;
using System.IO;
using System.IO.Compression;
using System.Linq;
using System.Reflection;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;
using AutoTool.AutoHelper;
using AutoTool.AutoMethods;
using AutoTool.Models;
using AutoTool.Properties;
using log4net;
using AutoTool.AutoCommons;
using System.Data;
using AutoTool.Network;

namespace AutoTool
{
    public partial class Main : Form, IDisposable
    {
        private ILog _log;
        private string _pathFileFirstName = Environment.CurrentDirectory + "\\source\\data\\firstName.txt";
        private string _pathFileLastName = Environment.CurrentDirectory + "\\source\\data\\lastName.txt";
        public delegate void ShowLog(string message);
        public delegate void LogInfo(string info);
        private bool _regFbIsRunning = false;
        private decimal _numberOfThread = 0;
        private List<Thread> _RegisFbThreads = new List<Thread>();
        private bool SettingInitialized = false;
        private bool SettingChanged = false;
        private List<EmulatorInfo> devices = new List<EmulatorInfo>();
        private List<AutoNetwork> autoNetworks = new List<AutoNetwork>()
        {
            new AutoNetwork(0, "Không dùng", AutoNetworkType.None),
            new AutoNetwork(1, "Dcom", AutoNetworkType.Dcom),
            new AutoNetwork(2, "Proxy", AutoNetworkType.Proxy),
            new AutoNetwork(3, "OneDotOne", AutoNetworkType.OneDotOne),
        };
        static object proxiesLock = new object();
        private AutoNetworkType _AutoNetworkType;

        public Main()
        {
            InitializeComponent();
            this.button1.Visible = false;
            this.button2.Visible = false;

            GlobalVar.ListFirstName = FunctionHelper.ReadAllTextFromFile(_pathFileFirstName);
            GlobalVar.ListLastName = FunctionHelper.ReadAllTextFromFile(_pathFileLastName);
            this.lbFirstName.Items.AddRange(GlobalVar.ListFirstName);
            this.lbLastName.Items.AddRange(GlobalVar.ListLastName);

            // init network 
            this.cbbAutoNetwork.DataSource = autoNetworks;

            InitSetting();

            this.lblStatus.Text = "Stopped";
            GlobalVar.MEmuWorkingDirectory = this.txtMEmuRootPath.Text;
            GlobalVar.LDPlayerWorkingDirectory = this.txtLDPlayerRootPath.Text;
            GlobalVar.OutputDirectory = this.txtOutputDirectory.Text;

            if (!Directory.Exists(GlobalVar.OutputDirectory))
            {
                Directory.CreateDirectory(GlobalVar.OutputDirectory);
            }
            this._log = LogManager.GetLogger(MethodBase.GetCurrentMethod().DeclaringType);

            if (!File.Exists(GlobalVar.OutputDirectory + Constant.ListUsedEmailPath))
            {
                File.WriteAllText(GlobalVar.OutputDirectory + Constant.ListUsedEmailPath, string.Empty);
            }
            if (!File.Exists(GlobalVar.OutputDirectory + Constant.ProxiesPath))
            {
                File.WriteAllText(GlobalVar.OutputDirectory + Constant.ProxiesPath, string.Empty);
            }
            if (!File.Exists(GlobalVar.OutputDirectory + Constant.ProxiesCounterPath))
            {
                //File.Create(GlobalVar.OutputDirectory + Constant.ProxiesCounterPath);
                File.WriteAllText(GlobalVar.OutputDirectory + Constant.ProxiesCounterPath, "0");
            }
            if (!File.Exists(GlobalVar.OutputDirectory + Constant.EmailsPath))
            {
                File.WriteAllText(GlobalVar.OutputDirectory + Constant.EmailsPath, string.Empty);
            }
            if (!File.Exists(GlobalVar.OutputDirectory + Constant.EmailsCounterPath))
            {
                File.WriteAllText(GlobalVar.OutputDirectory + Constant.EmailsCounterPath, "0");
            }
            if (!File.Exists(GlobalVar.OutputDirectory + Constant.ListSuccessAccountPath))
            {
                File.WriteAllText(GlobalVar.OutputDirectory + Constant.ListSuccessAccountPath, string.Empty);
            }
            if (!File.Exists(GlobalVar.OutputDirectory + Constant.ListFailureAccountPath))
            {
                File.WriteAllText(GlobalVar.OutputDirectory + Constant.ListFailureAccountPath, string.Empty);
            }
            if (!File.Exists(GlobalVar.OutputDirectory + Constant.DcomIpPath))
            {
                File.WriteAllText(GlobalVar.OutputDirectory + Constant.DcomIpPath, string.Empty);
            }
            if (!File.Exists(GlobalVar.OutputDirectory + Constant.DcomIpBlackListPath))
            {
                File.WriteAllText(GlobalVar.OutputDirectory + Constant.DcomIpBlackListPath, string.Empty);
            }

            SettingProxies();

            // get ip
            this.lblIpAddress.Text = DcomChanger.GetIP(true);

            var networkType = (AutoNetworkType)this.cbbAutoNetwork.SelectedValue;
            this.txtDcomNetworkName.Enabled = (networkType == AutoNetworkType.Dcom);

            this.rbUseLDPLayer.CheckedChanged += new EventHandler(this.ListRbCheckedChange);
            this.cbbAutoNetwork.SelectedIndexChanged += new EventHandler(this.cbbAutoNetwork_SelectedIndexChanged);
            // list devices
            LoadDevices();
        }

        private void cbbAutoNetwork_SelectedIndexChanged(object sender, EventArgs e)
        {
            var networkType = (AutoNetworkType)this.cbbAutoNetwork.SelectedValue;
            this.txtDcomNetworkName.Enabled = (networkType == AutoNetworkType.Dcom);
        }

        private static void SettingProxies()
        {
            GlobalVar.Proxies = File.ReadAllLines(GlobalVar.OutputDirectory + Constant.ProxiesPath);
            string proxiesCounter = File.ReadAllText(GlobalVar.OutputDirectory + Constant.ProxiesCounterPath);
            if (int.TryParse(proxiesCounter, out var pc))
            {
                GlobalVar.ProxiesCounter = pc;
            }
            else
            {
                GlobalVar.ProxiesCounter = 0;
            }
        }

        public void LogTrace(string message)
        {
            if (this.txtRegisterLogs.InvokeRequired)
            {
                RegFb.LogTrace d = new RegFb.LogTrace(LogTrace);
                this.Invoke(d, new object[] { message });
            }
            else
            {
                this.txtRegisterLogs.AppendText(message + "\r\n");
            }
        }

        static public void Info(string s)
        {
            MessageBox.Show(s, "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
        }

        static public void Warning(string s)
        {
            MessageBox.Show(s, "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
        }

        private void LoadDevices()
        {
            IEmulatorFunc emulatorFunc = GetEmulatorFunc();
            devices = emulatorFunc.GetDevices();
            this.dgvListDevices.DataSource = devices;
        }

        private IEmulatorFunc GetEmulatorFunc()
        {
            IEmulatorFunc emulatorFunc;
            if (this.rbUseMEmu.Checked)
                emulatorFunc = new MEmuFunc();
            else
                emulatorFunc = new LDPlayerFunc();
            return emulatorFunc;
        }

        private void ListRbCheckedChange(object sender, EventArgs e)
        {
            LoadDevices();
        }

        #region Register Facebook Clone

        private void btnStart_Click(object sender, EventArgs e)
        {
            _AutoNetworkType = (AutoNetworkType)this.cbbAutoNetwork.SelectedValue;
            IEmulatorFunc emulatorFunc = GetEmulatorFunc();
            try
            {
                if (AutoNetworkType.Proxy == (AutoNetworkType)this.cbbAutoNetwork.SelectedValue)
                {
                    SettingProxies();
                    if (GlobalVar.Proxies == null || GlobalVar.Proxies.Length <= 0)
                    {
                        Warning("Không có proxy nào trong danh sách");
                        return;
                    }
                }
                if (this.cbUseMailServer.Checked)
                {
                    var lstEmails = File.ReadAllLines(GlobalVar.OutputDirectory + Constant.EmailsPath);
                    GlobalVar.Emails = lstEmails;
                }

                // get choosed devices
                var choosedDevices = devices.Where(d => d.Choose).ToList();

                if (choosedDevices == null || choosedDevices.Count <= 0)
                {
                    Warning("Vui lòng chọn máy ảo để thực hiện.");
                    return;
                }

                // Caculate number of threads
                _numberOfThread = Math.Min(this.nudThreadNo.Value, choosedDevices.Count);

                if (_numberOfThread > 0)
                {
                    if (AutoNetworkType.Dcom == (AutoNetworkType)this.cbbAutoNetwork.SelectedValue)
                    {
                        _numberOfThread = 1;
                    }
                    _regFbIsRunning = true;
                    _SetStartUI();

                    for (var i = 0; i < _numberOfThread; i++)
                    {
                        var device = choosedDevices[i];
                        device.Index = i + 1;
                        var t = new Thread((d) =>
                        {
                            for (; ; )
                            {
                                RegisterAccountFacebook((EmulatorInfo)d);
                                Thread.Sleep(500);
                            }
                        })
                        { Name = "Facebook Clone Register Thread " + (i + 1) };
                        _RegisFbThreads.Add(t);
                        t.Start(device);
                    }
                }
            }
            catch (Exception ex)
            {
                Warning("Có lỗi xảy ra");
                _log.Error(ex.Message);
                AbortRegisFbThreads();
                _SetStopUI();
                _regFbIsRunning = false;
            }
        }

        private void btnStop_Click(object sender, EventArgs e)
        {
            var confirm = MessageBox.Show(this, "Bạn có chắc chắn muốn dừng không?", "Xác nhận", MessageBoxButtons.YesNo);

            if (confirm == DialogResult.Yes)
            {
                AbortRegisFbThreads();
                _SetStopUI();
                _regFbIsRunning = false;
            }
        }

        private void _SetStartUI()
        {
            this.btnStart.Enabled = false;
            this.btnStop.Enabled = true;
            this.nudThreadNo.Enabled = false;
            this.cbMinimizeChrome.Enabled = false;
            this.cbHideChrome.Enabled = false;
            this.cbTurn2faOn.Enabled = false;
            this.rbUseLDPLayer.Enabled = false;
            this.rbUseMEmu.Enabled = false;
            this.txtOutputDirectory.Enabled = false;
            //this.cbUseProxy.Enabled = false;
            this.cbUseMailServer.Enabled = false;
            this.cbbAutoNetwork.Enabled = false;
            this.txtDcomNetworkName.Enabled = false;
            this.lblStatus.Text = "Running with " + _numberOfThread + " threads";
        }

        private void _SetStopUI()
        {
            this.btnStart.Enabled = true;
            this.btnStop.Enabled = false;
            this.nudThreadNo.Enabled = true;
            this.cbMinimizeChrome.Enabled = true;
            this.cbHideChrome.Enabled = true;
            this.cbTurn2faOn.Enabled = true;
            this.rbUseLDPLayer.Enabled = true;
            this.rbUseMEmu.Enabled = true;
            this.txtOutputDirectory.Enabled = true;
            //this.cbUseProxy.Enabled = true;
            this.cbUseMailServer.Enabled = true;
            this.cbbAutoNetwork.Enabled = true;
            var networkType = (AutoNetworkType)this.cbbAutoNetwork.SelectedValue;
            this.txtDcomNetworkName.Enabled = (networkType == AutoNetworkType.Dcom);
            this.lblStatus.Text = "Stopped";
            this.lblIpAddress.Text = DcomChanger.GetIP(true);
        }

        private void AbortRegisFbThreads()
        {
            while (_RegisFbThreads.Count > 0)
            {

                _RegisFbThreads[0].Abort();
                _RegisFbThreads.RemoveAt(0);
            }
        }

        private void RegisterAccountFacebook(EmulatorInfo device)
        {
            // Dcom Changer
            string ipAddress = string.Empty;
            this.Invoke(new Action(() =>
            {
                this.lblIpAddress.Text = "Changing..";
            }));

            while (true)
            {
                if (DcomChanger.ChangeIP("Viettel", out ipAddress))
                {
                    this.Invoke(new Action<string>((ip) =>
                    {
                        this.lblIpAddress.Text = ip;
                    }), ipAddress);
                    break;
                }
            }
            var regFbConfig = new RegFbConfig();
            regFbConfig.HideChrome = this.cbHideChrome.Checked;
            regFbConfig.MinimizeChrome = this.cbMinimizeChrome.Checked;
            regFbConfig.RegFbNetworkType = _AutoNetworkType;
            regFbConfig.UseEmailServer = this.cbUseMailServer.Checked;
            regFbConfig.Turn2FaOn = this.cbTurn2faOn.Checked;
            if (this.rbUseMEmu.Checked)
                regFbConfig.RegType = RegFbType.REG_WITH_MEMU;
            else
                regFbConfig.RegType = RegFbType.REG_WITH_LDPLAYER;

            // Get proxy
            if (AutoNetworkType.Proxy == regFbConfig.RegFbNetworkType)
            {
                string proxy = string.Empty;
                lock (proxiesLock)
                {
                    if (GlobalVar.ProxiesCounter >= GlobalVar.Proxies.Length) GlobalVar.ProxiesCounter = 0;
                    proxy = GlobalVar.Proxies[GlobalVar.ProxiesCounter++];
                    File.WriteAllText(GlobalVar.OutputDirectory + Constant.ProxiesCounterPath, GlobalVar.ProxiesCounter.ToString());
                }
                device.Proxy = proxy;
            }

            var regClone = new RegFb(device, new RegFb.LogTrace(LogTrace), regFbConfig);
            try
            {
                regClone.LogStepTrace("Start register");
                var create = regClone.RegisterFacebook();

                if (create.Status == FbRegStatus.SUCCESS_WITH_VERI)
                {
                    regClone.GetUid();
                    if (cbTurn2faOn.Checked)
                    {
                        regClone.TurnOn2Fa();
                    }
                    this.Invoke((LogInfo)((logInfo) =>
                    {
                        FunctionHelper.SaveSuccess(logInfo);
                        txtSuccess.AppendText(logInfo + "\r\n");
                    }), regClone.FbAcc.StringInfo());
                }
                else
                {
                    regClone.LogStepTrace("Register error");
                    this.Invoke((LogInfo)((logInfo) =>
                    {
                        FunctionHelper.SaveFailure(logInfo);
                        txtFail.AppendText(logInfo + "\r\n");
                    }), regClone.FbAcc.StringInfo() + " <<< " + create.Message);
                }
                regClone.Dispose();
            }
            catch (ThreadAbortException)
            {
                regClone.Dispose();
            }
        }

        #endregion

        private void Main_FormClosing(object sender, FormClosingEventArgs e)
        {
            if (SettingChanged)
            {
                var confirm = MessageBox.Show(this, "Bạn có muốn lưu lại cài đặt không?", "Xác nhận", MessageBoxButtons.YesNo);

                if (confirm == DialogResult.Yes)
                {
                    SaveSetting();
                }
            }

            AbortRegisFbThreads();
        }

        #region Setting
        private void InitSetting()
        {
            // setting
            this.nudThreadNo.Value = (decimal)Settings.Default["ThreadNos"];
            this.cbTurn2faOn.Checked = (bool)Settings.Default["TurnOn2fa"];
            this.nudNoMEmuDevices.Value = (decimal)Settings.Default["MEmuDeviceNos"];
            this.nudNoLDPlayerDevices.Value = (decimal)Settings.Default["LDPlayerDeviceNos"];
            this.txtMEmuRootPath.Text = Settings.Default["MEmuCommanderRootPath"].ToString();
            this.txtLDPlayerRootPath.Text = Settings.Default["LDPlayerCommanderRootPath"].ToString();
            this.cbHideChrome.Checked = (bool)Settings.Default["HideChrome"];
            this.cbMinimizeChrome.Checked = (bool)Settings.Default["MinimizeChrome"];
            this.rbUseLDPLayer.Checked = (int)Settings.Default["RegType"] == 0;
            this.rbUseMEmu.Checked = (int)Settings.Default["RegType"] == 1;
            this.txtOutputDirectory.Text = Settings.Default["OutputDirectory"].ToString();
            this.cbLdRmDevices.Checked = (bool)Settings.Default["LdRmAllDevicesWhenRestore"];
            this.cbUseMailServer.Checked = (bool)Settings.Default["UseMailServer"];
            this.cbbAutoNetwork.SelectedValue = (AutoNetworkType)(int)Settings.Default["AutoNetworkType"]; ;
            this.txtDcomNetworkName.Text = (string)Settings.Default["DcomNetworkName"];
            SettingInitialized = true;
        }

        private void SaveSetting()
        {
            Settings.Default["ThreadNos"] = this.nudThreadNo.Value;
            Settings.Default["TurnOn2fa"] = this.cbTurn2faOn.Checked;
            Settings.Default["MEmuDeviceNos"] = this.nudNoMEmuDevices.Value;
            Settings.Default["LDPlayerDeviceNos"] = this.nudNoLDPlayerDevices.Value;
            Settings.Default["MEmuCommanderRootPath"] = this.txtMEmuRootPath.Text;
            Settings.Default["LDPlayerCommanderRootPath"] = this.txtLDPlayerRootPath.Text;
            Settings.Default["HideChrome"] = this.cbHideChrome.Checked;
            Settings.Default["MinimizeChrome"] = this.cbMinimizeChrome.Checked;
            Settings.Default["RegType"] = this.rbUseLDPLayer.Checked ? 0 : 1;
            Settings.Default["OutputDirectory"] = this.txtOutputDirectory.Text;
            Settings.Default["LdRmAllDevicesWhenRestore"] = this.cbLdRmDevices.Checked;
            Settings.Default["MmRmAllDevicesWhenRestore"] = this.cbMmRmDevices.Checked;
            Settings.Default["UseMailServer"] = this.cbUseMailServer.Checked;
            Settings.Default["AutoNetworkType"] = (int)(AutoNetworkType)this.cbbAutoNetwork.SelectedValue;
            Settings.Default["DcomNetworkName"] = this.txtDcomNetworkName.Text;
            Settings.Default.Save();
        }

        private void btnSaveSetting_Click(object sender, EventArgs e)
        {
            SaveSetting();
            SettingChanged = false;
            btnSaveSetting.Enabled = false;
            Info("Lưu cài đặt thành công");
        }

        private void btnResetSetting_Click(object sender, EventArgs e)
        {
            var confirm = MessageBox.Show(this, "Bạn có muốn Đặt cài đặt mặc định không?", "Xác nhận", MessageBoxButtons.YesNo);

            if (confirm == DialogResult.Yes)
            {
                Settings.Default.Reset();
                InitSetting();
                SettingChanged = false;
                btnSaveSetting.Enabled = false;
                Info("Đặt cài đặt mặc định thành công");
            }
        }

        private void SettingValueChanged(object sender, EventArgs e)
        {
            if (SettingInitialized)
            {
                SettingChanged = true;
                btnSaveSetting.Enabled = true;
            }
        }

        #endregion

        #region Firstname, Lastname

        private void btnAddFirstName_Click(object sender, EventArgs e)
        {
            if (!string.IsNullOrEmpty(this.txtFirstName.Text))
            {
                if (!this.lbFirstName.Items.Contains(this.txtFirstName.Text))
                {
                    this.lbFirstName.Items.Insert(0, this.txtFirstName.Text);
                    GlobalVar.ListFirstName = lbFirstName.ToStringArray();
                    File.WriteAllLines(_pathFileFirstName, GlobalVar.ListFirstName);
                }
                else
                {
                    Warning("\"" + this.txtFirstName.Text + "\" đã tồn tại");
                }
                this.txtFirstName.Text = string.Empty;
                this.txtFirstName.Focus();
            }
        }

        private void btnRemoveFirstName_Click(object sender, EventArgs e)
        {
            if (this.lbFirstName.SelectedItem != null)
            {
                this.lbFirstName.Items.Remove(this.lbFirstName.SelectedItem);
                GlobalVar.ListFirstName = lbFirstName.ToStringArray();
                File.WriteAllLines(_pathFileFirstName, GlobalVar.ListFirstName);
            }
        }

        private void btnAddLastName_Click(object sender, EventArgs e)
        {
            if (!string.IsNullOrEmpty(this.txtLastName.Text))
            {
                if (!this.lbLastName.Items.Contains(this.txtLastName.Text))
                {
                    this.lbLastName.Items.Insert(0, this.txtLastName.Text);

                    GlobalVar.ListLastName = lbLastName.ToStringArray();
                    File.WriteAllLines(_pathFileLastName, GlobalVar.ListLastName);
                }
                else
                {
                    Warning("\"" + this.txtLastName.Text + "\" đã tồn tại");
                }
                this.txtLastName.Text = string.Empty;
                this.txtLastName.Focus();
            }
        }

        private void btnRemoveLastName_Click(object sender, EventArgs e)
        {
            if (this.lbLastName.SelectedItem != null)
            {
                this.lbLastName.Items.Remove(this.lbLastName.SelectedItem);
                GlobalVar.ListLastName = lbLastName.ToStringArray();
                File.WriteAllLines(_pathFileLastName, GlobalVar.ListLastName);
            }
        }

        #endregion

        #region MEmu Settings

        private void btnSetupMEmu_Click(object sender, EventArgs e)
        {
            if (_regFbIsRunning)
            {
                Warning("Stop reg facebook trước khi cài đặt lại MEmu");
                return;
            }
            if (string.IsNullOrEmpty(this.txtMEmuZipBase.Text))
            {
                Warning("Vui lòng chọn file MEmu zip base");
                return;
            }
            new Thread(() =>
            {
                this.Invoke((MethodInvoker)delegate ()
                {
                    this.btnSetupMEmu.Enabled = false;
                    this.btnStart.Enabled = false;
                    this.btnSetupMEmu.Text = "Setting..";
                });
                var success = SetupMEmu();
                if (success)
                {
                    this.Invoke((ShowLog)Info, "Cấu hình MEmu thành công.");
                }
                else
                {
                    this.Invoke((ShowLog)Warning, "Cấu hình MEmu lỗi.");
                }
                this.Invoke((MethodInvoker)delegate ()
                {
                    this.btnSetupMEmu.Enabled = true;
                    this.btnStart.Enabled = true;
                    this.btnSetupMEmu.Text = "Cài đặt";
                });
            }).Start();
        }

        private void txtMEmuRootPath_MouseDoubleClick(object sender, MouseEventArgs e)
        {
            using (var fbd = new FolderBrowserDialog())
            {
                DialogResult result = fbd.ShowDialog();

                if (result == DialogResult.OK && !string.IsNullOrWhiteSpace(fbd.SelectedPath))
                {
                    GlobalVar.MEmuWorkingDirectory = this.txtMEmuRootPath.Text = fbd.SelectedPath;
                }
            }
        }

        private void btnChooseBaseMEmu_Click(object sender, EventArgs e)
        {
            using (var fd = new OpenFileDialog())
            {
                fd.Filter = "Zip files (*.zip, *.7z)|*.zip;*.7z";
                fd.FilterIndex = 0;
                fd.RestoreDirectory = true;
                DialogResult result = fd.ShowDialog();

                if (result == DialogResult.OK && !string.IsNullOrWhiteSpace(fd.FileName))
                {
                    this.txtMEmuZipBase.Text = fd.FileName;
                }
            }
        }

        private bool SetupMEmu()
        {
            var memuFunc = new MEmuFunc();
            bool isSuccess = false;
            try
            {
                var devices = memuFunc.GetDevices();

                // Stop all devices
                memuFunc.StopDevice(null);

                if (cbMmRmDevices.Checked)
                {
                    // Remove all devices
                    var removeDeviceTasks = new List<Task>();
                    foreach (var device in devices)
                    {
                        var removeDeviceTask = new Task(() =>
                        {
                            memuFunc.RemoveDevice(device);
                        });
                        removeDeviceTask.Start();
                        removeDeviceTasks.Add(removeDeviceTask);
                    }
                    Task.WhenAll(removeDeviceTasks.ToArray()).Wait();
                }

                // Restore base device
                var ovaPath = this.txtMEmuZipBase.Text;
                _RestoreFromMemuFile(ovaPath, this.txtMEmuRootPath.Text + @"\MemuHypervVMs_FB_CLONE");
                // Get base device
                var baseDevice = memuFunc.GetDevices().LastOrDefault();

                if (baseDevice != null)
                {
                    memuFunc.RenameDevice(baseDevice, "REG_FB_CLONE_0");
                    // Clone from Base (with id = 0)
                    var noMemu = this.nudNoMEmuDevices.Value;
                    var cloneDeviceTasks = new List<Task>();
                    decimal count = 0;
                    while (count++ < noMemu - 1)
                    {
                        var cloneDeviceTask = new Task((c) =>
                        {
                            memuFunc.CloneDevice(baseDevice, string.Format("REG_FB_CLONE_{0}", c));
                        }, count);
                        cloneDeviceTask.Start();
                        cloneDeviceTasks.Add(cloneDeviceTask);
                    }
                    Task.WaitAll(cloneDeviceTasks.ToArray());
                    isSuccess = true;
                }
            }
            catch (Exception ex)
            {
                this._log.Error(ex.Message);
            }
            return isSuccess;
        }

        private void _RestoreFromMemuFile(string zipPath, string hypervPath)
        {
            var memuFunc = new MEmuFunc();
            if (!Directory.Exists(hypervPath))
            {
                Directory.CreateDirectory(hypervPath);
            }
            else
            {
                Directory.Delete(hypervPath, true);
                Directory.CreateDirectory(hypervPath);
            }
            ZipFile.ExtractToDirectory(zipPath, hypervPath);
            var path = Path.Combine(hypervPath, @"MEmu\MEmu.memu");
            memuFunc.RestoreDevice(path);
        }

        #endregion

        #region LDPlayer settings

        private void txtLDPlayerRootPath_DoubleClick(object sender, EventArgs e)
        {
            using (var fbd = new FolderBrowserDialog())
            {
                DialogResult result = fbd.ShowDialog();

                if (result == DialogResult.OK && !string.IsNullOrWhiteSpace(fbd.SelectedPath))
                {
                    GlobalVar.LDPlayerWorkingDirectory = this.txtLDPlayerRootPath.Text = fbd.SelectedPath;
                }
            }
        }

        private void btnChooseBaseLDPlayer_Click(object sender, EventArgs e)
        {
            using (var fd = new OpenFileDialog())
            {
                fd.Filter = "LDPlayer backup files (*.ldbk)|*.ldbk";
                fd.FilterIndex = 0;
                fd.RestoreDirectory = true;
                DialogResult result = fd.ShowDialog();

                if (result == DialogResult.OK && !string.IsNullOrWhiteSpace(fd.FileName))
                {
                    this.txtLDPlayerBase.Text = fd.FileName;
                }
            }
        }

        private void btnSetupLDPlayer_Click(object sender, EventArgs e)
        {
            if (_regFbIsRunning)
            {
                Warning("Stop reg facebook trước khi cài đặt lại MEmu");
                return;
            }
            if (string.IsNullOrEmpty(this.txtLDPlayerBase.Text))
            {
                Warning("Vui lòng chọn file LDPlayer backup");
                return;
            }
            new Thread(() =>
            {
                this.Invoke((MethodInvoker)delegate ()
                {
                    this.btnSetupLDPlayer.Enabled = false;
                    this.btnStart.Enabled = false;
                    this.btnSetupLDPlayer.Text = "Setting..";
                });
                var success = SetupLDPlayer();
                if (success)
                {
                    this.Invoke((ShowLog)Info, "Cấu hình LDPlayer thành công.");
                }
                else
                {
                    this.Invoke((ShowLog)Warning, "Cấu hình LDPlayer lỗi.");
                }
                this.Invoke((MethodInvoker)delegate ()
                {
                    this.btnSetupLDPlayer.Enabled = true;
                    this.btnStart.Enabled = true;
                    this.btnSetupLDPlayer.Text = "Cài đặt";
                });
            }).Start();
        }

        private bool SetupLDPlayer()
        {
            var ldplayerFunc = new LDPlayerFunc();
            bool isSuccess = false;
            try
            {
                var devices = ldplayerFunc.GetDevices();

                // Stop all devices
                ldplayerFunc.StopDevice(null);

                // Remove all devices
                if (cbLdRmDevices.Checked)
                {
                    var removeDeviceTasks = new List<Task>();
                    foreach (var device in devices)
                    {
                        var removeDeviceTask = new Task(() =>
                        {
                            ldplayerFunc.RemoveDevice(device);
                        });
                        removeDeviceTask.Start();
                        removeDeviceTasks.Add(removeDeviceTask);
                    }
                    Task.WhenAll(removeDeviceTasks.ToArray()).Wait();
                }

                // Restore base device
                var ldbkFile = this.txtLDPlayerBase.Text;
                ldplayerFunc.RestoreDevice(ldbkFile);

                // Get base device
                var baseDevice = ldplayerFunc.GetDevices().LastOrDefault();

                if (baseDevice != null)
                {
                    ldplayerFunc.RenameDevice(baseDevice, "REG_FB_CLONE_0");
                    // Clone from Base (with id = 0)
                    var noMemu = this.nudNoMEmuDevices.Value;
                    var cloneDeviceTasks = new List<Task>();
                    decimal count = 0;
                    while (count++ < noMemu - 1)
                    {
                        var cloneDeviceTask = new Task((c) =>
                        {
                            ldplayerFunc.CloneDevice(baseDevice, string.Format("REG_FB_CLONE_{0}", c));
                        }, count);
                        cloneDeviceTask.Start();
                        cloneDeviceTasks.Add(cloneDeviceTask);
                    }
                    Task.WaitAll(cloneDeviceTasks.ToArray());
                    isSuccess = true;
                }
            }
            catch (Exception ex)
            {
                this._log.Error(ex.Message);
            }
            return isSuccess;
        }

        #endregion

        #region For testing

        static object linkslock = new object();
        static int count = 0;
        private void button1_Click(object sender, EventArgs e)
        {
            var t1 = new Thread(() =>
            {
                while (count < 20)
                {
                    lock (linkslock)
                    {
                        count++;
                    }
                    Console.WriteLine("t1 " + count);
                    Thread.Sleep(1);
                }
            });
            var t2 = new Thread(() =>
            {
                while (count < 20)
                {
                    lock (linkslock)
                    {
                        count++;
                    }
                    Console.WriteLine("t2 " + count);
                    Thread.Sleep(1);
                }
            });

            t1.Start();
            t2.Start();
            return;
            //var fb = new FacebookAccountInfo();
            //fb.Email = "ficeboh599@synevde.com";
            //fb.Passwd = "quocThang12321";
            //fb.TwoFacAuth = "";
            //var regFb = new RegFb(fb);
            ////regFb.TurnOn2Fa();
            ////regFb.GetUid();

            //for (var i = 0; i < 8; i++)
            //{
            //    var t = new Thread((obj) => {
            //        for (; ; )
            //        {
            //            RegisFb((int)obj);
            //        }
            //    });
            //    _RegisFbThreads.Add(t);
            //    t.Start(i + 1);
            //}
        }

        private void button2_Click(object sender, EventArgs e)
        {
            DcomChanger.ChangeIP("Viettel", out var ip);
            Info(ip);
        }

        private void printResult(string result)
        {
            this.txtSuccess.AppendText(result);
        }

        #endregion

        private void txtOutputDirectory_MouseDoubleClick(object sender, MouseEventArgs e)
        {
            using (var fbd = new FolderBrowserDialog())
            {
                DialogResult result = fbd.ShowDialog();

                if (result == DialogResult.OK && !string.IsNullOrWhiteSpace(fbd.SelectedPath))
                {
                    GlobalVar.OutputDirectory = this.txtOutputDirectory.Text = fbd.SelectedPath;
                }
            }
        }

        private void btnReloadDevices_Click(object sender, EventArgs e)
        {
            LoadDevices();
        }

        private void btnChooseAll_Click(object sender, EventArgs e)
        {
            for (int i = 0; i < devices.Count; i++)
            {
                devices[i].Choose = true;
            }

            this.dgvListDevices.Refresh();
        }
    }
}
