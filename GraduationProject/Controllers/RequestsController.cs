using GraduationProject.Models;
using GraduationProject.Data;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Authorization;

namespace GraduationProject.Controllers
{
    [Authorize]
    public class RequestsController : Controller
    {
        private readonly ApplicationDbContext _context;

        public RequestsController(ApplicationDbContext context)
        {
            _context = context;
        }


        public async Task<IActionResult> Index()
        {
            var requests = await _context.Requests
                .Include(r => r.User)
                .Include(r => r.Service)
                .ToListAsync();
            return View(requests);
        }


        public async Task<IActionResult> Details(int id)
        {
            var request = await _context.Requests
                .Include(r => r.User)
                .Include(r => r.Service)
                .FirstOrDefaultAsync(r => r.Id == id);

            if (request == null)
                return NotFound();

            return View(request);
        }


        [HttpGet]
        public IActionResult Create()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Create(Request model)
        {
            if (ModelState.IsValid)
            {
                _context.Requests.Add(model);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(model);
        }

        [HttpGet]
        public async Task<IActionResult> Edit(int id)
        {
            var request = await _context.Requests.FindAsync(id);

            if (request == null)
                return NotFound();

            return View(request);
        }

        [HttpPost]
        public async Task<IActionResult> Edit(int id, Request model)
        {
            if (id != model.Id)
                return BadRequest();

            if (ModelState.IsValid)
            {
                _context.Requests.Update(model);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(model);
        }

        [HttpGet]
        public async Task<IActionResult> Delete(int id)
        {
            var request = await _context.Requests.FindAsync(id);

            if (request == null)
                return NotFound();

            return View(request);
        }

        [HttpPost, ActionName("Delete")]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var request = await _context.Requests.FindAsync(id);

            if (request == null)
                return NotFound();

            _context.Requests.Remove(request);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }
    }
}
