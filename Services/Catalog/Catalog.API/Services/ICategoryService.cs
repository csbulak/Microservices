using Catalog.API.Dtos;
using Catalog.API.Models;
using Shared.Dtos;

namespace Catalog.API.Services;

public interface ICategoryService
{
    Task<Response<List<CategoryDto>>> GetAllAsync();
    Task<Response<CategoryDto>> CreateAsync(CategoryDto categoryDto);
    Task<Response<CategoryDto>> GetByIdAsync(string id);
}