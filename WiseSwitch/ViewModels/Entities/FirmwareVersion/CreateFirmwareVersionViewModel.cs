using System.ComponentModel.DataAnnotations;

namespace WiseSwitch.ViewModels.Entities.FirmwareVersion
{
    public class CreateFirmwareVersionViewModel : IInputViewModel
    {
        public string Version { get; set; }

        [DataType(DataType.Date)]
        public DateTime? LaunchDate { get; set; }
    }
}
