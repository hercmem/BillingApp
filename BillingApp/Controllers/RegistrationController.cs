using BillingApp.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace BillingApp.Controllers
{
    public class RegistrationController : Controller
    {
        private readonly BillingDbContext _context;

        public RegistrationController(BillingDbContext context)
        {
            _context = context;
        }

        [HttpGet]
        public IActionResult Register()
        {
            // Pass available programs for the dropdown
            ViewBag.ProgramNames = GetProgramNames();
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Register(AppUser model, string afm, string phoneNumber, string programName)
        {
            // Ensure ProgramNames is passed for POST in case of validation failure
            ViewBag.ProgramNames = GetProgramNames();

            if (model.Property != "client")
            {
                // Remove Client-specific fields from ModelState for Admin or Seller
                ModelState.Remove("afm");
                ModelState.Remove("phoneNumber");
                ModelState.Remove("programName");
            }

            if (ModelState.IsValid)
            {
                // Save the AppUser (basic user details)
                _context.AppUsers.Add(model);
                await _context.SaveChangesAsync();

                // If the Property is Client, save the Client and Phone data
                if (model.Property == "client")
                {
                    var phoneProgram = await _context.PhonePrograms
                        .FirstOrDefaultAsync(p => p.ProgramName == programName);

                    // Save Client details
                    var client = new Client
                    {
                        UserId = model.UserId,
                        Afm = afm,
                        PhoneNumber = phoneNumber
                    };
                    _context.Clients.Add(client);

                    // Save Phone details
                    var phone = new Phone
                    {
                        PhoneNumber = phoneNumber,
                        ProgramName = programName
                    };
                    _context.Phones.Add(phone);

                    // Associate the phone with the phone program
                    phone.ProgramNameNavigation = phoneProgram;
                }

                // Commit changes to the database
                await _context.SaveChangesAsync();

                return RedirectToAction("Profile", new { id = model.UserId });
            }

            // If validation fails, return the view with the model
            return View(model);
        }

        // Helper to fetch program names
        private List<string> GetProgramNames()
        {
            return new List<string>
    {
        "Basic Plan", "Business Plan", "Family Plan", "Premium Plan",
        "Standard Plan", "Student Plan", "Traveler Plan", "Unlimited Plan"
    };
        }




        public IActionResult Profile(int id)
        {
            // Retrieve user details
            var user = _context.AppUsers.Find(id);

            if (user == null)
            {
                return NotFound();
            }

            // Create a model to hold the user's data
            var profileData = new
            {
                user.UserId,
                user.FirstName,
                user.LastName,
                user.Username,
                user.Property,
                AdditionalInfo = user.Property == "client"
                    ? _context.Clients
                        .Where(c => c.UserId == user.UserId)
                        .Select(c => new
                        {
                            c.Afm,
                            c.PhoneNumber,
                            ProgramName = _context.Phones
                                .Where(p => p.PhoneNumber == c.PhoneNumber)
                                .Select(p => p.ProgramName)
                                .FirstOrDefault()
                        })
                        .FirstOrDefault()
                    : null
            };

            return View(profileData);
        }


    }
}
