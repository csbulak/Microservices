using Shared.Dtos;
using WebUI.Models.PhotoSock;
using WebUI.Services.Interfaces;

namespace WebUI.Services;

public class PhotoStockService : IPhotoStockService
{
    private readonly HttpClient _httpClient;

    public PhotoStockService(HttpClient httpClient)
    {
        _httpClient = httpClient;
    }

    public async Task<PhotoViewModel> UploadPhoto(IFormFile? file)
    {
        if (file == null || file.Length <= 0)
        {
            return null;
        }

        var randomFileName = $"{Guid.NewGuid()}{Path.GetExtension(file.FileName)}";

        using var ms = new MemoryStream();
        await file.CopyToAsync(ms);

        var multipartContent = new MultipartFormDataContent();
        multipartContent.Add(new ByteArrayContent(ms.ToArray()), "photo", randomFileName);

        var response = await _httpClient.PostAsync($"Photo", multipartContent);

        if (!response.IsSuccessStatusCode)
        {
            return null;
        }

        return await response.Content.ReadFromJsonAsync<PhotoViewModel>() ?? null;
    }

    public async Task<bool> DeletePhoto(string photoUrl)
    {
        var response = await _httpClient.DeleteAsync($"photo?photoUrl={photoUrl}");

        return response.IsSuccessStatusCode;

    }
}