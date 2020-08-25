using OpenQA.Selenium.Chrome;

namespace AutoTool.AutoHelper.EmailHelper
{
    abstract public class BaseEmailHelper
    {
        public ChromeDriver Driver;

        public BaseEmailHelper()
        {
            Driver = new ChromeDriver();
        }

        public BaseEmailHelper(ChromeDriver driver)
        {
            Driver = driver;
        }

        public abstract string EmailAddress();

        public abstract string GetFacebookConfirmationCode();
    }
}
