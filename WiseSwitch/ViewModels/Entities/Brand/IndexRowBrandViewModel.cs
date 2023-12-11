using System.Text.Json.Serialization;

namespace WiseSwitch.ViewModels.Entities.Brand
{
    public class IndexRowBrandViewModel
    {
        [JsonPropertyName("id")]
        public int Id { get; set; }

        [JsonPropertyName("name")]
        public string Name { get; set; }

        [JsonPropertyName("manufacturerName")]
        public string ManufacturerName { get; set; }
    }
}
