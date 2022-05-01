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

    public Task<PhotoViewModel> UploadPhoto(IFormFile file)
    {
        throw new NotImplementedException();
    }

    public Task<bool> DeletePhoto(string photoUrl)
    {
        throw new NotImplementedException();
    }
}