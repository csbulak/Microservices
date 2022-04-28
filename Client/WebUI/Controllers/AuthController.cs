using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Mvc;
using WebUI.Models;
using WebUI.Services.Interfaces;

namespace WebUI.Controllers;

public class AuthController : Controller
{
    private readonly IIdentityService _identityService;

    // GET
    public AuthController(IIdentityService identityService)
    {
        _identityService = identityService;
    }

    public IActionResult SignIn()
    {
        return View();
    }


    [HttpPost]
    public async Task<IActionResult> SignIn(SignInInput signInInput)
    {
        if (!ModelState.IsValid)
        {
            return View();
        }

        var result = await _identityService.SignIn(signInInput);

        if (result.IsSuccessful) return RedirectToAction("Index", "Home");

        result.Errors.ForEach(x =>
        {
            ModelState.AddModelError(string.Empty, x);
        });

        return View();

    }

    public async Task<IActionResult> Logout()
    {
        await HttpContext.SignOutAsync(CookieAuthenticationDefaults.AuthenticationScheme);

        await _identityService.RemoveRefreshToken();

        return RedirectToAction("Index", "Home");
    }
}