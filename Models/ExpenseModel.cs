using Microsoft.EntityFrameworkCore.Metadata.Internal;
using System.ComponentModel.DataAnnotations.Schema;

namespace HomeBookkeeping.Models
{
    public class ExpenseModel
    {
        public int ID { get; set; }
        public DateTime Date { get; set; }
        public int CategoryID { get; set; }
        [Column(TypeName = "decimal(18, 2)")]
        public decimal Amount { get; set; }
        public string Comment { get; set; }
    }
}
