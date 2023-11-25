using System.ComponentModel.DataAnnotations;
using SkiBookingAPI.Data;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.OpenApi;
using Microsoft.AspNetCore.Http.HttpResults;

namespace SkiBookingAPI.Data.Models
{
    public class Equipment
    {
        [Key]
        [Required]
        public long EquipmentID { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public bool Active { get; set; }
        public decimal TotalCostPerDay { get; set; }
        public decimal TotalGSTPerDay { get; set; }
    }
}
