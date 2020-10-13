using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Threading.Tasks;
using AutoMapper;
using Microsoft.AspNetCore.Hosting;
using Stock.Data;
using Stock.Domain;
using Stock.Infrastructure.Abstracts;
using Stock.Services.DTO;
using Stock.Services.Errors;
using Stock.Services.Repositories.Abstract;

namespace Stock.Services.Repositories.Concrete
{
    public class CustomerRepository:ICustomerRepository
    {
        private readonly IMapper _mapper;
        private readonly DataContext _context;
        private readonly IPhotoAccessor _photoAccessor;

        public CustomerRepository(
            IMapper mapper,
            DataContext context,
            IPhotoAccessor photoAccessor)
        {
            _mapper = mapper;
            _context = context;
            _photoAccessor = photoAccessor;
        }
       public List<CustomerDto> GetAll()
        {
            List<CustomerDto> customers = _mapper.Map<List<Customer>,List<CustomerDto>>(_context.Customers.ToList());
            return customers;
        }

        public CustomerDto GetById(Guid id)
        {
            Customer customer = _context.Customers.Find(id);

            if (customer != null)
            {
                var customerDto = _mapper.Map<Customer, CustomerDto>(customer);

                return customerDto;
            }
            throw new RestException(HttpStatusCode.NotFound, new { Customer = "Not found" });
        }

        public async Task<Response<CustomerDto>> Create(CreateCustomerDto createEntityDto)
        {
            try
            {
                Customer customer = new Customer
                {
                    Name = createEntityDto.Name,
                    Phone = createEntityDto.Phone,
                    Email = createEntityDto.Email,
                    Address = createEntityDto.Address,
                    CreatedAt = DateTime.Now,
                    UpdatedAt = DateTime.Now
                };

                if (createEntityDto.Photo != null)
                {
                    string path = _photoAccessor.Add("customers", createEntityDto.Photo);
                    customer.Photo = path;
                }
                
                await _context.Customers.AddAsync(customer);
                await _context.SaveChangesAsync();

                var returnEntityDto = _mapper.Map<Customer, CustomerDto>(customer);
                
                return new Response<CustomerDto>
                {
                    Data = returnEntityDto,
                    Message = "Customer saved",
                    Time = DateTime.Now,
                    IsSuccess = true
                };
            }
            catch (Exception e)
            {
                return new Response<CustomerDto>
                {
                    Data = null,
                    Message = e.Message,
                    Time = DateTime.Now,
                    IsSuccess = false
                };
            }
        }

        public async Task<Response<CustomerDto>> Update(Guid id, CreateCustomerDto entityDto)
        {
            Customer customer = await _context.Customers.FindAsync(id);
            
            if (customer == null)
            {
                throw new RestException(HttpStatusCode.NotFound, new { Customer = "Not found" });
            }

            customer.Address = entityDto.Address;
            customer.Email = entityDto.Email;
            customer.Name = entityDto.Name;
            customer.Phone = entityDto.Phone;
            customer.UpdatedAt = DateTime.Now;
            
            
            if (entityDto.Photo != null)
            {

                if (customer.Photo != null)
                {
                    _photoAccessor.Delete(customer.Photo);
                }
                
                
                string path = _photoAccessor.Add("customers", entityDto.Photo);
                customer.Photo = path;
            }
            
            var success = await _context.SaveChangesAsync() > 0;
            
            if (success)
            {
                var customerDto = _mapper.Map<Customer, CustomerDto>(customer);
            
                return new Response<CustomerDto>
                {
                    Data = customerDto,
                    Message = $"Customer {customerDto.Name} updated",
                    Time = DateTime.Now,
                    IsSuccess = true
                };
            };

            throw new Exception("Problem on saving category");
        }

        public Response<CustomerDto> Delete(Guid id)
        {
            Customer customer = _context.Customers.Find(id);

            if (customer == null)
            {
                throw new RestException(HttpStatusCode.NotFound, new { Customer = "Not found" });
                
            }
            
            if (customer.Photo != null)
            {
                _photoAccessor.Delete(customer.Photo);
            }

            _context.Customers.Remove(customer);
            var success = _context.SaveChanges() > 0;

            if (success)
            {

                return new Response<CustomerDto>
                {
                    Data = null,
                    Message = $"'{customer.Name}' removed successfully",
                    Time = DateTime.Now,
                    IsSuccess = true
                };
            }
            throw new Exception("Problem on deleting customer");
        }
    }
}