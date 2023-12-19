namespace WiseSwitch.ViewModels.Entities.ProductSeries
{
    public class CreateProductSeriesViewModel : IInputViewModel
    {
        public string Name { get; set; }


        public int ProductLineId { get; set; }

        public int BrandId { get; set; }
    }
}
