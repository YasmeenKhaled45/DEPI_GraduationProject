using GraduationProject.Models;
using GraduationProject.Data;
using GraduationProject.Interface;
using GraduationProject.ViewModels;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Authorization;

namespace GraduationProject.Controllers
{
    [Authorize]
    public class PostsController : Controller
    {

        private readonly IPostService _postService;

        public PostsController(IPostService postService)
        {
            _postService = postService;
        }
           
        // Get all posts with optional search and sorting.
        [HttpGet]
        public async Task<IActionResult> Index(string? search, string? sortBy, int page = 1, int pageSize = 10)
        {
            var posts = await _postService.GetAllAsync(search, sortBy, page, pageSize);
            return View(posts);
        }

        // Get details of a specific post by ID.
        [HttpGet]
        public async Task<IActionResult> Details(int id)
        {
            var post = await _postService.GetByIdAsync(id);
            if (post == null)
            {
                return NotFound();
            }
            return View(post);
        }

        // Display the create post form.
        [HttpGet]
        public IActionResult Create()
        {
            return View();
        }

        // Handle form submission for creating a new post.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(PostViewModel model)
        {
            if (ModelState.IsValid)
            {
                await _postService.CreateAsync(model);
                return RedirectToAction(nameof(Index));
            }
            return View(model);
        }

        // Display the edit form for a specific post.
        [HttpGet]
        public async Task<IActionResult> Edit(int id)
        {
            var post = await _postService.GetByIdAsync(id);
            if (post == null)
            {
                return NotFound();
            }
            return View(post);
        }

        // Handle form submission for updating a specific post.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, PostViewModel model)
        {
            if (id != model.Id)
            {
                return BadRequest();
            }

            if (ModelState.IsValid)
            {
                await _postService.UpdateAsync(model);
                return RedirectToAction(nameof(Index));
            }
            return View(model);
        }

        // Display the delete confirmation page for a specific post.
        [HttpGet]
        public async Task<IActionResult> Delete(int id)
        {
            var post = await _postService.GetByIdAsync(id);
            if (post == null)
            {
                return NotFound();
            }
            return View(post);
        }

        // Handle the deletion of a specific post.
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            await _postService.DeleteAsync(id);
            return RedirectToAction(nameof(Index));
        }
    }
}

