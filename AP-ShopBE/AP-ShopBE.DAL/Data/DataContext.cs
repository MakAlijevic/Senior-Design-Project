using Microsoft.EntityFrameworkCore;
using AP_ShopBE.DAL.Model;

namespace AP_ShopBE.DAL.Data
{
    public class DataContext : DbContext
    {
        public DataContext(DbContextOptions<DataContext> options) : base(options)
        {
            
        }

        public DbSet<User> Users { get; set; }
        public DbSet<Role> Roles { get; set; }
        public DbSet<Country> Countries { get; set; }
        public DbSet<Order> Orders { get; set; }
        public DbSet<Product> Products { get; set; }
        public DbSet<Category> Categories { get; set; }
        public DbSet<Condition> Conditions { get; set; }
        public DbSet<Shipping> Shippings { get; set; }
        public DbSet<CartItem> Cart_Item { get; set; }
        public DbSet<OrderProduct> OrderProduct { get; set; }
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<CartItem>()
                .HasKey(_=> _.Id);
            modelBuilder.Entity<CartItem>()
                .HasOne(_ => _.User)
                .WithMany(_ => _.CartItems)
                .HasForeignKey(_ => _.UserId);
            modelBuilder.Entity<CartItem>()
                .HasOne(_ => _.Product)
                .WithMany(_ => _.CartItems)
                .HasForeignKey(_ => _.ProductId);


            modelBuilder.Entity<OrderProduct>()
                .HasKey(_ => _.Id);
            modelBuilder.Entity<OrderProduct>()
                .HasOne(_ => _.Order)
                .WithMany(_ => _.OrderProducts)
                .HasForeignKey(_ => _.OrderId);
            modelBuilder.Entity<OrderProduct>()
                .HasOne(_ => _.Product)
                .WithMany(_ => _.OrderProducts)
                .HasForeignKey(_ => _.ProductId);
        }
    }
}
