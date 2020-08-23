using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AutoTool.Constants
{
    public class MEmuConsts
    {
        public static string MEMU_FOLDER_PATH = @"D:\Program Files\Microvirt\MEmu";

        public static string SCREEN_SHOT = "memuc -i {0} adb shell screencap -p \"{1}\" && memuc -i {0} adb pull \"{1}\" \"{2}\" && memuc -i {0} adb shell rm \"{1}\"";
        public static string TAP = "memuc -i {0} adb shell input tap {1} {2}";
        public static string SWIPE = "memuc -i {0} adb shell input swipe {1} {2} {3} {4}";
        public static string SWIPE_LONG = "memuc -i {0} adb shell input swipe {1} {2} {3} {4} {5}";
        public static string INPUT = "memuc -i {0} input \"{1}\"";
        public static string KEY = "memuc -i {0} adb shell input keyevent {1}";
        public static string CLEAR = "memuc -i {0} adb shell pm clear {1}";

        public static string LIST_DEVICES = "memuc listvms";
        public static string CLONE_MEMU_BY_NAME = "memuc clone -i {0}";
        public static string START_MEMU = "memuc -i {0} start";
        public static string STATUS_MEMU = "memuc -i {0} adb wait-for-device";
        public static string MEMU_STARTAPP_NAME = "memuc -i {0} startapp {1}";
        public static string MEMU_STOPAPP_NAME = "memuc -i {0} stopapp {1}";
        public static string RESTORE_MEMU = "memuc import \"{0}\"";
        public static string STOP_ALL_DEVICES = "memuc stopall";
        public static string STOP_DEVICE = "memuc stop -i {0}";
        public static string REMOVE_DEVICE = "memuc remove -i {0}";
        public static string RENAME_DEVICE_BY_ID = "memuc rename -i {0} {1}";
        public static string ISVMRUNNING_DEVICE = "memuc isvmrunning -i {0}";
    }
}
