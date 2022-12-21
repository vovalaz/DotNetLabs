using lab_2.Entities;
using Microsoft.EntityFrameworkCore;

namespace lab_2.EFCore
{
    public class AppDbContext : DbContext
    {
        public AppDbContext()    {}

        public AppDbContext(DbContextOptions<AppDbContext> options)
        : base(options) {}

        public DbSet<Passangers> Passangers { get; set; }
        public DbSet<Stations> Stations { get; set; }
        public DbSet<Tickets> Tickets { get; set; }
        public DbSet<Trains> Trains { get; set; }
        public DbSet<Cars> Cars { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {
                optionsBuilder.UseSqlServer("Server=(localdb)\\mssqllocaldb;Database=Booking;" +
                    "Trusted_Connection=True;MultipleActiveResultSets=true");
            }
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Cars>().HasKey(cars => 
                    new { cars.CarId, cars.TrainId });
            modelBuilder.Entity<Passangers>().HasKey(passangers => 
                    new { passangers.PassengerId});
            modelBuilder.Entity<Stations>().HasKey(stations =>
                    new { stations.StationId });
            modelBuilder.Entity<Trains>().HasKey(trains =>
                    new { trains.TrainType });

            modelBuilder.Entity<Tickets>().HasKey(tickets => 
                    new { tickets.TicketId  });

            OnModelCreatingPartial(modelBuilder);
        }

        private void OnModelCreatingPartial(ModelBuilder modelBuilder) { }
    }
}