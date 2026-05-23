using Application.DTOs;
using Application.Interfaces;
using AutoMapper;
using Domain.Entities;

namespace Infrastructure.Services;

public class CategoryService : ICategoryService
{
    private readonly ICategoryRepository _repository;
    private readonly IMapper _mapper;

    public CategoryService(
        ICategoryRepository repository,
        IMapper mapper)
    {
        _repository = repository;
        _mapper = mapper;
    }

    public async Task<IEnumerable<CategoryResponseDto>> GetAllAsync()
    {
        var categories = await _repository.GetAllAsync();

        return _mapper.Map<IEnumerable<CategoryResponseDto>>(categories);
    }

    public async Task<CategoryResponseDto?> GetByIdAsync(Guid id)
    {
        var category = await _repository.GetByIdAsync(id);

        return category == null
            ? null
            : _mapper.Map<CategoryResponseDto>(category);
    }

    public async Task AddAsync(CategoryCreateDto dto)
    {
        var category = _mapper.Map<Category>(dto);

        await _repository.AddAsync(category);

        await _repository.SaveChangesAsync();
    }

    public async Task UpdateAsync(Guid id, CategoryUpdateDto dto)
    {
        var category = await _repository.GetByIdAsync(id);

        if (category == null)
            throw new Exception("Category not found");

        _mapper.Map(dto, category);

        await _repository.UpdateAsync(category);

        await _repository.SaveChangesAsync();
    }

    public async Task DeleteAsync(Guid id)
    {
        var category = await _repository.GetByIdAsync(id);

        if (category == null)
            throw new Exception("Category not found");

        await _repository.DeleteAsync(category);

        await _repository.SaveChangesAsync();
    }
}