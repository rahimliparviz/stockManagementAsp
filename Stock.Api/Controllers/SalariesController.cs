using System;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Stock.Services.DTO;
using Stock.Services.Repositories.Abstract;

namespace Stock.Api.Controllers
{
    public class SalariesController : BaseController
    {
        private readonly ISalaryRepository _repo;

        public SalariesController(ISalaryRepository repo)
        {
            _repo = repo;
        }

        [HttpGet]
        public ActionResult Get()
        {
            var salaries = _repo.GetAll();
            return Ok(salaries);
        }
        
        [HttpPost("pay")]
        public async Task<ActionResult> Create([FromBody]SalaryDto salaryDto)
        {
            var result = await _repo.Create(salaryDto);
            return Ok(result);
        }
        
        [HttpGet("{id}")]
        public ActionResult Get(Guid id)
        {
            
            var salary = _repo.GetById(id);
            return Ok(salary);
        }
        
        [HttpGet("monthly/{month}")]
        public ActionResult GetByMonth(int month)
        {
            var salary = _repo.ShowByMonth(month);
            return Ok(salary);
        }
        
        [HttpPut("{id}")]
        public async Task<ActionResult> Edit(Guid id,[FromBody]SalaryDto salaryDto)
        {
            var result =await  _repo.Update(id,salaryDto);
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