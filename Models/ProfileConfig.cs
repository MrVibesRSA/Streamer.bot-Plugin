namespace MrVibesRSA.StreamerbotPlugin.Models
{
    public class ProfileConfig
    {
        public string Id { get; set; }
        public string Name { get; set; }    
        public string Address { get; set; }
        public string Port { get; set; }
        public string Endpoint { get; set; }
        public string Password { get; set; }
        public bool AutoConnect { get; set; }
    }
}
