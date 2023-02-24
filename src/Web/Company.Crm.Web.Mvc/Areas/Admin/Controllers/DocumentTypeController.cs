using Company.Crm.Application.Constants;
using Company.Crm.Application.Services.Abstracts;
using Company.Crm.Domain.Entities.Lst;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Company.Crm.Web.Mvc.Areas.Admin.Controllers;

[Authorize(Roles = RoleNameConsts.Administrator)]
[Area("Admin")]
public class DocumentTypeController : Controller
{
    private readonly IDocumentTypeService _service;

    public DocumentTypeController(IDocumentTypeService service)
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
        var data = new DocumentType();
        return PartialView("_Create", data);
    }


    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<ActionResult> Create(DocumentType documentType)
    {
        try
        {
            if (ModelState.IsValid)
            {
                var isInserted = _service.Insert(documentType);
                if (isInserted) return Json(new { IsSuccess = true, Redirect = Url.Action("Index") });
            }
        }
        catch
        {
            ModelState.AddModelError("", "Unable to save changes.");
        }

        return PartialView("_Create", documentType);
    }

    public async Task<PartialViewResult> Edit(int? id)
    {
        var model = new DocumentType();
        if (id.HasValue) model = _service.GetById(id.Value);

        return PartialView("_Edit", model);
    }

    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<ActionResult> Edit(DocumentType documentType)
    {
        try
        {
            if (ModelState.IsValid)
            {
                var isUpdated = _service.Update(documentType);
                if (isUpdated) return Json(new { IsSuccess = true, Redirect = Url.Action("Index") });
            }
        }
        catch
        {
            ModelState.AddModelError("", "Unable to save changes.");
        }

        return PartialView("_Edit", documentType);
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