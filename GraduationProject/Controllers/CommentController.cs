using GraduationProject.Data;
using GraduationProject.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace GraduationProject.Controllers
{
    [Authorize]
    public class CommentController : Controller
    {
        private readonly ApplicationDbContext _context;

        public CommentController(ApplicationDbContext context)
        {
            _context = context;
        }

        // Show all comments
        public IActionResult Index()
        {
            var comments = _context.Comments
                                   .Include(c => c.Post)
                                   .Include(c => c.User)
                                   .ToList();
            return View(comments);
        }

        // Display details of a specific comment
        public IActionResult Details(int id)
        {
            var comment = _context.Comments
                                  .Include(c => c.Post)
                                  .Include(c => c.User)
                                  .FirstOrDefault(c => c.Id == id);
            if (comment == null)
            {
                return NotFound();
            }
            return View(comment);
        }

        //Display a new comment creation form
        [HttpGet]
        public IActionResult Create()
        {
            return View();
        }

        // Submit data to create a new comment
        [HttpPost]
        public IActionResult Create(Comment comment)
        {
            if (ModelState.IsValid)
            {
                comment.CreatedAt = DateTime.UtcNow;
                _context.Comments.Add(comment);
                _context.SaveChanges();
                return RedirectToAction(nameof(Index));
            }
            return View(comment);
        }

        // Display a comment edit form
        [HttpGet]
        public IActionResult Edit(int id)
        {
            var comment = _context.Comments.Find(id);
            if (comment == null)
            {
                return NotFound();
            }
            return View(comment);
        }

        // Send and save modification data
        [HttpPost]
        public IActionResult Edit(Comment comment)
        {
            if (ModelState.IsValid)
            {
                _context.Comments.Update(comment);
                _context.SaveChanges();
                return RedirectToAction(nameof(Index));
            }
            return View(comment);
        }

        // Display the deletion confirmation page
        [HttpGet]
        public IActionResult Delete(int id)
        {
            var comment = _context.Comments.Find(id);
            if (comment == null)
            {
                return NotFound();
            }
            return View(comment);
        }

        // Perform the delete operation
        [HttpPost, ActionName("Delete")]
        public IActionResult DeleteConfirmed(int id)
        {
            var comment = _context.Comments.Find(id);
            if (comment != null)
            {
                _context.Comments.Remove(comment);
                _context.SaveChanges();
            }
            return RedirectToAction(nameof(Index));
        }
    }
}