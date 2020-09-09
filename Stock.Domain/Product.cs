using System;
using System.Collections.Generic;

namespace Stock.Domain
{
    public class Product:BaseEntity
    {
        public Guid CategoryId { get; set; }
        public Category Category { get; set; }
        public string  Name { get; set; }
        public string  Code { get; set; }
        public string  Root { get; set; }
        public string  BuyingPrice { get; set; }
        public string  SelingPrice { get; set; }
        public Guid SupplierId { get; set; }
        public Supplier Supplier { get; set; }
        public DateTime BuyingDate { get; set; }
        public string Photo { get; set; }
        public int Quantity { get; set; }
        public ICollection<OrderProduct> OrderProducts { get; set; }


    }
}