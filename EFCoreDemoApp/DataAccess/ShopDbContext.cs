using EFCoreDemoApp.DBModels;
using EFCoreDemoApp.Integration;
using Microsoft.EntityFrameworkCore;

namespace EFCoreDemoApp.DataAccess
{
    public class ShopDbContext : DbContext, IDefaultSchema
    {
        public string DefaultSchema { get; }

        // Registered DB Model in ShopDbContext file
        public DbSet<Customer> Customers { get; set; }
        public DbSet<Order> Orders { get; set; }


        public ShopDbContext(DbContextOptions<ShopDbContext> options, IDefaultSchema schema = null) : base(options)
        {
            DefaultSchema = schema?.DefaultSchema ?? "myschema";
        }

        /*
         OnModelCreating mainly used to configured our EF model
         And insert master data if required
        */
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            //Configure default schema
            modelBuilder.HasDefaultSchema(DefaultSchema);

            // Setting Order model
            modelBuilder.Entity<Order>().HasKey(x => x.Id);
            modelBuilder.Entity<Order>().Property(x => x.Contents).HasMaxLength(2000);
            modelBuilder.Entity<Order>(b =>
            {
                b.ComplexProperty(e => e.BillingAddress);
                b.ComplexProperty(e => e.ShippingAddress);
            });

            // Setting Customer model
            modelBuilder.Entity<Customer>().HasKey(x => x.Id);
            modelBuilder.Entity<Customer>().Property(x => x.Name).HasMaxLength(500);
            modelBuilder.Entity<Customer>()
                        .ComplexProperty(e => e.Address);
        }
    }
}
