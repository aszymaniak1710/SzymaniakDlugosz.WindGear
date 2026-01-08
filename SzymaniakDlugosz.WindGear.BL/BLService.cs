using System;
using System.Collections.Generic;
using System.Configuration;
using System.IO;
using System.Linq;
using System.Reflection;
using SzymaniakDlugosz.WindGear.Interfaces;

namespace SzymaniakDlugosz.WindGear.BL
{
    public class BLService : IBLService
    {
        private IDAO _dao;

        public BLService()
        {
            LoadDAO();
        }

        private void LoadDAO()
        {
            // config
            string daoAssemblyKey = ConfigurationManager.AppSettings["DAOLibrary"];
            if (string.IsNullOrEmpty(daoAssemblyKey))
            {
                throw new ConfigurationErrorsException("DAOLibrary key is missing in configuration.");
            }

            // Nazwa biblioteki DAO z pliku konfiguracyjnego
            
            string assemblyPath = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, daoAssemblyKey);
            // Nazwa np SzymaniakDlugosz.WindGear.DAOMock.dll
            
            if (!File.Exists(assemblyPath))
            {
                 // Próba dodania rozszerzenia .dll
                 if (File.Exists(assemblyPath + ".dll")) assemblyPath += ".dll";
                 else throw new FileNotFoundException($"DAO Assembly not found at {assemblyPath}");
            }

            Assembly assembly = Assembly.LoadFrom(assemblyPath);
            
            // Szukanie klasy implementującej IDAO 
            Type daoType = assembly.GetTypes().FirstOrDefault(t => typeof(IDAO).IsAssignableFrom(t) && !t.IsInterface && !t.IsAbstract);
            
            if (daoType == null)
            {
                throw new Exception($"No implementation of IDAO found in assembly {daoAssemblyKey}");
            }

            _dao = (IDAO)Activator.CreateInstance(daoType);
        }

        // Late binding względem DAO
        // _dao to typ nieznany w czasie kompilacji

        public List<IManufacturer> GetManufacturers()
        {
            return _dao.GetAllManufacturers();
        }

        public List<IProduct> GetProducts()
        {
            return _dao.GetAllProducts();
        }

        public void AddManufacturer(IManufacturer manufacturer)
        {
            ValidateManufacturer(manufacturer);
            _dao.AddManufacturer(manufacturer);
        }

        public void AddProduct(IProduct product)
        {
            ValidateProduct(product);
            _dao.AddProduct(product);
        }

        public void UpdateManufacturer(IManufacturer manufacturer)
        {
            ValidateManufacturer(manufacturer);
            _dao.UpdateManufacturer(manufacturer);
        }

        public void UpdateProduct(IProduct product)
        {
            ValidateProduct(product);
            _dao.UpdateProduct(product);
        }

        public void DeleteManufacturer(int id)
        {
            _dao.DeleteManufacturer(id);
        }

        public void DeleteProduct(int id)
        {
            _dao.DeleteProduct(id);
        }

        public IManufacturer CreateManufacturer()
        {
            return _dao.CreateManufacturer();
        }

        public IProduct CreateProduct()
        {
            return _dao.CreateProduct();
        }
        
        // Logika walidacji
        private void ValidateManufacturer(IManufacturer m)
        {
            if (string.IsNullOrWhiteSpace(m.Name)) throw new ArgumentException("Manufacturer Name cannot be empty.");
            if (string.IsNullOrWhiteSpace(m.Country)) throw new ArgumentException("Country cannot be empty.");
            if (m.FoundedYear < 1900 || m.FoundedYear > DateTime.Now.Year) throw new ArgumentException($"Founded Year must be between 1900 and {DateTime.Now.Year}.");
        }

        private void ValidateProduct(IProduct p)
        {
            if (string.IsNullOrWhiteSpace(p.Model)) throw new ArgumentException("Model cannot be empty.");
            if (p.Area <= 0 || p.Area > 20) throw new ArgumentException("Area must be between 0 and 20 m².");
            if (string.IsNullOrWhiteSpace(p.Material)) throw new ArgumentException("Material cannot be empty.");
            // Typ enum, ustawiony zawsze ok
            if (p.ManufacturerId <= 0) throw new ArgumentException("Product must be assigned to a Manufacturer.");
        }
    }
}
