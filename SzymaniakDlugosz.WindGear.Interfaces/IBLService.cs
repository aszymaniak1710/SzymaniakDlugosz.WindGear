using System.Collections.Generic;

namespace SzymaniakDlugosz.WindGear.Interfaces
{
    public interface IBLService
    {
        List<IManufacturer> GetManufacturers();
        List<IProduct> GetProducts();
        
        void AddManufacturer(IManufacturer manufacturer);
        void AddProduct(IProduct product);
        
        void UpdateManufacturer(IManufacturer manufacturer);
        void UpdateProduct(IProduct product);
        
        void DeleteManufacturer(int id);
        void DeleteProduct(int id);
        
        IManufacturer CreateManufacturer();
        IProduct CreateProduct();
        
        // Additional business logic like validation could go here
    }
}
