using AutoTool.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AutoTool.AutoMethods
{
    public class FbRegResult
    {
        public FbRegStatus Status { get; set; }
        public string Message { get; set; }

        public FbRegResult()
        {
            this.Status = FbRegStatus.RUNNING;
        }
    }
}
