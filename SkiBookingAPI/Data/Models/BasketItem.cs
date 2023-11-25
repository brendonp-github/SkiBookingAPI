using System.ComponentModel.DataAnnotations;

namespace SkiBookingAPI.Data.Models
{
    public class BasketItem
    {
        [Key]
        [Required]
        public long BasketItemID { get; set; }
        public long? PackageID { get; set; }
        public long? EquipmentID { get; set; }
        public long? LiftTicketID { get; set; }
        public int Quantity { get; set; }
        public int? NumDays { get; set; }
    }
}
