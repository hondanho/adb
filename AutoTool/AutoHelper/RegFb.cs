using System;
using System.Collections.Generic;
using System.Drawing;
using System.IO;
using System.Reflection;
using System.Security.Policy;
using System.Text.RegularExpressions;
using System.Threading;
using AutoTool.AutoCommons;
using AutoTool.AutoHelper.EmailHelper;
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
        public delegate void LogTrace(string message);
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
        private BaseEmailHelper _emailHelper;

        private string _onedotonePck = "com.cloudflare.onedotonedotonedotone";
        private string _hmaPck = "com.hidemyass.hidemyassprovpn";
        private string _facebookLitePck = "com.facebook.lite";
        private string _xpathCodeEmail = "/html/body/main/div[1]/div/div[3]/div[2]/div/div[1]/div/div[4]/ul/li[2]";
        private string _xpathUsername = "//*[@id=\"facebook\"]/head/meta[14]";
        private string _xpathUid = "//*[@id=\"facebook\"]/head/meta[10]";
        private string _xpathEmail = "//*[@id=\"mail\"]";

        // template mail
        private string _urlTemplateMail = "https://temp-mail.org/vi";
        //private string _xpathChangeMail = "//*[@id='click-to-delete']";

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

        //private string ButtonRequestReviewXpath = "//*[@id='checkpointBottomBar']/input";

        private string _facebookPackageName = "com.facebook.katana";
        private string _oneDotOnePackageName = "com.cloudflare.onedotonedotonedotone";

        private LogTrace _logTrace;

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
                    GC.Collect();
                    GC.SuppressFinalize(this);
                }
            }
            if (_EmulatorFunc != null)
            {
                _EmulatorFunc.ClearAppData(_device, _facebookPackageName);
                _EmulatorFunc.ClearAppData(_device, _oneDotOnePackageName);
                _EmulatorFunc.SetProxy(_device, ":0");
                _EmulatorFunc.StopDevice(_device);
            }
        }

        public RegFb(EmulatorInfo device, LogTrace logTrace, UserSetting userSetting, RegFbType regType, int timeout = 1000)
        {
            _logTrace = logTrace;
            _device = device;

            LogStepTrace("Initialing..");

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
            _timeout = timeout;
            _log = LogManager.GetLogger(MethodBase.GetCurrentMethod().DeclaringType);
            string proxy = null;
            if (GlobalVar.UseProxy)
            {
                proxy = device.Proxy;
            }
            _chromeDriver = FunctionHelper.InitWebDriver(_userSetting, device.Index, proxy);

            _emailHelper = new SmailPro(_chromeDriver);
            // Init Facebook info
            var random = new Random();
            FbAcc = new FacebookAccountInfo();
            FbAcc.FirstName = FunctionHelper.getFirstNameRandom();
            FbAcc.LastName = FunctionHelper.getLastNameRandom();
            FbAcc.Passwd = FunctionHelper.GetPasswordRandom();
            FbAcc.BirthDay = new DateTime(random.Next(1980, 2002), random.Next(1, 12), random.Next(1, 28));
            FbAcc.Gender = (FbGender)random.Next(0, 2);
        }

        public RegFb(FacebookAccountInfo fb, int idx)
        {
            FbAcc = fb;
            _chromeDriver = FunctionHelper.InitWebDriver(_userSetting, idx);
            _log = LogManager.GetLogger(MethodBase.GetCurrentMethod().DeclaringType);
        }

        public void LogStepTrace(string message)
        {
            _logTrace.Invoke($"{DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss,fff")}: {_device.Id}<>{_device.Name}: {message}");
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
                // Random config
                LogStepTrace("Set random config");
                _device.Config = new EmulatorConfig
                {
                    Imei = "auto",
                    Imsi = "auto",
                    PhoneNumber = FunctionHelper.RandPhoneNumber(),
                    SimSerial = "auto",
                    Mac = "auto"
                };
                _EmulatorFunc.SetConfig(_device);

                // Start device to register
                LogStepTrace("Start device");
                var deviceStarted = _EmulatorFunc.StartDevice(_device);
                if (!deviceStarted)
                {
                    result.Status = FbRegStatus.FAIL;
                    result.Message = "Không mở được device";
                    _log.Error(result.Message);
                    return result;
                }

                LogStepTrace("Clear facebook, 1.1.1.1 data");
                _EmulatorFunc.ClearAppData(_device, _facebookPackageName);
                _EmulatorFunc.ClearAppData(_device, _oneDotOnePackageName);
                Thread.Sleep(1000);

                if (GlobalVar.UseProxy)
                {
                    // Set proxy
                    LogStepTrace("Set proxy");
                    _EmulatorFunc.SetProxy(_device, _device.Proxy);
                    Thread.Sleep(120);
                }
                else
                {
                    // Start 1.1.1.1
                    LogStepTrace("Start 1.1.1.1");
                    _EmulatorFunc.StartApp(_device, _oneDotOnePackageName);
                    Thread.Sleep(120);

                    // Finding button Get started on home screen and Tap it
                    LogStepTrace("Tap button \"Get started\"");
                    _EmulatorFunc.TapImage(_device, _defaultPathExec + Constant.TemplateOneDotOne.BtnGetStarted);
                    Thread.Sleep(120);
                    LogStepTrace("Tap button \"Done\"");
                    // Finding button Done on intro screen and Tap it
                    _EmulatorFunc.TapImage(_device, _defaultPathExec + Constant.TemplateOneDotOne.BtnIntroDone);
                    Thread.Sleep(120);
                    LogStepTrace("Tap button \"Accept\"");
                    // Finding button Accept on Term & Privacy screen and Tap it
                    _EmulatorFunc.TapImage(_device, _defaultPathExec + Constant.TemplateOneDotOne.BtnAccept);
                    Thread.Sleep(120);
                    LogStepTrace("Finding button Disconnected or Connected");
                    // Finding button Disconnected on screen and Tap it
                    new WaitHelper(TimeSpan.FromSeconds(30)).Until(() => {
                        var pointDisconnected = _EmulatorFunc.FindOutPoint(_device,
                            _defaultPathExec + Constant.TemplateOneDotOne.Disconnected);
                        var pointConnected = _EmulatorFunc.FindOutPoint(_device,
                            _defaultPathExec + Constant.TemplateOneDotOne.Connected);

                        if (pointConnected == null
                            && pointDisconnected == null) return false;

                        if (pointDisconnected != null)
                        {
                            LogStepTrace(">>>> Tap button Disconnected");
                            _EmulatorFunc.Tap(_device, pointDisconnected.Point);
                            return false;
                        }

                        return true;
                    });
                }

                // Start app facebook
                LogStepTrace("Start Facebook");
                _EmulatorFunc.StartApp(_device, _facebookPackageName);
                Thread.Sleep(120);
                // Tap button Create New Facebook Account
                LogStepTrace("Tap button \"Create new facebook account\"");
                _EmulatorFunc.TapImage(_device, _defaultPathExec + Constant.TemplateFacebook.BtnCreateNewFacebookAccount, 60);
                // Tap button Next
                LogStepTrace("Tap button \"Next\"");
                _EmulatorFunc.TapImage(_device, _defaultPathExec + Constant.TemplateFacebook.BtnCreateAccountNext, 30);
                Thread.Sleep(120);
                // Tap First Name input and Send text
                LogStepTrace("Tap input \"First name\"");
                _EmulatorFunc.TapImage(_device, _defaultPathExec + Constant.TemplateFacebook.InputFirstNameClicked);
                Thread.Sleep(120);
                LogStepTrace("Input First name <" + FbAcc.FirstName + ">");
                _EmulatorFunc.Input(_device, FbAcc.FirstName);
                Thread.Sleep(120);
                // Tap Last Name input and Send text
                LogStepTrace("Tap input \"Last name\"");
                _EmulatorFunc.TapImage(_device, _defaultPathExec + Constant.TemplateFacebook.InputLastName);
                Thread.Sleep(120);
                LogStepTrace("Input Last name <" + FbAcc.LastName + ">");
                _EmulatorFunc.Input(_device, FbAcc.LastName);
                Thread.Sleep(120);
                // Tap button next
                LogStepTrace("Tap button \"Next\"");
                _EmulatorFunc.TapImage(_device, _defaultPathExec + Constant.TemplateFacebook.BtnNameNext);
                Thread.Sleep(120);
                // Calculate point of Birtday
                var inputBirthdayTop = new WaitHelper(TimeSpan.FromSeconds(30)).Until(() => {
                    return _EmulatorFunc.FindOutPoint(_device, _defaultPathExec + Constant.TemplateFacebook.InputBirthdayTop, false);
                });
                if (inputBirthdayTop == null)
                {
                    LogStepTrace("Không tìm thấy phần nhập Birthday");
                    result.Status = FbRegStatus.FAIL;
                    result.Message = "Không tìm thấy phần nhập Birthday";
                    this._log.Error(result.Message);
                    return result;
                }
                var baseBirthdayImage = ImageScanOpenCV.GetImage(_defaultPathExec + Constant.TemplateFacebook.InputBirthdayBase);
                var baseWith = baseBirthdayImage.Width / 3;
                var yy = inputBirthdayTop.Y + baseBirthdayImage.Height / 2;
                var xx = inputBirthdayTop.X = baseBirthdayImage.Width / 2;
                var monthPoint = new ImagePoint(xx, yy);
                var dayPoint = new ImagePoint(xx + baseWith, yy);
                var yearPoint = new ImagePoint(xx + baseWith * 2, yy);

                // tap month, day, year and send text
                LogStepTrace("Tap Birth month input");
                _EmulatorFunc.Tap(_device, monthPoint.Point);
                Thread.Sleep(120);
                LogStepTrace("Input Birth month <" + FbAcc.BirthDay.Month + ">");
                _EmulatorFunc.Input(_device, FunctionHelper.GetMonthAsText(FbAcc.BirthDay.Month));
                Thread.Sleep(120);
                LogStepTrace("Tap Birth day input");
                _EmulatorFunc.Tap(_device, dayPoint.Point);
                Thread.Sleep(120);
                LogStepTrace("Input Birth day <" + FbAcc.BirthDay.Day + ">");
                _EmulatorFunc.Input(_device, FbAcc.BirthDay.ToString("dd"));
                Thread.Sleep(120);
                LogStepTrace("Tap Birth year input");
                _EmulatorFunc.Tap(_device, yearPoint.Point);
                Thread.Sleep(120);
                LogStepTrace("Input Birth year <" + FbAcc.BirthDay.Year + ">");
                _EmulatorFunc.Input(_device, FbAcc.BirthDay.ToString("yyyy"));
                Thread.Sleep(120);
                // Tap next
                LogStepTrace("Tap button \"Next\"");
                _EmulatorFunc.TapImage(_device, _defaultPathExec + Constant.TemplateFacebook.BtnBirthdayNext);
                Thread.Sleep(120);
                // Tap gender
                LogStepTrace("Tap Gender <" + FbAcc.Gender + ">");
                if (FbAcc.Gender == FbGender.FEMALE)
                {
                    _EmulatorFunc.TapImage(_device, _defaultPathExec + Constant.TemplateFacebook.OptionGenderFemale);
                } else
                {
                    _EmulatorFunc.TapImage(_device, _defaultPathExec + Constant.TemplateFacebook.OptionGenderMale);
                }
                Thread.Sleep(120);
                // Tap Next
                LogStepTrace("Tap button \"Next\"");
                _EmulatorFunc.TapImage(_device, _defaultPathExec + Constant.TemplateFacebook.BtnGenderNext);
                Thread.Sleep(120);
                // Tap register with email address
                LogStepTrace("Tap link label \"Sign up with email address\"");
                _EmulatorFunc.TapImage(_device, _defaultPathExec + Constant.TemplateFacebook.LblSignUpWithEmail);
                Thread.Sleep(120);

                // Get Email
                //LogStepTrace("Go to " + _urlTemplateMail + " on chrome");
                //_chromeDriver.Navigate().GoToUrl(_urlTemplateMail);
                //Thread.Sleep(_timeout);

                //LogStepTrace("Getting email address from chrome");
                //new WaitHelper(TimeSpan.FromSeconds(30)).Until(() =>
                //{
                //    var webElement = _chromeDriver.FindElement(By.XPath(_xpathEmail));
                //    FbAcc.Email = webElement.GetAttribute("value");
                //    return !string.IsNullOrEmpty(FbAcc.Email) && FbAcc.Email.Contains("@");
                //});

                // Get email
                LogStepTrace("Getting Email address");
                _emailHelper.GetEmailAddress();
                FbAcc.Email = _emailHelper.EmailAddress;

                // Input email address
                LogStepTrace("Tap input Email address");
                _EmulatorFunc.TapImage(_device, _defaultPathExec + Constant.TemplateFacebook.InputEmailAddress);
                Thread.Sleep(120);
                LogStepTrace("Input Email address <" + FbAcc.Email + ">");
                _EmulatorFunc.Input(_device, FbAcc.Email);
                Thread.Sleep(120);
                // Tap next
                LogStepTrace("Tap button \"Next\"");
                _EmulatorFunc.TapImage(_device, _defaultPathExec + Constant.TemplateFacebook.BtnEmailAddressNext);
                Thread.Sleep(120);

                // Tap passwd
                LogStepTrace("Tap input \"Password\"");
                _EmulatorFunc.TapImage(_device, _defaultPathExec + Constant.TemplateFacebook.InputPasswd);
                Thread.Sleep(120);
                LogStepTrace("Input password <" + FbAcc.Passwd + ">");
                _EmulatorFunc.Input(_device, FbAcc.Passwd);
                Thread.Sleep(120);
                // Tap next
                LogStepTrace("Tap button \"Next\"");
                _EmulatorFunc.TapImage(_device, _defaultPathExec + Constant.TemplateFacebook.BtnPasswdNext);
                Thread.Sleep(120);

                // Tap Sign Up
                LogStepTrace("Tap button \"Sign Up\"");
                _EmulatorFunc.TapImage(_device, _defaultPathExec + Constant.TemplateFacebook.BtnSignUp);
                Thread.Sleep(120);

                // Chờ và kiểm tra Xem đăng ký thành công hay bị Checkpoint
                // wait for success
                // createStatus
                // null: time out
                LogStepTrace(".. Waitting for Signing-In ..");
                WaitingData<FbRegStatus> createStatus = new WaitHelper(TimeSpan.FromSeconds(60)).Until(() => {
                    WaitingData<FbRegStatus> waiter = null;
                    bool hasMatchSavePasswd = false;
                    while(true)
                    {
                        var pointCheckpoint = _EmulatorFunc.FindOutPoint(_device, _defaultPathExec + Constant.TemplateFacebook.Checkpoint);
                        var pointSavePasswd = _EmulatorFunc.FindOutPoint(_device, _defaultPathExec + Constant.TemplateFacebook.LblSavePassword);

                        if (pointCheckpoint != null)
                        {
                            waiter = new WaitingData<FbRegStatus>(FbRegStatus.CHECKPOINT);
                        }

                        if (pointSavePasswd != null)
                        {
                            if (!hasMatchSavePasswd) // Gặp lần đầu tiên
                            {
                                // Set là đã gặp rồi
                                hasMatchSavePasswd = true;
                                Thread.Sleep(500);
                                // Check lại lần thứ 2
                                continue;
                            }
                            waiter = new WaitingData<FbRegStatus>(FbRegStatus.CREATED);
                        }
                        break;
                    }
                    return waiter;
                });

                if (createStatus == null)
                {
                    LogStepTrace("Waiting timeout");
                    result.Status = FbRegStatus.FAIL;
                    result.Message = "Bước khởi tạo Tài khoản lỗi (waiting sign up time out)";
                    this._log.Error(result.Message);
                    return result;
                }

                if (createStatus.Data == FbRegStatus.CHECKPOINT)
                {
                    LogStepTrace("Account has been disabled");
                    result.Status = FbRegStatus.CHECKPOINT;
                    result.Message = "Tài khoản Checkpoint";
                    this._log.Error(result.Message);
                    return result;
                }

                LogStepTrace("Account created");
                result.Status = FbRegStatus.CREATED;
                result.Message = "Đã tạo được tài khoản";

                // Tap save passwd
                LogStepTrace("Tap link label \"SAVE PASSWORD\"");
                _EmulatorFunc.TapImage(_device, _defaultPathExec + Constant.TemplateFacebook.LblSavePassword);
                Thread.Sleep(120);
                // Tap remember OK
                LogStepTrace("Tap button \"OK\"");
                _EmulatorFunc.TapImage(_device, _defaultPathExec + Constant.TemplateFacebook.BtnRememberEmailOk);
                Thread.Sleep(120);

                // Verify Email
                LogStepTrace("Verifying by email address");
                var veri = VerifyMailFacebookKatana();

                if (veri)
                {
                    LogStepTrace("Account has verified with email address");
                    result.Status = FbRegStatus.SUCCESS_WITH_VERI;
                    result.Message = "Đã tạo được tài khoản veri";
                }

                Thread.Sleep(5000);

                _EmulatorFunc.TapImage(_device, _defaultPathExec + Constant.TemplateFacebook.LblSuccessSkip);

                Thread.Sleep(5000);

                return result;
            }
            catch (Exception ex)
            {
                _log.Error(ex);
                result.Status = FbRegStatus.FAIL;
                result.Message = "Tạo tài khoản lỗi (" + ex.Message + ")";
            }
            finally
            {
                if (!string.IsNullOrEmpty(FbAcc.Email))
                {
                    FunctionHelper.AppendUsedEmail(FbAcc.Email);
                }
                //_EmulatorFunc.ClearAppData(_device, _facebookPackageName);
                //_EmulatorFunc.ClearAppData(_device, _oneDotOnePackageName);
                //_EmulatorFunc.StopDevice(_device);
            }

            return result;
        }
        private bool VerifyMailFacebookKatana()
        {
            try
            {
                // get code mail
                LogStepTrace(".. Waiting for confirm code ..");
                //string codeMail = new WebDriverWait(_chromeDriver, TimeSpan.FromSeconds(30)).Until(x =>
                //{
                //    string resultCodeMail = null;
                //    try
                //    {
                //        var webElement = x.FindElement(By.XPath(_xpathCodeEmail));
                //        if (webElement != null && !string.IsNullOrEmpty(webElement.Text))
                //        {
                //            var code = Regex.Match(webElement.Text, @"\d+").Value;
                //            if (!string.IsNullOrEmpty(code)) resultCodeMail = code;
                //        }
                //    }
                //    catch (Exception ex)
                //    {
                //        _log.Error(ex.Message);
                //    }

                //    return resultCodeMail;
                //});
                _emailHelper.GetConfirmationCode();
                string codeMail = _emailHelper.ConfirmationCode;

                if (string.IsNullOrEmpty(codeMail)) return false;

                // Input confirm code
                LogStepTrace("Tap input \"Confirmation code\"");
                _EmulatorFunc.TapImage(_device, _defaultPathExec + Constant.TemplateFacebook.InputConfirmCode);
                Thread.Sleep(120);
                LogStepTrace("Input Confirmtion code <" + codeMail + ">");
                _EmulatorFunc.Input(_device, codeMail);
                Thread.Sleep(120);

                // Tap Confirm
                LogStepTrace("Tap button \"Confirm\"");
                _EmulatorFunc.TapImage(_device, _defaultPathExec + Constant.TemplateFacebook.BtnConfirm);
                Thread.Sleep(120);

                return true;
            }
            catch (Exception ex)
            {
                _log.Error(ex.Message);
                return false;
            }
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
                _EmulatorFunc.TapImage(_device, _defaultPathExec + Constant.inputHo, 30, new ImagePoint(0, 30));
                _EmulatorFunc.Input(_device, FbAcc.FirstName);
                _EmulatorFunc.TapImage(_device, _defaultPathExec + Constant.inputTen);
                _EmulatorFunc.Input(_device, FbAcc.LastName);

                // Chuyển sang bước tiếp theo (Đăng ký bằng Mobile hoặc Email)
                _EmulatorFunc.TapImage(_device, _defaultPathExec + Constant.btnTiep);
                // Chuyển sang màn hình đăng ký với Email
                _EmulatorFunc.TapImage(_device, _defaultPathExec + Constant.linkRegMail);


                // Lấy địa chỉ Email ở trang temp-mail.org
                // Sử dụng Chrome Driver
                var emailPoint = _EmulatorFunc.FindOutPoint(_device, _defaultPathExec + Constant.labelMail);
                _EmulatorFunc.Tap(_device, new Point(emailPoint.X, emailPoint.Y + 30));
                //var inputMail = _EmulatorFunc.TapImage(_device, _defaultPathExec + Constant.labelMail, new ImagePoint(0, 30));
                
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
                _EmulatorFunc.TapImage(_device, _defaultPathExec + Constant.btnTiep);
                Thread.Sleep(_timeout);

                // Nhập ngày tháng năm sinh
                _InputNumber(_device, _defaultPathExec + Constant.sourceNumber, FbAcc.BirthDay.ToString("dd"));
                _InputNumber(_device, _defaultPathExec + Constant.sourceNumber, FbAcc.BirthDay.ToString("MM"));
                _InputNumber(_device, _defaultPathExec + Constant.sourceNumber, FbAcc.BirthDay.ToString("yyyy"));
                _EmulatorFunc.TapImage(_device, _defaultPathExec + Constant.btnTiep);
                Thread.Sleep(_timeout);

                // Chọn giới tính
                _EmulatorFunc.TapImage(_device, _defaultPathExec + ((int)FbAcc.Gender == 0 ? Constant.male : Constant.female));
                Thread.Sleep(_timeout);

                // Input Passwd
                _EmulatorFunc.TapImage(_device, _defaultPathExec + Constant.labelMatkhau);
                _EmulatorFunc.Input(_device, FbAcc.Passwd);

                // Tap vào Đăng ký
                _EmulatorFunc.TapImage(_device, _defaultPathExec + Constant.btnDangKy);
                Thread.Sleep(_timeout);

                // Chờ và kiểm tra Xem đăng ký thành công hay bị Checkpoint
                // wait for success
                // createStatus
                // 1: Success
                // 0: Checkpoint
                // null: time out
                WaitingData<FbRegStatus> createStatus = new WaitHelper(TimeSpan.FromSeconds(60)).Until(() => {
                    WaitingData<FbRegStatus> waiter = null;
                    var isPass = _EmulatorFunc.FindOutPoint(_device, _defaultPathExec + Constant.btnOkReg);
                    var isCheckpoint = _EmulatorFunc.FindOutPoint(_device, _defaultPathExec + Constant.labelCheckpoint);

                    if (isCheckpoint != null) waiter = new WaitingData<FbRegStatus>(FbRegStatus.CHECKPOINT);
                    if (isPass != null) waiter = new WaitingData<FbRegStatus>(FbRegStatus.CREATED);

                    return waiter;
                });

                if (createStatus == null)
                {
                    result.Status = FbRegStatus.FAIL;
                    result.Message = "Bước khởi tạo Tài khoản lỗi (waiting sign up time out)";
                    this._log.Error(result.Message);
                    return result;
                }

                if (createStatus.Data == FbRegStatus.CHECKPOINT)
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
            _EmulatorFunc.TapImage(_device, _defaultPathExec + Constant.getStarted1111);
            _EmulatorFunc.TapImage(_device, _defaultPathExec + Constant.done1111);
            _EmulatorFunc.TapImage(_device, _defaultPathExec + Constant.accept1111);
            
            var isConnectedPoint = new WaitHelper(TimeSpan.FromSeconds(50)).Until(() =>
            {
                var connected = _EmulatorFunc.FindOutPoint(_device, _defaultPathExec + Constant.btnOpened1111);
                if (connected == null)
                {
                    _EmulatorFunc.TapImage(_device, _defaultPathExec + Constant.btnNotOpen1111);
                }
                return connected;
            });

            return isConnectedPoint != null;
        }

        private void OpenHma()
        {
            _EmulatorFunc.StartApp(_device, _hmaPck);
            Thread.Sleep(_timeout);

            var iconOff = _EmulatorFunc.FindOutPoint(_device, _defaultPathExec + Constant.iconOffHma);
            var iconOn = _EmulatorFunc.FindOutPoint(_device, _defaultPathExec + Constant.iconOnHma);
            while ((iconOff != null && iconOn == null) || (iconOff == null && iconOn == null))
            {
                _EmulatorFunc.TapImage(_device, _defaultPathExec + Constant.iconOffHma);
                iconOff = _EmulatorFunc.FindOutPoint(_device, _defaultPathExec + Constant.iconOffHma);
                iconOn = _EmulatorFunc.FindOutPoint(_device, _defaultPathExec + Constant.iconOnHma);
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
                _EmulatorFunc.TapImage(_device, _defaultPathExec + Constant.btnOkReg);

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

                _EmulatorFunc.TapImage(_device, _defaultPathExec + Constant.inputCodeMail);
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
                LogStepTrace("Get uid");
                // Check cookie
                _chromeDriver.Navigate().GoToUrl(_linkMbasic);
                Thread.Sleep(120);
                var cookie = _chromeDriver.Manage().Cookies.GetCookieNamed("c_user");

                if (cookie != null)
                {
                    FbAcc.Uid = cookie.Value;
                    return true;
                }

                // Nếu chưa đăng nhập
                _chromeDriver.Manage().Cookies.DeleteAllCookies();
                _chromeDriver.Navigate().GoToUrl(_linkMbasic);
                Thread.Sleep(120);

                var elementUsername = _chromeDriver.FindElement(By.XPath(_xMbasicLoginUsername));
                elementUsername.SendKeys(FbAcc.Email);
                Thread.Sleep(500);
                var elementPassword = _chromeDriver.FindElement(By.XPath(_xMbasicLoginPassword));
                elementPassword.SendKeys(FbAcc.Passwd);
                Thread.Sleep(500);
                var elementBtnLogin = _chromeDriver.FindElement(By.XPath(_xMbasicBtnLogin));
                elementBtnLogin.Click();
                Thread.Sleep(120);

                if (!string.IsNullOrEmpty(FbAcc.TwoFacAuth))
                {
                    var inputLoginApprovalCode = _chromeDriver.FindElement(By.XPath(_xMbasicInputLoginApprovalCode));
                    inputLoginApprovalCode.SendKeys(FunctionHelper.GetTotp(FbAcc.TwoFacAuth));
                    Thread.Sleep(500);
                    var btnSubmitApprovalLoginCode = _chromeDriver.FindElement(By.XPath(_xMbasicBtnSubmitLoginApprovalCode));
                    btnSubmitApprovalLoginCode.Click();
                    Thread.Sleep(500);
                    var btnContinueLogin = _chromeDriver.FindElement(By.XPath(_xMbasicBtnContinueLogin));
                    btnContinueLogin.Click();
                    Thread.Sleep(500);
                }

                cookie = _chromeDriver.Manage().Cookies.GetCookieNamed("c_user");

                if (cookie == null)
                {
                    LogStepTrace($"Uid <null>");
                    return false;
                }

                FbAcc.Uid = cookie?.Value;
                LogStepTrace($"Uid <" + FbAcc.Uid + ">");
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
                var username = string.IsNullOrEmpty(FbAcc.Uid) ? FbAcc.Email : FbAcc.Uid;
                LogStepTrace("Turning 2FA on");
                if (!string.IsNullOrEmpty(FbAcc.TwoFacAuth)) return true;

                _chromeDriver.Manage().Cookies.DeleteAllCookies();
                _chromeDriver.Navigate().GoToUrl(_linkMbasic);
                Thread.Sleep(_timeout);
                var elementUsername = _chromeDriver.FindElement(By.XPath(_xMbasicLoginUsername));
                elementUsername.SendKeys(username);
                Thread.Sleep(500);
                var elementPassword = _chromeDriver.FindElement(By.XPath(_xMbasicLoginPassword));
                elementPassword.SendKeys(FbAcc.Passwd);
                Thread.Sleep(500);
                var elementBtnLogin = _chromeDriver.FindElement(By.XPath(_xMbasicBtnLogin));
                elementBtnLogin.Click();
                Thread.Sleep(120);

                // Chuyển đến trang Bật xác minh 2 bước
                _chromeDriver.Navigate().GoToUrl(_linkMbasic2faIntro);
                Thread.Sleep(120);

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
                Thread.Sleep(120);

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
                Thread.Sleep(120);

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
                Thread.Sleep(120);

                // Click continue
                var btnSubmitTotp = _chromeDriver.FindElement(By.XPath(_xMbasicBtnSubmitTotp));
                btnSubmitTotp.Click();
                Thread.Sleep(120);

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
                Thread.Sleep(120);

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
                    LogStepTrace("Done turn 2fa on");
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
            var pointBtnOk = _EmulatorFunc.FindOutPoint(_device, _defaultPathExec + Constant.btnOkReg);
            var pointAskBoQua = _EmulatorFunc.FindOutPoint(_device, _defaultPathExec + Constant.askBoQua);
            var pointlabelBoQua = _EmulatorFunc.FindOutPoint(_device, _defaultPathExec + Constant.labelBoQua);
            while (pointBtnOk != null || pointAskBoQua != null || pointlabelBoQua != null)
            {
                if (pointAskBoQua != null)
                {
                    _EmulatorFunc.TapImage(_device, _defaultPathExec + Constant.askBoQua);
                }
                else if (pointBtnOk != null)
                {
                    _EmulatorFunc.TapImage(_device, _defaultPathExec + Constant.btnOkReg);
                }
                else if (pointlabelBoQua != null)
                {
                    _EmulatorFunc.TapImage(_device, _defaultPathExec + Constant.labelBoQua);
                }

                pointBtnOk = _EmulatorFunc.FindOutPoint(_device, _defaultPathExec + Constant.btnOkReg);
                pointAskBoQua = _EmulatorFunc.FindOutPoint(_device, _defaultPathExec + Constant.askBoQua);
                pointlabelBoQua = _EmulatorFunc.FindOutPoint(_device, _defaultPathExec + Constant.labelBoQua);
            }
        }

        private bool SetFbLanguageToVnFbLite()
        {
            var pointVtnRegVn = new WaitHelper(TimeSpan.FromSeconds(30)).Until(() =>
            {
                var pointFound = _EmulatorFunc.FindOutPoint(_device, _defaultPathExec + Constant.btnTaoMoiTaiKhoanVn);
                if (pointFound == null)
                {
                    var pointLabelLanguageVn = _EmulatorFunc.FindOutPoint(_device, _defaultPathExec + Constant.labelLanguageVn);
                    if (pointLabelLanguageVn == null) return null;
                    _EmulatorFunc.Tap(_device, pointLabelLanguageVn.Point);
                    Thread.Sleep(120);
                }
                return pointFound;
            });

            if (pointVtnRegVn == null)
            {
                this._log.Error("Không tìm thấy Button \"Tạo mới tài khoản\"");
                return false;
            }

            _EmulatorFunc.Tap(_device, pointVtnRegVn.Point);
            _EmulatorFunc.TapImage(_device, _defaultPathExec + Constant.btnTiep);
            return true;
        }

        private bool Turn2FaOnEmulator()
        {
            var pointSetting1 = _EmulatorFunc.FindOutPoint(_device, _defaultPathExec + Constant.iconSetting);
            var pointSetting2 = _EmulatorFunc.FindOutPoint(_device, _defaultPathExec + Constant.iconSetting2);
            if (pointSetting1 != null || pointSetting2 != null)
            {
                if (pointSetting1 != null)
                {
                    _EmulatorFunc.TapImage(this._device, _defaultPathExec + Constant.iconSetting);
                }
                else if (pointSetting2 != null)
                {
                    _EmulatorFunc.TapImage(this._device, _defaultPathExec + Constant.iconSetting2);
                }
            }

            _EmulatorFunc.TapImage(this._device, _defaultPathExec + Constant.btnCaiDat);
            _EmulatorFunc.TapImage(this._device, _defaultPathExec + Constant.baoMat);
            _EmulatorFunc.TapImage(this._device, _defaultPathExec + Constant.xacThuc2YeuTo);
            _EmulatorFunc.TapImage(this._device, _defaultPathExec + Constant.fa2UngDung);

            if (_EmulatorFunc.FindOutPoint(_device, _defaultPathExec + Constant.nhapMatKhau) != null)
            {
                _EmulatorFunc.TapImage(_device, _defaultPathExec + Constant.nhapMatKhau);
                _EmulatorFunc.Input(_device, FbAcc.Passwd);
                _EmulatorFunc.TapImage(_device, _defaultPathExec + Constant.btnTiepTuc);
            }
            if (_EmulatorFunc.FindOutPoint(_device, _defaultPathExec + Constant.screenCode2fa) != null)
            {
                _qrCode = _GetCurrentQRCode(_device);
                _EmulatorFunc.TapImage(_device, _defaultPathExec + Constant.btnTiepTuc);
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

        #endregion
    }
}
