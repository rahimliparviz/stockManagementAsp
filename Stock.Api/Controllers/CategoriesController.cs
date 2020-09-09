using System;
using Microsoft.AspNetCore.Mvc;
using Stock.Services.DTO;
using Stock.Services.Repositories.Abstract;

namespace Stock.Api.Controllers
{
    public class CategoriesController:BaseController
    {
        private readonly ICategoryRepository _repo;

        public CategoriesController(ICategoryRepository repo)
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
        public ActionResult Create([FromForm]CategoryDto categoryDto)
        {
            var result = _repo.Create(categoryDto);
            return Ok(result);
        }
        
        [HttpGet("{id}")]
        public ActionResult Get(Guid id)
        {
            var category = _repo.GetById(id);
            return Ok(category);
        }
        
        [HttpPut("{id}")]
        public ActionResult Edit(Guid id,[FromForm]CategoryDto categoryDto)
        {
            var result = _repo.Update(id,categoryDto);
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