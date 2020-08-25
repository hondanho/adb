using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AutoTool.Models
{
    public class WaitingData
    {
        public object Data { get; set; }

        public WaitingData()
        {

        }

        public WaitingData(object data)
        {
            this.Data = data;
        }
    }

    public class WaitingData<T>
    {
        public T Data { get; set; }

        public WaitingData()
        {

        }

        public WaitingData(T data)
        {
            this.Data = data;
        }
    }
}
