﻿using Shared.Dtos;
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

    public async Task<PhotoViewModel> UploadPhoto(IFormFile photo)
    {
        if (photo == null || photo.Length <= 0)
        {
            return null;
        }
        // örnek dosya ismi= 203802340234.jpg
        var randonFilename = $"{Guid.NewGuid().ToString()}{Path.GetExtension(photo.FileName)}";

        using var ms = new MemoryStream();

        await photo.CopyToAsync(ms);

        var multipartContent = new MultipartFormDataContent();

        multipartContent.Add(new ByteArrayContent(ms.ToArray()), "photo", randonFilename);

        var response = await _httpClient.PostAsync("photo", multipartContent);

        if (!response.IsSuccessStatusCode)
        {
            return null;
        }

        var responseSuccess = await response.Content.ReadFromJsonAsync<Response<PhotoViewModel>>();

        return responseSuccess.Data;
    }

    public async Task<bool> DeletePhoto(string photoUrl)
    {
        var response = await _httpClient.DeleteAsync($"photo?photoUrl={photoUrl}");
        return response.IsSuccessStatusCode;
    }
}