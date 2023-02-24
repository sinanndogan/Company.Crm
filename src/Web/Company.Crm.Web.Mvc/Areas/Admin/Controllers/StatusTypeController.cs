using Company.Crm.Application.Constants;
using Company.Crm.Application.Services.Abstracts;
using Company.Crm.Domain.Entities.Lst;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Company.Crm.Web.Mvc.Areas.Admin.Controllers;

[Authorize(Roles = RoleNameConsts.Administrator)]
[Area("Admin")]
public class StatusTypeController : Controller
{
    private readonly IStatusTypeService _service;

    public StatusTypeController(IStatusTypeService service)
    {
        _service = service;
    }

    public IActionResult Index()
    {
        var list = _service.GetAll();
        return View(list);
    }

    public async Task<PartialViewResult> Detail(int id)
    {
        var entity = _service.GetById(id);
        return PartialView("_Detail", entity);
    }

    [HttpGet]
    public PartialViewResult Create()
    {
        var entity = new StatusType();
        return PartialView("_Create", entity);
    }

    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<ActionResult> Create(StatusType st)
    {
        try
        {
            if (ModelState.IsValid)
            {
                var isInserted = _service.Insert(st);
                if (isInserted) return Json(new { IsSuccess = true, Redirect = Url.Action("Index") });
            }
        }
        catch
        {
            ModelState.AddModelError("", "Unable to save changes.");
        }

        return PartialView("_Create", st);
    }

    public async Task<PartialViewResult> Edit(int? id)
    {
        var entity = new StatusType();

        if (id.HasValue)
            entity = _service.GetById(id.Value);

        return PartialView("_Edit", entity);
    }

    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<ActionResult> Edit(StatusType st)
    {
        try
        {
            if (ModelState.IsValid)
            {
                var isUpdated = _service.Update(st);
                if (isUpdated) return Json(new { IsSuccess = true, Redirect = Url.Action("Index") });
            }
        }
        catch
        {
            ModelState.AddModelError("", "Unable to save changes.");
        }

        return PartialView("_Edit", st);
    }

    [HttpGet]
    public async Task<PartialViewResult> Delete(int id)
    {
        var entity = _service.GetById(id);
        return PartialView("_Delete", entity);
    }

    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<ActionResult> DeleteConfirmed(int id)
    {
        return Json(new { IsSuccess = _service.DeleteById(id), Redirect = Url.Action("Index") });
    }
}