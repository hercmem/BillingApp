using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using BillingApp.Models;

namespace BillingApp.Controllers
{
    public class BillsController : Controller
    {
        private readonly BillingDbContext _context;

        public BillsController(BillingDbContext context)
        {
            _context = context;
        }

        // GET: Bills
        public async Task<IActionResult> Index()
        {
            var billingDbContext = _context.Bills.Include(b => b.PhoneNumberNavigation);
            return View(await billingDbContext.ToListAsync());
        }

        // GET: Bills/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var bill = await _context.Bills
                .Include(b => b.PhoneNumberNavigation)
                .FirstOrDefaultAsync(m => m.BillId == id);
            if (bill == null)
            {
                return NotFound();
            }

            return View(bill);
        }

        // GET: Bills/Create
        public IActionResult Create()
        {
            ViewData["PhoneNumber"] = new SelectList(_context.Phones, "PhoneNumber", "PhoneNumber");
            return View();
        }

        // POST: Bills/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("BillId,PhoneNumber,Costs")] Bill bill)
        {
            if (ModelState.IsValid)
            {
                _context.Add(bill);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["PhoneNumber"] = new SelectList(_context.Phones, "PhoneNumber", "PhoneNumber", bill.PhoneNumber);
            return View(bill);
        }

        // GET: Bills/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var bill = await _context.Bills.FindAsync(id);
            if (bill == null)
            {
                return NotFound();
            }
            ViewData["PhoneNumber"] = new SelectList(_context.Phones, "PhoneNumber", "PhoneNumber", bill.PhoneNumber);
            return View(bill);
        }

        // POST: Bills/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("BillId,PhoneNumber,Costs")] Bill bill)
        {
            if (id != bill.BillId)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(bill);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!BillExists(bill.BillId))
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
            ViewData["PhoneNumber"] = new SelectList(_context.Phones, "PhoneNumber", "PhoneNumber", bill.PhoneNumber);
            return View(bill);
        }

        // GET: Bills/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var bill = await _context.Bills
                .Include(b => b.PhoneNumberNavigation)
                .FirstOrDefaultAsync(m => m.BillId == id);
            if (bill == null)
            {
                return NotFound();
            }

            return View(bill);
        }

        // POST: Bills/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var bill = await _context.Bills.FindAsync(id);
            if (bill != null)
            {
                _context.Bills.Remove(bill);
            }

            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool BillExists(int id)
        {
            return _context.Bills.Any(e => e.BillId == id);
        }


        [HttpGet]
        public async Task<IActionResult> IssueBill(string phoneNumber)
        {
            var phone = await _context.Phones
                .Include(p => p.ProgramNameNavigation) // Include the related PhoneProgram
                .FirstOrDefaultAsync(p => p.PhoneNumber == phoneNumber);

            if (phone == null || phone.ProgramNameNavigation == null)
            {
                return NotFound("Phone number or associated program not found.");
            }

            var model = new IssueBillViewModel
            {
                PhoneNumber = phone.PhoneNumber,
                ProgramName = phone.ProgramNameNavigation.ProgramName, // Retrieve Program Name
                IsPaid = false // Default to unpaid
            };

            return View(model);
        }

        [HttpPost]
        public async Task<IActionResult> IssueBill(IssueBillViewModel model)
        {
            if (ModelState.IsValid)
            {
                var newBill = new Bill
                {
                    PhoneNumber = model.PhoneNumber,
                    Costs = model.Cost, // Ensure correct property mapping
                    IsPaid = model.IsPaid
                };

                _context.Bills.Add(newBill);
                await _context.SaveChangesAsync();

                return RedirectToAction("ClientList", "Sellers"); // Redirect back to client list
            }

            return View(model);
        }



    }
}
