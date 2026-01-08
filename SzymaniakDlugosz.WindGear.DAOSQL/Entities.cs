using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using SzymaniakDlugosz.WindGear.Core;
using SzymaniakDlugosz.WindGear.Interfaces;

namespace SzymaniakDlugosz.WindGear.DAOSQL
{
    public class Manufacturer : IManufacturer
    {
        [Key]
        public int Id { get; set; }
        
        [Required]
        public string Name { get; set; }
        
        public string Country { get; set; }
        
        public int FoundedYear { get; set; }
    }

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
        [NotMapped] // Interface property navigation issue in EF sometimes, but let's try mapping it if we cast
        public IManufacturer Manufacturer 
        { 
            get => ManufacturerEntity; 
            set => ManufacturerEntity = value as Manufacturer; 
        }

        public virtual Manufacturer ManufacturerEntity { get; set; }
    }
}
