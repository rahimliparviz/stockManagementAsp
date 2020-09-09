using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
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
                _context.Orders.Include(c => c.Customer)
                    .ToList();
                 
            
            return _mapper.Map<List<Order>, List<OrderDto>>(orders);

        }
        
        public List<OrderDto> GetByDate(DateTime orderDate)
        {
            var orders =
                _context.Orders.Include(c => c.Customer)
                    .Where(o=>o.CreatedAt == orderDate)
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

        public Response<OrderDto> Create(CreateOrderDto entityDto)
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
               OrderProducts = entityDto.OrderProducts,
               CreatedAt = DateTime.Now,
               UpdatedAt = DateTime.Now
               
           };

           var productIds = entityDto.OrderProducts.Select(p => p.PruductId).ToList();
           var productsForDecreaseQuantity = _context.Products.Where(p => productIds.Contains(p.Id)).ToList();


           foreach (var product in productsForDecreaseQuantity)
           {
               product.Quantity -= 1;
           }

           _context.SaveChanges();
           
           var orderDto = _mapper.Map<Order, OrderDto>(order);

           return new Response<OrderDto>
           {
               Data = orderDto,
               Message = "Order created",
               Time = DateTime.Now,
               IsSuccess = true
           };
 
        }

        public Response<OrderDto> Update(Guid id, CreateOrderDto entityDto)
        {
            throw new NotImplementedException();
        }

        public Response<OrderDto> Delete(Guid id)
        {
            throw new NotImplementedException();
        }

      
    }
}