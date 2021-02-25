using System.ComponentModel.DataAnnotations;

namespace EFCoreContryCity.Models
{
    public class City : BaseEntity
    {
        [Required]
        public string Name { get; set; }
        [Required]
        public decimal Population { get; set; }
        [Required]
        public string Mayor { get; set; }
        [Required]
        public int CountryId { get; set; }
        public Country Country { get; set; }
    }
}
