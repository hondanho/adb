using System;
using System.Collections.Generic;
using System.Drawing;
using System.IO;
using System.Text.RegularExpressions;
using AutoTool.Constants;
using AutoTool.AutoCommons;
using AutoTool.Models;

namespace AutoTool.AutoMethods
{
    public class MEmuFunc : IEmulatorFunc
    {
        private CmdFunc _cmd;

        public MEmuFunc()
        {
            _cmd = new CmdFunc(GlobalVar.MEmuWorkingDirectory);
        }

        public List<EmulatorInfo> GetDevices()
        {
            List<EmulatorInfo> devices = new List<EmulatorInfo>();
            string output = _cmd.RunCMD(LDPlayerConsts.LIST_DEVICES);
            if (output != null)
            {
                var matchs = Regex.Matches(output, @"(\d+),([^\,]+),.*", RegexOptions.Multiline);
                int index = 1;
                foreach (Match match in matchs)
                {
                    var id = match.Groups[1].ToString();
                    var name = match.Groups[2].ToString();
                    devices.Add(new EmulatorInfo(id, name)
                    {
                        Index = index++
                    });
                }
            }
            return devices;
        }

        public List<EmulatorInfo> GetDevicess()
        {
            List<EmulatorInfo> list = new List<EmulatorInfo>();
            string input = _cmd.Run(MEmuConsts.LIST_DEVICES);
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

        public string GetSerialNo(EmulatorInfo device)
        {
            var output = _cmd.RunCMD(string.Format(MEmuConsts.GET_SERIAL_NO, device.Id));
            if (string.IsNullOrEmpty(output))
            {
                return null;
            }
            return output.Trim();
        }

        public bool StartDevice(EmulatorInfo device)
        {
            _cmd.Run(string.Format(MEmuConsts.START_DEVICE, device.Id));

            var isMEmuRunning = new WaitHelper(TimeSpan.FromSeconds(30)).Until(() =>
            {
                var isSuccess = _cmd.Run(string.Format(MEmuConsts.STATUS_DEVICE, device.Id));
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
                _cmd.Run(MEmuConsts.STOP_ALL_DEVICES);
            }
            else
            {
                _cmd.Run(string.Format(MEmuConsts.STOP_DEVICE, device.Id));
            }
            return true;
        }

        public bool IsRunning(EmulatorInfo device)
        {
            _cmd.Run(string.Format(MEmuConsts.IS_DEVICE_RUNNING, device.Id));
            return true;
        }

        public bool RemoveDevice(EmulatorInfo device)
        {
            _cmd.Run(string.Format(MEmuConsts.REMOVE_DEVICE, device.Id));
            return true;
        }

        public bool RenameDevice(EmulatorInfo device, string deviceName)
        {
            _cmd.Run(string.Format(MEmuConsts.RENAME_DEVICE, device.Id, deviceName));
            return true;
        }

        public bool RestoreDevice(string source)
        {
            _cmd.Run(string.Format(MEmuConsts.RESTORE_DEVICE, source));
            return true;
        }

        public bool CloneDevice(EmulatorInfo sourceDevice, string newDeviceName)
        {
            _cmd.Run(string.Format(MEmuConsts.CLONE_DEVICE, sourceDevice.Id, newDeviceName));
            return true;
        }

        public bool StartApp(EmulatorInfo device, string appPackage)
        {
            _cmd.Run(string.Format(MEmuConsts.START_APP, device.Id, appPackage));
            return true;
        }

        public bool StopApp(EmulatorInfo device, string appPackage)
        {
            _cmd.Run(string.Format(MEmuConsts.STOP_APP, device.Id, appPackage));
            return true;
        }

        public bool ClearAppData(EmulatorInfo device, string appPackage)
        {
            _cmd.Run(string.Format(MEmuConsts.CLEAR_APP_DATA, device.Id, appPackage));
            return true;
        }

        public bool SendKey(EmulatorInfo device, AdbKeyEvent keyEvent)
        {
            _cmd.Run(string.Format(MEmuConsts.KEY_EVENT, device.Id, (int)keyEvent));
            return true;
        }

        public bool LongPress(EmulatorInfo device, int x, int y, int duration = 1000)
        {
            _cmd.Run(string.Format(MEmuConsts.SWIPE_LONG, device.Id, x, y, x, y, duration));
            return true;
        }

        public bool LongPress(EmulatorInfo device, Point point, int duration = 1000)
        {
            return LongPress(device, point.X, point.Y, duration);
        }

        public bool Tap(EmulatorInfo device, double x, double y)
        {
            _cmd.Run(string.Format(MEmuConsts.TAP, device.Id, x, y));
            return true;
        }

        public bool Tap(EmulatorInfo device, Point point)
        {
            return Tap(device, point.X, point.Y);
        }

        public bool Swipe(EmulatorInfo device, Point from, Point to)
        {
            _cmd.Run(string.Format(MEmuConsts.SWIPE, device.Id, from.X, from.Y, to.X, to.Y));
            return true;
        }

        public bool SwipeLong(EmulatorInfo device, Point from, Point to, int duration = 1000)
        {
            _cmd.Run(string.Format(MEmuConsts.SWIPE_LONG, device.Id, from.X, from.Y, to.X, to.Y, duration));
            return true;
        }

        public bool ScreenShot(EmulatorInfo device, string destination)
        {
            var pathOnDevice = string.Format("/sdcard/{0}.png", new DateTime().Millisecond);
            _cmd.Run(string.Format(MEmuConsts.SCREEN_SHOT, device.Id, pathOnDevice, destination));
            return true;
        }

        public bool Input(EmulatorInfo device, string text)
        {
            _cmd.Run(string.Format(MEmuConsts.INPUT, device.Id, text));
            return true;
        }

        public bool Input(EmulatorInfo device, char[] text)
        {
            for (int i = 0; i < text.Length; i++)
            {
                _cmd.Run(string.Format(MEmuConsts.INPUT, device.Id, text[i]));
            }
            return true;
        }
    }
}
