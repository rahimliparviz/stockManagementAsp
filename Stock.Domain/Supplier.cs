namespace Stock.Domain
{
    public class Supplier:BaseEntity
    {
        public string Name { get; set; }
        public string Email { get; set; }
        public string Phone { get; set; }
        public string Address { get; set; }
        public string Photo { get; set; }
        public string ShopName { get; set; }
    }
}