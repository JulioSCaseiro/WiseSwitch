namespace WiseSwitch.ViewModels.Entities.ProductLine
{
    public class EditProductLineViewModel : IInputViewModel
    {
        public int Id { get; set; }

        public string Name { get; set; }

        
        public int BrandId { get; set; }
    }
}
