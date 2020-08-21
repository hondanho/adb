namespace AutoTool.Models
{
    class GlobalVar
    {
        private static string _commanderRootPath = "";
        public static string CommanderRootPath
        {
            get { return _commanderRootPath; }
            set { _commanderRootPath = value; }
        }
    }
}
