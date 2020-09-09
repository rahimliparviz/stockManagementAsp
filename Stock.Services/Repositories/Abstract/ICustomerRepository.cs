using Stock.Services.DTO;

namespace Stock.Services.Repositories.Abstract
{
    public interface ICustomerRepository : IRepository<CreateCustomerDto, CustomerDto>
    {
        
    }
}