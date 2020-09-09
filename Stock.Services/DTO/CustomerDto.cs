﻿using Microsoft.AspNetCore.Http;

namespace Stock.Services.DTO
{
    public class CustomerDto
    {
        public string Name { get; set; }
        public string Email { get; set; }
        public string Phone { get; set; }
        public string Address { get; set; }
        public string Photo { get; set; }
    }
}