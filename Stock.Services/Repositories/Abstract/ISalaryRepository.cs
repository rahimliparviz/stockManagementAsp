using System;
using System.Collections.Generic;
using Stock.Services.DTO;

namespace Stock.Services.Repositories.Abstract
{
    public interface ISalaryRepository:IRepository<SalaryDto,SalaryDto>
    {
        List<SalaryDto> ShowByMonth(int month);

    }
}