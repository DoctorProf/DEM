using Microsoft.EntityFrameworkCore;
using System.Windows.Controls;
using DEM.DataBase.models;

namespace DEM.DataBase
{
    public class DataBaseContext : DbContext
    {
        public DbSet<Partner> Partners { get; set; }
        public DbSet<Product> Products { get; set; }
        public DbSet<Order> Orders { get; set; }
        public DbSet<OrderItem> OrderItems { get; set; }
        public DbSet<Employee> Employees { get; set; }
        public DbSet<User> Users { get; set; }
        public DbSet<Manager> Managers { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseSqlServer("Server=DIMANPC;Database=MasterPol;Trusted_Connection=True;TrustServerCertificate=True;");
        }
    }
}