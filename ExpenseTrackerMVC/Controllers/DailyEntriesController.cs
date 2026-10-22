using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using ExpenseTrackerMVC.Data;
using ExpenseTrackerMVC.Models;

namespace ExpenseTrackerMVC.Controllers
{
    public class DailyEntriesController : Controller
    {
        private readonly ApplicationDbContext _context;

        public DailyEntriesController(ApplicationDbContext context)
        {
            _context = context;
        }

        // GET: DailyEntries
        public async Task<IActionResult> Index()
        {
            var applicationDbContext = _context.DailyEntries.Include(d => d.Employee);
            return View(await applicationDbContext.ToListAsync());
        }

        // GET: DailyEntries/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var dailyEntry = await _context.DailyEntries
                .Include(d => d.Employee)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (dailyEntry == null)
            {
                return NotFound();
            }

            return View(dailyEntry);
        }

        // GET: DailyEntries/Create
        public IActionResult Create()
        {
            ViewData["EmployeeId"] = new SelectList(_context.Employees, "Id", "Name");
            return View();
        }

        // POST: DailyEntries/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,Date,Value,EmployeeId")] DailyEntry dailyEntry)
        {
            if (!ModelState.IsValid)
            {
                foreach (var error in ModelState.Values.SelectMany(v => v.Errors))
                {
                    Console.WriteLine(error.ErrorMessage);
                }
            }

            if (ModelState.IsValid)
            {
                _context.Add(dailyEntry);

                await _context.SaveChangesAsync();

                return RedirectToAction(nameof(Index));
            }

            ViewData["EmployeeId"] =
                new SelectList(_context.Employees, "Id", "Name", dailyEntry.EmployeeId);

            return View(dailyEntry);
        }
        // GET: DailyEntries/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var dailyEntry = await _context.DailyEntries.FindAsync(id);
            if (dailyEntry == null)
            {
                return NotFound();
            }
            ViewData["EmployeeId"] = new SelectList(_context.Employees, "Id", "Name", dailyEntry.EmployeeId);
            return View(dailyEntry);
        }

        // POST: DailyEntries/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,Date,Value,EmployeeId")] DailyEntry dailyEntry)
        {
            if (id != dailyEntry.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(dailyEntry);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!DailyEntryExists(dailyEntry.Id))
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
            ViewData["EmployeeId"] = new SelectList(_context.Employees, "Id", "Name", dailyEntry.EmployeeId);
            return View(dailyEntry);
        }

        // GET: DailyEntries/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var dailyEntry = await _context.DailyEntries
                .Include(d => d.Employee)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (dailyEntry == null)
            {
                return NotFound();
            }

            return View(dailyEntry);
        }

        // POST: DailyEntries/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var dailyEntry = await _context.DailyEntries.FindAsync(id);
            if (dailyEntry != null)
            {
                _context.DailyEntries.Remove(dailyEntry);
            }

            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool DailyEntryExists(int id)
        {
            return _context.DailyEntries.Any(e => e.Id == id);
        }
    }
}
