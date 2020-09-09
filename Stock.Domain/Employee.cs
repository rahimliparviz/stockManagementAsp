using System;

namespace Stock.Domain
{
    public class Employee:BaseEntity
    {
        public string Address { get; set; }
        public double Salary { get; set; }
        public string Photo { get; set; }
        public DateTime JoiningDate { get; set; }
        public User User { get; set; }
        public string UserId { get; set; }
    }
}