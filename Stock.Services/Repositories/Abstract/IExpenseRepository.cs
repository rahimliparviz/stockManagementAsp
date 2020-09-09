using Stock.Domain;
using Stock.Services.DTO;

namespace Stock.Services.Repositories.Abstract
{
    public interface IExpenseRepository:IRepository<ExpenseDto,ExpenseDto>
    {}

   
}