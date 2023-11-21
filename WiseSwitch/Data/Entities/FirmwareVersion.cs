using System.ComponentModel.DataAnnotations;

namespace WiseSwitch.Data.Entities
{
    public class FirmwareVersion : IEntity
    {
        public int Id { get; set; }

        [Required]
        public string Version { get; set; }

        public DateTime LaunchDate { get; set; }
    }
}
