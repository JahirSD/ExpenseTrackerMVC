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
    public class EmployeeExpensesController : Controller
    {
        private readonly ApplicationDbContext _context;

        public EmployeeExpensesController(ApplicationDbContext context)
        {
            _context = context;
        }

        // GET: EmployeeExpenses
        public IActionResult Test()
        {
            return Content("Employee Expense Working");
        }
        //public async Task<IActionResult> Index()
        //{
        //    var applicationDbContext = _context.EmployeeExpenses.Include(e => e.Employee).Include(e => e.ExpenseType);
        //    return View(await applicationDbContext.ToListAsync());
        //}

        public async Task<IActionResult> Index()
        {
            var data = _context.EmployeeExpenses
                .Include(e => e.Employee)
                .Include(e => e.ExpenseType);

            return View(await data.ToListAsync());
        }

        // GET: EmployeeExpenses/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var employeeExpense = await _context.EmployeeExpenses
                .Include(e => e.Employee)
                .Include(e => e.ExpenseType)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (employeeExpense == null)
            {
                return NotFound();
            }

            return View(employeeExpense);
        }

        // GET: EmployeeExpenses/Create
        public IActionResult Create()
        {
            ViewData["EmployeeId"] = new SelectList(_context.Employees, "Id", "Name");
            ViewData["ExpenseTypeId"] = new SelectList(_context.ExpenseTypes, "Id", "Name");
            return View();
        }

        // POST: EmployeeExpenses/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,EmployeeId,ExpenseTypeId,Amount,Date")] EmployeeExpense employeeExpense)
        {
            if (ModelState.IsValid)
            {
                _context.Add(employeeExpense);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["EmployeeId"] = new SelectList(_context.Employees, "Id", "Name", employeeExpense.EmployeeId);
            ViewData["ExpenseTypeId"] = new SelectList(_context.ExpenseTypes, "Id", "Name", employeeExpense.ExpenseTypeId);
            return View(employeeExpense);
        }

        // GET: EmployeeExpenses/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var employeeExpense = await _context.EmployeeExpenses.FindAsync(id);
            if (employeeExpense == null)
            {
                return NotFound();
            }
            ViewData["EmployeeId"] = new SelectList(_context.Employees, "Id", "Name", employeeExpense.EmployeeId);
            ViewData["ExpenseTypeId"] = new SelectList(_context.ExpenseTypes, "Id", "Name", employeeExpense.ExpenseTypeId);
            return View(employeeExpense);
        }

        // POST: EmployeeExpenses/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,EmployeeId,ExpenseTypeId,Amount,Date")] EmployeeExpense employeeExpense)
        {
            if (id != employeeExpense.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(employeeExpense);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!EmployeeExpenseExists(employeeExpense.Id))
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
            ViewData["EmployeeId"] = new SelectList(_context.Employees, "Id", "Name", employeeExpense.EmployeeId);
            ViewData["ExpenseTypeId"] = new SelectList(_context.ExpenseTypes, "Id", "Name", employeeExpense.ExpenseTypeId);
            return View(employeeExpense);
        }

        // GET: EmployeeExpenses/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var employeeExpense = await _context.EmployeeExpenses
                .Include(e => e.Employee)
                .Include(e => e.ExpenseType)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (employeeExpense == null)
            {
                return NotFound();
            }

            return View(employeeExpense);
        }

        // POST: EmployeeExpenses/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var employeeExpense = await _context.EmployeeExpenses.FindAsync(id);
            if (employeeExpense != null)
            {
                _context.EmployeeExpenses.Remove(employeeExpense);
            }

            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool EmployeeExpenseExists(int id)
        {
            return _context.EmployeeExpenses.Any(e => e.Id == id);
        }
    }
}
