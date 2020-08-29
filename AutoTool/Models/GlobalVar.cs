using System.Collections.Generic;
using System.Windows.Forms;

namespace AutoTool.Models
{
    class GlobalVar
    {
        public static string LDPlayerWorkingDirectory { get; set; }
        public static string MEmuWorkingDirectory { get; set; }
        public static string[] ListLastName { get; set; }
        public static string[] ListFirstName { get; set; }
        public static string[] Proxies { get; set; }
        public static int ProxiesCounter { get; set; }
        public static bool UseProxy { get; set; }
        public static string OutputDirectory { get; set; }
    }
}
