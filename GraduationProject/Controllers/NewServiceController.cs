using GraduationProject.Interface;
using GraduationProject.ViewModels;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;

namespace GraduationProject.Controllers
{
    [Authorize]
    public class NewServiceController : Controller
    {
        private readonly IServiceService _serviceService;

        public NewServiceController(IServiceService serviceService)
        {
            _serviceService = serviceService;
        }

        // Index: Display all services with optional search, sorting, and pagination
        public async Task<IActionResult> Index(string? search, string? sortBy, int page = 1)
        {
            const int pageSize = 10;
            var services = await _serviceService.GetAllAsync(search, sortBy, page, pageSize);
            return View(services);
        }

        // Details: Display a single service
        public async Task<IActionResult> Details(int id)
        {
            var service = await _serviceService.GetByIdAsync(id);
            if (service == null)
            {
                return NotFound();
            }
            return View(service);
        }

        // Create: Display the form for creating a new service
        public IActionResult Create()
        {
            return View();
        }

        // Create: Handle form submission for creating a new service
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(ServiceViewModel model)
        {
            if (!ModelState.IsValid)
            {
                return View(model);
            }

            await _serviceService.CreateAsync(model);
            return RedirectToAction(nameof(Index));
        }

        // Edit: Display the form for editing an existing service
        public async Task<IActionResult> Edit(int id)
        {
            var service = await _serviceService.GetByIdAsync(id);
            if (service == null)
            {
                return NotFound();
            }
            return View(service);
        }

        // Edit: Handle form submission for updating an existing service
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, ServiceViewModel model)
        {
            if (id != model.Id)
            {
                return BadRequest();
            }

            if (!ModelState.IsValid)
            {
                return View(model);
            }

            await _serviceService.UpdateAsync(model);
            return RedirectToAction(nameof(Index));
        }

        // Delete: Display confirmation page for deleting a service
        public async Task<IActionResult> Delete(int id)
        {
            var service = await _serviceService.GetByIdAsync(id);
            if (service == null)
            {
                return NotFound();
            }
            return View(service);
        }

        // Delete: Handle deletion of a service
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            await _serviceService.DeleteAsync(id);
            return RedirectToAction(nameof(Index));
        }
    }
}
