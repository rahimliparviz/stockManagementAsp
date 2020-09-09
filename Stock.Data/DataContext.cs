using System;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using Stock.Domain;

namespace Stock.Data
{
    public class DataContext : IdentityDbContext<User>
    {
        public DataContext(DbContextOptions opt) : base(opt)
        {}

        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);
            
            builder.Entity<OrderProduct>()
                .HasKey(bc => new { bc.OrderId, bc.PruductId });  
            builder.Entity<OrderProduct>()
                .HasOne(bc => bc.Order)
                .WithMany(b => b.OrderProducts)
                .HasForeignKey(bc => bc.OrderId);  
            builder.Entity<OrderProduct>()
                .HasOne(bc => bc.Pruduct)
                .WithMany(c => c.OrderProducts)
                .HasForeignKey(bc => bc.PruductId);
        }

        public DbSet<Category> Categories { get; set; }
         public DbSet<Employee> Employees { get; set; }
         public DbSet<Expense> Expenses { get; set; }
         public DbSet<Customer> Customers { get; set; }
         
         public DbSet<Supplier> Suppliers { get; set; }

         public DbSet<Salary> Salaries { get; set; }

         public DbSet<Order> Orders { get; set; }
         public DbSet<Product> Products { get; set; }
         public DbSet<OrderProduct> OrderPruducts { get; set; }
         public DbSet<Regulation> Regulations { get; set; }

         
    }

}
