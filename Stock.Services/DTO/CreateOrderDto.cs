using System.Collections.Generic;
using Stock.Domain;

namespace Stock.Services.DTO
{
    public class CreateOrderDto
    {
        public string CustomerId { get; set; }
        public int Quantity { get; set; }
        public double Price { get; set; }
        public double PriceWithVat { get; set; }
        public double Pay { get; set; }
        public double Due { get; set; }
        public string PayBy { get; set; }
        
        public ICollection<Product> orderProducts { get; set; }

    }
}