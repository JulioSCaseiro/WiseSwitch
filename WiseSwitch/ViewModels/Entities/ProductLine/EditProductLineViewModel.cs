using System.ComponentModel.DataAnnotations;

namespace WiseSwitch.ViewModels.Entities.ProductLine
{
    public class EditProductLineViewModel : IInputViewModel
    {
        public int Id { get; set; }


        [Required(ErrorMessage = "A Name is required for the Product Line.")]
        public string Name { get; set; }


        [Display(Name = "Brand")]
        [Range(1, int.MaxValue, ErrorMessage = "The ID for the Brand is not valid.")]
        public int BrandId { get; set; }
    }
}
