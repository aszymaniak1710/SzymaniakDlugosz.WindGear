using System.ComponentModel.DataAnnotations;
using SzymaniakDlugosz.WindGear.Core;
using SzymaniakDlugosz.WindGear.Interfaces;

namespace SzymaniakDlugosz.WindGear.UI.Web.Models
{
    public class ProductViewModel : IProduct
    {
        public int Id { get; set; }

        [Required(ErrorMessage = "Model is required")]
        public string Model { get; set; }

        [Required(ErrorMessage = "Manufacturer is required")]
        public int ManufacturerId { get; set; }
        
        // Navigation property for display
        public IManufacturer? Manufacturer { get; set; }

        [Required(ErrorMessage = "Area is required")]
        [Range(0.1, 20.0, ErrorMessage = "Area must be between 0.1 and 20.0 m²")]
        public double Area { get; set; }

        [Required(ErrorMessage = "Material is required")]
        public string Material { get; set; }

        [Required(ErrorMessage = "Type is required")]
        public SailType Type { get; set; }

        public bool IsCamber { get; set; }
    }
}
