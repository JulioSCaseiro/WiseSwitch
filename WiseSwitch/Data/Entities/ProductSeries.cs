namespace WiseSwitch.Data.Entities
{
    public class ProductSeries : IEntity
    {
        public int Id { get; set; }

        public string Name { get; set; }


        public int ProductLineId { get; set; }

        public ProductLine ProductLine { get; set; }
    }
}
