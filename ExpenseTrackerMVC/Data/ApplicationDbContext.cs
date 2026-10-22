using ExpenseTrackerMVC.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace ExpenseTrackerMVC.Data
{
    public class ApplicationDbContext
        : IdentityDbContext<IdentityUser, IdentityRole, string>
    {
        public ApplicationDbContext(
            DbContextOptions<ApplicationDbContext> options)
            : base(options)
        {
        }

        public DbSet<Expense> Expenses { get; set; }
        public DbSet<Employee> Employees { get; set; }

        public DbSet<DailyEntry> DailyEntries { get; set; }

        public DbSet<ExpenseType> ExpenseTypes { get; set; }

        public DbSet<EmployeeExpense> EmployeeExpenses { get; set; }
    }
}