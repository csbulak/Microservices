using WebUI.Models.Basket;

namespace WebUI.Services.Interfaces;

public interface IBasketService
{
    Task<bool> SaveOrUpdate(BasketViewModel basketViewModel);
    Task<bool> Delete();

    Task<BasketViewModel> Get();

    Task AddBasketItem(BasketItemViewModel basketItemViewModel);
    Task<bool> RemoveBasketItem(string courseId);
    Task<bool> ApplyDiscount(string discountCode);
    Task<bool> CancelApplyDiscount();
}