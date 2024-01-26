using System.ComponentModel.DataAnnotations;
using WiseSwitch.Utils;

namespace WiseSwitch.ViewModels.Entities.SwitchModel
{
    public class EditSwitchModelViewModel : IInputViewModel
    {
        public int Id { get; set; }

        [Display(Name = "Model Name")]
        [Required(ErrorMessage = "A Model Name is required for the Switch.")]
        public string ModelName { get; set; }


        [CustomValidationSwitchModelYear]
        [Display(Name = "Model Year")]
        [Required(ErrorMessage = "A Model Year is required for the Switch.")]
        public string ModelYear { get; set; }


        [Display(Name = "Default Firmware Version")]
        [Range(1, int.MaxValue, ErrorMessage = "The ID for the Firmware Version is not valid.")]
        public int DefaultFirmwareVersionId { get; set; }


        [Display(Name = "Product Series")]
        [Range(1, int.MaxValue, ErrorMessage = "The ID for the Product Series is not valid.")]
        public int ProductSeriesId { get; set; }


        [Display(Name = "Product Line")]
        [Range(1, int.MaxValue, ErrorMessage = "The ID for the Product Line is not valid.")]
        public int ProductLineId { get; set; }


        [Display(Name = "Brand")]
        [Range(1, int.MaxValue, ErrorMessage = "The ID for the Brand is not valid.")]
        public int BrandId { get; set; }
    }
}
