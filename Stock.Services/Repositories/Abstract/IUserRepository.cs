using System.Threading.Tasks;
using Stock.Services.DTO;

namespace Stock.Services.Repositories.Abstract
{
    public interface IUserRepository
    {
        Task<UserDto> Login(LoginDto loginDto);
    }

 
}