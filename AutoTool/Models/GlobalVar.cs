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
        public static List<string> ListUsedEmail { get; set; }
        public static string OutputDirectory { get; set; }
    }
}
