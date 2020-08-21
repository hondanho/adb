using System;
using System.Drawing;
using System.Text.RegularExpressions;
using System.Threading;
using log4net;
using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;
using OpenQA.Selenium.Support.UI;

namespace auto_android.AutoHelper
{
    public class RegFb
    {
        private string _password, _email, _2Fa, _uid, _qrCode, _userName = string.Empty;
        private int _timeout = 1000;
        private string _defaultPathExec = Environment.CurrentDirectory;
        private ChromeDriver _chromeDriver;
        private ILog _log;
        private MEmuDevice _device;
        private MemuCommandHelper _memuHelper;

        private string _onedotonePck = "com.cloudflare.onedotonedotonedotone";
        private string _hmaPck = "com.hidemyass.hidemyassprovpn";
        private string _facebookLitePck = "com.facebook.lite";
        private string xpathCodeEmail = "/html/body/main/div[1]/div/div[3]/div[2]/div/div[1]/div/div[4]/ul/li[2]";
        private string xpathUsername = "//*[@id=\"facebook\"]/head/meta[14]";
        private string xpathUid = "//*[@id=\"facebook\"]/head/meta[10]";
        private string xpathEmail = "//*[@id=\"mail\"]";

        public RegFb(MEmuDevice device, MemuCommandHelper memuHelper, ILog log, int timeout = 1000)
        {
            _memuHelper = memuHelper;
            _device = device;
            this._timeout = timeout;
            _log = log;
            _chromeDriver = FunctionHelper.InitWebDriver();
            InitApp();
        }
        public void InitApp()
        {
            _memuHelper.ClearApp(_device.Id, _facebookLitePck);
            _memuHelper.SendKey(_device.Id, AdbKeyEvent.KEYCODE_HOME);
        }

        public void Turn1111()
        {
            _memuHelper.StartApp(_device.Id, _onedotonePck);
            Thread.Sleep(_timeout);
            _memuHelper.TapImg(_device.Id, _defaultPathExec + Constant.icon1111);
            Thread.Sleep(_timeout);

            var iconOff = _memuHelper.IsExistImg(_device.Id, _defaultPathExec + Constant.iconTurnOn1111);
            var iconOn = _memuHelper.IsExistImg(_device.Id, _defaultPathExec + Constant.iconTurned1111);
            while ((iconOff != null && iconOn == null) || (iconOff == null && iconOn == null))
            {
                _memuHelper.TapImg(_device.Id, _defaultPathExec + Constant.iconTurnOn1111);
                Thread.Sleep(_timeout);
                iconOff = _memuHelper.IsExistImg(_device.Id, _defaultPathExec + Constant.iconTurnOn1111);
                iconOn = _memuHelper.IsExistImg(_device.Id, _defaultPathExec + Constant.iconTurned1111);
            }
        }

        public void TurnHma()
        {
            _memuHelper.StartApp(_device.Id, _hmaPck);
            Thread.Sleep(_timeout);
            _memuHelper.TapImg(_device.Id, _defaultPathExec + Constant.iconHma);
            Thread.Sleep(_timeout);

            var iconOff = _memuHelper.IsExistImg(_device.Id, _defaultPathExec + Constant.iconOffHma);
            var iconOn = _memuHelper.IsExistImg(_device.Id, _defaultPathExec + Constant.iconOnHma);
            while ((iconOff != null && iconOn == null) || (iconOff == null && iconOn == null))
            {
                _memuHelper.TapImg(_device.Id, _defaultPathExec + Constant.iconOffHma);
                Thread.Sleep(_timeout);
                iconOff = _memuHelper.IsExistImg(_device.Id, _defaultPathExec + Constant.iconOffHma);
                iconOn = _memuHelper.IsExistImg(_device.Id, _defaultPathExec + Constant.iconOnHma);
            }
        }

        public bool RegisterFb()
        {
            try
            {
                this._uid = "error";
                // go to form reg
                _memuHelper.StartApp(_device.Id, _facebookLitePck);
                Thread.Sleep(_timeout);
                _memuHelper.TapImg(_device.Id, _defaultPathExec + Constant.iconFbLite);
                Thread.Sleep(_timeout);

                // set language

                var pointVtnRegVn = new WaitHelper(TimeSpan.FromSeconds(30)).Until(() =>
                {
                    var pointFound = _memuHelper.IsExistImg(_device.Id, _defaultPathExec + Constant.btnTaoMoiTaiKhoanVn);
                    if (pointFound == null)
                    {
                        var pointLabelLanguageVn = _memuHelper.IsExistImg(_device.Id, _defaultPathExec + Constant.labelLanguageVn);
                        if (!pointLabelLanguageVn.HasValue) return null;
                        _memuHelper.Tap(_device.Id, pointLabelLanguageVn.Value);
                        Thread.Sleep(1000);
                    }
                    return pointFound;
                });

                if (!pointVtnRegVn.HasValue)
                {
                    this._log.Error("Không tìm thấy Button \"Tạo mới tài khoản\"");
                    return false;
                }

                _memuHelper.Tap(_device.Id, pointVtnRegVn.Value);
                Thread.Sleep(1000);

                _memuHelper.TapImg(_device.Id, _defaultPathExec + Constant.btnTiep);
                Thread.Sleep(_timeout);

                // insert name
                _memuHelper.TapImg(_device.Id, _defaultPathExec + Constant.inputHo, new Point(0, 30));
                _memuHelper.Input(_device.Id, FunctionHelper.getHoRandom());
                _memuHelper.TapImg(_device.Id, _defaultPathExec + Constant.inputTen);
                _memuHelper.Input(_device.Id, FunctionHelper.getTenRandom());
                _memuHelper.TapImg(_device.Id, _defaultPathExec + Constant.btnTiep);
                Thread.Sleep(_timeout);
                _memuHelper.TapImg(_device.Id, _defaultPathExec + Constant.linkRegMail);
                Thread.Sleep(_timeout);

                // mail
                var inputMail = _memuHelper.TapImg(_device.Id, _defaultPathExec + Constant.labelMail, new Point(0, 30));
                _memuHelper.SwipeLong(_device.Id, new Point(20, inputMail.Value.Y), new Point(500, inputMail.Value.Y), 1500);
                _memuHelper.SendKey(_device.Id, AdbKeyEvent.KEYCODE_DEL);
                _chromeDriver.Navigate().GoToUrl("https://temp-mail.org/vi");
                Thread.Sleep(_timeout);
                var webElement = _chromeDriver.FindElement(By.XPath(xpathEmail));
                new WebDriverWait(_chromeDriver, TimeSpan.FromSeconds(30)).Until(x =>
                {
                    webElement = x.FindElement(By.XPath(xpathEmail));
                    this._email = webElement.GetAttribute("value");
                    return !string.IsNullOrEmpty(this._email) && this._email.Contains("@");
                });
                _memuHelper.Input(_device.Id, this._email);
                _memuHelper.TapImg(_device.Id, _defaultPathExec + Constant.btnTiep);
                Thread.Sleep(_timeout);

                // birth day
                _memuHelper.InputNumber(_device.Id, _defaultPathExec + Constant.sourceNumber, FunctionHelper.GetDayRandom());
                Thread.Sleep(10);
                _memuHelper.InputNumber(_device.Id, _defaultPathExec + Constant.sourceNumber, FunctionHelper.GetRandomMonth());
                Thread.Sleep(10);
                _memuHelper.InputNumber(_device.Id, _defaultPathExec + Constant.sourceNumber, FunctionHelper.GetYearRandom());
                Thread.Sleep(10);
                _memuHelper.TapImg(_device.Id, _defaultPathExec + Constant.btnTiep);
                Thread.Sleep(_timeout);

                // male
                _memuHelper.TapImg(_device.Id, _defaultPathExec + FunctionHelper.getMaleRandom());
                Thread.Sleep(_timeout);

                // password
                _memuHelper.TapImg(_device.Id, _defaultPathExec + Constant.labelMatkhau);
                this._password = FunctionHelper.GetMatkhauRandom();
                _memuHelper.Input(_device.Id, this._password);
                _memuHelper.TapImg(_device.Id, _defaultPathExec + Constant.btnDangKy);
                Thread.Sleep(_timeout);

                // wait for success
                // createStatus
                // 1: Success
                // 0: Checkpoint
                // null: time out
                byte? createStatus = new WaitHelper(TimeSpan.FromSeconds(30)).Until(() =>
                {
                    byte? res = null;
                    var isPass = _memuHelper.IsExistImg(_device.Id, _defaultPathExec + Constant.btnOkReg);
                    var isCheckpoint = _memuHelper.IsExistImg(_device.Id, _defaultPathExec + Constant.labelCheckpoint);

                    if (isPass != null) res = 1;
                    if (isCheckpoint != null) res = 0;

                    return res;
                });

                if (!createStatus.HasValue)
                {
                    this._log.Error("Bước khởi tạo Tài khoản lỗi");
                    return false;
                }

                if (createStatus == 0)
                {
                    this._log.Error("Tài khoản Checkpoint");
                    this._uid = "checkpoint";
                    return false;
                }

                if (createStatus == 1)
                {
                    _memuHelper.TapImg(_device.Id, _defaultPathExec + Constant.btnOkReg);
                    Thread.Sleep(_timeout);

                    new WebDriverWait(_chromeDriver, TimeSpan.FromSeconds(20)).Until(x =>
                    {
                        while (true)
                        {
                            try
                            {
                                // code mail
                                webElement = x.FindElement(By.XPath(xpathCodeEmail));
                                if (webElement != null && !string.IsNullOrEmpty(webElement.Text))
                                {
                                    var codeMail = Regex.Match(webElement.Text, @"\d+").Value;
                                    _memuHelper.TapImg(_device.Id, _defaultPathExec + Constant.inputCodeMail);
                                    _memuHelper.Input(_device.Id, codeMail);
                                    _chromeDriver.Close();

                                    return true;
                                }
                            }
                            catch (Exception ex)
                            {
                                _log.Error(ex.Message);
                            }
                        }
                    });

                    var pointBtnOk = _memuHelper.IsExistImg(this._device.Id, _defaultPathExec + Constant.btnOkReg);
                    var pointAskBoQua = _memuHelper.IsExistImg(this._device.Id, _defaultPathExec + Constant.askBoQua);
                    var pointlabelBoQua = _memuHelper.IsExistImg(this._device.Id, _defaultPathExec + Constant.labelBoQua);

                    while (pointBtnOk != null || pointAskBoQua != null || pointlabelBoQua != null)
                    {
                        if (pointAskBoQua != null)
                        {
                            _memuHelper.TapImg(_device.Id, _defaultPathExec + Constant.askBoQua);
                            Thread.Sleep(_timeout);
                        }
                        else if (pointBtnOk != null)
                        {
                            _memuHelper.TapImg(_device.Id, _defaultPathExec + Constant.btnOkReg);
                            Thread.Sleep(_timeout);
                        }
                        else if (pointlabelBoQua != null)
                        {
                            _memuHelper.TapImg(_device.Id, _defaultPathExec + Constant.labelBoQua);
                            Thread.Sleep(_timeout);
                        }

                        pointBtnOk = _memuHelper.IsExistImg(this._device.Id, _defaultPathExec + Constant.btnOkReg);
                        pointAskBoQua = _memuHelper.IsExistImg(this._device.Id, _defaultPathExec + Constant.askBoQua);
                        pointlabelBoQua = _memuHelper.IsExistImg(this._device.Id, _defaultPathExec + Constant.labelBoQua);
                    }

                    return true;
                }
                else
                {
                    return false;
                }
            }
            catch (Exception ex)
            {
                _log.Error(ex.Message);
                return false;
            }
        }

        public bool Turn2Fa()
        {
            var pointSetting1 = _memuHelper.IsExistImg(this._device.Id, _defaultPathExec + Constant.iconSetting);
            var pointSetting2 = _memuHelper.IsExistImg(this._device.Id, _defaultPathExec + Constant.iconSetting2);
            if (pointSetting1 != null || pointSetting2 != null)
            {
                if (pointSetting1 != null)
                {
                    _memuHelper.TapImg(this._device.Id, _defaultPathExec + Constant.iconSetting);
                    Thread.Sleep(_timeout);
                }
                else if (pointSetting2 != null)
                {
                    _memuHelper.TapImg(this._device.Id, _defaultPathExec + Constant.iconSetting2);
                    Thread.Sleep(_timeout);
                }
            }

            _memuHelper.TapImg(this._device.Id, _defaultPathExec + Constant.btnCaiDat);
            Thread.Sleep(_timeout);
            _memuHelper.TapImg(this._device.Id, _defaultPathExec + Constant.baoMat);
            Thread.Sleep(_timeout);
            _memuHelper.TapImg(this._device.Id, _defaultPathExec + Constant.xacThuc2YeuTo);
            Thread.Sleep(_timeout);
            _memuHelper.TapImg(this._device.Id, _defaultPathExec + Constant.fa2UngDung);
            Thread.Sleep(_timeout);

            if (_memuHelper.IsExistImg(this._device.Id, _defaultPathExec + Constant.nhapMatKhau) != null)
            {
                _memuHelper.TapImg(this._device.Id, _defaultPathExec + Constant.nhapMatKhau);
                _memuHelper.Input(this._device.Id, this._password);
                _memuHelper.TapImg(this._device.Id, _defaultPathExec + Constant.btnTiepTuc);
            }
            if (_memuHelper.IsExistImg(this._device.Id, _defaultPathExec + Constant.screenCode2fa) != null)
            {
                this._qrCode = _memuHelper.GetQRCode(this._device.Id);
                _memuHelper.TapImg(this._device.Id, _defaultPathExec + Constant.btnTiepTuc);
                Thread.Sleep(_timeout);
            }
            return true;
        }

        public string GetInfo()
        {
            if (!string.IsNullOrEmpty(this._qrCode))
            {
                this._2Fa = FunctionHelper.Get2faFromQR(this._qrCode);

                if (this._qrCode.Contains("ID:"))
                {
                    this._uid = FunctionHelper.GetUidFromQR(this._qrCode);
                    _chromeDriver.Navigate().GoToUrl(string.Format("https://www.facebook.com/{0}", this._uid));
                    Thread.Sleep(_timeout);
                    var webElement = _chromeDriver.FindElement(By.XPath(xpathUsername));
                    while (string.IsNullOrEmpty(webElement.GetAttribute("content")))
                    {
                        webElement = _chromeDriver.FindElement(By.XPath(xpathUsername));
                    }
                    this._userName = Regex.Match(webElement.GetAttribute("content"), "(?<=https://www.facebook.com/).*").Value;
                }
                else
                {
                    this._userName = FunctionHelper.GetUserNameFromQR(this._qrCode);
                    _chromeDriver.Navigate().GoToUrl(string.Format("https://www.facebook.com/{0}", this._userName));
                    Thread.Sleep(_timeout);
                    var webElement = _chromeDriver.FindElement(By.XPath(xpathUid));
                    while (string.IsNullOrEmpty(webElement.GetAttribute("content")))
                    {
                        webElement = _chromeDriver.FindElement(By.XPath(xpathUid));
                    }
                    this._uid = Regex.Match(webElement.GetAttribute("content"), "(?<=fb://profile/).*").Value;
                }
                _chromeDriver.Close();
            }

            // uid|password|2fa|email|username
            return string.Format("{0}", string.Join("|", _uid, _password, _2Fa, _email, _userName));
        }
    }
}
