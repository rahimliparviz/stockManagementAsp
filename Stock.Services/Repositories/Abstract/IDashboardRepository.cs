using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Stock.Services.DTO;

namespace Stock.Services.Repositories.Abstract
{
    public interface IDashboardRepository
    {
        Task<List<DashboardExpenseDto>> ExpensesWithMonthInterval(int currentMonth,int previousMonth);
        Task<List<DashboardOrderDto>> OrdersWithDateInterval(int currentMonth, int previousMonth);
    }
}