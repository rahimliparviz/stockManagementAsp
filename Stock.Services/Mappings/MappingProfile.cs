using System.Globalization;
using AutoMapper;
using AutoMapper.Configuration;
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
                .ForMember(e => e.CustomerName,
                    o => o.MapFrom(x => x.Customer.Name))
                .ForMember(e => e.CustomerAddress,
                    o => o.MapFrom(x => x.Customer.Address))
                .ForMember(e => e.CustomerPhone,
                    o => o.MapFrom(x => x.Customer.Phone));


                // .ForMember(e => e.OrderProducts,
                //     o => o.MapFrom(x => x.OrderProducts.))
                //
                //
                // ;

                // CreateMap<OrderProduct, Product>()
                //
                //     .ForMember(e => e.Name,
                //         o => o.MapFrom(
                //             x => x.Pruduct.Name))
                //     .ForMember(e => e.Id,
                //         o => o.MapFrom(
                //             x => x.Pruduct.Id))
                //     .ForMember(e => e.Quantity,
                //         o => o.MapFrom(
                //             x => x.Pruduct.Quantity))
                //     .ForMember(e => e.Photo,
                //         o => o.MapFrom(
                //             x => x.Pruduct.Photo))
                //     .ForMember(e => e.SelingPrice,
                //         o => o.MapFrom(
                //             x => x.Pruduct.SelingPrice))
                //     ;
                //
                
                
            CreateMap<Product, ProductDto>()
                .ForMember(e=>e.CategoryName,o=>o.MapFrom(x=>x.Category.Name))
                .ForMember(e=>e.SupplierName,o=>o.MapFrom(x=>x.Supplier.Name))
                .ForMember(e=>e.Photo,o=>o.MapFrom(x=>x.Photo))
                
                ;

            CreateMap<Employee, EmployeeDto>()
                .ForMember(e=>e.Phone,o=>o.MapFrom(x=>x.User.PhoneNumber))
                .ForMember(e=>e.UserName,o=>o.MapFrom(x=>x.User.UserName))
                .ForMember(e=>e.Email,
                    o=>o.MapFrom(x=>x.User.Email))
                ;
            CreateMap<Salary, SalaryDto>()
                .ForMember(e => e.EmployeeName,
                    o => o.MapFrom(x => x.Employee.User.UserName))
                .ForMember(e => e.EmployeeEmail,
                    o => o.MapFrom(x => x.Employee.User.Email))

                .ForMember(e => e.MonthName,
                    o => o.MapFrom(x => CultureInfo.CurrentCulture.DateTimeFormat.GetMonthName(x.Month)))
                
        

            
                ;
            
            
        }
    }
}