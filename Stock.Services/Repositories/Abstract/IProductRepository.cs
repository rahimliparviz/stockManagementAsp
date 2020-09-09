using System;
using Stock.Services.DTO;

namespace Stock.Services.Repositories.Abstract
{
    public interface IProductRepository:IRepository<CreateProductDto,ProductDto>
    {
        void StockUpdate(int quantity, Guid productId);
    }
}