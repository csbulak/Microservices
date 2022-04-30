using Shared.Dtos;
using WebUI.Models;
using WebUI.Models.Catalog;
using WebUI.Services.Interfaces;

namespace WebUI.Services;

public class CatalogService : ICatalogService
{
    private readonly HttpClient _httpClient;

    public CatalogService(HttpClient httpClient)
    {
        _httpClient = httpClient;
    }

    public async Task<List<CourseViewModel>> GetAllCourse()
    {
        var response = await _httpClient.GetAsync("Courses");
        if (!response.IsSuccessStatusCode)
        {
            return new List<CourseViewModel>();
        }

        var data = await response.Content.ReadFromJsonAsync<Response<List<CourseViewModel>>>();
        return data.Data ?? new List<CourseViewModel>();
    }

    public async Task<List<CourseViewModel>> GetAllCourseByUserId(string userId)
    {
        var response = await _httpClient.GetAsync($"Courses/GetAllByUserId/{userId}");
        if (!response.IsSuccessStatusCode)
        {
            return new List<CourseViewModel>();
        }

        var data = await response.Content.ReadFromJsonAsync<Response<List<CourseViewModel>>>();
        return data?.Data ?? new List<CourseViewModel>();
    }

    public async Task<CourseViewModel> GetCourseById(string courseId)
    {
        var response = await _httpClient.GetAsync($"Courses/{courseId}");
        if (!response.IsSuccessStatusCode)
        {
            return null;
        }

        var data = await response.Content.ReadFromJsonAsync<Response<CourseViewModel>>();
        return data?.Data;
    }

    public async Task<List<CategoryViewModel>> GetAllCategories()
    {
        var response = await _httpClient.GetAsync("Categories");
        if (!response.IsSuccessStatusCode)
        {
            return new List<CategoryViewModel>();
        }

        var data = await response.Content.ReadFromJsonAsync<Response<List<CategoryViewModel>>>();
        return data.Data ?? new List<CategoryViewModel>();
    }


    public async Task<CategoryViewModel> GetCategoryById(string categoryId)
    {
        var response = await _httpClient.GetAsync($"Categories/{categoryId}");
        if (!response.IsSuccessStatusCode)
        {
            return new CategoryViewModel();
        }

        var data = await response.Content.ReadFromJsonAsync<Response<CategoryViewModel>>();
        return data.Data ?? new CategoryViewModel();
    }

    public async Task<bool> CreateCourseAsync(CourseCreateInput input)
    {
        var response = await _httpClient.PostAsJsonAsync<CourseCreateInput>($"Courses", input);
        return response.IsSuccessStatusCode;
    }

    public async Task<bool> UpdateCourseAsync(CourseUpdateInput input)
    {
        var response = await _httpClient.PutAsJsonAsync<CourseUpdateInput>("Courses", input);
        return response.IsSuccessStatusCode;
    }

    public async Task<bool> DeleteCourseAsync(string courseId)
    {
        var response = await _httpClient.DeleteAsync($"Courses/{courseId}");
        return response.IsSuccessStatusCode;
    }

    public async Task<bool> CreateCategoryAsync(CategoryCreateInput input)
    {
        var response = await _httpClient.PostAsJsonAsync<CategoryCreateInput>("Categories", input);
        return response.IsSuccessStatusCode;
    }
}