using Domain.Entities;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using System.Reflection.Emit;

namespace Infrastructure.Data
{
	public class AppDbContext : IdentityDbContext<AppUser>
	{
		public AppDbContext(DbContextOptions<AppDbContext> options)
			: base(options)
		{
		}

        
        public DbSet<Category> Categories => Set<Category>();
        public DbSet<Product> Products => Set<Product>();
        public DbSet<ProductVariant> ProductVariants => Set<ProductVariant>();
        public DbSet<ProductVariantImage> ProductVariantImages => Set<ProductVariantImage>();

        public DbSet<ProductReview> ProductReviews => Set<ProductReview>();

        public DbSet<ProductImage> ProductImages => Set<ProductImage>();

        public DbSet<ProductFaq> ProductFaqs => Set<ProductFaq>();

        public DbSet<Cart> Carts => Set<Cart>();
        public DbSet<CartItem> CartItems => Set<CartItem>();

		public DbSet<Inventory> Inventories => Set<Inventory>();
		public DbSet<InventoryHistory> InventoryHistories => Set<InventoryHistory>();

		public DbSet<Order> Orders => Set<Order>();
		public DbSet<OrderItem> OrderItems => Set<OrderItem>();

		public DbSet<Payment> Payments => Set<Payment>();
		public DbSet<UserAddress> UserAddresses => Set<UserAddress>();
		public DbSet<WishlistItem> WishlistItems => Set<WishlistItem>();
		public DbSet<OtpRecord> OtpRecords => Set<OtpRecord>();
        public DbSet<Notification> Notifications => Set<Notification>();
        public DbSet<Coupon> Coupons => Set<Coupon>();
        public DbSet<ReturnRequest> ReturnRequests => Set<ReturnRequest>();
        public DbSet<AuditLog> AuditLogs => Set<AuditLog>();

        protected override void OnModelCreating(ModelBuilder builder)
		{
			base.OnModelCreating(builder);
            builder.Entity<ProductVariant>(entity =>
            {
                entity.Property(v => v.Price)
                      .HasColumnType("decimal(18,2)");

                entity.Property(v => v.DiscountPrice)
                      .HasColumnType("decimal(18,2)");
            });
            builder.Entity<ProductReview>()
                .HasOne(r => r.Product)
                .WithMany(p => p.Reviews)
                .HasForeignKey(r => r.ProductId);

            builder.Entity<ProductImage>()
                .HasOne(i => i.Product)
                .WithMany(p => p.Images)
                .HasForeignKey(i => i.ProductId);

            builder.Entity<ProductFaq>()
                .HasOne(f => f.Product)
                .WithMany(p => p.Faqs)
                .HasForeignKey(f => f.ProductId);

            builder.Entity<ProductVariant>()
    .HasOne(v => v.Product)
    .WithMany(p => p.Variants)
    .HasForeignKey(v => v.ProductId)
    .OnDelete(DeleteBehavior.Cascade);

            builder.Entity<ProductVariantImage>(entity =>
            {
                entity.HasOne(pvi => pvi.Variant)
                      .WithMany(v => v.Images)
                      .HasForeignKey(pvi => pvi.VariantId)
                      .OnDelete(DeleteBehavior.Cascade);
            });

            builder.Entity<OrderItem>(entity =>
			{
				entity.Property(oi => oi.UnitPrice).HasColumnType("decimal(18,2)");
			});

            builder.Entity<Payment>(entity =>
            {
                entity.Property(p => p.Amount)
                      .HasColumnType("decimal(18,2)");

                entity.HasOne(p => p.Order)
                      .WithMany(o => o.Payments)
                      .HasForeignKey(p => p.OrderId)
                      .OnDelete(DeleteBehavior.Cascade);
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
            builder.Entity<CartItem>()
           .HasOne(ci => ci.Variant)
          .WithMany()
          .HasForeignKey(ci => ci.VariantId)
          .OnDelete(DeleteBehavior.Restrict);

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

			builder.Entity<Inventory>(entity =>
			{
				entity.HasOne(i => i.Product)
					  .WithMany()
					  .HasForeignKey(i => i.ProductId)
					  .OnDelete(DeleteBehavior.Cascade);
                entity.Property(i => i.RowVersion)
      .IsRowVersion();
            });

			builder.Entity<InventoryHistory>(entity =>
			{
				entity.HasOne(h => h.Inventory)
					  .WithMany(i => i.History)
					  .HasForeignKey(h => h.InventoryId)
					  .OnDelete(DeleteBehavior.Cascade);
			});

            builder.Entity<Notification>(entity =>
            {
                entity.HasKey(x => x.Id);

                entity.HasOne(x => x.User)
                      .WithMany()
                      .HasForeignKey(x => x.UserId)
                      .OnDelete(DeleteBehavior.Cascade);
            });

            builder.Entity<Order>(entity =>
            {
                entity.Property(x => x.TotalAmount)
                      .HasColumnType("decimal(18,2)");

                entity.Property(x => x.DiscountAmount)
                      .HasColumnType("decimal(18,2)");
            });

            builder.Entity<Coupon>(entity =>
            {
                entity.Property(x => x.DiscountAmount)
                      .HasColumnType("decimal(18,2)");

                entity.Property(x => x.DiscountPercentage)
                      .HasColumnType("decimal(18,2)");

                entity.Property(x => x.MinimumOrderAmount)
                      .HasColumnType("decimal(18,2)");
            });

        }

	}
}