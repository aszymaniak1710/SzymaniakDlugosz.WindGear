using Microsoft.EntityFrameworkCore;

namespace SzymaniakDlugosz.WindGear.DAOSQL
{
    // Kontekst Entity Framework
    // Po³¹czenie z baz¹ danych
    public class WindGearContext : DbContext
    {
        public DbSet<Manufacturer> Manufacturers { get; set; } // kolekcja obiektów odpowiadaj¹ca tabeli w bazie danych
        public DbSet<Product> Products { get; set; }

        public WindGearContext()
        {
            // Tworzenie instancji kontekstu bez parametrów
        }

        // Opcje kontekstu 
        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured) // czy wczeœniej ustawiony provider
            {
                 optionsBuilder.UseSqlite("Data Source=WindGear.db"); // Baza SQLite
            }
        }
    }
}
