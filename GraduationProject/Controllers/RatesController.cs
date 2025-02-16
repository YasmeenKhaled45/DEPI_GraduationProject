using GraduationProject.Data;
using GraduationProject.Models;
using GraduationProject.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using System.Security.Claims;

namespace GraduationProject.Controllers
{
    public class RatesController : Controller
    {

        private readonly ApplicationDbContext _context;

        public RatesController(ApplicationDbContext context)
        {
            _context = context;
        }

        // GET: Rate
        public async Task<IActionResult> Index()
        {
            var rates = _context.Rates.Include(r => r.User).Include(r => r.Service);
            return View(await rates.ToListAsync());
        }

        // GET: Rate/Details/5
        public async Task<IActionResult> Details(int id)
        {
            var rate = await _context.Rates
                .Include(r => r.User)
                .Include(r => r.Service)
                .FirstOrDefaultAsync(m => m.Id == id);

            if (rate == null)
            {
                return NotFound();
            }

            return View(rate);
        }

        // GET: Rate/Create
        public IActionResult Create()
        {
            ViewData["ServiceId"] = new SelectList(_context.Services.ToList(), "Id", "Name");
            return View(new Rate());
        }

        public async Task<IActionResult> Edit(int id)
        {
            var rate = await _context.Rates.FindAsync(id);
            if (rate == null)
            {
                return NotFound();
            }

            ViewBag.Services = new SelectList(_context.Services, "Id", "Name", rate.ServiceId);
            return View(rate);
        }
        // POST: Rate/Create
        [HttpPost]
        [Authorize]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(Rate rate)
        {

            rate.UserId = User!.FindFirstValue(ClaimTypes.NameIdentifier);// Assign user
            _context.Add(rate);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        // POST: Rate/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, Rate rate)
        {
            if (id != rate.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(rate);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!_context.Rates.Any(e => e.Id == rate.Id))
                    {
                        return NotFound();
                    }
                    else
                    {
                        throw;
                    }
                }
                return RedirectToAction(nameof(Index));
            }
            return View(rate);
        }

        // GET: Rate/Delete/5
        public async Task<IActionResult> Delete(int id)
        {
            var rate = await _context.Rates
                .Include(r => r.User)
                .Include(r => r.Service)
                .FirstOrDefaultAsync(m => m.Id == id);

            if (rate == null)
            {
                return NotFound();
            }

            return View(rate);
        }

        // POST: Rate/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var rate = await _context.Rates.FindAsync(id);
            if (rate != null)
            {
                _context.Rates.Remove(rate);
                await _context.SaveChangesAsync();
            }
            return RedirectToAction(nameof(Index));
        }
    }
}
