using System;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Stock.Services.DTO;
using Stock.Services.Repositories.Abstract;

namespace Stock.Api.Controllers
{
    public class OrdersController : BaseController
    {
        private readonly IOrderRepository _repo;

        public OrdersController(IOrderRepository repo)
        {
            _repo = repo;
        }
      
        [HttpGet]
        public ActionResult Get()
        {
            var orders = _repo.GetAll();
            return Ok(orders);
        }

        
        [HttpGet("date/{date}")]
        public ActionResult GetByDate(DateTime date)
        {
            var orders = _repo.GetByDate(date);
            return Ok(orders);
        }
        
        [HttpGet("{id}")]
        public ActionResult Get(Guid id)
        {
            var order = _repo.GetById(id);
            return Ok(order);
        }
        
        [HttpPost]
        public async Task<ActionResult> Create([FromBody]CreateOrderDto orderDto)
        {
            var order =await _repo.Create(orderDto);
            return Ok(order);
        }
    }
}