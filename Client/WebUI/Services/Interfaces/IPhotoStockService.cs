using WebUI.Models.PhotoSock;

namespace WebUI.Services.Interfaces;

public interface IPhotoStockService
{
    Task<PhotoViewModel> UploadPhoto(IFormFile? file);
    Task<bool> DeletePhoto(string photoUrl);
}