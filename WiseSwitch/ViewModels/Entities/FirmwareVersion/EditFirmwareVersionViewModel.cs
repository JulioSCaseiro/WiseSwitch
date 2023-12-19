using System.ComponentModel.DataAnnotations;

namespace WiseSwitch.ViewModels.Entities.FirmwareVersion
{
    public class EditFirmwareVersionViewModel : IInputViewModel
    {
        public int Id { get; set; }

        public string Version { get; set; }

        [DataType(DataType.Date)]
        public DateTime? LaunchDate { get; set; }
    }
}
