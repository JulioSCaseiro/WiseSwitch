namespace WiseSwitch.ViewModels.Entities.Brand
{
    public class DisplayBrandViewModel
    {
        public int Id { get; set; }

        public string Name { get; set; }

        public string ManufacturerName { get; set; }

        public IEnumerable<string> ProductLinesNames { get; set; }
    }
}
