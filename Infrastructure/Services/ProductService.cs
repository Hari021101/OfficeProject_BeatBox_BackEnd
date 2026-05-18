using Application.DTOs;
using Application.Interfaces;
using AutoMapper;
using Domain.Entities;

namespace Infrastructure.Services;

public class ProductService : IProductService
{
    private readonly IProductRepository _productRepository;
    private readonly IMapper _mapper;

    public ProductService(IProductRepository productRepository, IMapper mapper)
    {
        _productRepository = productRepository;
        _mapper = mapper;
    }

    public async Task<IEnumerable<ProductResponseDto>> GetAllProductsAsync()
    {
        var products = await _productRepository.GetAllAsync();
        return _mapper.Map<IEnumerable<ProductResponseDto>>(products);
    }

    public async Task<ProductResponseDto?> GetProductByIdAsync(Guid id)
    {
        var product = await _productRepository.GetByIdAsync(id);
        return product == null ? null : _mapper.Map<ProductResponseDto>(product);
    }

    public async Task AddProductAsync(ProductCreateDto productCreateDto)
    {
        var product = _mapper.Map<Product>(productCreateDto);
        await _productRepository.AddAsync(product);
    }

    public async Task UpdateProductAsync(Guid id, ProductUpdateDto productUpdateDto)
    {
        var existingProduct = await _productRepository.GetByIdAsync(id);
        if (existingProduct == null) return;

        _mapper.Map(productUpdateDto, existingProduct);
        await _productRepository.UpdateAsync(existingProduct);
    }

    public async Task DeleteProductAsync(Guid id)
    {
        await _productRepository.DeleteAsync(id);
    }

    public async Task<IEnumerable<ProductResponseDto>> SearchProductsAsync(string searchTerm)
    {
        var products = await _productRepository.SearchAsync(searchTerm);
        return _mapper.Map<IEnumerable<ProductResponseDto>>(products);
    }

    public async Task<IEnumerable<ProductResponseDto>> FilterProductsAsync(decimal? minPrice, decimal? maxPrice, string? brand, string? color)
    {
        var products = await _productRepository.FilterAsync(minPrice, maxPrice, brand, color);
        return _mapper.Map<IEnumerable<ProductResponseDto>>(products);
    }

    public async Task<IEnumerable<ProductResponseDto>> GetPagedProductsAsync(int pageNumber, int pageSize)
    {
        var products = await _productRepository.GetPagedAsync(pageNumber, pageSize);
        return _mapper.Map<IEnumerable<ProductResponseDto>>(products);
    }
}