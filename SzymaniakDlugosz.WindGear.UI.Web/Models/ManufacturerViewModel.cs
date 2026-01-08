using System.ComponentModel.DataAnnotations;
using SzymaniakDlugosz.WindGear.Interfaces;

namespace SzymaniakDlugosz.WindGear.UI.Web.Models
{
    public class ManufacturerViewModel : IManufacturer
    {
        public int Id { get; set; }

        [Required(ErrorMessage = "Name is required")]
        [MinLength(2, ErrorMessage = "Name must be at least 2 characters long")]
        public string Name { get; set; }

        [Required(ErrorMessage = "Country is required")]
        [MinLength(2, ErrorMessage = "Country must be at least 2 characters long")]
        public string Country { get; set; }

        [Required(ErrorMessage = "Founded Year is required")]
        [Range(1900, 2100, ErrorMessage = "Founded Year must be between 1900 and 2100")]
        public int FoundedYear { get; set; }
    }
}
