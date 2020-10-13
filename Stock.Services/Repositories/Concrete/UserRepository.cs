using System.Linq;
using System.Net;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Stock.Data;
using Stock.Domain;
using Stock.Infrastructure.Abstracts;
using Stock.Services.DTO;
using Stock.Services.Errors;
using Stock.Services.Repositories.Abstract;

namespace Stock.Services.Repositories.Concrete
{
    public class UserRepository:IUserRepository
    {
        private readonly UserManager<User> _userManager;
        private readonly SignInManager<User> _signInManager;
        private readonly IJwtGenerator _jwtGenerator;
        private readonly DataContext _context;

        public UserRepository(
            UserManager<User> userManager,
            SignInManager<User> signInManager,
            IJwtGenerator jwtGenerator,
            DataContext context
            )
        {
            _userManager = userManager;
            _signInManager = signInManager;
            _jwtGenerator = jwtGenerator;
            _context = context;
        }
        public async Task<UserDto> Login(LoginDto loginDto)
        {
            
            var user = await _userManager.FindByEmailAsync(loginDto.Email);

            var userImage = _context.Employees
                .FirstOrDefault(e => e.UserId == user.Id)
                ?.Photo;
            
            if (user == null)
                throw new RestException(HttpStatusCode.Unauthorized);
            
            var result = await _signInManager
                .CheckPasswordSignInAsync(user, loginDto.Password, false);
            
            if (result.Succeeded)
            {
                // TODO: generate token
                return new UserDto
                {
                    DisplayName = user.UserName,
                    Token = _jwtGenerator.CreateToken(user),
                    UserType = user.UserType,
                    Username = user.UserName,
                    // Image = user.Photos.FirstOrDefault(predicate => predicate.IsMain == true)?.Url
                    Image = userImage
                };
            }
            
            throw new RestException(HttpStatusCode.Unauthorized);
        }
    }
}