using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace EFCoreContryCity.Models
{
    public class Country : BaseEntity
    {
        [Required]
        public string Name { get; set; }
        [Required]
        public string Capital { get; set; }
        [Required]
        public string President { get; set; }
        [Required]
        public decimal Population { get; set; }
        [Required]
        public string Language { get; set; }
        public ICollection<City> Cities { get; set; }

    }
}
