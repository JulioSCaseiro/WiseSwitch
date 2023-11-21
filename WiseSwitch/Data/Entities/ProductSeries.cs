using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations;

namespace WiseSwitch.Data.Entities
{
    [Index(nameof(Name), IsUnique = true)]
    public class ProductSeries : IEntity
    {
        public int Id { get; set; }

        [Required]
        public string Name { get; set; }


        public int ProductLineId { get; set; }

        public ProductLine ProductLine { get; set; }
    }
}
