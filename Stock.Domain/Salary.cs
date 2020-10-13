using System;

namespace Stock.Domain
{
    public class Salary:BaseEntity
    {
        public Employee Employee { get; set; }
        public Guid EmployeeId { get; set; }

        public double? Amount { get; set; }
        public DateTime SalaryDate { get; set; }
        public int Month { get; set; }
        public string Year { get; set; }


    }
}