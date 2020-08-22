using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;
using auto_android.AutoHelper;
using log4net;
using OpenQA.Selenium.Chrome;

namespace auto_android
{
    public partial class Main : Form, IDisposable
    {
        private ILog _log;
        private string _pathAccountSuccess = "accountSuccess.txt";
        private string _pathAccountFailer = "accountFailer.txt";
        private StreamWriter _fileAccountSuccess;
        private StreamWriter _fileAccountFailer;
        private MemuCommandHelper _memuHelper;
        public delegate void ShowLog(string message);
        public delegate void LogInfo(string info);
        private Thread _regFbThread;
        private List<ChromeDriver> _chromeDrivers;

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
            _memuHelper = new MemuCommandHelper(this.txtMEmuRootPath.Text);
            this._fileAccountSuccess = File.AppendText(_pathAccountSuccess);
            this._fileAccountFailer = File.AppendText(_pathAccountFailer);
            this._log = LogManager.GetLogger(MethodBase.GetCurrentMethod().DeclaringType);
            _chromeDrivers = new List<ChromeDriver>();
        }

        private void btnStart_Click(object sender, EventArgs e)
        {
            try
            {
                if (this._fileAccountSuccess == null) this._fileAccountSuccess = File.AppendText(_pathAccountSuccess);
                if (this._fileAccountFailer == null) this._fileAccountFailer = File.AppendText(_pathAccountFailer);

                var devices = _memuHelper.GetDevices();

                this.btnStart.Enabled = false;
                this.btnStop.Enabled = true;
                _regFbThread = new Thread(() =>
                {
                    var lstTask = new List<Task>();
                    for (var i = 0; i < Math.Min(devices.Count, this.nudThreadNo.Value) - 1; i++)
                    {
                        var t = new Task((index) =>
                        {
                            //this._fileAccountFailer.WriteLine(index);
                            Exec(devices[(int)index]);
                        }, i);
                        lstTask.Add(t);
                    }

                    while (true)
                    {
                        foreach (var task in lstTask)
                        {
                            if (!task.IsCompleted && (task.Status == TaskStatus.Faulted || task.Status == TaskStatus.Created))
                            {
                                task.Start();
                            }
                        }
                    }
                });

                _regFbThread.Start();
            }
            catch (Exception ex)
            {
                _log.Error(ex.Message);
            }
        }

        public void Exec(MEmuDevice device)
        {
            _memuHelper.StartMemu(device.Id);
            var chrom = FunctionHelper.InitWebDriver();
            _chromeDrivers.Add(chrom);
            var fb = new RegFb(device, _memuHelper, this._log, chrom);

            //fb.TurnHma();
            fb.Turn1111();

            bool registered = fb.RegisterFb();
            if (registered)
            {
                bool turned2faOn = fb.Turn2Fa();
                var info = fb.GetInfo();

                this.Invoke((LogInfo)((logInfo) =>
                {
                    this._fileAccountSuccess.WriteLine(logInfo);
                    this.txtSuccess.Text += logInfo + "\r\n";
                }), info);
            }
            else
            {
                var info = fb.GetInfo();
                this.Invoke((LogInfo)((logInfo) =>
                {
                    this._fileAccountFailer.WriteLine(logInfo);
                    this.txtFail.Text += logInfo + "\r\n";
                }), info);
            }
        }

        private void Main_FormClosing(object sender, FormClosingEventArgs e)
        {
            if (this._regFbThread != null) this._regFbThread.Abort();
            if (this._fileAccountFailer != null) this._fileAccountFailer.Dispose();
            if (this._fileAccountSuccess != null) this._fileAccountSuccess.Dispose();
            foreach (var chrom in _chromeDrivers)
            {
                chrom.Quit();
            }
        }

        private void txtMEmuRootPath_MouseDoubleClick(object sender, MouseEventArgs e)
        {
            using (var fbd = new FolderBrowserDialog())
            {
                DialogResult result = fbd.ShowDialog();

                if (result == DialogResult.OK && !string.IsNullOrWhiteSpace(fbd.SelectedPath))
                {
                    _memuHelper.MEmuRootPath = this.txtMEmuRootPath.Text = fbd.SelectedPath;
                }
            }
        }

        private void btnSetupMEmu_Click(object sender, EventArgs e)
        {
            if (_regFbThread != null && _regFbThread.IsAlive)
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

        private bool SetupMenu()
        {
            bool isSuccess = false;
            try
            {
                var devices = _memuHelper.GetDevices();

                // Stop all devices
                _memuHelper.StopDevice();

                // Remove all devices
                var removeDeviceTasks = new List<Task>();
                foreach (var device in devices)
                {
                    var removeDeviceTask = new Task(() =>
                    {
                        _memuHelper.RemoveDevice(device.Id);
                    });
                    removeDeviceTask.Start();
                    removeDeviceTasks.Add(removeDeviceTask);
                }
                Task.WhenAll(removeDeviceTasks.ToArray()).Wait();

                // Restore base device
                var ovaPath = this.txtMEmuZipBase.Text;
                _memuHelper.RestoreFromMemuFile(ovaPath, this.txtMEmuRootPath.Text + @"\MemuHypervVMs_FB_CLONE");
                // Get base device
                var baseDevice = _memuHelper.GetDevices().LastOrDefault();

                if (baseDevice != null)
                {
                    _memuHelper.RenameDeviceById(baseDevice.Id, "REG_FB_CLONE_0");
                    // Clone from Base (with id = 0)
                    var noMemu = this.nupNoMEmuDevices.Value;
                    var cloneDeviceTasks = new List<Task>();
                    decimal count = 0;
                    while (count++ < noMemu - 1)
                    {
                        var cloneDeviceTask = new Task((c) =>
                        {
                            _memuHelper.CloneMemu(baseDevice.Id, string.Format("REG_FB_CLONE_{0}", c));
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

        private void btnStop_Click(object sender, EventArgs e)
        {
            var confirm = MessageBox.Show(this, "Bạn có chắc chắn muốn dừng không?", "Xác nhận", MessageBoxButtons.YesNo);

            if (confirm == DialogResult.Yes)
            {
                if (_regFbThread != null) _regFbThread.Abort();
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
            }
        }

        private void nudThreadNo_ValueChanged(object sender, EventArgs e)
        {

        }
    }
}
