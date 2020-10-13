using System;
using Stock.Domain;

namespace Stock.Services.DTO
{
    public class SalaryDto
    {
        public string Id { get; set; }
        public string EmployeeId { get; set; }
        public string EmployeeName { get; set; }
        public string EmployeeEmail { get; set; }
        public double? Amount { get; set; }
        public DateTime SalaryDate { get; set; }
        public int Month { get; set; }
        public string MonthName { get; set; }
        public string Year { get; set; }
        public bool IsUpdating { get; set; } = false;

    }
}