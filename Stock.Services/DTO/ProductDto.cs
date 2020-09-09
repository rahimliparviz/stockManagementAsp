using System;
using Microsoft.AspNetCore.Http;

namespace Stock.Services.DTO
{
    public class ProductDto
    {
        public string CategoryId { get; set; }
        public string CategoryName { get; set; }
        public string  Name { get; set; }
        public string  Code { get; set; }
        public string  Root { get; set; }
        public string  BuyingPrice { get; set; }
        public string  SelingPrice { get; set; }
        public string SupplierId { get; set; }
        public string SupplierName { get; set; }
        public DateTime BuyingDate { get; set; }
        public string Photo { get; set; }
        public int Quantity { get; set; }
    }
}