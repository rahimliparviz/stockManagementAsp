using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Stock.Domain;
using Stock.Services.DTO;
using Stock.Services.Repositories.Abstract;

namespace Stock.Api.Controllers
{
    public class AuthController : BaseController
    {
        private readonly IUserRepository _repo;

        public AuthController(IUserRepository repo)
        {
            _repo = repo;
        }
        [AllowAnonymous]
        [HttpPost("login")]
        public async Task<ActionResult<UserDto>> Login(LoginDto loginDto)
        {
            return await _repo.Login(loginDto);
        }
    }
}