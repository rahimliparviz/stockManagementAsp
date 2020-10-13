using System;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Stock.Services.DTO;
using Stock.Services.Repositories.Abstract;

namespace Stock.Api.Controllers
{
    public class ExpensesController : BaseController
    {
        private readonly IExpenseRepository _repo;

        public ExpensesController(IExpenseRepository repo)
        {
            _repo = repo;
        }
        
        [HttpGet]
        public ActionResult Get()
        {
            var expenses = _repo.GetAll();
            return Ok(expenses);
        }

        [HttpPost]
        public async Task<ActionResult> Create([FromForm]ExpenseDto expenseDto)
        {
            var result = await _repo.Create(expenseDto);
            return Ok(result);
        }
        
        [HttpGet("{id}")]
        public ActionResult Get(Guid id)
        {
            var expense = _repo.GetById(id);
            return Ok(expense);
        }
        
        [HttpPut("{id}")]
        public ActionResult Edit(Guid id,[FromBody]ExpenseDto expenseDto)
        {
            var result = _repo.Update(id,expenseDto);
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