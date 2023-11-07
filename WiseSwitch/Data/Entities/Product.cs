namespace WiseSwitch.Data.Entities
{
    public class Product : IEntity
    {
        public int Id { get; set; }

        public int BrandId { get; set; }

        public int ModelId { get; set; }

        public int SubModelId { get; set; }

        public int TutorialId { get; set; }

        public int ProceduresId { get; set; }
    }
}
