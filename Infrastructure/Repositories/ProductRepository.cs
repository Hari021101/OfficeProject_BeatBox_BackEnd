using Application.Interfaces;
using Domain.Entities;
using Infrastructure.Data;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.Repositories;

public class ProductRepository : IProductRepository
{
    private readonly AppDbContext _context;

    public ProductRepository(AppDbContext context)
    {
        _context = context;
    }

    public async Task<IEnumerable<Product>> GetAllAsync()
    {
        return await _context.Products
    .Include(p => p.Category)
    .Include(p => p.Reviews)
        .ThenInclude(r => r.User)
    .Include(p => p.Images)
    .Include(p => p.Faqs)
    .Include(p => p.Variants)
    .ThenInclude(v => v.Images)
    .AsNoTracking()
    .ToListAsync();
    }

    public async Task<Product?> GetByIdAsync(Guid id)
    {
        return await _context.Products
    .Include(p => p.Category)
    .Include(p => p.Reviews)
        .ThenInclude(r => r.User)
    .Include(p => p.Images)
    .Include(p => p.Faqs)
   .Include(p => p.Variants)
    .ThenInclude(v => v.Images)
    .FirstOrDefaultAsync(p => p.Id == id);
    }

    public async Task AddAsync(Product product)
    {
        await _context.Products.AddAsync(product);
        await _context.SaveChangesAsync();
    }

    public async Task UpdateAsync(Product product)
    {
        _context.Products.Update(product);
        await _context.SaveChangesAsync();
    }

    public async Task DeleteAsync(Guid id)
    {
        var product = await GetByIdAsync(id);
        if (product != null)
        {
            _context.Products.Remove(product);
            await _context.SaveChangesAsync();
        }
    }

    public async Task<IEnumerable<Product>> SearchAsync(string searchTerm)
    {
        return await _context.Products
            .Where(p => p.Name.Contains(searchTerm) || p.Description.Contains(searchTerm))
            .Include(p => p.Category)
            .Include(p => p.Variants)
    .ThenInclude(v => v.Images)
            .ToListAsync();
    }

    public async Task<IEnumerable<Product>> FilterAsync(decimal? minPrice, decimal? maxPrice, string? brand, string? color)
    {
        var query = _context.Products.AsQueryable();

        if (minPrice.HasValue)
        {
            query = query.Where(p =>
                p.Variants.Any(v => v.Price >= minPrice.Value));
        }

        if (maxPrice.HasValue)
        {
            query = query.Where(p =>
                p.Variants.Any(v => v.Price <= maxPrice.Value));
        }

        if (!string.IsNullOrEmpty(color))
        {
            query = query.Where(p =>
                p.Variants.Any(v => v.Color == color));
        }

        if (!string.IsNullOrEmpty(brand))
            query = query.Where(p => p.Brand == brand);

        return await query.Include(p => p.Category)
.Include(p => p.Variants)
    .ThenInclude(v => v.Images).ToListAsync();
    }

    public async Task<IEnumerable<Product>> GetPagedAsync(int pageNumber, int pageSize)
    {
        return await _context.Products
           .Include(p => p.Category)
.Include(p => p.Variants)
    .ThenInclude(v => v.Images)
            .Skip((pageNumber - 1) * pageSize)
            .Take(pageSize)
            .ToListAsync();
    }
}