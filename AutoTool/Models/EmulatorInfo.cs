namespace AutoTool.Models
{
    public class EmulatorInfo
    {
        public string Id { get; set; }
        public string Name { get; set; }
        public int Index { get; set; }
        public EmulatorConfig EmulatorConfig { get; set; }

        public EmulatorInfo(string id, string name)
        {
            this.Id = id;
            this.Name = name;
        }
    }
}
