using System;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Stock.Services.DTO;
using Stock.Services.Repositories.Abstract;

namespace Stock.Api.Controllers
{

    public class CustomersController : BaseController
    {
        private readonly ICustomerRepository _repo;

        public CustomersController(ICustomerRepository repo)
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
        public ActionResult Create([FromForm]CreateCustomerDto customerDto)
        {
            var result = _repo.Create(customerDto);
            return Ok(result);
        }
        
        [HttpGet("{id}")]
        public ActionResult Get(Guid id)
        {
            var category = _repo.GetById(id);
            return Ok(category);
        }
        
        [HttpPut("{id}")]
        public ActionResult Edit(Guid id,[FromForm]CreateCustomerDto customerDto)
        {
            var result = _repo.Update(id,customerDto);
            return Ok(result);
        }
        
        [HttpDelete("{id}")]
        public ActionResult Delete(Guid id)
        {
            var result = _repo.Delete(id);
            return Ok(result);
        }
    }
}