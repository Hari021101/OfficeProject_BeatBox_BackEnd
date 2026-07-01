using Application.DTOs;
using Application.Interfaces;
using AutoMapper;
using Domain.Entities;

namespace Infrastructure.Services;

public class CategoryService : ICategoryService
{
    private readonly ICategoryRepository _repository;
    private readonly IMapper _mapper;
    private readonly IBusinessEventPublisher _eventPublisher;

    public CategoryService(
        ICategoryRepository repository,
        IMapper mapper,
        IBusinessEventPublisher eventPublisher)
    {
        _repository = repository;
        _mapper = mapper;
        _eventPublisher = eventPublisher;
    }

    public async Task<IEnumerable<CategoryResponseDto>> GetAllAsync()
    {
        return await _repository.GetProjectedAllAsync();
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

        await _eventPublisher.PublishAsync(new Application.Common.Events.BusinessEvent
        {
            ActionType = "CREATED",
            EntityType = "Category",
            EntityId = category.Id.ToString(),
            Title = category.Name,
            Description = $"Category '{category.Name}' created by Administrator",
            Icon = "PlusCircle",
            ColorClass = "text-success",
            BgClass = "bg-success"
        });
    }

    public async Task UpdateAsync(Guid id, CategoryUpdateDto dto)
    {
        var category = await _repository.GetByIdAsync(id);

        if (category == null)
            throw new Exception("Category not found");

        _mapper.Map(dto, category);

        await _repository.UpdateAsync(category);

        await _repository.SaveChangesAsync();

        await _eventPublisher.PublishAsync(new Application.Common.Events.BusinessEvent
        {
            ActionType = "UPDATED",
            EntityType = "Category",
            EntityId = category.Id.ToString(),
            Title = category.Name,
            Description = $"Category '{category.Name}' details updated by Administrator",
            Icon = "Edit",
            ColorClass = "text-info",
            BgClass = "bg-info"
        });
    }

    public async Task DeleteAsync(Guid id)
    {
        var category = await _repository.GetByIdAsync(id);

        if (category == null)
            throw new Exception("Category not found");

        await _repository.DeleteAsync(category);

        await _repository.SaveChangesAsync();

        await _eventPublisher.PublishAsync(new Application.Common.Events.BusinessEvent
        {
            ActionType = "DELETED",
            EntityType = "Category",
            EntityId = id.ToString(),
            Title = category.Name,
            Description = $"Category '{category.Name}' deleted by Administrator",
            Icon = "Trash2",
            ColorClass = "text-danger",
            BgClass = "bg-danger"
        });
    }
}