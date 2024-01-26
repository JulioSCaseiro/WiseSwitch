using System.ComponentModel.DataAnnotations;

namespace WiseSwitch.ViewModels.Entities.FirmwareVersion
{
    public class CreateFirmwareVersionViewModel : IInputViewModel
    {
        [Required(ErrorMessage = "The Version specification is required.")]
        public string Version { get; set; }


        [DataType(DataType.Date)]
        [Display(Name = "Launch Date")]
        public DateTime? LaunchDate { get; set; }
    }
}
