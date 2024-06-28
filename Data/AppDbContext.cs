// AppDbContext.cs
using EmedicineBE.Models;
using Microsoft.EntityFrameworkCore;

namespace EmedicineBE.Data
{
    public class AppDbContext : DbContext
    {
        public DbSet<Cart> Carts { get; set; }

        public DbSet<Medicines> Medicines { get; set; }

        public DbSet<Orders> Orders { get; set; }

        public DbSet<Users> Users { get; set; }

        public DbSet<OrderItems> OrderItems { get; set; }

        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options)
        {
        }
    }
}