using Stock.Domain;
using Stock.Services.DTO;

namespace Stock.Services.Repositories.Abstract
{
    public interface ICategoryRepository:IRepository<CategoryDto,CategoryDto>
    {}
}