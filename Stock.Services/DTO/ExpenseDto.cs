using System;

namespace Stock.Services.DTO
{
    public class ExpenseDto
    {
        public string Id { get; set; }

        public string Details { get; set; }
        public double  Amount { get; set; }
        public DateTime  CreatedAt { get; set; }
    }
}