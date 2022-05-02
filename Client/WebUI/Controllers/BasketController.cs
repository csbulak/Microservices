using Microsoft.AspNetCore.Mvc;
using WebUI.Models.Basket;
using WebUI.Services.Interfaces;

namespace WebUI.Controllers;

public class BasketController : Controller
{
    private readonly IBasketService _basketService;
    private readonly ICatalogService _catalogService;

    // GET
    public BasketController(IBasketService basketService, ICatalogService catalogService)
    {
        _basketService = basketService;
        _catalogService = catalogService;
    }

    public async Task<IActionResult> Index()
    {
        var basket = await _basketService.Get();
        return View(basket);
    }

    public async Task<IActionResult> AddBasketItem(string courseId)
    {
        var course = await _catalogService.GetCourseById(courseId);

        var basketItemViewModel = new BasketItemViewModel()
        {
            CourseId = course.Id,
            CourseName = course.Name,
            Price = course.Price,
            Quantity = 1
        };

        await _basketService.AddBasketItem(basketItemViewModel);

        return RedirectToAction("Index");
    }

    public async Task<IActionResult> RemoveBasketItem(string courseId)
    {
        var response = await _basketService.RemoveBasketItem(courseId);
        return RedirectToAction("Index");
    }
}