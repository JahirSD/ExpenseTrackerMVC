using ExpenseTrackerMVC.Models;

namespace ExpenseTrackerMVC.Models.ViewModels
{
    public class ReportViewModel
    {
        public List<Employee> Employees { get; set; }

        public List<DateTime> Dates { get; set; }

        public List<DailyEntry> Entries { get; set; }
    }
}