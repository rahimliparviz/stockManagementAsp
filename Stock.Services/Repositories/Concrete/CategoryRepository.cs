using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using AutoMapper;
using Microsoft.AspNetCore.Http;
using Stock.Data;
using Stock.Domain;
using Stock.Services.DTO;
using Stock.Services.Errors;
using Stock.Services.Repositories.Abstract;
using Stock.Services.Validations;

namespace Stock.Services.Repositories.Concrete
{
    public class CategoryRepository : ICategoryRepository
    {
        private readonly DataContext _context;
        private readonly IMapper _mapper;
        public CategoryRepository(DataContext context, IMapper mapper)
        {
            _mapper = mapper;
            _context = context;
        }
        public List<CategoryDto> GetAll()
        {
            List<CategoryDto> categories = _mapper.Map<List<Category>,List<CategoryDto>>(_context.Categories.ToList());
            return categories;
        }

        public CategoryDto GetById(Guid id)
        {
            Category category = _context.Categories.Find(id);

            if (category != null)
            {
                var categoryDto = _mapper.Map<Category, CategoryDto>(category);

                return categoryDto;
            }
            throw new RestException(HttpStatusCode.NotFound, new { Category = "Not found" });
        }

      

        public Response<CategoryDto> Create(CategoryDto entityDto)
        { ;

            try
            {
                Category category = new Category
                {
                    Name = entityDto.Name,
                    CreatedAt = DateTime.Now,
                    UpdatedAt = DateTime.Now
                };
                _context.Categories.Add(category);
                _context.SaveChanges();

                return new Response<CategoryDto>
                {
                    Data = entityDto,
                    Message = $"{category.Name} Category saved",
                    Time = DateTime.Now,
                    IsSuccess = true
                };
            }
            catch (Exception e)
            {
                return new Response<CategoryDto>
                {
                    Data = null,
                    Message = e.StackTrace,
                    Time = DateTime.Now,
                    IsSuccess = false
                };
            }
        }

        public Response<CategoryDto> Update(Guid id,CategoryDto entityDto)
        {
            // var rules = new List<KeyValuePair<string,string>>
            // {
            //         new KeyValuePair<string, string>("Name",$"unique:Categories,{id}")
            // };
            //  ModelValidations.Validate(rules,entityDto,_context);
            
            
            Category category = _context.Categories.Find(id);
            
            if (category == null)
            {
                throw new RestException(HttpStatusCode.NotFound, new { Category = "Not found" });
            
            }
            
            category.Name = entityDto.Name;
            category.UpdatedAt = DateTime.Now;
            
            var success = _context.SaveChanges() > 0;
            
            if (success)
            {
                var categoryDto = _mapper.Map<Category, CategoryDto>(category);
            
                return new Response<CategoryDto>
                {
                    Data = categoryDto,
                    Message = $"{category.Name} category updated ",
                    Time = DateTime.Now,
                    IsSuccess = true
                };
            };

            throw new Exception("Problem on saving category");
        }

        public Response<CategoryDto> Delete(Guid id)
        {
            Category category = _context.Categories.Find(id);

            if (category == null)
            {
                throw new RestException(HttpStatusCode.NotFound, new { Category = "Not found" });

            }

            _context.Categories.Remove(category);
            var success = _context.SaveChanges() > 0;

            if (success)
            {

                return new Response<CategoryDto>
                {
                    Data = null,
                    Message = $"'{category.Name}' category removed successfully",
                    Time = DateTime.Now,
                    IsSuccess = true
                };
            }
            throw new Exception("Problem on deleting a category");


        }
    }
}