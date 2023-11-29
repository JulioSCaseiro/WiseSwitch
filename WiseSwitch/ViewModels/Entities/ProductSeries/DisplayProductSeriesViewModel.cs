namespace WiseSwitch.ViewModels.Entities.ProductSeries
{
    public class DisplayProductSeriesViewModel
    {
        public int Id { get; set; }

        public string Name { get; set; }

        public string ProductLineName { get; set; }

        public string BrandName { get; set; }

        public IEnumerable<string> SwitchModelsNames { get; set; }
    }
}
