using BillingApp.Models;
using Microsoft.AspNetCore.Mvc;

namespace BillingApp.Controllers
{
    public class AuthenticationController : Controller
    {
        private readonly BillingDbContext _context;

        public AuthenticationController(BillingDbContext context)
        {
            _context = context;
        }

        [HttpGet]
        public IActionResult Login()
        {
            return View();
        }

        [HttpPost]
        public IActionResult Login(LoginViewModel model)
        {
            // Check if the user exists in the database
            var user = _context.AppUsers
                .FirstOrDefault(u => u.Username == model.Username && u.Password == model.Password);

            if (user != null)
            {
                if (user.Property == "client")
                {
                    return RedirectToAction("Profile", "Registration", new { id = user.UserId });
                }
                else if (user.Property == "seller")
                {
                    // Redirect to the Seller's client list page
                    return RedirectToAction("ClientList", "Sellers");
                }
                else if (user.Property == "admin")
                {
                    return RedirectToAction("AdminDashboard", "Admins");
                }
                // Redirect to the personalized page
                return RedirectToAction("Greeting", new { firstName = user.FirstName });
            }

            // If login fails, return the view with an error message
            ViewBag.ErrorMessage = "Invalid username or password.";
            return View();
        }

        [HttpGet]
        public IActionResult Greeting(string firstName)
        {
            ViewBag.FirstName = firstName;
            return View();
        }
    }
}
