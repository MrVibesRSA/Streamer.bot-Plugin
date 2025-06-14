namespace MrVibesRSA.StreamerbotPlugin.Utilities
{
    public class ComboBoxItemHelper
    {
        public string Name { get; set; }
        public object Id { get; set; }
        public bool IsConnected { get; set; }

        public ComboBoxItemHelper(string name, string id, bool isConnected = false)
        {
            Name = name;
            Id = id;
            IsConnected = isConnected;
        }

        public override string ToString()
        {
            return Name; // Important for display
        }
    }
}
