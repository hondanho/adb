namespace AutoTool.Constants
{
    public class LDPlayerConsts
    {
        public static string COMMANDER_WORKING_DIRECTORY = @"E:\ChangZhi\LDPlayer";

        public static string SCREEN_SHOT = "ldconsole adb --index {0} --command \"shell screencap -p \"{1}\"\" && ldconsole adb --index {0} --command \"pull \"{1}\" \"{2}\"\" && ldconsole adb --index {0} --command \"shell rm \"{1}\"\"";
        public static string TAP = "ldconsole adb --index {0} --command \"shell input tap {1} {2}\"";
        public static string SWIPE = "ldconsole adb --index {0} --command \"shell input swipe {1} {2} {3} {4}\"";
        public static string SWIPE_LONG = "ldconsole adb --index {0} --command \"shell input swipe {1} {2} {3} {4} {5}\"";
        public static string INPUT = "ldconsole adb --index {0} --command \"shell input text \"{1}\"\"";
        public static string KEY_EVENT = "ldconsole adb --index {0} --command \"shell input keyevent {1}\"";
        public static string CLEAR_APP = "ldconsole adb --index {0} --command \"shell pm clear {1}\"";

        public static string LIST_DEVICES = "ldconsole list2";
        public static string ADD_DEVICE = "ldconsole add --name \"{0}\"";
        public static string RESTORE_DEVICE = "ldconsole restore --index {0} --file \"{1}\"";
        public static string CLONE_DEVICE = "ldconsole copy --name \"{0}\" --from {1}";
        public static string START_DEVICE = "ldconsole launch --index {0}";
        public static string STOP_DEVICE = "ldconsole quit --index {0}";
        public static string STOP_ALL_DEVICES = "ldconsole quitall";
        public static string REMOVE_DEVICE = "ldconsole remove --index {0}";
        public static string RENAME_DEVICE = "ldconsole rename --index {0} --title \"{1}\"";
        public static string IS_DEVICE_RUNNING = "ldconsole isrunning --index {0}";
        // public static string STATUS_DEVICE = "ldconsole -i {0} adb get-status";
        public static string START_APP = "ldconsole runapp --index {0} --packagename \"{1}\"";
        public static string STOP_APP = "ldconsole killapp --index {0} --packagename \"{1}\"";
    }
}
