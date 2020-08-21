namespace AutoTool.Models
{
    public class FacebookAccountInfo
    {
        public string Uid { get; set; }
        public string Passwd { get; set; }
        public string TwoFacAuth { get; set; }
        public string Email { get; set; }
        public string Username { get; set; }
        public string Surname { get; set; }
        public string Givenname { get; set; }
        public string Cookie { get; set; }
        public string Token { get; set; }

        public string StringInfo()
        {
            // uid|passwd|2fa|email|username
            return string.Format("{0}", 
                string.Join("|", Uid, Passwd, TwoFacAuth, Email, Username));
        }
    }
}
