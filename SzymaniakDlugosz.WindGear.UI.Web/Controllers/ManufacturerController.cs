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
                // Map ViewModel to IManufacturer implementation handled by BL implementation or simple cast if model implements interface directly
                // Since ViewModel implements interface we can pass it, but BL creates new instances typically.
                // However, our BL takes IManufacturer. Let's create a new one using BL factory or just pass the model if it fits.
                // The safest way with current BL API which has CreateManufacturer() is to map properties.
                
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
                // We need to get the "real" object or just pass a compatible object.
                // The BL might rely on specific implementation types from DAO if not careful, 
                // but usually it works on Interfaces.
                // Let's assume we can map back to a fresh object or the viewmodel itself if it's accepted.
                // As ViewModel implements IManufacturer, we can try passing it directly IF the DAO accepts arbitrary implementations (which it should).
                // However, to be safe and cleaner, let's look up or map.
                // Since we don't have "GetById" in BL, we filter from GetAll.
                
                // Constructing an object to pass to Update.
                // Since our ViewModel implements IManufacturer, we can effectively use it as a DTO.
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
