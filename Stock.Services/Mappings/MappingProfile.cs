using AutoMapper;
using Stock.Domain;
using Stock.Services.DTO;

namespace Stock.Services.Mappings
{
    public class MappingProfile:Profile
    {
        public MappingProfile()
        {
            CreateMap<Category, CategoryDto>();
            CreateMap<Expense, ExpenseDto>();
            CreateMap<Customer, CustomerDto>();
            CreateMap<Supplier, SupplierDto>();
            CreateMap<Order, OrderDto>()
                .ForMember(e=>e.CustomerName,
                    o=>o.MapFrom(x=>x.Customer.Name));
            CreateMap<Product, ProductDto>()
                .ForMember(e=>e.CategoryName,o=>o.MapFrom(x=>x.Category.Name))
                .ForMember(e=>e.SupplierName,o=>o.MapFrom(x=>x.Supplier.Name))
                ;

            CreateMap<Employee, EmployeeDto>()
                .ForMember(e=>e.Phone,o=>o.MapFrom(x=>x.User.PhoneNumber))
                .ForMember(e=>e.Name,o=>o.MapFrom(x=>x.User.UserName))
                .ForMember(e=>e.Email,
                    o=>o.MapFrom(x=>x.User.Email))
                ;

            CreateMap<Salary, SalaryDto>()
                .ForMember(e => e.EmployeeName,
                    o => o.MapFrom(x => x.Employee.User.UserName))
                .ForMember(e => e.EmployeeEmail,
                    o => o.MapFrom(x => x.Employee.User.Email))
                ;
        }
    }
}