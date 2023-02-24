using Company.Crm.Application.Constants;
using Company.Crm.Application.Dtos.List;
using Company.Crm.Application.Services.Abstracts;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Company.Crm.Web.Mvc.Areas.Admin.Controllers;

[Authorize(Roles = RoleNameConsts.Administrator)]
[Area("Admin")]
public class GenderController : Controller
{
    private readonly IGenderService _genderService;

    public GenderController(IGenderService genderService)
    {
        _genderService = genderService;
    }

    public IActionResult Index()
    {
        var genderList = _genderService.GetAll();

        return View(genderList);
    }

    public async Task<PartialViewResult> Detail(int id)
    {
        var gender = _genderService.GetById(id);
        return PartialView("_Detail", gender);
    }

    [HttpGet]
    public PartialViewResult Create()
    {
        var gender = new GenderDto();

        return PartialView("_Create", gender);
    }

    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<ActionResult> Create(GenderDto gender)
    {
        try
        {
            if (ModelState.IsValid)
            {
                var isInserted = _genderService.Insert(gender);
                if (isInserted) return Json(new { IsSuccess = true, Redirect = Url.Action("Index") });
            }
        }
        catch
        {
            ModelState.AddModelError("", "Unable to save changes.");
        }

        return PartialView("_Create", gender);
    }

    public async Task<PartialViewResult> Edit(int? id)
    {
        var gender = new GenderDto();

        if (id.HasValue)
            gender = _genderService.GetById(id.Value);

        return PartialView("_Edit", gender);
    }

    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<ActionResult> Edit(GenderDto gender)
    {
        try
        {
            if (ModelState.IsValid)
            {
                var isUpdated = _genderService.Update(gender);
                if (isUpdated) return Json(new { IsSuccess = true, Redirect = Url.Action("Index") });
            }
        }
        catch
        {
            ModelState.AddModelError("", "Unable to save changes.");
        }

        return PartialView("_Edit", gender);
    }

    [HttpGet]
    public async Task<PartialViewResult> Delete(int id)
    {
        var gender = _genderService.GetById(id);

        return PartialView("_Delete", gender);
    }

    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<ActionResult> DeleteConfirmed(int id)
    {
        return Json(new { IsSuccess = _genderService.DeleteById(id), Redirect = Url.Action("Index") });
    }
}