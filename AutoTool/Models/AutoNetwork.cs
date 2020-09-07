namespace AutoTool.Models
{
    public class AutoNetwork
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public AutoNetworkType Type { get; set; }

        public AutoNetwork(int id, string name, AutoNetworkType type)
        {
            Id = id;
            Name = name;
            Type = type;
        }
    }
}
