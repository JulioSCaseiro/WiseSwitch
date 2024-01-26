using System.ComponentModel.DataAnnotations;

namespace WiseSwitch.ViewModels.Entities.Manufacturer
{
    public class EditManufacturerViewModel : IInputViewModel
    {
        public int Id { get; set; }


        [Required(ErrorMessage = "A Name is required for the Manufacturer.")]
        public string Name { get; set; }
    }
}
