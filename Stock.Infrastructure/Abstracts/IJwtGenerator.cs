using Stock.Domain;

namespace Stock.Infrastructure.Abstracts
{
    public interface IJwtGenerator
    {
        string CreateToken(User user);
    }
}