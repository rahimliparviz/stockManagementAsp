using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using Microsoft.EntityFrameworkCore;
using Stock.Data;
using Stock.Domain;
using Stock.Services.DTO;
using Stock.Services.Repositories.Abstract;

namespace Stock.Services.Repositories.Concrete
{
    public class DashboardRepository:IDashboardRepository
    {
        private readonly DataContext _context;
        private readonly IMapper _mapper;

        public DashboardRepository(DataContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }
        public async Task<List<DashboardExpenseDto>> ExpensesWithMonthInterval(int currentMonth, int previousMonth)
        {
            return  await _context.Expenses
                .Where(e => e.CreatedAt.Month >= previousMonth)
                .Where(e => e.CreatedAt.Month <= currentMonth)
                .GroupBy(e=>e.CreatedAt.Month)
                .Select(a => new DashboardExpenseDto {Month = a.Key, Amount=a.Sum(x=>x.Amount)})
                .ToListAsync();
                
        }

        public async Task<List<DashboardOrderDto>> OrdersWithDateInterval(int currentMonth, int previousMonth)
        {
            return await _context.Orders
                .Where(e => e.CreatedAt.Month >= previousMonth)
                .Where(e => e.CreatedAt.Month <= currentMonth)
                .GroupBy(e => e.CreatedAt.Month)
                .Select(a => new DashboardOrderDto {Month = a.Key, Amount = a.Sum(x => x.Pay)})
                .ToListAsync();

        }
    }
}