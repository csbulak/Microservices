using Shared.Dtos;
using Shared.Services;
using WebUI.Models.Basket;
using WebUI.Services.Interfaces;

namespace WebUI.Services;

public class BasketService : IBasketService
{
    private readonly HttpClient _httpClient;

    public BasketService(HttpClient httpClient)
    {
        _httpClient = httpClient;
    }

    public async Task<bool> SaveOrUpdate(BasketViewModel basketViewModel)
    {
        var response = await _httpClient.PostAsJsonAsync<BasketViewModel>("Baskets", basketViewModel);
        return response.IsSuccessStatusCode;
    }

    public async Task<bool> Delete()
    {
        var response = await _httpClient.GetAsync("Baskets");
        return response.IsSuccessStatusCode;
    }

    public async Task<BasketViewModel> Get()
    {
        var response = await _httpClient.GetAsync("Baskets");
        if (!response.IsSuccessStatusCode)
        {
            return null;
        }
        var basketViewModel = await response.Content.ReadFromJsonAsync<Response<BasketViewModel>>();
        return basketViewModel.Data;
    }

    public async Task AddBasketItem(BasketItemViewModel basketItemViewModel)
    {
        var basket = await Get();

        if (basket != null)
        {
            if (basket._basketItems.All(x => x.CourseId != basketItemViewModel.CourseId))
            {
                basket._basketItems.Add(basketItemViewModel);
            }
        }
        else
        {
            basket = new BasketViewModel()
            {
                _basketItems = new List<BasketItemViewModel>() { basketItemViewModel }
            };
        }

        await SaveOrUpdate(basket);
    }

    public async Task<bool> RemoveBasketItem(string courseId)
    {
        var basket = await Get();
        if (basket == null)
        {
            return false;
        }

        var deleteBasketItem = basket._basketItems.FirstOrDefault(x => x.CourseId == courseId);

        if (deleteBasketItem == null)
        {
            return false;
        }

        var deleteResult = basket._basketItems.Remove(deleteBasketItem);

        if (!deleteResult)
        {
            return false;
        }

        if (!basket._basketItems.Any())
        {
            basket.DiscountCode = null;
        }

        return await SaveOrUpdate(basket);
    }

    public Task<bool> ApplyDiscount(string discountCode)
    {
        throw new NotImplementedException();
    }

    public Task<bool> CancelApplyDiscount()
    {
        throw new NotImplementedException();
    }
}