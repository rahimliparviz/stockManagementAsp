using System;
using Microsoft.AspNetCore.Mvc;
using Stock.Services.DTO;
using Stock.Services.Repositories.Abstract;

namespace Stock.Api.Controllers
{
    public class SuppliersController : BaseController
    {
        private readonly ISupplierRepository _repo;

        public SuppliersController(ISupplierRepository repo)
        {
            _repo = repo;
        }


        [HttpGet]
        public ActionResult Get()
        {
            var suppliers = _repo.GetAll();
            return Ok(suppliers);
        }

        [HttpPost]
        public ActionResult Create([FromForm]CreateSupplierDto supplierDto)
        {
            var result = _repo.Create(supplierDto);
            return Ok(result);
        }
        
        [HttpGet("{id}")]
        public ActionResult Get(Guid id)
        {
            var supplier = _repo.GetById(id);
            return Ok(supplier);
        }
        
        [HttpPut("{id}")]
        public ActionResult Edit(Guid id,[FromForm]CreateSupplierDto supplierDto)
        {
            var result = _repo.Update(id,supplierDto);
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