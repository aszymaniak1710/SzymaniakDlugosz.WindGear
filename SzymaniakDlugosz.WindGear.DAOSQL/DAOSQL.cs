using System.Collections.Generic;
using System.Linq;
using Microsoft.EntityFrameworkCore;
using SzymaniakDlugosz.WindGear.Interfaces;
using SzymaniakDlugosz.WindGear.Core;

namespace SzymaniakDlugosz.WindGear.DAOSQL
{
    public class DAOSQL : IDAO
    {
        public DAOSQL()
        {
            using (var context = new WindGearContext())
            {
                context.Database.EnsureCreated();
                
                // Seed if empty
                if (!context.Manufacturers.Any())
                {
                    var m1 = new Manufacturer { Name = "Severne", Country = "Australia", FoundedYear = 2003 };
                    var m2 = new Manufacturer { Name = "Goya", Country = "USA", FoundedYear = 2000 };
                    context.Manufacturers.AddRange(m1, m2);
                    context.SaveChanges();
                    
                    context.Products.Add(new Product { Model = "Blade", Area = 5.0, Material = "Membrane", Type = SailType.Wave, IsCamber = false, ManufacturerId = m1.Id, ManufacturerEntity = m1 });
                    context.Products.Add(new Product { Model = "Mach 5", Area = 7.8, Material = "Monofilm", Type = SailType.Slalom, IsCamber = true, ManufacturerId = m1.Id, ManufacturerEntity = m1 });
                     context.SaveChanges();
                }
            }
        }

        public List<IManufacturer> GetAllManufacturers()
        {
            using (var db = new WindGearContext())
            {
                // ToList returns concrete types, but they are IManufacturer compatible.
                // We need to verify if List<Manufacturer> can be returned as List<IManufacturer>. 
                // No, List<T> is invariant. We must Cast/Convert.
                return db.Manufacturers.ToList<IManufacturer>();
            }
        }

        public List<IProduct> GetAllProducts()
        {
             using (var db = new WindGearContext())
            {
                var list = db.Products.Include(p => p.ManufacturerEntity).ToList();
                // Fix up interface property
                foreach(var item in list) item.Manufacturer = item.ManufacturerEntity;
                return list.Cast<IProduct>().ToList();
            }
        }

        public void AddManufacturer(IManufacturer manufacturer)
        {
            using (var db = new WindGearContext())
            {
                // We receive an interface, likely from BL which got it from CreateManufacturer().
                // If it's our concrete type, we can attach.
                if (manufacturer is Manufacturer m)
                {
                    db.Manufacturers.Add(m);
                    db.SaveChanges();
                    manufacturer.Id = m.Id; // Update ID
                }
            }
        }

        public void AddProduct(IProduct product)
        {
             using (var db = new WindGearContext())
            {
                if (product is Product p)
                {
                    db.Products.Add(p);
                    db.SaveChanges();
                    product.Id = p.Id;
                }
            }
        }

        public void UpdateManufacturer(IManufacturer manufacturer)
        {
             using (var db = new WindGearContext())
            {
                if (manufacturer is Manufacturer m)
                {
                    db.Manufacturers.Update(m);
                    db.SaveChanges();
                }
            }
        }

        public void UpdateProduct(IProduct product)
        {
             using (var db = new WindGearContext())
            {
                if (product is Product p)
                {
                    db.Products.Update(p);
                    db.SaveChanges();
                }
            }
        }

        public void DeleteManufacturer(int id)
        {
            using (var db = new WindGearContext())
            {
                var item = db.Manufacturers.Find(id);
                if (item != null)
                {
                    db.Manufacturers.Remove(item);
                    db.SaveChanges();
                }
            }
        }

        public void DeleteProduct(int id)
        {
            using (var db = new WindGearContext())
            {
                var item = db.Products.Find(id);
                if (item != null)
                {
                    db.Products.Remove(item);
                    db.SaveChanges();
                }
            }
        }

        public IManufacturer CreateManufacturer()
        {
            return new Manufacturer();
        }

        public IProduct CreateProduct()
        {
            return new Product();
        }
    }
}
