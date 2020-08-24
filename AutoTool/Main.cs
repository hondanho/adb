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
using AutoTool.Constants;
using Emgu.CV.ML;
using System.Drawing;

namespace AutoTool
{
    public partial class Main : Form, IDisposable
    {
        private ILog _log;
        private string _pathAccountSuccess = "accountSuccess.txt";
        private string _pathFileFirstName = Environment.CurrentDirectory + "\\source\\data\\firstName.txt";
        private string _pathFileLastName = Environment.CurrentDirectory + "\\source\\data\\lastName.txt";
        private string _pathAccountFailer = "accountFailer.txt";
        private StreamWriter _fileAccountSuccess;
        private StreamWriter _fileAccountFailer;
        private IEmulatorFunc _memuHelper;
        public delegate void ShowLog(string message);
        public delegate void LogInfo(string info);
        private bool _regFbIsRunning = false;
        private decimal _numberOfThread = 0;
        private List<Thread> _RegisFbThreads = new List<Thread>();
        private bool SettingInitialized = false;
        private bool SettingChanged = false;

        static public void Info(string s)
        {
            MessageBox.Show(s, "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
        }

        static public void Warning(string s)
        {
            MessageBox.Show(s, "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
        }

        public Main()
        {
            InitializeComponent();

            GlobalVar.ListFirstName = FunctionHelper.ReadAllTextFromFile(_pathFileFirstName);
            GlobalVar.ListLastName = FunctionHelper.ReadAllTextFromFile(_pathFileLastName);
            this.lbFirstName.Items.AddRange(GlobalVar.ListFirstName);
            this.lbLastName.Items.AddRange(GlobalVar.ListLastName);

            InitSetting();

            this.lblStatus.Text = "Stopped";
            GlobalVar.WorkingDirectory = this.txtMEmuRootPath.Text;
            _memuHelper = new MEmuFunc();
            this._fileAccountSuccess = File.AppendText(_pathAccountSuccess);
            this._fileAccountFailer = File.AppendText(_pathAccountFailer);
            this._log = LogManager.GetLogger(MethodBase.GetCurrentMethod().DeclaringType);
        }

        #region Register Facebook Clone

        private void btnStart_Click(object sender, EventArgs e)
        {
            try
            {
                var devices = _memuHelper.GetDevices();
                //var devices = new List<EmulatorInfo>
                //{
                //    new EmulatorInfo("1", "1111"),
                //    new EmulatorInfo("2", "2222"),
                //    new EmulatorInfo("3", "3333"),
                //    new EmulatorInfo("4", "4444"),
                //    new EmulatorInfo("5", "5555"),
                //    new EmulatorInfo("6", "6666")
                //};
                // Caculate number of threads
                _numberOfThread = Math.Min(this.nudThreadNo.Value, devices.Count);

                if (_numberOfThread > 0)
                {
                    _regFbIsRunning = true;
                    _SetStartUI();
                    
                    for (var i = 0; i < _numberOfThread; i++)
                    {
                        var device = devices[i];
                        device.Index = i + 1;
                        var t = new Thread((d) =>
                        {
                            for (;;)
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
            this.lblStatus.Text = "Running with " + _numberOfThread + " threads";

            if (this._fileAccountSuccess == null)
            {
                this._fileAccountSuccess = File.AppendText(_pathAccountSuccess);
            }
            if (this._fileAccountFailer == null)
            {
                this._fileAccountFailer = File.AppendText(_pathAccountFailer);
            }
        }

        private void _SetStopUI()
        {
            if (this._fileAccountFailer != null)
            {
                this._fileAccountFailer.Flush();
                this._fileAccountFailer.Dispose();
                this._fileAccountFailer = null;
            }
            if (this._fileAccountSuccess != null)
            {
                this._fileAccountSuccess.Flush();
                this._fileAccountSuccess.Dispose();
                this._fileAccountSuccess = null;
            }
            this.btnStart.Enabled = true;
            this.btnStop.Enabled = false;
            this.nudThreadNo.Enabled = true;
            this.cbMinimizeChrome.Enabled = true;
            this.cbHideChrome.Enabled = true;
            this.cbTurn2faOn.Enabled = true;
            this.lblStatus.Text = "Stopped";
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
            var userSetting = new UserSetting();
            userSetting.HideChrome = this.cbHideChrome.Checked;
            userSetting.Minimize = this.cbMinimizeChrome.Checked;
            var regClone = new RegFb(device, userSetting);
            try
            {
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
                        _fileAccountSuccess.WriteLine(logInfo);
                        txtSuccess.AppendText(logInfo + "\r\n");
                    }), regClone.FbAcc.StringInfo());
                }
                else
                {
                    this.Invoke((LogInfo)((logInfo) =>
                    {
                        _fileAccountFailer.WriteLine(logInfo);
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
                var success = SetupMenu();
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
                    GlobalVar.WorkingDirectory = this.txtMEmuRootPath.Text = fbd.SelectedPath;
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

        private bool SetupMenu()
        {
            bool isSuccess = false;
            try
            {
                var devices = _memuHelper.GetDevices();

                // Stop all devices
                _memuHelper.StopDevice(null);

                // Remove all devices
                var removeDeviceTasks = new List<Task>();
                foreach (var device in devices)
                {
                    var removeDeviceTask = new Task(() =>
                    {
                        _memuHelper.RemoveDevice(device);
                    });
                    removeDeviceTask.Start();
                    removeDeviceTasks.Add(removeDeviceTask);
                }
                Task.WhenAll(removeDeviceTasks.ToArray()).Wait();

                // Restore base device
                var ovaPath = this.txtMEmuZipBase.Text;
                _RestoreFromMemuFile(ovaPath, this.txtMEmuRootPath.Text + @"\MemuHypervVMs_FB_CLONE");
                // Get base device
                var baseDevice = _memuHelper.GetDevices().LastOrDefault();

                if (baseDevice != null)
                {
                    _memuHelper.RenameDevice(baseDevice, "REG_FB_CLONE_0");
                    // Clone from Base (with id = 0)
                    var noMemu = this.nudNoMEmuDevices.Value;
                    var cloneDeviceTasks = new List<Task>();
                    decimal count = 0;
                    while (count++ < noMemu - 1)
                    {
                        var cloneDeviceTask = new Task((c) =>
                        {
                            _memuHelper.CloneDevice(baseDevice, string.Format("REG_FB_CLONE_{0}", c));
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
            _memuHelper.RestoreDevice(path);
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
            if (this._fileAccountFailer != null) this._fileAccountFailer.Dispose();
            if (this._fileAccountSuccess != null) this._fileAccountSuccess.Dispose();
        }

        #region For testing

        private void button1_Click(object sender, EventArgs e)
        {
            //var fb = new FacebookAccountInfo();
            //fb.Email = "ficeboh599@synevde.com";
            //fb.Passwd = "quocThang12321";
            //fb.TwoFacAuth = "";
            //var regFb = new RegFb(fb);
            ////regFb.TurnOn2Fa();
            ////regFb.GetUid();

            for (var i = 0; i < 8; i++)
            {
                var t = new Thread((obj) => {
                    for (; ; )
                    {
                        RegisFb((int)obj);
                    }
                });
                _RegisFbThreads.Add(t);
                t.Start(i + 1);
            }
        }

        private void button2_Click(object sender, EventArgs e)
        {
            GlobalVar.WorkingDirectory = @"E:\ChangZhi\LDPlayer";
            this.txtSuccess.Text = CmdFunc.RunCMD(string.Format(LDPlayerConsts.LIST_DEVICES));
            IEmulatorFunc emu = new LdPlayerFunc();
            EmulatorInfo device = new EmulatorInfo("0", "LDPlayer", DeviceStatus.RUNNING);
            //var list = emu.RestoreDevice(@"E:\ldplayer\LDPlayer-8.ldbk");
            var ret = emu.ScreenShot(device, @"E:\screennnn.png");
            Console.WriteLine(this.txtSuccess.Text);
            EmulatorInfo device = new EmulatorInfo("0", "dd");
            var m = new MEmuFunc();
            m.TapNumber(device, new int[] { 2, 5, 1, 0, 1, 9, 7, 3 });
            //AbortRegisFbThreads();
        }

        private void RegisFb(int idx)
        {
            var fb = new FacebookAccountInfo();
            fb.Email = "ficeboh599@synevde.com";
            fb.Passwd = "quocThang12321";
            var regFb = new RegFb(fb, idx);
            try
            {
                regFb.TurnOn2Fa();
                this.Invoke((ShowLog)printResult, "name_" + idx);
            }
            catch(Exception ex)
            {
                _log.Error(ex);
            }
            finally
            {
                regFb.Dispose();
            }
        }

        private void printResult(string result)
        {
            this.txtSuccess.AppendText(result);
        }

        #endregion

        #region Setting
        private void InitSetting()
        {
            // setting
            this.nudThreadNo.Value = (decimal)Settings.Default["ThreadNos"];
            this.cbTurn2faOn.Checked = (bool)Settings.Default["TurnOn2fa"];
            this.nudNoMEmuDevices.Value = (decimal)Settings.Default["MEmuDeviceNos"];
            this.txtMEmuRootPath.Text = Settings.Default["MEmuCommanderRootPath"].ToString();
            this.cbHideChrome.Checked = (bool)Settings.Default["HideChrome"];
            this.cbMinimizeChrome.Checked = (bool)Settings.Default["MinimizeChrome"];
            SettingInitialized = true;
        }

        private void SaveSetting()
        {
            Settings.Default["ThreadNos"] = this.nudThreadNo.Value;
            Settings.Default["TurnOn2fa"] = this.cbTurn2faOn.Checked;
            Settings.Default["MEmuDeviceNos"] = this.nudNoMEmuDevices.Value;
            Settings.Default["MEmuCommanderRootPath"] = this.txtMEmuRootPath.Text;
            Settings.Default["HideChrome"] = this.cbHideChrome.Checked;
            Settings.Default["MinimizeChrome"] = this.cbMinimizeChrome.Checked;
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
            if(this.lbFirstName.SelectedItem != null)
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
    }
}
