# BeatBox Backend: Technical Interview Preparation Guide

This guide compiles the key architectural, .NET, and database concepts used throughout the **BeatBox Backend** project with exact references to the codebase.

---

## 1. Dependency Injection (DI)

### Concept
- **Dependency Injection** is a design pattern used to achieve Inversion of Control (IoC). Rather than creating instances using `new`, dependencies are registered in an IoC container and injected via class constructors.
- **Service Lifetimes**:
  - **Transient** (`AddTransient`): Created each time they are requested. Ideal for lightweight, stateless services.
  - **Scoped** (`AddScoped`): Created once per HTTP request lifecycle. Used for `DbContext`, Repositories, and Unit of Work.
  - **Singleton** (`AddSingleton`): Created once upon initial request or app startup and shared across the entire application lifetime. Used for memory caching and thread-safe shared state.

### Implementation in BeatBox
- **Service Registration**: [Infrastructure/DependencyInjection.cs](file:///c:/Users/Mhari/source/repos/OfficeProject_BeatBox_BackEnd/Infrastructure/DependencyInjection.cs#L21-L165)
  - `AppDbContext` is registered with `AddDbContext<AppDbContext>` (Scoped).
  - Repositories (`IProductRepository`, `IOrderRepository`) and Services (`IProductService`, `IOrderService`) are registered via `AddScoped`.
  - Configuration options are registered via `services.AddOptions<FrontendOptions>().ValidateOnStart()`.
- **Constructor Injection**:
  - [ProductController.cs](file:///c:/Users/Mhari/source/repos/OfficeProject_BeatBox_BackEnd/API/Controllers/ProductController.cs#L20-L24):
    ```csharp
    public ProductController(IProductService productService, IMemoryCache cache)
    {
        _productService = productService;
        _cache = cache;
    }
    ```

---

## 2. Async & Await

### Concept
- Implements the **Task-based Asynchronous Pattern (TAP)**.
- Frees up the ASP.NET thread pool during I/O bound operations (database queries, network requests, email dispatch), preventing **thread pool starvation** under high concurrent load.

### Implementation in BeatBox
- **Non-blocking Data Access**: [ProductRepository.cs](file:///c:/Users/Mhari/source/repos/OfficeProject_BeatBox_BackEnd/Infrastructure/Repositories/ProductRepository.cs#L17-L30):
  ```csharp
  public async Task<IEnumerable<Product>> GetAllAsync()
  {
      return await _context.Products
          .Include(p => p.Category)
          .AsSplitQuery()
          .AsNoTracking()
          .ToListAsync();
  }
  ```
- **Asynchronous Database Transactions**: [OrderService.cs](file:///c:/Users/Mhari/source/repos/OfficeProject_BeatBox_BackEnd/Infrastructure/Services/OrderService.cs#L61-L85):
  ```csharp
  using var tx = await _context.Database.BeginTransactionAsync();
  try
  {
      // ... process order and inventory
      await _context.SaveChangesAsync();
      await tx.CommitAsync();
  }
  catch
  {
      await tx.RollbackAsync();
      throw;
  }
  ```

---

## 3. IEnumerable vs IQueryable

### Concept
- **`IQueryable<T>`**:
  - Implements expression trees (`Expression<Func<T, bool>>`).
  - Evaluated **server-side** in the database engine.
  - Generates optimized SQL (`WHERE`, `ORDER BY`, `OFFSET ... ROWS FETCH NEXT ... ROWS ONLY`).
- **`IEnumerable<T>`**:
  - Uses in-memory delegates (`Func<T, bool>`).
  - Evaluated **client-side** in application memory.
  - Pulls records from the database before filtering.

### Implementation in BeatBox
- **Deferred Execution & Dynamic Filtering**: [ProductRepository.cs](file:///c:/Users/Mhari/source/repos/OfficeProject_BeatBox_BackEnd/Infrastructure/Repositories/ProductRepository.cs#L79-L118):
  ```csharp
  public async Task<IEnumerable<Product>> FilterAsync(decimal? minPrice, decimal? maxPrice, string? brand, string? color)
  {
      var query = _context.Products.AsQueryable(); // Remains an IQueryable

      if (minPrice.HasValue)
          query = query.Where(p => p.Variants.Any(v => v.Price >= minPrice.Value));

      if (!string.IsNullOrEmpty(brand))
          query = query.Where(p => p.Brand == brand);

      // The SQL query with all WHERE filters is compiled and executed here:
      return await query.Include(p => p.Category).ToListAsync();
  }
  ```
- **Database Pagination**:
  ```csharp
  query.Skip((pageNumber - 1) * pageSize).Take(pageSize).ToListAsync();
  ```

---

## 4. Database Indexing

### Concept
- An index organizes table data using a **B-Tree** structure, allowing $O(\log N)$ seeks instead of $O(N)$ full table scans.
- **Clustered Index**: Determines the physical ordering of records in storage. Only 1 exists per table (typically the Primary Key).
- **Non-Clustered Index**: A secondary lookup structure with pointers to the data rows.
- **Filtered Index**: An index applied to a subset of rows using a `WHERE` condition.

### Implementation in BeatBox
- Configured in [Infrastructure/Data/AppDbContext.cs](file:///c:/Users/Mhari/source/repos/OfficeProject_BeatBox_BackEnd/Infrastructure/Data/AppDbContext.cs#L52-L275):
  - **Unique Index on Coupon Code**:
    ```csharp
    builder.Entity<Coupon>().HasIndex(x => x.Code).IsUnique();
    ```
  - **Filtered Unique Index on Referral Code** (Allows NULLs, guarantees uniqueness when present):
    ```csharp
    builder.Entity<AppUser>()
        .HasIndex(u => u.ReferralCode)
        .HasFilter("[ReferralCode] IS NOT NULL")
        .IsUnique();
    ```
  - **Composite Filtered Unique Index**:
    ```csharp
    builder.Entity<StockNotificationSubscription>()
        .HasIndex(s => new { s.UserId, s.ProductVariantId, s.IsActive })
        .HasFilter("[IsActive] = 1")
        .IsUnique();
    ```

---

## 5. Stored Procedures vs EF Core

### Concept
- **EF Core LINQ**: Primary data access layer for BeatBox. Offers compile-time safety, LINQ syntax, and automated migrations.
- **Stored Procedures**: Precompiled SQL routines stored on the server. Used for heavy reporting, complex multi-statement transactions, or restricted access environments.

### Executing Stored Procedures in BeatBox
```csharp
// 1. Reading entities via Stored Procedure
var param = new SqlParameter("@CategoryId", categoryId);
var products = await _context.Products
    .FromSqlRaw("EXEC sp_GetProductsByCategory @CategoryId", param)
    .ToListAsync();

// 2. Executing batch non-query operations
var orderParam = new SqlParameter("@OrderId", orderId);
await _context.Database.ExecuteSqlRawAsync("EXEC sp_ProcessOrderInventory @OrderId", orderParam);
```

---

## 6. LINQ & EF Core Performance Optimizations

### Key Techniques in BeatBox
1. **`.AsNoTracking()`**:
   - Used in read-only endpoints (such as dashboard summaries and search queries) to bypass change tracker snapshot overhead.
   - Example: [AdminDashboardRepository.cs](file:///c:/Users/Mhari/source/repos/OfficeProject_BeatBox_BackEnd/Infrastructure/Repositories/AdminDashboardRepository.cs#L25-L29).
2. **`.AsSplitQuery()`**:
   - Prevents Cartesian explosion when eager loading multiple navigation collections (`Reviews`, `Images`, `Variants`, `Faqs`).
   - Example: [ProductRepository.cs](file:///c:/Users/Mhari/source/repos/OfficeProject_BeatBox_BackEnd/Infrastructure/Repositories/ProductRepository.cs#L27).
3. **Aggregations & Grouping**:
   - Calculating top-selling products in [AdminDashboardRepository.cs](file:///c:/Users/Mhari/source/repos/OfficeProject_BeatBox_BackEnd/Infrastructure/Repositories/AdminDashboardRepository.cs#L177-L185):
     ```csharp
     var topProducts = await _context.OrderItems
         .AsNoTracking()
         .GroupBy(oi => oi.ProductId)
         .Select(g => new {
             ProductId = g.Key,
             UnitsSold = g.Sum(x => x.Quantity),
             Revenue = g.Sum(x => x.Quantity * x.UnitPrice)
         })
         .OrderByDescending(x => x.UnitsSold)
         .Take(5)
         .ToListAsync();
     ```
