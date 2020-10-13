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
    public class SupplierRepository:ISupplierRepository
    {
        private readonly IMapper _mapper;
        private readonly DataContext _context;
        private readonly IPhotoAccessor _photoAccessor;

        public SupplierRepository(
            IMapper mapper,
            DataContext context,
            IPhotoAccessor photoAccessor)
        {
            _mapper = mapper;
            _context = context;
            _photoAccessor = photoAccessor;
        }
       public List<SupplierDto> GetAll()
        {
            List<SupplierDto> suppliers = _mapper.Map<List<Supplier>,List<SupplierDto>>(_context.Suppliers.ToList());
            return suppliers;
        }

        public SupplierDto GetById(Guid id)
        {
            Supplier supplier = _context.Suppliers.Find(id);

            if (supplier != null)
            {
                var supplierDto = _mapper.Map<Supplier, SupplierDto>(supplier);

                return supplierDto;
            }
            throw new RestException(HttpStatusCode.NotFound, new { Supplier = "Not found" });
        }

        public async Task<Response<SupplierDto>> Create(CreateSupplierDto createEntityDto)
        {
            try
            {
                Supplier supplier = new Supplier
                {
                    Name = createEntityDto.Name,
                    Phone = createEntityDto.Phone,
                    Email = createEntityDto.Email,
                    Address = createEntityDto.Address,
                    ShopName = createEntityDto.ShopName,
                    CreatedAt = DateTime.Now,
                    UpdatedAt = DateTime.Now
                };

                if (createEntityDto.Photo != null)
                {
                    string path = _photoAccessor.Add("suppliers", createEntityDto.Photo);
                    supplier.Photo = path;
                }
                
                await _context.Suppliers.AddAsync(supplier);
                await _context.SaveChangesAsync();

                var returnEntityDto = _mapper.Map<Supplier, SupplierDto>(supplier);
                
                return new Response<SupplierDto>
                {
                    Data = returnEntityDto,
                    Message = "Supplier saved",
                    Time = DateTime.Now,
                    IsSuccess = true
                };
            }
            catch (Exception e)
            {
                return new Response<SupplierDto>
                {
                    Data = null,
                    Message = e.Message,
                    Time = DateTime.Now,
                    IsSuccess = false
                };
            }
        }

        public async Task<Response<SupplierDto>> Update(Guid id, CreateSupplierDto entityDto)
        {
            Supplier supplier = await _context.Suppliers.FindAsync(id);
            
            if (supplier == null)
            {
                throw new RestException(HttpStatusCode.NotFound, new { Supplier = "Not found" });
            }

            supplier.Address = entityDto.Address;
            supplier.Email = entityDto.Email;
            supplier.Name = entityDto.Name;
            supplier.Phone = entityDto.Phone;
            supplier.ShopName = entityDto.ShopName;
            supplier.UpdatedAt = DateTime.Now;
            
            
            if (entityDto.Photo != null)
            {

                if (supplier.Photo != null)
                {
                    _photoAccessor.Delete(supplier.Photo);
                }
                
                
                string path = _photoAccessor.Add("suppliers", entityDto.Photo);
                supplier.Photo = path;
            }
            
            var success = await _context.SaveChangesAsync() > 0;
            
            if (success)
            {
                var supplierDto = _mapper.Map<Supplier, SupplierDto>(supplier);
            
                return new Response<SupplierDto>
                {
                    Data = supplierDto,
                    Message = $"Supplier {supplierDto.Name} updated",
                    Time = DateTime.Now,
                    IsSuccess = true
                };
            };

            throw new Exception("Problem on saving category");
        }

        public Response<SupplierDto> Delete(Guid id)
        {
            Supplier supplier = _context.Suppliers.Find(id);

            if (supplier == null)
            {
                throw new RestException(HttpStatusCode.NotFound, new { Supplier = "Not found" });
                
            }
            
            if (supplier.Photo != null)
            {
                _photoAccessor.Delete(supplier.Photo);
            }

            _context.Suppliers.Remove(supplier);
            var success = _context.SaveChanges() > 0;

            if (success)
            {

                return new Response<SupplierDto>
                {
                    Data = null,
                    Message = $"'{supplier.Name}' removed successfully",
                    Time = DateTime.Now,
                    IsSuccess = true
                };
            }
            throw new Exception("Problem on deleting supplier");
        }
    }
}