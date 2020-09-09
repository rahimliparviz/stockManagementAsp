namespace Stock.Domain
{
    public class Regulation:BaseEntity
    {
        public double Vat { get; set; }
        
        public string Email { get; set; }
        public string Phone { get; set; }
        public string Address { get; set; }
    }
}