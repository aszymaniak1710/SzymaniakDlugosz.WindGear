using Microsoft.EntityFrameworkCore;

namespace SzymaniakDlugosz.WindGear.DAOSQL
{
    public class WindGearContext : DbContext
    {
        public DbSet<Manufacturer> Manufacturers { get; set; }
        public DbSet<Product> Products { get; set; }

        public WindGearContext()
        {
            // Just for parameterless constructor
            // Database.EnsureCreated(); // Better to call this in DAO constructor or explicit init to avoid issues during design time
        }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {
                 // Path relative to execution? Ideally configurable path.
                 // For now hardcoded or relative to DLL.
                 optionsBuilder.UseSqlite("Data Source=WindGear.db");
            }
        }
    }
}
