namespace WiseSwitch.ViewModels.Entities.Manufacturer
{
    public class DisplayManufacturerViewModel
    {
        public int Id { get; set; }

        public string Name { get; set; }


        public IEnumerable<string> BrandsNames { get; set; }
    }
}
