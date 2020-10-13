using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Threading.Tasks;
using AutoMapper;
using Stock.Data;
using Stock.Domain;
using Stock.Services.DTO;
using Stock.Services.Errors;
using Stock.Services.Repositories.Abstract;

namespace Stock.Services.Repositories.Concrete
{
    public class ExpenseRepository:IExpenseRepository
    {
        private readonly DataContext _context;
        private readonly IMapper _mapper;

        public ExpenseRepository(DataContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }
        public List<ExpenseDto> GetAll()
        {
            List<ExpenseDto> expenses = _mapper.Map<List<Expense>,List<ExpenseDto>>(_context.Expenses.ToList());
            return expenses;
        }

        public ExpenseDto GetById(Guid id)
        {
            Expense expense = _context.Expenses.Find(id);

            if (expense != null)
            {
                var expenseDto = _mapper.Map<Expense, ExpenseDto>(expense);

                return expenseDto;
            }
            throw new RestException(HttpStatusCode.NotFound, new { Expense = "Not found" });
        }

        public async Task<Response<ExpenseDto>> Create(ExpenseDto entityDto)
        {
          
                Expense expense = new Expense
                {
                    Amount = entityDto.Amount,
                    Details = entityDto.Details,
                    CreatedAt = DateTime.Now,
                    UpdatedAt = DateTime.Now
                };
                 _context.Expenses.Add(expense);
                var success = await _context.SaveChangesAsync() > 0;

                if (success)
                {
                    return new Response<ExpenseDto>
                    {
                        Data = entityDto,
                        Message = "Expense saved",
                        Time = DateTime.Now,
                        IsSuccess = true
                    };
                }

                throw new Exception("Problem on saving expense");
        }

        public async Task<Response<ExpenseDto>> Update(Guid id, ExpenseDto entityDto)
        {
            Expense expense = await _context.Expenses.FindAsync(id);
            
            if (expense == null)
            {
                throw new RestException(HttpStatusCode.NotFound, new { Expense = "Not found" });
            
            }

            expense.Amount = entityDto.Amount;
            expense.Details = entityDto.Details;
            expense.UpdatedAt = DateTime.Now;
            
            var success = await _context.SaveChangesAsync() > 0;
            
            if (success)
            {
                var expenseDto = _mapper.Map<Expense, ExpenseDto>(expense);
            
                return new Response<ExpenseDto>
                {
                    Data = expenseDto,
                    Message = "Expense updated",
                    Time = DateTime.Now,
                    IsSuccess = true
                };
            };

            throw new Exception("Problem on saving category");
        }

        public Response<ExpenseDto> Delete(Guid id)
        {
            Expense expense = _context.Expenses.Find(id);

            if (expense == null)
            {
                throw new RestException(HttpStatusCode.NotFound, new { Expense = "Not found" });

            }

            _context.Expenses.Remove(expense);
            var success = _context.SaveChanges() > 0;

            if (success)
            {

                return new Response<ExpenseDto>
                {
                    Data = null,
                    Message = $"'{expense.Amount}' removed successfully",
                    Time = DateTime.Now,
                    IsSuccess = true
                };
            }
            throw new Exception("Problem on deleting expense");
        }
    }
}