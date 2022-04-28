using WebUI.Models;
using WebUI.Services.Interfaces;

namespace WebUI.Services;

public class UserService : IUserService
{
    private readonly HttpClient _httpClient;

    public UserService(HttpClient httpClient)
    {
        _httpClient = httpClient;
    }

    public async Task<UserViewModel?> GetUser()
    {
        return await _httpClient.GetFromJsonAsync<UserViewModel>("/api/User/GetUser");
    }
}