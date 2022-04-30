using Shared.Dtos;
using WebUI.Models.Catalog;

namespace WebUI.Services.Interfaces;

public interface ICatalogService
{
    Task<List<CourseViewModel>> GetAllCourse();
    Task<List<CourseViewModel>> GetAllCourseByUserId(string userId);
    Task<CourseViewModel> GetCourseById(string courseId);

    Task<List<CategoryViewModel>> GetAllCategories();
    Task<CategoryViewModel> GetCategoryById(string categoryId);

    Task<bool> CreateCourseAsync(CourseCreateInput input);
    Task<bool> UpdateCourseAsync(CourseUpdateInput input);
    Task<bool> DeleteCourseAsync(string courseId);

    Task<bool> CreateCategoryAsync(CategoryCreateInput input);
}