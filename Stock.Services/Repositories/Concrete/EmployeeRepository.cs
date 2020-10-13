using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Threading.Tasks;
using AutoMapper;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Stock.Data;
using Stock.Domain;
using Stock.Infrastructure.Abstracts;
using Stock.Services.DTO;
using Stock.Services.Errors;
using Stock.Services.Repositories.Abstract;

namespace Stock.Services.Repositories.Concrete
{
    public class EmployeeRepository:IEmployeeRepository
    {
        private readonly UserManager<User> _userManager;
        private readonly IMapper _mapper;
        private readonly DataContext _context;
        private readonly IPhotoAccessor _photoAccessor;

        public EmployeeRepository(
            UserManager<User> userManager,
            IMapper mapper,
            DataContext context,
            IPhotoAccessor photoAccessor)
        {
            _userManager = userManager;
            _mapper = mapper;
            _context = context;
            _photoAccessor = photoAccessor;
        }
       public List<EmployeeDto> GetAll()
       {
           var employeeEntities = 
               _context.Employees.
                   Include(u=>u.User).ToList();
           
            List<EmployeeDto> employees = _mapper.Map<List<Employee>,List<EmployeeDto>>(employeeEntities);
            return employees;
        }

        public EmployeeDto GetById(Guid id)
        {
            Employee employee = _context.Employees
                .Include(u=>u.User)
                .SingleOrDefault(e=>e.Id == id)
                ;

            if (employee != null)
            {
                var employeeDto = _mapper.Map<Employee, EmployeeDto>(employee);

                return employeeDto;
            }
            throw new RestException(HttpStatusCode.NotFound, new { Employee = "Not found" });
        }

        public async Task<Response<EmployeeDto>> Create(CreateEmployeeDto createEntityDto)
        {
            if ( _context.Users.Any(x => x.Email == createEntityDto.Email))
                throw new RestException(HttpStatusCode.BadRequest, new {Email = "Email already exists"});

            if ( _context.Users.Any(x => x.UserName == createEntityDto.UserName))
                throw new RestException(HttpStatusCode.BadRequest, new {UserName = "Username already exists"});
            
            try
            {
                Employee employee = new Employee
                {
                    Salary = createEntityDto.Salary,
                    JoiningDate = createEntityDto.JoiningDate,
                    Address = createEntityDto.Address,
                    CreatedAt = DateTime.Now,
                    UpdatedAt = DateTime.Now
                };
                User user = new User
                {
                    Email = createEntityDto.Email,
                    UserName = createEntityDto.UserName,
                    PhoneNumber = createEntityDto.Phone,
                    UserType = "employee",
                };

                employee.User = user;
                

                if (createEntityDto.Photo != null)
                {
                    string path = _photoAccessor.Add("employees", createEntityDto.Photo);
                    employee.Photo = path;
                }


                IdentityResult result = await _userManager.CreateAsync(user, createEntityDto.Password);
                 
                 await _context.Employees.AddAsync(employee);
                 await _context.SaveChangesAsync();

                 if (!result.Succeeded)
                 {
                     throw new Exception("Problem creating user");

                 }


                
                var returnEntityDto = _mapper.Map<Employee, EmployeeDto>(employee);
                
                return new Response<EmployeeDto>
                {
                    Data = returnEntityDto,
                    Message = "Employee saved",
                    Time = DateTime.Now,
                    IsSuccess = true
                };
            }
            catch (Exception e)
            {
                return new Response<EmployeeDto>
                {
                    Data = null,
                    Message = e.StackTrace,
                    Time = DateTime.Now,
                    IsSuccess = false
                };
            }
        }

        public async Task<Response<EmployeeDto>> Update(Guid id, CreateEmployeeDto entityDto)
        {
            
            Employee employee = _context.Employees
                    .Include(u=>u.User)
                    .SingleOrDefault(e=>e.Id == id) ?? 
                                throw new RestException(HttpStatusCode.NotFound, new { Employee = "Not found" }) 
                ;


            if (!string.IsNullOrWhiteSpace(entityDto.Password))
            {
                await _userManager.RemovePasswordAsync(employee.User);
                await _userManager.AddPasswordAsync(employee.User, entityDto.Password);
            }
            
            employee.User.Email = entityDto.Email;
            employee.User.UserName = entityDto.UserName;
            employee.User.PhoneNumber = entityDto.Phone;
            employee.Salary = entityDto.Salary;
            employee.JoiningDate = entityDto.JoiningDate;
            employee.Address = entityDto.Address;
            employee.UpdatedAt = DateTime.Now;
            
            
            
            if (entityDto.Photo != null)
            {

                if (employee.Photo != null)
                {
                    _photoAccessor.Delete(employee.Photo);
                }
                
                
                string path = _photoAccessor.Add("employees", entityDto.Photo);
                employee.Photo = path;
            }
            
            var success = await _context.SaveChangesAsync() > 0;
            
            if (success)
            {
                var employeeDto = _mapper.Map<Employee, EmployeeDto>(employee);
            
                return new Response<EmployeeDto>
                {
                    Data = employeeDto,
                    Message = $"Employee {employeeDto.UserName} updated",
                    Time = DateTime.Now,
                    IsSuccess = true
                };
            };

            throw new Exception("Problem on saving employee");
        }

        public Response<EmployeeDto> Delete(Guid id)
        {
            Employee employee = _context.Employees.Find(id);
            
            if (employee == null)
            {
                throw new RestException(HttpStatusCode.NotFound, new { Employee = "Not found" });
                
            }
            
            if (employee.Photo != null)
            {
                _photoAccessor.Delete(employee.Photo);
            }
            
            var employeeUser =_context.Users.Find( employee.UserId);
            //TODO make Cascade delete
            _context.Users.Remove(employeeUser);
            _context.Employees.Remove(employee);
            var success = _context.SaveChanges() > 0;
            
            if (success)
            {
            
                return new Response<EmployeeDto>
                {
                    Data = null,
                    Message = $"'{employeeUser.UserName}' removed successfully",
                    Time = DateTime.Now,
                    IsSuccess = true
                };
            }
            throw new Exception("Problem on deleting employee");
        }
    }
}