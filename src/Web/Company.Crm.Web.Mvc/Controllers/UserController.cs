using Microsoft.AspNetCore.Mvc;

namespace Company.Crm.Web.Mvc.Controllers;

public class UserController : Controller
{
    private readonly ILogger<UserController> _logger;

    public UserController(ILogger<UserController> logger)
    {
        _logger = logger;
    }

    public IActionResult Profile()
    {
        return View();
    }

    public IActionResult Settings()
    {
        return View();
    }
}