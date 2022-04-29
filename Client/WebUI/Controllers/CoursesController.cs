using Microsoft.AspNetCore.Mvc;
using Shared.Services;
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
}