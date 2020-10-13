using System;
using System.Collections.Generic;
using Stock.Domain;

namespace Stock.Services.DTO
{
    public class OrderDto
    {       
        public string Id { get; set; }
        public string CustomerName { get; set; }
        public string CustomerAddress { get; set; }
        public string CustomerPhone { get; set; }
        public int Quantity { get; set; }
        public double Price { get; set; }
        public double PriceWithVat { get; set; }
        public double Pay { get; set; }
        public double Due { get; set; }
        public string PayBy { get; set; }
        public DateTime CreatedAt { get; set; }

        public ICollection<OrderProduct> OrderProducts { get; set; }

    }
}