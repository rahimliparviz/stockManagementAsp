using Microsoft.AspNetCore.Http;

namespace Stock.Services.DTO
{
    public class CreateSupplierDto
    {
        public string Name { get; set; }
        public string Email { get; set; }
        public string Phone { get; set; }
        public string Address { get; set; }
        public string ShopName { get; set; }
        public IFormFile Photo { get; set; }
        
    }
}