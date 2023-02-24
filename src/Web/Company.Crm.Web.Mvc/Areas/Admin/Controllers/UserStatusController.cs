using Company.Crm.Application.Constants;
using Company.Crm.Application.Services.Abstracts;
using Company.Crm.Domain.Entities.Lst;
using Company.Framework.Dtos;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Company.Crm.Web.Mvc.Areas.Admin.Controllers;

[Authorize(Roles = RoleNameConsts.Administrator)]
[Area("Admin")]
public class UserStatusController : Controller
{
    private readonly IUserStatusService _userStatusService;

    public UserStatusController(IUserStatusService userStatusService)
    {
        _userStatusService = userStatusService;
    }

    public IActionResult Index(PaginationRequest request)
    {
        var data = _userStatusService.GetPaged(request);
        return View(data);
    }

    public async Task<PartialViewResult> Detail(int id)
    {
        var data = _userStatusService.GetById(id);
        return PartialView("_Detail", data.Data);
    }

    [HttpGet]
    public PartialViewResult Create()
    {
        var data = new UserStatus();

        return PartialView("_Create", data);
    }

    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<ActionResult> Create(UserStatus userStatus)
    {
        try
        {
            if (ModelState.IsValid)
            {
                ServiceResponse<bool> isInserted = _userStatusService.Insert(userStatus);
                if (isInserted.Data) return Json(new { IsSuccess = true, Redirect = Url.Action("Index") });
            }
        }
        catch
        {
            ModelState.AddModelError("", "Unable to save changes");
        }

        return PartialView("_Create", userStatus);
    }

    [HttpGet]
    public async Task<PartialViewResult> Edit(int? id)
    {
        var model = new UserStatus();
        if (id.HasValue) model = _userStatusService.GetById(id.Value).Data;

        return PartialView("_Edit", model);
    }

    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<ActionResult> Edit(UserStatus userStatus)
    {
        try
        {
            if (ModelState.IsValid)
            {
                ServiceResponse<bool> isUpdated = _userStatusService.Update(userStatus);
                if (isUpdated.Data) return Json(new { IsSuccess = true, Redirect = Url.Action("Index") });
            }
        }
        catch
        {
            ModelState.AddModelError("", "Unable to save changes.");
        }

        return PartialView("_Edit", userStatus);
    }

    [HttpGet]
    public async Task<PartialViewResult> Delete(int id)
    {
        var data = _userStatusService.GetById(id);
        return PartialView("_Delete", data.Data);
    }

    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<ActionResult> DeleteConfirmed(int id)
    {
        return Json(new { IsSuccess = _userStatusService.DeleteById(id).Data, Redirect = Url.Action("Index") });
    }
}