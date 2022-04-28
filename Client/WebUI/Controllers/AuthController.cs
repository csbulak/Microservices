using Microsoft.AspNetCore.Mvc;

namespace WebUI.Controllers;

public class AuthController : Controller
{
    // GET
    public IActionResult SignIn()
    {
        return View();
    }
}