using Microsoft.EntityFrameworkCore;

namespace WiseSwitch.Data.Entities
{
    [Index(nameof(Name), IsUnique = true)]
    public class Manufacturer : IEntity
    {
        public int Id { get; set; }

        public string Name { get; set; }


        public IEnumerable<Brand>? Brands { get; set; }
    }
}
