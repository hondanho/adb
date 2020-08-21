using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Drawing;
using System.IO;
using log4net;
using System.Text.RegularExpressions;
using System.Threading;
using System.Reflection;
using System.Linq;
using System.Runtime.CompilerServices;
using Emgu.CV.Ocl;
using System.IO.Compression;

namespace auto_android.AutoHelper
{
    public class MemuCommandHelper
    {
        private static string MEMU_FOLDER_PATH = @"D:\Program Files\Microvirt\MEmu";
        private static string LIST_DEVICES = "memuc listvms";
        private static string SCREEN_SHOT = "memuc -i {0} adb shell screencap -p \"{1}\" && memuc -i {0} adb pull \"{1}\" \"{2}\" && memuc -i {0} adb shell rm \"{1}\"";
        private static string TAP = "memuc -i {0} adb shell input tap {1} {2}";
        private static string SWIPE = "memuc -i {0} adb shell input swipe {1} {2} {3} {4}";
        private static string SWIPE_LONG = "memuc -i {0} adb shell input swipe {1} {2} {3} {4} {5}";
        private static string INPUT = "memuc -i {0} input \"{1}\"";
        private static string KEY = "memuc -i {0} adb shell input keyevent {1}";
        private static string CLEAR = "memuc -i {0} adb shell pm clear {1}";

        private static string CLONE_MEMU_BY_NAME = "memuc clone -i {0}";
        private static string START_MEMU = "memuc -i {0} start";
        private static string MEMU_STARTAPP_NAME = "memuc -i {0} startapp {1}";
        private static string RESTORE_MEMU = "memuc import \"{0}\"";
        private static string STOP_ALL_DEVICES = "memuc stopall";
        private static string STOP_DEVICE = "memuc stop -i {0}";
        private static string REMOVE_DEVICE = "memuc remove -i {0}";
        private static string RENAME_DEVICE_BY_ID = "memuc rename -i {0} {1}";
        private static string ISVMRUNNING_DEVICE = "memuc isvmrunning -i {0}";

        private static readonly ILog _log = LogManager.GetLogger(MethodBase.GetCurrentMethod().DeclaringType);

        public string MEmuRootPath;

        public MemuCommandHelper()
        {
            this.MEmuRootPath = MemuCommandHelper.MEMU_FOLDER_PATH;
        }

        public MemuCommandHelper(string memuRootPath)
        {
            this.MEmuRootPath = memuRootPath;
        }

        public string RunCMD(string cmdCommand)
        {
            string result;
            try
            {
                Process process = new Process();
                process.StartInfo = new ProcessStartInfo
                {
                    WorkingDirectory = this.MEmuRootPath,
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
            catch(Exception ex)
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
                    WorkingDirectory = this.MEmuRootPath,
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

        public List<MEmuDevice> GetDevices()
        {
            List<MEmuDevice> list = new List<MEmuDevice>();
            string input = RunCMD(LIST_DEVICES);
            string text = Path.GetPathRoot(AppDomain.CurrentDomain.BaseDirectory);
            text = text.Replace("\\", "");
            string pattern = "(?<=" + LIST_DEVICES + ").*";
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
                        list.Add(new MEmuDevice(id, name));
                    }
                }
            }
            return list;
        }

        public List<string> GetMemus()
        {
            string input = RunCMD(LIST_DEVICES);
            if (!string.IsNullOrEmpty(input))
            {
                return input.Split('\n').ToList();
            }
            else return new List<string>();
        }

        public void StopDevice(string deviceId = null)
        {
            if (string.IsNullOrEmpty(deviceId))
            {
                RunCMD(STOP_ALL_DEVICES);
            }
            else
            {
                RunCMD(string.Format(STOP_DEVICE, deviceId));
            }
        }

        public void RemoveDevice(string deviceId)
        {
            RunCMD(string.Format(REMOVE_DEVICE, deviceId));
        }

        public void RenameDeviceById(string deviceId, string deviceName)
        {
            RunCMD(string.Format(RENAME_DEVICE_BY_ID, deviceId, deviceName));
        }

        public void CloneMemu(string deviceId, string name)
        {
            RunCMD(string.Format(CLONE_MEMU_BY_NAME, deviceId, name));
        }

        public void RestoreMemu(string pathImport)
        {
            RunCMD(string.Format(RESTORE_MEMU, pathImport));
        }

        public void RestoreFromMemuFile(string zipPath, string hypervPath)
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
            RestoreMemu(path);
        }

        public void StartApp(string deviceId, string packageName)
        {
            RunCMD(string.Format(MEMU_STARTAPP_NAME, deviceId, packageName));
        }

        public void StartMemu(string deviceId, TimeSpan? timeOut = null)
        {
            if (timeOut == null)
            {
                RunCMD(string.Format(START_MEMU, deviceId));
            }
            else
            {
                RunCMDWithTime(string.Format(START_MEMU, deviceId), timeOut.Value);
            }
        }

        public void InputNumber(string deviceId, string numberDir, string number)
        {
            if (string.IsNullOrEmpty(numberDir)) return;
            numberDir.Replace("/", "\\");
            if (!numberDir.EndsWith(@"\"))
            {
                numberDir = numberDir + "\\";
            }
            var source = number.ToCharArray();
            for (int i = 0; i < source.Length; i++)
            {
                var numberPath = string.Format("{0}{1}.png", numberDir, source[i]);
                TapImg(deviceId, numberPath);
            }
        }

        public void InputNumber(string deviceId, string numberDir, int number)
        {
            InputNumber(deviceId, numberDir, number.ToString());
        }

        public void ClearApp(string deviceId, string packageName)
        {
            RunCMD(string.Format(CLEAR, deviceId, packageName));
        }


        public void SendKey(string deviceId, AdbKeyEvent key)
        {
            RunCMD(string.Format(KEY, deviceId, (int)key));
        }

        public void LongPress(string deviceID, int x, int y, int duration = 1000)
        {
            RunCMD(string.Format(SWIPE_LONG, deviceID, x, y, x, y, duration));
        }

        public void LongPress(string deviceID, Point point, int duration = 1000)
        {
            LongPress(deviceID, point.X, point.Y, duration);
        }

        public void Tap(string deviceId, Point point)
        {
            Tap(deviceId, point.X, point.Y);
        }

        public void Tap(string deviceId, double x, double y)
        {
            RunCMD(string.Format(TAP, deviceId, x, y));
        }

        public void Swipe(string deviceId, Point point1, Point point2)
        {
            RunCMD(string.Format(SWIPE, deviceId, point1.X, point1.Y, point2.X, point2.Y));
        }

        public void SwipeLong(string deviceId, Point point1, Point point2, int millisecond = 1000)
        {
            RunCMD(string.Format(SWIPE_LONG, deviceId, point1.X, point1.Y, point2.X, point2.Y, millisecond));
        }

        public void ScreenShot(string deviceId, string pathSave)
        {
            var pathDevice = string.Format("/sdcard/{0}.png", new DateTime().Millisecond);
            RunCMD(string.Format(SCREEN_SHOT, deviceId, pathDevice, pathSave));
        }

        public void Input(string deviceId, string text)
        {
            RunCMD(string.Format(INPUT, deviceId, text));
        }

        public void Input(string deviceId, char[] text)
        {
            for (int i = 0; i < text.Length; i++)
            {
                RunCMD(string.Format(INPUT, deviceId, text[i]));
            }
        }

        public Point? TapImg(string deviceId, string path, Point? pointAdd = null)
        {
            var screenPath = string.Format("{0}\\data\\{1}.png", Environment.CurrentDirectory, DateTime.Now.Ticks);
            while(!File.Exists(screenPath))
            {
                ScreenShot(deviceId, screenPath);
            }

            var point = ImageScanOpenCV.FindOutPoint(screenPath, path);
            while (point == null)
            {
                File.Delete(screenPath);
                _log.Error(string.Format("Not found :{0} in {1}", path, screenPath));
                Thread.Sleep(1);
                ScreenShot(deviceId, screenPath);
                point = ImageScanOpenCV.FindOutPoint(screenPath, path);
            }

            point = pointAdd == null ? point : new Point(point.Value.X + pointAdd.Value.X, point.Value.Y + pointAdd.Value.Y);
            Tap(deviceId, point.Value);
            File.Delete(screenPath);

            return point;
        }
        public string GetQRCode(string deviceId)
        {
            var screenPath = string.Format("{0}\\data\\{1}.png", Environment.CurrentDirectory, DateTime.Now.Ticks);
            ScreenShot(deviceId, screenPath);

            return QRCode.DecodeQR(screenPath);
        }

        public Point? IsExistImg(string deviceId, string subPath)
        {
            var screenPath = string.Format("{0}\\data\\{1}.png", Environment.CurrentDirectory, DateTime.Now.Ticks);
            ScreenShot(deviceId, screenPath);

            var point = ImageScanOpenCV.FindOutPoint(screenPath, subPath);
            File.Delete(screenPath);
            return point;
        }
    }

    public class MEmuDevice
    {
        public string Id { get; set; }
        public string Name { get; set; }

        public MEmuDevice(string id, string name)
        {
            this.Id = id;
            this.Name = name;
        }
    }
}
