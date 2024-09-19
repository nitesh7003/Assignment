using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Http;
using SampleMVCAppDB.Data;
using SampleMVCAppDB.Models;

namespace SampleMVCAppDB.Controllers
{
    public class AuthenticationController : Controller
    {
        private readonly ApplicationDbContext db;
        public AuthenticationController(ApplicationDbContext db)
        {
            this.db = db;
        }

        [HttpGet]
        public IActionResult Register()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Register(RegisterViewModel model)
        {
            if (ModelState.IsValid)
            {
                // Check if email or username is already in use
                if (db.Users.Any(u => u.Email == model.Email || u.Username == model.Username))
                {
                    ModelState.AddModelError("", "Email or Username already exists.");
                    return View(model);
                }

                // Hash and salt the password
                var passwordHash = BCrypt.Net.BCrypt.HashPassword(model.Password);

                var user = new User
                {
                    Username = model.Username,
                    Email = model.Email,
                    PasswordHash = passwordHash,
                    Role = "User"  // Hard-coded role for registration
                };

                db.Users.Add(user);
                await db.SaveChangesAsync();

                // Store session for user details
                HttpContext.Session.SetString("Username", user.Username);
                HttpContext.Session.SetString("Role", user.Role); // Store the user role in session

                // Redirect to dashboard after successful registration
                return RedirectToAction("Index", "Home");
            }

            return View(model);
        }


        [HttpGet]
        public IActionResult Login()
        {
            return View();
        }

        [HttpPost]
        public IActionResult Login(LoginViewModel model)
        {
            if (ModelState.IsValid)
            {
                var user = db.Users.FirstOrDefault(u => u.Username == model.Username);

                if (user != null && BCrypt.Net.BCrypt.Verify(model.Password, user.PasswordHash))
                {
                    // Store session for user details
                    HttpContext.Session.SetString("Username", user.Username);
                    HttpContext.Session.SetString("Role", user.Role); // Store the user role in session
                    return RedirectToAction("Index", "Home");
                }
                else
                {
                    ModelState.AddModelError("", "Invalid Username or Password.");
                }
            }
            return View(model);
        }

        // Logout Action
        public IActionResult Logout()
        {
            
            HttpContext.Session.Clear();

            return RedirectToAction("Register");
        }
    }
}
