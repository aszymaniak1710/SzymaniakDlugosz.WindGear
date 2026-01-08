using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using SzymaniakDlugosz.WindGear.Core;
using SzymaniakDlugosz.WindGear.Interfaces;

namespace SzymaniakDlugosz.WindGear.DAOSQL
{
    // Definicje klas dla EF SQL
    // Implementacja interfejsu IManufacturer
    public class Manufacturer : IManufacturer
    {
        [Key]
        public int Id { get; set; }
        
        [Required]
        public string Name { get; set; }
        
        public string Country { get; set; }
        
        public int FoundedYear { get; set; }
    }
    // Implementacja interfejsu IProduct
    public class Product : IProduct
    {
        [Key]
        public int Id { get; set; }
        
        [Required]
        public string Model { get; set; }
        
        public double Area { get; set; }
        
        public string Material { get; set; }
        
        public SailType Type { get; set; }
        
        public bool IsCamber { get; set; }
        
        public int ManufacturerId { get; set; }
        
        [ForeignKey("ManufacturerId")]
        [NotMapped] 
        public IManufacturer Manufacturer 
        { 
            get => ManufacturerEntity; 
            set => ManufacturerEntity = value as Manufacturer; 
        }
        // Nawigacja do producenta 
        // Virtual umozliwia lazy loading 
        public virtual Manufacturer ManufacturerEntity { get; set; }
    }
}
