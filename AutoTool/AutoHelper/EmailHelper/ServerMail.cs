using ActiveUp.Net.Mail;
using AutoTool.AutoCommons;
using AutoTool.AutoCommons.AutoExceptions;
using AutoTool.Models;
using Newtonsoft.Json;
using RestSharp;
using RestSharp.Authenticators;
using System;
using System.IO;
using Limilabs.Client.IMAP;
using System.Collections.Generic;
using Limilabs.Mail;
using System.Text.RegularExpressions;

namespace AutoTool.AutoHelper.EmailHelper
{
    public class ServerMail : BaseEmailHelper
    {
        private const string IMAP_HOST = "45.76.183.254";
        private static Regex FB_REG_EMAIL = new Regex("facebook", RegexOptions.IgnoreCase);
        private const int IMAP_PORT = 143;
        private static object emailLock = new object();

        private RestClient _client;

        public ServerMail()
        {
            this._client = new RestClient();
        }

        public override string GetEmailAddress()
        {
            var result = string.Empty;

            new WaitHelper(TimeSpan.FromSeconds(30)).Until(() => {
                result = GenerateEmailAddress();
                return !(string.IsNullOrEmpty(result) || FunctionHelper.EmailIsUsed(result));
            });
            
            if(!string.IsNullOrEmpty(result) && result.Split('|').Length > 1)
            {
                this.EmailAddress = result.Split('|')[0];
                this.EmailPasswd = result.Split('|')[1];
            }

            return this.EmailAddress;
        }

        private string GenerateEmailAddress()
        {
            var result = string.Empty;

            lock (emailLock)
            {
                string emailsCounter = File.ReadAllText(GlobalVar.OutputDirectory + Constant.EmailsCounterPath);
                if (int.TryParse(emailsCounter, out var counter))
                {
                    GlobalVar.EmailsCounter = counter;
                }
                else
                {
                    GlobalVar.EmailsCounter = 0;
                }

                if (GlobalVar.EmailsCounter >= GlobalVar.Emails.Length) throw new OutOfEmailException();
                result = GlobalVar.Emails[GlobalVar.EmailsCounter++];
                File.WriteAllText(GlobalVar.OutputDirectory + Constant.EmailsCounterPath, GlobalVar.EmailsCounter.ToString());
            }

            return result;
        }

        private List<IMail> GetUnseenMails(Imap imap)
        {
            var result = new List<IMail>();
            imap.SelectInbox();
            List<long> uids = imap.Search(Limilabs.Client.IMAP.Flag.Unseen);
            foreach (long uid in uids)
            {
                var eml = imap.GetMessageByUID(uid);
                IMail email = new MailBuilder().CreateFromEml(eml);
                result.Add(email);
            }

            return result;
        }

        public override string GetConfirmationCode()
        {

            // https://docs.iredmail.org/allow.insecure.pop3.imap.smtp.connections.html
            string result;
            try
            {
                using (Imap imap = new Imap())
                {
                    imap.Connect(IMAP_HOST, IMAP_PORT);
                    imap.UseBestLogin(this.EmailAddress, this.EmailPasswd);
                    result = new WaitHelper(TimeSpan.FromSeconds(60)).Until(() => {
                        string confirmCode = null;
                        var mails = GetUnseenMails(imap);
                        if (mails.Count <= 0) return null;

                        foreach (var mail in mails)
                        {
                            bool isFromFb = false;
                            foreach (var mb in mail.From)
                            {
                                if (FB_REG_EMAIL.IsMatch(mb.Address))
                                {
                                    isFromFb = true;
                                    break;
                                }
                            }
                            
                            if (isFromFb)
                            {
                                confirmCode = Regex.Match(mail.Subject, @"\d+").Value;
                                if (string.IsNullOrEmpty(confirmCode))
                                    continue;
                                else
                                    break;
                            }
                        }

                        return confirmCode;
                    });
                }
            }
            catch(Exception ex)
            {
                Console.WriteLine(ex);
                return null;
            }

            this.ConfirmationCode = result;
            return result;
        }
    }
}
