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
    public class CallsController : Controller
    {
        private readonly BillingDbContext _context;

        public CallsController(BillingDbContext context)
        {
            _context = context;
        }

        // GET: Calls
        public async Task<IActionResult> Index()
        {
            return View(await _context.Calls.ToListAsync());
        }

        // GET: Calls/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var call = await _context.Calls
                .FirstOrDefaultAsync(m => m.CallId == id);
            if (call == null)
            {
                return NotFound();
            }

            return View(call);
        }

        // GET: Calls/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: Calls/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("CallId,Description")] Call call)
        {
            if (ModelState.IsValid)
            {
                _context.Add(call);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(call);
        }

        // GET: Calls/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var call = await _context.Calls.FindAsync(id);
            if (call == null)
            {
                return NotFound();
            }
            return View(call);
        }

        // POST: Calls/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("CallId,Description")] Call call)
        {
            if (id != call.CallId)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(call);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!CallExists(call.CallId))
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
            return View(call);
        }

        // GET: Calls/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var call = await _context.Calls
                .FirstOrDefaultAsync(m => m.CallId == id);
            if (call == null)
            {
                return NotFound();
            }

            return View(call);
        }

        // POST: Calls/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var call = await _context.Calls.FindAsync(id);
            if (call != null)
            {
                _context.Calls.Remove(call);
            }

            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool CallExists(int id)
        {
            return _context.Calls.Any(e => e.CallId == id);
        }


        public IActionResult CallHistory(string phoneNumber, int? page)
        {
            if (string.IsNullOrEmpty(phoneNumber))
            {
                return NotFound("Phone number is required.");
            }

            // Store the phone number in ViewData to persist it during pagination
            ViewData["PhoneNumber"] = phoneNumber;

            // Fetch only the descriptions for the given phone number
            var callDescriptionsQuery = _context.BillsCalls
                .Include(bc => bc.Call)
                .Include(bc => bc.Bill)
                .ThenInclude(b => b.PhoneNumberNavigation)
                .Where(bc => bc.Bill.PhoneNumber == phoneNumber)
                .Select(bc => bc.Call.Description)
                .AsQueryable();

            // Pagination logic
            int pageSize = 10; // Set the number of items per page
            int pageNumber = (page ?? 1); // Default to page 1 if the page parameter is null

            // Get the paginated result
            var paginatedCallDescriptions = callDescriptionsQuery.ToPagedList(pageNumber, pageSize);

            if (!paginatedCallDescriptions.Any())
            {
                return NotFound("No call history found for this phone number.");
            }

            var client =  _context.Clients
                .Where(c => c.PhoneNumber == phoneNumber)
                .FirstOrDefaultAsync();


            return View(paginatedCallDescriptions);
        }




    }
}
