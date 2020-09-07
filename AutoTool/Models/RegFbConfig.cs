using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AutoTool.Models
{
    public class RegFbConfig
    {
        public bool HideChrome { get; set; }
        public bool MinimizeChrome { get; set; }
        public bool Turn2FaOn { get; set; }
        public AutoNetworkType RegFbNetworkType { get; set; }
        public bool UseEmailServer { get; set; }
        public RegFbType RegType { get; set; }
    }
}
