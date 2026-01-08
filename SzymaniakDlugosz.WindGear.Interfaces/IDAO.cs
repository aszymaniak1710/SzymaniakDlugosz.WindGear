using System.Collections.Generic;

namespace SzymaniakDlugosz.WindGear.Interfaces
{
    public interface IDAO
    {
        List<IManufacturer> GetAllManufacturers();
        List<IProduct> GetAllProducts();
        
        void AddManufacturer(IManufacturer manufacturer);
        void AddProduct(IProduct product);
        
        void UpdateManufacturer(IManufacturer manufacturer);
        void UpdateProduct(IProduct product);
        
        void DeleteManufacturer(int id);
        void DeleteProduct(int id);
        IManufacturer CreateManufacturer();
        IProduct CreateProduct();
    }
}
