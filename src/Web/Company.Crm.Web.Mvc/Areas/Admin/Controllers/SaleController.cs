using Company.Crm.Application.Constants;
using Company.Crm.Application.Dtos.Sale;
using Company.Crm.Application.Services.Abstracts;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Company.Crm.Web.Mvc.Areas.Admin.Controllers;

//[Authorize(Roles = RoleNameConsts.Administrator + "," + RoleNameConsts.SalesManager)]
//[Authorize(Roles = RoleNameConsts.SalesManager)]
[Authorize(Roles = RoleNameConsts.Administrator)]
[Area("Admin")]
public class SaleController : Controller
{
    private readonly ISaleService _saleService;

    public SaleController(ISaleService saleService)
    {
        _saleService = saleService;
    }

    public IActionResult Index()
    {
        var sales = _saleService.GetAll();
        return View(sales);
    }

    public async Task<PartialViewResult> Detail(int id)
    {
        var customer = _saleService.GetById(id);
        return PartialView("_Detail", customer);
    }

    [HttpGet]
    public PartialViewResult Create()
    {
        var dto = new CreateOrUpdateSaleDto();
        return PartialView("_Create", dto);
    }

    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<ActionResult> Create(CreateOrUpdateSaleDto item)
    {
        try
        {
            if (ModelState.IsValid)
            {
                var isInserted = _saleService.Insert(item);
                if (isInserted)
                    return Json(new { IsSuccess = true, Redirect = Url.Action("Index") });
            }
        }
        catch
        {
            ModelState.AddModelError("", "Unable to save changes.");
        }

        return PartialView("_Create", item);
    }

    public async Task<PartialViewResult> Delete(int id)
    {
        var serviceItem = _saleService.GetById(id);

        return PartialView("_Delete", serviceItem);
    }

    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<ActionResult> DeleteConfirmed(int id)
    {
        return Json(new { IsSuccess = _saleService.DeleteById(id), Redirect = Url.Action("Index") });
    }

    public async Task<PartialViewResult> Edit(int? id)
    {
        var dto = new CreateOrUpdateSaleDto();
        if (id.HasValue) dto = _saleService.GetForEditById(id.Value);
        return PartialView("_Edit", dto);
    }

    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<ActionResult> Edit(CreateOrUpdateSaleDto dto)
    {
        try
        {
            if (ModelState.IsValid)
            {
                var isUpdated = _saleService.Update(dto);
                if (isUpdated)
                    return Json(new { IsSuccess = true, Redirect = Url.Action("Index") });
            }
        }
        catch
        {
            ModelState.AddModelError("", "Unable to save changes.");
        }

        return PartialView("_Edit", dto);
    }
}