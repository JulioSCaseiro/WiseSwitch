using System.ComponentModel.DataAnnotations;

namespace WiseSwitch.ViewModels.Entities.Brand
{
    public class CreateBrandViewModel : IInputViewModel
    {
        [Required(ErrorMessage = "A Name is required for the Brand.")]
        public string Name { get; set; }


        [Display(Name = "Manufacturer")]
        [Range(1, int.MaxValue, ErrorMessage = "The ID for the Manufacturer is not valid.")]
        public int ManufacturerId { get; set; }
    }
}
