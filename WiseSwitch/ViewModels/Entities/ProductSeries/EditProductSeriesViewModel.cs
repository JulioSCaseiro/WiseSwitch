namespace WiseSwitch.ViewModels.Entities.ProductSeries
{
    public class EditProductSeriesViewModel : IInputViewModel
    {
        public int Id { get; set; }

        public string Name { get; set; }


        public int ProductLineId { get; set; }

        public int BrandId { get; set; }
    }
}
