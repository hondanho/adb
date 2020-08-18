using System;
using System.Reflection;
using System.Threading.Tasks;
using System.Windows.Forms;
using auto_android.AutoHelper;
using log4net;

namespace auto_android
{
    public partial class Main : Form
    {
        private static readonly ILog log = LogManager.GetLogger(MethodBase.GetCurrentMethod().DeclaringType);
        public Main()
        {
            InitializeComponent();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            Task t = new Task(() =>
            {
                Auto();
            });
            t.Start();
        }

        void Auto()
        {
            var devices = AdbHelper.GetDevices();
            
            foreach (var deviceID in devices)
            {
                var fb = new RegFb(deviceID);
                fb.Turn1111();
                if (fb.RegisterFb() && fb.Turn2FA())
                {
                    var info = fb.GetInfo();
                }
            }
        }
    }
}
