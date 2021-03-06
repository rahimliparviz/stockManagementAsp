﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Threading.Tasks;
using AutoMapper;
using Microsoft.EntityFrameworkCore;
using Stock.Data;
using Stock.Domain;
using Stock.Infrastructure.Abstracts;
using Stock.Services.DTO;
using Stock.Services.Errors;
using Stock.Services.Repositories.Abstract;

namespace Stock.Services.Repositories.Concrete
{
    public class ProductRepository:IProductRepository
    {
        private IMapper _mapper;
        private DataContext _context;
        private IPhotoAccessor _photoAccessor;

        public ProductRepository(
            IMapper mapper,
            DataContext context,
            IPhotoAccessor photoAccessor)
        {
            _mapper = mapper;
            _context = context;
            _photoAccessor = photoAccessor;
        }
        public List<ProductDto> GetAll()
        {
            var products =
                _context.Products
                    .Include(p => p.Category)
                    .Include(p => p.Supplier)
                    .ToList();


            return _mapper.Map<List<Product>, List<ProductDto>>(products);

        }

        public ProductDto GetById(Guid id)
        {
            Product product = _context.Products.Find(id);
            
            _ = product ?? throw new RestException(HttpStatusCode.NotFound, new { Product = "Not found" });
            
            var productDto = _mapper.Map<Product, ProductDto>(product);

            return productDto;
           
        }

        public async Task<Response<ProductDto>> Create(CreateProductDto entityDto)
        {
            try
            {

                Product product = new Product
                {
                    Name = entityDto.Name,
                    Code = entityDto.Code,
                    CategoryId = Guid.Parse(entityDto.CategoryId),
                    SupplierId  = Guid.Parse(entityDto.SupplierId),
                    BuyingDate = entityDto.BuyingDate,
                    BuyingPrice = entityDto.BuyingPrice,
                    SelingPrice = entityDto.SelingPrice,
                    Quantity = entityDto.Quantity,
                    CreatedAt = DateTime.Now,
                    UpdatedAt = DateTime.Now
                };

                //Todo : validasiyalar ucun helper method yarat 
                var category = _context.Categories.Find(Guid.Parse(entityDto.CategoryId));
                if (category == null)
                {
                    return new Response<ProductDto>
                    {
                        Data = null,
                        Message = "Category not found",
                        Time = DateTime.Now,
                        IsSuccess = false
                    };
                }
                var supplier = _context.Suppliers.Find(Guid.Parse(entityDto.SupplierId));
                if (supplier == null)
                {
                    return new Response<ProductDto>
                    {
                        Data = null,
                        Message = "Supplier not found",
                        Time = DateTime.Now,
                        IsSuccess = false
                    };
                }

                

                if (entityDto.Photo != null)
                {
                    string path = _photoAccessor.Add("products", entityDto.Photo);
                    product.Photo = path;
                }
                
                await _context.Products.AddAsync(product);
                await _context.SaveChangesAsync();

                var returnEntityDto = _mapper.Map<Product, ProductDto>(product);
                
                return new Response<ProductDto>
                {
                    Data = returnEntityDto,
                    Message = $"{product.Name} saved",
                    Time = DateTime.Now,
                    IsSuccess = true
                };
            }
            catch (Exception e)
            {
                return new Response<ProductDto>
                {
                    Data = null,
                    Message = e.Message,
                    Time = DateTime.Now,
                    IsSuccess = false
                };
            }
        }

        public async Task<Response<ProductDto>> Update(Guid id, CreateProductDto entityDto)
        {
            try
            {
                Product product = await _context.Products.FindAsync(id);

                _ = product ?? throw new RestException(HttpStatusCode.BadRequest, new {Product ="Product is not exist." });
           
                product.Name = entityDto.Name;
                product.Code = entityDto.Code;
                product.CategoryId = Guid.Parse(entityDto.CategoryId);
                product.SupplierId = Guid.Parse(entityDto.SupplierId);
                product.BuyingDate = entityDto.BuyingDate;
                product.BuyingPrice = entityDto.BuyingPrice;
                product.SelingPrice = entityDto.SelingPrice;
                product.Quantity = entityDto.Quantity;
                product.UpdatedAt = DateTime.Now;

                

                if (entityDto.Photo != null)
                {
                    string path = _photoAccessor.Add("products", entityDto.Photo);
                    product.Photo = path;
                }
                
                await _context.SaveChangesAsync();

                var returnEntityDto = _mapper.Map<Product, ProductDto>(product);
                
                return new Response<ProductDto>
                {
                    Data = returnEntityDto,
                    Message = $"{product.Name} updated",
                    Time = DateTime.Now,
                    IsSuccess = true
                };
            }
            catch (Exception e)
            {
                return new Response<ProductDto>
                {
                    Data = null,
                    Message = e.StackTrace,
                    Time = DateTime.Now,
                    IsSuccess = false
                };
            }
        }

        public Response<ProductDto> Delete(Guid id)
        {
            Product product = _context.Products.Find(id);
            
            if (product == null)
            {
                throw new RestException(HttpStatusCode.NotFound, new { Product = "Not found" });
                
            }
            
            if (product.Photo != null)
            {
                _photoAccessor.Delete(product.Photo);
            }
        
            _context.Products.Remove(product);
            var success = _context.SaveChanges() > 0;
            
            if (success)
            {
            
                return new Response<ProductDto>
                {
                    Data = null,
                    Message = $"'{product.Name}' removed successfully",
                    Time = DateTime.Now,
                    IsSuccess = true
                };
            }
            throw new Exception("Problem on deleting product");
        }

        public Response<ProductDto> StockUpdate(StockProductQuantityDto productQuantity, Guid productId)
        {
            var product = _context.Products.Find(productId);

            _ = product ??
                throw new RestException(HttpStatusCode.NotFound, new { Product = "Not found" });
            
            product.Quantity = productQuantity.Quantity;
            _context.SaveChanges();
            
            return new Response<ProductDto>
            {
                Data = null,
                Message = $"'{product.Name}' quantity changed ",
                Time = DateTime.Now,
                IsSuccess = true
            };
        }
    }
}