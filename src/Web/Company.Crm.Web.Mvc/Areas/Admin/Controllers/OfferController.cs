using Company.Crm.Application.Constants;
using Company.Crm.Application.Dtos.Offer;
using Company.Crm.Application.Services.Abstracts;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Company.Crm.Web.Mvc.Areas.Admin.Controllers;

[Authorize(Roles = RoleNameConsts.Administrator)]
[Area("Admin")]
public class OfferController : Controller
{
    private readonly IOfferService _offerService;

    public OfferController(IOfferService offerService)
    {
        _offerService = offerService;
    }

    public IActionResult Index()
    {
        var list = _offerService.GetAll();
        return View(list);
    }

    public async Task<PartialViewResult> Detail(int id)
    {
        var entity = _offerService.GetById(id);
        return PartialView("_Detail", entity);
    }

    [HttpGet]
    public PartialViewResult Create()
    {
        var entity = new OfferDto();
        return PartialView("_Create", entity);
    }

    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<ActionResult> Create(OfferDto st)
    {
        try
        {
            if (ModelState.IsValid)
            {
                var isInserted = _offerService.Insert(st);
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
        var entity = new OfferDto();

        if (id.HasValue)
            entity = _offerService.GetById(id.Value);

        return PartialView("_Edit", entity);
    }

    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<ActionResult> Edit(OfferDto st)
    {
        try
        {
            if (ModelState.IsValid)
            {
                var isUpdated = _offerService.Update(st);
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
        var entity = _offerService.GetById(id);
        return PartialView("_Delete", entity);
    }

    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<ActionResult> DeleteConfirmed(int id)
    {
        return Json(new { IsSuccess = _offerService.DeleteById(id), Redirect = Url.Action("Index") });
    }
}