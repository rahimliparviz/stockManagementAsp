using System;
using Microsoft.AspNetCore.Mvc;
using Stock.Services.DTO;
using Stock.Services.Repositories.Abstract;

namespace Stock.Api.Controllers
{
    public class ProductsController : BaseController
    {
        private readonly IProductRepository _repo;

        public ProductsController(IProductRepository repo)
        {
            _repo = repo;
        }


        [HttpGet]
        public ActionResult Get()
        {
            var categories = _repo.GetAll();
            return Ok(categories);
        }

        [HttpPost]
        public ActionResult Create([FromForm]CreateProductDto productDto)
        {
            var result = _repo.Create(productDto);
            return Ok(result);
        }
        
        [HttpGet("{id}")]
        public ActionResult Get(Guid id)
        {
            var category = _repo.GetById(id);
            return Ok(category);
        }
        
        [HttpPut("{id}")]
        public ActionResult Edit(Guid id,[FromForm]CreateProductDto productDto)
        {
            var result = _repo.Update(id,productDto);
            return Ok(result);
        }
        
        [HttpDelete("{id}")]
        public ActionResult Delete(Guid id)
        {
            var result = _repo.Delete(id);
            return Ok(result);
        }
        
        [HttpPost("stock-update")]
        public ActionResult StockUpdate(int quantity,Guid productId)
        {
             _repo.StockUpdate(quantity,productId);
            
            return Ok();
        }
    }
}