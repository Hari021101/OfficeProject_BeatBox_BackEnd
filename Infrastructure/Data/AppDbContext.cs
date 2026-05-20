using Domain.Entities;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.Data
{
    public class AppDbContext : IdentityDbContext<AppUser>
    {
        public AppDbContext(DbContextOptions<AppDbContext> options)
            : base(options)
        {
        }

        // =========================
        // DbSets
        // =========================

        public DbSet<Category> Categories => Set<Category>();

        public DbSet<Product> Products => Set<Product>();

        public DbSet<Cart> Carts => Set<Cart>();

        public DbSet<CartItem> CartItems => Set<CartItem>();

        public DbSet<Order> Orders => Set<Order>();

        public DbSet<OrderItem> OrderItems => Set<OrderItem>();

        public DbSet<Payment> Payments => Set<Payment>();


        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            // =========================
            // Product -> Category
            // =========================

            modelBuilder.Entity<Product>()
                .HasOne(p => p.Category)
                .WithMany(c => c.Products)
                .HasForeignKey(p => p.CategoryId)
                .OnDelete(DeleteBehavior.Restrict);

            // =========================
            // Cart -> CartItems
            // =========================

            modelBuilder.Entity<Cart>()
                .HasMany(c => c.CartItems)
                .WithOne(ci => ci.Cart)
                .HasForeignKey(ci => ci.CartId)
                .OnDelete(DeleteBehavior.Cascade);

            // =========================
            // CartItem -> Product
            // =========================

            modelBuilder.Entity<CartItem>()
                .HasOne(ci => ci.Product)
                .WithMany()
                .HasForeignKey(ci => ci.ProductId)
                .OnDelete(DeleteBehavior.Restrict);

            // =========================
            // Order -> OrderItems
            // =========================

            modelBuilder.Entity<Order>()
                .HasMany(o => o.OrderItems)
                .WithOne(oi => oi.Order)
                .HasForeignKey(oi => oi.OrderId)
                .OnDelete(DeleteBehavior.Cascade);

            // =========================
            // OrderItem -> Product
            // =========================

            modelBuilder.Entity<OrderItem>()
                .HasOne(oi => oi.Product)
                .WithMany()
                .HasForeignKey(oi => oi.ProductId)
                .OnDelete(DeleteBehavior.Restrict);

            // =========================
            // Payment -> Order
            // =========================

            modelBuilder.Entity<Payment>()
                .HasOne(p => p.Order)
                .WithOne()
                .HasForeignKey<Payment>(p => p.OrderId)
                .OnDelete(DeleteBehavior.Cascade);

            // =========================
            // Decimal Precision Config
            // =========================

            modelBuilder.Entity<Product>()
                .Property(p => p.Price)
                .HasPrecision(18, 2);

            modelBuilder.Entity<Product>()
                .Property(p => p.DiscountPrice)
                .HasPrecision(18, 2);

            modelBuilder.Entity<CartItem>()
                .Property(ci => ci.UnitPrice)
                .HasPrecision(18, 2);

            modelBuilder.Entity<Order>()
                .Property(o => o.TotalAmount)
                .HasPrecision(18, 2);

            modelBuilder.Entity<OrderItem>()
                .Property(oi => oi.UnitPrice)
                .HasPrecision(18, 2);

            modelBuilder.Entity<Payment>()
                .Property(p => p.Amount)
                .HasPrecision(18, 2);
        }
    }
}