using System;
using System.Collections.Generic;
using System.Drawing;
using System.Reflection;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;
using auto_android.AutoHelper;
using log4net;
using OpenQA.Selenium;
using OpenQA.Selenium.Support.UI;

namespace auto_android
{
    public partial class Main : Form
    {
        private static readonly ILog log = LogManager.GetLogger(MethodBase.GetCurrentMethod().DeclaringType);
        public bool isStop = false;
        public string email = "";
        public Main()
        {
            InitializeComponent();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            Task t = new Task(() =>
            {
                isStop = false;
                Auto();
            });
            t.Start();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            if (FunctionHelper.startDevices())
            {
                this.label1.Text = "ready";
            }
        }

        void Auto()
        {
            List<string> devices = new List<string>();
            devices = AdbHelper.GetDevices();

            foreach (var deviceID in devices)
            {
                var isSuccess1 = FunctionHelper.WaitTimeout(() =>
                {
                    return false;
                }
                , TimeSpan.FromSeconds(10));
                MessageBox.Show("ok");
                return;
                // main
                AdbHelper.SendKey(deviceID, (int)AdbKeyEvent.KEYCODE_HOME);
                Thread.Sleep(500);

                // turn 1111
                AdbHelper.TapImg(deviceID, Environment.CurrentDirectory + Constant.icon1111);
                Thread.Sleep(1000);
                while(FunctionHelper.CheckImgExist(deviceID, Environment.CurrentDirectory + Constant.iconTurned1111) == null)
                {
                    AdbHelper.TapImg(deviceID, Environment.CurrentDirectory + Constant.iconTurnOn1111);
                    Thread.Sleep(1000);
                }

                AdbHelper.SendKey(deviceID, (int)AdbKeyEvent.KEYCODE_HOME);
                Thread.Sleep(500);

                //go to reg fb
                AdbHelper.TapImg(deviceID, Environment.CurrentDirectory + Constant.iconFbLite);
                Thread.Sleep(1000);
                AdbHelper.TapImg(deviceID, Environment.CurrentDirectory + Constant.btnTaoMoiTaiKhoan);
                Thread.Sleep(1000);
                AdbHelper.TapImg(deviceID, Environment.CurrentDirectory + Constant.btnTiep);
                Thread.Sleep(1000);

                AdbHelper.TapImg(deviceID, Environment.CurrentDirectory + Constant.inputHo, new Point(0, 30));
                AdbHelper.Input(deviceID, FunctionHelper.getHoRandom());
                AdbHelper.TapImg(deviceID, Environment.CurrentDirectory + Constant.inputTen);
                AdbHelper.Input(deviceID, FunctionHelper.getTenRandom());
                AdbHelper.TapImg(deviceID, Environment.CurrentDirectory + Constant.btnTiep);
                Thread.Sleep(1000);
                AdbHelper.TapImg(deviceID, Environment.CurrentDirectory + Constant.linkRegMail);
                Thread.Sleep(1000);

                var inputMaill = AdbHelper.TapImg(deviceID, Environment.CurrentDirectory + Constant.labelMail, new Point(0, 30));
                AdbHelper.SwipeLong(deviceID, new Point(20, inputMaill.Value.Y), new Point(500, inputMaill.Value.Y), 1500);
                AdbHelper.SendKey(deviceID, (int)AdbKeyEvent.KEYCODE_DEL);
                var chromDriver = FunctionHelper.InitWebDriver();
                chromDriver.Navigate().GoToUrl("https://temp-mail.org/vi");
                Thread.Sleep(1000);
                var webElement = chromDriver.FindElement(By.XPath("//*[@id=\"mail\"]"));
                var email = string.Empty;
                new WebDriverWait(chromDriver, TimeSpan.FromSeconds(30)).Until(x => {
                    webElement = chromDriver.FindElement(By.XPath("//*[@id=\"mail\"]"));
                    email = webElement.GetAttribute("value");
                    return !string.IsNullOrEmpty(email) && email.Contains("@");
                });
                AdbHelper.Input(deviceID, email.ToCharArray());
                Thread.Sleep(500);
                AdbHelper.TapImg(deviceID, Environment.CurrentDirectory + Constant.btnTiep);
                Thread.Sleep(1000);
                AdbHelper.InputNumber(deviceID, Environment.CurrentDirectory + Constant.sourceNumber, FunctionHelper.GetRandomDay());
                Thread.Sleep(50);
                AdbHelper.InputNumber(deviceID, Environment.CurrentDirectory + Constant.sourceNumber, FunctionHelper.GetRandomMonth());
                Thread.Sleep(50);
                AdbHelper.InputNumber(deviceID, Environment.CurrentDirectory + Constant.sourceNumber, FunctionHelper.GetRandomYear());
                Thread.Sleep(50);
                AdbHelper.TapImg(deviceID, Environment.CurrentDirectory + Constant.btnTiep);
                Thread.Sleep(1000);
                AdbHelper.TapImg(deviceID, Environment.CurrentDirectory + FunctionHelper.getMaleRandom());
                Thread.Sleep(1000);
                AdbHelper.TapImg(deviceID, Environment.CurrentDirectory + Constant.labelMatkhau);
                var matkhau = FunctionHelper.GetRandomMatkhau();
                log.Info("taikhoan:" + email + "|" + matkhau);
                AdbHelper.Input(deviceID, matkhau.ToCharArray());
                AdbHelper.TapImg(deviceID, Environment.CurrentDirectory + Constant.btnDangKy);
                Thread.Sleep(1000);

                // wait for success
                var isSuccess = new WaitHelper<string>(deviceID, TimeSpan.FromSeconds(30)).Until(x => {
                    var result = false;
                    while(FunctionHelper.CheckImgExist(x, Environment.CurrentDirectory + Constant.labelMail) == null)
                    {

                    }
                    return result;
                });

                if(isSuccess)
                {
                    AdbHelper.TapImg(deviceID, Environment.CurrentDirectory + Constant.btnOk);
                    Thread.Sleep(1000);
                    new WebDriverWait(chromDriver, TimeSpan.FromSeconds(5)).Until(x => {
                        while(true)
                        {
                            try
                            {
                                webElement = chromDriver.FindElement(By.XPath("/html/body/main/div[1]/div/div[3]/div[2]/div/div[1]/div/div[4]/ul/li[2]"));
                                email = webElement.Text;
                                if (webElement != null && !string.IsNullOrEmpty(webElement.Text))
                                {
                                    var codeMail = System.Text.RegularExpressions.Regex.Match(webElement.Text, @"\d+").Value;
                                    AdbHelper.TapImg(deviceID, Environment.CurrentDirectory + Constant.inputCodeMail);
                                    AdbHelper.Input(deviceID, codeMail);
                                    Thread.Sleep(500);
                                    AdbHelper.TapImg(deviceID, Environment.CurrentDirectory + Constant.btnOk);
                                    Thread.Sleep(1000);
                                    AdbHelper.TapImg(deviceID, Environment.CurrentDirectory + Constant.btnOk);
                                    Thread.Sleep(1000);
                                    AdbHelper.TapImg(deviceID, Environment.CurrentDirectory + Constant.labelBoQua);
                                    Thread.Sleep(1000);
                                    AdbHelper.TapImg(deviceID, Environment.CurrentDirectory + Constant.labelBoQua);
                                    Thread.Sleep(1000);

                                    break;
                                }
                            }
                            catch (Exception ex) { 
                                log.Error(ex.Message); 
                            }
                        }
                        return true;
                    });
                    MessageBox.Show("ok");
                }

            }
        }

        private void Main_Load(object sender, EventArgs e)
        {

        }

        private void button3_Click(object sender, EventArgs e)
        {
            isStop = true;
        }

        private void textBox1_TextChanged(object sender, EventArgs e)
        {
            var textbox = (TextBox)sender;
            email = textbox.Text;
        }
    }
}
