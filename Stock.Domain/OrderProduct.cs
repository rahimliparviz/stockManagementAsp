using System;

namespace Stock.Domain
{
    public class OrderProduct
    {   
        
        public Guid OrderId { get; set; }
        public Order Order { get; set; }
        public Guid PruductId { get; set; }
        public Product Pruduct { get; set; }
    
    }
}