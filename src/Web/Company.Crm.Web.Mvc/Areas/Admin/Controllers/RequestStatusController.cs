using Company.Crm.Application.Constants;
using Company.Crm.Application.Services.Abstracts;
using Company.Crm.Domain.Entities.Lst;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Company.Crm.Web.Mvc.Areas.Admin.Controllers;

[Authorize(Roles = RoleNameConsts.Administrator)]
[Area("Admin")]
public class RequestStatusController : Controller
{
    private readonly IRequestStatusService _service;

    public RequestStatusController(IRequestStatusService service)
    {
        _service = service;
    }

    public IActionResult Index(int page = 1)
    {
        var list = _service.GetPaged(page);
        return View(list);
    }


    public async Task<PartialViewResult> Detail(int id)
    {
        var data = _service.GetById(id);
        return PartialView("_Detail", data);
    }

    [HttpGet]
    public PartialViewResult Create()
    {
        var data = new RequestStatus();
        return PartialView("_Create", data);
    }


    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<ActionResult> Create(RequestStatus requestStatus)
    {
        try
        {
            if (ModelState.IsValid)
            {
                var isInserted = _service.Insert(requestStatus);
                if (isInserted) return Json(new { IsSuccess = true, Redirect = Url.Action("Index") });
            }
        }
        catch
        {
            ModelState.AddModelError("", "Unable to save changes.");
        }

        return PartialView("_Create", requestStatus);
    }

    public async Task<PartialViewResult> Edit(int? id)
    {
        var model = new RequestStatus();
        if (id.HasValue) model = _service.GetById(id.Value);

        return PartialView("_Edit", model);
    }

    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<ActionResult> Edit(RequestStatus requestStatus)
    {
        try
        {
            if (ModelState.IsValid)
            {
                var isUpdated = _service.Update(requestStatus);
                if (isUpdated) return Json(new { IsSuccess = true, Redirect = Url.Action("Index") });
            }
        }
        catch
        {
            ModelState.AddModelError("", "Unable to save changes.");
        }

        return PartialView("_Edit", requestStatus);
    }


    [HttpGet]
    public async Task<PartialViewResult> Delete(int id)
    {
        var data = _service.GetById(id);
        return PartialView("_Delete", data);
    }


    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<ActionResult> DeleteConfirmed(int id)
    {
        return Json(new { IsSuccess = _service.DeleteById(id), Redirect = Url.Action("Index") });
    }
}