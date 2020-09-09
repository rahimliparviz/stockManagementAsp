using System;
using Microsoft.AspNetCore.Http;

namespace Stock.Services.DTO
{
    public class CreateEmployeeDto 
    {
        public string Address { get; set; }
        public double Salary { get; set; }
        public IFormFile Photo { get; set; }
        public DateTime JoiningDate { get; set; }

        public string Phone { get; set; }
        public string Email { get; set; }
        public string Name { get; set; }
    }
}