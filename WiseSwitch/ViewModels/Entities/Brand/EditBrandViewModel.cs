namespace WiseSwitch.ViewModels.Entities.Brand
{
    public class EditBrandViewModel : IInputViewModel
    {
        public int Id { get; set; }

        public string Name { get; set; }


        public int ManufacturerId { get; set; }
    }
}
