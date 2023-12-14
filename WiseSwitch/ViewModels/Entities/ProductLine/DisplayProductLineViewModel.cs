namespace WiseSwitch.ViewModels.Entities.ProductLine
{
    public class DisplayProductLineViewModel
    {
        public int Id { get; set; }

        public string Name { get; set; }


        public string BrandName { get; set; }


        public IEnumerable<string> ProductSeriesNames { get; set; }
    }
}
