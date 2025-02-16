using GraduationProject.Models;
using GraduationProject.Repositories;
using GraduationProject.ViewModels;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace GraduationProject.Controllers
{
    public class UsersController : Controller
    {
        private readonly IRepository<User> _userRepository;
        private readonly UserManager<User> _userManager;
        private readonly SignInManager<User> _signInManager;
        //private readonly ApplicationDbContext _context;
        public UsersController(IRepository<User> userRepository, UserManager<User> userManager, SignInManager<User> signInManager)
        {
            _userRepository = userRepository;
            _userManager = userManager;
            _signInManager = signInManager;
        }
        [Authorize]
        public IActionResult Index()
        {
            var users = _userRepository.GetAll();
            //var users = _context.Users.ToList();
            return View(users);
        }

        [HttpGet]
        public IActionResult Register()
        {
            return View();
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Register(UserRegistrationViewModel model)
        {
            if (ModelState.IsValid)
            {
                // Create User object
                var user = new User
                {
                    UserName = model.Email,
                    Email = model.Email,
                    PhoneNumber = model.PhoneNumber,
                    Title = model.Title,
                };

                // Add addresses to the user
                foreach (var addressViewModel in model.Addresses)
                {
                    user.Addresses.Add(new Address
                    {
                        address = addressViewModel.Address,
                        City = addressViewModel.City,
                        PostalCode = addressViewModel.PostalCode,
                    });
                }

                // Save User and Addresses
                var result = await _userManager.CreateAsync(user, model.Password);

                if (result.Succeeded)
                {
                    return RedirectToAction("Index", "Home");
                }

                foreach (var error in result.Errors)
                {
                    ModelState.AddModelError(string.Empty, error.Description);
                }
            }

            return View(model);
        }


        // تسجيل الدخول (GET)
        [HttpGet]
        public IActionResult Login(string returnUrl = null)
        {
            ViewData["ReturnUrl"] = returnUrl;
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Login(LoginViewModel model, string returnUrl = null)
        {
            ViewData["ReturnUrl"] = returnUrl;

            if (!ModelState.IsValid)
            {
                return View(model);
            }

            // Find the user by email
            var user = await _userManager.FindByEmailAsync(model.Email);
            if (user == null)
            {
                ModelState.AddModelError(string.Empty, "Invalid login attempt.");
                return View(model);
            }

            // Verify password
            var isPasswordValid = await _userManager.CheckPasswordAsync(user, model.Password);
            if (!isPasswordValid)
            {
                ModelState.AddModelError(string.Empty, "Invalid login attempt.");
                return View(model);
            }

            // Sign the user in
            await _signInManager.SignInAsync(user, model.RememberMe);

            // Redirect to home or returnUrl if valid
            return RedirectToLocal(returnUrl);
        }

        // Helper method to handle redirects
        private IActionResult RedirectToLocal(string returnUrl)
        {
            if (!string.IsNullOrEmpty(returnUrl) && Url.IsLocalUrl(returnUrl))
            {
                return Redirect(returnUrl);
            }
            return RedirectToAction("Index", "Home");
        }


        // تسجيل الخروج
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Logout()
        {
            await _signInManager.SignOutAsync();
            return RedirectToAction(nameof(HomeController.Index), "Home");
        }

    }
}
