using Company.Crm.Application.Constants;
using Company.Crm.Application.Services;
using Company.Crm.Application.Services.Abstracts;
using Company.Crm.Domain.Entities.Lst;
using Company.Framework.Dtos;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Company.Crm.Web.Mvc.Areas.Admin.Controllers;

[Authorize(Roles = RoleNameConsts.Administrator)]
[Area("Admin")]
public class RegionController : Controller
{
    private readonly IRegionService _regionService;

    public RegionController(IRegionService regionService)
    {
        _regionService = regionService;
    }


    public IActionResult Index(PaginationRequest request) 
    {
        ServicePaginationResponse<List<Region>> regions = _regionService.GetPaged(request);
        return View(regions.Data);
    }


    public async Task<PartialViewResult> Detail(int id)
    {
        var regions = _regionService.GetById(id);
        return PartialView("_Detail", regions.Data);
    }


    [HttpGet]
    public PartialViewResult Create()
    {
        var region = new Region();

        return PartialView("_Create", region);
    }



    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<ActionResult> Create(Region region)
    {
        try
        {
            if (ModelState.IsValid)
            {
                ServiceResponse<bool> isInserted = _regionService.Insert(region);
                if (isInserted.Data)
                {
                    return Json(new { IsSuccess = true, Redirect = Url.Action("Index") });
                }
            }
        }
        catch
        {
            ModelState.AddModelError("", "Unable to save changes.");
        }

        return PartialView("_Create", region);
    }


    [HttpGet]
    public async Task<PartialViewResult> Edit(int? id)
    {
        var regions = new Region();
        if (id.HasValue)
        {
            regions = _regionService.GetForEditById(id.Value).Data;
        }

        return PartialView("_Edit", regions);


       
    }



    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<ActionResult> Edit(Region regions)
    {
        try
        {
            if (ModelState.IsValid)
            {
                var isUpdated = _regionService.Update(regions);
                if (isUpdated.Data)
                    return Json(new { IsSuccess = true, Redirect = Url.Action("Index") });
            }
        }
        catch
        {
            ModelState.AddModelError("", "Unable to save changes.");
        }

        return PartialView("_Edit", regions);

        

    }


    [HttpGet]
    public async Task<PartialViewResult> Delete(int id)
    {
        var region = _regionService.GetById(id);

        return PartialView("_Delete", region);
    }


    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<ActionResult> DeleteConfirmed(int id)
    {
        return Json(new { IsSuccess = _regionService.DeleteById(id).Data, Redirect = Url.Action("Index") });
    }
}
