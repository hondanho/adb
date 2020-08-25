using AutoTool.AutoHelper;
using AutoTool.Models;
using log4net;
using OpenQA.Selenium.Chrome;
using OtpNet;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Diagnostics;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Runtime.CompilerServices;
using System.Text.RegularExpressions;

namespace AutoTool
{
    public class FunctionHelper
    {
        private static readonly ILog log = LogManager.GetLogger(MethodBase.GetCurrentMethod().DeclaringType);

        public static string getMaleRandom()
        {
            var randomNumber = new Random();
            var male = randomNumber.Next(1, 2);
            return male > 1 ? Constant.male : Constant.female;
        }
        public static string getFirstNameRandom()
        {
            var listHo = new List<string>() {
                "Nguyen Van",
                "Le Minh",
                "Tran Van",
                "Ma Thanh",
                "Phan Thanh",
                "Pham Minh",
                "Vu Quan",
                "Ha Thi",
                "Do Minh",
                "Hoang Thai",
                "Dinh Hoang",
                "Thieu Van",
                "Ta Cong",
                "Duong Thai",
                "Bui",
                "Mai",
                "Mac",
                "Hua",
                "Dong",
                "Phung",
                "Cao",
                "Huynh",
                "Truong",
                "Vu",
                "Luong",
                "Nguyen",
                "Quoc",
                "Le",
                "Tran",
                "Duong",
                "Nguyen",
                "Nguyen",
                "Van",
                "Tran",
                "Thi",
                "Nguyen",
                "Danh",
                "Nguyen",
                "Tran",
                "Phan Le",
                "Minh",
                "Oanh",
                "Mai",
                "Loan",
                "Linh",
                "Minh",
                "Ninh",
                "Khanh"
            };
            var randomNumber = new Random();
            return GlobalVar.ListFirstName[randomNumber.Next(0, GlobalVar.ListFirstName.Length - 1)];
        }

        public static string getLastNameRandom()
        {
            var randomNumber = new Random();
            return GlobalVar.ListLastName[randomNumber.Next(0, GlobalVar.ListLastName.Length - 1)];
        }

        public static string Get2faFromQR(string stringQr)
        {
            return Regex.Match(stringQr, @"(?<=secret=)([^\&]+)(?=\&?)").Value;
        }

        public static string[] ReadAllTextFromFile(string pathFile)
        {
            return File.ReadAllLines(pathFile);
        }

        public static string GetUserNameFromQR(string stringQr)
        {
            return Regex.Match(stringQr, "(?<=content=\")([^\\?]+)(?=\\?\")").Value;
        }

        public static string GetUidFromQR(string stringQr)
        {
            return Regex.Match(stringQr, @"(?<=ID:)([^\?]+)(?=\??)").Value;
        }

        public static string GetTotp(string secret)
        {
            var secretKey = Base32Encoding.ToBytes(secret);
            var totp = new Totp(secretKey);
            return totp.ComputeTotp();
        }

        public static ChromeDriver InitWebDriver(UserSetting userSetting, int idx = 1)
        {
            var workingArea = System.Windows.Forms.Screen.PrimaryScreen.WorkingArea;
            var width = workingArea.Width;
            var height = workingArea.Height;
            var ww = 4;
            var hh = 2;
            if (height > width) { hh = 4; ww = 2; }
            var bh = height / hh;
            var bw = width / ww;
            var xx = idx % ww == 0 ? ww - 1 : (idx % ww) - 1;
            var yy = idx % ww > 0 ? idx / ww : (idx / ww) - 1;
            var x = xx * bw;
            var y = yy * bh;

            ChromeOptions chromeOptions = new ChromeOptions();
            var service = ChromeDriverService.CreateDefaultService();
            service.SuppressInitialDiagnosticInformation = true;
            service.HideCommandPromptWindow = true;

            if (userSetting.HideChrome)
            {
                chromeOptions.AddArguments("headless");
            }
            if (userSetting.HideChrome)
            {
                chromeOptions.AddArguments("start-maximized");
            }

            chromeOptions.AddArguments($"--window-size={bw},{bh}");
            chromeOptions.AddArguments($"--window-position={x},{y}");
            chromeOptions.AddArguments("--disable-notifications");
            chromeOptions.AddArgument("--disable-blink-features=AutomationControlled");
            chromeOptions.AddArguments("profile.default_content_setting_values.images", "2");
            chromeOptions.AddExcludedArgument("enable-automation");
            chromeOptions.AddAdditionalCapability("useAutomationExtension", false);
            chromeOptions.AddUserProfilePreference("profile.password_manager_enabled", false);
            chromeOptions.AddUserProfilePreference("credentials_enable_service", true);
            var chromDriver = new ChromeDriver(service, chromeOptions);
            if (userSetting.Minimize)
            {
                chromDriver.Manage().Window.Minimize();
            }
            return chromDriver;
        }

        public static string GetRandomMonth()
        {
            var number = new Random();
            var month = number.Next(1, 12);
            if (month < 10) return ("0" + month);
            return month.ToString();
        }

        public static string GetPasswordRandom()
        {
            var random = new Random();
            var passwordLength = random.Next(10, 12);
            string allowedChars = "abcdefghijkmnopqrstuvwxyzABCDEFGHJKLMNOPQRSTUVWXYZ";
            char[] chars = new char[passwordLength];
            Random rd = new Random();

            for (int i = 0; i < passwordLength; i++)
            {
                chars[i] = allowedChars[rd.Next(0, allowedChars.Length)];
            }

            return new string(chars);
        }

        public static string GetYearRandom()
        {
            var number = new Random();
            return number.Next(1980, 2002).ToString();
        }

        public static string GetDayRandom()
        {
            var number = new Random();
            var day = number.Next(1, 28);
            if (day < 10) return ("0" + day);
            return day.ToString();
        }

        public static string GetMonthAsText(int month)
        {
            return MonthEng[month];
        }

        public static string[] MonthEng = new string[] {
            "",
            "Jan",
            "Feb",
            "Mar",
            "Apr",
            "May",
            "Jun",
            "Jul",
            "Aug",
            "Sep",
            "Oct",
            "Nov",
            "Dec"
        };
    }
}
