using System;
using Stock.Services.DTO;

namespace Stock.Services.Repositories.Abstract
{
    public interface IProductRepository:IRepository<CreateProductDto,ProductDto>
    {
        Response<ProductDto> StockUpdate(StockProductQuantityDto quantity, Guid productId);
    }
}