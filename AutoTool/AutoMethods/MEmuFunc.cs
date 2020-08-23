using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Drawing;
using System.IO;
using log4net;
using System.Text.RegularExpressions;
using System.Threading;
using System.Reflection;
using AutoTool.Constants;
using AutoTool.AutoCommons;
using AutoTool.Models;
using System.Windows.Forms;

namespace AutoTool.AutoMethods
{
    public class MEmuFunc : IEmulatorFunc
    {
        private static readonly ILog _log = LogManager.GetLogger(MethodBase.GetCurrentMethod().DeclaringType);

        public string RunCMD(string cmdCommand)
        {
            string result;
            try
            {
                Process process = new Process();
                process.StartInfo = new ProcessStartInfo
                {
                    WorkingDirectory = GlobalVar.CommanderRootPath,
                    FileName = "cmd.exe",
                    CreateNoWindow = true,
                    UseShellExecute = false,
                    WindowStyle = ProcessWindowStyle.Hidden,
                    RedirectStandardInput = true,
                    RedirectStandardOutput = true
                };
                process.Start();
                process.StandardInput.WriteLine(cmdCommand);
                process.StandardInput.Flush();
                process.StandardInput.Close();
                process.WaitForExit();
                string text = process.StandardOutput.ReadToEnd();
                result = text;
            }
            catch (Exception ex)
            {
                _log.Error(ex.Message);
                result = null;
            }
            return result;
        }

        public string RunCMDWithTime(string cmdCommand, TimeSpan time)
        {
            string result;
            try
            {
                Process process = new Process();
                process.StartInfo = new ProcessStartInfo
                {
                    WorkingDirectory = GlobalVar.CommanderRootPath,
                    FileName = "cmd.exe",
                    CreateNoWindow = true,
                    UseShellExecute = false,
                    WindowStyle = ProcessWindowStyle.Hidden,
                    RedirectStandardInput = true,
                    RedirectStandardOutput = true
                };
                process.Start();
                process.StandardInput.WriteLine(cmdCommand);
                process.StandardInput.Flush();
                process.StandardInput.Close();
                Thread.Sleep(time);
                process.Kill();
                result = string.Empty;
            }
            catch (Exception ex)
            {
                _log.Error(ex.Message);
                result = null;
            }
            return result;
        }

        public List<EmulatorInfo> GetDevices()
        {
            List<EmulatorInfo> list = new List<EmulatorInfo>();
            string input = RunCMD(MEmuConsts.LIST_DEVICES);
            string text = Path.GetPathRoot(AppDomain.CurrentDomain.BaseDirectory);
            text = text.Replace("\\", "");
            string pattern = "(?<=" + MEmuConsts.LIST_DEVICES + ").*";
            MatchCollection matchCollection = Regex.Matches(input, pattern, RegexOptions.Singleline);
            bool flag = matchCollection != null && matchCollection.Count > 0;
            if (flag)
            {
                var reg = new Regex(@"(\d+),([^\,]+),.*");
                foreach (object obj in matchCollection)
                {
                    string text2 = obj.ToString();
                    var matches = reg.Matches(text2);
                    foreach (Match o in matches)
                    {
                        var id = o.Groups[1].ToString();
                        var name = o.Groups[2].ToString();
                        list.Add(new EmulatorInfo(id, name));
                    }
                }
            }
            return list;
        }

        public bool StartDevice(EmulatorInfo device)
        {
            RunCMD(string.Format(MEmuConsts.START_MEMU, device.Id));

            var isMEmuRunning = new WaitHelper(TimeSpan.FromSeconds(30)).Until(() =>
            {
                var isSuccess = RunCMD(string.Format(MEmuConsts.STATUS_MEMU, device.Id));
                if (!string.IsNullOrEmpty(isSuccess) && isSuccess.Contains("already connected"))
                {
                    return true;
                }
                return false;
            });

            return isMEmuRunning;
        }

        public bool StopDevice(EmulatorInfo device)
        {
            if (device == null)
            {
                RunCMD(MEmuConsts.STOP_ALL_DEVICES);
            }
            else
            {
                RunCMD(string.Format(MEmuConsts.STOP_DEVICE, device.Id));
            }
            return true;
        }

        public bool RemoveDevice(EmulatorInfo device)
        {
            RunCMD(string.Format(MEmuConsts.REMOVE_DEVICE, device.Id));
            return true;
        }

        public bool RenameDevice(EmulatorInfo device, string deviceName)
        {
            RunCMD(string.Format(MEmuConsts.RENAME_DEVICE_BY_ID, device.Id, deviceName));
            return true;
        }

        public bool RestoreDevice(string source)
        {
            RunCMD(string.Format(MEmuConsts.RESTORE_MEMU, source));
            return true;
        }

        public bool CloneDevice(EmulatorInfo sourceDevice, string newDeviceName)
        {
            RunCMD(string.Format(MEmuConsts.CLONE_MEMU_BY_NAME, sourceDevice.Id, newDeviceName));
            return true;
        }

        public bool StartApp(EmulatorInfo device, string appPackage)
        {
            RunCMD(string.Format(MEmuConsts.MEMU_STARTAPP_NAME, device.Id, appPackage));
            return true;
        }

        public bool StopApp(EmulatorInfo device, string appPackage)
        {
            RunCMD(string.Format(MEmuConsts.MEMU_STOPAPP_NAME, device.Id, appPackage));
            return true;
        }

        public bool ClearAppData(EmulatorInfo device, string appPackage)
        {
            RunCMD(string.Format(MEmuConsts.CLEAR, device.Id, appPackage));
            return true;
        }

        public bool SendKey(EmulatorInfo device, AdbKeyEvent keyEvent)
        {
            RunCMD(string.Format(MEmuConsts.KEY, device.Id, (int)keyEvent));
            return true;
        }

        public bool LongPress(EmulatorInfo device, int x, int y, int duration = 1000)
        {
            RunCMD(string.Format(MEmuConsts.SWIPE_LONG, device.Id, x, y, x, y, duration));
            return true;
        }

        public bool LongPress(EmulatorInfo device, Point point, int duration = 1000)
        {
            return LongPress(device, point.X, point.Y, duration);
        }

        public bool Tap(EmulatorInfo device, double x, double y)
        {
            RunCMD(string.Format(MEmuConsts.TAP, device.Id, x, y));
            return true;
        }

        public bool Tap(EmulatorInfo device, Point point)
        {
            return Tap(device, point.X, point.Y);
        }

        public bool Swipe(EmulatorInfo device, Point from, Point to)
        {
            RunCMD(string.Format(MEmuConsts.SWIPE, device.Id, from.X, from.Y, to.X, to.Y));
            return true;
        }

        public bool SwipeLong(EmulatorInfo device, Point from, Point to, int duration = 1000)
        {
            RunCMD(string.Format(MEmuConsts.SWIPE_LONG, device.Id, from.X, from.Y, to.X, to.Y, duration));
            return true;
        }

        public bool ScreenShot(EmulatorInfo device, string destination)
        {
            var pathOnDevice = string.Format("/sdcard/{0}.png", new DateTime().Millisecond);
            RunCMD(string.Format(MEmuConsts.SCREEN_SHOT, device.Id, pathOnDevice, destination));
            return true;
        }

        public bool Input(EmulatorInfo device, string text)
        {
            RunCMD(string.Format(MEmuConsts.INPUT, device.Id, text));
            return true;
        }

        public bool Input(EmulatorInfo device, char[] text)
        {
            for (int i = 0; i < text.Length; i++)
            {
                RunCMD(string.Format(MEmuConsts.INPUT, device.Id, text[i]));
            }
            return true;
        }
    }
}
