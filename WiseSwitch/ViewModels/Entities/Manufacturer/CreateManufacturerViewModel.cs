using System.ComponentModel.DataAnnotations;

namespace WiseSwitch.ViewModels.Entities.Manufacturer
{
    public class CreateManufacturerViewModel : IInputViewModel
    {
        [Required(ErrorMessage = "A Name is required for the Manufacturer.")]
        public string Name { get; set; }
    }
}
