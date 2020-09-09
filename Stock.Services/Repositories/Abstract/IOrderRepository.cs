using System;
using System.Collections.Generic;
using Stock.Services.DTO;

namespace Stock.Services.Repositories.Abstract
{
    public interface IOrderRepository:IRepository<CreateOrderDto,OrderDto>
    {
          List<OrderDto>  GetByDate (DateTime orderDate);
    }

  
}