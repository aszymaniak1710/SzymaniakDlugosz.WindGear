using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using SzymaniakDlugosz.WindGear.Core;
using SzymaniakDlugosz.WindGear.Interfaces;
using SzymaniakDlugosz.WindGear.UI.Web.Models;

namespace SzymaniakDlugosz.WindGear.UI.Web.Controllers
{
    public class ProductController : Controller
    {
        private readonly IBLService _bl;

        public ProductController(IBLService bl)
        {
            _bl = bl;
        }

        private void PrepareViewBags()
        {
            ViewBag.Manufacturers = new SelectList(_bl.GetManufacturers(), "Id", "Name");
            // Enum logic
             ViewBag.SailTypes = new SelectList(Enum.GetValues(typeof(SailType)));
        }

        public IActionResult Index(string searchString)
        {
            var products = _bl.GetProducts();
            
            if (!string.IsNullOrEmpty(searchString))
            {
                products = products.Where(p => 
                    (p.Model != null && p.Model.Contains(searchString, StringComparison.OrdinalIgnoreCase)) ||
                    (p.Manufacturer != null && p.Manufacturer.Name.Contains(searchString, StringComparison.OrdinalIgnoreCase))
                ).ToList();
            }

            return View(products);
        }

        public IActionResult Create()
        {
            PrepareViewBags();
            return View();
        }

        [HttpPost]
        public IActionResult Create(ProductViewModel productModel)
        {
            ModelState.Remove(nameof(ProductViewModel.Manufacturer));
            if (ModelState.IsValid)
            {
                var product = _bl.CreateProduct();
                product.Model = productModel.Model;
                product.ManufacturerId = productModel.ManufacturerId;
                product.Area = productModel.Area;
                product.Material = productModel.Material;
                product.Type = productModel.Type;
                product.IsCamber = productModel.IsCamber;

                try
                {
                    _bl.AddProduct(product);
                    return RedirectToAction(nameof(Index));
                }
                catch (ArgumentException ex)
                {
                    ModelState.AddModelError("", ex.Message);
                }
                catch (Exception ex)
                {
                    ModelState.AddModelError("", "An error occurred while creating the product.");
                }
            }
            PrepareViewBags();
            return View(productModel);
        }

        public IActionResult Edit(int id)
        {
            var product = _bl.GetProducts().FirstOrDefault(p => p.Id == id);
            if (product == null) return NotFound();

            var model = new ProductViewModel
            {
                Id = product.Id,
                Model = product.Model,
                ManufacturerId = product.ManufacturerId,
                Area = product.Area,
                Material = product.Material,
                Type = product.Type,
                IsCamber = product.IsCamber
            };
            PrepareViewBags();
            return View(model);
        }

        [HttpPost]
        public IActionResult Edit(ProductViewModel productModel)
        {
            ModelState.Remove(nameof(ProductViewModel.Manufacturer));
            if (ModelState.IsValid)
            {
                try
                {
                    // Just use the ViewModel as IProduct, assuming it works with BL validation
                    _bl.UpdateProduct(productModel);
                    return RedirectToAction(nameof(Index));
                }
                catch (ArgumentException ex)
                {
                    ModelState.AddModelError("", ex.Message);
                }
                catch (Exception ex)
                {
                    ModelState.AddModelError("", "An error occurred while updating the product.");
                }
            }
            PrepareViewBags();
            return View(productModel);
        }

        public IActionResult Delete(int id)
        {
            var product = _bl.GetProducts().FirstOrDefault(p => p.Id == id);
            if (product == null) return NotFound();
            return View(product);
        }

        [HttpPost, ActionName("Delete")]
        public IActionResult DeleteConfirmed(int id)
        {
            _bl.DeleteProduct(id);
            return RedirectToAction(nameof(Index));
        }
    }
}
