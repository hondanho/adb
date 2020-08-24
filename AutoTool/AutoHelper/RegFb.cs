using System;
using System.Collections.Generic;
using System.Drawing;
using System.IO;
using System.Reflection;
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
    public class RegFb : IDisposable
    {
        private string _qrCode = string.Empty;
        public FacebookAccountInfo FbAcc;
        private int _timeout = 1000;
        private string _defaultPathExec = Environment.CurrentDirectory;
        private ChromeDriver _chromeDriver;
        private ILog _log;
        private EmulatorInfo _device;
        private IEmulatorFunc _memuHelper;
        private UserSetting _userSetting;

        private string _onedotonePck = "com.cloudflare.onedotonedotonedotone";
        private string _hmaPck = "com.hidemyass.hidemyassprovpn";
        private string _facebookLitePck = "com.facebook.lite";
        private string _xpathCodeEmail = "/html/body/main/div[1]/div/div[3]/div[2]/div/div[1]/div/div[4]/ul/li[2]";
        private string _xpathUsername = "//*[@id=\"facebook\"]/head/meta[14]";
        private string _xpathUid = "//*[@id=\"facebook\"]/head/meta[10]";
        private string _xpathEmail = "//*[@id=\"mail\"]";

        // template mail
        private string _urlTemplateMail = "https://temp-mail.org/vi";
        private string _xpathChangeMail = "//*[@id='click-to-delete']";

        // mbasic fb
        private string _linkMbasic = "https://mbasic.facebook.com/";
        private string _linkMbasic2faIntro = "https://mbasic.facebook.com/security/2fac/setup/intro/metadata/?source=1";
        private string _linkMbasic2faSetting = "https://mbasic.facebook.com/security/2fac/settings/?_rdr";
        private string _linkMbasic2faQrcode = "https://mbasic.facebook.com/security/2fac/setup/qrcode";
        private string _linkMbasic2faTypeCode = "https://mbasic.facebook.com/security/2fac/setup/type_code";
        private string _linkMbasic2faOutro = "https://mbasic.facebook.com/security/2fac/setup/outro";

        private string _xMbasicLoginUsername = "//*[@id='m_login_email']";
        private string _xMbasicLoginPassword = "//*[@id='login_form']/ul/li[2]/section/input";
        private string _xMbasicBtnLogin = "//*[@id='login_form']/ul/li[3]/input";
        private string _xMbasicBtnLogout = "//*[@id='mbasic_logout_button']";
        private string _xMbasicBtnUseAuthApp = "//*[@id='root']/table/tbody/tr/td/div/div/div/div/div[1]/div/table/tbody/tr/td[2]/div/div[3]/a";
        //private string _zMbasicQrcode = "//*[@id='root']/table/tbody/tr/td/form/div[2]/div/table/tbody/tr/td/div/div[1]/div/img";
        private string _xMbasic2faSecret = "//*[@id='root']/table/tbody/tr/td/form/div[2]/div/table/tbody/tr/td/div/div[2]/div[2]";
        private string _xMbasicBtnQrConfirm = "//*[@id='qr_confirm_button']";
        private string _xMbasicInputTotp = "//*[@id='type_code_container']";
        private string _xMbasicBtnSubmitTotp = "//*[@id='submit_code_button']";
        //private string _xMbasicBtn2faDone = "//*[@id='TwoFactButton']/form/input[2]";
        private string _xMbasicHrefRemove2fa = "//*[@id='root']/table/tbody/tr/td/div[2]/div/div[2]/div[2]/div[2]/a";

        private string _xMbasicInputLoginApprovalCode = "//*[@id='approvals_code']";
        private string _xMbasicBtnSubmitLoginApprovalCode = "//*[@id='checkpointSubmitButton-actual-button']";
        private string _xMbasicBtnContinueLogin = "//*[@id='checkpointSubmitButton-actual-button']";

        ~RegFb()
        {
            this.Dispose();
        }

        public void Dispose()
        {
            if (_chromeDriver != null)
            {
                try
                {
                    _chromeDriver.Close();
                }
                catch (Exception ex)
                {
                    _log.Error(ex);
                }
                finally
                {
                    _chromeDriver.Dispose();
                    GC.SuppressFinalize(this);
                }
            }
        }

        public RegFb(EmulatorInfo device, UserSetting userSetting, int timeout = 1000)
        {
            _userSetting = userSetting;
            _memuHelper = new MEmuFunc();
            _device = device;
            _timeout = timeout;
            _log = LogManager.GetLogger(MethodBase.GetCurrentMethod().DeclaringType);
            _chromeDriver = FunctionHelper.InitWebDriver(_userSetting, device.Index);

            // Init Facebook info
            var random = new Random();
            FbAcc = new FacebookAccountInfo();
            FbAcc.FirstName = FunctionHelper.getFirstNameRandom();
            FbAcc.LastName = FunctionHelper.getLastNameRandom();
            FbAcc.Passwd = FunctionHelper.GetPasswordRandom();
            FbAcc.BirthDay = new DateTime(random.Next(1980, 2002), random.Next(1, 12), random.Next(1, 28));
            FbAcc.Gender = (FbGender)random.Next(0, 1);
        }

        public RegFb(FacebookAccountInfo fb, int idx)
        {
            FbAcc = fb;
            _chromeDriver = FunctionHelper.InitWebDriver(_userSetting, idx);
            _log = LogManager.GetLogger(MethodBase.GetCurrentMethod().DeclaringType);
        }

        public FbRegResult RegisterFacebook()
        {
            var result = new FbRegResult();
            try
            {
                var mEmuStarted = _memuHelper.StartDevice(_device);
                if (!mEmuStarted)
                {
                    result.Status = FbRegStatus.FAIL;
                    result.Message = "Không mở được MEmu device";
                    _log.Error(result.Message);
                    return result;
                }

                var stateOpen1111 = Open1111();
                if (!stateOpen1111)
                {
                    result.Status = FbRegStatus.FAIL;
                    result.Message = "Không mở được 1111";
                    _log.Error(result.Message);
                    return result;
                }

                OpenFbLite();

                // Change language
                var setLaguageSuccess = SetFbLanguageToVn();
                if (!setLaguageSuccess)
                {
                    result.Status = FbRegStatus.FAIL;
                    result.Message = "Không thay đổi được ngôn ngữ sang tiếng Việt";
                    return result;
                }

                // Input firstname, lastname
                _TapImg(_device, _defaultPathExec + Constant.inputHo, new ImagePoint(0, 30));
                _memuHelper.Input(_device, FbAcc.FirstName);
                _TapImg(_device, _defaultPathExec + Constant.inputTen);
                _memuHelper.Input(_device, FbAcc.LastName);

                // Chuyển sang bước tiếp theo (Đăng ký bằng Mobile hoặc Email)
                _TapImg(_device, _defaultPathExec + Constant.btnTiep);
                // Chuyển sang màn hình đăng ký với Email
                _TapImg(_device, _defaultPathExec + Constant.linkRegMail);


                // Lấy địa chỉ Email ở trang temp-mail.org
                // Sử dụng Chrome Driver

                var pointInputMail = _IsExistImg(_device, _defaultPathExec + Constant.labelMail);
                if (pointInputMail == null) {
                    result.Status = FbRegStatus.FAIL;
                    result.Message = "Bước điền email bi lỗi.";
                    this._log.Error(result.Message);
                    return result;
                }

                _memuHelper.Tap(_device, new Point(pointInputMail.X, pointInputMail.Y + 30));
                _memuHelper.SwipeLong(_device, new Point(20, pointInputMail.Y), new Point(500, pointInputMail.Y), 1500);
                _memuHelper.SendKey(_device, AdbKeyEvent.KEYCODE_DEL);

                _chromeDriver.Navigate().GoToUrl(_urlTemplateMail);
                Thread.Sleep(_timeout);

                new WaitHelper(TimeSpan.FromSeconds(30)).Until(() =>
                {
                    var webElement = _chromeDriver.FindElement(By.XPath(_xpathEmail));
                    FbAcc.Email = webElement.GetAttribute("value");
                    return !string.IsNullOrEmpty(FbAcc.Email) && FbAcc.Email.Contains("@");
                });

                // Nhập địa chỉ Email
                _memuHelper.Input(_device, FbAcc.Email);
                Thread.Sleep(_timeout);

                // Chuyển sang bước tiếp theo (Bước nhập ngày tháng năm sinh)
                _TapImg(_device, _defaultPathExec + Constant.btnTiep);
                Thread.Sleep(_timeout);

                // Nhập ngày tháng năm sinh
                _InputNumber(_device, _defaultPathExec + Constant.sourceNumber, FbAcc.BirthDay.ToString("dd"));
                _InputNumber(_device, _defaultPathExec + Constant.sourceNumber, FbAcc.BirthDay.ToString("MM"));
                _InputNumber(_device, _defaultPathExec + Constant.sourceNumber, FbAcc.BirthDay.ToString("yyyy"));
                _TapImg(_device, _defaultPathExec + Constant.btnTiep);
                Thread.Sleep(_timeout);

                // Chọn giới tính
                _TapImg(_device, _defaultPathExec + ((int)FbAcc.Gender == 0 ? Constant.male : Constant.female));
                Thread.Sleep(_timeout);

                // Input Passwd
                _TapImg(_device, _defaultPathExec + Constant.labelMatkhau);
                _memuHelper.Input(_device, FbAcc.Passwd);

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

                result.Status = FbRegStatus.CREATED;
                result.Message = "Đã tạo được tài khoản";

                var veri = VerifyMail();

                if (veri)
                {
                    result.Status = FbRegStatus.SUCCESS_WITH_VERI;
                    result.Message = "Đã tạo được tài khoản veri";
                }

                return result;
            }
            catch (Exception ex)
            {
                _log.Error(ex.Message);
                result.Status = FbRegStatus.FAIL;
                result.Message = "Tạo tài khoản lỗi (" + ex.Message + ")";
                return result;
            }
        }

        private bool Open1111()
        {
            _memuHelper.ClearAppData(_device, _onedotonePck);
            _memuHelper.StartApp(_device, _onedotonePck);
            _TapImg(_device, _defaultPathExec + Constant.getStarted1111);
            _TapImg(_device, _defaultPathExec + Constant.done1111);
            _TapImg(_device, _defaultPathExec + Constant.accept1111);
            
            var isConnectedPoint = new WaitHelper(TimeSpan.FromSeconds(50)).Until(() =>
            {
                var connected = _IsExistImg(_device, _defaultPathExec + Constant.btnOpened1111) != null;
                if (!connected)
                {
                    _TapImg(_device, _defaultPathExec + Constant.btnNotOpen1111);
                }
                return connected;
            });

            return isConnectedPoint;
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
                iconOff = _IsExistImg(_device, _defaultPathExec + Constant.iconOffHma);
                iconOn = _IsExistImg(_device, _defaultPathExec + Constant.iconOnHma);
            }
        }

        private void OpenFbLite()
        {
            _memuHelper.ClearAppData(_device, _facebookLitePck);
            _memuHelper.StartApp(_device, _facebookLitePck);
        }

        private bool VerifyMail()
        {
            try
            {
                // Chuyển sang màn hình lấy code mail
                _TapImg(_device, _defaultPathExec + Constant.btnOkReg);

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
            catch (Exception ex)
            {
                _log.Error(ex.Message);
                return false;
            }
        }

        public bool GetUid()
        {
            try
            {
                // Check cookie
                _chromeDriver.Navigate().GoToUrl(_linkMbasic);
                Thread.Sleep(3000);
                var cookie = _chromeDriver.Manage().Cookies.GetCookieNamed("c_user");

                if (cookie != null)
                {
                    FbAcc.Uid = cookie.Value;
                    return true;
                }

                // Nếu chưa đăng nhập
                _chromeDriver.Manage().Cookies.DeleteAllCookies();
                _chromeDriver.Navigate().GoToUrl(_linkMbasic);
                Thread.Sleep(3000);

                var elementUsername = _chromeDriver.FindElement(By.XPath(_xMbasicLoginUsername));
                elementUsername.SendKeys(FbAcc.Email);
                Thread.Sleep(1000);
                var elementPassword = _chromeDriver.FindElement(By.XPath(_xMbasicLoginPassword));
                elementPassword.SendKeys(FbAcc.Passwd);
                Thread.Sleep(1000);
                var elementBtnLogin = _chromeDriver.FindElement(By.XPath(_xMbasicBtnLogin));
                elementBtnLogin.Click();
                Thread.Sleep(3000);

                if (!string.IsNullOrEmpty(FbAcc.TwoFacAuth))
                {
                    var inputLoginApprovalCode = _chromeDriver.FindElement(By.XPath(_xMbasicInputLoginApprovalCode));
                    inputLoginApprovalCode.SendKeys(FunctionHelper.GetTotp(FbAcc.TwoFacAuth));
                    Thread.Sleep(3000);
                    var btnSubmitApprovalLoginCode = _chromeDriver.FindElement(By.XPath(_xMbasicBtnSubmitLoginApprovalCode));
                    btnSubmitApprovalLoginCode.Click();
                    Thread.Sleep(3000);
                    var btnContinueLogin = _chromeDriver.FindElement(By.XPath(_xMbasicBtnContinueLogin));
                    btnContinueLogin.Click();
                    Thread.Sleep(3000);
                }

                cookie = _chromeDriver.Manage().Cookies.GetCookieNamed("c_user");
                FbAcc.Uid = cookie.Value;

                return true;
            }
            catch (Exception ex)
            {
                _log.Error(ex.Message);
                return false;
            }
        }

        public bool TurnOn2Fa()
        {
            try
            {
                if (!string.IsNullOrEmpty(FbAcc.TwoFacAuth)) return true;

                _chromeDriver.Manage().Cookies.DeleteAllCookies();
                _chromeDriver.Navigate().GoToUrl(_linkMbasic);
                Thread.Sleep(_timeout);
                var elementUsername = _chromeDriver.FindElement(By.XPath(_xMbasicLoginUsername));
                elementUsername.SendKeys(FbAcc.Email);
                Thread.Sleep(1000);
                var elementPassword = _chromeDriver.FindElement(By.XPath(_xMbasicLoginPassword));
                elementPassword.SendKeys(FbAcc.Passwd);
                Thread.Sleep(1000);
                var elementBtnLogin = _chromeDriver.FindElement(By.XPath(_xMbasicBtnLogin));
                elementBtnLogin.Click();
                Thread.Sleep(3000);

                // Chuyển đến trang Bật xác minh 2 bước
                _chromeDriver.Navigate().GoToUrl(_linkMbasic2faIntro);
                Thread.Sleep(3000);

                // Khi có link Logout (đã đăng nhập thành công)
                bool hasLogOutButton = new WaitHelper<ChromeDriver>(_chromeDriver, TimeSpan.FromSeconds(30)).Until((driver) =>
                {
                    try
                    {
                        return driver.FindElement(By.XPath(_xMbasicBtnLogout)).Displayed;
                    }
                    catch (Exception)
                    {
                        return false;
                    }
                });

                if (!hasLogOutButton)
                {
                    _log.Error("Đăng nhập không thành công");
                    return false;
                }

                // Tìm button "Dùng ứng dụng xác thực"
                var btnUseAuthApp = _chromeDriver.FindElement(By.XPath(_xMbasicBtnUseAuthApp));
                btnUseAuthApp.Click();
                Thread.Sleep(3000);

                // Check url trang QRCode
                bool isQrCodePage = new WaitHelper<ChromeDriver>(_chromeDriver, TimeSpan.FromSeconds(30)).Until((driver) =>
                {
                    try
                    {
                        return driver.Url.StartsWith(_linkMbasic2faQrcode);
                    }
                    catch (Exception)
                    {
                        return false;
                    }
                });

                // Lấy 2FA Secret key
                var lbl2faSecret = _chromeDriver.FindElement(By.XPath(_xMbasic2faSecret));
                var secret2fa = lbl2faSecret.Text;
                secret2fa = Regex.Replace(secret2fa, @"\s+", string.Empty);

                // Lấy TOTP
                var totp = FunctionHelper.GetTotp(secret2fa);

                // Click tiếp tục
                var btnQrConfirm = _chromeDriver.FindElement(By.XPath(_xMbasicBtnQrConfirm));
                btnQrConfirm.Click();
                Thread.Sleep(3000);

                // Check url trang QRCode
                bool isTypeCodePage = new WaitHelper<ChromeDriver>(_chromeDriver, TimeSpan.FromSeconds(30)).Until((driver) =>
                {
                    try
                    {
                        return driver.Url.StartsWith(_linkMbasic2faTypeCode);
                    }
                    catch (Exception)
                    {
                        return false;
                    }
                });

                // Input Totp
                var inputTotp = _chromeDriver.FindElement(By.XPath(_xMbasicInputTotp));
                inputTotp.SendKeys(totp);
                Thread.Sleep(3000);

                // Click continue
                var btnSubmitTotp = _chromeDriver.FindElement(By.XPath(_xMbasicBtnSubmitTotp));
                btnSubmitTotp.Click();
                Thread.Sleep(3000);

                // Check url trang 2fa Outro
                bool is2faOutroPage = new WaitHelper<ChromeDriver>(_chromeDriver, TimeSpan.FromSeconds(30)).Until((driver) =>
                {
                    try
                    {
                        return driver.Url.StartsWith(_linkMbasic2faOutro);
                    }
                    catch (Exception)
                    {
                        return false;
                    }
                });

                // Click done
                //var btn2faDone = _chromeDriver.FindElement(By.XPath(_xMbasicBtn2faDone));
                //btn2faDone.Click();
                //Thread.Sleep(3000);

                // Goto 2fa setting page
                _chromeDriver.Navigate().GoToUrl(_linkMbasic2faSetting);
                Thread.Sleep(3000);

                // Khi có link remove 2fa
                bool hasRemove2fa = new WaitHelper<ChromeDriver>(_chromeDriver, TimeSpan.FromSeconds(30)).Until((driver) =>
                {
                    try
                    {
                        return driver.FindElement(By.XPath(_xMbasicHrefRemove2fa)).Displayed;
                    }
                    catch (Exception)
                    {
                        return false;
                    }
                });

                if (hasRemove2fa)
                {
                    FbAcc.TwoFacAuth = secret2fa;
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
                }
                else if (pointBtnOk != null)
                {
                    _TapImg(_device, _defaultPathExec + Constant.btnOkReg);
                }
                else if (pointlabelBoQua != null)
                {
                    _TapImg(_device, _defaultPathExec + Constant.labelBoQua);
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
                    if (pointLabelLanguageVn == null)
                    {
                        return null;
                    } 
                    else
                    {
                        _memuHelper.Tap(_device, pointLabelLanguageVn.Point);
                        Thread.Sleep(100);
                    }
                }
                return pointFound;
            });

            if (pointVtnRegVn == null)
            {
                this._log.Error("Không tìm thấy Button \"Tạo mới tài khoản\"");
                return false;
            }

            _memuHelper.Tap(_device, pointVtnRegVn.Point);
            Thread.Sleep(500);
            _TapImg(_device, _defaultPathExec + Constant.btnTiep);

            return true;
        }

        private bool Turn2FaOnEmulator()
        {
            var pointSetting1 = _IsExistImg(_device, _defaultPathExec + Constant.iconSetting);
            var pointSetting2 = _IsExistImg(_device, _defaultPathExec + Constant.iconSetting2);
            if (pointSetting1 != null || pointSetting2 != null)
            {
                if (pointSetting1 != null)
                {
                    _TapImg(this._device, _defaultPathExec + Constant.iconSetting);
                }
                else if (pointSetting2 != null)
                {
                    _TapImg(this._device, _defaultPathExec + Constant.iconSetting2);
                }
            }

            _TapImg(this._device, _defaultPathExec + Constant.btnCaiDat);
            _TapImg(this._device, _defaultPathExec + Constant.baoMat);
            _TapImg(this._device, _defaultPathExec + Constant.xacThuc2YeuTo);
            _TapImg(this._device, _defaultPathExec + Constant.fa2UngDung);

            if (_IsExistImg(_device, _defaultPathExec + Constant.nhapMatKhau) != null)
            {
                _TapImg(_device, _defaultPathExec + Constant.nhapMatKhau);
                _memuHelper.Input(_device, FbAcc.Passwd);
                _TapImg(_device, _defaultPathExec + Constant.btnTiepTuc);
            }
            if (_IsExistImg(_device, _defaultPathExec + Constant.screenCode2fa) != null)
            {
                _qrCode = _GetCurrentQRCode(_device);
                _TapImg(_device, _defaultPathExec + Constant.btnTiepTuc);
            }
            return true;
        }

        private string GetInfo()
        {
            if (!string.IsNullOrEmpty(_qrCode))
            {
                FbAcc.TwoFacAuth = FunctionHelper.Get2faFromQR(_qrCode);

                if (this._qrCode.Contains("ID:"))
                {
                    FbAcc.Uid = FunctionHelper.GetUidFromQR(_qrCode);
                    _chromeDriver.Navigate().GoToUrl(string.Format("https://www.facebook.com/{0}", FbAcc.Uid));
                    Thread.Sleep(_timeout);
                    var webElement = _chromeDriver.FindElement(By.XPath(_xpathUsername));
                    while (string.IsNullOrEmpty(webElement.GetAttribute("content")))
                    {
                        webElement = _chromeDriver.FindElement(By.XPath(_xpathUsername));
                    }
                    FbAcc.Username = Regex.Match(webElement.GetAttribute("content"), "(?<=https://www.facebook.com/).*").Value;
                }
                else
                {
                    FbAcc.Username = FunctionHelper.GetUserNameFromQR(this._qrCode);
                    _chromeDriver.Navigate().GoToUrl(string.Format("https://www.facebook.com/{0}", FbAcc.Username));
                    Thread.Sleep(_timeout);
                    var webElement = _chromeDriver.FindElement(By.XPath(_xpathUid));
                    while (string.IsNullOrEmpty(webElement.GetAttribute("content")))
                    {
                        webElement = _chromeDriver.FindElement(By.XPath(_xpathUid));
                    }
                    FbAcc.Uid = Regex.Match(webElement.GetAttribute("content"), "(?<=fb://profile/).*").Value;
                }
                _chromeDriver.Close();
            }

            // uid|password|2fa|email|username
            return FbAcc.StringInfo();
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

        private ImagePoint _TapImg(EmulatorInfo device, string path, ImagePoint pointAdd)
        {
            var screenPath = string.Format("{0}\\data\\{1}.png", Environment.CurrentDirectory, DateTime.Now.Ticks);

            var point = new WaitHelper(TimeSpan.FromSeconds(30)).Until(() =>
            {
                try
                {
                    _memuHelper.ScreenShot(device, screenPath);
                    return ImageScanOpenCV.FindOutPoint(screenPath, path);
                }
                catch
                {
                    return null;
                }
            });

            if (point != null)
            {
                if (pointAdd != null)
                {
                    point = new ImagePoint(point.X + pointAdd.X, point.Y + pointAdd.Y);
                }
                _memuHelper.Tap(device, point.Point);
            }

            File.Delete(screenPath);
            return point;
        }

        private bool _TapImg(EmulatorInfo device, string path)
        {
            var screenPath = string.Format("{0}\\data\\{1}.png", Environment.CurrentDirectory, DateTime.Now.Ticks);

            return new WaitHelper(TimeSpan.FromSeconds(30)).Until(() =>
            {
                try
                {
                    _memuHelper.ScreenShot(device, screenPath);
                    var point = ImageScanOpenCV.FindOutPoint(screenPath, path);
                    if (point != null)
                    {
                        _memuHelper.Tap(device, point.Point);
                        File.Delete(screenPath);
                        return true;
                    }
                    
                    return false;
                }
                catch
                {
                    return false;
                }
            });
        }

        private ImagePoint NumberBasePoint(string path, int number)
        {
            Bitmap numPadImg = ImageScanOpenCV.GetImage(path);
            var width = numPadImg.Width;
            var height = numPadImg.Height;
            var ww = 3;
            var hh = 4;
            var bh = height / hh;
            var bw = width / ww;
            if (number > 9 || number <= 0) number = 11;
            var xx = number % ww == 0 ? ww - 1 : (number % ww) - 1;
            var yy = number % ww > 0 ? number / ww : (number / ww) - 1;
            var x = xx * bw + bw / 2;
            var y = yy * bh + bh / 2;

            return new ImagePoint(x, y);
        }

        private void _TapImgs(EmulatorInfo device, List<string> lstPath, ImagePoint pointAdd = null)
        {
            var screenPath = string.Format("{0}\\data\\{1}.png", Environment.CurrentDirectory, DateTime.Now.Ticks);
            var isHasImg = false;

            foreach (var path in lstPath)
            {
                new WaitHelper(TimeSpan.FromSeconds(30)).Until(() =>
                {
                    try
                    {
                        _memuHelper.ScreenShot(device, screenPath);
                        var point = ImageScanOpenCV.FindOutPoint(screenPath, path);
                        if (point != null)
                        {
                            if (pointAdd != null)
                            {
                                point = new ImagePoint(point.X + pointAdd.X, point.Y + pointAdd.Y);
                            }

                            _memuHelper.Tap(device, point.Point);
                            return true;
                        }

                        return false;
                    }
                    catch
                    {
                        return false;
                    }
                });
            }

            File.Delete(screenPath);
        }

        private string _GetCurrentQRCode(EmulatorInfo device)
        {
            var screenPath = string.Format("{0}\\data\\{1}.png", Environment.CurrentDirectory, DateTime.Now.Ticks);
            _memuHelper.ScreenShot(device, screenPath);

            return QRCode.DecodeQR(screenPath);
        }

        private ImagePoint _IsExistImg(EmulatorInfo device, string subPath)
        {
            try
            {
                var screenPath = string.Format("{0}\\data\\{1}.png", Environment.CurrentDirectory, DateTime.Now.Ticks);
                _memuHelper.ScreenShot(device, screenPath);

                var point = ImageScanOpenCV.FindOutPoint(screenPath, subPath);
                File.Delete(screenPath);
                return point;
            }
            catch
            {
                return null;
            }
        }

        #endregion
    }
}
