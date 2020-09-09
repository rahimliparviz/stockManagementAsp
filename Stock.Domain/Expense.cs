namespace Stock.Domain
{
    public class Expense:BaseEntity
    {
        public string Details { get; set; }
        public double  Amount { get; set; }
    }
}