using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using BillingApp.Models;
using X.PagedList.Extensions;

namespace BillingApp.Controllers
{
    public class ClientsController : Controller
    {
        private readonly BillingDbContext _context;

        public ClientsController(BillingDbContext context)
        {
            _context = context;
        }

        // GET: Clients
        public  IActionResult Index(int? page, string? search)
        {
            ViewData["CurrentFilter"] = search;
            var clients = _context.Clients
                .Include(c => c.PhoneNumberNavigation)
                .Include(c => c.User)
                .AsQueryable();


            if (!String.IsNullOrEmpty(search))
            {
                clients = clients.Where(c => c.User.FirstName.StartsWith(search) ||
                                                c.User.LastName.StartsWith(search) ||
                                                c.Afm.StartsWith(search) ||
                                                c.PhoneNumber.StartsWith(search));
            }

            //Pagination
            if (page != null && page < 1)
            {
                page = 1;
            }

            int PageSize = 10;

            return View(clients.ToPagedList(page ?? 1, PageSize));
        }

        // GET: Clients/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var client = await _context.Clients
                .Include(c => c.PhoneNumberNavigation)
                .ThenInclude(p => p.Bills)
                .Include(c => c.User)
                .FirstOrDefaultAsync(m => m.ClientId == id);
            if (client == null)
            {
                return NotFound();
            }

            return View(client);
        }

        // GET: Clients/Create
        public IActionResult Create()
        {
            ViewData["PhoneNumber"] = new SelectList(_context.Phones, "PhoneNumber", "PhoneNumber");
            ViewData["UserId"] = new SelectList(_context.AppUsers, "UserId", "FullName");
            return View();
        }

        // POST: Clients/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Afm,PhoneNumber,UserId")] Client client)
        {
            ModelState.Remove(nameof(Client.PhoneNumberNavigation));
            ModelState.Remove(nameof(Client.User));

            if (ModelState.IsValid)
            {
                _context.Add(client);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["PhoneNumber"] = new SelectList(_context.Phones, "PhoneNumber", "PhoneNumber", client.PhoneNumber);
            ViewData["UserId"] = new SelectList(_context.AppUsers, "UserId", "FullName", client.UserId);
            return View(client);
        }

        // GET: Clients/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var client = await _context.Clients.FindAsync(id);
            if (client == null)
            {
                return NotFound();
            }
            ViewData["PhoneNumber"] = new SelectList(_context.Phones, "PhoneNumber", "PhoneNumber", client.PhoneNumber);
            ViewData["UserId"] = new SelectList(_context.AppUsers, "UserId", "FullName", client.UserId);
            ViewData["FullName"] = _context.AppUsers
                .Where(u => u.UserId == client.UserId)
                .Select(u => u.FullName)
                .FirstOrDefault();

            return View(client);
        }

        // POST: Clients/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("ClientId,Afm,PhoneNumber,UserId")] Client client)

        {

            ModelState.Remove(nameof(Client.PhoneNumberNavigation));
            ModelState.Remove(nameof(Client.User));

            if (id != client.ClientId)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(client);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!ClientExists(client.ClientId))
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
            ViewData["PhoneNumber"] = new SelectList(_context.Phones, "PhoneNumber", "PhoneNumber", client.PhoneNumber);
            ViewData["UserId"] = new SelectList(_context.AppUsers, "UserId", "FullName", client.UserId);

            return View(client);
        }

        // GET: Clients/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var client = await _context.Clients
                .Include(c => c.PhoneNumberNavigation)
                .Include(c => c.User)
                .FirstOrDefaultAsync(m => m.ClientId == id);
            if (client == null)
            {
                return NotFound();
            }

            return View(client);
        }

        // POST: Clients/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var client = await _context.Clients.FindAsync(id);
            if (client != null)
            {
                _context.Clients.Remove(client);
            }

            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool ClientExists(int id)
        {
            return _context.Clients.Any(e => e.ClientId == id);
        }


        public async Task<IActionResult> ViewClientBills(int userId)
        {

            // Fetch the client associated with the logged-in user
            var client = await _context.Clients
                .Include(c => c.PhoneNumberNavigation) // Include related Phone data
                .ThenInclude(p => p.Bills) // Include related Bills data
                .FirstOrDefaultAsync(c => c.UserId == userId);

            if (client == null)
            {
                return NotFound("Client not found.");
            }

            // Prepare data for the view
            var clientBills = client.PhoneNumberNavigation.Bills
                .Select(b => new ClientBillViewModel
                {
                    ProgramName = client.PhoneNumberNavigation.ProgramName,
                    Costs = b.Costs
                }).ToList();

            return View(clientBills);
        }


    }
}
