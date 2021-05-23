using IdentityServer4.EntityFramework.Options;
using IDWorkFlow.Models;
using Microsoft.AspNetCore.ApiAuthorization.IdentityServer;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Options;

namespace IDWorkFlow.Data
{
    public class ApplicationDbContext : ApiAuthorizationDbContext<ApplicationUser>
    {
        public ApplicationDbContext(
            DbContextOptions options,
            IOptions<OperationalStoreOptions> operationalStoreOptions) : base(options, operationalStoreOptions)
        {
        }
        public DbSet<Product> Product { get; set; }
        public DbSet<ProductHistory> ProductHistory { get; set; }

        protected override void OnModelCreating(ModelBuilder builder)
        {

            builder.Entity<Product>()
        .HasMany(e => e.ProductHistories)
        .WithOne(c => c.Product);


            //    builder.Entity<ProductHistory>()
            //.HasOne(e => e.Product)
            //.WithMany(c => c.ProductHistories);
            base.OnModelCreating(builder);
        }
    }
}
