namespace AutoTool.Constants
{
    public class LDPlayerConsts
    {
        public const string COMMANDER_WORKING_DIRECTORY = @"E:\ChangZhi\LDPlayer";

        public const string SCREEN_SHOT = "ldconsole adb --index {0} --command \"shell screencap -p \"{1}\"\" && ldconsole adb --index {0} --command \"pull \"{1}\" \"{2}\"\" && ldconsole adb --index {0} --command \"shell rm \"{1}\"\"";
        public const string TAP = "ldconsole adb --index {0} --command \"shell input tap {1} {2}\"";
        public const string SWIPE = "ldconsole adb --index {0} --command \"shell input swipe {1} {2} {3} {4}\"";
        public const string SWIPE_LONG = "ldconsole adb --index {0} --command \"shell input swipe {1} {2} {3} {4} {5}\"";
        public const string INPUT = "ldconsole adb --index {0} --command \"shell input text \"{1}\"\"";
        public const string KEY_EVENT = "ldconsole adb --index {0} --command \"shell input keyevent {1}\"";
        public const string CLEAR_APP_DATA = "ldconsole adb --index {0} --command \"shell pm clear {1}\"";

        public const string LIST_DEVICES = "ldconsole list2";
        public const string ADD_DEVICE = "ldconsole add";
        public const string ADD_DEVICE_WITH_NAME = "ldconsole add --name \"{0}\"";
        public const string RESTORE_DEVICE = "ldconsole restore --index {0} --file \"{1}\"";
        public const string RESTORE_DEVICE_WITH_NAME = "ldconsole restore --name {0} --file \"{1}\"";
        public const string CLONE_DEVICE = "ldconsole copy --name \"{0}\" --from {1}";
        public const string START_DEVICE = "ldconsole launch --index {0}";
        public const string STOP_DEVICE = "ldconsole quit --index {0}";
        public const string STOP_ALL_DEVICES = "ldconsole quitall";
        public const string REMOVE_DEVICE = "ldconsole remove --index {0}";
        public const string RENAME_DEVICE = "ldconsole rename --index {0} --title \"{1}\"";
        public const string IS_DEVICE_RUNNING = "ldconsole isrunning --index {0}";
        // public const string STATUS_DEVICE = "ldconsole -i {0} adb get-status";
        public const string START_APP = "ldconsole runapp --index {0} --packagename \"{1}\"";
        public const string STOP_APP = "ldconsole killapp --index {0} --packagename \"{1}\"";

        public const string GET_SERIAL_NO = "ldconsole adb --index {0} --command \"get-serialno\"";

        public const string SET_CONFIG = "ldconsole modify --index {0} --{1} {2}";
        public const string GET_PROP = "ldconsole getprop --index {0} --key \"{1}\"";
        public const string SET_PROXY = "ldconsole adb --index {0} --command \"shell settings put global http_proxy {1}\" && ldconsole adb --index {0} --command \"shell settings put global https_proxy {1}\"";
    }
}
