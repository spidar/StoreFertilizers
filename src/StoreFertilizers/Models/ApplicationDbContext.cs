using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNet.Identity.EntityFramework;
using Microsoft.Data.Entity;

namespace StoreFertilizers.Models
{
    public class ApplicationDbContext : IdentityDbContext<ApplicationUser>
    {
        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);
            // Customize the ASP.NET Identity model and override the defaults if needed.
            // For example, you can rename the ASP.NET Identity table names and more.
            // Add your customizations after calling base.OnModelCreating(builder);
        }

        public DbSet<Customer> Customers { get; set; }
        public DbSet<Provider> Providers { get; set; }
        public DbSet<Invoice> Invoices { get; set; }
        public DbSet<Purchase> Purchases { get; set; }
        public DbSet<InvoiceDetails> InvoiceDetails { get; set; }
        public DbSet<Stock> Stocks { get; set; }

        public DbSet<Bank> Banks { get; set; }
        public DbSet<Employee> Employees { get; set; }
        public DbSet<PaymentType> PaymentTypes { get; set; }
        public DbSet<Product> Products { get; set; }
        public DbSet<ProductType> ProductTypes { get; set; }
        public DbSet<UnitType> UnitTypes { get; set; }

        public DbSet<User> CustomUsers { get; set; }
    }
}
