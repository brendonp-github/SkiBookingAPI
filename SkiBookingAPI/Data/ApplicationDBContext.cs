using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Hosting;
using SkiBookingAPI.Data.Models;

namespace SkiBookingAPI.Data
{
    public class ApplicationDBContext : IdentityDbContext<ApplicationUser>
    {
        public ApplicationDBContext() : base() { }
        public ApplicationDBContext(DbContextOptions dbContextOptions) : base(dbContextOptions) { }
        public DbSet<Equipment> Equipment => Set<Equipment>();
        public DbSet<LiftTicket> LiftTicket => Set<LiftTicket>();
        public DbSet<Package> Package => Set<Package>();
        public DbSet<BasketItem> BasketItem => Set<BasketItem>();
    }
}
