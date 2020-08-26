using AutoTool.AutoMethods;
using log4net;
using System;
using System.IO;
using System.Net;
using System.Reflection;
using System.Text.RegularExpressions;

namespace AutoTool.Network
{
    public class DcomChanger
    {
        private static ILog _log = LogManager.GetLogger(MethodBase.GetCurrentMethod().DeclaringType);

        private static bool CheckDuplicateIp = true;
        private static bool CheckBlackList = true;

        public static string RunCMD(string cmd)
        {
            return new CmdFunc(null).RunCMD(cmd);
        }

        private static void LogError(string message)
        {
            _log.Error($"DcomChanger: {message}");
        }

        public static bool ChangeIP(string networkName, out string ipAddress)
        {
            ipAddress = null;
            // disconnect dcom
            string disconnect = RunCMD("Rasdial /disconnect");
            if (!disconnect.Contains("Command completed"))
            {
                LogError("error disconnect");
                return false;
            }
            string connect = RunCMD($"Rasdial {networkName}");
            if (connect.Contains("modem was not found"))
            {
                LogError("Modem was not found");
                return false;
            }
            else if (connect.Contains("Access error 623"))
            {
                LogError("Sai tên nhà mạng");
                return false;
            }
            else if (connect.Contains("Successfully connected to"))
            {
                ipAddress = GetIP();
                if (!string.IsNullOrEmpty(ipAddress))
                {
                    return true;
                }
                else
                {
                    return false;
                }
            }
            else
            {
                LogError("Error connect");
            }
            return false;
        }

        private static string GetIP()
        {
            string ipAddress;
            string output = RunCMD("nslookup myip.opendns.com. resolver1.opendns.com");
            var ip = Regex.Matches(output, @"\b(?:(?:25[0-5]|2[0-4][0-9]|[01]?[0-9][0-9]?)\.){3}(?:25[0-5]|2[0-4][0-9]|[01]?[0-9][0-9]?)\b");
            if (ip.Count == 2)
            {
                if (CheckDuplicateIp)
                if (isDuplicate(ip[1].Value))
                {
                    return null;
                }
                ipAddress = ip[1].Value;
            }
            else
            {
                LogError("Get ip error");
                return null;
            }
            if (CheckBlackList)
            {
                // check blacklist
                if (IsBlacklist(ip[1].Value.Trim()))
                {
                    File.AppendAllText("Blacklist.txt", DateTime.Now.ToString("dd/MM HH:mm:ss") + "\t" + ip[1] + Environment.NewLine);
                    return null;
                }
                //if (isWhoerBlacklist())
                //{
                //    File.AppendAllText("Blacklist.txt", DateTime.Now.ToString("dd/MM HH:mm:ss") + "\t" + ip[1] + Environment.NewLine);
                //    return null;
                //}
            }
            return ipAddress;
        }

        private static bool isDuplicate(string ip)
        {
            if (File.ReadAllText("ip.txt").Contains(ip))
            {
                return true;
            }
            File.AppendAllText("ip.txt", ip + Environment.NewLine);
            return false;
        }

        private static bool IsBlacklist(string ip)
        {
            string[] ipreverse = ip.Split(Convert.ToChar("."));
            Array.Reverse(ipreverse);
            string ipreverseJoin = string.Join(".", ipreverse);
            string output = RunCMD("nslookup " + ipreverseJoin + ".bl.spamcop.net. & nslookup " + ipreverseJoin + ".cbl.abuseat.org. & nslookup " + ipreverseJoin + ".dnsbl.sorbs.net. & nslookup " + ipreverseJoin + ".zen.spamhaus.org. & nslookup " + ipreverseJoin + ".psbl.surriel.com.");
            var bl = Regex.Matches(output, "127.0.0");
            if (bl.Count > 0)
            {
                return true;
            }
            return false;
        }

        private static bool isWhoerBlacklist()
        {
            using (WebClient client = new WebClient())
            {
                string html = client.DownloadString("https://whoer.net/");
                Match match = Regex.Match(html, @"dsbl : ([^\n]+)", RegexOptions.IgnoreCase);
                if (match.Success)
                {
                    if (match.Groups[1].Value.ToString() != "0")
                    {
                        return true;
                    }
                }
                return false;
            }
        }
    }
}
