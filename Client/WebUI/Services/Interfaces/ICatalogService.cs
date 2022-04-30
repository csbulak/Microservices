using Shared.Dtos;
using WebUI.Models.Catalog;

namespace WebUI.Services.Interfaces;

public interface ICatalogService
{
    Task<Response<List<CourseViewModel>>> GetAllCourse();
    Task<Response<List<CourseViewModel>>> GetAllCourseByUserId(string userId);
    Task<Response<CourseViewModel>> GetCourseById(string courseId);

    Task<Response<List<CategoryViewModel>>> GetAllCategories();
    Task<Response<CategoryViewModel>> GetCategoryById(string categoryId);

    Task<bool> CreateCourseAsync(CourseCreateInput input);
    Task<bool> UpdateCourseAsync(CourseUpdateInput input);
    Task<bool> DeleteCourseAsync(string courseId);

    Task<bool> CreateCategoryAsync(CategoryCreateInput input);
}