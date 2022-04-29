using WebUI.Models;
using WebUI.Models.Catalog;
using WebUI.Services.Interfaces;

namespace WebUI.Services;

public class CatalogService : ICatalogService
{
    public Task<List<CourseViewModel>> GetAllCourse()
    {
        throw new NotImplementedException();
    }

    public Task<List<CourseViewModel>> GetAllCourseByUserId(string userId)
    {
        throw new NotImplementedException();
    }

    public Task<CourseViewModel> GetCourseById(string courseId)
    {
        throw new NotImplementedException();
    }

    public Task<List<CategoryViewModel>> GetAllCategories()
    {
        throw new NotImplementedException();
    }

    public Task<List<CategoryViewModel>> GetCategoriesByUserId(string userId)
    {
        throw new NotImplementedException();
    }

    public Task<CategoryViewModel> GetCategoryById(string categoryId)
    {
        throw new NotImplementedException();
    }

    public Task<bool> CreateCourseAsync(CourseCreateInput input)
    {
        throw new NotImplementedException();
    }

    public Task<bool> UpdateCourseAsync(CourseUpdateInput input)
    {
        throw new NotImplementedException();
    }

    public Task<bool> DeleteCourseAsync(string courseId)
    {
        throw new NotImplementedException();
    }

    public Task<bool> CreateCategoryAsync(CategoryCreateInput input)
    {
        throw new NotImplementedException();
    }
}