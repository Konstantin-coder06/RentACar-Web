using Microsoft.EntityFrameworkCore;
using RentACar.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RentACar.DataAccess
{
    public class RentACarDbContext:DbContext
    {
        public RentACarDbContext(DbContextOptions<RentACarDbContext> options) : base(options) { }   
        public DbSet<Car> Cars { get; set; }
        public DbSet<CType> Types { get; set; }
        public DbSet<CarType> CarsTypes { get; set; }
        public DbSet<Feature> Features { get; set; }
        public DbSet<CarFeature> CarsFeatures { get; set; }
        public DbSet<Customer>Customers { get; set; }
        public DbSet<Reservation> Reservations { get; set; }
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
        }
    }
}
