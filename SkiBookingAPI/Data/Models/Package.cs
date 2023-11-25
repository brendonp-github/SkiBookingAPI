using System.ComponentModel.DataAnnotations;

namespace SkiBookingAPI.Data.Models
{
    public class Package
    {
        [Key]
        [Required]
        public long PackageID { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public bool Active { get; set; }
        public decimal TotalCost { get; set; }
        public decimal TotalGST { get; set; }
    }
}
