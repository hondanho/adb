using AutoTool.AutoHelper;
using log4net;
using OpenQA.Selenium.Chrome;
using OtpNet;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Diagnostics;
using System.Drawing;
using System.IO;
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
            return listHo[randomNumber.Next(0, listHo.Count - 1)];
        }

        public static string getLastNameRandom()
        {
            var listTen = new List<string>() {
                "Quynh",
                "Thanh",
                "Cong",
                "Duong",
                "Hai",
                "Luan",
                "Tien",
                "Manh",
                "Luc",
                "Phi",
                "Toan",
                "Kien",
                "Mai",
                "Hao",
                "Giang",
                "Huy",
                "Duy",
                "May",
                "Quyet",
                "Khanh",
                "Linh",
                "Thao",
                "Diep",
                "Long",
                "Hung",
                "Manh",
                "Hai",
                "Tung",
                "Quang",
                "Linh",
                "Trang",
                "Van",
                "Kien",
                "Tuan",
                "Anh",
                "Ha",
                "Hoc",
                "Nam",
                "Tam",
                "Bac",
                "Xuan",
                "Kinh",
                "Hoang",
                "Hau",
                "Tap",
                "Thu",
                "Hoan",
                "Hanh",
                "Tam",
                "Hien",
                "Kha",
                "Phong",
                "Phuoc",
                "Cong",
                "Man",
                "Duc",
                "Nguyen",
                "Nhat",
                "Nhut",
                "Hoang",
                "Diep",
                "Ngoc",
                "Minh",
                "Thieu",
                "Khai",
                "Dat",
                "Quynh",
                "Thanh",
                "Giang",
                "Thao",
                "Diep",
                "Yen",
                "Trang",
                "Huyen",
                "Nga",
                "Huong",
                "Lan",
                "My",
                "Hau",
                "Loan",
                "Hien",
                "Diep",
                "Minh",
                "Oanh",
                "Đức",
                "Mai",
                "Loan",
                "Linh",
                "Minh",
                "Ninh",
                "Khanh"
            };
            var randomNumber = new Random();
            return listTen[randomNumber.Next(0, listTen.Count - 1)];
        }

        public static string Get2faFromQR(string stringQr)
        {
            return Regex.Match(stringQr, @"(?<=secret=)([^\&]+)(?=\&?)").Value;
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

        public static ChromeDriver InitWebDriver(int idx = 1)
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
            chromeOptions.AddArguments($"--window-size={bw},{bh}");
            chromeOptions.AddArguments($"--window-position={x},{y}");
            chromeOptions.AddArguments("--disable-notifications");
            chromeOptions.AddArgument("--disable-blink-features=AutomationControlled");
            chromeOptions.AddArguments("profile.default_content_setting_values.images", "2");
            chromeOptions.AddExcludedArgument("enable-automation");
            chromeOptions.AddAdditionalCapability("useAutomationExtension", false);
            chromeOptions.AddUserProfilePreference("profile.password_manager_enabled", false);
            chromeOptions.AddUserProfilePreference("credentials_enable_service", true);
            return new ChromeDriver(service, chromeOptions);
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
    }
}
