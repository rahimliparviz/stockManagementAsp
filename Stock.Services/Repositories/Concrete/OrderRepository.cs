using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Threading.Tasks;
using AutoMapper;
using Microsoft.EntityFrameworkCore;
using Stock.Data;
using Stock.Domain;
using Stock.Services.DTO;
using Stock.Services.Errors;
using Stock.Services.Repositories.Abstract;

namespace Stock.Services.Repositories.Concrete
{
    public class OrderRepository:IOrderRepository
    {
        private readonly IMapper _mapper;
        private readonly DataContext _context;

        public OrderRepository( IMapper mapper,
            DataContext context)
        {
            _mapper = mapper;
            _context = context;
        }
        public List<OrderDto> GetAll()
        {

            var orders =
                _context.Orders
                    .Include(c => c.Customer)
                    .ToList();
                 
            
            return _mapper.Map<List<Order>, List<OrderDto>>(orders);

        }
        
        public List<OrderDto> GetByDate(DateTime orderDate)
        {
            var orders =
                _context.Orders.Include(c => c.Customer)
                    .Where(o=>o.CreatedAt.Date == orderDate.Date)
                    .ToList();
            
            return _mapper.Map<List<Order>, List<OrderDto>>(orders);
        }

        public OrderDto GetById(Guid id)
        {
            var order = _context.Orders
                .Include(o=>o.OrderProducts)
                .Include(o=>o.Customer)
                .FirstOrDefault(o => o.Id == id);
            
            _ = order ?? throw new RestException(HttpStatusCode.NotFound, new { Order = "Not found" });
            
            var orderDto = _mapper.Map<Order, OrderDto>(order);

            return orderDto;
        }

        public async Task<Response<OrderDto>> Create(CreateOrderDto entityDto)
        {
           var order = new Order()
           {
               CustomerId = Guid.Parse(entityDto.CustomerId),
               Quantity = entityDto.Quantity,
               Price = entityDto.Price,
               PriceWithVat = entityDto.PriceWithVat,
               Pay = entityDto.Pay,
               Due = entityDto.Due,
               PayBy = entityDto.PayBy,
               CreatedAt = DateTime.Now,
               UpdatedAt = DateTime.Now
               
           };

            await _context.Orders.AddAsync(order);

            var productIds = entityDto.orderProducts.Select(p => p.Id).ToList();
            var products = _context.Products.Where(p => productIds.Contains(p.Id)).ToList();
            
            var pr = entityDto.orderProducts.First();

            foreach (var product in entityDto.orderProducts)
           {
               OrderProduct orderProduct = new OrderProduct{OrderId = order.Id,PruductId = product.Id};
               products.First(p => p.Id == product.Id).Quantity = product.Quantity;
                await _context.OrderPruducts.AddAsync(orderProduct);
           }


           try
           {
               var success = await _context.SaveChangesAsync() > 0;

               if (success)
               {

                   return new Response<OrderDto>
                   {
                       Data = null,
                       Message = "Order created",
                       Time = DateTime.Now,
                       IsSuccess = true
                   };

               }
           }
           catch (Exception e)
           {
               return new Response<OrderDto>
               {
                   Data = null,
                   Message = e.InnerException.Message,
                   Time = DateTime.Now,
                   IsSuccess = false
               };
           }

           

           throw new Exception("Problem on saving order");
           
           
           
 
        }

        public async Task<Response<OrderDto>> Update(Guid id, CreateOrderDto entityDto)
        {
            throw new NotImplementedException();
        }

        public Response<OrderDto> Delete(Guid id)
        {
            throw new NotImplementedException();
        }

      
    }
}