namespace HomeBookkeeping.Models
{
    public class ExpenseReportParameters
    {
        public DateTime StartDate { get; set; }
        public DateTime EndDate { get; set; }
        public string[] Category { get; set; }
    }
}
