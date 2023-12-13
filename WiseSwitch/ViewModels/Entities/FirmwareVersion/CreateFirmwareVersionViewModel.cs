using System.ComponentModel.DataAnnotations;

namespace WiseSwitch.ViewModels.Entities.FirmwareVersion
{
    public class CreateFirmwareVersionViewModel
    {
        public string Version { get; set; }

        [DataType(DataType.Date)]
        public DateTime? LaunchDate { get; set; }
    }
}
