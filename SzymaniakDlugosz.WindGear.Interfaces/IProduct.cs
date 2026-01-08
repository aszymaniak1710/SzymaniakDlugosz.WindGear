using SzymaniakDlugosz.WindGear.Core;

namespace SzymaniakDlugosz.WindGear.Interfaces
{
    public interface IProduct
    {
        int Id { get; set; }
        string Model { get; set; }
        double Area { get; set; }
        string Material { get; set; }
        SailType Type { get; set; }
        bool IsCamber { get; set; }
        
        int ManufacturerId { get; set; }
        IManufacturer Manufacturer { get; set; }
    }
}
