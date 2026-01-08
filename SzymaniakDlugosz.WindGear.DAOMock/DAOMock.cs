using System;
using System.Collections.Generic;
using System.Linq;
using SzymaniakDlugosz.WindGear.Core;
using SzymaniakDlugosz.WindGear.Interfaces;

namespace SzymaniakDlugosz.WindGear.DAOMock
{
    public class DAOMock : IDAO
    {
        private List<IManufacturer> _manufacturers;
        private List<IProduct> _products;
        private int _nextManufacturerId = 1;
        private int _nextProductId = 1;

        public DAOMock()
        {
            _manufacturers = new List<IManufacturer>();
            _products = new List<IProduct>();

            // Seed some data
            AddManufacturer(new Manufacturer { Name = "NeilPryde", Country = "Hong Kong", FoundedYear = 1970 });
            AddManufacturer(new Manufacturer { Name = "Duotone", Country = "Austria", FoundedYear = 2018 });

            AddProduct(new Product { Model = "Combat", Area = 4.7, Material = "X-Ply", Type = SailType.Wave, IsCamber = false, ManufacturerId = 1 });
            AddProduct(new Product { Model = "Warp", Area = 7.8, Material = "Monofilm", Type = SailType.Slalom, IsCamber = true, ManufacturerId = 2 });
        }

        public List<IManufacturer> GetAllManufacturers()
        {
            return _manufacturers.ToList(); // Return copy
        }

        public List<IProduct> GetAllProducts()
        {
             // Join logic if needed for Manufacturer property, but for now just return list.
             // Manually linking objects to emulate ORM behavior
             foreach(var p in _products)
             {
                 p.Manufacturer = _manufacturers.FirstOrDefault(m => m.Id == p.ManufacturerId);
             }
             return _products.ToList();
        }

        public void AddManufacturer(IManufacturer manufacturer)
        {
            manufacturer.Id = _nextManufacturerId++;
            _manufacturers.Add(manufacturer);
        }

        public void AddProduct(IProduct product)
        {
            product.Id = _nextProductId++;
            _products.Add(product);
        }

        public void UpdateManufacturer(IManufacturer manufacturer)
        {
            var existing = _manufacturers.FirstOrDefault(m => m.Id == manufacturer.Id);
            if (existing != null)
            {
                existing.Name = manufacturer.Name;
                existing.Country = manufacturer.Country;
                existing.FoundedYear = manufacturer.FoundedYear;
            }
        }

        public void UpdateProduct(IProduct product)
        {
            var existing = _products.FirstOrDefault(p => p.Id == product.Id);
            if (existing != null)
            {
                existing.Model = product.Model;
                existing.Area = product.Area;
                existing.Material = product.Material;
                existing.Type = product.Type;
                existing.IsCamber = product.IsCamber;
                existing.ManufacturerId = product.ManufacturerId;
            }
        }

        public void DeleteManufacturer(int id)
        {
            var item = _manufacturers.FirstOrDefault(m => m.Id == id);
            if (item != null)
            {
                _manufacturers.Remove(item);
                // Cascade delete? Simple mock, maybe yes.
                _products.RemoveAll(p => p.ManufacturerId == id);
            }
        }

        public void DeleteProduct(int id)
        {
             var item = _products.FirstOrDefault(p => p.Id == id);
             if (item != null) _products.Remove(item);
        }

        public IManufacturer CreateManufacturer()
        {
            return new Manufacturer();
        }

        public IProduct CreateProduct()
        {
            return new Product();
        }
        
        // Private implementations of interfaces
        private class Manufacturer : IManufacturer
        {
            public int Id { get; set; }
            public string Name { get; set; }
            public string Country { get; set; }
            public int FoundedYear { get; set; }
        }

        private class Product : IProduct
        {
            public int Id { get; set; }
            public string Model { get; set; }
            public double Area { get; set; }
            public string Material { get; set; }
            public SailType Type { get; set; }
            public bool IsCamber { get; set; }
            public int ManufacturerId { get; set; }
            public IManufacturer Manufacturer { get; set; }
        }
    }
}
