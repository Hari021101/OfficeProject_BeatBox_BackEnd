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

		// Existing
		public DbSet<Category> Categories => Set<Category>();
		public DbSet<Product> Products => Set<Product>();

		// ADD THESE BACK
		public DbSet<Cart> Carts => Set<Cart>();
		public DbSet<CartItem> CartItems => Set<CartItem>();

		public DbSet<Order> Orders => Set<Order>();
		public DbSet<OrderItem> OrderItems => Set<OrderItem>();

		public DbSet<Payment> Payments => Set<Payment>();
		public DbSet<UserAddress> UserAddresses => Set<UserAddress>();
		public DbSet<WishlistItem> WishlistItems => Set<WishlistItem>();

		protected override void OnModelCreating(ModelBuilder builder)
		{
			base.OnModelCreating(builder);

			builder.Entity<Product>(entity =>
			{
				entity.Property(p => p.Price)
					.HasColumnType("decimal(18,2)");

				entity.Property(p => p.DiscountPrice)
					.HasColumnType("decimal(18,2)");
			});

			// Fix for new Decimal properties to avoid EF Core warnings/truncation
			builder.Entity<Order>(entity =>
			{
				entity.Property(o => o.TotalAmount).HasColumnType("decimal(18,2)");
				// Configure foreign key mapping to AppUser since there's no navigation property
				entity.HasOne<AppUser>().WithMany().HasForeignKey(o => o.UserId).OnDelete(DeleteBehavior.Restrict);
			});

			builder.Entity<OrderItem>(entity =>
			{
				entity.Property(oi => oi.UnitPrice).HasColumnType("decimal(18,2)");
			});

			builder.Entity<Payment>(entity =>
			{
				entity.Property(p => p.Amount).HasColumnType("decimal(18,2)");
			});

			builder.Entity<Cart>(entity =>
			{
				// Configure foreign key mapping to AppUser since there's no navigation property
				entity.HasOne<AppUser>().WithMany().HasForeignKey(c => c.UserId).OnDelete(DeleteBehavior.Cascade);
			});

			builder.Entity<CartItem>(entity =>
			{
				entity.Property(ci => ci.UnitPrice).HasColumnType("decimal(18,2)");
			});

			builder.Entity<UserAddress>(entity =>
			{
				entity.HasOne(ua => ua.User)
					.WithMany()
					.HasForeignKey(ua => ua.UserId)
					.OnDelete(DeleteBehavior.Cascade);
			});

			builder.Entity<WishlistItem>(entity =>
			{
				entity.HasOne(wl => wl.User)
					.WithMany()
					.HasForeignKey(wl => wl.UserId)
					.OnDelete(DeleteBehavior.Cascade);

				entity.HasOne(wl => wl.Product)
					.WithMany()
					.HasForeignKey(wl => wl.ProductId)
					.OnDelete(DeleteBehavior.Cascade);
			});
		}
	}
}