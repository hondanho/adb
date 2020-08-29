using log4net;
using System;
using System.Diagnostics;
using System.Reflection;
using System.Threading;

namespace AutoTool.AutoMethods
{
    public class CmdFunc
    {
        private static readonly ILog _log = LogManager.GetLogger(MethodBase.GetCurrentMethod().DeclaringType);
        private string WorkingDirectory;

        public CmdFunc(string workingDirectory)
        {
            this.WorkingDirectory = workingDirectory;
        }

        ~CmdFunc()
        {
            this.WorkingDirectory = null;
        }

        public string RunCMD(string cmd, string workingDirectory)
        {
            string output;
            try
            {
                Process cmdProcess;
                cmdProcess = new Process();
                cmdProcess.StartInfo.WorkingDirectory = workingDirectory;
                cmdProcess.StartInfo.FileName = "cmd.exe";
                cmdProcess.StartInfo.Arguments = "/c " + cmd;
                cmdProcess.StartInfo.RedirectStandardOutput = true;
                cmdProcess.StartInfo.UseShellExecute = false;
                cmdProcess.StartInfo.CreateNoWindow = true;
                cmdProcess.Start();
                output = cmdProcess.StandardOutput.ReadToEnd();
                cmdProcess.WaitForExit();
                if (String.IsNullOrEmpty(output))
                    output = string.Empty;
            }
            catch (Exception ex)
            {
                _log.Error(ex);
                output = null;
            }
            return output;
        }

        public string RunCMD(string cmd)
        {
            return RunCMD(cmd, this.WorkingDirectory);
        }

        public string Run(string cmdCommand)
        {
            return Run(cmdCommand, this.WorkingDirectory);
        }

        public string Run(string cmdCommand, string workingDirectory)
        {
            string result;
            try
            {
                Process process = new Process();
                process.StartInfo = new ProcessStartInfo
                {
                    WorkingDirectory = workingDirectory,
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

        public string RunWithTime(string cmdCommand, TimeSpan time)
        {
            string result;
            try
            {
                Process process = new Process();
                process.StartInfo = new ProcessStartInfo
                {
                    WorkingDirectory = this.WorkingDirectory,
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
    }
}
