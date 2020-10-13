using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Threading.Tasks;
using AutoMapper;
using Microsoft.EntityFrameworkCore;
using Stock.Data;
using Stock.Domain;
using Stock.Services.DTO;
using Stock.Services.Errors;
using Stock.Services.Repositories.Abstract;

namespace Stock.Services.Repositories.Concrete
{
    public class SalaryRepository:ISalaryRepository
    {
        
        private readonly DataContext _context;
        private readonly IMapper _mapper;
        
        public SalaryRepository(DataContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }
        public List<SalaryDto> GetAll()
        {
            var salariesByMonth = _context.Salaries
                .Select(s => new Salary()
                    {
                        Month = s.Month,

                    })
                    .Distinct()
                    .ToList();
                
                ;
            List<SalaryDto> salaries = _mapper.Map<List<Salary>,List<SalaryDto>>(salariesByMonth);
            return salaries;
        }

        public SalaryDto GetById(Guid id)
        {
            var salary = _context.Salaries
                .Include(s=>s.Employee.User)
                .First(s=>s.Id == id);

            _ = salary ?? throw new RestException(HttpStatusCode.NotFound, new { Salary = "Not found" });
        
                return  _mapper.Map<Salary, SalaryDto>(salary);

            
        }

        public async Task<Response<SalaryDto>> Create(SalaryDto entityDto)
        {
            var check = _context.Salaries
                .Any(s => s.EmployeeId.ToString() == entityDto.EmployeeId &&
                          s.Month == entityDto.Month &&
                          s.Year == entityDto.Year)
                ;

            if (check)
            {
                throw new RestException(HttpStatusCode.BadRequest, new { Salary = "Salary is already paid" });

            }

            try
            {
                Salary salary = new Salary
                {
                    Amount = entityDto.Amount,
                    Month = entityDto.Month,
                    Year = entityDto.Year,
                    EmployeeId= Guid.Parse(entityDto.EmployeeId),
                    SalaryDate= DateTime.Now,
                    CreatedAt = DateTime.Now,
                    UpdatedAt = DateTime.Now
                };
                await _context.Salaries.AddAsync(salary);
                await _context.SaveChangesAsync();

                return new Response<SalaryDto>
                {
                    Data = entityDto,
                    Message = "Salary saved",
                    Time = DateTime.Now,
                    IsSuccess = true
                };
            }
            catch (Exception e)
            {
                return new Response<SalaryDto>
                {
                    Data = null,
                    Message = e.StackTrace,
                    Time = DateTime.Now,
                    IsSuccess = false
                };
            }
        }

        public async Task<Response<SalaryDto>> Update(Guid id, SalaryDto entityDto)
        {
            Salary salary = await _context.Salaries.FindAsync(id);
            
            if (salary == null)
            {
                throw new RestException(HttpStatusCode.NotFound, new { Salary = "Not found" });
            
            }
            
            salary.Month = entityDto.Month;
            salary.Amount = (double) entityDto.Amount;
            salary.EmployeeId = Guid.Parse(entityDto.EmployeeId);
            salary.UpdatedAt = DateTime.Now;
            
            var success = await _context.SaveChangesAsync() > 0;
            
            if (success)
            {
                var salaryDto = _mapper.Map<Salary, SalaryDto>(salary);
            
                return new Response<SalaryDto>
                {
                    Data = salaryDto,
                    Message = "Salary is updated ",
                    Time = DateTime.Now,
                    IsSuccess = true
                };
            };

            throw new Exception("Problem on saving salary");
        }

        public Response<SalaryDto> Delete(Guid id)
        {
            Salary salary = _context.Salaries.Find(id);

            if (salary == null)
            {
                throw new RestException(HttpStatusCode.NotFound, new { Salary = "Not found" });

            }

            _context.Salaries.Remove(salary);
            var success = _context.SaveChanges() > 0;

            if (success)
            {

                return new Response<SalaryDto>
                {
                    Data = null,
                    Message = "Salary removed successfully",
                    Time = DateTime.Now,
                    IsSuccess = true
                };
            }
            throw new Exception("Problem on deleting a salary");

        }

        public List<SalaryDto> ShowByMonth(int month)
        {
            var salaries = _context.Salaries
                .Include(s=>s.Employee.User)
                .Where(s=>s.Month == month)
                .ToList();

            if (salaries.Count() > 0)
            {
                var salariesDto = _mapper.Map<List<Salary>,List<SalaryDto>>(salaries);

                return salariesDto;
            }
            throw new RestException(HttpStatusCode.NotFound, new { Salary = "Salaries for month not found" });
        }
    }
}