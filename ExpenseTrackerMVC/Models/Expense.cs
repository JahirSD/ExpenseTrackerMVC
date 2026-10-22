using System.ComponentModel.DataAnnotations;

namespace ExpenseTrackerMVC.Models
{
    public class Expense
    {
        public int Id { get; set; }

        [Required]
        public string Title { get; set; }

        [Required]
        public decimal Amount { get; set; }
        [Required]
        public string Description { get; set; }
        [Required]
        public DateTime ExpenseDate { get; set; }
    }
}