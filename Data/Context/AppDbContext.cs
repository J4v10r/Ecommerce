using Microsoft.EntityFrameworkCore.Design;
using Microsoft.EntityFrameworkCore;
using Saas.Services.TenantServices;
using Saas.Models;

namespace Saas.Infrastructure.Context
{
    public class AppDbContext : DbContext
    {

        private readonly ITenantProvider _tenantProvider;

        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options)
        {
        }

        public DbSet<User> Users { get; set; }
        public DbSet<Tenant> Tenants { get; set; }
        public DbSet<Product> Products { get; set; }
        public DbSet<Catalog> Catalogs { get; set; }
        public DbSet<Order> Orders { get; set; }
        public DbSet<OrderItem> OrderItems { get; set; }
        public DbSet<Payment> Payments { get; set; }
        public DbSet<Plan> Plans { get; set; }
        public DbSet<Admin> Admins { get; set; }
        public DbSet<OrderPayment> OrderPayments { get; set; }
        public DbSet<Category> Categories { get; set; }
        public DbSet<Address> Addresses { get; set; }
    }
}
