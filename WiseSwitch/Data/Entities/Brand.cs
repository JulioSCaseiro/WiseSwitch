namespace WiseSwitch.Data.Entities
{
    public class Brand : IEntity
    {
        public int Id { get; set; }

        public string Name { get; set; }


        public int ManufacturerId { get; set; }

        public Manufacturer Manufacturer { get; set; }
    }
}
