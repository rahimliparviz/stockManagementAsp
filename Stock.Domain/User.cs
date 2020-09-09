using Microsoft.AspNetCore.Identity;

namespace Stock.Domain
{
    public class User:IdentityUser
    {
        public string UserType { get; set; }
    }
}