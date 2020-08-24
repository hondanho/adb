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
        private IEmulatorFunc _EmulatorFunc;
        private UserSetting _userSetting;
        private RegFbType _regType;

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

        private string _facebookPackageName = "com.facebook.katana";
        private string _oneDotOnePackageName = "com.cloudflare.onedotonedotonedotone";

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

        public RegFb(EmulatorInfo device, UserSetting userSetting, RegFbType regType, int timeout = 1000)
        {
            _userSetting = userSetting;
            _regType = regType;
            switch (_regType)
            {
                case RegFbType.REG_WITH_MEMU:
                    _EmulatorFunc = new MEmuFunc();
                    break;
                case RegFbType.REG_WITH_LDPLAYER:
                default:
                    _EmulatorFunc = new LDPlayerFunc();
                    break;
            }
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
            if (_regType == RegFbType.REG_WITH_MEMU)
            {
                return RegisterFacebookMEmuFbLite();
            }
            else
            {
                return RegisterFacebookLDPlayerFbKatana();
            }
        }

        private FbRegResult RegisterFacebookLDPlayerFbKatana()
        {
            var screenShotPath = string.Format("{0}\\data\\{1}.png", Environment.CurrentDirectory, DateTime.Now.Ticks);
            var result = new FbRegResult();

            try
            {
                // Start device to register
                var deviceStarted = _EmulatorFunc.StartDevice(_device);
                if (!deviceStarted)
                {
                    result.Status = FbRegStatus.FAIL;
                    result.Message = "Không mở được device";
                    _log.Error(result.Message);
                    return result;
                }

                // Start 1.1.1.1
                _EmulatorFunc.StartApp(_device, _oneDotOnePackageName);
                
                // Finding button Get started on home screen and Tap it
                new WaitHelper(TimeSpan.FromSeconds(30)).Until(() => {
                    _EmulatorFunc.ScreenShot(_device, screenShotPath);
                    var point = ImageScanOpenCV.FindOutPoint(screenShotPath,
                        _defaultPathExec + Constant.TemplateOneDotOne.BtnGetStarted);
                    if (point == null) return false;

                    return _EmulatorFunc.Tap(_device, point.Point);
                });
                // Finding button Done on intro screen and Tap it
                new WaitHelper(TimeSpan.FromSeconds(30)).Until(() => {
                    _EmulatorFunc.ScreenShot(_device, screenShotPath);
                    var point = ImageScanOpenCV.FindOutPoint(screenShotPath,
                        _defaultPathExec + Constant.TemplateOneDotOne.BtnIntroDone);
                    if (point == null) return false;

                    return _EmulatorFunc.Tap(_device, point.Point);
                });
                // Finding button Accept on Term & Privacy screen and Tap it
                new WaitHelper(TimeSpan.FromSeconds(30)).Until(() => {
                    _EmulatorFunc.ScreenShot(_device, screenShotPath);
                    var point = ImageScanOpenCV.FindOutPoint(screenShotPath,
                        _defaultPathExec + Constant.TemplateOneDotOne.BtnAccept);
                    if (point == null) return false;

                    return _EmulatorFunc.Tap(_device, point.Point);
                });
                // Finding button Disconnected on screen and Tap it
                new WaitHelper(TimeSpan.FromSeconds(30)).Until(() => {
                    _EmulatorFunc.ScreenShot(_device, screenShotPath);
                    var pointDisconnected = ImageScanOpenCV.FindOutPoint(screenShotPath,
                        _defaultPathExec + Constant.TemplateOneDotOne.Disconnected);
                    var pointConnected = ImageScanOpenCV.FindOutPoint(screenShotPath,
                        _defaultPathExec + Constant.TemplateOneDotOne.Connected);

                    if (pointConnected == null
                        && pointDisconnected == null) return false;

                    if (pointDisconnected != null)
                    {
                        return _EmulatorFunc.Tap(_device, pointDisconnected.Point);
                    }

                    return true;
                });

                // Start app facebook
                _EmulatorFunc.StartApp(_device, _facebookPackageName);

                //  
            }
            catch (Exception ex)
            {
                _log.Error(ex);
                result.Status = FbRegStatus.FAIL;
                result.Message = "Tạo tài khoản lỗi (" + ex.Message + ")";
            }
            finally
            {
                _EmulatorFunc.ClearAppData(_device, _facebookPackageName);
                _EmulatorFunc.ClearAppData(_device, _oneDotOnePackageName);
            }

            return result;
        } 

        private FbRegResult RegisterFacebookMEmuFbLite()
        {
            var result = new FbRegResult();
            try
            {
                var mEmuStarted = _EmulatorFunc.StartDevice(_device);
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
                var setLaguageSuccess = SetFbLanguageToVnFbLite();
                if (!setLaguageSuccess)
                {
                    result.Status = FbRegStatus.FAIL;
                    result.Message = "Không thay đổi được ngôn ngữ sang tiếng Việt";
                    return result;
                }

                // Input firstname, lastname
                _TapImg(_device, _defaultPathExec + Constant.inputHo, 30, new ImagePoint(0, 30));
                _EmulatorFunc.Input(_device, FbAcc.FirstName);
                _TapImg(_device, _defaultPathExec + Constant.inputTen);
                _EmulatorFunc.Input(_device, FbAcc.LastName);

                // Chuyển sang bước tiếp theo (Đăng ký bằng Mobile hoặc Email)
                _TapImg(_device, _defaultPathExec + Constant.btnTiep);
                // Chuyển sang màn hình đăng ký với Email
                _TapImg(_device, _defaultPathExec + Constant.linkRegMail);


                // Lấy địa chỉ Email ở trang temp-mail.org
                // Sử dụng Chrome Driver
                var emailPoint = FindOutPoint(_device, _defaultPathExec + Constant.labelMail);
                _EmulatorFunc.Tap(_device, new Point(emailPoint.X, emailPoint.Y + 30));
                //var inputMail = _TapImg(_device, _defaultPathExec + Constant.labelMail, new ImagePoint(0, 30));
                
                _EmulatorFunc.SwipeLong(_device, new Point(20, emailPoint.Y), new Point(500, emailPoint.Y), 1500);
                _EmulatorFunc.SendKey(_device, AdbKeyEvent.KEYCODE_DEL);

                _chromeDriver.Navigate().GoToUrl(_urlTemplateMail);
                Thread.Sleep(_timeout);

                new WaitHelper(TimeSpan.FromSeconds(30)).Until(() =>
                {
                    var webElement = _chromeDriver.FindElement(By.XPath(_xpathEmail));
                    FbAcc.Email = webElement.GetAttribute("value");
                    return !string.IsNullOrEmpty(FbAcc.Email) && FbAcc.Email.Contains("@");
                });

                // Nhập địa chỉ Email
                _EmulatorFunc.Input(_device, FbAcc.Email);
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
                _EmulatorFunc.Input(_device, FbAcc.Passwd);

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
                    var isPass = FindOutPoint(_device, _defaultPathExec + Constant.btnOkReg);
                    var isCheckpoint = FindOutPoint(_device, _defaultPathExec + Constant.labelCheckpoint);


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

                var veri = VerifyMailFbLite();

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
            _EmulatorFunc.ClearAppData(_device, _onedotonePck);
            _EmulatorFunc.StartApp(_device, _onedotonePck);
            _TapImg(_device, _defaultPathExec + Constant.getStarted1111);
            _TapImg(_device, _defaultPathExec + Constant.done1111);
            _TapImg(_device, _defaultPathExec + Constant.accept1111);
            
            var isConnectedPoint = new WaitHelper(TimeSpan.FromSeconds(50)).Until(() =>
            {
                var connected = FindOutPoint(_device, _defaultPathExec + Constant.btnOpened1111);
                if (connected == null)
                {
                    _TapImg(_device, _defaultPathExec + Constant.btnNotOpen1111);
                }
                return connected;
            });

            return isConnectedPoint != null;
        }

        private void OpenHma()
        {
            _EmulatorFunc.StartApp(_device, _hmaPck);
            Thread.Sleep(_timeout);

            var iconOff = FindOutPoint(_device, _defaultPathExec + Constant.iconOffHma);
            var iconOn = FindOutPoint(_device, _defaultPathExec + Constant.iconOnHma);
            while ((iconOff != null && iconOn == null) || (iconOff == null && iconOn == null))
            {
                _TapImg(_device, _defaultPathExec + Constant.iconOffHma);
                iconOff = FindOutPoint(_device, _defaultPathExec + Constant.iconOffHma);
                iconOn = FindOutPoint(_device, _defaultPathExec + Constant.iconOnHma);
            }
        }

        private void OpenFbLite()
        {
            _EmulatorFunc.ClearAppData(_device, _facebookLitePck);
            _EmulatorFunc.StartApp(_device, _facebookLitePck);
        }

        private bool VerifyMailFbLite()
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
                _EmulatorFunc.Input(_device, codeMail);

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
            var pointBtnOk = FindOutPoint(_device, _defaultPathExec + Constant.btnOkReg);
            var pointAskBoQua = FindOutPoint(_device, _defaultPathExec + Constant.askBoQua);
            var pointlabelBoQua = FindOutPoint(_device, _defaultPathExec + Constant.labelBoQua);
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

                pointBtnOk = FindOutPoint(_device, _defaultPathExec + Constant.btnOkReg);
                pointAskBoQua = FindOutPoint(_device, _defaultPathExec + Constant.askBoQua);
                pointlabelBoQua = FindOutPoint(_device, _defaultPathExec + Constant.labelBoQua);
            }
        }

        private bool SetFbLanguageToVnFbLite()
        {
            var pointVtnRegVn = new WaitHelper(TimeSpan.FromSeconds(30)).Until(() =>
            {
                var pointFound = FindOutPoint(_device, _defaultPathExec + Constant.btnTaoMoiTaiKhoanVn);
                if (pointFound == null)
                {
                    var pointLabelLanguageVn = FindOutPoint(_device, _defaultPathExec + Constant.labelLanguageVn);
                    if (pointLabelLanguageVn == null) return null;
                    _EmulatorFunc.Tap(_device, pointLabelLanguageVn.Point);
                    Thread.Sleep(100);
                }
                return pointFound;
            });

            if (pointVtnRegVn == null)
            {
                this._log.Error("Không tìm thấy Button \"Tạo mới tài khoản\"");
                return false;
            }

            _EmulatorFunc.Tap(_device, pointVtnRegVn.Point);
            _TapImg(_device, _defaultPathExec + Constant.btnTiep);
            return true;
        }

        private bool Turn2FaOnEmulator()
        {
            var pointSetting1 = FindOutPoint(_device, _defaultPathExec + Constant.iconSetting);
            var pointSetting2 = FindOutPoint(_device, _defaultPathExec + Constant.iconSetting2);
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

            if (FindOutPoint(_device, _defaultPathExec + Constant.nhapMatKhau) != null)
            {
                _TapImg(_device, _defaultPathExec + Constant.nhapMatKhau);
                _EmulatorFunc.Input(_device, FbAcc.Passwd);
                _TapImg(_device, _defaultPathExec + Constant.btnTiepTuc);
            }
            if (FindOutPoint(_device, _defaultPathExec + Constant.screenCode2fa) != null)
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

        public bool IsImgValid(string path)
        {
            try
            {
                if(File.Exists(path))
                {
                    using (var fs = new FileStream(path, FileMode.Open))
                    {
                        if (fs.CanRead && (Bitmap)Image.FromStream(fs) != null)
                        {
                            return true;
                        }
                    }
                }
            }
            catch
            {
                return false;
            }

            return false;
        }

        private bool _TapImg(EmulatorInfo device, string path, int timeOutInSecond = 30, ImagePoint offsetPoint = null)
        {
            var point = new WaitHelper(TimeSpan.FromSeconds(timeOutInSecond)).Until(() =>
            {
                try
                {
                    return FindOutPoint(device, path);
                }
                catch
                {
                    return null;
                }
            });

            if (point == null) return false;

            if (offsetPoint != null)
            {
                point = new ImagePoint(point.X + offsetPoint.X, point.Y + offsetPoint.Y);
            }
            return _EmulatorFunc.Tap(device, point.Point);
        }

        private void _TapImgs(EmulatorInfo device, List<string> lstPath, Point? pointAdd = null)
        {
            var screenPath = string.Format("{0}\\data\\{1}.png", Environment.CurrentDirectory, DateTime.Now.Ticks);

            foreach (var path in lstPath)
            {
                var point = new WaitHelper(TimeSpan.FromSeconds(20)).Until(() =>
                {
                    try
                    {
                        _EmulatorFunc.ScreenShot(device, screenPath);
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
                        point = new ImagePoint(point.X + pointAdd.Value.X, point.Y + pointAdd.Value.Y);
                    }

                    _EmulatorFunc.Tap(device, point.Point);
                }
            }

            File.Delete(screenPath);
        }

        private string _GetCurrentQRCode(EmulatorInfo device)
        {
            var screenPath = string.Format("{0}\\data\\{1}.png", Environment.CurrentDirectory, DateTime.Now.Ticks);
            _EmulatorFunc.ScreenShot(device, screenPath);

            return QRCode.DecodeQR(screenPath);
        }

        private ImagePoint FindOutPoint(string mainPath, string subPath, bool getMiddle = true)
        {
            return ImageScanOpenCV.FindOutPoint(mainPath, subPath, getMiddle);
        }

        private ImagePoint FindOutPoint(EmulatorInfo device, string subPath, bool getMiddle = true)
        {
            try
            {
                var screenPath = string.Format("{0}\\data\\{1}.png", Environment.CurrentDirectory, DateTime.Now.Ticks);
                _EmulatorFunc.ScreenShot(device, screenPath);

                var point = FindOutPoint(screenPath, subPath, getMiddle);
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
