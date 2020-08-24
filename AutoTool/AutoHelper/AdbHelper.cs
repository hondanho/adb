using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Drawing;
using System.IO;
using log4net;
using System.Text.RegularExpressions;
using System.Threading;
using System.Reflection;
using AutoTool.AutoCommons;

namespace AutoTool.AutoHelper
{
    public class AdbHelper
    {
        private static string ADB_FOLDER_PATH = "adb";
        private static string LIST_DEVICES = "adb devices";
        private static string SCREEN_SHOT = "adb -s {0} shell screencap -p \"{1}\" && adb -s {0} pull \"{1}\" \"{2}\" && adb -s {0} shell rm -f \"{1}\"";
        private static string TAP = "adb -s {0} shell input tap {1} {2}";
        private static string SWIPE = "adb -s {0} shell input swipe {1} {2} {3} {4}";
        private static string SWIPE_LONG = "adb -s {0} shell input swipe {1} {2} {3} {4} {5}";
        private static string INPUT = "adb -s {0} shell input text \"{1}\"";
        private static string KEY = "adb -s {0} shell input keyevent {1}";
        private static string CLEAR = "adb -s {0} shell pm clear {1}";
        private static readonly ILog _log = LogManager.GetLogger(MethodBase.GetCurrentMethod().DeclaringType);

        public static string RunCMD(string cmdCommand)
        {
            string result;
            try
            {
                Process process = new Process();
                process.StartInfo = new ProcessStartInfo
                {
                    WorkingDirectory = ADB_FOLDER_PATH,
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

        public static List<string> GetDevices()
        {
            List<string> list = new List<string>();
            string input = RunCMD(LIST_DEVICES);
            string text = Path.GetPathRoot(AppDomain.CurrentDomain.BaseDirectory);
            text = text.Replace("\\", "");
            string pattern = "(?<=List of devices attached ).*?(?=" + text + ")";
            MatchCollection matchCollection = Regex.Matches(input, pattern, RegexOptions.Singleline);
            bool flag = matchCollection != null && matchCollection.Count > 0;
            if (flag)
            {
                foreach (object obj in matchCollection)
                {
                    string text2 = obj.ToString();
                    string[] array = text2.Split(new string[]
                    {
                        "device"
                    }, StringSplitOptions.None);

                    _log.Info(string.Format("Init amount device: {0}", array.Length));
                    for (int i = 0; i < array.Length - 1; i++)
                    {
                        string text3 = array[i];
                        string item = text3.Replace("\r", "").Replace("\n", "").Replace("\t", "");
                        list.Add(item);
                    }
                }
            }
            return list;
        }
        public static void InputNumber(string deviceId, string numberDir, string number)
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
        public static void InputNumber(string deviceId, string numberDir, int number)
        {
            InputNumber(deviceId, numberDir, number.ToString());
        }

        public static void ClearApp(string deviceId, string packageName)
        {
            RunCMD(string.Format(CLEAR, deviceId, packageName));
        }


        public static void SendKey(string deviceId, int key)
        {
            RunCMD(string.Format(KEY, deviceId, key));
        }

        public static void LongPress(string deviceID, int x, int y, int duration = 1000)
        {
            RunCMD(string.Format(SWIPE_LONG, deviceID, x, y, x, y, duration));
        }

        public static void LongPress(string deviceID, Point point, int duration = 1000)
        {
            LongPress(deviceID, point.X, point.Y, duration);
        }

        public static void Tap(string deviceId, ImagePoint point)
        {
            Tap(deviceId, point.X, point.Y);
        }

        public static void Tap(string deviceId, double x, double y)
        {
            RunCMD(string.Format(TAP, deviceId, x, y));
        }

        public static void Swipe(string deviceId, Point point1, Point point2)
        {
            RunCMD(string.Format(SWIPE, deviceId, point1.X, point1.Y, point2.X, point2.Y));
        }

        public static void SwipeLong(string deviceId, Point point1, Point point2, int millisecond = 1000)
        {
            RunCMD(string.Format(SWIPE_LONG, deviceId, point1.X, point1.Y, point2.X, point2.Y, millisecond));
        }

        public static void ScreenShot(string deviceId, string pathSave)
        {
            var pathDevice = string.Format("/sdcard/{0}.png", new DateTime().Millisecond);
            RunCMD(string.Format(SCREEN_SHOT, deviceId, pathDevice, pathSave));
        }

        public static void Input(string deviceId, string text)
        {
            RunCMD(string.Format(INPUT, deviceId, text));
        }

        public static void Input(string deviceId, char[] text)
        {
            for(int i = 0; i < text.Length; i++)
            {
                RunCMD(string.Format(INPUT, deviceId, text[i]));
            }
        }
        public static ImagePoint TapImg(string deviceId, string path, ImagePoint pointAdd = null)
        {
            var screenPath = string.Format("{0}\\data\\{1}.png", Environment.CurrentDirectory, DateTime.Now.Ticks);

            while (!File.Exists(screenPath))
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

            point = pointAdd == null ? point : new ImagePoint(new Point(point.X + pointAdd.X, point.Y + pointAdd.Y));
            Tap(deviceId, point);
            File.Delete(screenPath);

            return point;
        }
    }
}
