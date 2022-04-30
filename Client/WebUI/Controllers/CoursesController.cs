using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Shared.Services;
using WebUI.Models.Catalog;
using WebUI.Services.Interfaces;

namespace WebUI.Controllers;

public class CoursesController : Controller
{
    private readonly ICatalogService _catalogService;
    private readonly ISharedIdentityService _sharedIdentityService;
    // GET
    public CoursesController(ICatalogService catalogService, ISharedIdentityService sharedIdentityService)
    {
        _catalogService = catalogService;
        _sharedIdentityService = sharedIdentityService;
    }

    public async Task<IActionResult> Index()
    {
        var courses = await _catalogService.GetAllCourseByUserId(_sharedIdentityService.GetUserId);
        return View(courses);
    }

    public async Task<IActionResult> Create()
    {
        var categories = await _catalogService.GetAllCategories();
        ViewBag.Categories = new SelectList(categories, "Id", "Name");
        return View();
    }

    [HttpPost]
    public async Task<IActionResult> Create(CourseCreateInput input)
    {
        var categories = await _catalogService.GetAllCategories();
        //if (!ModelState.IsValid)
        //{
        //    ViewBag.Categories = new SelectList(categories, "Id", "Name");
        //    return View();
        //}
        input.UserId = _sharedIdentityService.GetUserId;
        var response = await _catalogService.CreateCourseAsync(input);

        if (response)
        {
            return RedirectToAction("Index", "Courses");
        }

        ViewBag.Categories = new SelectList(categories, "Id", "Name");
        return View();
    }
}