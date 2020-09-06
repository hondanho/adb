using OpenQA.Selenium.Chrome;

namespace AutoTool.AutoHelper.EmailHelper
{
    abstract public class BaseEmailHelper
    {
        public ChromeDriver Driver;
        public string EmailAddress;
        public string EmailPasswd;
        public string ConfirmationCode;

        public BaseEmailHelper()
        {
            //Driver = new ChromeDriver();
        }

        public BaseEmailHelper(ChromeDriver driver)
        {
            Driver = driver;
        }

        public abstract string GetEmailAddress();

        public abstract string GetConfirmationCode();
    }
}
