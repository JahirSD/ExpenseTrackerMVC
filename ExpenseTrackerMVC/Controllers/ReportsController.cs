using ExpenseTrackerMVC.Data;
using ExpenseTrackerMVC.Models.ViewModels;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace ExpenseTrackerMVC.Controllers
{
    public class ReportsController : Controller
    {
        private readonly ApplicationDbContext _context;

        public ReportsController(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<IActionResult> Index()
        {
            var model = new ReportViewModel
            {
                Employees = await _context.Employees.ToListAsync(),

                Dates = await _context.DailyEntries
                    .Select(x => x.Date.Date)
                    .Distinct()
                    .OrderBy(x => x)
                    .ToListAsync(),

                Entries = await _context.DailyEntries
                    .Include(x => x.Employee)
                    .ToListAsync()
            };

            return View(model);
        }

        public async Task<IActionResult> Monthly()
        {
            var employees = await _context.Employees.ToListAsync();

            var expenseTypes = await _context.ExpenseTypes.ToListAsync();

            var employeeExpenses = await _context.EmployeeExpenses
                .Include(x => x.Employee)
                .Include(x => x.ExpenseType)
                .ToListAsync();

            ViewBag.Employees = employees;

            ViewBag.ExpenseTypes = expenseTypes;

            return View(employeeExpenses);
        }
    }
}