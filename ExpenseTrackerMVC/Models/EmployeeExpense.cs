namespace ExpenseTrackerMVC.Models
{
    public class EmployeeExpense
    {
        public int Id { get; set; }

        public int EmployeeId { get; set; }

        public Employee? Employee { get; set; }

        public int ExpenseTypeId { get; set; }

        public ExpenseType? ExpenseType { get; set; }

        public decimal Amount { get; set; }

        public DateTime Date { get; set; }
    }
}
