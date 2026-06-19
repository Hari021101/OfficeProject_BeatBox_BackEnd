using Application.DTOs;
using Domain.Entities;

namespace Application.Interfaces;

public interface ICategoryRepository
{
    Task<IEnumerable<Category>> GetAllAsync();

    Task<IEnumerable<CategoryResponseDto>> GetProjectedAllAsync();

    Task<Category?> GetByIdAsync(Guid id);

    Task AddAsync(Category category);

    Task UpdateAsync(Category category);

    Task DeleteAsync(Category category);

    Task<bool> SaveChangesAsync();
}