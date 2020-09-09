using System;
using Microsoft.AspNetCore.Http;

namespace Stock.Services.DTO
{
    public class CreateProductDto
    {
        public string CategoryId { get; set; }
        public string  Name { get; set; }
        public string  Code { get; set; }
        public string  Root { get; set; }
        public string  BuyingPrice { get; set; }
        public string  SelingPrice { get; set; }
        public string SupplierId { get; set; }
        public DateTime BuyingDate { get; set; }
        public IFormFile Photo { get; set; }
        public int Quantity { get; set; }
    }
}