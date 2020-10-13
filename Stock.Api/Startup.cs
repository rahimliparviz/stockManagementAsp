
using System;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;

using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Stock.Data;
using Microsoft.EntityFrameworkCore;
using Stock.Services.Repositories.Abstract;
using Stock.Services.Repositories.Concrete;
using AutoMapper;
using Stock.Api.Middlewares;
using Stock.Services.Mappings;
using FluentValidation.AspNetCore;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc.Authorization;
using Microsoft.IdentityModel.Tokens;
using Newtonsoft.Json;
using Stock.Data.Validations.Category;
using Newtonsoft.Json.Serialization;
using Stock.Domain;
using Stock.Infrastructure.Abstracts;
using Stock.Infrastructure.Concrete;

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
            services.AddCors();
            
         
            //Api den gelen variable namelerini camelcase etmek ucun servis
            // services.AddIdentityCore<User>();
            // services.AddControllers()
            services.AddControllers(opt =>
                {
                    // var policy = new AuthorizationPolicyBuilder().RequireAuthenticatedUser().Build();
                    // opt.Filters.Add(new AuthorizeFilter(policy));
                    opt.Filters.Add(new AuthorizeFilter());
                })
                .AddNewtonsoftJson(opts => {
                    opts.SerializerSettings.ContractResolver = new DefaultContractResolver {
                        NamingStrategy = new CamelCaseNamingStrategy()
                    };
                    opts.SerializerSettings.DateFormatString = "MM/dd/yyyy HH:mm:ss";

                }).AddFluentValidation(cfg => cfg.RegisterValidatorsFromAssemblyContaining<CategoryDtoValidation>())
                ;
            
            
            services.AddDbContext<DataContext>(opts => {
                opts.EnableDetailedErrors();
                opts.UseNpgsql(Configuration.GetConnectionString("stock_db"));
            });
        
            

             var builder = services.AddIdentityCore<User>();
             var identityBuilder = new IdentityBuilder(builder.UserType,builder.Services);
             identityBuilder.AddEntityFrameworkStores<DataContext>();
             identityBuilder.AddSignInManager<SignInManager<User>>();


             // services.AddAuthorization();
             // var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(Configuration["TokenKey"]));
             // TODO-key i deyis
             
             
             //// ilk once user login ve parol yazdiqda  token authenticationda yoxlanilir
             /// eger problem varsa geri error mesaj return edilir
             /// eger nolmaldirsa davam etdirilir request
             /// ve eger user [authorise] olan resursa sorgu gonderirse
             /// o tokende olan (claim,policilere) gore icaze verib verilmediyine qerar verilir
             var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes("1234567890veryhard"));
             services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
                 .AddJwtBearer(opt =>
                 {
                     opt.TokenValidationParameters = new TokenValidationParameters
                     {
                         ValidateIssuerSigningKey = true,
                         IssuerSigningKey = key,
                         ValidateAudience = false,
                         ValidateIssuer = false,
                         //asagidaki iki setr token expire olandan sonre
                         //logini engellemek ucundu ve 401 unauthorise mesaji gonderir, bulari elave etmesek token
                         //expire olsa bele sayta daxil olmaq olurdu
                         ValidateLifetime = true,
                         ClockSkew = TimeSpan.Zero
                     };
                     
                 });

             
        
            
            services.AddAutoMapper(typeof(MappingProfile).Assembly);

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
            services.AddTransient<IDashboardRepository, DashboardRepository>();
            services.AddTransient<IUserRepository, UserRepository>();
            services.AddTransient<IJwtGenerator, JwtGenerator>();

            
            


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

            app.UseCors(
                builder => builder
                    .WithOrigins(
                        "http://localhost:8080", 
                        "http://localhost:8081", 
                        "http://localhost:8082")
                    .AllowAnyMethod()
                    .AllowAnyHeader()
                    .AllowCredentials()
            );
            
            app.UseAuthentication();
            app.UseAuthorization();

            app.UseStaticFiles();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });
        }
    }
}
