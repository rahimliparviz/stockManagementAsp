using System;
using System.Collections.Generic;

namespace Stock.Services.Repositories.Abstract
{
    public interface IRepository<TEntityDto,TReturnEntityDto>
    {
        List<TReturnEntityDto> GetAll();
        TReturnEntityDto GetById(Guid id);
   
        Response<TReturnEntityDto> Create(TEntityDto entityDto);
        Response<TReturnEntityDto> Update(Guid id,TEntityDto entityDto);
        Response<TReturnEntityDto> Delete(Guid id);  
    }
}