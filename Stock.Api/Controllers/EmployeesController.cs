using System;
using Microsoft.AspNetCore.Mvc;
using Stock.Services.DTO;
using Stock.Services.Repositories.Abstract;

namespace Stock.Api.Controllers
{
    public class EmployeesController : BaseController
    {
        private readonly IEmployeeRepository _repo;

        public EmployeesController(IEmployeeRepository repo)
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
        public ActionResult Create([FromForm]CreateEmployeeDto employeeDto)
        {
            var result = _repo.Create(employeeDto);
            return Ok(result);
        }
        
        [HttpGet("{id}")]
        public ActionResult Get(Guid id)
        {
            var employee = _repo.GetById(id);
            return Ok(employee);
        }
        
        [HttpPut("{id}")]
        public ActionResult Edit(Guid id,[FromForm]CreateEmployeeDto employeeDto)
        {
            var result = _repo.Update(id,employeeDto);
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