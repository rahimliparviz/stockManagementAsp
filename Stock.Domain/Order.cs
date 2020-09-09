using System;
using System.Collections.Generic;

namespace Stock.Domain
{
    public class Order:BaseEntity
    {
        public Guid CustomerId { get; set; }
        public Customer Customer { get; set; }
        public int Quantity { get; set; }
        public double Price { get; set; }
        public double PriceWithVat { get; set; }
        public double Pay { get; set; }
        public double Due { get; set; }
        public string PayBy { get; set; }
        
        public ICollection<OrderProduct> OrderProducts { get; set; }

    }
}