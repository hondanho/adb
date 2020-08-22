using System;
using System.Collections.Generic;
using System.Drawing;
using System.IO;
using System.Text.RegularExpressions;
using System.Threading;
using AutoTool.AutoCommons;
using AutoTool.AutoMethods;
using AutoTool.Models;
using log4net;
using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;
using OpenQA.Selenium.Support.UI;

namespace AutoTool.AutoHelper
{
    public class RegFb
    {
        private string _qrCode = string.Empty;
        private FacebookAccountInfo _fb;
        private int _timeout = 1000;
        private string _defaultPathExec = Environment.CurrentDirectory;
        private ChromeDriver _chromeDriver;
        private ILog _log;
        private EmulatorInfo _device;
        private IEmulatorFunc _memuHelper;

        private string _onedotonePck = "com.cloudflare.onedotonedotonedotone";
        private string _hmaPck = "com.hidemyass.hidemyassprovpn";
        private string _facebookLitePck = "com.facebook.lite";
        private string _xpathCodeEmail = "/html/body/main/div[1]/div/div[3]/div[2]/div/div[1]/div/div[4]/ul/li[2]";
        private string _xpathUsername = "//*[@id=\"facebook\"]/head/meta[14]";
        private string _xpathUid = "//*[@id=\"facebook\"]/head/meta[10]";
        private string _xpathEmail = "//*[@id=\"mail\"]";

        // mbasic fb
        private string _linkLoginFb = "https://mbasic.facebook.com/";
        private string _xMbasicLoginUsername = "//*[@id='m_login_email']";
        private string _xMbasicLoginPassword = "//*[@id='login_form']/ul/li[2]/section/input";
        private string _xMbasicBtnLogin = "//*[@id='login_form']/ul/li[3]/input";

        public RegFb(EmulatorInfo device, IEmulatorFunc memuHelper, ILog log, int timeout = 1000)
        {
            _memuHelper = memuHelper;
            _device = device;
            this._timeout = timeout;
            _log = log;
            var random = new Random();
            _chromeDriver = FunctionHelper.InitWebDriver();
            _fb = new FacebookAccountInfo();
            _fb.FirstName = FunctionHelper.getFirstNameRandom();
            _fb.LastName = FunctionHelper.getLastNameRandom();
            _fb.Passwd = FunctionHelper.GetPasswordRandom();
            _fb.BirthDay = new DateTime(random.Next(1980, 2002), random.Next(1, 12), random.Next(1, 28));
            _fb.Gender = (FbGender)random.Next(0, 1);
        }

        public FbRegResult RegisterFacebook() {
            var result = new FbRegResult();
            _memuHelper.StartDevice(_device);

            Open1111();

            OpenFbLite();

            // Change language
            var setLaguageSuccess = SetFbLanguageToVn();
            if(!setLaguageSuccess)
            {
                result.Status = FbRegStatus.FAIL;
                result.Message = "Không thay đổi được ngôn ngữ sang tiếng Việt";
                return result;
            }

            // Input firstname, lastname
            _TapImg(_device, _defaultPathExec + Constant.inputHo, new Point(0, 30));
            _memuHelper.Input(_device, _fb.FirstName);
            _TapImg(_device, _defaultPathExec + Constant.inputTen);
            _memuHelper.Input(_device, _fb.LastName);

            // Chuyển sang bước tiếp theo (Đăng ký bằng Mobile hoặc Email)
            _TapImg(_device, _defaultPathExec + Constant.btnTiep);
            // Chuyển sang màn hình đăng ký với Email
            _TapImg(_device, _defaultPathExec + Constant.linkRegMail);


            // Lấy địa chỉ Email ở trang temp-mail.org
            // Sử dụng Chrome Driver
            var inputMail = _TapImg(_device, _defaultPathExec + Constant.labelMail, new Point(0, 30));
            _memuHelper.SwipeLong(_device, new Point(20, inputMail.Value.Y), new Point(500, inputMail.Value.Y), 1500);
            _memuHelper.SendKey(_device, AdbKeyEvent.KEYCODE_DEL);
            _chromeDriver.Navigate().GoToUrl("https://temp-mail.org/vi");
            Thread.Sleep(_timeout);
            var webElement = _chromeDriver.FindElement(By.XPath(_xpathEmail));
            new WebDriverWait(_chromeDriver, TimeSpan.FromSeconds(30)).Until(x =>
            {
                webElement = x.FindElement(By.XPath(_xpathEmail));
                _fb.Email = webElement.GetAttribute("value");
                return !string.IsNullOrEmpty(_fb.Email) && _fb.Email.Contains("@");
            });
            // Nhập địa chỉ Email
            _memuHelper.Input(_device, _fb.Email);

            // Chuyển sang bước tiếp theo (Bước nhập ngày tháng năm sinh)
            _TapImg(_device, _defaultPathExec + Constant.btnTiep);
            Thread.Sleep(_timeout);

            // Nhập ngày tháng năm sinh
            _InputNumber(_device, _defaultPathExec + Constant.sourceNumber, _fb.BirthDay.ToString("dd"));
            _InputNumber(_device, _defaultPathExec + Constant.sourceNumber, _fb.BirthDay.ToString("MM"));
            _InputNumber(_device, _defaultPathExec + Constant.sourceNumber, _fb.BirthDay.ToString("yyyy"));
            _TapImg(_device, _defaultPathExec + Constant.btnTiep);
            Thread.Sleep(_timeout);

            // Chọn giới tính
            _TapImg(_device, _defaultPathExec + ((int)_fb.Gender == 0 ? Constant.male : Constant.female));
            Thread.Sleep(_timeout);

            // Input Passwd
            _TapImg(_device, _defaultPathExec + Constant.labelMatkhau);
            _memuHelper.Input(_device, _fb.Passwd);

            // Tap vào Đăng ký
            _TapImg(_device, _defaultPathExec + Constant.btnDangKy);
            Thread.Sleep(_timeout);

            // Chờ và kiểm tra Xem đăng ký thành công hay bị Checkpoint
            // wait for success
            // createStatus
            // 1: Success
            // 0: Checkpoint
            // null: time out
            byte? createStatus = new WaitHelper(TimeSpan.FromSeconds(30)).Until(() =>
            {
                byte? res = null;
                var isPass = _IsExistImg(_device, _defaultPathExec + Constant.btnOkReg);
                var isCheckpoint = _IsExistImg(_device, _defaultPathExec + Constant.labelCheckpoint);

                if (isPass != null) res = 1;
                if (isCheckpoint != null) res = 0;

                return res;
            });

            if (!createStatus.HasValue)
            {
                result.Status = FbRegStatus.FAIL;
                result.Message = "Bước khởi tạo Tài khoản lỗi";
                this._log.Error(result.Message);
                return result;
            }

            if (createStatus == 0)
            {
                result.Status = FbRegStatus.CHECKPOINT;
                result.Message = "Tài khoản Checkpoint";
                this._log.Error(result.Message);
                return result;
            }

            result.Status = FbRegStatus.SUCCESS;
            result.Message = "Đã tạo được tài khoản";
            return result;
        }

        private void Open1111()
        {
            _memuHelper.StartApp(_device, _onedotonePck);
            Thread.Sleep(_timeout);

            var iconOff = _IsExistImg(_device, _defaultPathExec + Constant.iconTurnOn1111);
            var iconOn = _IsExistImg(_device, _defaultPathExec + Constant.iconTurned1111);
            while ((iconOff != null && iconOn == null) || (iconOff == null && iconOn == null))
            {
                _TapImg(_device, _defaultPathExec + Constant.iconTurnOn1111);
                Thread.Sleep(_timeout);
                iconOff = _IsExistImg(_device, _defaultPathExec + Constant.iconTurnOn1111);
                iconOn = _IsExistImg(_device, _defaultPathExec + Constant.iconTurned1111);
            }
        }

        private void OpenHma()
        {
            _memuHelper.StartApp(_device, _hmaPck);
            Thread.Sleep(_timeout);

            var iconOff = _IsExistImg(_device, _defaultPathExec + Constant.iconOffHma);
            var iconOn = _IsExistImg(_device, _defaultPathExec + Constant.iconOnHma);
            while ((iconOff != null && iconOn == null) || (iconOff == null && iconOn == null))
            {
                _TapImg(_device, _defaultPathExec + Constant.iconOffHma);
                Thread.Sleep(_timeout);
                iconOff = _IsExistImg(_device, _defaultPathExec + Constant.iconOffHma);
                iconOn = _IsExistImg(_device, _defaultPathExec + Constant.iconOnHma);
            }
        }

        private void OpenFbLite()
        {
            _memuHelper.ClearAppData(_device, _facebookLitePck);
            Thread.Sleep(_timeout);
            _memuHelper.StartApp(_device, _facebookLitePck);
            Thread.Sleep(_timeout);
        }

        private bool VerifyMail()
        {
            // Chuyển sang màn hình lấy code mail
            _TapImg(_device, _defaultPathExec + Constant.btnOkReg);
            Thread.Sleep(_timeout);

            // get code mail
            string codeMail = new WebDriverWait(_chromeDriver, TimeSpan.FromSeconds(30)).Until(x =>
            {
                string resultCodeMail = null;
                try
                {
                    var webElement = x.FindElement(By.XPath(_xpathCodeEmail));
                    if (webElement != null && !string.IsNullOrEmpty(webElement.Text))
                    {
                        var code = Regex.Match(webElement.Text, @"\d+").Value;
                        if (!string.IsNullOrEmpty(code)) resultCodeMail = code;
                    }
                }
                catch (Exception ex)
                {
                    _log.Error(ex.Message);
                }

                return resultCodeMail;
            });

            if (string.IsNullOrEmpty(codeMail)) return false;

            _TapImg(_device, _defaultPathExec + Constant.inputCodeMail);
            _memuHelper.Input(_device, codeMail);

            // go to home
            //GoToHomeAfterVerify();

            return true;
        }

        public bool TurnOn2Fa()
        {
            _chromeDriver.Navigate().GoToUrl(_linkLoginFb);
            Thread.Sleep(_timeout);
            var elementUsername = _chromeDriver.FindElement(By.XPath(_xMbasicLoginUsername));
            elementUsername.SendKeys(_fb.Email);
            Thread.Sleep(1000);
            var elementPassword = _chromeDriver.FindElement(By.XPath(_xMbasicLoginUsername));
            elementPassword.SendKeys(_fb.Passwd);
            Thread.Sleep(1000);
            var elementBtnLogin = _chromeDriver.FindElement(By.XPath(_xMbasicLoginUsername));
            elementBtnLogin.Click();
            Thread.Sleep(1000);

            var isHasUsername = new WebDriverWait(_chromeDriver, TimeSpan.FromSeconds(20)).Until(() =>
            {
                try
                {
                    return _chromeDriver.FindElement(By.XPath(_xMbasicLoginUsername)).Displayed;
                }
                catch (Exception)
                {
                    return false;
                }
            });


            return true;
        }

        private void GoToHomeAfterVerify()
        {
            var pointBtnOk = _IsExistImg(_device, _defaultPathExec + Constant.btnOkReg);
            var pointAskBoQua = _IsExistImg(_device, _defaultPathExec + Constant.askBoQua);
            var pointlabelBoQua = _IsExistImg(_device, _defaultPathExec + Constant.labelBoQua);
            while (pointBtnOk != null || pointAskBoQua != null || pointlabelBoQua != null)
            {
                if (pointAskBoQua != null)
                {
                    _TapImg(_device, _defaultPathExec + Constant.askBoQua);
                    Thread.Sleep(_timeout);
                }
                else if (pointBtnOk != null)
                {
                    _TapImg(_device, _defaultPathExec + Constant.btnOkReg);
                    Thread.Sleep(_timeout);
                }
                else if (pointlabelBoQua != null)
                {
                    _TapImg(_device, _defaultPathExec + Constant.labelBoQua);
                    Thread.Sleep(_timeout);
                }

                pointBtnOk = _IsExistImg(_device, _defaultPathExec + Constant.btnOkReg);
                pointAskBoQua = _IsExistImg(_device, _defaultPathExec + Constant.askBoQua);
                pointlabelBoQua = _IsExistImg(_device, _defaultPathExec + Constant.labelBoQua);
            }
        }

        private bool SetFbLanguageToVn()
        {
            var pointVtnRegVn = new WaitHelper(TimeSpan.FromSeconds(30)).Until(() =>
            {
                var pointFound = _IsExistImg(_device, _defaultPathExec + Constant.btnTaoMoiTaiKhoanVn);
                if (pointFound == null)
                {
                    var pointLabelLanguageVn = _IsExistImg(_device, _defaultPathExec + Constant.labelLanguageVn);
                    if (!pointLabelLanguageVn.HasValue) return null;
                    _memuHelper.Tap(_device, pointLabelLanguageVn.Value);
                    Thread.Sleep(100);
                }
                return pointFound;
            });

            if (!pointVtnRegVn.HasValue)
            {
                this._log.Error("Không tìm thấy Button \"Tạo mới tài khoản\"");
                return false;
            }

            _memuHelper.Tap(_device, pointVtnRegVn.Value);
            _TapImg(_device, _defaultPathExec + Constant.btnTiep);
            return true;
        }

        public bool Turn2Fa()
        {
            var pointSetting1 = _IsExistImg(_device, _defaultPathExec + Constant.iconSetting);
            var pointSetting2 = _IsExistImg(_device, _defaultPathExec + Constant.iconSetting2);
            if (pointSetting1 != null || pointSetting2 != null)
            {
                if (pointSetting1 != null)
                {
                    _TapImg(this._device, _defaultPathExec + Constant.iconSetting);
                    Thread.Sleep(_timeout);
                }
                else if (pointSetting2 != null)
                {
                    _TapImg(this._device, _defaultPathExec + Constant.iconSetting2);
                    Thread.Sleep(_timeout);
                }
            }

            _TapImg(this._device, _defaultPathExec + Constant.btnCaiDat);
            Thread.Sleep(_timeout);
            _TapImg(this._device, _defaultPathExec + Constant.baoMat);
            Thread.Sleep(_timeout);
            _TapImg(this._device, _defaultPathExec + Constant.xacThuc2YeuTo);
            Thread.Sleep(_timeout);
            _TapImg(this._device, _defaultPathExec + Constant.fa2UngDung);
            Thread.Sleep(_timeout);

            if (_IsExistImg(_device, _defaultPathExec + Constant.nhapMatKhau) != null)
            {
                _TapImg(_device, _defaultPathExec + Constant.nhapMatKhau);
                _memuHelper.Input(_device, _fb.Passwd);
                _TapImg(_device, _defaultPathExec + Constant.btnTiepTuc);
            }
            if (_IsExistImg(_device, _defaultPathExec + Constant.screenCode2fa) != null)
            {
                _qrCode = _GetCurrentQRCode(_device);
                _TapImg(_device, _defaultPathExec + Constant.btnTiepTuc);
                Thread.Sleep(_timeout);
            }
            return true;
        }

        public string GetInfo()
        {
            if (!string.IsNullOrEmpty(_qrCode))
            {
                _fb.TwoFacAuth = FunctionHelper.Get2faFromQR(_qrCode);

                if (this._qrCode.Contains("ID:"))
                {
                    _fb.Uid = FunctionHelper.GetUidFromQR(_qrCode);
                    _chromeDriver.Navigate().GoToUrl(string.Format("https://www.facebook.com/{0}", _fb.Uid));
                    Thread.Sleep(_timeout);
                    var webElement = _chromeDriver.FindElement(By.XPath(_xpathUsername));
                    while (string.IsNullOrEmpty(webElement.GetAttribute("content")))
                    {
                        webElement = _chromeDriver.FindElement(By.XPath(_xpathUsername));
                    }
                    _fb.Username = Regex.Match(webElement.GetAttribute("content"), "(?<=https://www.facebook.com/).*").Value;
                }
                else
                {
                    _fb.Username = FunctionHelper.GetUserNameFromQR(this._qrCode);
                    _chromeDriver.Navigate().GoToUrl(string.Format("https://www.facebook.com/{0}", _fb.Username));
                    Thread.Sleep(_timeout);
                    var webElement = _chromeDriver.FindElement(By.XPath(_xpathUid));
                    while (string.IsNullOrEmpty(webElement.GetAttribute("content")))
                    {
                        webElement = _chromeDriver.FindElement(By.XPath(_xpathUid));
                    }
                    _fb.Uid = Regex.Match(webElement.GetAttribute("content"), "(?<=fb://profile/).*").Value;
                }
                _chromeDriver.Close();
            }

            // uid|password|2fa|email|username
            return _fb.StringInfo();
        }

        #region Additional methods

        public void _InputNumber(EmulatorInfo device, string numberDir, string number)
        {
            if (string.IsNullOrEmpty(numberDir)) return;
            numberDir.Replace("/", "\\");
            if (!numberDir.EndsWith(@"\"))
            {
                numberDir = numberDir + "\\";
            }
            var source = number.ToCharArray();
            var lstImgs = new List<string>();
            for (int i = 0; i < source.Length; i++)
            {
                var numberPath = string.Format("{0}{1}.png", numberDir, source[i]);
                lstImgs.Add(numberPath);
            }

            _TapImgs(device, lstImgs);
        }

        public void _InputNumber(EmulatorInfo device, string numberDir, int number)
        {
            _InputNumber(device, numberDir, number.ToString());
        }

        private Point? _TapImg(EmulatorInfo device, string path, Point? pointAdd = null)
        {
            var screenPath = string.Format("{0}\\data\\{1}.png", Environment.CurrentDirectory, DateTime.Now.Ticks);
            while (!File.Exists(screenPath))
            {
                _memuHelper.ScreenShot(device, screenPath);
            }

            var point = ImageScanOpenCV.FindOutPoint(screenPath, path);
            while (point == null)
            {
                Thread.Sleep(500);
                File.Delete(screenPath);
                _log.Error(string.Format("Not found :{0} in {1}", path, screenPath));
                _memuHelper.ScreenShot(device, screenPath);
                point = ImageScanOpenCV.FindOutPoint(screenPath, path);
            }

            point = pointAdd == null ? point : new Point(point.Value.X + pointAdd.Value.X, point.Value.Y + pointAdd.Value.Y);
            _memuHelper.Tap(device, point.Value);
            File.Delete(screenPath);

            return point;
        }

        private void _TapImgs(EmulatorInfo device, List<string> lstPath, Point? pointAdd = null)
        {
            var screenPath = string.Format("{0}\\data\\{1}.png", Environment.CurrentDirectory, DateTime.Now.Ticks);
            while (!File.Exists(screenPath))
            {
                _memuHelper.ScreenShot(device, screenPath);
            }

            foreach (var path in lstPath)
            {
                var point = ImageScanOpenCV.FindOutPoint(screenPath, path);
                while (point == null)
                {
                    File.Delete(screenPath);
                    _log.Error(string.Format("Not found :{0} in {1}", path, screenPath));
                    _memuHelper.ScreenShot(device, screenPath);
                    point = ImageScanOpenCV.FindOutPoint(screenPath, path);
                }

                point = pointAdd == null ? point : new Point(point.Value.X + pointAdd.Value.X, point.Value.Y + pointAdd.Value.Y);
                _memuHelper.Tap(device, point.Value);
            }

            File.Delete(screenPath);
        }

        private string _GetCurrentQRCode(EmulatorInfo device)
        {
            var screenPath = string.Format("{0}\\data\\{1}.png", Environment.CurrentDirectory, DateTime.Now.Ticks);
            _memuHelper.ScreenShot(device, screenPath);

            return QRCode.DecodeQR(screenPath);
        }

        private Point? _IsExistImg(EmulatorInfo device, string subPath)
        {
            var screenPath = string.Format("{0}\\data\\{1}.png", Environment.CurrentDirectory, DateTime.Now.Ticks);
            _memuHelper.ScreenShot(device, screenPath);

            var point = ImageScanOpenCV.FindOutPoint(screenPath, subPath);
            File.Delete(screenPath);
            return point;
        }

        #endregion
    }
}
