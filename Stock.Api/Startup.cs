using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.HttpsPolicy;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using Stock.Data;
using Microsoft.EntityFrameworkCore;
using Stock.Domain;
using Stock.Services.Repositories.Abstract;
using Stock.Services.Repositories.Concrete;
using AutoMapper;
using Stock.Api.Middlewares;
using Stock.Services.Mappings;
using FluentValidation.AspNetCore;
using Stock.Data.Validations.Category;
using FluentValidation;
using Stock.Infrastructure.Abstracts;
using Stock.Infrastructure.Concrete;
using Stock.Services.DTO;

namespace Stock.Api
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }


        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddDbContext<DataContext>(opts => {
                opts.EnableDetailedErrors();
                opts.UseNpgsql(Configuration.GetConnectionString("stock_db"));
            });
        
            services.AddAutoMapper(typeof(MappingProfile).Assembly);

            services.AddControllers()
            .AddFluentValidation(cfg => cfg.RegisterValidatorsFromAssemblyContaining<CategoryDtoValidation>());
            ;

            services.AddTransient<IPhotoAccessor, PhotoAccessor>();

            services.AddTransient<ICategoryRepository, CategoryRepository>();
            services.AddTransient<IExpenseRepository, ExpenseRepository>();
            services.AddTransient<ICustomerRepository, CustomerRepository>();
            services.AddTransient<IEmployeeRepository, EmployeeRepository>();
            services.AddTransient<ISupplierRepository, SupplierRepository>();
            services.AddTransient<ISalaryRepository, SalaryRepository>();
            services.AddTransient<IProductRepository, ProductRepository>();
            services.AddTransient<IOrderRepository, OrderRepository>();
            services.AddTransient<IRegulationRepository, RegulationRepository>();

        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            app.UseMiddleware<RestErrorHandlingMiddleware>();

            if (env.IsDevelopment())
            {
                // app.UseDeveloperExceptionPage();
            }

            // app.UseHttpsRedirection();

            app.UseRouting();

            app.UseAuthorization();

            app.UseStaticFiles();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });
        }
    }
}
