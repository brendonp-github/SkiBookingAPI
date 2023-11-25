using System.ComponentModel.DataAnnotations;
using SkiBookingAPI.Data;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.OpenApi;
using Microsoft.AspNetCore.Http.HttpResults;

namespace SkiBookingAPI.Data.Models
{
    public class LiftTicket
    {
        [Key]
        [Required]
        public long LiftTicketID { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public bool Active { get; set; }
        public decimal TotalCostPerDay { get; set; }
        public decimal TotalGSTPerDay { get; set; }
    }
}
