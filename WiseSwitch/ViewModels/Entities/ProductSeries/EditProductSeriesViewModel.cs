using System.ComponentModel.DataAnnotations;

namespace WiseSwitch.ViewModels.Entities.ProductSeries
{
    public class EditProductSeriesViewModel : IInputViewModel
    {
        public int Id { get; set; }


        [Required(ErrorMessage = "A Name is required for the Product Series.")]
        public string Name { get; set; }


        [Display(Name = "Product Line")]
        [Range(1, int.MaxValue, ErrorMessage = "The ID for the Product Line is not valid.")]
        public int ProductLineId { get; set; }


        [Display(Name = "Brand")]
        [Range(1, int.MaxValue, ErrorMessage = "The ID for the Brand is not valid.")]
        public int BrandId { get; set; }
    }
}
