using Microsoft.AspNetCore.Mvc;
using SzymaniakDlugosz.WindGear.Interfaces;
using SzymaniakDlugosz.WindGear.UI.Web.Models;

namespace SzymaniakDlugosz.WindGear.UI.Web.Controllers
{
    public class ManufacturerController : Controller
    {
        private readonly IBLService _bl;

        public ManufacturerController(IBLService bl)
        {
            _bl = bl;
        }

        public IActionResult Index()
        {
            var manufacturers = _bl.GetManufacturers();
            return View(manufacturers);
        }

        public IActionResult Create()
        {
            return View();
        }

        [HttpPost]
        public IActionResult Create(ManufacturerViewModel model)
        {
            if (ModelState.IsValid)
            {
                
                var manufacturer = _bl.CreateManufacturer();
                manufacturer.Name = model.Name;
                manufacturer.Country = model.Country;
                manufacturer.FoundedYear = model.FoundedYear;

                try
                {
                    _bl.AddManufacturer(manufacturer);
                    return RedirectToAction(nameof(Index));
                }
                catch (ArgumentException ex)
                {
                    ModelState.AddModelError("", ex.Message);
                }
                catch (Exception ex)
                {
                    ModelState.AddModelError("", "An error occurred: " + ex.Message);
                }
            }
            return View(model);
        }

        public IActionResult Edit(int id)
        {
            var manufacturer = _bl.GetManufacturers().FirstOrDefault(m => m.Id == id);
            if (manufacturer == null) return NotFound();

            var model = new ManufacturerViewModel
            {
                Id = manufacturer.Id,
                Name = manufacturer.Name,
                Country = manufacturer.Country,
                FoundedYear = manufacturer.FoundedYear
            };
            return View(model);
        }

        [HttpPost]
        public IActionResult Edit(ManufacturerViewModel model)
        {
            if (ModelState.IsValid)
            {
                try
                {
                    _bl.UpdateManufacturer(model);
                    return RedirectToAction(nameof(Index));
                }
                catch (ArgumentException ex)
                {
                     ModelState.AddModelError("", ex.Message);
                }
                catch (Exception ex)
                {
                     ModelState.AddModelError("", "An error occurred: " + ex.Message);
                }
            }
            return View(model);
        }

        public IActionResult Delete(int id)
        {
            var manufacturer = _bl.GetManufacturers().FirstOrDefault(m => m.Id == id);
            if (manufacturer == null) return NotFound();
            return View(manufacturer);
        }

        [HttpPost, ActionName("Delete")]
        public IActionResult DeleteConfirmed(int id)
        {
            _bl.DeleteManufacturer(id);
            return RedirectToAction(nameof(Index));
        }
    }
}
