using Application.DTOs;

namespace Application.Interfaces;

public interface ICategoryService
{
    Task<IEnumerable<CategoryResponseDto>> GetAllAsync();

    Task<CategoryResponseDto?> GetByIdAsync(Guid id);

    Task AddAsync(CategoryCreateDto dto);

    Task UpdateAsync(Guid id, CategoryUpdateDto dto);

    Task DeleteAsync(Guid id);
}