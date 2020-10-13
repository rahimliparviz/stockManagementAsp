using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Stock.Services.Repositories.Abstract
{
    public interface IRepository<TEntityDto,TReturnEntityDto>
    {
        List<TReturnEntityDto> GetAll();
        TReturnEntityDto GetById(Guid id);
   
        Task<Response<TReturnEntityDto>> Create(TEntityDto entityDto);
        Task<Response<TReturnEntityDto>> Update(Guid id,TEntityDto entityDto);
        Response<TReturnEntityDto> Delete(Guid id);  
    }
}