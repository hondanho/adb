using System.Collections.Generic;
using System.Windows.Forms;

namespace AutoTool.AutoCommons
{
    public static class Extensions
    {
        public static string[] ToStringArray(this ListBox lb)
        {
            List<string> lst = new List<string>();
            foreach (var m in lb.Items)
            {
                lst.Add(m.ToString());
            }
            return lst.ToArray();
        }
    }
}
