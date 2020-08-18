using System;
using System.Drawing;
using System.Reflection;
using System.Threading;
using log4net;
using OpenQA.Selenium;
using OpenQA.Selenium.Support.UI;

namespace auto_android.AutoHelper
{
    public class RegFb
    {
        public string _deviceId, _password, _email, _2Fa, _uid, _cookie = string.Empty;
        public int _timeout = 1000;
        public string _defaultPathExec = Environment.CurrentDirectory;
        private static readonly ILog log = LogManager.GetLogger(MethodBase.GetCurrentMethod().DeclaringType);

        public RegFb(string deviceID, int timeout = 1000)
        {
            this._deviceId = deviceID;
            this._timeout = timeout;
            ClearDataApp();
        }

        public void Turn1111()
        {
            AdbHelper.SendKey(_deviceId, (int)AdbKeyEvent.KEYCODE_HOME);
            AdbHelper.TapImg(_deviceId, _defaultPathExec + Constant.icon1111);
            Thread.Sleep(_timeout);
            while (FunctionHelper.IsExistImg(_deviceId, _defaultPathExec + Constant.iconTurned1111) == null)
            {
                AdbHelper.TapImg(_deviceId, _defaultPathExec + Constant.iconTurnOn1111);
                Thread.Sleep(_timeout);
            }
        }

        public bool RegisterFb()
        {
            try
            {
                // go to form reg
                AdbHelper.SendKey(_deviceId, (int)AdbKeyEvent.KEYCODE_HOME);
                AdbHelper.TapImg(_deviceId, _defaultPathExec + Constant.iconFbLite);
                Thread.Sleep(_timeout);
                AdbHelper.TapImg(_deviceId, _defaultPathExec + Constant.btnTaoMoiTaiKhoan);
                Thread.Sleep(_timeout);
                AdbHelper.TapImg(_deviceId, _defaultPathExec + Constant.btnTiep);
                Thread.Sleep(_timeout);

                // insert name
                AdbHelper.TapImg(_deviceId, _defaultPathExec + Constant.inputHo, new Point(0, 30));
                AdbHelper.Input(_deviceId, FunctionHelper.getHoRandom());
                AdbHelper.TapImg(_deviceId, _defaultPathExec + Constant.inputTen);
                AdbHelper.Input(_deviceId, FunctionHelper.getTenRandom());
                AdbHelper.TapImg(_deviceId, _defaultPathExec + Constant.btnTiep);
                Thread.Sleep(_timeout);
                AdbHelper.TapImg(_deviceId, _defaultPathExec + Constant.linkRegMail);
                Thread.Sleep(_timeout);

                // mail
                var inputMail = AdbHelper.TapImg(_deviceId, _defaultPathExec + Constant.labelMail, new Point(0, 30));
                AdbHelper.SwipeLong(_deviceId, new Point(20, inputMail.Value.Y), new Point(500, inputMail.Value.Y), 1500);
                AdbHelper.SendKey(_deviceId, (int)AdbKeyEvent.KEYCODE_DEL);
                var chromeDriver = FunctionHelper.InitWebDriver();
                chromeDriver.Navigate().GoToUrl("https://temp-mail.org/vi");
                Thread.Sleep(_timeout);
                var webElement = chromeDriver.FindElement(By.XPath("//*[@id=\"mail\"]"));
                new WebDriverWait(chromeDriver, TimeSpan.FromSeconds(30)).Until(x =>
                {
                    webElement = x.FindElement(By.XPath("//*[@id=\"mail\"]"));
                    this._email = webElement.GetAttribute("value");
                    return !string.IsNullOrEmpty(this._email) && this._email.Contains("@");
                });
                AdbHelper.Input(_deviceId, this._email.ToCharArray());
                AdbHelper.TapImg(_deviceId, _defaultPathExec + Constant.btnTiep);
                Thread.Sleep(_timeout);

                // birth day
                AdbHelper.InputNumber(_deviceId, _defaultPathExec + Constant.sourceNumber, FunctionHelper.GetRandomDay());
                Thread.Sleep(10);
                AdbHelper.InputNumber(_deviceId, _defaultPathExec + Constant.sourceNumber, FunctionHelper.GetRandomMonth());
                Thread.Sleep(10);
                AdbHelper.InputNumber(_deviceId, _defaultPathExec + Constant.sourceNumber, FunctionHelper.GetRandomYear());
                Thread.Sleep(10);
                AdbHelper.TapImg(_deviceId, _defaultPathExec + Constant.btnTiep);
                Thread.Sleep(_timeout);

                // male
                AdbHelper.TapImg(_deviceId, _defaultPathExec + FunctionHelper.getMaleRandom());
                Thread.Sleep(_timeout);

                // password
                AdbHelper.TapImg(_deviceId, _defaultPathExec + Constant.labelMatkhau);
                this._password = FunctionHelper.GetRandomMatkhau();
                AdbHelper.Input(_deviceId, this._password.ToCharArray());
                AdbHelper.TapImg(_deviceId, _defaultPathExec + Constant.btnDangKy);
                Thread.Sleep(_timeout);
                AdbHelper.TapImg(_deviceId, _defaultPathExec + Constant.btnOk);
                Thread.Sleep(_timeout);

                // wait for success
                var isSuccess = false;
                isSuccess = new WaitHelper(TimeSpan.FromSeconds(15)).Until(() =>
                {
                    while (FunctionHelper.IsExistImg(_deviceId, _defaultPathExec + Constant.labelCodeMail) == null)
                    {
                    }
                    return true;
                });

                if (isSuccess)
                {
                    new WebDriverWait(chromeDriver, TimeSpan.FromSeconds(20)).Until(x =>
                    {
                        while (true)
                        {
                            try
                            {
                                // code mail
                                webElement = x.FindElement(By.XPath("/html/body/main/div[1]/div/div[3]/div[2]/div/div[1]/div/div[4]/ul/li[2]"));
                                if (webElement != null && !string.IsNullOrEmpty(webElement.Text))
                                {
                                    var codeMail = System.Text.RegularExpressions.Regex.Match(webElement.Text, @"\d+").Value;
                                    AdbHelper.TapImg(_deviceId, _defaultPathExec + Constant.inputCodeMail);
                                    AdbHelper.Input(_deviceId, codeMail);
                                    Thread.Sleep(_timeout);
                                    AdbHelper.TapImg(_deviceId, _defaultPathExec + Constant.btnOk);
                                    Thread.Sleep(_timeout);
                                    AdbHelper.TapImg(_deviceId, _defaultPathExec + Constant.btnOk);
                                    Thread.Sleep(_timeout);
                                    AdbHelper.TapImg(_deviceId, _defaultPathExec + Constant.labelBoQua);
                                    Thread.Sleep(_timeout);
                                    if (FunctionHelper.IsExistImg(this._deviceId, _defaultPathExec + Constant.askBoQua) != null)
                                    {
                                        AdbHelper.TapImg(_deviceId, _defaultPathExec + Constant.askBoQua);
                                    }
                                    else
                                    {
                                        AdbHelper.TapImg(_deviceId, _defaultPathExec + Constant.labelBoQua);
                                    }
                                    Thread.Sleep(_timeout);
                                    return true;
                                }
                            }
                            catch (Exception ex)
                            {
                                log.Error(ex.Message);
                            }
                        }
                    });
                }
                return true;
            }
            catch (Exception ex)
            {
                log.Error(ex.Message);
                return false;
            }
        }

        public void ClearDataApp()
        {

        }

        public string GetCookie()
        {
            return _cookie;
        }

        public bool Turn2FA()
        {
            if (FunctionHelper.IsExistImg(this._deviceId, _defaultPathExec + Constant.iconSetting) != null)
            {
                AdbHelper.TapImg(this._deviceId, _defaultPathExec + Constant.iconSetting);
                Thread.Sleep(_timeout);
            }

            AdbHelper.TapImg(this._deviceId, _defaultPathExec + Constant.btnCaiDat);
            Thread.Sleep(_timeout);
            AdbHelper.TapImg(this._deviceId, _defaultPathExec + Constant.baoMat);
            Thread.Sleep(_timeout);
            AdbHelper.TapImg(this._deviceId, _defaultPathExec + Constant.xacThuc2YeuTo);
            Thread.Sleep(_timeout);
            AdbHelper.TapImg(this._deviceId, _defaultPathExec + Constant.fa2UngDung);
            Thread.Sleep(_timeout);

            if (FunctionHelper.IsExistImg(this._deviceId, _defaultPathExec + Constant.nhapMatKhau) != null)
            {
                AdbHelper.TapImg(this._deviceId, _defaultPathExec + Constant.nhapMatKhau);
                AdbHelper.Input(this._deviceId, this._password.ToCharArray());
                AdbHelper.TapImg(this._deviceId, _defaultPathExec + Constant.btnTiepTuc);

            }
            if (FunctionHelper.IsExistImg(this._deviceId, _defaultPathExec + Constant.screenCode2fa) != null)
            {
                this._2Fa = FunctionHelper.Get2faCurrentScreen(this._deviceId);
            }
            return true;
        }

        public string GetInfo()
        {
            this._2Fa = FunctionHelper.Get2faCurrentScreen(this._deviceId);
            return string.Join("|", _uid, _email, _password, _2Fa);
        }
    }
}
