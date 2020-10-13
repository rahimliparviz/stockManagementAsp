using System;
using Stock.Domain;

namespace Stock.Services.DTO
{
    public class EmployeeDto
    {
        public string Id { get; set; }
        public string UserId { get; set; }
        public string Address { get; set; }
        public double Salary { get; set; }
        public string Photo { get; set; }
        public DateTime JoiningDate { get; set; }

        public string Phone { get; set; }
        public string Email { get; set; }
        public string UserName { get; set; }
   
    }
}