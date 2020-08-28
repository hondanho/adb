namespace AutoTool.Models
{
    public class EmulatorInfo
    {
        public bool Choose { get; set; }
        public string Id { get; set; }
        public string Name { get; set; }
        public int Index { get; set; }
        public DeviceStatus Status { get; set; }
        public EmulatorConfig Config { get; set; }
        public string Proxy { get; set; }

        public EmulatorInfo(string id, string name)
        {
            this.Id = id;
            this.Name = name;
            this.Status = DeviceStatus.UNKNOWN;
        }

        public EmulatorInfo(string id, string name, DeviceStatus status)
        {
            this.Id = id;
            this.Name = name;
            this.Status = status;
        }

        public EmulatorInfo(string id, string name, DeviceStatus status, EmulatorConfig config)
        {
            this.Id = id;
            this.Name = name;
            this.Status = status;
            this.Config = config;
        }
    }
}
