using System.ComponentModel.DataAnnotations;
using Microsoft.EntityFrameworkCore;

namespace WiseSwitch.Data.Entities
{
    [Index(nameof(Version), IsUnique = true)]
    public class FirmwareVersion : IEntity
    {
        public int Id { get; set; }

        [Required]
        public string Version { get; set; }

        [DataType(DataType.Date)]
        public DateTime? LaunchDate { get; set; }


        public IEnumerable<SwitchModel>? SwitchModels { get; set; }
    }
}
