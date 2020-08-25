using AutoTool.AutoCommons;
using AutoTool.Models;
using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;
using System;
using System.Linq;
using System.Text.RegularExpressions;
using System.Threading;
using System.Windows.Forms;

namespace AutoTool.AutoHelper.EmailHelper
{
    public class SmailPro : BaseEmailHelper
    {
        private static string SmailproUrl = "https://smailpro.com/";


        private static string EmailTabXpath = "//*[@id='semail-tab']";
        private static string EmailAddressXpath = "//*[@id='semail']/div/div[2]/div[1]/div[1]/div/span";
        private static string ButtonGenerateXpath = "//*[@id='semail']/div/div[2]/div[1]/div[2]/div/div[2]/button";
        private static string ButtonGenerateInPopupXpath = "//*[@id='settingsEmail']/div/div/div[3]/button[2]";
        private static string ButtonCloseInPopupXpath = "//*[@id='settingsEmail']/div/div/div[3]/button[1]";
        private static string ButtonRefreshXpath = "//*[@id='inbox']/div[1]/button";

        private static string EmailTitleXpath = "//*[@id='inbox']/div[2]/table/tbody/tr/td/span";

        public SmailPro(ChromeDriver driver) : base(driver)
        {

        }

        public override string EmailAddress()
        {
            Driver.Navigate().GoToUrl(SmailproUrl);
            new WaitHelper(TimeSpan.FromSeconds(30)).Until(() => {
                try
                {
                    var emailLink = Driver.FindElement(By.XPath(EmailTabXpath));
                    if (emailLink == null) return false;
                    emailLink.Click();
                    return true;
                }
                catch (Exception)
                {
                    return false;
                }
            });

            var emailAddress = new WaitHelper(TimeSpan.FromSeconds(60)).Until(() => {
                var email = GenerateEmailAddress();
                if (string.IsNullOrEmpty(email)
                    || GlobalVar.ListUsedEmail.Contains(email)) return null;

                return email;
            });

            return emailAddress;
        }

        private string GenerateEmailAddress()
        {
            new WaitHelper(TimeSpan.FromSeconds(30)).Until(() => {
                try
                {
                    var btnGenerate = Driver.FindElement(By.XPath(ButtonGenerateXpath));
                    if (btnGenerate == null) return false;
                    btnGenerate.Click();
                    return true;
                }
                catch (Exception)
                {
                    return false;
                }
            });

            new WaitHelper(TimeSpan.FromSeconds(5)).Until(() => {
                try
                {
                    var btnGenerate = Driver.FindElement(By.XPath(ButtonGenerateInPopupXpath));
                    if (btnGenerate == null) return false;
                    btnGenerate.Click();
                    return true;
                }
                catch (Exception)
                {
                    return false;
                }
            });

            Thread.Sleep(500);

            new WaitHelper(TimeSpan.FromSeconds(5)).Until(() => {
                try
                {
                    var btnClose = Driver.FindElement(By.XPath(ButtonCloseInPopupXpath));
                    if (btnClose == null) return false;
                    btnClose.Click();
                    return true;
                }
                catch (Exception)
                {
                    return false;
                }
            });

            Thread.Sleep(500);

            string emailAddress = new WaitHelper(TimeSpan.FromSeconds(30)).Until(() => {
                try
                {
                    var emailAddressLabel = Driver.FindElement(By.XPath(EmailAddressXpath));
                    if (emailAddressLabel == null) return null;
                    if (emailAddressLabel.Text.Contains("@"))
                        return emailAddressLabel.Text;
                    return null;
                }
                catch (Exception)
                {
                    return null;
                }
            });

            return emailAddress;
        }

        public override string GetFacebookConfirmationCode()
        {
            string code = new WaitHelper(TimeSpan.FromSeconds(60)).Until(() => {
                // Click Refresh
                new WaitHelper(TimeSpan.FromSeconds(10)).Until(() => {
                    try
                    {
                        var btnRefresh = Driver.FindElement(By.XPath(ButtonRefreshXpath));
                        if (btnRefresh == null) return false;
                        btnRefresh.Click();
                        return true;
                    }
                    catch (Exception)
                    {
                        return false;
                    }
                });

                Thread.Sleep(120);

                new WaitHelper(TimeSpan.FromSeconds(10)).Until(() => {
                    try
                    {
                        var btnRefresh = Driver.FindElement(By.XPath(ButtonRefreshXpath));
                        if (btnRefresh == null) return false;
                        if (btnRefresh.Text.Equals("Refresh"))
                            return true;
                        return false;
                    }
                    catch (Exception)
                    {
                        return false;
                    }
                });

                try
                {
                    var lblEmailTitle = Driver.FindElement(By.XPath(EmailTitleXpath));
                    if (lblEmailTitle == null) return null;
                    if (lblEmailTitle.Text != null)
                    {
                        var confirmCode = Regex.Match(lblEmailTitle.Text, @"\d+").Value;
                        return confirmCode;
                    }
                    return null;
                }
                catch (Exception)
                {
                    return null;
                }
            });

            return code;
        }
    }
}
