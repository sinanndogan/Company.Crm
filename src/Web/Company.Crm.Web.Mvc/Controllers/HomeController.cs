using Company.Crm.Web.Mvc.Models;
using Microsoft.AspNetCore.Mvc;
using System.Diagnostics;

namespace Company.Crm.Web.Mvc.Controllers;

//[SampleActionFilter("Cem")]
public class HomeController : Controller
{
    private readonly ILogger<HomeController> _logger;

    public HomeController(ILogger<HomeController> logger)
    {
        _logger = logger;
    }

    //[ResponseHeader("Egitim", "Filter yapisi")]
    //[SampleActionFilter("Cem")]
    //[AuthorizePermission("Permission.Create.Sale")]
    public async Task<IActionResult> Index()
    {
        return View();
    }

    public IActionResult Privacy()
    {
        return View();
    }

    [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
    public IActionResult Error()
    {
        return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
    }

    [HttpGet("/Error/{errorCode}")]
    public IActionResult StatusError(string errorCode)
    {
        ViewBag.ErrorCode = errorCode;

        return View();
    }
}