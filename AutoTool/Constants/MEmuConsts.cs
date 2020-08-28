namespace AutoTool.Constants
{
    public class MEmuConsts
    {
        public const string COMMANDER_WORKING_DIRECTORY = @"D:\Program Files\Microvirt\MEmu";

        public const string SCREEN_SHOT = "memuc -i {0} adb shell screencap -p \"{1}\" && memuc -i {0} adb pull \"{1}\" \"{2}\" && memuc -i {0} adb shell rm \"{1}\"";
        public const string TAP = "memuc -i {0} adb shell input tap {1} {2}";
        public const string SWIPE = "memuc -i {0} adb shell input swipe {1} {2} {3} {4}";
        public const string SWIPE_LONG = "memuc -i {0} adb shell input swipe {1} {2} {3} {4} {5}";
        public const string INPUT = "memuc -i {0} input \"{1}\"";
        public const string KEY_EVENT = "memuc -i {0} adb shell input keyevent {1}";
        public const string CLEAR_APP_DATA = "memuc -i {0} adb shell pm clear {1}";

        public const string LIST_DEVICES = "memuc listvms";
        public const string RESTORE_DEVICE = "memuc import \"{0}\"";
        public const string CLONE_DEVICE = "memuc clone -i {0}";
        public const string START_DEVICE = "memuc -i {0} start";
        public const string STOP_DEVICE = "memuc stop -i {0}";
        public const string STOP_ALL_DEVICES = "memuc stopall";
        public const string REMOVE_DEVICE = "memuc remove -i {0}";
        public const string RENAME_DEVICE = "memuc rename -i {0} {1}";
        public const string IS_DEVICE_RUNNING = "memuc isvmrunning -i {0}";
        public const string STATUS_DEVICE = "memuc -i {0} adb wait-for-device";
        public const string START_APP = "memuc -i {0} startapp {1}";
        public const string STOP_APP = "memuc -i {0} stopapp {1}";

        public const string GET_SERIAL_NO = "memuc -i {0} abd get-serialno";


        public const string SET_PROXY = "memuc -i {0} adb shell settings put global http_proxy {1} && memuc -i {0} adb shell settings put global https_proxy {1}";
    }
}
