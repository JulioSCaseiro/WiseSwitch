namespace WiseSwitch.Data.Entities
{
    public class FirmwareVersion : IEntity
    {
        public int Id { get; set; }

        public string Version { get; set; }

        public DateTime LaunchDate { get; set; }
    }
}
