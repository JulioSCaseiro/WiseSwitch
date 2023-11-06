namespace WiseSwitch.Data.Entities
{
    public class SwitchModel : IEntity
    {
        public int Id { get; set; }


        public string ModelName { get; set; }

        public string ModelYear { get; set; }


        public int ProductSeriesId { get; set; }

        public ProductSeries ProductSeries { get; set; }


        public int DefaultFirmwareVersionId { get; set; }

        public FirmwareVersion DefaultFirmwareVersion { get; set; }
    }
}
