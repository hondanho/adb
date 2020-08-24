namespace AutoTool.Constants
{
    public class MEmuConsts
    {
        public static string COMMANDER_WORKING_DIRECTORY = @"D:\Program Files\Microvirt\MEmu";

        public static string SCREEN_SHOT = "memuc -i {0} adb shell screencap -p \"{1}\" && memuc -i {0} adb pull \"{1}\" \"{2}\" && memuc -i {0} adb shell rm \"{1}\"";
        public static string TAP = "memuc -i {0} adb shell input tap {1} {2}";
        public static string SWIPE = "memuc -i {0} adb shell input swipe {1} {2} {3} {4}";
        public static string SWIPE_LONG = "memuc -i {0} adb shell input swipe {1} {2} {3} {4} {5}";
        public static string INPUT = "memuc -i {0} input \"{1}\"";
        public static string KEY_EVENT = "memuc -i {0} adb shell input keyevent {1}";
        public static string CLEAR_APP = "memuc -i {0} adb shell pm clear {1}";

        public static string LIST_DEVICES = "memuc listvms";
        public static string RESTORE_DEVICE = "memuc import \"{0}\"";
        public static string CLONE_DEVICE = "memuc clone -i {0}";
        public static string START_DEVICE = "memuc -i {0} start";
        public static string STOP_DEVICE = "memuc stop -i {0}";
        public static string STOP_ALL_DEVICES = "memuc stopall";
        public static string REMOVE_DEVICE = "memuc remove -i {0}";
        public static string RENAME_DEVICE = "memuc rename -i {0} {1}";
        public static string IS_DEVICE_RUNNING = "memuc isvmrunning -i {0}";
        public static string STATUS_DEVICE = "memuc -i {0} adb get-status";
        public static string START_APP = "memuc -i {0} startapp {1}";
        public static string STOP_APP = "memuc -i {0} stopapp {1}";
    }
}
