namespace AutoTool.Models
{
    public class EmulatorInfo
    {
        public string Id { get; set; }
        public string Name { get; set; }
        public int Index { get; set; }
        public DeviceStatus Status { get; set; }

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
    }
}
