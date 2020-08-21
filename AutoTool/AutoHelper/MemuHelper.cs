using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;

namespace AutoTool.AutoHelper
{
    public class MemuHelper
    {
        private static string _MEMU_FOLDER_PATH = "memu\\MEmu";
        private static string LIST_MEMU = "memuc listvms";
        private static string CLONE_MEMU_BY_NAME = "memuc clone -n {0}";
        private static string START_MEMU_BY_NAME = "memuc -n {0} start";
        private static string MEMU_STARTAPP_NAME = "memuc -n {0} startapp {1}";

        public static string RunCMD(string cmdCommand)
        {
            string result;
            try
            {
                Process process = new Process();
                process.StartInfo = new ProcessStartInfo
                {
                    WorkingDirectory = _MEMU_FOLDER_PATH,
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
                Console.WriteLine(ex.Message);
                result = null;
            }
            return result;
        }

        public static List<string> GetMemus()
        {
            string input = RunCMD(LIST_MEMU);
            if (!string.IsNullOrEmpty(input))
            {
                return input.Split('\n').ToList();
            }
            else return new List<string>();
        }

        public static void CloneMemu(string name)
        {
            RunCMD(string.Format(CLONE_MEMU_BY_NAME, name));
        }

        public static void startApp(string name, string packageName)
        {
            RunCMD(string.Format(MEMU_STARTAPP_NAME, name, packageName));
        }

        public static void StartMemu(string name)
        {
            RunCMD(string.Format(START_MEMU_BY_NAME, name));
        }
    }
}
