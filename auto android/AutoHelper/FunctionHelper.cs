using auto_android.AutoHelper;
using RandomNameGeneratorLibrary;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Diagnostics;
using System.Drawing;
using System.IO;
using OpenQA.Selenium;
using OpenQA.Selenium.Remote;
using System.Threading;
using OpenQA.Selenium.Chrome;
using OpenQA.Selenium.Support.UI;
using System.Web.Security;
using log4net;
using System.Reflection;
using System.Threading.Tasks;
using OtpNet;
using System.Text.RegularExpressions;

namespace auto_android
{
    public class FunctionHelper
    {
        private static readonly ILog log = LogManager.GetLogger(MethodBase.GetCurrentMethod().DeclaringType);

        public static bool startDevices()
        {
            Process[] pname = Process.GetProcessesByName(ConfigurationManager.AppSettings["servicename"]);
            if (pname.Length == 0)
            {
                AdbHelper.RunCMD(string.Format("cd \"{0}\" && start {1}",
                 Path.GetDirectoryName(ConfigurationManager.AppSettings["pathdevice"]),
                 Path.GetFileName(ConfigurationManager.AppSettings["pathdevice"])
                 ));
            }

            return true;
        }

        public static string getMaleRandom()
        {
            var randomNumber = new Random();
            var male = randomNumber.Next(1, 2);
            return male > 1 ? Constant.maleName : Constant.maleNu;
        }
        public static string getHoRandom()
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

        public static string getTenRandom()
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

        public static string Get2fa(string qrPath)
        {
            var code = QRCode.DecodeQR(qrPath);
            return Regex.Match(code, @"(?<=secret=)([^\&]+)(?=\&?)").Value;
        }

        public static string GetTotp(string secret)
        {
            var secretKey = Base32Encoding.ToBytes(secret);
            var totp = new Totp(secretKey);
            return totp.ComputeTotp();
        }

        public static ChromeDriver InitWebDriver()
        {
            ChromeOptions chromeOptions = new ChromeOptions();
            chromeOptions.AddArguments("--disable-notifications");
            chromeOptions.AddArgument("--disable-blink-features=AutomationControlled");
            chromeOptions.AddArguments("profile.default_content_setting_values.images", "2");
            chromeOptions.AddExcludedArgument("enable-automation");
            chromeOptions.AddAdditionalCapability("useAutomationExtension", false);
            chromeOptions.AddUserProfilePreference("profile.password_manager_enabled", false);
            chromeOptions.AddArguments("--disable-notifications");
            chromeOptions.AddUserProfilePreference("credentials_enable_service", false);
            chromeOptions.AddUserProfilePreference("profile.password_manager_enabled", false);
            return new ChromeDriver(ChromeDriverService.CreateDefaultService(), chromeOptions);
        }

        public static string GetRandomMonth()
        {
            var number = new Random();
            var month = number.Next(1, 12);
            if (month < 10)
            {
                return "0" + month;
            }
            else
            {
                return month.ToString();
            }
        }

        public static string GetRandomMatkhau()
        {
            return Membership.GeneratePassword(10, 0);
        }

        public static string GetRandomYear()
        {
            var number = new Random();
            return number.Next(1980, 2002).ToString();
        }

        public static string GetRandomDay()
        {
            var number = new Random();
            var day = number.Next(1, 28);
            if (day < 10)
            {
                return "0" + day;
            }
            else
            {
                return day.ToString();
            }
        }
        public static Point? CheckImgExist(string deviceId, string subPath)
        {
            var screenPath = string.Format("{0}\\data\\{1}.png", Environment.CurrentDirectory, DateTime.Now.Ticks);
            AdbHelper.ScreenShot(deviceId, screenPath);

            return ImageScanOpenCV.FindOutPoint(screenPath, subPath);
        }

        public static TResult WaitTimeout<TResult>(Func<TResult> task, TimeSpan timeSpan)
        {
            var success = default(TResult);
            int elapsed = 0;

            while (elapsed < timeSpan.TotalMilliseconds)
            {
                Thread.Sleep(1000);
                elapsed += 1000;
                success = task.Invoke();

                if (success != null)
                {
                    var isBreak = true;
                    foreach (var pi in success.GetType().GetProperties())
                    {
                        if (pi.GetType() == typeof(bool))
                        {
                            bool val = (bool)pi.GetValue(success);
                            if (!val)
                            {
                                isBreak = false;
                                break;
                            }
                        }
                    }
                    if (isBreak) break;
                }
            }
            return success;
        }
    }
    public class WaitHelper<T>
    {
        public TimeSpan TimeOut;
        public T Input;

        public WaitHelper(T input, TimeSpan timeOut)
        {
            this.Input = input;
            this.TimeOut = timeOut;
        }

        public WaitHelper(TimeSpan timeOut)
        {
            this.Input = default;
            this.TimeOut = timeOut;
        }

        public TResult Until<TResult>(Func<TResult> condition)
        {
            TResult result = default(TResult);

            Task runCondition = new Task(() =>
            {

                while (true)
                {
                    result = condition.Invoke();
                    if (result != null)
                    {
                        var isBreak = true;
                        foreach (var pi in result.GetType().GetProperties())
                        {
                            if (pi.GetType() == typeof(bool))
                            {
                                bool val = (bool)pi.GetValue(result);
                                if (!val)
                                {
                                    isBreak = false;
                                    break;
                                }
                            }
                        }
                        if (isBreak) break;
                    }
                };
            });
            runCondition.Start();

            Task.WhenAny(runCondition, Task.Delay(this.TimeOut)).Wait();

            return result;
        }

        public TResult Until<TResult>(Func<T, TResult> condition)
        {
            TResult result = default(TResult);

            Task runCondition = new Task(() =>
            {

                while (true)
                {
                    result = condition.Invoke(this.Input);
                    if (result != null)
                    {
                        var isBreak = true;
                        foreach (var pi in result.GetType().GetProperties())
                        {
                            if (pi.GetType() == typeof(bool))
                            {
                                bool val = (bool)pi.GetValue(result);
                                if (!val)
                                {
                                    isBreak = false;
                                    break;
                                }
                            }
                        }
                        if (isBreak) break;
                    }
                };
            });
            runCondition.Start();

            Task.WhenAny(runCondition, Task.Delay(this.TimeOut)).Wait();

            return result;
        }
    }
}
