using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using WebUI.Services.Interfaces;

namespace WebUI.Controllers;


[Authorize]
public class UserController : Controller
{
    private readonly IUserService _userService;

    // GET
    public UserController(IUserService userService)
    {
        _userService = userService;
    }

    public async Task<IActionResult> Index()
    {
        var user = await _userService.GetUser();

        return View(user);
    }
}